using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Markup;
using System.Xml.Serialization;
using Standard;
using System.Xml;
using System.Xml.Schema;

namespace AutoDog.ViewModels.Serialization
{
    [ContentProperty("APIRoot")]
    [Serializable]
    public class APIRoot :ViewModelBase , IXmlSerializable
    {
        public APIRoot()
        {

        }

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            if (reader.IsEmptyElement)
            {
                reader.Read();
                return;
            }
        }

        public string APITestCase{ get; set; }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("APIRoot");
            if (APITestCase != null)
            {
                //APITestCase.WriteXml(writer);
            }
            writer.WriteEndElement();

            // Write all floating windows (can be LayoutDocumentFloatingWindow or LayoutAnchorableFloatingWindow).
            // To prevent "can not create instance of abstract type", the type is retrieved with GetType().Name
            //writer.WriteStartElement("FloatingWindows");
            //foreach (var layoutFloatingWindow in FloatingWindows)
            //{
            //    writer.WriteStartElement(layoutFloatingWindow.GetType().Name);
            //    layoutFloatingWindow.WriteXml(writer);
            //    writer.WriteEndElement();
            //}
            //writer.WriteEndElement();

            //writer.WriteStartElement("Hidden");
            //foreach (var layoutAnchorable in Hidden)
            //{
            //    layoutAnchorable.WriteXml(writer);
            //}
            //writer.WriteEndElement();
        }
    }
}
