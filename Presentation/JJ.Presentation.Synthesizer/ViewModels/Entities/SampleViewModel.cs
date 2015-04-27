using JJ.Business.CanonicalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    public sealed class SampleViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Amplifier { get; set; }
        public double TimeMultiplier { get; set; }
        public bool IsActive { get; set; }
        public int SamplingRate { get; set; }
        public int BytesToSkip { get; set; }
        public string Location { get; set; }
        public IDName SampleDataType { get; set; }
        public IDName SpeakerSetup { get; set; }
        public IDName AudioFileFormat { get; set; }
        public IDName InterpolationType { get; set; }
    }
}
