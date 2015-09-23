using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.CanonicalModel;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    public sealed class ScaleViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public IDAndName ScaleType { get; set; }
        public double? BaseFrequency { get; set; }
        public IList<ToneViewModel> Tones { get; set; }
    }
}
