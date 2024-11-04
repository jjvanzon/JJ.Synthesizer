using JJ.Persistence.Synthesizer;
using System;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.docs;

namespace JJ.Business.Synthesizer.Wishes
{
    // RelatedObjectWishes
    
    // Operator
    
    public partial class FluentOutlet
    {
        /// <inheritdoc cref="_relatedobjectextensions"/>
        public Operator Operator => _wrappedOutlet.Operator;
    }
    
    // Curve
        
    public partial class FluentOutlet
    {
        /// <inheritdoc cref="_getcurve" />"/>
        public Curve Curve() => _wrappedOutlet.Curve();
    }

    /// <inheritdoc cref="_relatedobjectextensions"/>
    public static partial class RelatedObjectExtensionWishes
    {
        /// <inheritdoc cref="_getcurve" />"/>
        public static Curve Curve(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return Curve(entity.Operator);
        }

        /// <inheritdoc cref="_getcurve" />"/>
        public static Curve Curve(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AsCurveIn == null) throw new NullException(() => entity.AsCurveIn);
            return entity.AsCurveIn.Curve;
        }
    }

    // Sample
    
    public partial class FluentOutlet
    {
        /// <inheritdoc cref="_getsample" />
        public Sample Sample() => _wrappedOutlet.Sample();
    }

    /// <inheritdoc cref="_relatedobjectextensions"/>
    public static partial class RelatedObjectExtensionWishes
    {
        /// <inheritdoc cref="_getsample" />
        public static Sample Sample(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return Sample(entity.Operator);
        }

        /// <inheritdoc cref="_getsample" />
        public static Sample Sample(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AsSampleOperator == null) throw new NullException(() => entity.AsSampleOperator);
            return entity.AsSampleOperator.Sample;
        }
    }
}
