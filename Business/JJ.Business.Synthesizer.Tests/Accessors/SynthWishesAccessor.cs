using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class SynthWishesAccessor
    {
        private readonly Accessor _accessor;
        
        public SynthWishesAccessor(SynthWishes obj)
        {
            _accessor = new Accessor(obj, typeof(SynthWishes));
        }

        public CurveInWrapper GetOrCreateCurveIn(string name, Func<CurveInWrapper> func)
        {
            return (CurveInWrapper)_accessor.InvokeMethod(nameof(GetOrCreateCurveIn), name, func);
        }

        public IList<Outlet> FlattenTerms(Outlet sumOrAdd)
        {
            return (IList<Outlet>)_accessor.InvokeMethod(nameof(FlattenTerms), sumOrAdd);
        }

        public IList<Outlet> FlattenFactors(IList<Outlet> operands)
        { 
            return (IList<Outlet>)_accessor.InvokeMethod(nameof(FlattenFactors), operands);
        }

        public IList<Outlet> FlattenFactors(Outlet multiply)
        {
            return (IList<Outlet>)_accessor.InvokeMethod(nameof(FlattenFactors), multiply);
        }
    }
}
