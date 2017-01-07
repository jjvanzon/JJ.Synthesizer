namespace JJ.Presentation.Synthesizer.ViewModels
{
    /// <summary> Leading for saving when it comes to the simple properties. </summary>
    public sealed class PatchPropertiesViewModel : ViewModelBase
    {
        public int ID { get; internal set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public bool CanAddToCurrentPatches { get; set; }
    }
}
