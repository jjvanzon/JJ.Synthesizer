using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
// ReSharper disable FieldCanBeMadeReadOnly.Local

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
        
        // Swift Conversion Back to Outlet
        
        public static implicit operator Outlet(FluentOutlet fluentOutlet) 
            => fluentOutlet._thisOutlet;

        public Outlet Outlet => _thisOutlet;

        // Value
        
        public static explicit operator double(FluentOutlet fluentOutlet) 
            => fluentOutlet.Value;

        public double Value 
        {
            get
            {
                double? constant = _thisOutlet.AsConst();
                if (constant != null) return constant.Value;

                double calculated = _thisOutlet.Calculate(time: 0);
                return calculated;
            }
        }
        
        // Basic Operators
        
        public FluentOutlet Add(IList<Outlet> operands) => _synthWishes.Add(new[] { _thisOutlet }.Concat(operands).ToArray());
        
        public FluentOutlet Add(params Outlet[] operands) => _synthWishes.Add(new[] { _thisOutlet }.Concat(operands).ToArray());

        public FluentOutlet Add(Outlet b) => _synthWishes.Add(_thisOutlet, b);

        public FluentOutlet Add(double b) => _synthWishes.Add(_thisOutlet, b);
        
        public FluentOutlet Plus(params Outlet[] operands) => Add(operands);

        public FluentOutlet Plus(IList<Outlet> operands) =>  Add(operands);

        public FluentOutlet Plus(Outlet b) => Add(b);

        public FluentOutlet Plus(double b) => Add(b);

        public FluentOutlet Subtract(Outlet b) => _synthWishes.Subtract(_thisOutlet, b);

        public FluentOutlet Subtract(double b) => _synthWishes.Subtract(_thisOutlet, b);

        public FluentOutlet Minus(Outlet b) => Subtract(b);

        public FluentOutlet Minus(double b) => Subtract(b);

        public FluentOutlet Multiply(Outlet b) => _synthWishes.Multiply(_thisOutlet, b);
            
        public FluentOutlet Multiply(double b) => _synthWishes.Multiply(_thisOutlet, b);
        
        public FluentOutlet Times(Outlet b) => Multiply(b);

        public FluentOutlet Times(double b) => Multiply(b);

        public FluentOutlet Divide(Outlet b) => _synthWishes.Divide(_thisOutlet, b);

        public FluentOutlet Divide(double b) => _synthWishes.Divide(_thisOutlet, b);

        public FluentOutlet Power(Outlet exponent) => _synthWishes.Power(_thisOutlet, exponent);

        public FluentOutlet Power(double exponent) => _synthWishes.Power(_thisOutlet, exponent);

        /// <inheritdoc cref="docs._sine" />
        public FluentOutlet Sine => _synthWishes.Sine(_thisOutlet);

        public FluentOutlet Delay(Outlet delay) => _synthWishes.Delay(_thisOutlet, delay);

        public FluentOutlet Delay(double delay) => _synthWishes.Delay(_thisOutlet, delay);

        public FluentOutlet Skip(Outlet skip) => _synthWishes.Skip(_thisOutlet, skip);

        public FluentOutlet Skip(double skip) => _synthWishes.Skip(_thisOutlet, skip);
        
        public FluentOutlet Stretch(Outlet timeScale) => _synthWishes.Stretch(_thisOutlet, timeScale);
        
        public FluentOutlet Stretch(double timeScale) => _synthWishes.Stretch(_thisOutlet, timeScale);
        
        public FluentOutlet SpeedUp(Outlet speed) => _synthWishes.SpeedUp(_thisOutlet, speed);
        
        public FluentOutlet SpeedUp(double speed) => _synthWishes.SpeedUp(_thisOutlet, speed);
        
        public FluentOutlet TimePower(Outlet exponent) => _synthWishes.TimePower(_thisOutlet, exponent);
        
        public FluentOutlet TimePower(double exponent) => _synthWishes.TimePower(_thisOutlet, exponent);
        
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

        // Misc Chaining Methods
        
        public void PlayMono()
        {
            _synthWishes.Channel = _synthWishes.Single;
            _synthWishes.PlayMono(() => _thisOutlet);
        }
        
        // Delegate to Extension Methods
        // (Not Included with the Implicit Conversion to Outlet)
        
        public Sample GetSample() 
            => Outlet.GetSample();

        public SampleOperatorWrapper GetSampleWrapper() 
            => Outlet.GetSampleWrapper();

        public double Calculate(double time, ChannelEnum channelEnum) 
            => Outlet.Calculate(time, channelEnum);
        
        public double Calculate(double time = 0, int channelIndex = 0)  
            => Outlet.Calculate(time, channelIndex);
        
        // TODO: More

        // C# Operators
        
        // Operator +
        
        public static FluentOutlet operator +(FluentOutlet a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[0];
            b = b ?? x._[0];
            return x._[a].Plus(b);
        }
        
        public static FluentOutlet operator +(FluentOutlet a, Outlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[0];
            b = b ?? x._[0];
            return x._[a].Plus(b);
        }

        public static FluentOutlet operator +(Outlet a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[0];
            b = b ?? x._[0];
            return x._[a].Plus(b);
        }
                
        public static FluentOutlet operator +(FluentOutlet a, double b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[0];
            return x._[a].Plus(b);
        }

        public static FluentOutlet operator +(double a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            b = b ?? x._[0];
            return x._[a].Plus(b);
        }

        // Operator -
        
        public static FluentOutlet operator -(FluentOutlet a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[0];
            b = b ?? x._[0];
            return x._[a].Minus(b);
        }
        
        public static FluentOutlet operator -(FluentOutlet a, Outlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[0];
            b = b ?? x._[0];
            return x._[a].Minus(b);
        }
        
        public static FluentOutlet operator -(Outlet a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[0];
            b = b ?? x._[0];
            return x._[a].Minus(b);
        }
        
        public static FluentOutlet operator -(FluentOutlet a, double b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[0];
            return x._[a].Minus(b);
        }
        
        public static FluentOutlet operator -(double a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            b = b ?? x._[0];
            return x._[a].Minus(b);
        }

        // Operator *
        
        public static FluentOutlet operator *(FluentOutlet a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[1];
            b = b ?? x._[1];
            return x._[a].Times(b);
        }
        
        public static FluentOutlet operator *(FluentOutlet a, Outlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[1];
            b = b ?? x._[1];
            return x._[a].Times(b);
        }
        
        public static FluentOutlet operator *(Outlet a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[1];
            b = b ?? x._[1];
            return x._[a].Times(b);
        }
        
        public static FluentOutlet operator *(FluentOutlet a, double b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[1];
            return x._[a].Times(b);
        }
        
        public static FluentOutlet operator *(double a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            b = b ?? x._[1];
            return x._[a].Times(b);
        }

        // Operator /
        
        public static FluentOutlet operator /(FluentOutlet a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[0];
            b = b ?? x._[1];
            return x._[a].Divide(b);
        }
        
        public static FluentOutlet operator /(FluentOutlet a, Outlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[0];
            b = b ?? x._[1];
            return x._[a].Divide(b);
        }
                
        public static FluentOutlet operator /(Outlet a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[0];
            b = b ?? x._[1];
            return x._[a].Divide(b);
        }
        
        public static FluentOutlet operator /(FluentOutlet a, double b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[0];
            return x._[a].Divide(b);
        }
        
        public static FluentOutlet operator /(double a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            b = b ?? x._[1];
            return x._[a].Divide(b);
        }
    
        // Defaults SynthWishes preventing most exceptions in the C# operators.

        // ReSharper disable UnusedParameter.Local

        private static SynthWishes GetSynthWishesOrThrow(FluentOutlet a, FluentOutlet b) 
            => a?._synthWishes ?? b?._synthWishes ?? throw new Exception(noSynthWishesMessage);

        private static SynthWishes GetSynthWishesOrThrow(FluentOutlet a, double b) 
            => a?._synthWishes ?? throw new Exception(noSynthWishesMessage);

        private static SynthWishes GetSynthWishesOrThrow(double a, FluentOutlet b) 
            => b?._synthWishes ?? throw new Exception(noSynthWishesMessage);

        private static SynthWishes GetSynthWishesOrThrow(FluentOutlet a, Outlet b) 
            => a?._synthWishes ?? throw new Exception(noSynthWishesMessage);

        private static SynthWishes GetSynthWishesOrThrow(Outlet a, FluentOutlet b) 
            => b?._synthWishes ?? throw new Exception(noSynthWishesMessage);

        private const string noSynthWishesMessage = 
            "Cannot perform math operator when no Outlet is involved "+
            "to provide the Context in which to create more Outlets.";

    }
}
