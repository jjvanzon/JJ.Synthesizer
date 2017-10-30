namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
    public sealed class CurrentInstrumentItemViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool CanGoBackward { get; set; }
        public bool CanGoForward { get; set; }
    }
}
