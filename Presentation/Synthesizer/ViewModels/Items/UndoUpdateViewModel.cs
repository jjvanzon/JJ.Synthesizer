using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
	internal class UndoUpdateViewModel : UndoItemViewModelBase
	{
		public IList<ViewModelBase> OldStates { get; set; }
		public IList<ViewModelBase> NewStates { get; set; }
	}
}