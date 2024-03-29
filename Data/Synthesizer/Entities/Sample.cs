﻿using System.Diagnostics;
using JJ.Data.Synthesizer.Helpers;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
// ReSharper disable VirtualMemberCallInConstructor

namespace JJ.Data.Synthesizer.Entities
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class Sample
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual double Amplifier { get; set; }
        public virtual double TimeMultiplier { get; set; }
        public virtual int SamplingRate { get; set; }

        /// <summary> E.g. when you need to skip a header of a file. </summary>
        public virtual int BytesToSkip { get; set; }

        /// <summary> not nullable </summary>
        public virtual AudioFileFormat AudioFileFormat { get; set; }

        /// <summary> not nullable </summary>
        public virtual InterpolationType InterpolationType { get; set; }

        /// <summary> not nullable </summary>
        public virtual SampleDataType SampleDataType { get; set; }

        /// <summary> not nullable </summary>
        public virtual SpeakerSetup SpeakerSetup { get; set; }

        private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
    }
}