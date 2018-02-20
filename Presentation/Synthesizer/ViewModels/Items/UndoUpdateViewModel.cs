using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
	internal class UndoUpdateViewModel : UndoItemViewModelBase
	{
		public IList<ScreenViewModelBase> OldStates { get; set; }
		public IList<ScreenViewModelBase> NewStates { get; set; }
	}
}