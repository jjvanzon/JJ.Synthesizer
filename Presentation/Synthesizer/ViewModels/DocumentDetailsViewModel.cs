using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels
{
	public sealed class DocumentDetailsViewModel : ViewModelBase
	{
		public IDAndName Document { get; set; }

		/// <summary> Do not show in the UI. Is there to persist along with the document create action. </summary>
		public AudioOutputViewModel AudioOutput { get; set; }

		/// <summary> Do not show in the UI. Is there to persist along with the document create action. </summary>
		public LibraryPropertiesViewModel SystemLibraryProperties { get; set; }

		/// <summary> Do not show in the UI. Is there to persist along with the document create action. </summary>
		public IDAndName Patch { get; set; }

		public bool CanDelete { get; set; }
		public bool IDVisible { get; set; }
	}
}
