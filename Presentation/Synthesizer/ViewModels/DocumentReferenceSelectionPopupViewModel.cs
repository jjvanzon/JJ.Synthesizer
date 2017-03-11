using System;
using System.Collections.Generic;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class DocumentReferenceSelectionPopupViewModel : ViewModelBase
    {
        [Obsolete("Probably needed at some point. If not delete it.")]
        public int HigherDocumentID { get; set; }
        public IList<IDAndName> LowerDocumentList { get; set; }
    }
}
