using System.Xml.Serialization;

namespace JJ.Presentation.Synthesizer.WinForms.Configuration
{
    internal class ConfigurationSection
    {
        [XmlAttribute]
        public string DefaultCulture { get; set; }

        [XmlAttribute]
        public bool ExecuteOperatorMoveActionWhileDragging { get; set; }

        [XmlAttribute]
        public string PlayActionOutputFilePath { get; set; }

        [XmlAttribute]
        public double PlayActionDurationInSeconds { get; set; }
    }
}
