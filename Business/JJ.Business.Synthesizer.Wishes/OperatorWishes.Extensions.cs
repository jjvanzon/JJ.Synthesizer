using System;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.docs;

// ReSharper disable NotResolvedInText

namespace JJ.Business.Synthesizer.Wishes
{
    /// <inheritdoc cref="_operatorextensionwishes"/>
    public static class OperatorExtensionsWishes
    {
        // Missing
        
        public static Operator Operator(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Result == null) throw new NullException(() => wrapper.Result);
            return wrapper.Result.Operator;
        }
        
        public static Operator Operator(this CurveInWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Result == null) throw new NullException(() => wrapper.Result);
            return wrapper.Result.Operator;
        }

    }
}