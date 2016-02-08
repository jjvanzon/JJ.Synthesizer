using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels.Entities;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class ScalePropertiesViewModel : ViewModelBase
    {
        public ScaleViewModel Entity { get; set; }
        public IList<IDAndName> ScaleTypeLookup { get; set; }
    }
}
