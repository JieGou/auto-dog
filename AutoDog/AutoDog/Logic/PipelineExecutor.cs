using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Threading;

namespace AutoDog.Logic
{
    /// <summary>
    /// 帮助异步执行和检索powershell脚本管道的结果的类。
    /// </summary>
    public class PipelineExecutor
    {
        #region public types, members
        /// <summary>
        /// 获得与该管道执行器相关联的powershell管道
        /// </summary>
        public Pipeline Pipeline
        {
            get
            {
                return pipeline;
            }
        }

        public delegate void DataReadyDelegate(PipelineExecutor sender, ICollection<PSObject> data);
        public delegate void DataEndDelegate(PipelineExecutor sender);
        public delegate void ErrorReadyDelegate(PipelineExecutor sender, ICollection<object> data);

        /// <summary>
        /// 当powershell脚本中有新数据可用时就会发生。
        /// </summary>
        public event DataReadyDelegate OnDataReady;

        /// <summary>
        /// 当powershell脚本完成它的执行时发生。
        /// </summary>
        public event DataEndDelegate OnDataEnd;

        /// <summary>
        /// 发生在powershell脚本中有新的errordata时。
        /// </summary>
        public event ErrorReadyDelegate OnErrorReady;

        #endregion

        #region private types, members

        /// <summary>
        /// 用来调用事件的对象。
        /// </summary>
        private ISynchronizeInvoke invoker;

        /// <summary>
        /// 将异步执行的powershell脚本流水线。
        /// </summary>
        private Pipeline pipeline;

        /// <summary>
        ///一个私有方法的本地委托
        /// </summary>
        private DataReadyDelegate synchDataReady;

        /// <summary>
        /// 一个私有方法的本地委托
        /// </summary>
        private DataEndDelegate synchDataEnd;

        /// <summary>
        /// 一个私有方法的本地委托
        /// </summary>
        private ErrorReadyDelegate synchErrorReady;

        /// <summary>
        /// 当用户想要停止脚本执行时，事件集。
        /// </summary>
        private ManualResetEvent stopEvent;

        /// <summary>
        /// 在StoppableInvoke（）方法中使用的waithandles数组。
        /// </summary>
        private WaitHandle[] waitHandles;
        #endregion

        #region public methods
        /// <summary>
        /// 构造器，为给定的powershell脚本创建一个新的管道执行器。
        /// </summary>
        /// <param name="runSpace">用于创建和执行脚本的Powershell runspace。</param>
        /// <param name="invoker">将DataReady和DataEnd事件同步的对象。
        /// 通常你会在这里通过表单或组件。</param>
        /// <param name="command">The script to run</param>
        public PipelineExecutor(Runspace runSpace, ISynchronizeInvoke invoker, string command)
        {
            this.invoker = invoker;

            // 初始化委托
            synchDataReady = new DataReadyDelegate(SynchDataReady);
            synchDataEnd = new DataEndDelegate(SynchDataEnd);
            synchErrorReady = new ErrorReadyDelegate(SynchErrorReady);

            // 初始化事件成员
            stopEvent = new ManualResetEvent(false);
            waitHandles = new WaitHandle[] { null, stopEvent };
            // 创建一条管道并将其提供给脚本文本
            pipeline = runSpace.CreatePipeline(command);

            // 我们将通过DataReady事件来侦听脚本输出数据
            pipeline.Output.DataReady += new EventHandler(Output_DataReady);
            pipeline.Error.DataReady += new EventHandler(Error_DataReady);
        }

        void Error_DataReady(object sender, EventArgs e)
        {
            // 获取所有可用的对象
            Collection<object> data = pipeline.Error.NonBlockingRead();

            // 如果有的话，调用ErrorReady事件
            if (data.Count > 0)
            {
                StoppableInvoke(synchErrorReady, new object[] { this, data });
            }
        }

        /// <summary>
        /// 开始在后台执行脚本
        /// </summary>
        public void Start()
        {
            if (pipeline.PipelineStateInfo.State == PipelineState.NotStarted)
            {
                // 关闭管道输入。如果你忘记了，它就不会开始处理脚本了。
                pipeline.Input.Close();
                // 调用管道。这将导致它在后台处理脚本。
                pipeline.InvokeAsync();
            }
        }

        /// <summary>
        /// 停止执行
        /// </summary>
        public void Stop()
        {
            // 首先确保StoppableInvoke（）停止阻塞
            stopEvent.Set();
            // 然后告诉管道停止脚本
            pipeline.Stop();
        }
        #endregion

        #region private methods

        /// <summary>
        /// 特殊的调用方法，它的操作类似于<参考 cref="ISynchronizeInvoke.Invoke"/> ,但除此之外,它可以通过设置stopEvent终止。这就避免了使用常规时可能出现的死锁。 
        /// <see cref="ISynchronizeInvoke.Invoke"/> method.
        /// </summary>
        /// <param name="method">A <see cref="System.Delegate"/> 对于一种方法，它接受包含在其中的相同数量和类型的参数<paramref name="args"/></param>
        /// <param name="args">An array of type <see cref="System.Object"/> to pass as arguments to the given method. This can be null if 
        /// no arguments are needed </param>
        /// <returns>The <see cref="Object"/> returned by the asynchronous operation</returns>
        private object StoppableInvoke(Delegate method, object[] args)
        {
            IAsyncResult asyncResult = invoker.BeginInvoke(method, args);
            waitHandles[0] = asyncResult.AsyncWaitHandle;
            return (WaitHandle.WaitAny(waitHandles) == 0) ? invoker.EndInvoke(asyncResult) : null;
        }

        /// <summary>
        /// powershell脚本管道的DataReady事件的事件处理程序。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Output_DataReady(object sender, EventArgs e)
        {
            // 获取所有可用的对象
            Collection<PSObject> data = pipeline.Output.NonBlockingRead();

            // 如果有的话，调用DataReady事件
            if (data.Count > 0)
            {
                StoppableInvoke(synchDataReady, new object[] { this, data });
            }

            if (pipeline.Output.EndOfPipeline)
            {
                // 我们完成了!调用DataEnd事件
                StoppableInvoke(synchDataEnd, new object[] { this });
            }
        }

        /// <summary>
        /// 私有DataReady处理方法，它将把调用传递给连接到OnDataReady事件的任何事件处理程序 <参考 cref="PipelineExecutor"/> 实例.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void SynchDataReady(PipelineExecutor sender, ICollection<PSObject> data)
        {
            DataReadyDelegate delegateDataReadyCopy = OnDataReady;
            if (delegateDataReadyCopy != null)
            {
                delegateDataReadyCopy(sender, data);
            }
        }

        /// <summary>
        ///私有数据端处理方法，它将把调用传递给附着在OnDataEnd事件上的任何处理程序<参考 cref="PipelineExecutor"/> 实例.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void SynchDataEnd(PipelineExecutor sender)
        {
            DataEndDelegate delegateDataEndCopy = OnDataEnd;
            if (delegateDataEndCopy != null)
            {
                delegateDataEndCopy(sender);
            }
        }

        /// <summary>
        /// 私有ErrorReady处理方法，它将把调用传递给连接到OnErrorReady事件的任何事件处理程序 <参考 cref="PipelineExecutor"/> 实例.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void SynchErrorReady(PipelineExecutor sender, ICollection<object> data)
        {
            ErrorReadyDelegate delegateErrorReadyCopy = OnErrorReady;
            if (delegateErrorReadyCopy != null)
            {
                delegateErrorReadyCopy(sender, data);
            }
        }

        #endregion
    }
}
