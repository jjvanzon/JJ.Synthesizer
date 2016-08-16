namespace JJ.Data.Synthesizer
{
    /// <summary> bridge entity </summary>
    public class SpeakerSetupChannel
    {
        public virtual int ID { get; set; }
        public virtual int IndexNumber { get; set; }

        /// <summary> not nullable </summary>
        public virtual SpeakerSetup SpeakerSetup { get; set; }

        /// <summary> not nullable </summary>
        public virtual Channel Channel { get; set; }
    }
}
