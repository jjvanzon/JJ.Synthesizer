using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using JJ.Framework.Wishes.Common;
using static System.Net.Mime.MediaTypeNames;
using static System.String;
using static JJ.Framework.Wishes.Common.FilledInWishes;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal class Case : CaseBase<int>
    {
        protected override ICaseProp[] Properties => new ICaseProp[] { FrameCount, SamplingRate, CourtesyFrames, AudioLength };

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
        protected override object[] DescriptorElements => new object[] { Name, "~", PropDescriptor, "f", "(", SamplingRate, "Hz", "+", CourtesyFrames, (",", AudioLength, "s"), ")" };
        
        public override string Descriptor => DescriptorWip;
        
        private string DescriptorWip
        {
            get
            {
                object[] descriptorElements = DescriptorElements;
                if (!Has(descriptorElements))
                {
                    return DescriptorFromProperties;
                }
                
                var destList = new List<string>();
                bool mustAddUnit = false;
                
                foreach (object item in descriptorElements)
                {
                    if (IsCaseProp(item))
                    {
                        string text = GetCasePropText(item, ref mustAddUnit);
                        destList.Add(text);
                    }
                    else if (IsUnit(item))
                    {
                        string text = GetUnit(item, ref mustAddUnit);
                        destList.Add(text);
                    }
                    else if (IsDescriptorTuple(item))
                    {
                        var texts = GetTupleText(item);
                        destList.AddRange(texts);
                    }
                    else if (!Has(item))
                    {
                        mustAddUnit = false;
                    }
                    else
                    {
                        destList.Add($"{item}");
                    }
                }
                
                string descriptor = Join(" ", destList.Where(FilledIn));
                return descriptor;
            }
        }
        
        private string DescriptorWorks
        {
            get
            {
                object[] descriptorElements = DescriptorElements;
                if (!Has(descriptorElements))
                {
                    return DescriptorFromProperties;
                }
                
                var destList = new List<object>();
                bool mustAddNextUnit = false;
                
                foreach (object item in descriptorElements)
                {
                    Type type = item.GetType();

                    if (item is ICaseProp prop)
                    {
                        var propText = $"{prop.PropDescriptor}";
                        if (Has(propText))
                        {
                            destList.Add(propText);
                        }
                        mustAddNextUnit = Has(propText);
                    }
                    else if (item is string unit && unit.Length <= 3)
                    {
                        if (mustAddNextUnit) 
                        { 
                            destList.Add(unit);
                        }
                        else
                        {
                            mustAddNextUnit = true;
                        }
                    }
                    else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ValueTuple<,,>))
                    {
                        string prefix = type.GetField("Item1").GetValue(item).ToString();
                        string value  = type.GetField("Item2").GetValue(item).ToString();
                        string suffix = type.GetField("Item3").GetValue(item).ToString();
                        
                        if (Has(value))
                        {
                            destList.Add(prefix);
                            destList.Add(value);
                            destList.Add(suffix);
                        }
                    }
                    else
                    {
                        destList.Add(item);
                    }
                }
                
                string descriptor = Join(" ", destList.Select(x => x.ToString()).Where(FilledIn));
                return descriptor;
            }
        }
        
        private bool IsDescriptorTuple(object item)
        { 
            if (item == null)
            {
                return false;
            }

            Type type = item.GetType();

            if (!type.IsGenericType)
            {
                return false;
            }
            
            if (type.GetGenericTypeDefinition() != typeof(ValueTuple<,,>))
            {
                return false;
            }

            return true;
        }
                        
        private IList<string> GetTupleText(object tuple)
        {
            var texts = new List<string>();
            
            if (tuple == null) return texts;
            
            Type type = tuple.GetType();

            string prefix = type.GetField("Item1").GetValue(tuple).ToString();
            string value  = type.GetField("Item2").GetValue(tuple).ToString();
            string suffix = type.GetField("Item3").GetValue(tuple).ToString();
            
            if (Has(value))
            {
                texts.Add(prefix);
                texts.Add(value);
                texts.Add(suffix);
            }

            return texts;
        }
        
        private bool UseTextsFromTuple(object item, out IList<string> texts)
        {
            texts = new List<string>();
            
            if (item == null)
            {
                return false;
            }
            
            Type type = item.GetType();

            if (!type.IsGenericType)
            {
                return false;
            }
            
            if (type.GetGenericTypeDefinition() != typeof(ValueTuple<,,>))
            {
                return false;
            }
            
            string prefix = type.GetField("Item1").GetValue(item).ToString();
            string value  = type.GetField("Item2").GetValue(item).ToString();
            string suffix = type.GetField("Item3").GetValue(item).ToString();
            
            if (Has(value))
            {
                texts.Add(prefix);
                texts.Add(value);
                texts.Add(suffix);
            }

            return true;
        }
        
        private bool IsCaseProp(object item) => item is ICaseProp;
        
        private string GetCasePropText(object item, ref bool mustAddUnit)
        {
            var prop = item as ICaseProp;
            if (prop.IsNully())
            {
                return default;
            }
            
            string text = prop.PropDescriptor;
            if (text.IsNully())
            {
                mustAddUnit = false;
                return default;
            }
         
            mustAddUnit = true;
            return text;
        }
        
        private bool UseCasePropText(object item, out string text, ref bool mustAddUnit)
        {
            text = "";
            
            var prop = item as ICaseProp;
            if (prop.IsNully())
            {
                return false;
            }
            
            string propText = prop.PropDescriptor;
            if (propText.IsNully())
            {
                return false;
            }
         
            text = propText;
            mustAddUnit = true;
            return true;
        }

        private bool IsUnit(object item)
        {
            var str = item as string;
            
            if (!Has(str))
            {
                return false;
            }
            
            return str.Length <= 3;
        }
        
        private string GetUnit(object item, ref bool mustAddUnit)
        {
            string unit = item as string;
            
            if (unit.IsNully())
            {
                return default;
            }
            
            if (unit.Length > 3)
            {
                return default;
            }

            if (!mustAddUnit)
            {
                mustAddUnit = true; // Skip at most successive 1 unit.
                return default;
            }
         
            return unit;
        }
        
        private bool UseUnitFromString(object item, out string unit, ref bool mustAddUnit)
        {
            unit = item as string;
            
            if (!Has(unit))
            {
                return false;
            }
            
            if (unit.Length > 3)
            {
                return false;
            }

            if (mustAddUnit)
            {
                return true;
            }
            else
            {
                mustAddUnit = false; // Skip at most 1 unit.
                return false;
            }
        }
                
        private string UnitFromStringOld(object item, ref bool mustAddUnit)
        {
            if (item is string unit && unit.Length <= 3)
            {
                if (mustAddUnit)
                {
                    return unit;
                }
                mustAddUnit = false;
            }
            
            return default;
        }

        private string DescriptorFromProperties
        {
            get 
            {
                // Name
                string name = GetElementDescriptor(Name, "~");
                
                // Main Prop
                string mainProp = PropDescriptor;
                
                // Other Props
                string propString = "";
                if (Has(Properties))
                {
                    propString = Join(", ", Properties.Select(x => x.PropDescriptor).Where(FilledIn));
                }
                if (Has(propString))
                {
                    propString = "(" + propString + ")";
                }

                // Return
                return Join(" ", name, mainProp, propString); 
            }
        }

        private string Descriptor_FromTuples
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

        private string Descriptor_LoneUnitOmission
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
                            
                        default:
                           elements.Add($"{descriptorElement}");
                           break;
                    }
                }
                
                return Join(" ", elements.Where(FilledIn));
            }
        }
        
        private string Descriptor_RawDescriptorElements => Join(" ", DescriptorElements);
        
        private string Descriptor_RawDescriptorElements_WithTypeStrings => Join(" ", DescriptorElements.Select(x => $"{x}{{{x?.GetType().FullName}}}"));

        private string Descriptor_Custom
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
