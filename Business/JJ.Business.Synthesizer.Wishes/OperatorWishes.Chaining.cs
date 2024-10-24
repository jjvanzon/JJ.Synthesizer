using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Wishes
{
    public class ChainedOutlet
    {
        private readonly SynthWishes _synthWishes;
        private readonly Outlet _thisOutlet;

        public ChainedOutlet(SynthWishes synthWishes, Outlet firstFirstOperand)
        {
            _synthWishes = synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
            _thisOutlet = firstFirstOperand ?? throw new ArgumentNullException(nameof(firstFirstOperand));
        }
        
        // Swift Conversion back to Outlet
        
        public static implicit operator Outlet(ChainedOutlet chainedOutlet) 
            => chainedOutlet._thisOutlet;

        // Basic Operators

        public ChainedOutlet Add(params Outlet[] operands)
            => Add((IList<Outlet>)operands);
        
        public ChainedOutlet Add(IList<Outlet> operands)
            => _synthWishes.Add(new[] { _thisOutlet }.Concat(operands).ToArray());

        public ChainedOutlet Subtract(Outlet operandB)
            => _synthWishes.Subtract(_thisOutlet, operandB);

        public ChainedOutlet Subtract(double operandB)
            => _synthWishes.Subtract(_thisOutlet, operandB);
        
        public ChainedOutlet Multiply(Outlet operandB)
            => _synthWishes.Multiply(_thisOutlet, operandB);
            
        public ChainedOutlet Multiply(double operandB)
            => _synthWishes.Multiply(_thisOutlet, operandB);

        public ChainedOutlet Divide(Outlet denominator)
            => _synthWishes.Divide(_thisOutlet, denominator);

        public ChainedOutlet Divide(double denominator)
            => _synthWishes.Divide(_thisOutlet, denominator);

        public ChainedOutlet Power(Outlet exponent)
            => _synthWishes.Power(_thisOutlet, exponent);

        public ChainedOutlet Power(double exponent)
            => _synthWishes.Power(_thisOutlet, exponent);
        
        public ChainedOutlet Sine()
            => _synthWishes.Sine(_thisOutlet);

        public ChainedOutlet Delay(Outlet timeDifference)
            => _synthWishes.Delay(_thisOutlet, timeDifference);

        public ChainedOutlet Delay(double timeDifference)
            => _synthWishes.Delay(_thisOutlet, timeDifference);

        public ChainedOutlet Skip(Outlet timeDifference)
            => _synthWishes.Skip(_thisOutlet, timeDifference);

        public ChainedOutlet Skip(double timeDifference)
            => _synthWishes.Skip(_thisOutlet, timeDifference);
        
        public ChainedOutlet Stretch(Outlet timeFactor)
            => _synthWishes.Stretch(_thisOutlet, timeFactor);
        
        public ChainedOutlet Stretch(double timeFactor)
            => _synthWishes.Stretch(_thisOutlet, timeFactor);
        
        public ChainedOutlet Squash(Outlet timeDivider)
            => _synthWishes.Squash(_thisOutlet, timeDivider);
        
        public ChainedOutlet Squash(double timeDivider)
            => _synthWishes.Squash(_thisOutlet, timeDivider);
        
        public ChainedOutlet TimePower(Outlet exponent)
            => _synthWishes.TimePower(_thisOutlet, exponent);
        
        public ChainedOutlet TimePower(double exponent)
            => _synthWishes.TimePower(_thisOutlet, exponent);
        
        // Derived Operators

        public ChainedOutlet StrikeNote(Outlet delay = null, Outlet volume = default)
            => _synthWishes.StrikeNote(_thisOutlet, delay, volume);

        public ChainedOutlet StrikeNote(Outlet delay, double volume)
            => _synthWishes.StrikeNote(_thisOutlet, delay, volume);
        
        public ChainedOutlet StrikeNote(double delay, Outlet volume = default)
            => _synthWishes.StrikeNote(_thisOutlet, delay, volume);

        public ChainedOutlet StrikeNote(double delay, double volume)
            => _synthWishes.StrikeNote(_thisOutlet, delay, volume);

        public ChainedOutlet VibratoOverPitch(Outlet speed = default, Outlet depth = default)
            => _synthWishes.VibratoOverPitch(_thisOutlet, (speed, depth));

        public ChainedOutlet VibratoOverPitch(Outlet speed, double depth)
            => _synthWishes.VibratoOverPitch(_thisOutlet, (speed, depth));

        public ChainedOutlet VibratoOverPitch(double speed, Outlet depth = default)
            => _synthWishes.VibratoOverPitch(_thisOutlet, (speed, depth));

        public ChainedOutlet VibratoOverPitch(double speed, double depth)
            => _synthWishes.VibratoOverPitch(_thisOutlet, (speed, depth));

        public ChainedOutlet Tremolo(Outlet speed, Outlet depth)
            => _synthWishes.Tremolo(_thisOutlet, (speed, depth));

        public ChainedOutlet Tremolo(Outlet speed, double depth)
            => _synthWishes.Tremolo(_thisOutlet, (speed, depth));

        public ChainedOutlet Tremolo(double speed, Outlet depth)
            => _synthWishes.Tremolo(_thisOutlet, (speed, depth));

        public ChainedOutlet Tremolo(double speed, double depth)
            => _synthWishes.Tremolo(_thisOutlet, (speed, depth));

        public ChainedOutlet Panning(Outlet panning, ChannelEnum channel = default)
            => _synthWishes.Panning(_thisOutlet, panning, channel);

        public ChainedOutlet Panning(double panning, ChannelEnum channel = default)
            => _synthWishes.Panning(_thisOutlet, panning, channel);
        
        /// <inheritdoc cref="docs._panbrello" />
        public ChainedOutlet Panbrello(Outlet speed = default, Outlet depth = default, ChannelEnum channel = default)
            => _synthWishes.Panbrello(_thisOutlet, (speed, depth), channel);
        
        /// <inheritdoc cref="docs._panbrello" />
        public ChainedOutlet Panbrello(Outlet speed, double depth, ChannelEnum channel = default)
            => _synthWishes.Panbrello(_thisOutlet, (speed, depth), channel);
        
        /// <inheritdoc cref="docs._panbrello" />
        public ChainedOutlet Panbrello(double speed, Outlet depth, ChannelEnum channel = default)
            => _synthWishes.Panbrello(_thisOutlet, (speed, depth), channel);

        /// <inheritdoc cref="docs._panbrello" />
        public ChainedOutlet Panbrello(double speed, double depth, ChannelEnum channel = default) 
            => _synthWishes.Panbrello(_thisOutlet, (speed, depth), channel);
        
        /// <inheritdoc cref="docs._pitchpan" />
        public Outlet PitchPan(Outlet centerFrequency, Outlet referenceFrequency, Outlet referencePanning)
            => _synthWishes.PitchPan(_thisOutlet, centerFrequency, referenceFrequency, referencePanning);

        /// <inheritdoc cref="docs._pitchpan" />
        public Outlet PitchPan(double centerFrequency, double referenceFrequency, double referencePanning)
            => _synthWishes.PitchPan(_thisOutlet, centerFrequency, referenceFrequency, referencePanning);

        public ChainedOutlet Echo(Outlet magnitude = default, Outlet delay = default, int count = 8)
            => _synthWishes.Echo(_thisOutlet, magnitude, delay, count);

        public ChainedOutlet Echo(Outlet magnitude, double delay, int count = 8)
            => _synthWishes.Echo(_thisOutlet, magnitude, delay, count);

        public ChainedOutlet Echo(double magnitude, Outlet delay, int count = 8)
            => _synthWishes.Echo(_thisOutlet, magnitude, delay, count);

        public ChainedOutlet Echo(double magnitude = default, double delay = default, int count = 8)
            => _synthWishes.Echo(_thisOutlet, magnitude, delay, count);

        public ChainedOutlet EchoAdditive(Outlet magnitude = default, Outlet delay = default, int count = 8)
            => _synthWishes.EchoAdditive(_thisOutlet, magnitude, delay, count);

        public ChainedOutlet EchoAdditive(Outlet magnitude, double delay, int count = 8)
            => _synthWishes.EchoAdditive(_thisOutlet, magnitude, delay, count);

        public ChainedOutlet EchoAdditive(double magnitude, Outlet delay, int count = 8)
            => _synthWishes.EchoAdditive(_thisOutlet, magnitude, delay, count);

        public ChainedOutlet EchoAdditive(double magnitude = default, double delay = default, int count = 8)
            => _synthWishes.EchoAdditive(_thisOutlet, magnitude, delay, count);

        public ChainedOutlet EchoFeedBack(Outlet magnitude = default, Outlet delay = default, int count = 8)
            => _synthWishes.EchoFeedBack(_thisOutlet, magnitude, delay, count);

        public ChainedOutlet EchoFeedBack(Outlet magnitude, double delay, int count = 8)
            => _synthWishes.EchoFeedBack(_thisOutlet, magnitude, delay, count);

        public ChainedOutlet EchoFeedBack(double magnitude, Outlet delay, int count = 8)
            => _synthWishes.EchoFeedBack(_thisOutlet, magnitude, delay, count);

        public ChainedOutlet EchoFeedBack(double magnitude = default, double delay = default, int count = 8)
            => _synthWishes.EchoFeedBack(_thisOutlet, magnitude, delay, count);
    }
}
