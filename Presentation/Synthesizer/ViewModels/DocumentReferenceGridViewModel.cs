using System;
using System.Collections.Generic;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class DocumentReferenceGridViewModel : ViewModelBase
    {
        [Obsolete("Probably needed at some point. If not delete it.")]
        public int HigherDocumentID { get; set; }
        public IList<DocumentReferenceListItemViewModel> LowerDocumentList { get; set; }
    }
}