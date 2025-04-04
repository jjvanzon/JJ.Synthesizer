using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Reflection;
using JJ.Framework.Reflection.Core;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class TapeRunnerAccessor
    {
        private readonly AccessorCore _accessor;
        
        public TapeRunnerAccessor(object obj)
        {
            Assembly assembly = typeof(SynthWishes).Assembly;
            string typeName = "JJ.Business.Synthesizer.Wishes.TapeWishes.TapeRunner";
            Type type = assembly.GetType(typeName, true);
            _accessor = new AccessorCore(obj, type);
        }
        
        public void RunAllTapes() 
            => _accessor.InvokeMethod(MemberName());
    }
}
