using System;
using System.IO;
using System.Drawing;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

/// <summary>
/// 尚未使用此方法
/// </summary>
namespace TestExerciserPro.TEViews.AutoTesting.Logic
{
    /// <summary>
    /// 提供从操作系统读取图标的方法
    /// </summary>

    public class FileIcons
    {    
        public static string SetIcons(string filePath)
        {
            string iconPath = null;
            string iconFolder = "../Images/FileIcons/";
            string fileExt = Path.GetExtension(filePath);
            iconPath = iconFolder + fileExt + ".png";
            if (!File.Exists(iconPath)) iconPath = iconFolder + "default.png";
            return iconPath;
        }
    }
 }
