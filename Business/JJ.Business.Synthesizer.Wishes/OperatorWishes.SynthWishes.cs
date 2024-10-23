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

        // TODO: Delegate everything to OperatorExtensionsWishes and add deprecated ones there too.
        
        [Obsolete("Use _[123] instead.")]
        public ValueOperatorWrapper Value(double value = 0) => _[value];

        /// <inheritdoc cref="docs._add"/>
        public Outlet Add(params Outlet[] operands) 
            => OperatorExtensionsWishes.Add(_operatorFactory, operands);
        
        /// <inheritdoc cref="docs._add"/>
        public Outlet Add(IList<Outlet> operands) 
            => OperatorExtensionsWishes.Add(_operatorFactory, operands);

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
            => OperatorExtensionsWishes.Multiply(_operatorFactory, operandA, operandB);

        [Obsolete("Origin parameter obsolete.", true)]
        public Outlet Multiply(Outlet operandA, Outlet operandB, Outlet origin)
            => OperatorExtensionsWishes.Multiply(_operatorFactory, operandA, operandB, origin);
        
        public Divide Divide(Outlet numerator, Outlet denominator)
            => _operatorFactory.Divide(numerator, denominator);

        [Obsolete("Origin parameter obsolete.", true)]
        public Divide Divide(Outlet numerator, Outlet denominator, Outlet origin) => throw new NotSupportedException();

        public Power Power(Outlet @base = null, Outlet exponent = null)
            => _operatorFactory.Power(@base, exponent);

        /// <inheritdoc cref="docs._sine" />
        public Outlet Sine(Outlet pitch = null) 
            => OperatorExtensionsWishes.Sine(_operatorFactory, pitch);

        // TODO: Add obsolete ones to OperatorExtensionsWishes too.
        [Obsolete("Use Multiply(Sine(pitch), volume) instead of Sine(volume, pitch).", true)]
        public Sine Sine(Outlet volume, Outlet pitch) => throw new NotSupportedException();

        [Obsolete("Use Add(Multiply(Sine(pitch), volume), level) instead of Sine(volume, pitch, level).", true)]
        public Sine Sine(Outlet volume, Outlet pitch, Outlet level) => throw new NotSupportedException();

        [Obsolete("Use Delay(Add(Multiply(Sine(pitch), volume), level), phaseStart) instead of Sine(volume, pitch, level, phaseStart).", true)]
        public Sine Sine(Outlet volume, Outlet pitch, Outlet level, Outlet phaseStart) => throw new NotSupportedException();

        public Outlet Delay(Outlet signal = null, Outlet timeDifference = null)
            => OperatorExtensionsWishes.Delay(_operatorFactory, signal, timeDifference);

        [Obsolete("Use Delay instead.", true)]
        public TimeAdd TimeAdd(Outlet signal = null, Outlet timeDifference = null) => throw new NotSupportedException();

        /// <inheritdoc cref="docs._default" />
        public Outlet Stretch(Outlet signal, Outlet timeFactor)
            => OperatorExtensionsWishes.Stretch(_operatorFactory, signal, timeFactor);

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