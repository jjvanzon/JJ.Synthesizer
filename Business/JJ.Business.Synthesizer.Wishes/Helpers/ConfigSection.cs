using System.Xml.Serialization;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    internal class ConfigSection
    {
        [XmlAttribute] public int? DefaultSamplingRate { get; set; } 
        [XmlAttribute] public string LongRunningTestCategory { get; set;}
        [XmlAttribute] public bool? PlayAudioEnabled { get; set;}

        public ToolingConfiguration AzurePipelines { get; set; } = new ToolingConfiguration();
        public ToolingConfiguration NCrunch { get; set; } = new ToolingConfiguration();
    }

    internal class ToolingConfiguration
    {
        [XmlAttribute] public int? SamplingRate { get; set;}
        [XmlAttribute] public int? SamplingRateLongRunning { get; set;}
        [XmlAttribute] public bool? PlayAudioEnabled { get; set;}
        [XmlAttribute] public bool? Pretend { get; set;}
    }
}