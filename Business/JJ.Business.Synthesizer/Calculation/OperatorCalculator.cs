using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Common;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Calculation
{
    public class OperatorCalculator
    {
        private ChannelType _channelType;

        private IDictionary<string, Func<Operator, double, double>> _funcDictionary;

        private IDictionary<SampleChannel, ISampleCalculator> _sampleCalculatorDictionary =
            new Dictionary<SampleChannel, ISampleCalculator>();

        public OperatorCalculator(ChannelType channelType)
        {
            if (channelType == null) throw new NullException(() => channelType);

            _channelType = channelType;

            _funcDictionary = new Dictionary<string, Func<Operator, double, double>>
            {
                { PropertyNames.Add, CalculateAdd },
                { PropertyNames.Adder, CalculateAdder },
                { PropertyNames.Divide, CalculateDivide },
                { PropertyNames.Multiply, CalculateMultiply },
                { PropertyNames.PatchInlet, CalculatePatchInlet },
                { PropertyNames.PatchOutlet, CalculatePatchOutlet },
                { PropertyNames.Power, CalculatePower },
                { PropertyNames.Sine, CalculateSine },
                { PropertyNames.Substract, CalculateSubstract },
                { PropertyNames.TimeAdd, CalculateTimeAdd },
                { PropertyNames.TimeDivide, CalculateTimeDivide },
                { PropertyNames.TimeMultiply, CalculateTimeMultiply },
                { PropertyNames.TimePower, CalculateTimePower },
                { PropertyNames.TimeSubstract, CalculateTimeSubstract },
                { PropertyNames.ValueOperator, CalculateValueOperator },
                { PropertyNames.CurveIn, CalculateCurveIn },
            };
        }

        public double CalculateValue(Outlet outlet, double time)
        {
            Func<Operator, double, double> func = _funcDictionary[outlet.Operator.OperatorTypeName];
            // TODO: This will break when there are multiple outlets.
            double value = func(outlet.Operator, time);
            return value;
        }

        private double CalculateAdd(Operator op, double time)
        {
            var wrapper = new Add(op);
            Outlet operandAOutlet = wrapper.OperandA;
            Outlet operandBOutlet = wrapper.OperandB;

            if (operandAOutlet == null || operandBOutlet == null) return 0;

            double a = CalculateValue(operandAOutlet, time);
            double b = CalculateValue(operandBOutlet, time);
            return a + b;
        }

        private double CalculateAdder(Operator op, double time)
        {
            var wrapper = new Adder(op);

            IList<Outlet> operands = wrapper.Operands;

            double result = 0;

            for (int i = 0; i < operands.Count; i++)
            {
                Outlet operand = operands[i];

                if (operand != null)
                {
                    result += CalculateValue(operand, time);
                }
            }

            return result;
        }

        private double CalculateDivide(Operator op, double time)
        {
            var wrapper = new Divide(op);
            Outlet originOutlet = wrapper.Origin;
            Outlet numeratorOutlet = wrapper.Numerator;
            Outlet denominatorOutlet = wrapper.Denominator;

            // Without Origin
            if (originOutlet == null)
            {
                if (numeratorOutlet == null || denominatorOutlet == null) return 0;

                double denominator = CalculateValue(denominatorOutlet, time);

                if (denominator == 0) return 0;

                double numerator = CalculateValue(numeratorOutlet, time);

                return numerator / denominator;
            }

            // With Origin
            else
            {
                double origin = CalculateValue(originOutlet, time);

                if (numeratorOutlet == null || denominatorOutlet == null) return origin;

                double denominator = CalculateValue(denominatorOutlet, time);

                if (denominator == 0) return origin;

                double numerator = CalculateValue(wrapper.Numerator, time);

                return (numerator - origin) / denominator + origin;
            }
        }

        private double CalculateMultiply(Operator op, double time)
        {
            var wrapper = new Multiply(op);
            Outlet originOutlet = wrapper.Origin;
            Outlet operandAOutlet = wrapper.OperandA;
            Outlet operandBOutlet = wrapper.OperandB;

            if (originOutlet == null)
            {
                if (operandAOutlet == null || operandBOutlet == null) return 0;

                double a = CalculateValue(operandAOutlet, time);
                double b = CalculateValue(operandBOutlet, time);
                return a * b;
            }
            else
            {
                double origin = CalculateValue(originOutlet, time);

                if (operandAOutlet == null || operandBOutlet == null) return origin;

                double a = CalculateValue(operandAOutlet, time);
                double b = CalculateValue(operandBOutlet, time);
                return (a - origin) * b + origin;
            }
        }

        private double CalculatePatchInlet(Operator op, double time)
        {
            var wrapper = new PatchInlet(op);

            Outlet inputOutlet = wrapper.Input;

            if (inputOutlet == null) return 0;

            return CalculateValue(inputOutlet, time);
        }

        private double CalculatePatchOutlet(Operator op, double time)
        {
            var wrapper = new PatchOutlet(op);

            Outlet inputOutlet = wrapper.Input;

            if (inputOutlet == null) return 0;

            return CalculateValue(inputOutlet, time);
        }

        private double CalculatePower(Operator op, double time)
        {
            var wrapper = new Power(op);
            Outlet baseOutlet = wrapper.Base;
            Outlet exponentOutlet = wrapper.Exponent;

            if (baseOutlet == null || exponentOutlet == null) return 0;

            double @base = CalculateValue(baseOutlet, time);
            double exponent = CalculateValue(exponentOutlet, time);

            return Math.Pow(@base, exponent);
        }

        private double CalculateSine(Operator op, double time)
        {
            var wrapper = new Sine(op);
            Outlet volumeOutlet = wrapper.Volume;
            Outlet pitchOutlet = wrapper.Volume;

            if (volumeOutlet == null || pitchOutlet == null) return 0;

            Outlet levelOutlet = wrapper.Level;
            Outlet phaseStartOutlet = wrapper.PhaseStart;

            double volume = CalculateValue(volumeOutlet, time);
            double pitch = CalculateValue(pitchOutlet, time);

            if (levelOutlet == null && phaseStartOutlet == null)
            {
                return volume * Math.Sin(2 * Math.PI * pitch * time);
            }

            double level = levelOutlet != null ? CalculateValue(levelOutlet, time) : 0;
            double phaseStart = levelOutlet != null ? CalculateValue(phaseStartOutlet, time) : 0;

            double result = level + volume * Math.Sin(2 * (Math.PI * phaseStart + Math.PI * pitch * time));
            return result;
        }

        private double CalculateSubstract(Operator op, double time)
        {
            var wrapper = new Substract(op);
            Outlet operandAOutlet = wrapper.OperandA;
            Outlet operandBOutlet = wrapper.OperandB;

            if (operandAOutlet == null || operandBOutlet == null) return 0;

            double a = CalculateValue(operandAOutlet, time);
            double b = CalculateValue(operandBOutlet, time);

            return a - b;
        }

        private double CalculateTimeAdd(Operator op, double time)
        {
            var wrapper = new TimeAdd(op);

            Outlet signalOutlet = wrapper.Signal;
            if (signalOutlet == null) return 0;

            Outlet timeDifferenceOutlet = wrapper.TimeDifference;
            if (timeDifferenceOutlet == null)
            {
                double result = CalculateValue(signalOutlet, time);
                return result;
            }

            // IMPORTANT: To add time to the output, you have substract time from the input.
            double timeDifference = CalculateValue(timeDifferenceOutlet, time);
            double transformedTime = time - timeDifference;
            double result2 = CalculateValue(signalOutlet, transformedTime);
            return result2;
        }

        private double CalculateTimeDivide(Operator op, double time)
        {
            var wrapper = new TimeDivide(op);

            // Determine origin
            Outlet originOutlet = wrapper.Origin;
            double origin = 0;
            if (originOutlet != null)
            {
                origin = CalculateValue(originOutlet, time);
            }

            // No signal? Exit with default (the origin).
            Outlet signalOutlet = wrapper.Signal;
            if (signalOutlet == null)
            {
                return origin;
            }

            // No time divider? Just pass through signal.
            Outlet timeDividerOutlet = wrapper.TimeDivider;
            if (timeDividerOutlet == null)
            {
                double result = CalculateValue(signalOutlet, time);
                return result;
            }

            // Time divider 0? Don't return infinity, but just pass through signal.
            double timeDivider = CalculateValue(timeDividerOutlet, time);
            if (timeDivider == 0)
            {
                double result = CalculateValue(signalOutlet, time);
                return result;
            }

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.

            // Formula without origin
            if (originOutlet == null)
            {
                double transformedTime = time * timeDivider;
                double result = CalculateValue(signalOutlet, transformedTime);
                return result;
            }

            // Formula with origin
            else
            {
                double transformedTime = (time - origin) * timeDivider + origin;
                double result = CalculateValue(signalOutlet, transformedTime);
                return result;
            }
        }

        private double CalculateTimeMultiply(Operator op, double time)
        {
            var wrapper = new TimeMultiply(op);

            // Determine origin
            Outlet originOutlet = wrapper.Origin;
            double origin = 0;
            if (originOutlet != null)
            {
                origin = CalculateValue(originOutlet, time);
            }

            // No signal? Exit with default (the origin).
            Outlet signalOutlet = wrapper.Signal;
            if (signalOutlet == null)
            {
                return origin;
            }

            // No time multiplier? Just pass through signal.
            Outlet timeMultiplierOutlet = wrapper.TimeMultiplier;
            if (timeMultiplierOutlet == null)
            {
                double result = CalculateValue(signalOutlet, time);
                return result;
            }

            // Time multiplier 0? See that as multiplier = 1 or rather: just pass through signal.
            double timeMultiply = CalculateValue(timeMultiplierOutlet, time);
            if (timeMultiply == 0)
            {
                double result = CalculateValue(signalOutlet, time);
                return result;
            }

            // IMPORTANT: To multiply the time in the output, you have to divide the time of the input.

            // Formula without origin
            if (originOutlet == null)
            {
                double transformedTime = time / timeMultiply;
                double result = CalculateValue(signalOutlet, transformedTime);
                return result;
            }

            // Formula with origin
            else
            {
                double transformedTime = (time - origin) / timeMultiply + origin;
                double result = CalculateValue(signalOutlet, transformedTime);
                return result;
            }
        }

        private double CalculateTimePower(Operator op, double time)
        {
            var wrapper = new TimePower(op);

            Outlet signalOutlet = wrapper.Signal;
            if (signalOutlet == null)
            {
                return 0;
            }

            Outlet exponentOutlet = wrapper.Exponent;
            if (exponentOutlet == null)
            {
                return CalculateValue(signalOutlet, time);
            }

            // IMPORTANT: 

            // To increase time in the output, you have to decrease time of the input. 
            // That is why the reciprocal of the exponent is used.

            // Furthermore, you can not use a fractional exponent on a negative number.
            // Time can be negative, that is why the sign is taken off the time 
            // before taking the power and then added to it again after taking the power.

            Outlet originOutlet = wrapper.Origin;
            if (originOutlet == null)
            {
                // (time: -4, exponent: 2) => -1 * Pow(4, 1/2)
                double timeAbs = Math.Abs(time);
                double timeSign = Math.Sign(time);

                double exponent = CalculateValue(exponentOutlet, time);

                double transformedTime = timeSign * Math.Pow(timeAbs, 1 / exponent);

                double result = CalculateValue(signalOutlet, transformedTime);
                return result;
            }
            else
            {
                double origin = CalculateValue(originOutlet, time);

                double timeAbs = System.Math.Abs(time - origin);
                double timeSign = System.Math.Sign(time - origin);

                double exponent = CalculateValue(exponentOutlet, time);

                double transformedTime = timeSign * Math.Pow(timeAbs, 1 / exponent) + origin;

                double result = CalculateValue(signalOutlet, transformedTime);
                return result;
            }
        }

        private double CalculateTimeSubstract(Operator op, double time)
        {
            var wrapper = new TimeSubstract(op);

            Outlet signalOutlet = wrapper.Signal;
            if (signalOutlet == null) return 0;

            Outlet timeDifferenceOutlet = wrapper.TimeDifference;
            if (timeDifferenceOutlet == null)
            {
                double result = CalculateValue(signalOutlet, time);
                return result;
            }

            // IMPORTANT: To substract time from the output, you have add time to the input.
            double timeDifference = CalculateValue(timeDifferenceOutlet, time);
            double transformedTime = time + timeDifference;
            double result2 = CalculateValue(signalOutlet, transformedTime);
            return result2;
        }

        private double CalculateValueOperator(Operator op, double time)
        {
            if (op.AsValueOperator == null) throw new NullException(() => op.AsValueOperator);

            var wrapper = new ValueOperatorWrapper(op);

            return wrapper.Value;
        }

        private double CalculateCurveIn(Operator op, double time)
        {
            if (op.AsCurveIn == null) throw new NullException(() => op.AsCurveIn);

            if (op.AsCurveIn.Curve == null) return 0; // TODO: Think about if this null tolerance is appropriate.

            Curve curve = op.AsCurveIn.Curve;

            var curveCalculator = new CurveCalculator(curve);
            double result = curveCalculator.CalculateValue(time);
            return result;
        }

        private double CalculateSampleOperator(Operator op, double time)
        {
            if (op.AsSampleOperator == null) throw new NullException(() => op.AsSampleOperator);

            if (op.AsSampleOperator.Sample == null) return 0; // TODO: Think about if this null tolerance is appropriate.

            Sample sample = op.AsSampleOperator.Sample;

            // TODO: What about when the channel type is not there in the Sample?
            SampleChannel sampleChannel = sample.SampleChannels.Where(x => x.ChannelType.ID == _channelType.ID).Single();

            ISampleCalculator sampleCalculator;
            if (!_sampleCalculatorDictionary.TryGetValue(sampleChannel, out sampleCalculator))
            {
                sampleCalculator = SampleCalculatorFactory.CreateSampleCalculator(sampleChannel);
                _sampleCalculatorDictionary.Add(sampleChannel, sampleCalculator);
            }

            double result = sampleCalculator.CalculateValue(time);
            return result;
        }
    }
}