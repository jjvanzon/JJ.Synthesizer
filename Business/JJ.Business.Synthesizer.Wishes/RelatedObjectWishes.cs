using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Persistence.Synthesizer;
using System;
using JJ.Framework.Reflection;
using System.Linq;
using static JJ.Business.Synthesizer.Wishes.docs;

namespace JJ.Business.Synthesizer.Wishes
{
    // Related Object Wishes
    
    // Operator Extension Method

    public partial class FluentOutlet
    {
        /// <inheritdoc cref="_operatorextensionwishes"/>
        public Operator Operator => _this.Operator;
    }

    /// <inheritdoc cref="_operatorextensionwishes"/>
    public static partial class RelatedObjectsExtensionWishes
    {
        /// <inheritdoc cref="_operatorextensionwishes"/>
        public static Operator Operator(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Result == null) throw new NullException(() => wrapper.Result);
            return wrapper.Result.Operator;
        }
        
        /// <inheritdoc cref="_operatorextensionwishes"/>
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
    }

    // Operands

    // TODO: Make conditional and throw.

    public partial class FluentOutlet
    {
        public FluentOutlet A => _[Outlet.A()];
        public FluentOutlet B => _[Outlet.B()];
    }

    public static partial class RelatedObjectsExtensionWishes
    { 
        public static Outlet A(this Outlet outlet) => A(outlet.Operator);
        public static Outlet B(this Outlet outlet) => B(outlet.Operator);

        public static Outlet A(this Operator op) => op.Inlets.ElementAtOrDefault(0)?.Input;
        public static Outlet B(this Operator op) => op.Inlets.ElementAtOrDefault(1)?.Input;
        public static Outlet Result(this Operator op) => op.Outlets[0];
    }

    // Operand Origin

    public partial class FluentOutlet
    {
        [Obsolete("Rarely used because default origin 0 usually works. " +
                  "Otherwise consider use separate operators like Shift and Stretch instead.")]
        public FluentOutlet Origin => _[Outlet.Operator?.Inlets.ElementAtOrDefault(2)?.Input];

    }

    public static partial class RelatedObjectsExtensionWishes
    { 
        [Obsolete("Rarely used because default origin 0 usually works. " +
                  "Otherwise consider use separate operators like Shift and Stretch instead.")]
        public static Outlet Origin(this Operator op) => op.Inlets.ElementAtOrDefault(2)?.Input;
    }
}
