using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.ViewModels.Partials;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class DocumentTreeViewModel : ScreenViewModelBase
    {
        public int ID { get; set; }
        public PatchesTreeNodeViewModel PatchesNode { get; set; }
        public EntityWithListTreeNodeViewModel MidiNode { get; set; }
        public EntityWithListTreeNodeViewModel ScalesNode { get; set; }
        public TreeLeafViewModel AudioOutputNode { get; set; }
        public TreeLeafViewModel AudioFileOutputListNode { get; set; }
        public LibrariesTreeNodeViewModel LibrariesNode { get; set; }

        public DocumentTreeNodeTypeEnum SelectedNodeType { get; set; }
        public int? SelectedItemID { get; set; }
        public int? SelectedPatchGroupLowerDocumentReferenceID { get; set; }
        /// <summary> For a default for new patches. </summary>
        public string SelectedFriendlyPatchGroupName { get; set; }
        /// <summary> For use as a key for grouping. </summary>
        public string SelectedCanonicalPatchGroupName { get; set; }

        public string PatchToolTipText { get; set; }
        
        internal int? OutletIDToPlay { get; set; }
        internal IDAndName DocumentToOpenExternally { get; set; }
        internal IDAndName PatchToOpenExternally { get; set; }
        internal int CreatedEntityID { get; set; }
    }
}
