using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class ChildDocumentGridViewModel
    {
        public int RootDocumentID { get; set; }
        public int ChildDocumentTypeID { get; set; }
        public bool Visible { get; set; }
        public IList<ChildDocumentListItemViewModel> List { get; set; }
    }
}
