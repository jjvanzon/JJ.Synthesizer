using JJ.Data.Canonical;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel_ForPatchOutlet : OperatorPropertiesViewModel
    {
        /// <summary> Note that this is 1-based, while the value stored in the entity model is 0-based. </summary>
        public int Number { get; set; }
    }
}
