using JJ.Data.Canonical;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation : OperatorPropertiesViewModelBase
    {
        public int PatchID { get; internal set; }
        public string Name { get; set; }

        public IDAndName OperatorType { get; set; }
        public IDAndName Dimension { get; set; }
        public IList<IDAndName> DimensionLookup { get; set; }
        public IDAndName CollectionRecalculation { get; set; }
        public IList<IDAndName> CollectionRecalculationLookup { get; set; }
    }
}