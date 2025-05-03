using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JJ.Framework.Reflection.Core;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class TapeCollectionAccessor
    {
        private readonly AccessorCore _accessor;
        
        public TapeCollectionAccessor(object obj)
        {
            //Assembly assembly = typeof(SynthWishes).Assembly;
            //string typeName = "JJ.Business.Synthesizer.Wishes.TapeWishes.TapeCollection";
            //Type type = assembly.GetType(typeName, true);
            //_accessor = new AccessorCore(obj, type);
            _accessor = new AccessorCore(obj);
        }
        
        public IList<TapeAccessor> GetAll()
        {
            var tapes = (IList<object>)_accessor.Call(MemberName());
            
            return tapes.Select(x => new TapeAccessor(x)).ToArray();
        }
    }
}
