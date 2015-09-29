using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.ViewModels.Entities;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class ScalePropertiesViewModel
    {
        public ScaleViewModel Entity { get; set; }
        public IList<IDAndName> ScaleTypeLookup { get; set; }
        public IList<Message> ValidationMessages { get; set; }
        public bool Visible { get; set; }
        public bool Successful { get; set; }
    }
}
