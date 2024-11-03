using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Business.Synthesizer.Enums;
using JJ.Persistence.Synthesizer;
using System;
using static JJ.Business.Synthesizer.Wishes.docs;

namespace JJ.Business.Synthesizer.Wishes
{
    // Calculation

    // SynthWishes
    
    public partial class SynthWishes
    {
        /// <inheritdoc cref="_synthwishescalculate"/>
        public double Calculate(Outlet outlet, double time)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            return outlet.Calculate(time, ChannelIndex);
        }
    }

    public static partial class CalculationExtensionWishes
    {
        // FluentOutlet

        public static double Calculate(this FluentOutlet fluentOutlet, double time, ChannelEnum channelEnum)
            => fluentOutlet.WrappedOutlet.Calculate(time, channelEnum);

        public static double Calculate(this FluentOutlet fluentOutlet, double time = 0, int channelIndex = 0)
            => fluentOutlet.WrappedOutlet.Calculate(time, channelIndex);
        
        // Curve

        public static double Calculate(this Curve curve, double time)
            => new CurveCalculator(curve).CalculateValue(time);

        // Sample

        public static double Calculate(this Sample sample, double time, Channel channel)
        {
            if (channel == null) throw new ArgumentNullException(nameof(channel));
            return Calculate(sample, time, channel.Index);
        }

        public static double Calculate(this Sample sample, double time, ChannelEnum channelEnum)
            => Calculate(sample, time, channelEnum.ToIndex());

        public static double Calculate(this Sample sample, double time, int channelIndex)
            => SampleCalculatorFactory.CreateSampleCalculator(sample).CalculateValue(channelIndex, time);

        // Operator

        public static double Calculate(this Outlet outlet, double time, ChannelEnum channelEnum)
            => Calculate(outlet, time, channelEnum.ToIndex());

        public static double Calculate(this Outlet outlet, double time = 0, int channelIndex = 0) 
            => new OperatorCalculator(channelIndex).CalculateValue(outlet, time);

        public static double Calculate(this Operator op, double time, ChannelEnum channelEnum)
            => Calculate(op, time, channelEnum.ToIndex());

        public static double Calculate(this Operator op, double time = 0, int channelIndex = 0)
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

        public static double Calculate(this Inlet inlet, double time, ChannelEnum channelEnum)
            => Calculate(inlet, time, channelEnum.ToIndex());
        
        public static double Calculate(this Inlet inlet, double time = 0, int channelIndex = 0)
        {
            if (inlet == null) throw new ArgumentNullException(nameof(inlet));
            var calculator = new OperatorCalculator(channelIndex);
            return calculator.CalculateValue(inlet.Input, time);
        }
    }
}
