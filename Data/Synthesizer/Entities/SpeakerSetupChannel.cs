// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
// ReSharper disable VirtualMemberCallInConstructor
// ReSharper disable UnusedAutoPropertyAccessor.Global

using JetBrains.Annotations;

namespace JJ.Data.Synthesizer.Entities
{
    /// <summary> bridge entity </summary>
    [UsedImplicitly]
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
