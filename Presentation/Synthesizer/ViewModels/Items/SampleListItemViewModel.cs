namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
    public sealed class SampleListItemViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SampleDataType { get; set; }
        public string SpeakerSetup { get; set; }
        public int SamplingRate { get; set; }
        public bool IsActive { get; set; }
        public string IsActiveText { get; set; }
        public string UsedIn { get; set; }
    }
}
