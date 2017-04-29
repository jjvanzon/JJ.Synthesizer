using System.Xml.Serialization;

namespace JJ.Presentation.Synthesizer.Helpers
{
    internal class ConfigurationSection
    {
        [XmlAttribute]
        public string PlayActionOutputFilePath { get; set; }

        [XmlAttribute]
        public double PlayActionDurationInSeconds { get; set; }

        [XmlAttribute]
        public string TitleBarExtraText { get; set; }
    }
}
