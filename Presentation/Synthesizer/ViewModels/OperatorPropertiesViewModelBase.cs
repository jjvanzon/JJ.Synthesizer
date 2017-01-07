using System.Collections.Generic;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public abstract class OperatorPropertiesViewModelBase : ViewModelBase
    {
        public int ID { get; set; }
        public int PatchID { get; set; }
        public string Name { get; set; }

        /// <summary> not editable </summary>
        public IDAndName OperatorType { get; set; }

        public bool StandardDimensionVisible { get; set; }
        public IDAndName StandardDimension { get; set; }
        public IList<IDAndName> StandardDimensionLookup { get; set; }

        public bool CustomDimensionNameVisible { get; set; }
        public string CustomDimensionName { get; set; }
    }
}
