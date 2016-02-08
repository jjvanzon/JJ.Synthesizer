using JJ.Data.Canonical;
using System.Collections.Generic;
using JJ.Presentation.Synthesizer.ViewModels.Entities;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel_ForCustomOperator : ViewModelBase
    {
        public int ID { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// The lookup is inside the DocumentViewModel,
        /// to prevent a lot of repeated data. So use the lookup from there.
        /// </summary>
        public ChildDocumentIDAndNameViewModel UnderlyingPatch { get; set; }
    }
}
