using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace JJ.Business.Synthesizer.Wishes
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public partial class FluentOutlet
    {
        private readonly SynthWishes _synthWishes;
        private readonly Outlet _thisOutlet;

        private string DebuggerDisplay
        {
            get
            {
                var text = "";
                                
                if (!_thisOutlet.IsConst())
                { 
                    text += $"{Calculate()} = ";
                }

                text += $"{Stringify(true)} {{ {GetType().Name} }}";

                return text;
            }
        }

        public FluentOutlet(SynthWishes synthWishes, Outlet firstFirstOperand)
        {
            _synthWishes = synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
            _thisOutlet = firstFirstOperand ?? throw new ArgumentNullException(nameof(firstFirstOperand));
        }
        
        // Swift Conversion Back to Outlet
        
        public static implicit operator Outlet(FluentOutlet fluentOutlet) 
            => fluentOutlet?._thisOutlet;

        public Outlet Outlet => _thisOutlet;

        // Name
                
        public FluentOutlet WithName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return this;
            
            _thisOutlet.WithName(name);

            return this;
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

        public FluentOutlet Echo(int count = 4, Outlet magnitude = default, Outlet delay = default)
            => _synthWishes.Echo(_thisOutlet, count, magnitude, delay);

        public FluentOutlet Echo(int count, Outlet magnitude, double delay)
            => _synthWishes.Echo(_thisOutlet, count, magnitude, delay);

        public FluentOutlet Echo(int count, double magnitude, Outlet delay)
            => _synthWishes.Echo(_thisOutlet, count, magnitude, delay);

        public FluentOutlet Echo(int count = 4, double magnitude = default, double delay = default)
            => _synthWishes.Echo(_thisOutlet, count, magnitude, delay);

        public FluentOutlet EchoAdditive(int count = 8, Outlet magnitude = default, Outlet delay = default)
            => _synthWishes.EchoAdditive(_thisOutlet, count, magnitude, delay);

        public FluentOutlet EchoAdditive(int count, Outlet magnitude, double delay)
            => _synthWishes.EchoAdditive(_thisOutlet, count, magnitude, delay);

        public FluentOutlet EchoAdditive(int count, double magnitude, Outlet delay)
            => _synthWishes.EchoAdditive(_thisOutlet, count, magnitude, delay);

        public FluentOutlet EchoAdditive(int count = 8, double magnitude = default, double delay = default)
            => _synthWishes.EchoAdditive(_thisOutlet, count, magnitude, delay);

        public FluentOutlet EchoFeedBack(int count = 8, Outlet magnitude = default, Outlet delay = default)
            => _synthWishes.EchoFeedBack(_thisOutlet, count, magnitude, delay);

        public FluentOutlet EchoFeedBack(int count, Outlet magnitude, double delay)
            => _synthWishes.EchoFeedBack(_thisOutlet, count, magnitude, delay);

        public FluentOutlet EchoFeedBack(int count, double magnitude, Outlet delay)
            => _synthWishes.EchoFeedBack(_thisOutlet, count, magnitude, delay);

        public FluentOutlet EchoFeedBack(double magnitude = default, double delay = default, int count = 8)
            => _synthWishes.EchoFeedBack(_thisOutlet, count, magnitude, delay);

        public FluentOutlet EchoParallel(
            int count = 8, Outlet magnitude = default, Outlet delay = default, 
            bool mustAddAudioLength = true, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.EchoParallel(_thisOutlet, count, magnitude, delay, mustAddAudioLength, callerMemberName);

        // Curve Chaining Methods

        public FluentOutlet Curve(Outlet curve)
            => _thisOutlet * _synthWishes._[curve];

        public FluentOutlet Curve(IList<NodeInfo> nodeInfos)
            => _thisOutlet * _synthWishes.Curve(nodeInfos);

        public FluentOutlet Curve(params NodeInfo[] nodeInfos)
            => _thisOutlet * _synthWishes.Curve(nodeInfos);

        /// <inheritdoc cref="CurveFactory.CreateCurve(double, double?[])" />
        /// <inheritdoc cref="docs._createcurve" />
        public FluentOutlet Curve(string name, params double?[] values)
            => _thisOutlet * _synthWishes.Curve(values);

        /// <inheritdoc cref="CurveFactory.CreateCurve(double, double?[])" />
        /// <inheritdoc cref="docs._createcurve" />
        public FluentOutlet Curve(params double?[] values)
            => _thisOutlet * _synthWishes.Curve(values);

        /// <inheritdoc cref="docs._createcurvewithtuples" />
        public FluentOutlet Curve(IList<(double time, double value)> nodeTuples)
            => _thisOutlet * _synthWishes.Curve(nodeTuples);

        /// <inheritdoc cref="docs._createcurvewithtuples" />
        public FluentOutlet Curve(params (double time, double value)[] nodeTuples)
            => _thisOutlet * _synthWishes.Curve(nodeTuples);

        /// <inheritdoc cref="docs._createcurvefromstring" />
        public FluentOutlet Curve(string text) 
            => _thisOutlet * _synthWishes.Curve(text);

        /// <inheritdoc cref="docs._createcurvefromstring" />
        public FluentOutlet Curve(
            (double start, double end) x,
            (double min, double max) y,
            string text)
            => _thisOutlet * _synthWishes.Curve(x, y, text);
        
                
        // Value / Calculate
        
        public static explicit operator double(FluentOutlet fluentOutlet) 
            => fluentOutlet.Value;

        public double Value 
        {
            get
            {
                double? constant = AsConst;
                if (constant != null) return constant.Value;

                double calculated = Calculate(time: 0);
                return calculated;
            }
        }

        public double Calculate(double time, ChannelEnum channelEnum) 
            => _thisOutlet.Calculate(time, channelEnum);
        
        public double Calculate(double time = 0, int channelIndex = 0)  
            => _thisOutlet.Calculate(time, channelIndex);
        
        // Stringify
        
        public string Stringify(bool singleLine = false) => _thisOutlet.Stringify(singleLine);

        // Validation

        public Result Validate(bool recursive = true) => _thisOutlet.Validate(recursive);
        public void Assert(bool recursive = true) => _thisOutlet.Assert(recursive);
        public IList<string> GetWarnings(bool recursive = true) => _thisOutlet.GetWarnings(recursive);

        // Is/As
        
        public double? AsConst => _thisOutlet.AsConst();
        public bool IsConst => _thisOutlet.IsConst();
        public bool IsVar => _thisOutlet.IsVar();
        public bool IsAdd => _thisOutlet.IsAdd();
        public bool IsSubtract => _thisOutlet.IsSubtract();
        public bool IsMultiply => _thisOutlet.IsMultiply();
        public bool IsDivide => _thisOutlet.IsDivide();
        public bool IsPower => _thisOutlet.IsPower();
        public bool IsSine => _thisOutlet.IsSine();
        public bool IsDelay => _thisOutlet.IsDelay();
        public bool IsSkip => _thisOutlet.IsSkip();
        public bool IsStretch => _thisOutlet.IsStretch();
        public bool IsSpeedUp => _thisOutlet.IsSpeedUp();
        public bool IsTimePower => _thisOutlet.IsTimePower();
        internal bool IsAdder => _thisOutlet.IsAdder();

        // Related Object

        public Sample GetSample() => _thisOutlet.GetSample();

        public SampleOperatorWrapper GetSampleWrapper() => _thisOutlet.GetSampleWrapper();

        public Curve GetCurve() => _thisOutlet.GetCurve();

        public CurveInWrapper GetCurveWrapper() => _thisOutlet.GetCurveWrapper();
            
        // Fluent Configuration
        
        public FluentOutlet WithAudioLength(FluentOutlet newLength)
        {
            _synthWishes.WithAudioLength(newLength);
            return this;
        }

        public FluentOutlet AddAudioLength(FluentOutlet additionalLength)
        {
            _synthWishes.AddAudioLength(additionalLength);
            return this;
        }

        // Misc Chaining Methods

        public FluentOutlet PlayMono(double volume = default)
        {
            _synthWishes.Channel = ChannelEnum.Single;
            _synthWishes.Mono().Play(() => _synthWishes.Multiply(_thisOutlet, volume));
            return this;
        }
    }
}
