namespace JJ.Presentation.Synthesizer.NAudio
{
    internal class NoteInfo
    {
        public int MidiNoteNumber { get; set; }
        public int MidiVelocity { get; set; }
        public int MidiChannel { get; set; }
        public int ListIndex { get; set; }
        public double StartTime { get; set; }
        public double ReleaseTime { get; set; }
        public double EndTime { get; set; }
    }
}
