using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Tests.Helpers.DebuggerDisplayFormatter;
using static JJ.Framework.Wishes.Common.FilledInWishes;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal class Case : CaseProp<int>
    {
        /// <inheritdoc cref="docs._strict />
        public bool Strict { get; set; } = true;

        // FrameCount: The main property being tested, adjusted directly or via dependencies.
        public CaseProp<int> FrameCount => this;
        public CaseProp<int> Frames => this;
        
        // SamplingRate: Scales FrameCount
        public CaseProp<int> SamplingRate { get; set; } = new CaseProp<int>();
        public CaseProp<int> Hertz { get => SamplingRate; set => SamplingRate = value; }
        public CaseProp<int> Hz    { get => SamplingRate; set => SamplingRate = value; }

        // AudioLength: Scales FrameCount + FrameCount setters adjust AudioLength.
        public CaseProp<double> AudioLength { get; set; } = new CaseProp<double>();
        public CaseProp<double> Length   { get => AudioLength; set => AudioLength = value; }
        public CaseProp<double> Len      { get => AudioLength; set => AudioLength = value; }
        public CaseProp<double> Duration { get => AudioLength; set => AudioLength = value; }
        public CaseProp<double> seconds  { get => AudioLength; set => AudioLength = value; }
        public CaseProp<double> sec      { get => AudioLength; set => AudioLength = value; }
        
        // CourtesyFrames: AudioLength does not incorporate CourtesyFrames, but FrameCount does.
        public CaseProp<int> CourtesyFrames { get; set; } = new CaseProp<int>();
        public CaseProp<int> PlusFrames { get => CourtesyFrames; set => CourtesyFrames = value; }
        public CaseProp<int> Plus       { get => CourtesyFrames; set => CourtesyFrames = value; }

        // Descriptions            
        
        string DebuggerDisplay => DebuggerDisplay(this);
        public override string ToString() => Descriptor;
        public string Name { get; set; }
        public object[] DynamicData => new object[] { Descriptor };

        public override string Descriptor
        {
            get 
            {
                string name = Has(Name) ? $"{Name} ~ " : "";
                
                string frameCount = $"{base.Descriptor}";
                frameCount = Has(frameCount) ? $"{frameCount} f " : "";
                
                string samplingRate = $"{SamplingRate}";
                samplingRate = Has(samplingRate) ? $"{samplingRate} Hz " : "";
                
                string plusFrames = $"{PlusFrames}";
                plusFrames = Has(plusFrames) ? $"+ {plusFrames} " : "";
                
                string audioLength = $"{AudioLength}";
                audioLength = Has(audioLength) ? $", {audioLength} s" : "";
                
                string braced = samplingRate + plusFrames + audioLength;
                braced = Has(braced) ? $"({braced.TrimStart(',').Trim()})" : "";
                
                return $"{name}{frameCount}{braced}"; 
            }
        }

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
        
        public Case() : base() { }
        public Case( int  frameCount) : base(frameCount) { }
        public Case( int? frameCount) : base(frameCount) { }
        public Case( int  from ,  int  to) : base(from, to) { }
        public Case( int  from ,  int? to) : base(from, to) { }
        public Case( int? from ,  int  to) : base(from, to) { }
        public Case( int? from ,  int? to) : base(from, to) { }
        public Case((int  from ,  int  to) values) : base(values) { }
        public Case((int? from ,  int  to) values) : base(values) { }
        public Case((int  from ,  int? to) values) : base(values) { }
        public Case((int? from ,  int? to) values) : base(values) { }
        public Case( int  from , (int? nully, int coalesced) to) : base(from, to) { }
        public Case((int? nully,  int coalesced) from,  int to) : base(from, to) { }
        public Case((int? nully,  int coalesced) from, (int? nully, int coalesced) to) : base(from, to) { }

        // Conversion Operators
        
        public static implicit operator Case(int  value) => new Case(value);
        public static implicit operator Case(int? value) => new Case(value);
        public static implicit operator Case((int  from, int  to) values) => new Case(values);
        public static implicit operator Case((int? from, int  to) values) => new Case(values);
        public static implicit operator Case((int  from, int? to) values) => new Case(values);
        public static implicit operator Case((int? from, int? to) values) => new Case(values);
        public static implicit operator Case((int from, (int? nully, int coalesced) to) x) => new Case(x.from, x.to);
        public static implicit operator Case(((int? nully, int coalesced) from, int to) x) =>  new Case(x.from, x.to);
        public static implicit operator Case(((int? nully, int coalesced) from, (int? nully, int coalesced) to) x) => new Case(x.from, x.to);

        // Templating
        
        public static Case[] FromTemplate(Case template, params Case[] cases)
        {
            if (template == null) throw new NullException(() => template);
            return template.CloneTo(cases);
        }

        public Case[] CloneTo(params Case[] cases)
        {
            if (cases == null) throw new NullException(() => cases);
            for (int i = 0; i < cases.Length; i++)
            {
                if (cases[i] == null) throw new NullException(() => cases[i]);
                var testCase = cases[i];
                testCase.Name = Coalesce(testCase.Name, Name);
                if (Strict == false) testCase.Strict = false; // Yield over alleviation from strictness.
                testCase.FrameCount    .CloneFrom(FrameCount    );
                testCase.SamplingRate  .CloneFrom(SamplingRate  );
                testCase.AudioLength   .CloneFrom(AudioLength   );
                testCase.CourtesyFrames.CloneFrom(CourtesyFrames);
            }
            return cases;
        }
    }
}
