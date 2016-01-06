using System;
using System.Xml.Serialization;

namespace JJ.Business.Synthesizer.Configuration
{
    public class ConfigurationSection
    {
        [XmlAttribute]
        public int? NameMaxLength { get; set; }
    }
}
