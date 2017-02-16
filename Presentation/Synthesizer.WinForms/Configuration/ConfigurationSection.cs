using System.Xml.Serialization;

namespace JJ.Presentation.Synthesizer.WinForms.Configuration
{
    internal class ConfigurationSection
    {
        [XmlAttribute]
        public string DefaultCulture { get; set; }

        [XmlAttribute]
        public bool AudioOutputEnabled { get; set; }

        [XmlAttribute]
        public bool MidiInputEnabled { get; set; }

        [XmlAttribute]
        public bool ExecuteOperatorMoveActionWhileDragging { get; set; }
    }
}
