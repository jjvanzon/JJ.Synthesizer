using System.Xml.Serialization;

namespace JJ.Presentation.Synthesizer.WinForms.Configuration
{
    internal class ConfigurationSection
    {
        [XmlAttribute]
        public bool AlwaysRecreateDiagram { get; set; }

        [XmlAttribute]
        public string DefaultCulture { get; set; }

        [XmlAttribute]
        public bool MultiThreaded { get; set; }

        [XmlAttribute]
        public bool AudioOutputEnabled { get; set; }

        [XmlAttribute]
        public bool MidiInputEnabled { get; set; }
    }
}
