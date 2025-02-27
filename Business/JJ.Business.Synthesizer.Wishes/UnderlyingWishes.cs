using System;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Wishes.docs;

namespace JJ.Business.Synthesizer.Wishes
{
    /// <inheritdoc cref="_underlyingextensions"/>
    public partial class FlowNode
    {
        /// <inheritdoc cref="_underlyingextensions"/>
        public Operator UnderlyingOperator => _underlyingOutlet.Operator;

        /// <inheritdoc cref="_underlyingcurve" />"/>
        public Curve UnderlyingCurve() => _underlyingOutlet.UnderlyingCurve();

        /// <inheritdoc cref="_underlyingcurve" />"/>
        public FlowNode UnderlyingCurve(Curve curve) { _underlyingOutlet.UnderlyingCurve(curve); return this; }

        /// <inheritdoc cref="_underlyingsample" />
        public Sample UnderlyingSample() => _underlyingOutlet.UnderlyingSample();
        
        /// <inheritdoc cref="_underlyingsample" />
        public FlowNode UnderlyingSample(Sample sample) { _underlyingOutlet.UnderlyingSample(sample); return this; }
    }

    /// <inheritdoc cref="_underlyingextensions"/>
    public static class UnderlyingExtensionWishes
    {
        /// <inheritdoc cref="_underlyingcurve" />"/>
        public static Curve UnderlyingCurve(this Outlet outlet)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            return UnderlyingCurve(outlet.Operator);
        }
        
        /// <inheritdoc cref="_underlyingcurve" />"/>
        public static Outlet UnderlyingCurve(this Outlet outlet, Curve curve)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            UnderlyingCurve(outlet.Operator, curve);
            return outlet;
        }

        /// <inheritdoc cref="_underlyingsample" />
        public static Sample UnderlyingSample(this Outlet outlet)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            return UnderlyingSample(outlet.Operator);
        }
        
        public static Outlet UnderlyingSample(this Outlet outlet, Sample sample)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            UnderlyingSample(outlet.Operator, sample);
            return outlet;
        }

        /// <inheritdoc cref="_underlyingcurve" />"/>
        public static Curve UnderlyingCurve(this Operator op)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));
            if (op.AsCurveIn == null) throw new NullException(() => op.AsCurveIn);
            return op.AsCurveIn.Curve;
        }
        
        /// <inheritdoc cref="_underlyingcurve" />
        public static Operator UnderlyingCurve(this Operator op, Curve curve)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));
            if (op.AsCurveIn == null) throw new NullException(() => op.AsCurveIn);
            op.AsCurveIn.Curve = curve;
            return op;
        }
        
        /// <inheritdoc cref="_underlyingsample" />
        public static Sample UnderlyingSample(this Operator op)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));
            if (op.AsSampleOperator == null) throw new NullException(() => op.AsSampleOperator);
            return op.AsSampleOperator.Sample;
        }

        /// <inheritdoc cref="_underlyingsample" />
        public static Operator UnderlyingSample(this Operator op, Sample sample)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));
            if (op.AsSampleOperator == null) throw new NullException(() => op.AsSampleOperator);
            op.AsSampleOperator.Sample = sample;
            return op;
        }
    }
}
