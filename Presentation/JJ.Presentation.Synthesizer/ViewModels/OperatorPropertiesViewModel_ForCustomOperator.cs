using JJ.Data.Canonical;
using System.Collections.Generic;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel_ForCustomOperator : OperatorPropertiesViewModelBase
    {
        /// <summary>
        /// The lookup is inside the DocumentViewModel,
        /// to prevent a lot of repeated data. So use the lookup from there.
        /// </summary>
        public ChildDocumentIDAndNameViewModel UnderlyingPatch { get; set; }
    }
}
