using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Persistence.Synthesizer;
using System;
using JJ.Framework.Reflection;
using System.Linq;
using static JJ.Business.Synthesizer.Wishes.docs;

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
        /// <inheritdoc cref="_getsample" />
        public Sample GetSample() => _this.GetSample();

        /// <inheritdoc cref="_getsamplewrapper" />
        public SampleOperatorWrapper GetSampleWrapper() => _this.GetSampleWrapper();

        /// <inheritdoc cref="_getcurve" />"/>
        public Curve GetCurve() => _this.GetCurve();

        /// <inheritdoc cref="_getcurvewrapper"/>
        public CurveInWrapper GetCurveWrapper() => _this.GetCurveWrapper();

        public FluentOutlet A => _[Outlet.Operator?.Inlets.ElementAtOrDefault(0)?.Input];
        public FluentOutlet B => _[Outlet.Operator?.Inlets.ElementAtOrDefault(1)?.Input];
    }
}
