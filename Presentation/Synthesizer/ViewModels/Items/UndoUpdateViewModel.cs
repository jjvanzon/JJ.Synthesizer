namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
    internal class UndoUpdateViewModel : UndoItemViewModelBase
    {
        public ViewModelBase OriginalState { get; set; }
        public ViewModelBase NewState { get; set; }
    }
}
