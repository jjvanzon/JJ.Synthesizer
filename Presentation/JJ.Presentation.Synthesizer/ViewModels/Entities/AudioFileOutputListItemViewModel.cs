namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    public sealed class AudioFileOutputListItemViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public string AudioFileFormat { get; set; }
        public string SampleDataType { get; set; }
        public string SpeakerSetup { get; set; }
        public int SamplingRate { get; set; }
    }
}
