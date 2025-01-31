using System;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.Obsolete.InletObsoleteMessages;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Wishes.Obsolete
{
    internal static class InletObsoleteMessages
    {
        public const string ObsoleteMessage =
            "Direct use of Inlets is rare and cumbersome. " +
            "Use Operators, Outlets, FlowNode and SynthWishes instead. " +
            "If direct Inlet access is needed for advanced use cases, this message can be ignored.";
    }
    
    // InletWishes CalculationWishes

    [Obsolete(ObsoleteMessage)]
    public static class InletCalculationExtensions
    {
        [Obsolete(ObsoleteMessage)]
        public static double Calculate(this Inlet inlet, double time, ChannelEnum channelEnum)
        {
            if (!Has(channelEnum)) throw new Exception($"{nameof(channelEnum)} not defined.");
            return Calculate(inlet, time, channelEnum.Channel().Value);
        }
        
        [Obsolete(ObsoleteMessage)]
        public static double Calculate(this Inlet inlet, double time = 0, int channel = 0)
        {
            if (inlet == null) throw new ArgumentNullException(nameof(inlet));
            var calculator = new OperatorCalculator(channel);
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
        /// <inheritdoc cref="docs._asconst" />
        public static double? AsConst(this Inlet inlet) => inlet?.Input?.AsConst();

        [Obsolete(ObsoleteMessage)]
        /// <inheritdoc cref="docs._asconst" />
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
    
    // InletWishes from UnderlyingWishes

    [Obsolete(ObsoleteMessage)]
    public static class UnderlyingInletExtensions 
    {
        [Obsolete(ObsoleteMessage)]
        /// <inheritdoc cref="docs._getcurve" />"/>
        public static Curve Curve(this Inlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return entity.Input.UnderlyingCurve();
        }

        [Obsolete(ObsoleteMessage)]
        /// <inheritdoc cref="docs._getsample" />
        public static Sample Sample(this Inlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return entity.Input.UnderlyingSample();
        }
    }
    
    // InletWishes from StringifyWishes

    [Obsolete(ObsoleteMessage)]
    public static class InletStringifyExtensions
    {
        [Obsolete(ObsoleteMessage)]
        /// <inheritdoc cref="docs._stringify"/>
        public static string Stringify(this Inlet entity, bool singleLine = false, bool canOmitNameForBasicMath = false)
            => new OperatorStringifier(singleLine, canOmitNameForBasicMath).StringifyRecursive(entity);

        [Obsolete(InletObsoleteMessages.ObsoleteMessage)]
        internal static string StringifyRecursive(this OperatorStringifier operatorStringifier, Inlet entity)
        {
            if (operatorStringifier == null) throw new ArgumentNullException(nameof(operatorStringifier));
            operatorStringifier._sb = operatorStringifier.CreateStringBuilder();
            operatorStringifier.BuildStringRecursive(entity);
            return OperatorStringifier.RemoveOuterBraces(operatorStringifier._sb.ToString());
        }
    }

    // Inlet Complexity

    [Obsolete(ObsoleteMessage)]
    public static class InletComplexityExtensions
    {
        [Obsolete(ObsoleteMessage)]
        public static int Complexity(this Inlet inlet)
        {
            if (inlet == null) throw new ArgumentNullException(nameof(inlet));
            return inlet.Input?.Complexity() ?? 0;
        }
    }

}