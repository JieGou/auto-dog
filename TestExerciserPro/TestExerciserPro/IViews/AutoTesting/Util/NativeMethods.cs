using System;
using System.Runtime.InteropServices;

namespace TestExerciserPro.IViews.AutoTesting
{
    internal class NativeMethods
    {
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject(IntPtr hObject);
    }
}
