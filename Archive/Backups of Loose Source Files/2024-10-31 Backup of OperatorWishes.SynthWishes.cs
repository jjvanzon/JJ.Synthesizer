// Moved

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
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using static JJ.Business.Synthesizer.Wishes.CopiedFromFramework;
using static JJ.Business.Synthesizer.Wishes.docs;
using static JJ.Framework.Mathematics.Maths;

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
            _ = new CaptureIndexer(this);
        }
        
        // Start Chaining
        
        public FluentOutlet Fluent(Outlet outlet)
            => _[outlet];
        
        // Basic Operators

        /// <inheritdoc cref="_add"/>
        public FluentOutlet Add(params Outlet[] operands) 
            => Add((IList<Outlet>)operands);

        /// <inheritdoc cref="_add"/>
        public FluentOutlet Add(IList<Outlet> operands)
        {
            if (operands == null) throw new ArgumentNullException(nameof(operands));
            
            // Flatten Nested Sums
            IList<Outlet> terms = FlattenTerms(operands);
            
            // Consts
            IList<Outlet> vars = terms.Where(y => y.IsVar()).ToArray();
            double constant = terms.Sum(y => y.AsConst() ?? 0);
            
            if (constant != 0)  // Skip Identity 0
            {
                terms = vars.Concat(new [] { (Outlet)_[constant] }).ToArray();
            }

            switch (terms.Count)
            {
                case 0:
                    return _[_[0]];
                
                case 1:
                    // Return single term
                    return _[terms[0]];
                
                case 2:
                    // Simple Add for 2 Operands
                    return _[_operatorFactory.Add(terms[0], terms[1])].WithName(FetchName());
                
                default:
                    // Make Normal Adder
                    return _[_operatorFactory.Adder(terms)].WithName(FetchName());
            }
        }

        /// <inheritdoc cref="_add"/>
        public FluentOutlet Add(double a, double b) => Add(_[a], _[b]);

        /// <inheritdoc cref="_add"/>
        public FluentOutlet Add(Outlet a, double b) => Add(a, _[b]);

        /// <inheritdoc cref="_add"/>
        public FluentOutlet Add(double a, Outlet b) => Add(_[a], b);

        /// <inheritdoc cref="_flattentermswithsumoradd"/>
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

        // Overloads

        public FluentOutlet Subtract(Outlet a, Outlet b) => _[_operatorFactory.Substract(a, b)].WithName(FetchName());

        public FluentOutlet Subtract(Outlet a, double b) => _[_operatorFactory.Substract(a, _[b])].WithName(FetchName());

        public FluentOutlet Subtract(double a, Outlet b) => _[_operatorFactory.Substract(_[a], b)].WithName(FetchName());

        /// <inheritdoc cref="_multiply"/>
        public FluentOutlet Multiply(Outlet a, Outlet b)
        {
            // Reverse operands increasing likelihood to have a 0-valued (volume) curve first.
            (a, b) = (b, a);

            // Flatten Nested Sums
            IList<Outlet> flattenedFactors = FlattenFactors(a, b);
            
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
                    return _[_[1]].WithName(FetchName());
                
                case 1:
                    // Return single number
                    return _[factors[0]];
                
                case 2:
                    // Simple Multiply for 2 Operands
                    return _[_operatorFactory.Multiply(factors[0], factors[1])].WithName(FetchName());
                
                default:
                    // Re-nest remaining factors
                    return _[NestMultiplications(factors)].WithName(FetchName());
            }
        }

        /// <inheritdoc cref="_multiply"/>
        public FluentOutlet Multiply(Outlet a, double b) => Multiply(a, _[b]);

        /// <inheritdoc cref="_multiply"/>
        public FluentOutlet Multiply(double a, Outlet b) => Multiply(_[a], b);

        /// <inheritdoc cref="_flattenfactorswithmultiplyoutlet"/>
        [UsedImplicitly]
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
        
        public FluentOutlet Divide(Outlet a, Outlet b) => _[_operatorFactory.Divide(a, b)].WithName(FetchName());

        public FluentOutlet Divide(Outlet a, double b) => Divide(a, _[b]);
        
        public FluentOutlet Divide(double a, Outlet b) => Divide(_[a], b);

        public FluentOutlet Power(Outlet @base, Outlet exponent) => _[_operatorFactory.Power(@base, exponent)].WithName(FetchName());
        
        public FluentOutlet Power(Outlet @base, double exponent) => Power(@base, _[exponent]);

        public FluentOutlet Power(double @base, Outlet exponent) => Power(_[@base], exponent);

        /// <inheritdoc cref="_sine" />
        public FluentOutlet Sine(Outlet pitch = null) => _[_operatorFactory.Sine(_[1], pitch ?? _[1])].WithName(FetchName());

        /// <inheritdoc cref="_sine" />
        public FluentOutlet Sine(double pitch) => Sine(_[pitch]);
        
        public FluentOutlet Delay(Outlet signal, Outlet delay) => _[_operatorFactory.TimeAdd(signal, delay ?? _[0])].WithName(FetchName());

        public FluentOutlet Delay(Outlet signal, double delay) => Delay(signal, _[delay]);
        
        public FluentOutlet Skip(Outlet signal, Outlet skip) => _[_operatorFactory.TimeSubstract(signal, skip ?? _[1])].WithName(FetchName());

        public FluentOutlet Skip(Outlet signal, double skip) => Skip(signal, _[skip]);
        
        public FluentOutlet Stretch(Outlet signal, Outlet timeScale) => _[_operatorFactory.TimeMultiply(signal, timeScale ?? _[1])].WithName(FetchName());

        public FluentOutlet Stretch(Outlet signal, double timeScale) => Stretch(signal, _[timeScale]);

        public FluentOutlet SpeedUp(Outlet signal, Outlet factor) => _[_operatorFactory.TimeDivide(signal, factor)].WithName(FetchName());

        public FluentOutlet SpeedUp(Outlet signal, double factor) => SpeedUp(signal, _[factor]);

        public FluentOutlet TimePower(Outlet signal, Outlet exponent) => _[_operatorFactory.TimePower(signal, exponent)].WithName(FetchName());

        public FluentOutlet TimePower(Outlet signal, double exponent) => TimePower(signal, _[exponent]);
        
        // Derived Operators

        /// <inheritdoc cref="_default" />
        public FluentOutlet StrikeNote(Outlet sound, Outlet delay = default, Outlet volume = default)
        {
            // A little optimization, because so slow...
            bool delayFilledIn = delay != null && delay.AsConst() != 0;
            bool volumeFilledIn = volume != null && volume.AsConst() != 1;
            
            if (delayFilledIn) sound = Delay(sound, delay);
            if (volumeFilledIn) sound = Multiply(sound, volume);

            return _[sound];
        }

        /// <inheritdoc cref="_default" />
        public FluentOutlet StrikeNote(Outlet sound, Outlet delay, double volume)
            =>  StrikeNote(sound, delay, _[volume]);

        /// <inheritdoc cref="_default" />
        public FluentOutlet StrikeNote(Outlet sound, double delay, Outlet volume = default)
            =>  StrikeNote(sound, _[delay], volume);
                
        /// <inheritdoc cref="_default" />
        public FluentOutlet StrikeNote(Outlet sound, double delay, double volume)
            =>  StrikeNote(sound, _[delay], _[volume]);
        
        /// <inheritdoc cref="_vibrato" />
        public FluentOutlet VibratoOverPitch(Outlet freq, (Outlet speed, Outlet depth) vibrato = default)
        {
            vibrato.speed = vibrato.speed ?? _[5.5];
            vibrato.depth = vibrato.depth ?? _[0.0005];

            return Multiply(freq, Add(_[1], Multiply(Sine(vibrato.speed), vibrato.depth)));
        }
        
        /// <inheritdoc cref="_vibrato" />
        public FluentOutlet VibratoOverPitch(Outlet freq, (Outlet speed, double depth) vibrato)
        {
            (Outlet speed, Outlet depth) vibrato2 = default;
            if (vibrato.speed != default) vibrato2.speed = vibrato.speed;
            if (vibrato.depth != default) vibrato2.depth = _[vibrato.depth];
            return VibratoOverPitch(freq, vibrato2);
        }

        /// <inheritdoc cref="_vibrato" />
        public FluentOutlet VibratoOverPitch(Outlet freq, (double speed, Outlet depth) vibrato)
        {
            (Outlet speed, Outlet depth) vibrato2 = default;
            if (vibrato.speed != default) vibrato2.speed = _[vibrato.speed];
            if (vibrato.depth != default) vibrato2.depth = vibrato.depth;
            return VibratoOverPitch(freq, vibrato2);
        }

        /// <inheritdoc cref="_vibrato" />
        public FluentOutlet VibratoOverPitch(Outlet freq, (double speed, double depth) vibrato)
        {
            (Outlet speed, Outlet depth) vibrato2 = default;
            if (vibrato.speed != default) vibrato2.speed = _[vibrato.speed];
            if (vibrato.depth != default) vibrato2.depth = _[vibrato.depth];
            return VibratoOverPitch(freq, vibrato2);
        }

        /// <inheritdoc cref="_tremolo" />
        public FluentOutlet Tremolo(Outlet sound, (Outlet speed, Outlet depth) tremolo = default)
        {
            tremolo.speed = tremolo.speed ?? _[8];
            tremolo.depth = tremolo.depth ?? _[0.33];

            return Multiply(sound, Add(Multiply(Sine(tremolo.speed), tremolo.depth), _[1]));
        }

        /// <inheritdoc cref="_tremolo" />
        public FluentOutlet Tremolo(Outlet sound, (Outlet speed, double depth) tremolo)
        {
            (Outlet speed, Outlet depth) tremolo2 = default;
            if (tremolo.speed != default) tremolo2.speed = tremolo.speed;
            if (tremolo.depth != default) tremolo2.depth = _[tremolo.depth];
            return VibratoOverPitch(sound, tremolo2);
        }

        /// <inheritdoc cref="_tremolo" />
        public FluentOutlet Tremolo(Outlet sound, (double speed, Outlet depth) tremolo)
        {
            (Outlet speed, Outlet depth) tremolo2 = default;
            if (tremolo.speed != default) tremolo2.speed = _[tremolo.speed];
            if (tremolo.depth != default) tremolo2.depth = tremolo.depth;
            return VibratoOverPitch(sound, tremolo2);
        }
        
        /// <inheritdoc cref="_tremolo" />
        public FluentOutlet Tremolo(Outlet sound, (double speed, double depth) tremolo)
        {
            (Outlet speed, Outlet depth) tremolo2 = default;
            if (tremolo.speed != default) tremolo2.speed = _[tremolo.speed];
            if (tremolo.depth != default) tremolo2.depth = _[tremolo.depth];
            return VibratoOverPitch(sound, tremolo2);
        }

        /// <inheritdoc cref="_panning" />
        public FluentOutlet Panning(Outlet sound, Outlet panning, ChannelEnum channel = default)
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
                case ChannelEnum.Single: return _[sound];
                case ChannelEnum.Left:   return Multiply(sound, Subtract(_[1], panning));
                case ChannelEnum.Right:  return Multiply(sound, panning);

                default: throw new ValueNotSupportedException(channel);
            }
        }

        /// <inheritdoc cref="_panning" />
        public FluentOutlet Panning(Outlet sound, double panning, ChannelEnum channel = default)
        {
            if (channel == default) channel = Channel;

            if (panning < 0) panning = 0;
            if (panning > 1) panning = 1;

            switch (channel)
            {
                case ChannelEnum.Single: return _[sound];
                case ChannelEnum.Left:   return Multiply(sound, _[1 - panning]);
                case ChannelEnum.Right:  return Multiply(sound, _[panning]);

                default: throw new ValueNotSupportedException(channel);
            }
        }
        
        // Panbrello
        
        /// <inheritdoc cref="_panbrello" />
        public FluentOutlet Panbrello(
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

        /// <inheritdoc cref="_panbrello" />
        public FluentOutlet Panbrello(
            Outlet sound, (Outlet speed, double depth) panbrello, ChannelEnum channel = default)
        {
            (Outlet speed, Outlet depth) panbrello2 = default;
            if (panbrello.speed != default) panbrello2.speed = panbrello.speed;
            if (panbrello.depth != default) panbrello2.depth = _[panbrello.depth];
            return Panbrello(sound, panbrello2, channel);
        }

        /// <inheritdoc cref="_panbrello" />
        public FluentOutlet Panbrello(
            Outlet sound, (double speed, Outlet depth) panbrello, ChannelEnum channel = default)
        {
            (Outlet speed, Outlet depth) panbrello2 = default;
            if (panbrello.speed != default) panbrello2.speed = _[panbrello.speed];
            if (panbrello.depth != default) panbrello2.depth = panbrello.depth;
            return Panbrello(sound, panbrello2, channel);
        }

        /// <inheritdoc cref="_panbrello" />
        public FluentOutlet Panbrello(
            Outlet sound, (double speed, double depth) panbrello, ChannelEnum channel = default)
        {
            (Outlet speed, Outlet depth) panbrello2 = default;
            if (panbrello.speed != default) panbrello2.speed = _[panbrello.speed];
            if (panbrello.depth != default) panbrello2.depth = _[panbrello.depth];
            return Panbrello(sound, panbrello2, channel);
        }

        // PitchPan

        /// <inheritdoc cref="_pitchpan" />
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
        
        /// <inheritdoc cref="_pitchpan" />
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

        /// <inheritdoc cref="_pitchpan" />
        public Outlet PitchPan(
            Outlet actualFrequency, double centerFrequency,
            double referenceFrequency, double referencePanning)
            => PitchPan(actualFrequency, _[centerFrequency], _[referenceFrequency], _[referencePanning]);
        
        public FluentOutlet Echo(Outlet signal, int count = 8, Outlet magnitude = default, Outlet delay = default)
            => EchoAdditive(signal, count, magnitude, delay);

        public FluentOutlet Echo(Outlet signal, int count, Outlet magnitude, double delay)
            => Echo(signal, count, magnitude, _[delay]);

        public FluentOutlet Echo(Outlet signal, int count, double magnitude, Outlet delay = default)
            => Echo(signal, count, _[magnitude], delay);
        
        public FluentOutlet Echo(Outlet signal, int count, double magnitude, double delay)
            => Echo(signal, count, _[magnitude], _[delay]);

        public FluentOutlet EchoParallel(
            Outlet signal, int count = 8, Outlet magnitude = default, Outlet delay = default, 
            bool mustAddAudioLength = true, [CallerMemberName] string callerMemberName = null)
        {
            // Fetch (user-chosen) name before anything else does.
            string name = FetchName(callerMemberName);
            
            if (magnitude == null) magnitude = _[0.66];
            if (delay == null) delay = _[0.25];
            
            var cumulativeMagnitude = _[1];
            var cumulativeDelay     = _[0];

            var echoTasks = new Func<Outlet>[count];

            for (int i = 0; i < count; i++)
            {
                var quieter = signal * cumulativeMagnitude;
                var shifted = Delay(quieter, cumulativeDelay);

                echoTasks[i] = () => shifted;

                cumulativeMagnitude *= magnitude;
                cumulativeDelay += delay;
            }

            // BUG: If you nest ParallelAdds, every time you pass this method, it adds more audio length.
            if (mustAddAudioLength)
            {
                AddAudioLength(cumulativeDelay - delay);
            }

            //WithName(name, callerMemberName, MemberName());
            WithName(name);
            
            return ParallelAdd(echoTasks);
        }

        public FluentOutlet EchoAdditive(
            Outlet signal, int count = 8, Outlet magnitude = default, Outlet delay = default)
        {
            if (magnitude == null) magnitude = _[0.66];
            if (delay == null) delay         = _[0.25];

            var cumulativeMagnitude = _[1];
            var cumulativeDelay     = _[0];

            IList<Outlet> repeats = new List<Outlet>(count);

            for (int i = 0; i < count; i++)
            {
                var divide  = signal * cumulativeMagnitude;
                var timeAdd = Delay(divide, cumulativeDelay);
                
                repeats.Add(timeAdd);

                cumulativeMagnitude *= magnitude;
                cumulativeDelay += delay;
            }

            // Add some audio length
            AddAudioLength(cumulativeDelay - delay);

            return Add(repeats);
        }

        public FluentOutlet EchoAdditive(Outlet signal, int count, Outlet magnitude, double delay)
            => EchoAdditive(signal, count, magnitude, _[delay]);

        public FluentOutlet EchoAdditive(Outlet signal, int count, double magnitude, Outlet delay = default)
            => EchoAdditive(signal, count, _[magnitude], delay);
        
        public FluentOutlet EchoAdditive(Outlet signal, int count, double magnitude, double delay)
            => EchoAdditive(signal, count, _[magnitude], _[delay]);
        
        /// <inheritdoc cref="_echofeedback"/>
        public FluentOutlet EchoFeedBack(
            Outlet signal, int count = 8, Outlet magnitude = default, Outlet delay = default)
        {
            if (signal == null) throw new ArgumentNullException(nameof(signal));
            if (magnitude == null) magnitude = _[0.66];
            if (delay == null) delay         = _[0.25];

            var cumulativeSignal    = _[signal];
            var cumulativeMagnitude = _[magnitude];
            var cumulativeDelay     = _[delay];

            int loopCount = Log(count, 2);

            for (int i = 0; i < loopCount; i++)
            {
                var quieter = cumulativeSignal * cumulativeMagnitude;
                var shifted = Delay(quieter, cumulativeDelay);

                cumulativeSignal += shifted;
                
                cumulativeMagnitude *= cumulativeMagnitude;
                cumulativeDelay += cumulativeDelay;
            }

            // Add some audio length
            AddAudioLength(cumulativeDelay - delay);

            return _[cumulativeSignal];
        }

        /// <inheritdoc cref="_echofeedback"/>
        public FluentOutlet EchoFeedBack(Outlet signal, int count, Outlet magnitude, double delay)
            => EchoFeedBack(signal, count, magnitude, _[delay]);

        /// <inheritdoc cref="_echofeedback"/>
        public FluentOutlet EchoFeedBack(Outlet signal, int count, double magnitude, Outlet delay = default)
            => EchoFeedBack(signal, count, _[magnitude], delay);
        
        /// <inheritdoc cref="_echofeedback"/>
        public FluentOutlet EchoFeedBack(Outlet signal, int count, double magnitude, double delay)
            => EchoFeedBack(signal, count, _[magnitude], _[delay]);
   }
}
