using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Validation.Entities;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
// ReSharper disable InvokeAsExtensionMethod

namespace JJ.Business.Synthesizer.Wishes
{
    public static class CurveExtensionWishes
    {
        // Name

        public static Curve WithName(this Curve entity, string name)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.Name = name;
            return entity;
        }
                
        public static CurveInWrapper WithName(this CurveInWrapper wrapper, string name)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Operator() == null) throw new ArgumentNullException("wrapper.Operator()");
            
            wrapper.Operator().WithName(name);
            wrapper.Curve.WithName(name);

            return wrapper;
        }

        // Calculation

        public static double Calculate(this Curve curve, double time)
            => new CurveCalculator(curve).CalculateValue(time);

        // Validation

        public static void Assert(this Curve curve)
            => new CurveValidator(curve).Verify();

        public static void Assert(this Node node)
            => new NodeValidator(node).Verify();

        public static Result Validate(this Curve curve)
            => new CurveValidator(curve).ToResult();

        public static Result Validate(this Node node)
            => new NodeValidator(node).ToResult();

        // Is / As

        public static bool IsCurve(this Outlet entity)
            => OperatorExtensionsWishes.HasOperatorTypeName(entity, nameof(CurveIn));

        public static bool IsCurve(this Operator entity)
            => OperatorExtensionsWishes.HasOperatorTypeName(entity, nameof(CurveIn));

        public static bool IsCurve(this Inlet entity)
            => OperatorExtensionsWishes.HasOperatorTypeName(entity, nameof(CurveIn));

        public static Curve GetCurve(this Inlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetCurve(entity.Input);
        }

        public static Curve GetCurve(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetCurve(entity.Operator);
        }

        public static Curve GetCurve(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AsCurveIn == null) throw new NullException(() => entity.AsCurveIn);
            return entity.AsCurveIn.Curve;
        }

        public static CurveInWrapper GetCurveWrapper(this Inlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetCurveWrapper(entity.Input);
        }

        public static CurveInWrapper GetCurveWrapper(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetCurveWrapper(entity.Operator);
        }

        public static CurveInWrapper GetCurveWrapper(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return new CurveInWrapper(entity.AsCurveIn);
        }
    }
}