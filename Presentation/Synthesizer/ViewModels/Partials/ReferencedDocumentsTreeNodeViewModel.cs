using JJ.Presentation.Synthesizer.ViewModels.Items;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels.Partials
{
    public sealed class ReferencedDocumentsTreeNodeViewModel
    {
        public string Text { get; set; }
        public IList<ReferencedDocumentViewModel> List { get; set; }
    }
}
