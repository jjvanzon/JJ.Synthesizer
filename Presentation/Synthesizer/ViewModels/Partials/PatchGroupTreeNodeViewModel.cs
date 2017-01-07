using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels.Partials
{
    public sealed class PatchGroupTreeNodeViewModel
    {
        public string Text { get; set; }
        public string GroupName { get; set; }
        public IList<PatchTreeNodeViewModel> PatchNodes { get; set; }
    }
}
