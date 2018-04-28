using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AutoDog.ViewModels.Serialization
{
    [XmlRoot("APITestCase")]
    public class APIEntity
    {
        [XmlAttribute("TestSuite")]
        public string TestSuite { get; set; }

        [XmlElement("Request")]
        public APIRequest Request { get; set; }

        [XmlElement("Response")]
        public APIResponse Response { get; set; }

        [XmlElement("Excepted")]
        public APIExcepted Excepted { get; set; }
    }
}
