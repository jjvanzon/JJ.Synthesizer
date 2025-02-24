using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using JJ.Framework.Wishes.Logging;

namespace JJ.Business.Synthesizer.Wishes.Config
{
    internal class ConfigToolingElement
    {
        [XmlAttribute] public bool? AudioPlayback           { get; set; }
        [XmlAttribute] public int?  SamplingRate            { get; set; }
        [XmlAttribute] public int?  SamplingRateLongRunning { get; set; }
        [XmlAttribute] public bool? ImpersonationMode       { get; set; }
        
        public LoggingConfigSection Logging { get; set; }
    }
}