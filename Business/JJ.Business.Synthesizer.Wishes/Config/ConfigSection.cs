using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Business.Synthesizer.Wishes.docs;
using JJ.Framework.Logging.Core.Mappers;

namespace JJ.Business.Synthesizer.Wishes.Config
{
    /// <inheritdoc cref="_leafchecktimeout" />
    public enum TimeOutActionEnum
    {
        Undefined,
        Continue,
        Log,
        Stop
    }

    [DebuggerDisplay("{DebuggerDisplay}")]
    internal class ConfigSection
    {
        string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
        
        // Audio Quality
        
        [XmlAttribute] public int? Bits { get; set; }
        [XmlAttribute] public int? Channels { get; set; }
        [XmlAttribute] public int? SamplingRate { get; set; }
        [XmlAttribute] public AudioFileFormatEnum? AudioFormat { get; set; }
        [XmlAttribute] public InterpolationTypeEnum? Interpolation { get; set; }
        [XmlAttribute] public int? CourtesyFrames { get; set; }
        
        // Durations
        
        /// <inheritdoc cref="_notelength" />
        [XmlAttribute] public double? NoteLength { get; set; }
        [XmlAttribute] public double? BarLength { get; set; }
        [XmlAttribute] public double? BeatLength { get; set; }
        [XmlAttribute] public double? AudioLength { get; set; }
        [XmlAttribute] public double? LeadingSilence { get; set; }
        [XmlAttribute] public double? TrailingSilence { get; set; }
        
        // Feature Toggles
        
        [XmlAttribute] public bool? AudioPlayback { get; set; }
        [XmlAttribute] public bool? DiskCache { get; set; }
        [XmlAttribute] public bool? MathBoost { get; set; }
        [XmlAttribute] public bool? ParallelProcessing { get; set; }
        [XmlAttribute] public bool? PlayAllTapes { get; set; }
        
        // Tooling
        
        public ConfigToolingElement AzurePipelines { get; set; } 
        public ConfigToolingElement NCrunch { get; set; }
        public RootLoggerXml Logging { get; set; }
        
        // Misc
        
        /// <inheritdoc cref="_leafchecktimeout" />
        [XmlAttribute] public double? LeafCheckTimeOut { get; set; }
        /// <inheritdoc cref="_leafchecktimeout" />
        [XmlAttribute] public TimeOutActionEnum? TimeOutAction { get; set; }
        [XmlAttribute] public int? FileExtensionMaxLength { get; set; }
        [XmlAttribute] public string LongTestCategory { get; set; }
    }
}