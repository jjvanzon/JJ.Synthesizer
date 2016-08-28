using JJ.Data.Canonical;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel_ForCurve : OperatorPropertiesViewModelBase
    {
        /// <summary>
        /// The lookup is inside the PatchDocumentViewModel,
        /// to prevent a lot of repeated data. So use the lookup from there.
        /// </summary>
        public IDAndName Curve { get; set; }
    }
}
