using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JJ.Business.Synthesizer.Wishes.Config
{
    internal class ConfigToolingElement
    {
        [XmlAttribute] public bool? AudioPlayback           { get; set; }
        [XmlAttribute] public int?  SamplingRate            { get; set; }
        [XmlAttribute] public int?  SamplingRateLongRunning { get; set; }
        [XmlAttribute] public bool? ImpersonationMode       { get; set; }
    }
}