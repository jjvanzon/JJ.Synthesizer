using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Validation.Entities;
using JJ.Persistence.Synthesizer;
// ReSharper disable InvokeAsExtensionMethod

namespace JJ.Business.Synthesizer.Wishes
{
    public static class CurveExtensionWishes
    {
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
        
        // IsCurve
        
        public static bool IsCurve(this Outlet entity) 
            => OperatorExtensionsWishes.HasOperatorTypeName(entity, nameof(CurveIn));

        public static bool IsCurve(this Operator entity) 
            => OperatorExtensionsWishes.HasOperatorTypeName(entity, nameof(CurveIn));

        public static bool IsCurve(this Inlet entity) 
            => OperatorExtensionsWishes.HasOperatorTypeName(entity, nameof(CurveIn));
    }
}