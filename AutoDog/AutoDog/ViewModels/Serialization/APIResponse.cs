using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AutoDog.ViewModels.Serialization
{
    public class APIResponse
    {
        [XmlElement("Body")]
        public string Body { get; set; }
    }
}
