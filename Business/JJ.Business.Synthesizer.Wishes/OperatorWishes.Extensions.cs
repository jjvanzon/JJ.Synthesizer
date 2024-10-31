using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Warnings;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.Helpers.NameHelper;
using static JJ.Business.Synthesizer.Wishes.docs;

// ReSharper disable NotResolvedInText

namespace JJ.Business.Synthesizer.Wishes
{
    /// <inheritdoc cref="_operatorextensionwishes"/>
    public static class OperatorExtensionsWishes
    {
        // Name

        public static Outlet WithName(this Outlet entity, string name)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.Operator.WithName(name);
            return entity;
        }

        public static Operator WithName(this Operator op, string name)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));
            
            if (string.IsNullOrWhiteSpace(name)) return op;
            
            op.Name = name;

            if (op.AsCurveIn?.Curve != null)
            {
                op.AsCurveIn.Curve.Name = name;
            }

            if (op.AsSampleOperator?.Sample != null)
            {
                op.AsSampleOperator.Sample.Name = name;
            }

            return op;
        }

        public static Inlet WithName(this Inlet entity, string name)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.Input.WithName(name);
            return entity;
        }
        
        public static OperatorWrapperBase WithName(this OperatorWrapperBase wrapper, string name)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            wrapper.Operator.WithName(name);
            return wrapper;
        }
        
        // Missing
        
        public static Operator Operator(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Result == null) throw new NullException(() => wrapper.Result);
            return wrapper.Result.Operator;
        }
        
        public static Operator Operator(this CurveInWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Result == null) throw new NullException(() => wrapper.Result);
            return wrapper.Result.Operator;
        }

        // Calculate

        public static double Calculate(this Outlet outlet, double time, ChannelEnum channelEnum)
            => Calculate(outlet, time, channelEnum.ToIndex());

        public static double Calculate(this Outlet outlet, double time = 0, int channelIndex = 0) 
            => new OperatorCalculator(channelIndex).CalculateValue(outlet, time);

        public static double Calculate(this Operator op, double time, ChannelEnum channelEnum)
            => Calculate(op, time, channelEnum.ToIndex());

        public static double Calculate(this Operator op, double time = 0, int channelIndex = 0)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));

            Outlet outlet;
            switch (op.Outlets.Count)
            { 
                case 1: 
                    outlet = op.Outlets[0]; 
                    break;

                default: 
                    throw new Exception(
                        $"{nameof(Calculate)} can only be called on "+
                        $"{nameof(Operator)}s with exactly one {nameof(Outlet)}. " +
                        $"Consider calling {nameof(Operator)}.{nameof(Outlet)}.{nameof(Calculate)}() instead. " +
                        $"({nameof(op.OperatorTypeName)} = '{op.OperatorTypeName}')");
            }

            return Calculate(outlet, time, channelIndex);
        }

        public static double Calculate(this Inlet inlet, double time, ChannelEnum channelEnum)
            => Calculate(inlet, time, channelEnum.ToIndex());
        
        public static double Calculate(this Inlet inlet, double time = 0, int channelIndex = 0)
        {
            if (inlet == null) throw new ArgumentNullException(nameof(inlet));
            var calculator = new OperatorCalculator(channelIndex);
            return calculator.CalculateValue(inlet.Input, time);
        }

        public static double Calculate(this OperatorWrapperBase wrapper, double time, ChannelEnum channelEnum)
            => Calculate(wrapper, time, channelEnum.ToIndex());

        public static double Calculate(this OperatorWrapperBase wrapper, double time = 0, int channelIndex = 0)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return Calculate(wrapper.Operator, time, channelIndex);
        }

        public static double Calculate(this SampleOperatorWrapper wrapper, double time, ChannelEnum channelEnum)
            => Calculate(wrapper, time, channelEnum.ToIndex());

        public static double Calculate(this SampleOperatorWrapper wrapper, double time = 0, int channelIndex = 0)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return Calculate(wrapper.Result, time, channelIndex);
        }

        public static double Calculate(this CurveInWrapper wrapper, double time, ChannelEnum channelEnum)
            => Calculate(wrapper, time, channelEnum.ToIndex());

        public static double Calculate(this CurveInWrapper wrapper, double time = 0, int channelIndex = 0)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            
            
            return Calculate(wrapper.Result, time, channelIndex);
        }

        // String

        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this Outlet entity, bool singleLine = false)
            => new OperatorStringifier().StringifyRecursive(entity, singleLine);

        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this Operator entity, bool singleLine = false)
            => new OperatorStringifier().StringifyRecursive(entity, singleLine);

        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this Inlet entity, bool singleLine = false)
            => new OperatorStringifier().StringifyRecursive(entity, singleLine);

        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this OperatorWrapperBase wrapper, bool singleLine = false)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return new OperatorStringifier().StringifyRecursive(wrapper.Operator, singleLine);
        }

        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this SampleOperatorWrapper wrapper, bool singleLine = false)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return new OperatorStringifier().StringifyRecursive(wrapper.Result, singleLine);
        }

        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this CurveInWrapper wrapper, bool singleLine = false)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return new OperatorStringifier().StringifyRecursive(wrapper.Result, singleLine);
        }

        // Validation

        public static Result Validate(this Outlet entity, bool recursive = true)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return Validate(entity.Operator, recursive);
        }

        public static Result Validate(this Operator entity, bool recursive = true)
        {
            if (recursive)
            {
                return new RecursiveOperatorValidator(entity).ToResult();
            }
            else
            {
                return new VersatileOperatorValidator(entity).ToResult();
            }
        }

        public static Result Validate(this OperatorWrapperBase wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return Validate( wrapper.Operator, recursive);
        }

        public static Result Validate(this CurveInWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return Validate(wrapper.Result, recursive);
        }

        public static Result Validate(this SampleOperatorWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return Validate(wrapper.Result, recursive);
        }

        public static void Assert(this Outlet entity, bool recursive = true)
            => Validate(entity, recursive).Assert();

        public static void Assert(this Operator entity, bool recursive = true)
            => Validate(entity, recursive).Assert();
        
        public static void Assert(this OperatorWrapperBase wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            Assert( wrapper.Operator, recursive);
        }

        public static void Assert(this CurveInWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            Assert(wrapper.Result, recursive);
        }

        public static void Assert(this SampleOperatorWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            Assert(wrapper.Result, recursive);
        }

        public static IList<string> GetWarnings(this Operator entity, bool recursive = true)
        {
            IValidator validator;

            if (recursive)
            {
                validator = new RecursiveOperatorWarningValidator(entity);
            }
            else
            {
                validator = new VersatileOperatorWarningValidator(entity);
            }

            return validator.ValidationMessages.Select(x => x.Text).ToList();
        }

        public static IList<string> GetWarnings(this Outlet entity, bool recursive = true)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetWarnings(entity.Operator, recursive);
        }
                
        public static IList<string> GetWarnings(this OperatorWrapperBase wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetWarnings( wrapper.Operator, recursive);
        }

        public static IList<string> GetWarnings(this CurveInWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetWarnings(wrapper.Result, recursive);
        }

        public static IList<string> GetWarnings(this SampleOperatorWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetWarnings(wrapper.Result, recursive);
        }

        // As/Is

        /// <inheritdoc cref="_asconst"/>
        public static double? AsConst(this Inlet inlet) => inlet?.Input?.AsConst();

        /// <inheritdoc cref="_asconst"/>
        public static double? AsConst(this Outlet outlet) => outlet?.Operator?.AsConst();

        /// <inheritdoc cref="_asconst"/>
        public static double? AsConst(this Operator op) => op?.AsValueOperator?.Value;

        /// <inheritdoc cref="_asconst"/>
        public static bool IsConst(this Inlet inlet) => inlet?.AsConst() != null;

        /// <inheritdoc cref="_asconst"/>
        public static bool IsConst(this Outlet outlet) => outlet?.AsConst() != null;

        /// <inheritdoc cref="_asconst"/>
        public static bool IsConst(this Operator op) => op?.AsConst() != null;
        
        public static bool IsVar(this Inlet inlet)
        {
            if (inlet == null) throw new ArgumentNullException(nameof(inlet));
            return inlet.AsConst() == null;
        }

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

        public  static bool IsAdd      (this Inlet    entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public  static bool IsAdd      (this Outlet   entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public  static bool IsAdd      (this Operator entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public  static bool IsSubtract (this Inlet    entity) => HasOperatorTypeName(entity, nameof(Substract));
        public  static bool IsSubtract (this Outlet   entity) => HasOperatorTypeName(entity, nameof(Substract));
        public  static bool IsSubtract (this Operator entity) => HasOperatorTypeName(entity, nameof(Substract));
        public  static bool IsMultiply (this Inlet    entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public  static bool IsMultiply (this Outlet   entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public  static bool IsMultiply (this Operator entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public  static bool IsDivide   (this Inlet    entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public  static bool IsDivide   (this Outlet   entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public  static bool IsDivide   (this Operator entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public  static bool IsPower    (this Inlet    entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public  static bool IsPower    (this Outlet   entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public  static bool IsPower    (this Operator entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public  static bool IsSine     (this Inlet    entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public  static bool IsSine     (this Outlet   entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public  static bool IsSine     (this Operator entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public  static bool IsDelay    (this Inlet    entity) => HasOperatorTypeName(entity, nameof(TimeAdd));
        public  static bool IsDelay    (this Outlet   entity) => HasOperatorTypeName(entity, nameof(TimeAdd));
        public  static bool IsDelay    (this Operator entity) => HasOperatorTypeName(entity, nameof(TimeAdd));
        public  static bool IsSkip     (this Inlet    entity) => HasOperatorTypeName(entity, nameof(TimeSubstract));
        public  static bool IsSkip     (this Outlet   entity) => HasOperatorTypeName(entity, nameof(TimeSubstract));
        public  static bool IsSkip     (this Operator entity) => HasOperatorTypeName(entity, nameof(TimeSubstract));
        public  static bool IsStretch  (this Inlet    entity) => HasOperatorTypeName(entity, nameof(TimeMultiply));
        public  static bool IsStretch  (this Outlet   entity) => HasOperatorTypeName(entity, nameof(TimeMultiply));
        public  static bool IsStretch  (this Operator entity) => HasOperatorTypeName(entity, nameof(TimeMultiply));
        public  static bool IsSpeedUp  (this Inlet    entity) => HasOperatorTypeName(entity, nameof(TimeDivide));
        public  static bool IsSpeedUp  (this Outlet   entity) => HasOperatorTypeName(entity, nameof(TimeDivide));
        public  static bool IsSpeedUp  (this Operator entity) => HasOperatorTypeName(entity, nameof(TimeDivide));
        public  static bool IsTimePower(this Inlet    entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public  static bool IsTimePower(this Outlet   entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        public  static bool IsTimePower(this Operator entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        
        internal static bool IsAdder   (this Outlet   entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));
        internal static bool IsAdder   (this Operator entity) => HasOperatorTypeName(entity, MemberName().CutLeft("Is"));

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

        internal static bool HasOperatorTypeName(this Inlet inlet, string operatorTypeName)
        {
            if (inlet == null) throw new ArgumentNullException(nameof(inlet));
            return HasOperatorTypeName(inlet.Input, operatorTypeName);
        }
    }
}