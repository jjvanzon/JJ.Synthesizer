using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using JJ.Framework.Core.Reflection;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class ConfigToolingElementAccessor
    {
        public object Obj { get; }
        
        private readonly AccessorEx _accessor;
                
        public ConfigToolingElementAccessor()
        {
            Type type = GetUnderlyingType();
            Obj       = Activator.CreateInstance(type);
            _accessor = new AccessorEx(Obj, Obj.GetType());
        }

        public ConfigToolingElementAccessor(object obj)
        {
            Obj = obj;
            _accessor = new AccessorEx(obj, GetUnderlyingType());
        }
        
        private Type GetUnderlyingType()
        {
            Assembly assembly = typeof(SynthWishes).Assembly;
            string   typeName = "JJ.Business.Synthesizer.Wishes.Config.ConfigToolingElement";
            return   assembly.GetType(typeName, true);
        }
        
        public bool? AudioPlayback
        {
            get => (bool?)_accessor.GetPropertyValue(MemberName());
            set =>        _accessor.SetPropertyValue(MemberName(), value);
        }
        
        public int? SamplingRate
        {
            get => (int?)_accessor.GetPropertyValue(MemberName());
            set =>       _accessor.SetPropertyValue(MemberName(), value);            
        }

        public int? SamplingRateLongRunning
        {
            get => (int?)_accessor.GetPropertyValue(MemberName());
            set =>       _accessor.SetPropertyValue(MemberName(), value);            
        }
                
        public bool? ImpersonationMode
        {
            get => (bool?)_accessor.GetPropertyValue(MemberName());
            set =>        _accessor.SetPropertyValue(MemberName(), value);
        }
    }
}