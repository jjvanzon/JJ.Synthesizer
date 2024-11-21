using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Business.Synthesizer.Enums;
using JJ.Persistence.Synthesizer;
using System;
using System.Linq;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Wishes
{
    // Calculation

    // SynthWishes
    
    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._synthwishescalculate"/>
        public double Calculate(Outlet outlet, double time)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            return outlet.Calculate(time, ChannelIndex);
        }
    }

    public static class CalculationExtensionWishes
    {
        // FlowNode

        public static double Calculate(this FlowNode flowNode, double time, ChannelEnum channelEnum)
        {
            if (flowNode == null) throw new ArgumentNullException(nameof(flowNode));
            return flowNode.UnderlyingOutlet.Calculate(time, channelEnum);
        }

        public static double Calculate(this FlowNode flowNode, double time = 0, int channelIndex = 0)
        {
            if (flowNode == null) throw new ArgumentNullException(nameof(flowNode));
            return flowNode.UnderlyingOutlet.Calculate(time, channelIndex);
        }
        
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

    }

    // Complexity
    
    public static class ComplexityExtensionWishes
    {
        public static int Complexity(this Outlet outlet)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            return Complexity(outlet.Operator);
        }

        public static int Complexity(this Operator op)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));

            var operators = new[] { op }.SelectRecursive(x => x.Operands()
                                                               .Where(y => y != null)
                                                               .Select(y => y.Operator)
                                                               .Where(y => y.IsVar()));
            return operators.Count();
        }
        
        public static int Complexity(this StreamAudioResult result)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));
            return Complexity(result.UnderlyingAudioFileOutput);
        }
        
        public static int Complexity(this AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return entity.AudioFileOutputChannels.Sum(x => x.Complexity());
        }
        
        public static int Complexity(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return entity.Outlet?.Complexity() ?? 0;
        }
    }

    public partial class FlowNode
    {
        public int Complexity => _underlyingOutlet.Complexity();
    }
}
