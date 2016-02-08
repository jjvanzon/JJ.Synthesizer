using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels.Entities;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class NodePropertiesViewModel : ViewModelBase
    {
        public NodeViewModel Entity { get; set; }
        public IList<IDAndName> NodeTypeLookup { get; set; }
    }
}
