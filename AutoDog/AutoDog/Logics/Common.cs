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

        public static string ProjectLocalPath = myDocumentsPaths  + @"\"+ autoDogName + "\\Projects"; //工程文件路径
        public static string SettingLocalPath = myDocumentsPaths  + @"\" + autoDogName + "\\Settings"; //设置文件路径
        public static string BackUpLocalPath = myDocumentsPaths + @"\" + autoDogName + "\\Backup Files"; //备份文件路径
        public static string TemplatesLocalPath = myDocumentsPaths + @"\" + autoDogName + "\\Templates"; //模板文件路径

        public enum TreeNodeType
        {
            RootNode = 0, //根节点
            FolderNode = 1,//文件夹节点
            FileNode = 2, //文件节点
        } 

        public enum ProjectType
        {
            Python,
            CSharp,
            Java,
            Ruby,
            API,
            Performance,
        }
    }
}
