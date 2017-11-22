namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
	internal class UndoUpdateViewModel : UndoItemViewModelBase
	{
		public ViewModelBase OldState { get; set; }
		public ViewModelBase NewState { get; set; }
	}
}
