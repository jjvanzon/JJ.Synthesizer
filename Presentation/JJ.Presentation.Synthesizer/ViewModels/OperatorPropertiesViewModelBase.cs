using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public abstract class OperatorPropertiesViewModelBase : ViewModelBase
    {
        public int ID { get; set; }
        public int PatchID { get; set; }
        public string Name { get; set; }

        public bool DimensionVisible { get; set; }
        public IDAndName Dimension { get; set; }
        public IList<IDAndName> DimensionLookup { get; set; }
        public string CustomDimensionName { get; set; }
    }
}
