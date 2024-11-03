using System.Collections.Generic;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class SynthWishesAccessor
    {
        private readonly Accessor _accessor;
        
        public SynthWishesAccessor(SynthWishes obj)
        {
            _accessor = new Accessor(obj, typeof(SynthWishes));
        }

        public IList<FluentOutlet> FlattenTerms(FluentOutlet sumOrAdd)
        {
            return (IList<FluentOutlet>)_accessor.InvokeMethod(nameof(FlattenTerms), sumOrAdd);
        }

        public IList<FluentOutlet> FlattenFactors(IList<FluentOutlet> operands)
        { 
            return (IList<FluentOutlet>)_accessor.InvokeMethod(nameof(FlattenFactors), operands);
        }

        public IList<FluentOutlet> FlattenFactors(FluentOutlet multiply)
        {
            return (IList<FluentOutlet>)_accessor.InvokeMethod(nameof(FlattenFactors), multiply);
        }
    }
}
