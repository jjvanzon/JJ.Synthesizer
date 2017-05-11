using System.Collections.Generic;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class LibraryPatchGridViewModel : ViewModelBase
    {
        public string Title { get; set; }
        public int LowerDocumentReferenceID { get; set; }
        public string Group { get; set; }
        public IList<IDAndName> List { get; set; }
        internal int? OutletIDToPlay { get; set; }
        internal IDAndName DocumentToOpenExternally { get; set; }
        internal IDAndName PatchToOpenExternally { get; set; }
    }
}
