using System.Xml.Serialization;

namespace JJ.Presentation.Synthesizer.Helpers
{
    internal class ConfigurationSection
    {
        [XmlAttribute]
        public string PatchPlayHackedAudioFileOutputFilePath { get; set; }

        [XmlAttribute]
        public double PatchPlayDurationInSeconds { get; set; }

        [XmlAttribute]
        public string TitleBarExtraText { get; set; }

        [XmlAttribute]
        public bool ShowAutoPatchPolyphonicEnabled { get; set; }
    }
}
