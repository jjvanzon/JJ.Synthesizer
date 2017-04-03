namespace JJ.Data.Synthesizer.Entities
{
    public class AudioOutput
    {
        public virtual int ID { get; set; }
        public virtual SpeakerSetup SpeakerSetup { get; set; }
        public virtual int SamplingRate { get; set; }
        public virtual int MaxConcurrentNotes { get; set; }

        /// <summary> 
        /// The desired buffer duration is only an indication, 
        /// that could be adapted to the audio device's capabilities.
        /// </summary>
        public virtual double DesiredBufferDuration { get; set; }

        // No parent reference to document, 
        // because 1-to-1 inverse property are a disaster in NHibernate.
    }
}