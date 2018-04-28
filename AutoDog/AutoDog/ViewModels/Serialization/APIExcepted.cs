using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AutoDog.ViewModels.Serialization
{
    public class APIExcepted
    {
        [XmlElement("Expect")]
        public string Expect { get; set; }
    }
}
