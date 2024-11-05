using System.Xml.Serialization;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    internal class ConfigSection
    {
        [XmlAttribute] public int? DefaultSamplingRate { get; set; }
        [XmlAttribute] public SpeakerSetupEnum? DefaultSpeakerSetup { get; set; }
        [XmlAttribute] public int? DefaultBitDepth { get; set; }
        [XmlAttribute] public AudioFileFormatEnum? DefaultAudioFormat { get; set; }
        [XmlAttribute] public InterpolationTypeEnum? DefaultInterpolation { get; set; }
        [XmlAttribute] public double? DefaultAudioLength { get; set; }
        [XmlAttribute] public string LongRunningTestCategory { get; set; }
        [XmlAttribute] public bool? PlayEnabled { get; set; }
        [XmlAttribute] public double? PlayLeadingSilence { get; set; }
        [XmlAttribute] public double? PlayTrailingSilence { get; set; }
        [XmlAttribute] public bool? ParallelEnabled { get; set; }
        [XmlAttribute] public bool? CacheToDisk { get; set; }

        public ToolingConfiguration AzurePipelines { get; set; } = new ToolingConfiguration();
        public ToolingConfiguration NCrunch { get; set; } = new ToolingConfiguration();
    }

    internal class ToolingConfiguration
    {
        [XmlAttribute] public int? SamplingRate { get; set; }
        [XmlAttribute] public int? SamplingRateLongRunning { get; set; }
        [XmlAttribute] public bool? PlayEnabled { get; set; }
        [XmlAttribute] public bool? Pretend { get; set; }
    }
}