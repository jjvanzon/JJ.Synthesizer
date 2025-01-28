using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Reflection;
using JJ.Framework.Wishes.Common;
using JJ.Framework.Wishes.Reflection;
using static System.String;
using static JJ.Framework.Wishes.Common.FilledInWishes;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    class CaseDescriptorBuilder<T>
        where T : struct
    {
        readonly CaseBase<T> _testCase;
        
        public CaseDescriptorBuilder(CaseBase<T> testCase)
        {
            _testCase = testCase ?? throw new NullException(() => testCase);
            
            if (testCase.MainProp == null) throw new NullException(() => testCase.MainProp);
            if (testCase.Props == null) throw new NullException(() => testCase.Props);
        }
        
        public string BuildDescriptor()
        {
            var descriptorElements = _testCase.DescriptorElements;
            if (!Has(descriptorElements))
            {
                return DescriptorFromProps;
            }
            
            var texts = new List<string>();
            bool mustAddUnit = false;
            
            foreach (object element in descriptorElements)
            {
                if (IsCaseProp(element))
                {
                    string text = GetCasePropText(element, ref mustAddUnit);
                    texts.Add(text);
                }
                else if (IsUnit(element))
                {
                    string text = GetUnit(element, ref mustAddUnit);
                    texts.Add(text);
                }
                else if (IsDescriptorTuple(element))
                {
                    var texts2 = GetDescriptorTupleTexts(element);
                    texts.AddRange(texts2);
                }
                else if (!Has(element))
                {
                    mustAddUnit = false;
                }
                else
                {
                    texts.Add($"{element}");
                    mustAddUnit = true;
                }
            }
            
            string descriptor = Join(" ", texts.Where(FilledIn));
            return descriptor;
        }
        
        bool IsDescriptorTuple(object item)
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
        
        private IList<string> GetDescriptorTupleTexts(object tuple)
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
        
        private string DescriptorFromProps
        {
            get
            {
                // Name
                string name = GetElementDescriptor(_testCase.Name, "~");
                
                // Main Prop
                string mainProp = _testCase.PropDescriptor;
                
                // Other Props
                string propString = "";
                if (Has(_testCase.Props))
                {
                    propString = Join(", ", _testCase.Props
                                                     .Except(new [] { _testCase })
                                                     .Where(FilledIn)
                                                     .Select(x => x.PropDescriptor)
                                                     .Where(FilledIn));
                }
                
                if (Has(propString))
                {
                    propString = "(" + propString + ")";
                }
                
                // Return
                return Join(" ", name, mainProp, propString);
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
    }
}