using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
    public sealed class PatchViewModel
    {
        public int PatchID { get; set; }
        public int ChildDocumentID { get; set; }
        public IList<OperatorViewModel> Operators { get; set; }
    }
}
