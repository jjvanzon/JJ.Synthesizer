using System;
using JJ.Business.Synthesizer.Names;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.NameHelper;

namespace JJ.Business.Synthesizer.Wishes
{
    // Is/As
    
    // FluentOutlet
    
    public partial class FluentOutlet
    {
        public double? AsConst => _wrappedOutlet.AsConst();
        public bool IsConst => _wrappedOutlet.IsConst();
        public bool IsVar => _wrappedOutlet.IsVar();
        public bool IsAdd => _wrappedOutlet.IsAdd();
        public bool IsSubtract => _wrappedOutlet.IsSubtract();
        public bool IsMultiply => _wrappedOutlet.IsMultiply();
        public bool IsDivide => _wrappedOutlet.IsDivide();
        public bool IsPower => _wrappedOutlet.IsPower();
        public bool IsSine => _wrappedOutlet.IsSine();
        public bool IsDelay => _wrappedOutlet.IsDelay();
        public bool IsSkip => _wrappedOutlet.IsSkip();
        public bool IsStretch => _wrappedOutlet.IsStretch();
        public bool IsSpeedUp => _wrappedOutlet.IsSpeedUp();
        public bool IsTimePower => _wrappedOutlet.IsTimePower();
        public bool IsAdder => _wrappedOutlet.IsAdder();
    }

    public static class IsExtensionWishes
    {
        // Curves

        public static bool IsCurve(this Outlet entity)
            => HasOperatorTypeName(entity, nameof(PropertyNames.CurveIn));

        public static bool IsCurve(this Operator entity)
            => HasOperatorTypeName(entity, nameof(PropertyNames.CurveIn));

        // Samples / AudioFileOutput
        
        public static bool IsSample(this Outlet entity) 
            => HasOperatorTypeName(entity, PropertyNames.SampleOperator);

        public static bool IsSample(this Operator entity) 
            => HasOperatorTypeName(entity, nameof(PropertyNames.SampleOperator));

        // Operators

        /// <inheritdoc cref="_asconst"/>
        public static double? AsConst(this Outlet outlet) => outlet?.Operator?.AsConst();

        /// <inheritdoc cref="_asconst"/>
        public static double? AsConst(this Operator op) => op?.AsValueOperator?.Value;

        /// <inheritdoc cref="_asconst"/>
        public static bool IsConst(this Outlet outlet) => outlet?.AsConst() != null;

        /// <inheritdoc cref="_asconst"/>
        public static bool IsConst(this Operator op) => op?.AsConst() != null;

        public static bool IsVar(this Outlet outlet)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            return outlet.AsConst() == null;
        }

        public static bool IsVar(this Operator op)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));
            return op.AsConst() == null;
        }

        public static bool IsAdd(this Outlet entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public static bool IsAdd(this Operator entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public static bool IsSubtract(this Outlet entity) => HasOperatorTypeName(entity, nameof(PropertyNames.Substract));
        public static bool IsSubtract(this Operator entity) => HasOperatorTypeName(entity, nameof(PropertyNames.Substract));
        public static bool IsMultiply(this Outlet entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public static bool IsMultiply(this Operator entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public static bool IsDivide(this Outlet entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public static bool IsDivide(this Operator entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public static bool IsPower(this Outlet entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public static bool IsPower(this Operator entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public static bool IsSine(this Outlet entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public static bool IsSine(this Operator entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public static bool IsDelay(this Outlet entity) => HasOperatorTypeName(entity, nameof(PropertyNames.TimeAdd));
        public static bool IsDelay(this Operator entity) => HasOperatorTypeName(entity, nameof(PropertyNames.TimeAdd));
        public static bool IsSkip(this Outlet entity) => HasOperatorTypeName(entity, nameof(PropertyNames.TimeSubstract));
        public static bool IsSkip(this Operator entity) => HasOperatorTypeName(entity, nameof(PropertyNames.TimeSubstract));
        public static bool IsStretch(this Outlet entity) => HasOperatorTypeName(entity, nameof(PropertyNames.TimeMultiply));
        public static bool IsStretch(this Operator entity) => HasOperatorTypeName(entity, nameof(PropertyNames.TimeMultiply));
        public static bool IsSpeedUp(this Outlet entity) => HasOperatorTypeName(entity, nameof(PropertyNames.TimeDivide));
        public static bool IsSpeedUp(this Operator entity) => HasOperatorTypeName(entity, nameof(PropertyNames.TimeDivide));
        public static bool IsTimePower(this Outlet entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public static bool IsTimePower(this Operator entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public static bool IsAdder(this Outlet entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public static bool IsAdder(this Operator entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));

        internal static bool HasOperatorTypeName(this Outlet outlet, string operatorTypeName)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            return HasOperatorTypeName(outlet.Operator, operatorTypeName);
        }

        internal static bool HasOperatorTypeName(this Operator op, string operatorTypeName)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));
            return string.Equals(op.OperatorTypeName, operatorTypeName, StringComparison.Ordinal);
        }
    }
}