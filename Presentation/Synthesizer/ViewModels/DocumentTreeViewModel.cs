using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.ViewModels.Partials;

namespace JJ.Presentation.Synthesizer.ViewModels
{
	public sealed class DocumentTreeViewModel : ViewModelBase
	{
		public int ID { get; set; }
		public PatchesTreeNodeViewModel PatchesNode { get; set; }
		public MidiTreeNodeViewModel MidiNode { get; set; }
		public TreeLeafViewModel ScalesNode { get; set; }
		public TreeLeafViewModel AudioOutputNode { get; set; }
		public TreeLeafViewModel AudioFileOutputListNode { get; set; }
		public LibrariesTreeNodeViewModel LibrariesNode { get; set; }

		public bool CanAdd { get; set; }
		public bool CanAddToInstrument { get; set; }
		public bool CanCreateNew { get; set; }
		public bool CanOpenExternally { get; set; }
		public bool CanPlay { get; set; }
		public bool CanRemove { get; set; }

		public DocumentTreeNodeTypeEnum SelectedNodeType { get; set; }
		public int? SelectedItemID { get; set; }
		public int? SelectedPatchGroupLowerDocumentReferenceID { get; set; }
		public string SelectedCanonicalPatchGroup { get; set; }

		public string PatchToolTipText { get; set; }
		
		internal int? OutletIDToPlay { get; set; }
		internal IDAndName DocumentToOpenExternally { get; set; }
		internal IDAndName PatchToOpenExternally { get; set; }
	}
}
