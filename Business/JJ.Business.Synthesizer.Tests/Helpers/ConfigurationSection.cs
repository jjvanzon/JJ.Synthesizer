using System.Xml.Serialization;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    public class ConfigurationSection
    {
        [XmlAttribute] public double DefaultOutputVolume { get; set; }
        [XmlAttribute] public double DefaultOutputDuration { get; set; }
        [XmlAttribute] public int DefaultSamplingRate { get; set; }
        [XmlAttribute] public int AzurePipelinesSamplingRate { get; set; }
        [XmlAttribute] public int NCrunchSamplingRate { get; set; }
    }
}