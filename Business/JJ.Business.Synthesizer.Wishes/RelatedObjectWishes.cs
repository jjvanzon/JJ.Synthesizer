using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Persistence.Synthesizer;
using System;
using JJ.Framework.Reflection;
using System.Linq;
using JJ.Business.Synthesizer.Wishes.Helpers;
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
    
    /// <inheritdoc cref="_relatedobjectextensions"/>
    public static partial class RelatedObjectsExtensionWishes
    {
        /// <inheritdoc cref="_relatedobjectextensions"/>
        public static Operator Operator(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Result == null) throw new NullException(() => wrapper.Result);
            return wrapper.Result.Operator;
        }
        
        /// <inheritdoc cref="_relatedobjectextensions"/>
        public static Operator Operator(this CurveInWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Result == null) throw new NullException(() => wrapper.Result);
            return wrapper.Result.Operator;
        }
    }

    // Curve
        
    public partial class FluentOutlet
    {
        /// <inheritdoc cref="_getcurve" />"/>
        public Curve Curve() => _wrappedOutlet.Curve();

        /// <inheritdoc cref="_getcurvewrapper"/>
        public CurveInWrapper GetCurveWrapper() => _wrappedOutlet.GetCurveWrapper();
    }

    /// <inheritdoc cref="_relatedobjectextensions"/>
    public static partial class RelatedObjectsExtensionWishes
    {
        /// <inheritdoc cref="_getcurve" />"/>
        public static Curve Curve(this Inlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return Curve(entity.Input);
        }

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

        /// <inheritdoc cref="_getcurvewrapper"/>
        public static CurveInWrapper GetCurveWrapper(this Inlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetCurveWrapper(entity.Input);
        }

        /// <inheritdoc cref="_getcurvewrapper"/>
        public static CurveInWrapper GetCurveWrapper(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetCurveWrapper(entity.Operator);
        }

        /// <inheritdoc cref="_getcurvewrapper"/>
        public static CurveInWrapper GetCurveWrapper(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return new CurveInWrapper(entity.AsCurveIn);
        }
    }

    // Sample
    
    public partial class FluentOutlet
    {
        /// <inheritdoc cref="_getsample" />
        public Sample Sample() => _wrappedOutlet.Sample();

        /// <inheritdoc cref="_getsamplewrapper" />
        public SampleOperatorWrapper GetSampleWrapper() => _wrappedOutlet.GetSampleWrapper();
    }

    /// <inheritdoc cref="_relatedobjectextensions"/>
    public static partial class RelatedObjectsExtensionWishes
    {
        /// <inheritdoc cref="_getsample" />
        public static Sample Sample(this Inlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return Sample(entity.Input);
        }

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

        /// <inheritdoc cref="_getsamplewrapper" />
        public static SampleOperatorWrapper GetSampleWrapper(this Inlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetSampleWrapper(entity.Input);
        }

        /// <inheritdoc cref="_getsamplewrapper" />
        public static SampleOperatorWrapper GetSampleWrapper(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetSampleWrapper(entity.Operator);
        }

        /// <inheritdoc cref="_getsamplewrapper" />
        public static SampleOperatorWrapper GetSampleWrapper(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return new SampleOperatorWrapper(entity.AsSampleOperator);
        }
    }
}
