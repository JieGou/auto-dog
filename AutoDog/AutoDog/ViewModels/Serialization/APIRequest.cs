using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AutoDog.ViewModels.Serialization
{
    public class APIRequest
    {
        [XmlElement("Type")]
        public string Type { get; set; }

        [XmlElement("Url")]
        public string Url { get; set; }
    }
}
