using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using static System.String;
using static JJ.Framework.Wishes.Common.FilledInWishes;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal class Case : CaseBase<int>
    {
        protected override ICaseProp[] Properties => new ICaseProp[] { FrameCount, SamplingRate, AudioLength, CourtesyFrames };
        //protected override string[] Units() => new [] { "f", "Hz", "s", "+" };

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
        
        //protected object[] DescriptorElements => new object[] { Name, "~", PropDescriptor, "f", "(", SamplingRate, "Hz", "+", CourtesyFrames, "", ",", AudioLength, "s", ")" };
        //protected object[] DescriptorElements => new object[] { ("", Name, "~"), ("", PropDescriptor, "f"), "(", ("", SamplingRate, "Hz"), ("+", CourtesyFrames, ""), (",", AudioLength, "s"), ")" };
        protected object[] DescriptorElements => new object[] { Name, "~", PropDescriptor, "f", "(", SamplingRate, "Hz", "+", CourtesyFrames, (",", AudioLength, "s"), ")" };
        
        public override string Descriptor => Descriptor12_ComboSolution;

        private string Descriptor12_ComboSolution
        {
            get
            {
                var destList = new List<object>();
                
                bool mustAddNextUnit = false;

                foreach (object sourceItem in DescriptorElements)
                {
                    Type type = sourceItem.GetType();

                    if (sourceItem is ICaseProp prop)
                    {
                        var propText = $"{prop}";
                        if (Has(propText))
                        {
                            destList.Add(propText);
                            mustAddNextUnit = true;
                        }
                        else
                        {
                            mustAddNextUnit = false;
                        }
                    }
                    else if (sourceItem is string unit && unit.Length <= 3)
                    {
                        if (mustAddNextUnit)
                        {
                            destList.Add(unit);
                        }
                        mustAddNextUnit = true;
                    }
                    
                    else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ValueTuple<,,>))
                    {
                        object item1 = type.GetField("Item1").GetValue(sourceItem);
                        object item2 = type.GetField("Item2").GetValue(sourceItem);
                        object item3 = type.GetField("Item3").GetValue(sourceItem);
                        
                        if (Has(item2))
                        {
                            destList.Add(item1);
                            destList.Add(item2);
                            destList.Add(item3);
                        }
                    }
                    else
                    {
                        destList.Add(sourceItem);
                    }
                }
                
                string descriptor = Join(" ", destList.Select(x => x.ToString()).Where(FilledIn));
                return descriptor;
            }
        }

        private string Descriptor11_Works
        {
            get
            {
                var textElements = new List<string>();
                
                foreach (object descriptorElement in DescriptorElements)
                {
                    Type type = descriptorElement.GetType();
                    
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ValueTuple<,,>))
                    {
                        string item1 = type.GetField("Item1")?.GetValue(descriptorElement).ToString();
                        string item2 = type.GetField("Item2")?.GetValue(descriptorElement).ToString();
                        string item3 = type.GetField("Item3")?.GetValue(descriptorElement).ToString();
                        if (Has(item2))
                        {
                            textElements.Add(item1);
                            textElements.Add(item2);
                            textElements.Add(item3);
                        }
                    }
                    else
                    {
                        textElements.Add($"{descriptorElement}");
                    }
                } 
                
                return Join(" ", textElements.Where(FilledIn));
            }
        }
        
        private string Descriptor9_ForDebugging => Join(" ", DescriptorElements.Select(x => $"{x}{{{x?.GetType().FullName}}}"));

        private string Descriptor6_AlmostRight
        {
            get
            {
                var elements = new List<string>();
                
                bool mustAddNextUnit = false;
                
                foreach (object descriptorElement in DescriptorElements)
                {
                    switch (descriptorElement)
                    {
                        case ICaseProp prop:
                            string propText = $"{prop}";
                            if (Has(propText))
                            {
                                elements.Add(propText);
                                mustAddNextUnit = true;
                            }
                            else
                            {
                                mustAddNextUnit = false;
                            }
                            break;
                            
                        case string unit when unit.Length <= 3:
                            if (mustAddNextUnit)
                            {
                                elements.Add(unit);
                            }
                            mustAddNextUnit = true;
                            break;
                            
                        //case string str:
                        //   elements.Add($"{str}");
                        //   break;
                            
                        default:
                           elements.Add($"{descriptorElement}");
                           break;
                            
                    }
                }
                
                return Join(" ", elements.Where(FilledIn));
            }
        }

        private string Descriptor2 => Join(" ", DescriptorElements);
        
        private string Descriptor1
        {
            get 
            {
                // Name
                string name = GetElementDescriptor(Name, "~");
                
                // Main Prop
                string frameCount = GetElementDescriptor(base.PropDescriptor, "f");
                
                // Other Props
                string samplingRate = GetElementDescriptor(SamplingRate, "Hz");
                string plusFrames = GetElementDescriptor("+", PlusFrames);
                string audioLength = GetElementDescriptor(",", AudioLength, "s");
                string braced = Join(" ", samplingRate, plusFrames, audioLength);
                braced = Has(braced) ? $"({braced.TrimStart(',').Trim()})" : "";

                // Return
                return Join(" ", name, frameCount, braced); 
            }
        }

        private string GetElementDescriptor(string prop, string unit) => GetElementDescriptor(null, prop, unit);
        private string GetElementDescriptor(object prop, string unit) => GetElementDescriptor(null, prop, unit);
        private string GetElementDescriptor(string unit, object prop) => GetElementDescriptor(unit, prop, null);
        
        private string GetElementDescriptor(string prefixUnit, object value, string suffixUnit)
        {
            string valueText = $"{value}";
            if (!Has(valueText)) return "";
            string[] elements = { prefixUnit, valueText, suffixUnit };
            return Join(" ", elements.Where(FilledIn));
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
