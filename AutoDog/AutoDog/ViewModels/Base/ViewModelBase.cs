using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.ComponentModel;
using NHotkey.Wpf;

namespace AutoDog.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged, IDataErrorInfo, IDisposable
    {   
        protected ViewModelBase()
        {

        }

        #region IDisposable
        public void Dispose()
        {
            HotkeyManager.Current.Remove("demo");
        }

        #endregion


        #region IDataErrorInfo
        public string Error { get { return string.Empty; } }

        public string this[string columnName]
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        #region Event Handlers

        /// <summary>
        /// 获取属性名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetPropertyName<T>(Expression<Func<T>> e)
        {
            var member = (MemberExpression)e.Body;
            return member.Member.Name;
        }
        /// <summary>
        /// 属性改变触发事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyExpression"></param>
        protected virtual void RaisePropertyChanged<T>
            (Expression<Func<T>> propertyExpression)
        {
            RaisePropertyChanged(GetPropertyName(propertyExpression));
        }
        /// <summary>
        /// 属性改变触发事件
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void RaisePropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

    }
}
