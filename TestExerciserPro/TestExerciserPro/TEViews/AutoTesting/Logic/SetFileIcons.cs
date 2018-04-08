using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExerciserPro.TEViews.AutoTesting.Logic
{
    class SetFileIcons
    {
        public static string setFileIcon(string filePath)
        {
            string fileType = System.IO.Path.GetExtension(filePath);
            string imagePath = null;
            switch(fileType)
            {
                case ".txt":
                    imagePath = @"../Images/FileIcons/txt.png";
                    break;
                case ".py":
                    imagePath = @"../Images/FileIcons/python.png";
                    break;
                case ".cs":
                    imagePath = @"../Images/FileIcons/txt.png";
                    break;
                case ".html":
                    imagePath = @"../Images/FileIcons/txt.png";
                    break;
                case ".zip":
                    imagePath = @"../Images/FileIcons/txt.png";
                    break;
                default:
                    imagePath = @"../Images/FileIcons/default.png";
                    break;
            }
            return imagePath;
        }
    }
}
