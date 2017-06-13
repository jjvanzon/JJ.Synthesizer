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

        public bool UnderlyingPatchVisible { get; set; }
        /// <summary>
        /// The lookup is inside the DocumentViewModel,
        /// to prevent a lot of repeated data. So use the lookup from there.
        /// </summary>
        public IDAndName UnderlyingPatch { get; set; }

        /// <summary> not displayed </summary>
        public bool HasDimension { get; set; }

        internal int? OutletIDToPlay { get; set; }
    }
}
