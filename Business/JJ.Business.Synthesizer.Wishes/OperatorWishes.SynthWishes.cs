using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using JJ.Framework.Persistence;

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

        /// <inheritdoc cref="docs._add"/>
        public Outlet Add(params Outlet[] operands) 
            => OperatorExtensionsWishes.Add(_operatorFactory, operands);
        
        /// <inheritdoc cref="docs._add"/>
        public Outlet Add(IList<Outlet> operands) 
            => OperatorExtensionsWishes.Add(_operatorFactory, operands);

        [Obsolete("Use " + nameof(Add) + " instead. Or " + nameof(OperatorFactory) + "." + nameof(Adder) + "() in exceptional cases.", true)]
        public Adder Adder(params Outlet[] operands)
            => throw new NotSupportedException();

        [Obsolete("Use " + nameof(Add) + " instead. Or " + nameof(OperatorFactory) + "." + nameof(Adder) + "() in exceptional cases.", true)]
        public Adder Adder(IList<Outlet> operands)
            => throw new NotSupportedException();

        public Divide Divide(Outlet numerator, Outlet denominator, Outlet origin = null)
            => _operatorFactory.Divide(numerator, denominator, origin);

        /// <inheritdoc cref="docs._multiply"/>
        public Outlet Multiply(Outlet operandA, Outlet operandB, Outlet origin = null)
            => OperatorExtensionsWishes.Multiply(_operatorFactory, operandA, operandB, origin);

        public Power Power(Outlet @base = null, Outlet exponent = null)
            => _operatorFactory.Power(@base, exponent);

        /// <inheritdoc cref="docs._sine" />
        public Outlet Sine(Outlet pitch = null) 
            => OperatorExtensionsWishes.Sine(_operatorFactory, pitch);

        // TODO: Add obsolete ones to OperatorExtensionsWishes too.
        [Obsolete("Use Multiply(Sine(pitch), volume) instead of " +
                  "Sine(volume, pitch).", true)]
        public Sine Sine(Outlet volume, Outlet pitch) 
            => throw new NotImplementedException();

        [Obsolete("Use Add(Multiply(Sine(pitch), volume), level) instead of " +
                  "Sine(volume, pitch, level).", true)]
        public Sine Sine(Outlet volume, Outlet pitch, Outlet level)
            => throw new NotImplementedException();

        [Obsolete("Use Delay(Add(Multiply(Sine(pitch), volume), level), phaseStart) instead of " +
                  "Sine(volume, pitch, level, phaseStart).", true)]
        public Sine Sine(Outlet volume, Outlet pitch, Outlet level, Outlet phaseStart)
            => throw new NotImplementedException();

        // TODO: Delegate to OperatorExtensionWishes and create and obsolete synonym.
        public Substract Subtract(Outlet operandA = null, Outlet operandB = null)
            => _operatorFactory.Substract(operandA, operandB);

        [Obsolete("Use " + nameof(Delay) + " instead.", true)]
        public TimeAdd TimeAdd(Outlet signal = null, Outlet timeDifference = null)
            => throw new NotSupportedException();

        public Outlet Delay(Outlet signal = null, Outlet timeDifference = null)
            => OperatorExtensionsWishes.Delay(_operatorFactory, signal, timeDifference);

        [Obsolete("Use " + nameof(Squash) + " instead.", true)]
        public TimeDivide TimeDivide(Outlet signal = null, Outlet timeDivider = null, Outlet origin = null)
            => throw new NotSupportedException();

        // TODO: Delegate to OperatorExtensionWishes and create and obsolete synonym.
        /// <summary> Opposite of <see cref="Stretch"/>. </summary>
        public TimeDivide Squash(Outlet signal = null, Outlet timeDivider = null, Outlet origin = null)
            => _operatorFactory.TimeDivide(signal, timeDivider, origin);

        [Obsolete("Use " + nameof(Stretch) + " instead.")]
        public TimeMultiply TimeMultiply(Outlet signal = null, Outlet timeMultiplier = null, Outlet origin = null)
            => throw new NotSupportedException();

        /// <inheritdoc cref="docs._default" />
        public Outlet Stretch(Outlet signal, Outlet timeFactor)
            => OperatorExtensionsWishes.Stretch(_operatorFactory, signal, timeFactor);

        public TimePower TimePower(Outlet signal = null, Outlet exponent = null, Outlet origin = null)
            => _operatorFactory.TimePower(signal, exponent, origin);

        [Obsolete("Typo. Use TimeSubtract instead.", true)]
        public TimeSubstract TimeSubstract(Outlet signal = null, Outlet timeDifference = null)
            => throw new NotSupportedException();

        // TODO: Delegate to OperatorExtensionWishes and create and obsolete synonym.
        public TimeSubstract TimeSubtract(Outlet signal = null, Outlet timeDifference = null)
            => _operatorFactory.TimeSubstract(signal, timeDifference);

        [Obsolete("Use _[123] instead.")]
        public ValueOperatorWrapper Value(double value = 0) => _[value];

        // TODO: Sample (see which overloads are practical.

        // Derived Operators

        /// <inheritdoc cref="docs._default" />
        public Outlet StrikeNote(
            Outlet sound, Outlet delay = null, Outlet volume = null)
            => OperatorExtensionsWishes.StrikeNote(_operatorFactory, sound, delay, volume);

        /// <inheritdoc cref="docs._vibrato" />
        public Outlet VibratoOverPitch(
            Outlet freq, (Outlet speed, Outlet depth) vibrato = default)
            => OperatorExtensionsWishes.VibratoOverPitch(_operatorFactory, freq, vibrato);

        /// <inheritdoc cref="docs._tremolo" />
        public Outlet Tremolo(
            Outlet sound, (Outlet speed, Outlet depth) tremolo = default)
            => OperatorExtensionsWishes.Tremolo(_operatorFactory, sound, tremolo);

        /// <inheritdoc cref="docs._panning" />
        public Outlet Panning(
            Outlet sound, Outlet panning) 
            => _operatorFactory.Panning(sound, panning, Channel);
        
        /// <inheritdoc cref="docs._panbrello" />
        public Outlet Panbrello(
            Outlet sound, (Outlet speed, Outlet depth) panbrello = default)
            => _operatorFactory.Panbrello(sound, panbrello, Channel);

        /// <inheritdoc cref="docs._pitchpan" />
        public Outlet PitchPan(
            Outlet actualFrequency, Outlet centerFrequency,
            Outlet referenceFrequency, Outlet referencePanning)
            => OperatorExtensionsWishes.PitchPan(
                _operatorFactory, actualFrequency, centerFrequency, referenceFrequency, referencePanning);

        /// <inheritdoc cref="docs._pitchpan" />
        public double PitchPan(
            double actualFrequency, double centerFrequency,
            double referenceFrequency, double referencePanning)
            => OperatorExtensionsWishes.PitchPan(
                _operatorFactory, actualFrequency, centerFrequency, referenceFrequency, referencePanning);

        public Outlet Echo(Outlet signal, Outlet magnitude = default, Outlet delay = default, int count = 16)
            => OperatorExtensionsWishes.Echo(_operatorFactory, signal, magnitude, delay, count);

        public Outlet EchoAdditive(Outlet signal, Outlet magnitude = default, Outlet delay = default, int count = 16)
            => OperatorExtensionsWishes.EchoAdditive(_operatorFactory, signal, magnitude, delay, count);

        public Outlet EchoFeedBack(Outlet signal, Outlet magnitude = default, Outlet delay = default, int count = 16)
            => OperatorExtensionsWishes.EchoFeedBack(_operatorFactory, signal, magnitude, delay, count);

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