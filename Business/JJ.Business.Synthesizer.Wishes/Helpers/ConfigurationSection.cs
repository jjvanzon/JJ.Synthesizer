using System.Xml.Serialization;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    public class ConfigurationSection
    {
        [XmlAttribute] public double DefaultOutputVolume                   { get; set; }
        [XmlAttribute] public double DefaultOutputDuration                 { get; set; }
        [XmlAttribute] public int    DefaultSamplingRate                   { get; set; }
        [XmlAttribute] public int    AzurePipelinesSamplingRate            { get; set; }
        [XmlAttribute] public int    AzurePipelinesSamplingRateLongRunning { get; set; }
        [XmlAttribute] public int    NCrunchSamplingRate                   { get; set; }
        [XmlAttribute] public int    NCrunchSamplingRateLongRunning        { get; set; }
        [XmlAttribute] public string LongRunningTestCategory               { get; set; }
    }
}