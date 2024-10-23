using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using JJ.Framework.Persistence;
using System.Linq;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;

// ReSharper disable InvokeAsExtensionMethod
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable AssignmentInsteadOfDiscard

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        private OperatorFactory _operatorFactory;

        private void InitializeOperatorWishes(IContext context)
        {
            _operatorFactory = ServiceFactory.CreateOperatorFactory(context);
            _ = new ValueIndexer(_operatorFactory);
        }

        public ChannelEnum Channel { get; set; }

        // Basic Operators

        // TODO: Delegate everything to OperatorExtensionsWishes and add deprecated ones there too.
        
        [Obsolete("Use _[123] instead.")]
        public ValueOperatorWrapper Value(double value = 0) => _[value];

        /// <inheritdoc cref="docs._add"/>
        public Outlet Add(params Outlet[] operands) 
            => Add((IList<Outlet>)operands);

        /// <inheritdoc cref="docs._add"/>
        public Outlet Add(IList<Outlet> operands)
        {
            if (operands == null) throw new ArgumentNullException(nameof(operands));
            
            // Flatten Nested Sums
            IList<Outlet> flattenedTerms = FlattenTerms(operands);
            
            // Consts
            IList<Outlet> vars = flattenedTerms.Where(y => y.IsVar()).ToArray();
            double constant = flattenedTerms.Sum(y => y.AsConst() ?? 0);
            
            if (constant != 0)  // Skip Identity 0
            {
                Outlet constOutlet = _[constant];
                flattenedTerms = vars.Concat(new [] { constOutlet }).ToArray();
            }

            switch (flattenedTerms.Count)
            {
                case 0:
                    return _[0];
                
                case 1:
                    // Return single term
                    return flattenedTerms[0];
                
                case 2:
                    // Simple Add for 2 Operands
                    return _operatorFactory.Add(flattenedTerms[0], flattenedTerms[1]);
                
                default:
                    // Make Normal Adder
                    return _operatorFactory.Adder(flattenedTerms);
            }
        }

        /// <summary> Alternative entry point (Operator) Outlet. </summary>
        [UsedImplicitly]
        private IList<Outlet> FlattenTerms(Outlet sumOrAdd)
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

        private IList<Outlet> FlattenTerms(params Outlet[] operands)
            => FlattenTerms((IList<Outlet>)operands);

        private IList<Outlet> FlattenTerms(IList<Outlet> operands)
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

        [Obsolete("Use Add instead.", true)]
        public Adder Adder(params Outlet[] operands) => throw new NotSupportedException();

        [Obsolete("Use Add instead.", true)]
        public Adder Adder(IList<Outlet> operands) => throw new NotSupportedException();

        public Substract Subtract(Outlet operandA = null, Outlet operandB = null)
            => _operatorFactory.Substract(operandA, operandB);

        [Obsolete("Typo. Use Subtract instead.", true)]
        public Substract Substract(Outlet operandA = null, Outlet operandB = null) => throw new NotSupportedException();
        
        /// <inheritdoc cref="docs._multiply"/>
        public Outlet Multiply(Outlet operandA, Outlet operandB)
        {
            //if (origin != null)
            //{
            //    return _operatorFactory.Multiply(operandA, operandB, origin);
            //}

            // Reverse operands increasing likelihood to have a 0-valued (volume) curve first.
            (operandA, operandB) = (operandB, operandA);

            // Flatten Nested Sums
            IList<Outlet> flattenedFactors = FlattenFactors(operandA, operandB);
            
            // Consts
            IList<Outlet> vars = flattenedFactors.Where(y => y.IsVar()).ToArray();
            double constant = flattenedFactors.Product(y => y.AsConst() ?? 1);

            IList<Outlet> factors = new List<Outlet>(vars);
            if (constant != 1)  // Skip Identity 1
            {
                factors.Add(_[constant]);
            }

            switch (factors.Count)
            {
                case 0:
                    // Return identity 1
                    return _[1];
                
                case 1:
                    // Return single number
                    return factors[0];
                
                case 2:
                    // Simple Multiply for 2 Operands
                    return _operatorFactory.Multiply(factors[0], factors[1]);
                
                default:
                    // Re-nest remaining factors
                    return NestMultiplications(factors);
            }
        }

        /// <summary> Alternative entry point (Operator) Outlet. </summary>
        public IList<Outlet> FlattenFactors(Outlet multiplyOutlet)
        {
            if (multiplyOutlet == null) throw new ArgumentNullException(nameof(multiplyOutlet));

            if (!multiplyOutlet.IsMultiply())
            {
                throw new Exception($"{nameof(multiplyOutlet)} parameter is not a Multiply operator.");
            }

            var multiplyWrapper = new Multiply(multiplyOutlet.Operator);
            return FlattenFactors(multiplyWrapper.OperandA, multiplyWrapper.OperandB);
        }

        public IList<Outlet> FlattenFactors(params Outlet[] operands)
            => FlattenFactors((IList<Outlet>)operands);

        public IList<Outlet> FlattenFactors(IList<Outlet> operands)
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
        
        private Outlet NestMultiplications(IList<Outlet> flattenedFactors)
        {
            // Base case: If there's only one factor, return it
            // Also stops the recursion
            if (flattenedFactors.Count == 1)
            {
                return flattenedFactors[0];
            }

            // Recursive case: Nest the first factor with the result of nesting the rest
            var firstFactor = flattenedFactors[0];
            var remainingFactors = flattenedFactors.Skip(1).ToList();

            // Recursively nest the remaining factors and multiply with the first
            return _operatorFactory.Multiply(firstFactor, NestMultiplications(remainingFactors));
        }

        [Obsolete("Origin parameter obsolete.", true)]
        public Outlet Multiply(Outlet operandA, Outlet operandB, Outlet origin) => throw new NotSupportedException();
        
        public Divide Divide(Outlet numerator, Outlet denominator)
            => _operatorFactory.Divide(numerator, denominator);

        [Obsolete("Origin parameter obsolete.", true)]
        public Divide Divide(Outlet numerator, Outlet denominator, Outlet origin) => throw new NotSupportedException();

        public Power Power(Outlet @base = null, Outlet exponent = null)
            => _operatorFactory.Power(@base, exponent);
        
        /// <inheritdoc cref="docs._sine" />
        public Outlet Sine(Outlet pitch = null)
        {
            pitch = pitch ?? _[1];
            
            return _operatorFactory.Sine(_[1], pitch);
        }

        [Obsolete("Use Multiply(Sine(pitch), volume) instead of Sine(volume, pitch).", true)]
        public Sine Sine(Outlet volume, Outlet pitch) => throw new NotSupportedException();

        [Obsolete("Use Add(Multiply(Sine(pitch), volume), level) instead of Sine(volume, pitch, level).", true)]
        public Sine Sine(Outlet volume, Outlet pitch, Outlet level) => throw new NotSupportedException();

        [Obsolete("Use Delay(Add(Multiply(Sine(pitch), volume), level), phaseStart) instead of Sine(volume, pitch, level, phaseStart).", true)]
        public Sine Sine(Outlet volume, Outlet pitch, Outlet level, Outlet phaseStart) => throw new NotSupportedException();

        public Outlet Delay(Outlet signal, Outlet timeDifference)
        {
            return _operatorFactory.TimeAdd(signal, timeDifference);
        }

        [Obsolete("Use Delay instead.", true)]
        public TimeAdd TimeAdd(Outlet signal = null, Outlet timeDifference = null) => throw new NotSupportedException();

        /// <inheritdoc cref="docs._default" />
        public Outlet Stretch(Outlet signal, Outlet timeFactor)
        {
            return _operatorFactory.TimeMultiply(signal, timeFactor ?? _[1]);
        }

        [Obsolete("Origin parameter obsolete.", true)]
        public Outlet Stretch(Outlet signal, Outlet timeFactor, Outlet origin) => throw new NotSupportedException();
        
        [Obsolete("Use Stretch instead.", true)]
        public TimeMultiply TimeMultiply(Outlet signal = null, Outlet timeMultiplier = null, Outlet origin = null) => throw new NotSupportedException();

        public TimeDivide Squash(Outlet signal = null, Outlet timeDivider = null)
            => _operatorFactory.TimeDivide(signal, timeDivider);

        [Obsolete("Origin parameter obsolete.", true)]
        public TimeDivide Squash(Outlet signal, Outlet timeDivider, Outlet origin) => throw new NotSupportedException();

        [Obsolete("Use Squash instead.", true)]
        public TimeDivide TimeDivide(Outlet signal = null, Outlet timeDivider = null, Outlet origin = null) => throw new NotSupportedException();

        public TimeSubstract TimeSubtract(Outlet signal = null, Outlet timeDifference = null)
            => _operatorFactory.TimeSubstract(signal, timeDifference);

        [Obsolete("Typo. Use TimeSubtract instead.", true)]
        public TimeSubstract TimeSubstract(Outlet signal = null, Outlet timeDifference = null) => throw new NotSupportedException();

        public TimePower TimePower(Outlet signal = null, Outlet exponent = null)
            => _operatorFactory.TimePower(signal, exponent);

        [Obsolete("Origin parameter obsolete.", true)]
        public TimePower TimePower(Outlet signal, Outlet exponent, Outlet origin) => throw new NotSupportedException();

        // Derived Operators

        /// <inheritdoc cref="docs._default" />
        public Outlet StrikeNote(Outlet sound, Outlet delay = null, Outlet volume = null)
        {
            // A little optimization, because so slow...
            bool delayFilledIn = delay != null && delay.AsConst() != 0;
            bool volumeFilledIn = volume != null && volume.AsConst() != 1;
            
            if (delayFilledIn) sound = Delay(sound, delay);
            if (volumeFilledIn) sound = Multiply(sound, volume);

            return sound;
        }

        /// <inheritdoc cref="docs._vibrato" />
        public Outlet VibratoOverPitch(Outlet freq, (Outlet speed, Outlet depth) vibrato = default)
        {
            vibrato.speed = vibrato.speed ?? _[5.5];
            vibrato.depth = vibrato.depth ?? _[0.0005];

            return Multiply(freq, Add(_[1], Multiply(Sine(vibrato.speed), vibrato.depth)));
        }

        /// <inheritdoc cref="docs._tremolo" />
        public Outlet Tremolo(Outlet sound, (Outlet speed, Outlet depth) tremolo = default)
        {
            tremolo.speed = tremolo.speed ?? _[8];
            tremolo.depth = tremolo.depth ?? _[0.33];

            return Multiply(sound, Add(Multiply(Sine(tremolo.speed), tremolo.depth), _[1]));
        }

        /// <inheritdoc cref="docs._panning" />
        public Outlet Panning(Outlet sound, Outlet panning, ChannelEnum channel = default)
        {
            if (channel == default) channel = Channel;

            // Some optimization in case of a constant value
            {
                double? constPanning = panning.AsConst();
                if (constPanning != null)
                {
                    return Panning(sound, constPanning.Value, channel);
                }
            }

            switch (channel)
            {
                case ChannelEnum.Single: return sound;
                case ChannelEnum.Left:   return Multiply(sound, Subtract(_[1], panning));
                case ChannelEnum.Right:  return Multiply(sound, panning);

                default: throw new ValueNotSupportedException(channel);
            }
        }

        /// <inheritdoc cref="docs._panning" />
        private Outlet Panning(Outlet sound, double panning, ChannelEnum channel = default)
        {
            if (channel == default) channel = Channel;

            if (panning < 0) panning = 0;
            if (panning > 1) panning = 1;

            switch (channel)
            {
                case ChannelEnum.Single: return sound;
                case ChannelEnum.Left:   return Multiply(sound, _[1 - panning]);
                case ChannelEnum.Right:  return Multiply(sound, _[panning]);

                default: throw new ValueNotSupportedException(channel);
            }
        }
        
        // Panbrello
        
        /// <inheritdoc cref="docs._panbrello" />
        public Outlet Panbrello(
            Outlet sound, (Outlet speed, Outlet depth) panbrello = default, ChannelEnum channel = default)
        {
            if (channel == default) channel = Channel;

            panbrello.speed = panbrello.speed ?? _[1];
            panbrello.depth = panbrello.depth ?? _[1];

            // 0.5 is in the middle. 0 is left, 1 is right.
            var sine      = Multiply(Sine(panbrello.speed), panbrello.depth); // [-1,+1]
            var halfSine  = Multiply(_[0.5], sine); // [-0.5,+0.5]
            var zeroToOne = Add(_[0.5], halfSine); // [0,1]

            return Panning(sound, zeroToOne, channel);
        }

        // PitchPan

        /// <inheritdoc cref="docs._pitchpan" />
        public Outlet PitchPan(
            Outlet actualFrequency, Outlet centerFrequency,
            Outlet referenceFrequency, Outlet referencePanning)
        {
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
                    double pitchPan = PitchPan(constActualFrequency.Value, constCenterFrequency.Value,
                                               constReferenceFrequency.Value, constReferencePanning.Value);
                    return _[pitchPan];
                }
            }
            
            // Defaults
            centerFrequency    = centerFrequency ?? A4;
            referenceFrequency = referenceFrequency ?? E4;
            referencePanning   = referencePanning ?? _[0.6];
            
            var centerPanning = _[0.5];

            // Calculate intervals relative to the center frequency
            var referenceInterval = Divide(referenceFrequency, centerFrequency);
            var actualInterval    = Divide(actualFrequency,    centerFrequency);

            var factor = Multiply(actualInterval, referenceInterval);

            // Calculate panning deviation
            //var newPanningDeviation = Multiply(x, x.Substract(referencePanning, centerPanning), factor);
            // AI's correction:
            var newPanningDeviation = Multiply(Subtract(referencePanning, centerPanning), Subtract(factor, _[1]));
            var newPanning          = Add(centerPanning, newPanningDeviation);

            return newPanning;
        }
        
        /// <inheritdoc cref="docs._pitchpan" />
        public double PitchPan(
            double actualFrequency, double centerFrequency,
            double referenceFrequency, double referencePanning)
        {
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

        public Outlet Echo(Outlet signal, Outlet magnitude = null, Outlet delay = null, int count = 8)
            => EchoFeedBack(signal, magnitude, delay, count);
        
        public Outlet EchoAdditive(
            Outlet signal,
            Outlet magnitude = null, Outlet delay = null, int count = 8)
        {
            Outlet cumulativeMagnitude = _[1];
            Outlet cumulativeDelay     = _[0];

            IList<Outlet> repeats = new List<Outlet>(count);

            for (int i = 0; i < count; i++)
            {
                Outlet divide  = Multiply(signal, cumulativeMagnitude);
                Outlet timeAdd = Delay(divide, cumulativeDelay);
                
                repeats.Add(timeAdd);

                cumulativeMagnitude = Multiply(cumulativeMagnitude, magnitude);
                cumulativeDelay     = Add(cumulativeDelay, delay);
            }

            var adder = Add(repeats);
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
        public Outlet EchoFeedBack(
            Outlet signal,
            Outlet magnitude = null, Outlet delay = null, int count = 8)
        {
            if (signal == null) throw new ArgumentNullException(nameof(signal));
            if (magnitude == null) magnitude = _[0.66];
            if (delay == null) delay         = _[0.25];

            Outlet cumulativeSignal    = signal;
            Outlet cumulativeMagnitude = magnitude;
            Outlet cumulativeDelay     = delay;

            int loopCount = Log(count, 2);

            for (int i = 0; i < loopCount; i++)
            {
                Outlet quieter = Multiply(cumulativeSignal, cumulativeMagnitude);
                Outlet shifted = Delay(quieter, cumulativeDelay);

                cumulativeSignal = Add(cumulativeSignal, shifted);
                
                cumulativeMagnitude = Multiply(cumulativeMagnitude, cumulativeMagnitude);
                cumulativeDelay = Add(cumulativeDelay, cumulativeDelay);
            }

            return cumulativeSignal;
        }

        // ValueIndexer

        /// <inheritdoc cref="docs._valueindexer" />
        public ValueIndexer _;

        /// <inheritdoc cref="docs._valueindexer" />
        public class ValueIndexer
        {
            private readonly OperatorFactory _parent;

            /// <inheritdoc cref="docs._valueindexer" />
            internal ValueIndexer(OperatorFactory parent)
            {
                _parent = parent;
            }

            /// <inheritdoc cref="docs._valueindexer" />
            public ValueOperatorWrapper this[double value] => _parent.Value(value);
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

        public double Calculate(Outlet outlet, double time)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            return outlet.Calculate(time, ChannelIndex);
        }

        public int ChannelIndex
        {
            get
            {
                switch (Channel)
                {
                    case ChannelEnum.Single: return 0;
                    case ChannelEnum.Left:   return 0;
                    case ChannelEnum.Right:  return 1;
                    
                    default: throw new InvalidValueException(Channel);
                }
            }
        }   
    }
}