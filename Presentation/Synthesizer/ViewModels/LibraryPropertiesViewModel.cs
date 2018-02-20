using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels
{
	public sealed class LibraryPropertiesViewModel : ScreenViewModelBase
	{
		public int DocumentReferenceID { get; set; }
		public int LowerDocumentID { get; set; }
		/// <summary> not editable </summary>
		public string Name { get; set; }
		public string Alias { get; set; }
		internal int? OutletIDToPlay { get; set; }
		internal IDAndName DocumentToOpenExternally { get; set; }
	}
}