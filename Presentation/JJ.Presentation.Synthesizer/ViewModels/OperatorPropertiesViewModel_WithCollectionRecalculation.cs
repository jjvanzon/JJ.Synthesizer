using JJ.Data.Canonical;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel_WithCollectionRecalculation : OperatorPropertiesViewModel
    {
        public IDAndName CollectionRecalculation { get; set; }
        public IList<IDAndName> CollectionRecalculationLookup { get; set; }
    }
}