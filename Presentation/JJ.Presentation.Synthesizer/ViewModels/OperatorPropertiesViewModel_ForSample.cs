using JJ.Data.Canonical;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel_ForSample : OperatorPropertiesViewModelBase
    {
        public string Name { get; set; }

        /// <summary>
        /// The lookup is inside the DocumentViewModel,
        /// to prevent a lot of repeated data. So use the lookup from there.
        /// </summary>
        public IDAndName Sample { get; set; }
    }
}
