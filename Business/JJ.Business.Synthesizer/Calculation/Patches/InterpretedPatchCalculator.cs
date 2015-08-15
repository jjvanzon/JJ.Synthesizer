using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Business.Synthesizer.Calculation.Patches
{
    internal class InterpretedPatchCalculator : IPatchCalculator
    {
        // The InterpretedPatchCalculator may not have the exact same behavior as the OptimizedPatchCalculator,
        // because it has not been used lately (2015-07-30) and it has not been maintained very much.

        private ICurveRepository _curveRepository;
        private ISampleRepository _sampleRepository;
        private IDocumentRepository _documentRepository;

        private WhiteNoiseCalculator _whiteNoiseCalculator;

        /// <summary>
        /// Is set in the Calculate method
        /// and used in other methods.
        /// </summary>
        private int _channelIndex;
        private Outlet[] _channelOutlets;
        private Dictionary<OperatorTypeEnum, Func<Operator, double, double>> _funcDictionary;

        public InterpretedPatchCalculator(
            IList<Outlet> channelOutlets,
            WhiteNoiseCalculator whiteNoiseCalculator,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            IDocumentRepository documentRepository)
        {
            if (channelOutlets == null) throw new NullException(() => channelOutlets);
            if (whiteNoiseCalculator == null) throw new NullException(() => whiteNoiseCalculator);
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _curveRepository = curveRepository;
            _sampleRepository = sampleRepository;
            _documentRepository = documentRepository;
            _whiteNoiseCalculator = whiteNoiseCalculator;

            foreach (Outlet channelOutlet in channelOutlets)
            {
                IValidator validator = new OperatorValidator_Recursive(
                    channelOutlet.Operator,
                    _curveRepository,
                    _sampleRepository,
                    _documentRepository,
                    alreadyDone: new HashSet<object>());

                validator.Verify();
            }

            _channelOutlets = channelOutlets.ToArray();

            _funcDictionary = new Dictionary<OperatorTypeEnum, Func<Operator, double, double>>
            {
                { OperatorTypeEnum.Add, CalculateAdd },
                { OperatorTypeEnum.Adder, CalculateAdder },
                { OperatorTypeEnum.CurveIn, CalculateCurveIn },
                { OperatorTypeEnum.Divide, CalculateDivide },
                { OperatorTypeEnum.Multiply, CalculateMultiply },
                { OperatorTypeEnum.PatchInlet, CalculatePatchInlet },
                { OperatorTypeEnum.PatchOutlet, CalculatePatchOutlet },
                { OperatorTypeEnum.Power, CalculatePower },
                { OperatorTypeEnum.Sample, CalculateSampleOperator },
                { OperatorTypeEnum.Sine, CalculateSine },
                { OperatorTypeEnum.Substract, CalculateSubstract },
                { OperatorTypeEnum.TimeAdd, CalculateTimeAdd },
                { OperatorTypeEnum.TimeDivide, CalculateTimeDivide },
                { OperatorTypeEnum.TimeMultiply, CalculateTimeMultiply },
                { OperatorTypeEnum.TimePower, CalculateTimePower },
                { OperatorTypeEnum.TimeSubstract, CalculateTimeSubstract },
                { OperatorTypeEnum.Value, CalculateValueOperator },
                { OperatorTypeEnum.WhiteNoise, CalculateWhiteNoise },
            };
        }

        public double Calculate(double time, int channelIndex)
        {
            _channelIndex = channelIndex;
            return Calculate(_channelOutlets[_channelIndex], time);
        }

        private double Calculate(Outlet outlet, double time)
        {
            OperatorTypeEnum operatorTypeEnum = outlet.Operator.GetOperatorTypeEnum();

            if (operatorTypeEnum == OperatorTypeEnum.CustomOperator)
            {
                double value = CalculateCustomOperator(outlet, time);
                return value;
            }
            else
            {
                // Only the CustomOperator is missing from the _funcDictionary.
                // The _funcDictionary will only work for operators with one outlet.

                // Oh: plus Resample operator is missing from interpreted mode too.
                Func<Operator, double, double> func = _funcDictionary[operatorTypeEnum];
                double value = func(outlet.Operator, time);
                return value;
            }

            // TODO: Low priority:
            // Make the all the Calculate methods in the _funcDictionary take Outlet instead of Operator,
            // so they could all work with multiple outlets if needed.
        }

        private Dictionary<Operator, double> _valueOperatorValueDictionary = new Dictionary<Operator, double>();

        private double CalculateValueOperator(Operator op, double time)
        {
            double value;
            if (!_valueOperatorValueDictionary.TryGetValue(op, out value))
            {
                var wrapper = new Value_OperatorWrapper(op);
                value = wrapper.Value;
                _valueOperatorValueDictionary.Add(op, value);
            }
            return value;
        }

        private double CalculateAdd(Operator op, double time)
        {
            var wrapper = new Add_OperatorWrapper(op);

            Outlet operandAOutlet = wrapper.OperandA;
            Outlet operandBOutlet = wrapper.OperandB;

            if (operandAOutlet == null || operandBOutlet == null) return 0;

            double a = Calculate(operandAOutlet, time);
            double b = Calculate(operandBOutlet, time);
            return a + b;
        }

        private double CalculateSubstract(Operator op, double time)
        {
            var wrapper = new Substract_OperatorWrapper(op);

            Outlet operandAOutlet = wrapper.OperandA;
            Outlet operandBOutlet = wrapper.OperandB;

            if (operandAOutlet == null || operandBOutlet == null) return 0;

            double a = Calculate(operandAOutlet, time);
            double b = Calculate(operandBOutlet, time);

            return a - b;
        }

        private double CalculateMultiply(Operator op, double time)
        {
            var wrapper = new Multiply_OperatorWrapper(op);

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

        private double CalculateDivide(Operator op, double time)
        {
            var wrapper = new Divide_OperatorWrapper(op);

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

                double numerator = Calculate(numeratorOutlet, time);

                return (numerator - origin) / denominator + origin;
            }
        }

        private double CalculatePower(Operator op, double time)
        {
            var wrapper = new Power_OperatorWrapper(op);

            Outlet baseOutlet = wrapper.Base;
            Outlet exponentOutlet = wrapper.Exponent;

            if (baseOutlet == null || exponentOutlet == null) return 0;

            double @base = Calculate(baseOutlet, time);
            double exponent = Calculate(exponentOutlet, time);

            return Math.Pow(@base, exponent);
        }

        private double CalculateTimeAdd(Operator op, double time)
        {
            var wrapper = new TimeAdd_OperatorWrapper(op);

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

        private double CalculateTimeSubstract(Operator op, double time)
        {
            var wrapper = new TimeSubstract_OperatorWrapper(op);

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

        private double CalculateTimeMultiply(Operator op, double time)
        {
            var wrapper = new TimeMultiply_OperatorWrapper(op);

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
                // TODO: This seesm useless. Origin is a time variable, while we have to return an x.
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

        private double CalculateTimeDivide(Operator op, double time)
        {
            var wrapper = new TimeDivide_OperatorWrapper(op);

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

        private double CalculateTimePower(Operator op, double time)
        {
            var wrapper = new TimePower_OperatorWrapper(op);

            Outlet signalOutlet = wrapper.Signal;
            Outlet exponentOutlet = wrapper.Exponent;
            Outlet originOutlet = wrapper.Origin;

            if (signalOutlet == null)
            {
                return 0;
            }

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

        private double CalculateAdder(Operator op, double time)
        {
            var wrapper = new Adder_OperatorWrapper(op);

            Outlet[] operands = wrapper.Operands.ToArray();

            double result = 0;

            for (int i = 0; i < operands.Length; i++)
            {
                Outlet operand = operands[i];

                if (operand != null)
                {
                    result += Calculate(operand, time);
                }
            }

            return result;
        }

        private double CalculateSine(Operator op, double time)
        {
            var wrapper = new Sine_OperatorWrapper(op);

            Outlet volumeOutlet = wrapper.Volume;
            Outlet pitchOutlet = wrapper.Pitch;

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

        private Dictionary<Operator, Curve> _curveIn_Curve_Dictionary = new Dictionary<Operator, Curve>();

        private double CalculateCurveIn(Operator op, double time)
        {
            Curve curve;
            if (!_curveIn_Curve_Dictionary.TryGetValue(op, out curve))
            {
                var wrapper = new CurveIn_OperatorWrapper(op, _curveRepository);
                curve = wrapper.Curve;
                _curveIn_Curve_Dictionary.Add(op, curve);
            }

            if (curve == null) return 0;

            // TODO: Cache CurveCalculators?
            var curveCalculator = new CurveCalculator(curve);
            double result = curveCalculator.CalculateValue(time);
            return result;
        }

        private Dictionary<Operator, Sample> _sampleOperator_Sample_Dictionary = new Dictionary<Operator, Sample>();
        private Dictionary<int, ISampleCalculator> _sampleCalculatorDictionary =
            new Dictionary<int, ISampleCalculator>();

        private double CalculateSampleOperator(Operator op, double time)
        {
            Sample sample;
            if (!_sampleOperator_Sample_Dictionary.TryGetValue(op, out sample))
            {
                var wrapper = new Sample_OperatorWrapper(op, _sampleRepository);
                sample = wrapper.Sample;
                _sampleOperator_Sample_Dictionary.Add(op, sample);
            }

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

        private double CalculatePatchInlet(Operator op, double time)
        {
            var wrapper = new PatchInlet_OperatorWrapper(op);

            Outlet inputOutlet = wrapper.Input;

            if (inputOutlet == null) return 0;

            return Calculate(inputOutlet, time);
        }

        private double CalculatePatchOutlet(Operator op, double time)
        {
            var wrapper = new PatchOutlet_OperatorWrapper(op);

            Outlet inputOutlet = wrapper.Input;

            if (inputOutlet == null) return 0;

            return Calculate(inputOutlet, time);
        }

        /// <summary>
        /// Key is operator ID, value is offset in seconds.
        /// </summary>
        private Dictionary<int, double> _whiteNoiseOffsetDictionary = new Dictionary<int, double>();

        private double CalculateWhiteNoise(Operator op, double time)
        {
            double offset;
            if (!_whiteNoiseOffsetDictionary.TryGetValue(op.ID, out offset))
            {
                offset = _whiteNoiseCalculator.GetRandomOffset();
                _whiteNoiseOffsetDictionary.Add(op.ID, offset);
            }

            double value = _whiteNoiseCalculator.GetValue(time + offset);

            return value;
        }

        private double CalculateCustomOperator(Outlet outlet, double time)
        {
            var customOperatorWrapper = new Custom_OperatorWrapper(outlet.Operator, _documentRepository);
            Document document = customOperatorWrapper.Document;

            if (document == null)
            {
                return 0.0;
            }

            if (document.MainPatch == null)
            {
                return 0.0;
            }

            // Cross reference custom operator's inlets with the Document MainPatch's PatchInlets.
            var tuples = from operatorInlet in outlet.Operator.Inlets
                         join patchInlet in document.MainPatch.Operators.Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.PatchInlet)
                         on operatorInlet.Name equals patchInlet.Name
                         select new { OperatorInlet = operatorInlet, PatchInlet = patchInlet };

            // Each custom operator's inlet has an input outlet. 
            // This input outlet should be assigned as the corresponding input outlet
            // of the Document MainPatch's PatchInlet.
            foreach (var tuple in tuples)
            {
                Operator patchInlet = tuple.PatchInlet;
                Inlet operatorInlet = tuple.OperatorInlet;

                var patchInletWrapper = new PatchInlet_OperatorWrapper(patchInlet);
                patchInletWrapper.Input = operatorInlet.InputOutlet;
            }

            // Use the (custom operator's) outlet name and look it up in the Document MainPatch's outlets.
            Operator patchOutlet = document.MainPatch.Operators.Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.PatchOutlet &&
                                                                           x.Name == outlet.Name).Single();

            // Return the result of that Document MainPatch's outlet.
            var patchOutletWrapper = new PatchOutlet_OperatorWrapper(patchOutlet);
            double result = Calculate(patchOutletWrapper.Result, time);
            return result;
        }
    }
}
