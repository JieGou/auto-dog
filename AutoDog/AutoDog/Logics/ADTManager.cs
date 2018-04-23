using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AutoDog.Logics
{
    class ADTManager
    {
        public void WriteADTFile()
        {
            //创建一个xml文档
            XmlDocument doc = new XmlDocument();
            //声明xml文档头部
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            //添加进doc子对象
            doc.AppendChild(dec);

            //创建根节点
            XmlElement root = doc.CreateElement("Solution");
            doc.AppendChild(root);

            //再创建根节点下的子节点
            XmlElement project = doc.CreateElement("Project");
            project.SetAttribute("", "");

        }
    }
}
