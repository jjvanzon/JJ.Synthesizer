using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Wishes
{
    public class FluentOutlet
    {
        private readonly SynthWishes _synthWishes;
        private readonly Outlet _thisOutlet;

        public FluentOutlet(SynthWishes synthWishes, Outlet firstFirstOperand)
        {
            _synthWishes = synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
            _thisOutlet = firstFirstOperand ?? throw new ArgumentNullException(nameof(firstFirstOperand));
        }
        
        // Swift Conversion back to Outlet
        
        public static implicit operator Outlet(FluentOutlet fluentOutlet) 
            => fluentOutlet._thisOutlet;

        public Outlet Outlet => _thisOutlet;

        // Basic Operators

        public FluentOutlet Add(params Outlet[] operands)
            => Add((IList<Outlet>)operands);
        
        public FluentOutlet Add(IList<Outlet> operands)
            => _synthWishes.Add(new[] { _thisOutlet }.Concat(operands).ToArray());

        public FluentOutlet Add(Outlet outlet)
            => Add(_thisOutlet, outlet);

        public FluentOutlet Add(double outlet)
            => _synthWishes.Add(_thisOutlet, outlet);
        
        public FluentOutlet Plus(params Outlet[] operands)
            => Add((IList<Outlet>)operands);

        public FluentOutlet Plus(IList<Outlet> operands)
            => _synthWishes.Add(new[] { _thisOutlet }.Concat(operands).ToArray());

        public FluentOutlet Plus(Outlet outlet)
            => Add(_thisOutlet, outlet);

        public FluentOutlet Plus(double outlet)
            => _synthWishes.Add(_thisOutlet, outlet);

        public FluentOutlet Subtract(Outlet operandB)
            => _synthWishes.Subtract(_thisOutlet, operandB);

        public FluentOutlet Subtract(double operandB)
            => _synthWishes.Subtract(_thisOutlet, operandB);

        public FluentOutlet Minus(Outlet operandB)
            => _synthWishes.Subtract(_thisOutlet, operandB);

        public FluentOutlet Minus(double operandB)
            => _synthWishes.Subtract(_thisOutlet, operandB);

        public FluentOutlet Multiply(Outlet operandB)
            => _synthWishes.Multiply(_thisOutlet, operandB);
            
        public FluentOutlet Multiply(double operandB)
            => _synthWishes.Multiply(_thisOutlet, operandB);
        
        public FluentOutlet Times(Outlet operandB)
            => _synthWishes.Multiply(_thisOutlet, operandB);

        public FluentOutlet Times(double operandB)
            => _synthWishes.Multiply(_thisOutlet, operandB);

        public FluentOutlet Divide(Outlet denominator)
            => _synthWishes.Divide(_thisOutlet, denominator);

        public FluentOutlet Divide(double denominator)
            => _synthWishes.Divide(_thisOutlet, denominator);

        public FluentOutlet Power(Outlet exponent)
            => _synthWishes.Power(_thisOutlet, exponent);

        public FluentOutlet Power(double exponent)
            => _synthWishes.Power(_thisOutlet, exponent);

        public FluentOutlet Sine 
            => _synthWishes.Sine(_thisOutlet);

        public FluentOutlet Delay(Outlet timeDifference)
            => _synthWishes.Delay(_thisOutlet, timeDifference);

        public FluentOutlet Delay(double timeDifference)
            => _synthWishes.Delay(_thisOutlet, timeDifference);

        public FluentOutlet Skip(Outlet timeDifference)
            => _synthWishes.Skip(_thisOutlet, timeDifference);

        public FluentOutlet Skip(double timeDifference)
            => _synthWishes.Skip(_thisOutlet, timeDifference);
        
        public FluentOutlet Stretch(Outlet timeFactor)
            => _synthWishes.Stretch(_thisOutlet, timeFactor);
        
        public FluentOutlet Stretch(double timeFactor)
            => _synthWishes.Stretch(_thisOutlet, timeFactor);
        
        public FluentOutlet Squash(Outlet timeDivider)
            => _synthWishes.Squash(_thisOutlet, timeDivider);
        
        public FluentOutlet Squash(double timeDivider)
            => _synthWishes.Squash(_thisOutlet, timeDivider);
        
        public FluentOutlet TimePower(Outlet exponent)
            => _synthWishes.TimePower(_thisOutlet, exponent);
        
        public FluentOutlet TimePower(double exponent)
            => _synthWishes.TimePower(_thisOutlet, exponent);
        
        // Derived Operators

        public FluentOutlet StrikeNote(Outlet delay = null, Outlet volume = default)
            => _synthWishes.StrikeNote(_thisOutlet, delay, volume);

        public FluentOutlet StrikeNote(Outlet delay, double volume)
            => _synthWishes.StrikeNote(_thisOutlet, delay, volume);
        
        public FluentOutlet StrikeNote(double delay, Outlet volume = default)
            => _synthWishes.StrikeNote(_thisOutlet, delay, volume);

        public FluentOutlet StrikeNote(double delay, double volume)
            => _synthWishes.StrikeNote(_thisOutlet, delay, volume);

        public FluentOutlet VibratoOverPitch(Outlet speed = default, Outlet depth = default)
            => _synthWishes.VibratoOverPitch(_thisOutlet, (speed, depth));

        public FluentOutlet VibratoOverPitch(Outlet speed, double depth)
            => _synthWishes.VibratoOverPitch(_thisOutlet, (speed, depth));

        public FluentOutlet VibratoOverPitch(double speed, Outlet depth = default)
            => _synthWishes.VibratoOverPitch(_thisOutlet, (speed, depth));

        public FluentOutlet VibratoOverPitch(double speed, double depth)
            => _synthWishes.VibratoOverPitch(_thisOutlet, (speed, depth));

        public FluentOutlet Tremolo(Outlet speed, Outlet depth)
            => _synthWishes.Tremolo(_thisOutlet, (speed, depth));

        public FluentOutlet Tremolo(Outlet speed, double depth)
            => _synthWishes.Tremolo(_thisOutlet, (speed, depth));

        public FluentOutlet Tremolo(double speed, Outlet depth)
            => _synthWishes.Tremolo(_thisOutlet, (speed, depth));

        public FluentOutlet Tremolo(double speed, double depth)
            => _synthWishes.Tremolo(_thisOutlet, (speed, depth));

        public FluentOutlet Panning(Outlet panning, ChannelEnum channel = default)
            => _synthWishes.Panning(_thisOutlet, panning, channel);

        public FluentOutlet Panning(double panning, ChannelEnum channel = default)
            => _synthWishes.Panning(_thisOutlet, panning, channel);
        
        /// <inheritdoc cref="docs._panbrello" />
        public FluentOutlet Panbrello(Outlet speed = default, Outlet depth = default, ChannelEnum channel = default)
            => _synthWishes.Panbrello(_thisOutlet, (speed, depth), channel);
        
        /// <inheritdoc cref="docs._panbrello" />
        public FluentOutlet Panbrello(Outlet speed, double depth, ChannelEnum channel = default)
            => _synthWishes.Panbrello(_thisOutlet, (speed, depth), channel);
        
        /// <inheritdoc cref="docs._panbrello" />
        public FluentOutlet Panbrello(double speed, Outlet depth, ChannelEnum channel = default)
            => _synthWishes.Panbrello(_thisOutlet, (speed, depth), channel);

        /// <inheritdoc cref="docs._panbrello" />
        public FluentOutlet Panbrello(double speed, double depth, ChannelEnum channel = default) 
            => _synthWishes.Panbrello(_thisOutlet, (speed, depth), channel);
        
        /// <inheritdoc cref="docs._pitchpan" />
        public Outlet PitchPan(Outlet centerFrequency, Outlet referenceFrequency, Outlet referencePanning)
            => _synthWishes.PitchPan(_thisOutlet, centerFrequency, referenceFrequency, referencePanning);

        /// <inheritdoc cref="docs._pitchpan" />
        public Outlet PitchPan(double centerFrequency, double referenceFrequency, double referencePanning)
            => _synthWishes.PitchPan(_thisOutlet, centerFrequency, referenceFrequency, referencePanning);

        public FluentOutlet Echo(Outlet magnitude = default, Outlet delay = default, int count = 8)
            => _synthWishes.Echo(_thisOutlet, magnitude, delay, count);

        public FluentOutlet Echo(Outlet magnitude, double delay, int count = 8)
            => _synthWishes.Echo(_thisOutlet, magnitude, delay, count);

        public FluentOutlet Echo(double magnitude, Outlet delay, int count = 8)
            => _synthWishes.Echo(_thisOutlet, magnitude, delay, count);

        public FluentOutlet Echo(double magnitude = default, double delay = default, int count = 8)
            => _synthWishes.Echo(_thisOutlet, magnitude, delay, count);

        public FluentOutlet EchoAdditive(Outlet magnitude = default, Outlet delay = default, int count = 8)
            => _synthWishes.EchoAdditive(_thisOutlet, magnitude, delay, count);

        public FluentOutlet EchoAdditive(Outlet magnitude, double delay, int count = 8)
            => _synthWishes.EchoAdditive(_thisOutlet, magnitude, delay, count);

        public FluentOutlet EchoAdditive(double magnitude, Outlet delay, int count = 8)
            => _synthWishes.EchoAdditive(_thisOutlet, magnitude, delay, count);

        public FluentOutlet EchoAdditive(double magnitude = default, double delay = default, int count = 8)
            => _synthWishes.EchoAdditive(_thisOutlet, magnitude, delay, count);

        public FluentOutlet EchoFeedBack(Outlet magnitude = default, Outlet delay = default, int count = 8)
            => _synthWishes.EchoFeedBack(_thisOutlet, magnitude, delay, count);

        public FluentOutlet EchoFeedBack(Outlet magnitude, double delay, int count = 8)
            => _synthWishes.EchoFeedBack(_thisOutlet, magnitude, delay, count);

        public FluentOutlet EchoFeedBack(double magnitude, Outlet delay, int count = 8)
            => _synthWishes.EchoFeedBack(_thisOutlet, magnitude, delay, count);

        public FluentOutlet EchoFeedBack(double magnitude = default, double delay = default, int count = 8)
            => _synthWishes.EchoFeedBack(_thisOutlet, magnitude, delay, count);

        public void PlayMono()
        {
            _synthWishes.Channel = _synthWishes.Single;
            _synthWishes.PlayMono(() => _thisOutlet);
        }
    }
}
