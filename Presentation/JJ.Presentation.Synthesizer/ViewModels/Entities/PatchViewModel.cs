using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    public sealed class PatchViewModel
    {
        public int PatchID { get; set; }
        public IList<OperatorViewModel> Operators { get; set; }
    }
}
