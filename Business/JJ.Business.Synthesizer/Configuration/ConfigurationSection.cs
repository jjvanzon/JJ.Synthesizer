using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JJ.Business.Synthesizer.Configuration
{
    public class ConfigurationSection
    {
        [XmlAttribute]
        public int? NameMaxLength { get; set; }

        [XmlAttribute]
        public PatchCalculatorTypeEnum PatchCalculatorType { get; set; }
    }
}
