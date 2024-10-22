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

        // IsConst

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

        // Operators
        
        public static bool IsAdd(this Outlet outlet)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            return IsAdd(outlet.Operator);
        }

        public static bool IsAdd(this Operator op)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));
            return string.Equals(op.OperatorTypeName, nameof(Add), StringComparison.Ordinal);
        }
        
        public static bool IsAdder(this Outlet operand)
        {
            if (operand == null) throw new ArgumentNullException(nameof(operand));
            return IsAdder(operand.Operator);
        }

        public static bool IsAdder(this Operator op)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));
            return string.Equals(op.OperatorTypeName, nameof(Adder), StringComparison.Ordinal);
        }

        /// <inheritdoc cref="docs._add"/>
        public static Outlet Add(this OperatorFactory x, Outlet operandA, Outlet operandB)
        {
            return Sum(x, operandA, operandB);
        }

        /// <inheritdoc cref="docs._sum"/>
        public static Outlet Sum(this OperatorFactory x, params Outlet[] operands) 
            => Sum(x, (IList<Outlet>)operands);

        /// <inheritdoc cref="docs._sum"/>
        public static Outlet Sum(this OperatorFactory x, IList<Outlet> operands)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (operands == null) throw new ArgumentNullException(nameof(operands));
            
            // Flatten Nested Sums
            IList<Outlet> flattenedTerms = FlattenTerms(operands);
            
            // Consts
            IList<Outlet> vars = flattenedTerms.Where(y => !y.IsConst()).ToArray();
            double constant = flattenedTerms.Sum(y => y.AsConst() ?? 0);
            
            if (constant != 0)  // Skip Identity 0
            {
                Outlet constOutlet = x.Value(constant);
                flattenedTerms = vars.Concat(new [] { constOutlet }).ToArray();
            }

            switch (flattenedTerms.Count)
            {
                case 0:
                    return x.Value(0);
                
                case 1:
                    // Return single term
                    return flattenedTerms[0];
                
                case 2:
                    // Simple Add for 2 Operands
                    return x.Add(flattenedTerms[0], flattenedTerms[1]);
                
                default:
                    // Make Normal Adder
                    return x.Adder(flattenedTerms);
            }
        }

        /// <summary> Alternative entry point (Operator) Outlet. </summary>
        [UsedImplicitly]
        private static IList<Outlet> FlattenTerms(Outlet sumOrAdd)
        {
            if (sumOrAdd == null) throw new ArgumentNullException(nameof(sumOrAdd));
            
            if (sumOrAdd.IsAdd())
            {
                var add = new Add(sumOrAdd.Operator);
                return FlattenTerms(add.OperandA, add.OperandB);
            }

            if (sumOrAdd.IsAdder())
            {
                var sum = new Adder(sumOrAdd.Operator);
                return FlattenTerms(sum.Operands);
            }

            throw new Exception("sumOrAdd is not a Sum / Adder or Add operator.");
        }

        private static IList<Outlet> FlattenTerms(params Outlet[] operands)
            => FlattenTerms((IList<Outlet>)operands);

        private static IList<Outlet> FlattenTerms(IList<Outlet> operands)
        {
            return operands.SelectMany(x =>
            {
                if (x.IsAdder() || x.IsAdd())
                {
                    var wrapper = new Adder(x.Operator);
                    return FlattenTerms(wrapper.Operands);
                }
                else
                {
                    // Wrap the single operand in a list
                    return new List<Outlet> { x }; 
                }
            }).ToList();
        }
                
        public static bool IsMultiply(this Outlet outlet)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            return IsMultiply(outlet.Operator);
        }

        public static bool IsMultiply(this Operator op)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));
            return string.Equals(op.OperatorTypeName, nameof(Multiply), StringComparison.Ordinal);
        }
        
        /// <inheritdoc cref="docs._multiply"/>
        public static Outlet Multiply(this OperatorFactory x, Outlet operandA, Outlet operandB, Outlet origin = null)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));

            if (origin != null)
            {
                return x.Multiply(operandA, operandB, origin);
            }

            // Flatten Nested Sums
            IList<Outlet> flattenedFactors = FlattenFactors(operandA, operandB);
            
            // Consts
            IList<Outlet> vars = flattenedFactors.Where(y => !y.IsConst()).ToArray();
            double constant = flattenedFactors.Product(y => y.AsConst() ?? 1);

            IList<Outlet> factors = new List<Outlet>(vars);
            if (constant != 1)  // Skip Identity 1
            {
                factors.Add(x.Value(constant));
            }

            switch (factors.Count)
            {
                case 0:
                    // Return identity 1
                    return x.Value(1);
                
                case 1:
                    // Return single number
                    return factors[0];
                
                case 2:
                    // Simple Multiply for 2 Operands
                    return x.Multiply(factors[0], factors[1]);
                
                default:
                    // Re-nest remaining factors
                    return NestMultiplications(x, factors);
            }
        }

        private static Outlet NestMultiplications(OperatorFactory x, IList<Outlet> flattenedFactors)
        {
            // Base case: If there's only one factor, return it
            if (flattenedFactors.Count == 1)
            {
                return flattenedFactors[0];
            }

            //if (flattenedFactors.Count == 2)
            //{
            //    return x.Multiply(flattenedFactors[0], flattenedFactors[1]);
            //}

            // Recursive case: Nest the first factor with the result of nesting the rest
            var firstFactor = flattenedFactors[0];
            var remainingFactors = flattenedFactors.Skip(1).ToList();

            // Recursively nest the remaining factors and multiply with the first
            return x.Multiply(firstFactor, NestMultiplications(x, remainingFactors));
        }

        /// <inheritdoc cref="docs._multiply"/>
        private static Outlet Multiply_Old(this OperatorFactory x, Outlet operandA, Outlet operandB, Outlet origin = null)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            
            operandA = operandA ?? x.Value(1);
            operandB = operandB ?? x.Value(1);

            if (origin == null)
            {
                double? constOperandA = operandA.AsConst();
                double? constOperandB = operandB.AsConst();

                // Const
                if (constOperandA != null && constOperandB != null)
                {
                    double multiplied = constOperandA.Value * constOperandB.Value;
                    return x.Value(multiplied);
                }

                // Identity 1
                if (constOperandA == 1)
                {
                    return operandB;
                }

                if (constOperandB == 1)
                {
                    return operandA;
                }
            }

            return x.Multiply(operandA, operandB, origin);
        }
        
        /// <summary> Alternative entry point (Operator) Outlet. </summary>
        public static IList<Outlet> FlattenFactors(Outlet multiplyOutlet)
        {
            if (multiplyOutlet == null) throw new ArgumentNullException(nameof(multiplyOutlet));

            if (!multiplyOutlet.IsMultiply())
            {
                throw new Exception($"{nameof(multiplyOutlet)} parameter is not a Multiply operator.");
            }

            var multiplyWrapper = new Multiply(multiplyOutlet.Operator);
            return FlattenFactors(multiplyWrapper.OperandA, multiplyWrapper.OperandB);
        }

        public static IList<Outlet> FlattenFactors(params Outlet[] operands)
            => FlattenFactors((IList<Outlet>)operands);

        public static IList<Outlet> FlattenFactors(IList<Outlet> operands)
        {
            return operands.SelectMany(x =>
            {
                if (x.IsMultiply())
                {
                    var multiplyWrapper = new Multiply(x.Operator);
                    return FlattenFactors(multiplyWrapper.OperandA, multiplyWrapper.OperandB);
                }
                else
                {
                    // Wrap the single operand in a list
                    return new List<Outlet> { x }; 
                }
            }).ToList();
        }

        /// <inheritdoc cref="docs._default" />
        public static Outlet Stretch(this OperatorFactory operatorFactory, Outlet signal, Outlet timeFactor)
        {
            if (operatorFactory == null) throw new ArgumentNullException(nameof(operatorFactory));
            var x = operatorFactory;
            
            return x.TimeMultiply(signal, timeFactor ?? x.Value(1));
        }

        /// <inheritdoc cref="docs._sine" />
        public static Outlet Sine(this OperatorFactory operatorFactory, Outlet pitch)
        {
            if (operatorFactory == null) throw new ArgumentNullException(nameof(operatorFactory));
            var x = operatorFactory;
            
            return x.Sine(x.Value(1), pitch);
        }

        /// <inheritdoc cref="docs._default" />
        public static Outlet StrikeNote(
            this OperatorFactory operatorFactory, Outlet sound, Outlet delay = null, Outlet volume = null)
        {
            if (operatorFactory == null) throw new ArgumentNullException(nameof(operatorFactory));
            var x = operatorFactory;

            // A little optimization, because so slow...
            bool delayFilledIn = delay != null && delay.AsConst() != 0;
            bool volumeFilledIn = volume != null && volume.AsConst() != 1;
            
            if (delayFilledIn) sound = x.TimeAdd(sound, delay);
            if (volumeFilledIn) sound = Multiply(x, sound, volume);

            return sound;
        }

        /// <inheritdoc cref="docs._vibrato" />
        public static Outlet VibratoOverPitch(
            this OperatorFactory operatorFactory, Outlet freq, (Outlet speed, Outlet depth) vibrato = default)
        {
            if (operatorFactory == null) throw new ArgumentNullException(nameof(operatorFactory));
            var x = operatorFactory;

            vibrato.speed = vibrato.speed ?? x.Value(5.5);
            vibrato.depth = vibrato.depth ?? x.Value(0.0005);

            return Multiply(x, freq, Add(x, x.Value(1), x.Sine(vibrato.depth, vibrato.speed)));
        }

        /// <inheritdoc cref="docs._tremolo" />
        public static Outlet Tremolo(
            this OperatorFactory operatorFactory, Outlet sound, (Outlet speed, Outlet depth) tremolo = default)
        {
            if (operatorFactory == null) throw new ArgumentNullException(nameof(operatorFactory));
            var x = operatorFactory;
            
            tremolo.speed = tremolo.speed ?? x.Value(8);
            tremolo.depth = tremolo.depth ?? x.Value(0.33);

            return Multiply(x, sound, Add(x, x.Sine(tremolo.depth, tremolo.speed), x.Value(1)));
        }

        // Panning

        /// <inheritdoc cref="docs._panning" />
        public static Outlet Panning(
            this OperatorFactory operatorFactory, Outlet sound, Outlet panning, ChannelEnum channel)
        {
            if (operatorFactory == null) throw new ArgumentNullException(nameof(operatorFactory));
            var x = operatorFactory;

            // Some optimization in case of a constant value
            {
                double? constPanning = panning.AsConst();
                if (constPanning != null)
                {
                    return x.Panning(sound, constPanning.Value, channel);
                }
            }

            switch (channel)
            {
                case ChannelEnum.Single: return sound;
                case ChannelEnum.Left:   return Multiply(x, sound, x.Substract(x.Value(1), panning));
                case ChannelEnum.Right:  return Multiply(x, sound, panning);

                default: throw new ValueNotSupportedException(channel);
            }
        }

        /// <inheritdoc cref="docs._panning" />
        private static Outlet Panning(
            this OperatorFactory operatorFactory, Outlet sound, double panning, ChannelEnum channel)
        {
            if (operatorFactory == null) throw new ArgumentNullException(nameof(operatorFactory));
            var x = operatorFactory;
            
            if (panning < 0) panning = 0;
            if (panning > 1) panning = 1;

            switch (channel)
            {
                case ChannelEnum.Single: return sound;
                case ChannelEnum.Left:   return Multiply(x, sound, x.Value(1 - panning));
                case ChannelEnum.Right:  return Multiply(x, sound, x.Value(panning));

                default: throw new ValueNotSupportedException(channel);
            }
        }

        // Panbrello
        
        /// <inheritdoc cref="docs._panbrello" />
        public static Outlet Panbrello(
            this OperatorFactory operatorFactory, 
            Outlet sound, (Outlet speed, Outlet depth) panbrello = default,
            ChannelEnum channel = ChannelEnum.Undefined)
        {
            if (operatorFactory == null) throw new ArgumentNullException(nameof(operatorFactory));
            var x = operatorFactory;
            
            panbrello.speed = panbrello.speed ?? x.Value(1);
            panbrello.depth = panbrello.depth ?? x.Value(1);

            // 0.5 is in the middle. 0 is left, 1 is right.
            var sine      = x.Sine(panbrello.depth, panbrello.speed); // [-1,+1]
            var halfSine  = Multiply(x, x.Value(0.5), sine); // [-0.5,+0.5]
            var zeroToOne = Add(x, x.Value(0.5), halfSine); // [0,1]

            return x.Panning(sound, zeroToOne, channel);
        }

        // PitchPan
        
        /// <inheritdoc cref="docs._pitchpan" />
        public static Outlet PitchPan(
            this OperatorFactory operatorFactory,
            Outlet actualFrequency, Outlet centerFrequency,
            Outlet referenceFrequency, Outlet referencePanning)
        {
            if (operatorFactory == null) throw new ArgumentNullException(nameof(operatorFactory));
            var x = operatorFactory;

            // Some optimization in case of constants, because things are currently so slow.
            {
                double? constActualFrequency    = actualFrequency?.AsConst();
                double? constCenterFrequency    = centerFrequency?.AsConst();
                double? constReferenceFrequency = referenceFrequency?.AsConst();
                double? constReferencePanning   = referencePanning?.AsConst();

                if (constActualFrequency != null &&
                    constCenterFrequency != null &&
                    constReferenceFrequency != null &&
                    constReferencePanning != null)
                {
                    double pitchPan =
                        PitchPan(operatorFactory,
                                 constActualFrequency.Value,    constCenterFrequency.Value,
                                 constReferenceFrequency.Value, constReferencePanning.Value);
                    return x.Value(pitchPan);
                }
            }
            
            // Defaults
            centerFrequency    = centerFrequency ?? x.Value(Notes.A4);
            referenceFrequency = referenceFrequency ?? x.Value(Notes.E4);
            referencePanning   = referencePanning ?? x.Value(0.6);
            
            var centerPanning = x.Value(0.5);

            // Calculate intervals relative to the center frequency
            var referenceInterval = x.Divide(referenceFrequency, centerFrequency);
            var actualInterval    = x.Divide(actualFrequency,    centerFrequency);

            var factor = Multiply(x, actualInterval, referenceInterval);

            // Calculate panning deviation
            //var newPanningDeviation = Multiply(x, x.Substract(referencePanning, centerPanning), factor);
            // AI's correction:
            var newPanningDeviation = Multiply(x, x.Substract(referencePanning, centerPanning), x.Substract(factor, x.Value(1)));
            var newPanning          = Add(x, centerPanning, newPanningDeviation);

            return newPanning;
        }

        // PitchPan
        
        /// <inheritdoc cref="docs._pitchpan" />
        public static double PitchPan(
            this OperatorFactory operatorFactory,
            double actualFrequency, double centerFrequency,
            double referenceFrequency, double referencePanning)
        {
            if (operatorFactory == null) throw new ArgumentNullException(nameof(operatorFactory));

            // Defaults
            if (centerFrequency == default) centerFrequency       = Notes.A4;
            if (referenceFrequency == default) referenceFrequency = Notes.E4;
            if (referencePanning == default) referencePanning     = 0.6;

            double centerPanning = 0.5;

            // Calculate intervals relative to the center frequency
            double referenceInterval = referenceFrequency / centerFrequency;
            double actualInterval    = actualFrequency / centerFrequency;

            double factor = actualInterval * referenceInterval;

            // Calculate panning deviation
            //double newPanningDeviation = (referencePanning - centerPanning) * factor;
            // AI's correction:
            double newPanningDeviation = (referencePanning - centerPanning) * (factor - 1);
            double newPanning          = centerPanning + newPanningDeviation;

            return newPanning;
        }
        
        // Echo

        public static Outlet Echo(
            this OperatorFactory x, Outlet signal,
            Outlet magnitude = null, Outlet delay = null, int count = 8)
            => EchoFeedBack(x, signal, magnitude, delay, count);
        
        public static Outlet EchoAdditive(
            this OperatorFactory x, Outlet signal,
            Outlet magnitude = null, Outlet delay = null, int count = 8)
        {
            Outlet cumulativeMagnitude = x.Value(1);
            Outlet cumulativeDelay     = x.Value();

            IList<Outlet> repeats = new List<Outlet>(count);

            for (int i = 0; i < count; i++)
            {
                Outlet divide  = Multiply(x, signal, cumulativeMagnitude);
                Outlet timeAdd = x.TimeAdd(divide, cumulativeDelay);
                
                repeats.Add(timeAdd);

                cumulativeMagnitude = Multiply(x, cumulativeMagnitude, magnitude);
                cumulativeDelay     = Add(x, cumulativeDelay, delay);
            }

            var adder = Sum(x, repeats);
            return adder;
        }

        /// <summary>
        /// Applies an echo effect using a feedback loop.
        /// The goal is to make it more efficient than an additive approach by reusing double echoes 
        /// to generate quadruple echoes, so 4 echoes take 2 iterations, and 8 echoes take 3 iterations.
        /// However, since values from the same formula are not yet reused within the final calculation,
        /// this optimization is currently ineffective. Future versions may improve on this.
        /// Keeping it in here just to have an optimization option for later.
        /// </summary>
        public static Outlet EchoFeedBack(
            this OperatorFactory x, Outlet signal,
            Outlet magnitude = null, Outlet delay = null, int count = 8)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (signal == null) throw new ArgumentNullException(nameof(signal));
            if (magnitude == null) magnitude = x.Value(0.66);
            if (delay == null) delay         = x.Value(0.25);

            Outlet cumulativeSignal    = signal;
            Outlet cumulativeMagnitude = magnitude;
            Outlet cumulativeDelay     = delay;

            int loopCount = Log(count, 2);

            for (int i = 0; i < loopCount; i++)
            {
                Outlet quieter = Multiply(x, cumulativeSignal, cumulativeMagnitude);
                Outlet shifted = x.TimeAdd(quieter, cumulativeDelay);

                cumulativeSignal = Add(x, cumulativeSignal, shifted);
                
                cumulativeMagnitude = Multiply(x, cumulativeMagnitude, cumulativeMagnitude);
                cumulativeDelay = Add(x, cumulativeDelay, cumulativeDelay);
            }

            return cumulativeSignal;
        }

        // Helpers

        /// <summary>
        /// Integer variation of the Math.Log function.
        /// It will only return integers,
        /// but will prevent rounding errors such as
        /// 1000 log 10 = 2.99999999996.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Log(int value, int n)
        {
            int temp = value;
            var i    = 0;

            while (temp >= n)
            {
                temp /= n;
                i++;
            }

            return i;
        }
    }
}