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
using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Business.Synthesizer.Calculation;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal class TestOperatorCalculator_WithWrappersAndNullChecks_MoreOperators : ITestOperatorCalculator
    {
        private int _channelIndex;

        private IDictionary<string, Func<Operator, double, double>> _funcDictionary;

        private IDictionary<int, ISampleCalculator> _sampleCalculatorDictionary =
            new Dictionary<int, ISampleCalculator>();

        public TestOperatorCalculator_WithWrappersAndNullChecks_MoreOperators(int channelIndex)
        {
            if (channelIndex < 0) throw new Exception("channelIndex must a positive number.");

            _channelIndex = channelIndex;

            _funcDictionary = new Dictionary<string, Func<Operator, double, double>>
            {
                { PropertyNames.Add, CalculateAdd },
                { PropertyNames.Adder, CalculateAdder },
                { PropertyNames.CurveIn, CalculateCurveIn },
                { PropertyNames.Divide, CalculateDivide },
                { PropertyNames.Multiply, CalculateMultiply },
                { PropertyNames.PatchInlet, CalculatePatchInlet },
                { PropertyNames.PatchOutlet, CalculatePatchOutlet },
                { PropertyNames.Power, CalculatePower },
                { PropertyNames.SampleOperator, CalculateSampleOperator },
                { PropertyNames.Sine, CalculateSine },
                { PropertyNames.Substract, CalculateSubstract },
                { PropertyNames.TimeAdd, CalculateTimeAdd },
                { PropertyNames.TimeDivide, CalculateTimeDivide },
                { PropertyNames.TimeMultiply, CalculateTimeMultiply },
                { PropertyNames.TimePower, CalculateTimePower },
                { PropertyNames.TimeSubstract, CalculateTimeSubstract },
                { PropertyNames.ValueOperator, CalculateValueOperator },
            };
        }

        public double Calculate(Outlet outlet, double time)
        {
            Func<Operator, double, double> func = _funcDictionary[outlet.Operator.OperatorTypeName];
            // TODO: This will break when there are multiple outlets.
            double value = func(outlet.Operator, time);
            return value;
        }

        private double CalculateAdd(Operator op, double time)
        {
            var wrapper = new AddWrapper(op);
            Outlet operandAOutlet = wrapper.OperandA;
            Outlet operandBOutlet = wrapper.OperandB;

            if (operandAOutlet == null || operandBOutlet == null) return 0;

            double a = Calculate(operandAOutlet, time);
            double b = Calculate(operandBOutlet, time);
            return a + b;
        }

        private double CalculateAdder(Operator op, double time)
        {
            var wrapper = new AdderWrapper(op);

            IList<Outlet> operands = wrapper.Operands;

            double result = 0;

            for (int i = 0; i < operands.Count; i++)
            {
                Outlet operand = operands[i];

                if (operand != null)
                {
                    result += Calculate(operand, time);
                }
            }

            return result;
        }

        private double CalculateDivide(Operator op, double time)
        {
            var wrapper = new DivideWrapper(op);
            Outlet originOutlet = wrapper.Origin;
            Outlet numeratorOutlet = wrapper.Numerator;
            Outlet denominatorOutlet = wrapper.Denominator;

            // Without Origin
            if (originOutlet == null)
            {
                if (numeratorOutlet == null || denominatorOutlet == null) return 0;

                double denominator = Calculate(denominatorOutlet, time);

                if (denominator == 0) return 0;

                double numerator = Calculate(numeratorOutlet, time);

                return numerator / denominator;
            }

            // With Origin
            else
            {
                double origin = Calculate(originOutlet, time);

                if (numeratorOutlet == null || denominatorOutlet == null) return origin;

                double denominator = Calculate(denominatorOutlet, time);

                if (denominator == 0) return origin;

                double numerator = Calculate(wrapper.Numerator, time);

                return (numerator - origin) / denominator + origin;
            }
        }

        private double CalculateMultiply(Operator op, double time)
        {
            var wrapper = new MultiplyWrapper(op);
            Outlet originOutlet = wrapper.Origin;
            Outlet operandAOutlet = wrapper.OperandA;
            Outlet operandBOutlet = wrapper.OperandB;

            if (originOutlet == null)
            {
                if (operandAOutlet == null || operandBOutlet == null) return 0;

                double a = Calculate(operandAOutlet, time);
                double b = Calculate(operandBOutlet, time);
                return a * b;
            }
            else
            {
                double origin = Calculate(originOutlet, time);

                if (operandAOutlet == null || operandBOutlet == null) return origin;

                double a = Calculate(operandAOutlet, time);
                double b = Calculate(operandBOutlet, time);
                return (a - origin) * b + origin;
            }
        }

        private double CalculatePatchInlet(Operator op, double time)
        {
            var wrapper = new PatchInletWrapper(op);

            Outlet inputOutlet = wrapper.Input;

            if (inputOutlet == null) return 0;

            return Calculate(inputOutlet, time);
        }

        private double CalculatePatchOutlet(Operator op, double time)
        {
            var wrapper = new PatchOutletWrapper(op);

            Outlet inputOutlet = wrapper.Input;

            if (inputOutlet == null) return 0;

            return Calculate(inputOutlet, time);
        }

        private double CalculatePower(Operator op, double time)
        {
            var wrapper = new PowerWrapper(op);
            Outlet baseOutlet = wrapper.Base;
            Outlet exponentOutlet = wrapper.Exponent;

            if (baseOutlet == null || exponentOutlet == null) return 0;

            double @base = Calculate(baseOutlet, time);
            double exponent = Calculate(exponentOutlet, time);

            return Math.Pow(@base, exponent);
        }

        private double CalculateSine(Operator op, double time)
        {
            var wrapper = new SineWrapper(op);
            Outlet volumeOutlet = wrapper.Volume;
            Outlet pitchOutlet = wrapper.Volume;

            if (volumeOutlet == null || pitchOutlet == null) return 0;

            Outlet levelOutlet = wrapper.Level;
            Outlet phaseStartOutlet = wrapper.PhaseStart;

            double volume = Calculate(volumeOutlet, time);
            double pitch = Calculate(pitchOutlet, time);

            if (levelOutlet == null && phaseStartOutlet == null)
            {
                return volume * Math.Sin(2 * Math.PI * pitch * time);
            }

            double level = levelOutlet != null ? Calculate(levelOutlet, time) : 0;
            double phaseStart = levelOutlet != null ? Calculate(phaseStartOutlet, time) : 0;

            double result = level + volume * Math.Sin(2 * (Math.PI * phaseStart + Math.PI * pitch * time));
            return result;
        }

        private double CalculateSubstract(Operator op, double time)
        {
            var wrapper = new SubstractWrapper(op);
            Outlet operandAOutlet = wrapper.OperandA;
            Outlet operandBOutlet = wrapper.OperandB;

            if (operandAOutlet == null || operandBOutlet == null) return 0;

            double a = Calculate(operandAOutlet, time);
            double b = Calculate(operandBOutlet, time);

            return a - b;
        }

        private double CalculateTimeAdd(Operator op, double time)
        {
            var wrapper = new TimeAddWrapper(op);

            Outlet signalOutlet = wrapper.Signal;
            if (signalOutlet == null) return 0;

            Outlet timeDifferenceOutlet = wrapper.TimeDifference;
            if (timeDifferenceOutlet == null)
            {
                double result = Calculate(signalOutlet, time);
                return result;
            }

            // IMPORTANT: To add time to the output, you have substract time from the input.
            double timeDifference = Calculate(timeDifferenceOutlet, time);
            double transformedTime = time - timeDifference;
            double result2 = Calculate(signalOutlet, transformedTime);
            return result2;
        }

        private double CalculateTimeDivide(Operator op, double time)
        {
            var wrapper = new TimeDivideWrapper(op);

            // Determine origin
            Outlet originOutlet = wrapper.Origin;
            double origin = 0;
            if (originOutlet != null)
            {
                origin = Calculate(originOutlet, time);
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
                double result = Calculate(signalOutlet, time);
                return result;
            }

            // Time divider 0? Don't return infinity, but just pass through signal.
            double timeDivider = Calculate(timeDividerOutlet, time);
            if (timeDivider == 0)
            {
                double result = Calculate(signalOutlet, time);
                return result;
            }

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.

            // Formula without origin
            if (originOutlet == null)
            {
                double transformedTime = time * timeDivider;
                double result = Calculate(signalOutlet, transformedTime);
                return result;
            }

            // Formula with origin
            else
            {
                double transformedTime = (time - origin) * timeDivider + origin;
                double result = Calculate(signalOutlet, transformedTime);
                return result;
            }
        }

        private double CalculateTimeMultiply(Operator op, double time)
        {
            var wrapper = new TimeMultiplyWrapper(op);

            // Determine origin
            Outlet originOutlet = wrapper.Origin;
            double origin = 0;
            if (originOutlet != null)
            {
                origin = Calculate(originOutlet, time);
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
                double result = Calculate(signalOutlet, time);
                return result;
            }

            // Time multiplier 0? See that as multiplier = 1 or rather: just pass through signal.
            double timeMultiplier = Calculate(timeMultiplierOutlet, time);
            if (timeMultiplier == 0)
            {
                double result = Calculate(signalOutlet, time);
                return result;
            }

            // IMPORTANT: To multiply the time in the output, you have to divide the time of the input.

            // Formula without origin
            if (originOutlet == null)
            {
                double transformedTime = time / timeMultiplier;
                double result = Calculate(signalOutlet, transformedTime);
                return result;
            }

            // Formula with origin
            else
            {
                double transformedTime = (time - origin) / timeMultiplier + origin;
                double result = Calculate(signalOutlet, transformedTime);
                return result;
            }
        }

        private double CalculateTimePower(Operator op, double time)
        {
            var wrapper = new TimePowerWrapper(op);

            Outlet signalOutlet = wrapper.Signal;
            if (signalOutlet == null)
            {
                return 0;
            }

            Outlet exponentOutlet = wrapper.Exponent;
            if (exponentOutlet == null)
            {
                return Calculate(signalOutlet, time);
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

                double exponent = Calculate(exponentOutlet, time);

                double transformedTime = timeSign * Math.Pow(timeAbs, 1 / exponent);

                double result = Calculate(signalOutlet, transformedTime);
                return result;
            }
            else
            {
                double origin = Calculate(originOutlet, time);

                double timeAbs = System.Math.Abs(time - origin);
                double timeSign = System.Math.Sign(time - origin);

                double exponent = Calculate(exponentOutlet, time);

                double transformedTime = timeSign * Math.Pow(timeAbs, 1 / exponent) + origin;

                double result = Calculate(signalOutlet, transformedTime);
                return result;
            }
        }

        private double CalculateTimeSubstract(Operator op, double time)
        {
            var wrapper = new TimeSubstractWrapper(op);

            Outlet signalOutlet = wrapper.Signal;
            if (signalOutlet == null) return 0;

            Outlet timeDifferenceOutlet = wrapper.TimeDifference;
            if (timeDifferenceOutlet == null)
            {
                double result = Calculate(signalOutlet, time);
                return result;
            }

            // IMPORTANT: To substract time from the output, you have add time to the input.
            double timeDifference = Calculate(timeDifferenceOutlet, time);
            double transformedTime = time + timeDifference;
            double result2 = Calculate(signalOutlet, transformedTime);
            return result2;
        }

        private double CalculateValueOperator(Operator op, double time)
        {
            var wrapper = new ValueOperatorWrapper(op);

            return wrapper.Value;
        }

        private double CalculateCurveIn(Operator op, double time)
        {
            if (op.AsCurveIn.Curve == null) return 0;

            Curve curve = op.AsCurveIn.Curve;

            var curveCalculator = new CurveCalculator(curve);
            double result = curveCalculator.CalculateValue(time);
            return result;
        }

        private double CalculateSampleOperator(Operator op, double time)
        {
            Sample sample = op.AsSampleOperator.Sample;

            if (sample == null) return 0;

            ISampleCalculator sampleCalculator;
            if (!_sampleCalculatorDictionary.TryGetValue(sample.ID, out sampleCalculator))
            {
                sampleCalculator = SampleCalculatorFactory.CreateSampleCalculator(sample);
                _sampleCalculatorDictionary.Add(sample.ID, sampleCalculator);
            }

            // This is a solution for when the sample channels do not match the channel we want.
            // But this is only a fallback that will work for mono and stereo,
            // and not for e.g. 5.1 surround sound.
            int channelIndex;
            if (sampleCalculator.ChannelCount < _channelIndex)
            {
                channelIndex = 0;
            }
            else
            {
                channelIndex = _channelIndex;
            }

            double result = sampleCalculator.CalculateValue(time, channelIndex);
            return result;
        }
    }
}
