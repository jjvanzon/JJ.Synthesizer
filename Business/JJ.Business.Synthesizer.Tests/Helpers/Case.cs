using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using JJ.Framework.Reflection;
using static JJ.Framework.Wishes.Common.FilledInWishes;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal class Case : CaseBase<int>
    {
        // FrameCount: The main property being tested, adjusted directly or via dependencies.
        public CaseProp<int> FrameCount => this;
        public CaseProp<int> Frames => this;
        
        // SamplingRate: Scales FrameCount
        public CaseProp<int> SamplingRate { get => GetProp<int>(0); set => SetProp(0, value); }
        public CaseProp<int> Hertz        { get => SamplingRate; set => SamplingRate = value; }
        public CaseProp<int> Hz           { get => SamplingRate; set => SamplingRate = value; }

        // AudioLength: Scales FrameCount + FrameCount setters adjust AudioLength.
        public CaseProp<double> AudioLength { get => GetProp<double>(1); set => SetProp(1, value); }
        public CaseProp<double> Length      { get => AudioLength; set => AudioLength = value; }
        public CaseProp<double> Len         { get => AudioLength; set => AudioLength = value; }
        public CaseProp<double> Duration    { get => AudioLength; set => AudioLength = value; }
        public CaseProp<double> seconds     { get => AudioLength; set => AudioLength = value; }
        public CaseProp<double> sec         { get => AudioLength; set => AudioLength = value; }
        
        // CourtesyFrames: AudioLength does not incorporate CourtesyFrames, but FrameCount does.
        public CaseProp<int> CourtesyFrames { get => GetProp<int>(2); set => SetProp(2, value); }
        public CaseProp<int> PlusFrames     { get => CourtesyFrames; set => CourtesyFrames = value; }
        public CaseProp<int> Plus           { get => CourtesyFrames; set => CourtesyFrames = value; }

        // Descriptions            
        
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
        
        public Case() { }
        public Case(int frameCount) : base(frameCount) { }
        public Case(int from, int to) : base(from, to) { }

        // Templating

        public static Case[] FromTemplate(Case template, params Case[] cases)
        {
            //return CaseBase<int>.FromTemplate(template, cases);
            if (template == null) throw new NullException(() => template);
            return template.CloneTo(cases).Cast<Case>().ToArray();
        }
    }
}
