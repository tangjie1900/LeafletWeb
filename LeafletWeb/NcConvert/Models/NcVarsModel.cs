using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using NcConvert.Utils;
using System.IO;

namespace NcConvert.Models
{
    [XmlRoot("NcVarsModel")]
    public class NcVarsModel
    {
        [XmlElement("NcVarModel")]
        public List<NcVarModel> models { get; set; }

        public static IList<NcVarModel> Load()
        {
            var result = XmlUtil.DeSerialize<NcVarsModel>(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.xml"));
            return result.models;
        }
    }

    public class NcVarModel : IModel
    {
        [XmlAttribute]
        public string LonVarName { get; set; }
        [XmlAttribute]
        public string LatVarName { get; set; }
        [XmlAttribute]
        public string TimeSteps { get; set; }
        [XmlAttribute]
        public string Value { get; set; }
        [XmlAttribute]
        public string Dir { get; set; }
    }
}
