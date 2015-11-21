namespace JJ.Presentation.Synthesizer.ViewModels.Partials
{
    public sealed class ChildDocumentTreeNodeViewModel
    {
        public int ChildDocumentID { get; set; }

        public string Name { get; set; }
        public bool IsExpanded { get; set; }

        public DummyViewModel CurvesNode { get; set; }
        public DummyViewModel SamplesNode { get; set; }
    }
}
