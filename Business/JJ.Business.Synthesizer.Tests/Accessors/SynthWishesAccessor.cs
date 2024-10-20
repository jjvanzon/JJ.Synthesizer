using System;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class SynthWishesAccessor
    {
        private readonly Accessor _accessor;
        
        public SynthWishesAccessor(SynthWishes obj)
        {
            _accessor = new Accessor(obj);
        }

        public CurveInWrapper GetOrCreateCurveIn(string name, Func<CurveInWrapper> func)
            => (CurveInWrapper)_accessor.InvokeMethod(nameof(GetOrCreateCurveIn), name, func);
    }
}
