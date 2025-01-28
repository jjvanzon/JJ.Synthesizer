using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Reflection;

// ReSharper disable CoVariantArrayConversion

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal class Case : CaseBase<int>
    {
        internal override IList<object> DescriptorElements 
            => new object[] { Name, "~", PropDescriptor, "f", "(", SamplingRate, "Hz", "+", CourtesyFrames, (",", AudioLength, "s"), ")" };

        // FrameCount: The main property being tested, adjusted directly or via dependencies.
        public CaseProp<int> FrameCount => this;
        public CaseProp<int> Frames => this;
        
        // SamplingRate: Scales FrameCount
        public CaseProp<int> SamplingRate { get; set; } = new CaseProp<int>();
        public CaseProp<int> Hertz        { get => SamplingRate; set => SamplingRate = value; }
        public CaseProp<int> Hz           { get => SamplingRate; set => SamplingRate = value; }

        // AudioLength: Scales FrameCount + FrameCount setters adjust AudioLength.
        public CaseProp<double> AudioLength { get; set; } = new CaseProp<double>();
        public CaseProp<double> Length      { get => AudioLength; set => AudioLength = value; }
        public CaseProp<double> Len         { get => AudioLength; set => AudioLength = value; }
        public CaseProp<double> Duration    { get => AudioLength; set => AudioLength = value; }
        public CaseProp<double> seconds     { get => AudioLength; set => AudioLength = value; }
        public CaseProp<double> sec         { get => AudioLength; set => AudioLength = value; }
        
        // CourtesyFrames: AudioLength does not incorporate CourtesyFrames, but FrameCount does.
        public CaseProp<int> CourtesyFrames { get; set; } = new CaseProp<int>();
        public CaseProp<int> PlusFrames     { get => CourtesyFrames; set => CourtesyFrames = value; }
        public CaseProp<int> Plus           { get => CourtesyFrames; set => CourtesyFrames = value; }

        // Constructors
        
        public Case(
            int?    frameCount     = null,
            int?    samplingRate   = null,
            double? audioLength    = null,
            int?    courtesyFrames = null)
        {
            if (frameCount     != null) From = To      = frameCount.Value;
            if (samplingRate   != null) SamplingRate   = samplingRate.Value;
            if (audioLength    != null) AudioLength    = audioLength.Value;
            if (courtesyFrames != null) CourtesyFrames = courtesyFrames.Value;
        }
        
        public Case() { }
        public Case(int frameCount) : base(frameCount) { }
        public Case(int from, int to) : base(from, to) { }

        // Templating

        public static Case[] FromTemplate(Case template, params Case[] cases)
        {
            if (template == null) throw new NullException(() => template);
            return template.FromTemplate(cases).Cast<Case>().ToArray();
        }
    }
}
