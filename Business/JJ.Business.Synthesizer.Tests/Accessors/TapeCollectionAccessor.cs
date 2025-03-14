using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JJ.Framework.Core.Reflection;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class TapeCollectionAccessor
    {
        private readonly AccessorEx _accessor;
        
        public TapeCollectionAccessor(object obj)
        {
            Assembly assembly = typeof(SynthWishes).Assembly;
            string typeName = "JJ.Business.Synthesizer.Wishes.TapeWishes.TapeCollection";
            Type type = assembly.GetType(typeName, true);
            _accessor = new AccessorEx(obj, type);
        }
        
        public IList<TapeAccessor> GetAll()
        {
            var tapes = (IList<object>)_accessor.InvokeMethod(MemberName());
            
            return tapes.Select(x => new TapeAccessor(x)).ToArray();
        }
    }
}
