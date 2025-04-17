using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Persistence.Synthesizer;
using System;
using System.Linq;
using JJ.Business.Synthesizer.Wishes.OperandWishes;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Business.Synthesizer.Wishes.docs;
using JJ.Framework.Common;

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
            return outlet.Calculate(time, GetChannel ?? default);
        }
    }

    public static class CalculationExtensionWishes
    {
        // FlowNode
        
        public static double Calculate(this FlowNode flowNode)
        {
            if (flowNode == null) throw new ArgumentNullException(nameof(flowNode));
            return flowNode.UnderlyingOutlet.Calculate();
        }
        
        public static double Calculate(this FlowNode flowNode, double time)
        {
            if (flowNode == null) throw new ArgumentNullException(nameof(flowNode));
            return flowNode.UnderlyingOutlet.Calculate(time);
        }

        public static double Calculate(this FlowNode flowNode, double time, int channel)
        {
            if (flowNode == null) throw new ArgumentNullException(nameof(flowNode));
            return flowNode.UnderlyingOutlet.Calculate(time, channel);
        }
        
        // Curve

        public static double Calculate(this Curve curve, double time = 0)
            => new CurveCalculator(curve).CalculateValue(time);

        // Sample

        public static double Calculate(this Sample sample, double time = 0, int channel = 0)
            => SampleCalculatorFactory.CreateSampleCalculator(sample).CalculateValue(channel, time);

        // Operator

        public static double Calculate(this Outlet outlet, double time = 0, int channel = 0) 
            => new OperatorCalculator(channel).CalculateValue(outlet, time);

        public static double Calculate(this Operator op, double time = 0, int channel = 0)
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

            return Calculate(outlet, time, channel);
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

            var operators = new[] { op }.UnionRecursive(x => x.Operands()
                                                              .Where(y => y != null)
                                                              .Select(y => y.Operator)
                                                              .Where(y => y.IsVar()));
            return operators.Count();
        }
        
        public static int Complexity(this FlowNode flowNode)
        {
            if (flowNode == null) throw new ArgumentNullException(nameof(flowNode));
            return Complexity(flowNode.UnderlyingOutlet);
        }

        internal static int Complexity(this Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            return tape.Outlets.Sum(Complexity);
        }

        public static int Complexity(this Buff buff)
        {
            if (buff == null) throw new ArgumentNullException(nameof(buff));
            return Complexity(buff.UnderlyingAudioFileOutput);
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
