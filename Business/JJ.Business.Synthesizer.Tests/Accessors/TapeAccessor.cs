using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using System;
using System.Reflection;
using static JJ.Business.Synthesizer.Wishes.NameHelper;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class TapeAccessor
    {
        private readonly Accessor _accessor;
        
        public TapeAccessor(object obj)
        {
            Assembly assembly = typeof(SynthWishes).Assembly;
            string typeName = "JJ.Business.Synthesizer.Wishes.TapeWishes.Tape";
            Type type = assembly.GetType(typeName, true);
            _accessor = new Accessor(obj, type);
        }
        
        public FlowNode Signal
        {
            get => (FlowNode)_accessor.GetPropertyValue(MemberName());
            set => _accessor.SetPropertyValue(MemberName(), value);
        }
    }
}