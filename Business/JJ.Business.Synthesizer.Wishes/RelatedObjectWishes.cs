using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Persistence.Synthesizer;
using System;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Wishes
{
    // Related Object Wishes
    
    // OperatorWishes Missing Extensions
    
    /// <inheritdoc cref="_operatorextensionwishes"/>
    public static class OperatorExtensionsWishes
    {
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

    // Related Objects in FluentOutlet
    
    public partial class FluentOutlet
    {
        // Related Object

        /// <inheritdoc cref="_getsample" />
        public Sample GetSample() => _thisOutlet.GetSample();

        /// <inheritdoc cref="_getsamplewrapper" />
        public SampleOperatorWrapper GetSampleWrapper() => _thisOutlet.GetSampleWrapper();

        /// <inheritdoc cref="_getcurve" />"/>
        public Curve GetCurve() => _thisOutlet.GetCurve();

        /// <inheritdoc cref="_getcurvewrapper"/>
        public CurveInWrapper GetCurveWrapper() => _thisOutlet.GetCurveWrapper();
    }
}
