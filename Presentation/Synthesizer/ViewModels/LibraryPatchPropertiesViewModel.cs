namespace JJ.Presentation.Synthesizer.ViewModels
{
    public class LibraryPatchPropertiesViewModel : ViewModelBase
    {
        public int PatchID { get; set; }
        public int DocumentReferenceID { get; set; }
        /// <summary> not editable </summary>
        public string Name { get; set; }
        /// <summary> not editable </summary>
        public string Group { get; set; }
        /// <summary> not editable </summary>
        public string Library { get; set; }

        internal int? OutletIDToPlay { get; set; }
    }
}