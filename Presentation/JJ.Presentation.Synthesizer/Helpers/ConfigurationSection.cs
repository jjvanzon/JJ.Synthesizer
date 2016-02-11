using System.Xml.Serialization;

namespace JJ.Presentation.Synthesizer.Helpers
{
    public class ConfigurationSection
    {
        [XmlAttribute]
        public string PatchPlayHackedAudioFileOutputFilePath { get; set; }

        [XmlAttribute]
        public double PatchPlayDurationInSeconds { get; set; }
    }
}
