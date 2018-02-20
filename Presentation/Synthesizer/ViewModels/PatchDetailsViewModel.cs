using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels
{
	public sealed class PatchDetailsViewModel : ScreenViewModelBase
	{
		public PatchViewModel Entity { get; set; }
		public OperatorViewModel SelectedOperator { get; set; }
		public bool CanSave { get; set; }
		internal int? OutletIDToPlay { get; set; }
	}
}
