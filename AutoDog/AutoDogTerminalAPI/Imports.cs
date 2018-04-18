using System;
using System.Runtime.InteropServices;

namespace AutoDogTerminalAPI
{
    internal static class Imports
    {
        /************************************************************************* 
  向控制台进程组发送一个指定的信号，该组共享与呼叫过程相关联的控制台。

  是要生成的信号的类型。
  接收信号的过程组的标识符。当createnewprocessgroup标志在给CreateProcess函数的调用中指定时，就会创建一个进程组。新流程的工艺标识符也是新流程组的流程组标识符。过程组包括所有作为根进程的后代的进程。只有与调用过程共享同一台主机的组中的进程才会接收到信号。换句话说，如果团队中的某个进程创建了一个新的控制台，那么这个过程就不会接收到信号，也不会接收到它的后代。
  如果这个参数为零，那么信号就会在共享调用过程控制台的所有进程中生成。
  如果函数成功了，返回值是非零的。
  如果函数失败，返回值为零。要获得扩展的错误信息，请调用GetLastError。
         *************************************************************************/
        [DllImport("Kernel32.dll")]
        public static extern bool GenerateConsoleCtrlEvent(CTRL_EVENT dwCtrlEvent, UInt32 dwProcessGroupId);
    }

    /// <summary>
    /// The type of signal to be generated.
    /// </summary>
    internal enum CTRL_EVENT : uint
    {
        /***
         接收到ctrl+c信号
         * **/
        CTRL_C_EVENT = 0,
        /***
         * 生成一个 CTRL+BREAK信号。
         * **/
        CTRL_BREAK_EVENT = 1
    }
}