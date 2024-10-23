using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Warnings;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Common;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.Helpers.NameHelper;

// ReSharper disable RedundantIfElseBlock

namespace JJ.Business.Synthesizer.Wishes
{
    /// <summary>
    /// Extensions that are wishes for the back-end related to the Operator entity.
    /// </summary>
    public static class OperatorExtensionsWishes
    {
        // Calculate

        public static double Calculate(this Outlet outlet, double time, int channelIndex = 0)
        {
            var calculator = new OperatorCalculator(channelIndex);
            return calculator.CalculateValue(outlet, time);
        }

        public static double Calculate(this Operator op, double time, int channelIndex = 0)
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

        public static double Calculate(this Inlet inlet, double time, int channelIndex = 0)
        {
            if (inlet == null) throw new ArgumentNullException(nameof(inlet));
            var calculator = new OperatorCalculator(channelIndex);
            return calculator.CalculateValue(inlet.Input, time);
        }

        public static double Calculate(this OperatorWrapperBase wrapper, double time, int channelIndex = 0)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return Calculate(wrapper.Operator, time, channelIndex);
        }

        // String

        public static string Stringify(this Outlet entity)
            => new OperatorStringifier().StringifyRecursive(entity);

        public static string Stringify(this Operator entity)
            => new OperatorStringifier().StringifyRecursive(entity);

        public static string Stringify(this Inlet entity)
            => new OperatorStringifier().StringifyRecursive(entity);

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

        public static void Assert(this Outlet entity, bool recursive = true)
            => Validate(entity, recursive).Assert();

        public static void Assert(this Operator entity, bool recursive = true)
            => Validate(entity, recursive).Assert();

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

        // As/Is

        /// <inheritdoc cref="docs._asconst"/>
        public static double? AsConst(this Inlet inlet) => inlet?.Input?.AsConst();

        /// <inheritdoc cref="docs._asconst"/>
        public static double? AsConst(this Outlet outlet) => outlet?.Operator?.AsConst();

        /// <inheritdoc cref="docs._asconst"/>
        public static double? AsConst(this Operator op) => op?.AsValueOperator?.Value;

        /// <inheritdoc cref="docs._asconst"/>
        public static bool IsConst(this Inlet inlet) => inlet?.AsConst() != null;

        /// <inheritdoc cref="docs._asconst"/>
        public static bool IsConst(this Outlet outlet) => outlet?.AsConst() != null;

        /// <inheritdoc cref="docs._asconst"/>
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

        public  static bool IsAdd         (this Inlet    entity) => HasOperatorTypeName(entity, Name().CutLeft("Is"));
        public  static bool IsAdd         (this Outlet   entity) => HasOperatorTypeName(entity, Name().CutLeft("Is"));
        public  static bool IsAdd         (this Operator entity) => HasOperatorTypeName(entity, Name().CutLeft("Is"));
        public  static bool IsSubtract    (this Inlet    entity) => HasOperatorTypeName(entity, nameof(Substract));
        public  static bool IsSubtract    (this Outlet   entity) => HasOperatorTypeName(entity, nameof(Substract));
        public  static bool IsSubtract    (this Operator entity) => HasOperatorTypeName(entity, nameof(Substract));
        public  static bool IsMultiply    (this Inlet    entity) => HasOperatorTypeName(entity, Name().CutLeft("Is"));
        public  static bool IsMultiply    (this Outlet   entity) => HasOperatorTypeName(entity, Name().CutLeft("Is"));
        public  static bool IsMultiply    (this Operator entity) => HasOperatorTypeName(entity, Name().CutLeft("Is"));
        public  static bool IsDivide      (this Inlet    entity) => HasOperatorTypeName(entity, Name().CutLeft("Is"));
        public  static bool IsDivide      (this Outlet   entity) => HasOperatorTypeName(entity, Name().CutLeft("Is"));
        public  static bool IsDivide      (this Operator entity) => HasOperatorTypeName(entity, Name().CutLeft("Is"));
        public  static bool IsPower       (this Inlet    entity) => HasOperatorTypeName(entity, Name().CutLeft("Is"));
        public  static bool IsPower       (this Outlet   entity) => HasOperatorTypeName(entity, Name().CutLeft("Is"));
        public  static bool IsPower       (this Operator entity) => HasOperatorTypeName(entity, Name().CutLeft("Is"));
        public  static bool IsSine        (this Inlet    entity) => HasOperatorTypeName(entity, Name().CutLeft("Is"));
        public  static bool IsSine        (this Outlet   entity) => HasOperatorTypeName(entity, Name().CutLeft("Is"));
        public  static bool IsSine        (this Operator entity) => HasOperatorTypeName(entity, Name().CutLeft("Is"));
        public  static bool IsDelay       (this Inlet    entity) => HasOperatorTypeName(entity, nameof(TimeAdd));
        public  static bool IsDelay       (this Outlet   entity) => HasOperatorTypeName(entity, nameof(TimeAdd));
        public  static bool IsDelay       (this Operator entity) => HasOperatorTypeName(entity, nameof(TimeAdd));
        public  static bool IsStretch     (this Inlet    entity) => HasOperatorTypeName(entity, nameof(TimeMultiply));
        public  static bool IsStretch     (this Outlet   entity) => HasOperatorTypeName(entity, nameof(TimeMultiply));
        public  static bool IsStretch     (this Operator entity) => HasOperatorTypeName(entity, nameof(TimeMultiply));
        public  static bool IsSquash      (this Inlet    entity) => HasOperatorTypeName(entity, nameof(TimeDivide));
        public  static bool IsSquash      (this Outlet   entity) => HasOperatorTypeName(entity, nameof(TimeDivide));
        public  static bool IsSquash      (this Operator entity) => HasOperatorTypeName(entity, nameof(TimeDivide));
        public  static bool IsTimeSubtract(this Inlet    entity) => HasOperatorTypeName(entity, nameof(TimeSubstract));
        public  static bool IsTimeSubtract(this Outlet   entity) => HasOperatorTypeName(entity, nameof(TimeSubstract));
        public  static bool IsTimeSubtract(this Operator entity) => HasOperatorTypeName(entity, nameof(TimeSubstract));
        public  static bool IsTimePower   (this Inlet    entity) => HasOperatorTypeName(entity, Name().CutLeft("Is"));
        public  static bool IsTimePower   (this Outlet   entity) => HasOperatorTypeName(entity, Name().CutLeft("Is"));
        public  static bool IsTimePower   (this Operator entity) => HasOperatorTypeName(entity, Name().CutLeft("Is"));
        
        internal static bool IsAdder       (this Outlet   entity) => HasOperatorTypeName(entity, Name().CutLeft("Is"));
        internal static bool IsAdder       (this Operator entity) => HasOperatorTypeName(entity, Name().CutLeft("Is"));

        private static bool HasOperatorTypeName(this Outlet outlet, string operatorTypeName)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            return HasOperatorTypeName(outlet.Operator, operatorTypeName);
        }

        private static bool HasOperatorTypeName(this Operator op, string operatorTypeName)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));
            return string.Equals(op.OperatorTypeName, operatorTypeName, StringComparison.Ordinal);
        }

        private static bool HasOperatorTypeName(this Inlet inlet, string operatorTypeName)
        {
            if (inlet == null) throw new ArgumentNullException(nameof(inlet));
            return HasOperatorTypeName(inlet.Input, operatorTypeName);
        }
    }
}