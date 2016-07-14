using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels.Partials
{
    public sealed class PatchTreeNodeViewModel
    {
        public int ChildDocumentID { get; set; }

        public string Text { get; set; }
        public bool IsExpanded { get; set; }

        public TreeLeafViewModel CurvesNode { get; set; }
        public TreeLeafViewModel SamplesNode { get; set; }
    }
}
