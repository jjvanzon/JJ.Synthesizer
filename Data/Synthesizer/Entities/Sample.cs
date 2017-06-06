using System.Diagnostics;
using JetBrains.Annotations;
using JJ.Data.Synthesizer.Helpers;

namespace JJ.Data.Synthesizer.Entities
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class Sample
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual double Amplifier { get; set; }
        public virtual double TimeMultiplier { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual int SamplingRate { get; set; }

        /// <summary>
        /// E.g. when you need to skip a header of a file.
        /// </summary>
        public virtual int BytesToSkip { get; set; }

        /// <summary>
        /// Optional. E.g. the file path.
        /// </summary>
        public virtual string OriginalLocation { get; set; }

        /// <summary> not nullable </summary>
        [NotNull]
        public virtual AudioFileFormat AudioFileFormat { get; set; }

        /// <summary> not nullable </summary>
        [NotNull]
        public virtual InterpolationType InterpolationType { get; set; }

        /// <summary> not nullable </summary>
        [NotNull]
        public virtual SampleDataType SampleDataType { get; set; }

        /// <summary> not nullable </summary>
        [NotNull]
        public virtual SpeakerSetup SpeakerSetup { get; set; }

        /// <summary> parent, nullable </summary>
        [CanBeNull]
        public virtual Document Document { get; set; }

        private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
    }
}