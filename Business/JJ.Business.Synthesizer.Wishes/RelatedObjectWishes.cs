using JJ.Persistence.Synthesizer;
using System;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Wishes
{
    /// <inheritdoc cref="docs._relatedobjectextensions"/>
    public partial class FlowNode
    {
        /// <inheritdoc cref="docs._relatedobjectextensions"/>
        public Operator UnderlyingOperator => _underlyingOutlet.Operator;

        /// <inheritdoc cref="docs._underlyingcurve" />"/>
        public Curve UnderlyingCurve() => _underlyingOutlet.UnderlyingCurve();

        /// <inheritdoc cref="docs._underlyingsample" />
        public Sample UnderlyingSample() => _underlyingOutlet.UnderlyingSample();
    }

    /// <inheritdoc cref="docs._relatedobjectextensions"/>
    public static class RelatedObjectExtensionWishes
    {
        /// <inheritdoc cref="docs._underlyingcurve" />"/>
        public static Curve UnderlyingCurve(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return UnderlyingCurve(entity.Operator);
        }

        /// <inheritdoc cref="docs._underlyingsample" />
        public static Sample UnderlyingSample(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return UnderlyingSample(entity.Operator);
        }

        /// <inheritdoc cref="docs._underlyingcurve" />"/>
        public static Curve UnderlyingCurve(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AsCurveIn == null) throw new NullException(() => entity.AsCurveIn);
            return entity.AsCurveIn.Curve;
        }

        /// <inheritdoc cref="docs._underlyingsample" />
        public static Sample UnderlyingSample(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AsSampleOperator == null) throw new NullException(() => entity.AsSampleOperator);
            return entity.AsSampleOperator.Sample;
        }
    }
}
