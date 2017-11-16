namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
    internal class UndoItemViewModel
    {
        public ViewModelBase OriginalState { get; set; }
        public ViewModelBase NewState { get; set; }
    }
}
