using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class ConfigSectionAccessor
    {
        private readonly Accessor _accessor;
        
        public object Obj { get; }
        
        public ConfigSectionAccessor(object obj)
        {
            Assembly assembly = typeof(SynthWishes).Assembly;
            string typeName = "JJ.Business.Synthesizer.Wishes.ConfigSection";
            Type type = assembly.GetType(typeName, true);
            _accessor = new Accessor(obj, type);
            Obj = obj;
        }
        
        public int? Bits
        {
            get => (int?)_accessor.GetPropertyValue(MemberName());
            set => _accessor.SetPropertyValue(MemberName(), value);
        }
    }
}
