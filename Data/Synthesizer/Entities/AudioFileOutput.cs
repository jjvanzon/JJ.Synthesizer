using System.Diagnostics;
using JJ.Data.Synthesizer.Helpers;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
// ReSharper disable VirtualMemberCallInConstructor

namespace JJ.Data.Synthesizer.Entities
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class AudioFileOutput
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual double Amplifier { get; set; }
        public virtual double TimeMultiplier { get; set; }
        public virtual double StartTime { get; set; }
        public virtual double Duration { get; set; }
        public virtual int SamplingRate { get; set; }
        public virtual string FilePath { get; set; }

        public virtual AudioFileFormat AudioFileFormat { get; set; }
        public virtual SampleDataType SampleDataType { get; set; }
        public virtual SpeakerSetup SpeakerSetup { get; set; }

        public virtual Outlet Outlet { get; set; }

        /// <summary> parent, nullable </summary>
        public virtual Document Document { get; set; }

        private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
    }
}
