using System;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class LibraryPropertiesViewModel
    {
        public int DocumentReferenceID { get; set; }
        [Obsolete]
        public int HigherDocumentID { get; set; }
        public int LowerDocumentID { get; set; }
        /// <summary> not editable </summary>
        public string Name { get; set; }
        public string Alias { get; set; }
    }
}