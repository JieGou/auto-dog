using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDog.Logics
{
    public static class Common
    {
        static string autoDogName = "AutoDog";
        static string myDocumentsPaths = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

        public static string ProjectLocalPath = myDocumentsPaths + @"\\" + autoDogName + "\\Projects";
        public static string SettingLocalPath = myDocumentsPaths + @"\\" + autoDogName + "\\Settings";
        public static string BackUpLocalPath = myDocumentsPaths + @"\\" + autoDogName + "\\Backup Files";
        public static string TemplatesLocalPath = myDocumentsPaths + @"\\" + autoDogName + "\\Templates";

        public enum TreeNodeType
        {
            RootNode = 0,
            FolderNode = 1,
            FileNode = 2,
        } 
    }
}
