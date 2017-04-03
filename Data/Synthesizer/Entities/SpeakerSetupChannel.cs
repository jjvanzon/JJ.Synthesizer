using JetBrains.Annotations;

namespace JJ.Data.Synthesizer.Entities
{
    /// <summary> bridge entity </summary>
    public class SpeakerSetupChannel
    {
        public virtual int ID { get; set; }
        public virtual int IndexNumber { get; set; }

        /// <summary> not nullable </summary>
        [NotNull]
        public virtual SpeakerSetup SpeakerSetup { get; set; }

        /// <summary> not nullable </summary>
        [NotNull]
        public virtual Channel Channel { get; set; }
    }
}
