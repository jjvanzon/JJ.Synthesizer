using System;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Names;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.InletObsoleteMessages;
using static JJ.Business.Synthesizer.Wishes.NameHelper;

namespace JJ.Business.Synthesizer.Wishes
{
    internal static class InletObsoleteMessages
    {
        public const string ObsoleteMessage =
            "Direct use of Inlets is rare and cumbersome. " +
            "Use Operators, Outlets, FluentOutlet and SynthWishes instead. " +
            "If direct Inlet access is needed for advanced use cases, this message can be ignored.";
    }
    
    // InletWishes CalculationWishes

    [Obsolete(ObsoleteMessage)]
    public static class InletCalculationExtensions
    {
        [Obsolete(ObsoleteMessage)]
        public static double Calculate(this Inlet inlet, double time, ChannelEnum channelEnum)
            => Calculate(inlet, time, channelEnum.ToIndex());

        [Obsolete(ObsoleteMessage)]
        public static double Calculate(this Inlet inlet, double time = 0, int channelIndex = 0)
        {
            if (inlet == null) throw new ArgumentNullException(nameof(inlet));
            var calculator = new OperatorCalculator(channelIndex);
            return calculator.CalculateValue(inlet.Input, time);
        }
    }

    // InletWishes from IsWishes
    
    [Obsolete(ObsoleteMessage)]
    public static class InletIsExtensions
    {
        [Obsolete(ObsoleteMessage)]
        public static bool IsCurve(this Inlet entity)
            => HasOperatorTypeName(entity, PropertyNames.CurveIn);

        [Obsolete(ObsoleteMessage)]
        public static bool IsSample(this Inlet entity)
            => HasOperatorTypeName(entity, nameof(PropertyNames.SampleOperator));

        [Obsolete(ObsoleteMessage)]
        /// <inheritdoc cref="_asconst" />
        public static double? AsConst(this Inlet inlet) => inlet?.Input?.AsConst();

        [Obsolete(ObsoleteMessage)]
        /// <inheritdoc cref="_asconst" />
        public static bool IsConst(this Inlet inlet) => inlet?.AsConst() != null;

        [Obsolete(ObsoleteMessage)]
        public static bool IsVar(this Inlet inlet)
        {
            if (inlet == null) throw new ArgumentNullException(nameof(inlet));
            return inlet.AsConst() == null;
        }

        [Obsolete(ObsoleteMessage)]
        public static bool IsAdd(this Inlet entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        
        [Obsolete(ObsoleteMessage)]
        public static bool IsSubtract(this Inlet entity) => HasOperatorTypeName(entity, nameof(PropertyNames.Substract));
        
        [Obsolete(ObsoleteMessage)]
        public static bool IsMultiply(this Inlet entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        
        [Obsolete(ObsoleteMessage)]
        public static bool IsDivide(this Inlet entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        
        [Obsolete(ObsoleteMessage)]
        public static bool IsPower(this Inlet entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        
        [Obsolete(ObsoleteMessage)]
        public static bool IsSine(this Inlet entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        
        [Obsolete(ObsoleteMessage)]
        public static bool IsDelay(this Inlet entity) => HasOperatorTypeName(entity, nameof(PropertyNames.TimeAdd));
        
        [Obsolete(ObsoleteMessage)]
        public static bool IsSkip(this Inlet entity) => HasOperatorTypeName(entity, nameof(PropertyNames.TimeSubstract));
        
        [Obsolete(ObsoleteMessage)]
        public static bool IsStretch(this Inlet entity) => HasOperatorTypeName(entity, nameof(PropertyNames.TimeMultiply));
        
        [Obsolete(ObsoleteMessage)]
        public static bool IsSpeedUp(this Inlet entity) => HasOperatorTypeName(entity, nameof(PropertyNames.TimeDivide));
        
        [Obsolete(ObsoleteMessage)]
        public static bool IsTimePower(this Inlet entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        
        [Obsolete(ObsoleteMessage)]
        public static bool IsAdder(this Inlet entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));

        [Obsolete(ObsoleteMessage)]
        internal static bool HasOperatorTypeName(this Inlet inlet, string operatorTypeName)
        {
            if (inlet == null) throw new ArgumentNullException(nameof(inlet));
            // TODO: Exception is possible here?
            return inlet.Input.HasOperatorTypeName(operatorTypeName);
        }
    }

    // InletWishes from NameWishes
    
    [Obsolete(ObsoleteMessage)]
    public static class InletNameExtensions
    {
        /// <inheritdoc cref="docs._names"/>
        [Obsolete(ObsoleteMessage)]
        public static Inlet SetName(this Inlet entity, string name)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.Input.SetName(name);
            return entity;
        }
    }
    
    // InletWishes from RelatedObjectWishes

    [Obsolete(ObsoleteMessage)]
    public static class InletRelatedObjectExtensions 
    {
        [Obsolete(ObsoleteMessage)]
        /// <inheritdoc cref="_getcurve" />"/>
        public static Curve Curve(this Inlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return entity.Input.Curve();
        }

        [Obsolete(ObsoleteMessage)]
        /// <inheritdoc cref="_getsample" />
        public static Sample Sample(this Inlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return entity.Input.Sample();
        }
    }
    
    // InletWishes from StringifyWishes

    [Obsolete(ObsoleteMessage)]
    public static class InletStringifyExtensions
    {
        [Obsolete(ObsoleteMessage)]
        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this Inlet entity, bool singleLine = false, bool mustUseShortOperators = false)
            => new OperatorStringifier(singleLine, mustUseShortOperators).StringifyRecursive(entity);
    }

    internal partial class OperatorStringifier
    {
        [Obsolete(ObsoleteMessage)]
        public string StringifyRecursive(Inlet entity)
        {
            _sb = CreateStringBuilder();
            BuildStringRecursive(entity);
            return RemoveOuterBraces(_sb.ToString());
        }
    }
}