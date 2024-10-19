using System.Xml.Serialization;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    internal class ConfigurationSection
    {
        [XmlAttribute] public int DefaultSamplingRate { get; set; }
        [XmlAttribute] public string LongRunningTestCategory { get; set; }
        [XmlAttribute] public bool PlayAudioAfterSave { get; set; }
        [XmlAttribute] public bool PlaySynchronous { get; set; }

        public ToolingConfiguration AzurePipelines { get; set; }
        public ToolingConfiguration NCrunch { get; set; }

    }

    internal class ToolingConfiguration
    {
        [XmlAttribute] public int SamplingRate { get; set; }
        [XmlAttribute] public int SamplingRateLongRunning { get; set; }
        [XmlAttribute] public bool PlayAudioAfterSave { get; set; }
    }
}