using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using JJ.Framework.Logging.Core.Mappers;

namespace JJ.Business.Synthesizer.Wishes.Config
{
    internal class ConfigToolingElement
    {
        [XmlAttribute] public bool? AudioPlayback           { get; set; }
        [XmlAttribute] public int?  SamplingRate            { get; set; }
        // TODO: Remove outcommented
        // Removing "IsLongTestCategory" feature gets rid of Testing.Core dependency
        //[XmlAttribute] public int?  SamplingRateLongRunning { get; set; }
        [XmlAttribute] public bool? ImpersonationMode       { get; set; }
        
        public RootLoggerXml Logging { get; set; } = new();
    }
}