namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class LibraryPropertiesViewModel : ViewModelBase
    {
        public int DocumentReferenceID { get; set; }
        public int LowerDocumentID { get; set; }
        /// <summary> not editable </summary>
        public string Name { get; set; }
        public string Alias { get; set; }
        public int? OutletIDToPlay { get; set; }
    }
}