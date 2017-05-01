using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels.Partials
{
    public sealed class PatchGroupTreeNodeViewModel
    {
        /// <summary> Can contain the name of the PatchGroup along with the number of items in braces. </summary>
        public string Caption { get; set; }
        /// <summary> not visible </summary>
        public string GroupName { get; set; }
        public IList<PatchTreeNodeViewModel> PatchNodes { get; set; }
    }
}
