using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel_ForResample : OperatorPropertiesViewModelBase
    {
        // Properties put directly here, instead of entity view model,
        // because entity view model is too elaborate.

        public int PatchID { get; internal set; }
        public string Name { get; set; }

        /// <summary> not editable </summary>
        public IDAndName OperatorType { get; set; }

        public IDAndName Interpolation { get; set; }
        public IList<IDAndName> InterpolationLookup { get; set; }
        public IDAndName Dimension { get; set; }
        public IList<IDAndName> DimensionLookup { get; set; }
    }
}