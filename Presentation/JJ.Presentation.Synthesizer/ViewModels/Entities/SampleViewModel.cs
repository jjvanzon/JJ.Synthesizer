using JJ.Business.CanonicalModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    public sealed class SampleViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int SamplingRate { get; set; }
        public IDAndName AudioFileFormat { get; set; }
        public IDAndName SampleDataType { get; set; }
        public IDAndName SpeakerSetup { get; set; }
        public double Amplifier { get; set; }
        public double TimeMultiplier { get; set; }
        public bool IsActive { get; set; }
        public int BytesToSkip { get; set; }
        public IDAndName InterpolationType { get; set; }
        public string OriginalLocation { get; set; }
        public byte[] Bytes { get; set; }
        /// <summary> read-only </summary>
        public double Duration { get; set; }
    }
}
