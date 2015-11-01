using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Calculation.Patches
{
    internal class InterpretedPatchCalculator : IPatchCalculator
    {
        // The InterpretedPatchCalculator may not have the exact same behavior as the OptimizedPatchCalculator,
        // because it has not been used lately (2015-07-30) and it has not been maintained very much.

        private readonly ICurveRepository _curveRepository;
        private readonly ISampleRepository _sampleRepository;
        private readonly IDocumentRepository _documentRepository;

        private WhiteNoiseCalculator _whiteNoiseCalculator;

        /// <summary> Is set in the Calculate method and used in other methods. </summary>
        private int _channelIndex;
        private Outlet[] _channelOutlets;
        private Dictionary<OperatorTypeEnum, Func<Outlet, double, double>> _funcDictionary;

        private Stack<Outlet> _outletStack = new Stack<Outlet>();
        private Stack<int> _bundleIndexStack = new Stack<int>();

        // TODO: The _previousTimeDictionary and _phaseDictionary have not been well debugged yet.
        // The effect on the output sound should be inspected: if the phase is reused at the right moments,
        // if the previous time is remembered independently where needed.

        /// <summary> Key is a composite string with the path of operator ID's in it. </summary>
        private Dictionary<string, double> _previousTimeDictionary = new Dictionary<string, double>();
        /// <summary> Key is a composite string with the path of operator ID's in it. </summary>
        private Dictionary<string, double> _phaseDictionary = new Dictionary<string, double>();

        private Dictionary<Operator, Curve> _curveOperator_Curve_Dictionary = new Dictionary<Operator, Curve>();
        /// <summary> Value can be null of Curve Operator's Curve is not set. </summary>
        private Dictionary<Operator, CurveCalculator> _operator_curveCalculatorDictionary = new Dictionary<Operator, CurveCalculator>();
        private Dictionary<Operator, double> _numberOperatorValueDictionary = new Dictionary<Operator, double>();
        private Dictionary<Operator, SampleInfo> _sampleOperator_SampleInfo_Dictionary = new Dictionary<Operator, SampleInfo>();
        private Dictionary<int, ISampleCalculator> _sampleID_sampleCalculatorDictionary = new Dictionary<int, ISampleCalculator>();
        /// <summary> Key is operator ID, value is offset in seconds. </summary>
        private Dictionary<int, double> _operatorID_whiteNoiseOffsetDictionary = new Dictionary<int, double>();

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

            _funcDictionary = new Dictionary<OperatorTypeEnum, Func<Outlet, double, double>>
            {
                { OperatorTypeEnum.Add, CalculateAdd },
                { OperatorTypeEnum.Adder, CalculateAdder },
                { OperatorTypeEnum.Bundle, CalculateBundle },
                { OperatorTypeEnum.Curve, CalculateCurveOperator },
                { OperatorTypeEnum.CustomOperator, CalculateCustomOperator },
                { OperatorTypeEnum.Divide, CalculateDivide },
                { OperatorTypeEnum.Exponent, CalculateExponent },
                { OperatorTypeEnum.Loop, CalculateLoop },
                { OperatorTypeEnum.Multiply, CalculateMultiply },
                { OperatorTypeEnum.PatchInlet, CalculatePatchInlet },
                { OperatorTypeEnum.PatchOutlet, CalculatePatchOutlet },
                { OperatorTypeEnum.Power, CalculatePower },
                { OperatorTypeEnum.Sample, CalculateSampleOperator },
                { OperatorTypeEnum.SawTooth, CalculateSawTooth },
                { OperatorTypeEnum.Select, CalculateSelect },
                { OperatorTypeEnum.Sine, CalculateSine },
                { OperatorTypeEnum.SquareWave, CalculateSquareWave },
                { OperatorTypeEnum.Substract, CalculateSubstract },
                { OperatorTypeEnum.Delay, CalculateDelay },
                { OperatorTypeEnum.SpeedUp, CalculateSpeedUp },
                { OperatorTypeEnum.SlowDown, CalculateSlowDown },
                { OperatorTypeEnum.TimePower, CalculateTimePower },
                { OperatorTypeEnum.TimeSubstract, CalculateTimeSubstract },
                { OperatorTypeEnum.TriangleWave, CalculateTriangleWave },
                { OperatorTypeEnum.Number, CalculateNumberOperator },
                { OperatorTypeEnum.Unbundle, CalculateUnbundle },
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
            _outletStack.Push(outlet);

            OperatorTypeEnum operatorTypeEnum = outlet.Operator.GetOperatorTypeEnum();

            // Resample operator is missing from interpreted mode too.
            Func<Outlet, double, double> func;
            if (!_funcDictionary.TryGetValue(operatorTypeEnum, out func))
            {
                throw new ValueNotSupportedException(operatorTypeEnum);
            }
            double value = func(outlet, time);

            _outletStack.Pop();

            return value;
        }

        private double CalculateAdd(Outlet outlet, double time)
        {
            Operator op = outlet.Operator;

            var wrapper = new OperatorWrapper_Add(op);

            Outlet operandAOutlet = wrapper.OperandA;
            Outlet operandBOutlet = wrapper.OperandB;

            if (operandAOutlet == null || operandBOutlet == null) return 0;

            double a = Calculate(operandAOutlet, time);
            double b = Calculate(operandBOutlet, time);
            return a + b;
        }

        private double CalculateAdder(Outlet outlet, double time)
        {
            Operator op = outlet.Operator;

            var wrapper = new OperatorWrapper_Adder(op);

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

        private double CalculateBundle(Outlet outlet, double time)
        {
            if (_bundleIndexStack.Count == 0)
            {
                throw new NotSupportedException(String.Format("Bundle Operator with ID '{0}' encountered without first encountering an Unbundle Operator. This is not yet supported.", outlet.Operator.ID));
            }

            int bundleIndex = _bundleIndexStack.Pop();

            if (bundleIndex >= outlet.Operator.Inlets.Count)
            {
                throw new Exception(String.Format("Index '{0}' does not exist in Bundle Operator with ID '{0}'.", bundleIndex, outlet.Operator.ID));
            }

            Inlet inlet = outlet.Operator.Inlets[bundleIndex];
            if (inlet.InputOutlet == null)
            {
                return 0.0;
            }

            double value = Calculate(inlet.InputOutlet, time);

            _bundleIndexStack.Push(bundleIndex);

            return value;
        }

        private double CalculateCurveOperator(Outlet outlet, double time)
        {
            Operator op = outlet.Operator;

            CurveCalculator curveCalculator;
            if (!_operator_curveCalculatorDictionary.TryGetValue(op, out curveCalculator))
            {
                var wrapper = new OperatorWrapper_Curve(op, _curveRepository);
                Curve curve = wrapper.Curve;

                if (curve != null)
                {
                    curveCalculator = new CurveCalculator(curve);
                }

                _operator_curveCalculatorDictionary.Add(op, curveCalculator);
            }

            if (curveCalculator == null)
            {
                return 0;
            }

            double result = curveCalculator.CalculateValue(time);
            return result;
        }

        private double CalculateCustomOperator(Outlet customOperatorOutlet, double time)
        {
            Outlet outlet = PatchCalculationHelper.TryApplyCustomOperatorToUnderlyingPatch(customOperatorOutlet, _documentRepository);

            if (outlet == null)
            {
                return 0.0;
            }

            double result = Calculate(outlet, time);
            return result;
        }

        private double CalculateDelay(Outlet outlet, double time)
        {
            Operator op = outlet.Operator;

            var wrapper = new OperatorWrapper_Delay(op);

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

        private double CalculateDivide(Outlet outlet, double time)
        {
            Operator op = outlet.Operator;

            var wrapper = new OperatorWrapper_Divide(op);

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

        private double CalculateExponent(Outlet outlet, double time)
        {
            Operator op = outlet.Operator;

            var wrapper = new OperatorWrapper_Exponent(op);

            Outlet lowOutlet = wrapper.Low;
            Outlet highOutlet = wrapper.High;
            Outlet ratioOutlet = wrapper.Ratio;

            if (lowOutlet == null)
            {
                return 0.0;
            }

            if (highOutlet == null)
            {
                return Calculate(lowOutlet, time);
            }

            if (ratioOutlet == null)
            {
                return Calculate(lowOutlet, time);
            }

            double low = Calculate(lowOutlet, time);
            double high = Calculate(highOutlet, time);
            double ratio = Calculate(ratioOutlet, time);

            if (low == 0.0)
            {
                // Prevent NaN out of division by 0.
                return 0.0;
            }

            double value = low * Math.Pow(high / low, ratio);
            return value;
        }

        private double CalculateLoop(Outlet outlet, double time)
        {
            Operator op = outlet.Operator;

            var wrapper = new OperatorWrapper_Loop(op);

            double outputTime = time;
            double inputAttack = Calculate(wrapper.Attack, outputTime);
            double inputTime = time + inputAttack;

            bool isBeforeAttack = inputTime < inputAttack;
            if (isBeforeAttack)
            {
                return 0;
            }

            double inputStart = Calculate(wrapper.Start, outputTime);
            bool isInAttack = inputTime < inputStart;
            if (isInAttack)
            {
                double value =  Calculate(wrapper.Signal, inputTime);
                return value;
            }

            double inputEnd = Calculate(wrapper.End, outputTime);
            double outputSustain = Calculate(wrapper.Sustain, outputTime);
            double outputEnd = inputStart - inputAttack + outputSustain;
            bool isInLoop = outputTime < outputEnd;
            if (isInLoop)
            {
                double inputSustain = inputEnd - inputStart;
                double positionInCycle = (inputTime - inputStart) % inputSustain;
                inputTime = inputStart + positionInCycle;
                double value = Calculate(wrapper.Signal, inputTime);
                return value;
            }

            double inputRelease = Calculate(wrapper.Release, outputTime);
            double releaseDuration = inputRelease - inputEnd;
            double outputRelease = outputEnd + releaseDuration;
            bool isInRelease = outputTime < outputRelease;
            if (isInRelease)
            {
                double positionInRelease = outputTime - outputEnd;
                inputTime = inputEnd + positionInRelease;
                double value = Calculate(wrapper.Signal, inputTime);
                return value;
            }

            // IsAfterRelease
            return 0;
        }

        private double CalculateMultiply(Outlet outlet, double time)
        {
            Operator op = outlet.Operator;

            var wrapper = new OperatorWrapper_Multiply(op);

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

        private double CalculateNumberOperator(Outlet outlet, double time)
        {
            Operator op = outlet.Operator;

            double value;
            if (!_numberOperatorValueDictionary.TryGetValue(op, out value))
            {
                var wrapper = new OperatorWrapper_Number(op);
                value = wrapper.Number;
                _numberOperatorValueDictionary.Add(op, value);
            }
            return value;
        }

        private double CalculatePower(Outlet outlet, double time)
        {
            Operator op = outlet.Operator;

            var wrapper = new OperatorWrapper_Power(op);

            Outlet baseOutlet = wrapper.Base;
            Outlet exponentOutlet = wrapper.Exponent;

            if (baseOutlet == null || exponentOutlet == null) return 0;

            double @base = Calculate(baseOutlet, time);
            double exponent = Calculate(exponentOutlet, time);

            return Math.Pow(@base, exponent);
        }

        private double CalculatePatchInlet(Outlet outlet, double time)
        {
            Operator op = outlet.Operator;

            var wrapper = new OperatorWrapper_PatchInlet(op);

            Outlet inputOutlet = wrapper.Input;

            if (inputOutlet == null) return 0;

            return Calculate(inputOutlet, time);
        }

        private double CalculatePatchOutlet(Outlet outlet, double time)
        {
            Operator op = outlet.Operator;

            var wrapper = new OperatorWrapper_PatchOutlet(op);

            Outlet inputOutlet = wrapper.Input;

            if (inputOutlet == null) return 0;

            return Calculate(inputOutlet, time);
        }

        private double CalculateSlowDown(Outlet outlet, double time)
        {
            Operator op = outlet.Operator;

            var wrapper = new OperatorWrapper_SlowDown(op);

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

        private double CalculateSubstract(Outlet outlet, double time)
        {
            Operator op = outlet.Operator;

            var wrapper = new OperatorWrapper_Substract(op);

            Outlet operandAOutlet = wrapper.OperandA;
            Outlet operandBOutlet = wrapper.OperandB;

            if (operandAOutlet == null || operandBOutlet == null) return 0;

            double a = Calculate(operandAOutlet, time);
            double b = Calculate(operandBOutlet, time);

            return a - b;
        }

        private double CalculateSpeedUp(Outlet outlet, double time)
        {
            Operator op = outlet.Operator;

            var wrapper = new OperatorWrapper_SpeedUp(op);

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

        private double CalculateSawTooth(Outlet outlet, double time)
        {
            Operator op = outlet.Operator;

            var wrapper = new OperatorWrapper_SawTooth(op);

            Outlet phaseShiftOutlet = wrapper.PhaseShift;
            Outlet pitchOutlet = wrapper.Pitch;

            double pitch = 0;
            double phaseShift = 0;

            if (pitchOutlet != null)
            {
                pitch = Calculate(pitchOutlet, time);
            }

            if (phaseShiftOutlet != null)
            {
                phaseShift = Calculate(phaseShiftOutlet, time);
            }

            // Get phase variables
            string key = GetOutletPathKey();
            double previousTime = 0;
            _previousTimeDictionary.TryGetValue(key, out previousTime);
            double phase = 0;
            _phaseDictionary.TryGetValue(key, out phase);

            // Calculate new phase
            double dt = time - previousTime;
            phase = phase + dt * pitch;

            // Calculate value
            double shiftedPhase = phase + phaseShift;
            double value = -1 + (2 * shiftedPhase % 2);

            // Store phase variables
            _phaseDictionary[key] = phase;
            _previousTimeDictionary[key] = time;

            return value;
        }

        private double CalculateSelect(Outlet outlet, double time)
        {
            Operator op = outlet.Operator;

            var wrapper = new OperatorWrapper_Select(op);

            double time2  = Calculate(wrapper.Time, time);

            double value = Calculate(wrapper.Signal, time2);
            return value;
        }

        private double CalculateSquareWave(Outlet outlet, double time)
        {
            Operator op = outlet.Operator;

            var wrapper = new OperatorWrapper_SquareWave(op);

            Outlet phaseShiftOutlet = wrapper.PhaseShift;
            Outlet pitchOutlet = wrapper.Pitch;

            double pitch = 0;
            double phaseShift = 0;

            if (pitchOutlet != null)
            {
                pitch = Calculate(pitchOutlet, time);
            }

            if (phaseShiftOutlet != null)
            {
                phaseShift = Calculate(phaseShiftOutlet, time);
            }

            // Get phase variables
            string key = GetOutletPathKey();
            double previousTime = 0;
            _previousTimeDictionary.TryGetValue(key, out previousTime);
            double phase = 0;
            _phaseDictionary.TryGetValue(key, out phase);

            // Calculate new phase
            double dt = time - previousTime;
            phase = phase + dt * pitch;

            // Calculate value
            double value;
            double shiftedPhase = phase + phaseShift;
            double relativePhase = shiftedPhase % 1;
            if (relativePhase < 0.5)
            {
                value = -1;
            }
            else
            {
                value = 1;
            }

            // Store phase variables
            _phaseDictionary[key] = phase;
            _previousTimeDictionary[key] = time;

            return value;
        }

        private double CalculateSine(Outlet outlet, double time)
        {
            Operator op = outlet.Operator;

            var wrapper = new OperatorWrapper_Sine(op);

            Outlet volumeOutlet = wrapper.Volume;
            Outlet pitchOutlet = wrapper.Pitch;
            Outlet originOutlet = wrapper.Origin;

            if (volumeOutlet == null || pitchOutlet == null)
            {
                if (originOutlet != null)
                {
                    return Calculate(originOutlet, time);
                }
                else
                {
                    return 0;
                }
            }

            Outlet phaseShiftOutlet = wrapper.PhaseShift;

            double volume = Calculate(volumeOutlet, time);
            double pitch = Calculate(pitchOutlet, time);

            if (originOutlet == null && phaseShiftOutlet == null)
            {
                return volume * Math.Sin(2 * Math.PI * pitch * time);
            }

            double origin = originOutlet != null ? Calculate(originOutlet, time) : 0;
            double phaseShift = phaseShiftOutlet != null ? Calculate(phaseShiftOutlet, time) : 0;

            double result = origin + volume * Math.Sin(2 * (Math.PI * phaseShift + Math.PI * pitch * time));
            return result;
        }

        private double CalculateSampleOperator(Outlet outlet, double time)
        {
            Operator op = outlet.Operator;

            SampleInfo sampleInfo;
            if (!_sampleOperator_SampleInfo_Dictionary.TryGetValue(op, out sampleInfo))
            {
                var wrapper = new OperatorWrapper_Sample(op, _sampleRepository);
                sampleInfo = wrapper.SampleInfo;
                _sampleOperator_SampleInfo_Dictionary.Add(op, sampleInfo);
            }

            if (sampleInfo.Sample == null) return 0;

            ISampleCalculator sampleCalculator;
            if (!_sampleID_sampleCalculatorDictionary.TryGetValue(sampleInfo.Sample.ID, out sampleCalculator))
            {
                sampleCalculator = SampleCalculatorFactory.CreateSampleCalculator(sampleInfo.Sample, sampleInfo.Bytes);
                _sampleID_sampleCalculatorDictionary.Add(sampleInfo.Sample.ID, sampleCalculator);
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

        private double CalculateTimeSubstract(Outlet outlet, double time)
        {
            Operator op = outlet.Operator;

            var wrapper = new OperatorWrapper_TimeSubstract(op);

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

        private double CalculateTimePower(Outlet outlet, double time)
        {
            Operator op = outlet.Operator;

            var wrapper = new OperatorWrapper_TimePower(op);

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

        private double CalculateTriangleWave(Outlet outlet, double time)
        {
            Operator op = outlet.Operator;

            var wrapper = new OperatorWrapper_TriangleWave(op);

            Outlet phaseShiftOutlet = wrapper.PhaseShift;
            Outlet pitchOutlet = wrapper.Pitch;

            double pitch = 0;
            double phaseShift = 0;

            if (pitchOutlet != null)
            {
                pitch = Calculate(pitchOutlet, time);
            }

            if (phaseShiftOutlet != null)
            {
                phaseShift = Calculate(phaseShiftOutlet, time);
            }

            // Get phase variables
            string key = GetOutletPathKey();
            double previousTime = 0;
            _previousTimeDictionary.TryGetValue(key, out previousTime);
            double phase = 0;
            _phaseDictionary.TryGetValue(key, out phase);

            // Calculate new phase
            double dt = time - previousTime;
            phase = phase + dt * pitch;

            // Calculate value
            double shiftedPhase = phase + phaseShift;
            // Correct the phase, because our calculation starts with value -1, but in practice you want to start at value 0 going up.
            shiftedPhase += 0.25;
            double relativePhase = shiftedPhase % 1.0;
            double value;
            if (relativePhase < 0.5)
            {
                // Starts going up at a rate of 2 up over 1/2 a cycle.
                value = -1.0 + 4.0 * relativePhase;
            }
            else
            {
                // And then going down at phase 1/2.
                // (Extending the line to x = 0 it ends up at y = 3.)
                value = 3.0 - 4.0 * relativePhase;
            }

            // Store phase variables
            _phaseDictionary[key] = phase;
            _previousTimeDictionary[key] = time;

            return value;
        }

        private double CalculateUnbundle(Outlet outlet, double time)
        {
            Operator op = outlet.Operator;
            Inlet inlet = op.Inlets.Single();
            Outlet inputOutlet = inlet.InputOutlet;
            if (inputOutlet == null)
            {
                return 0.0;
            }

            int outletIndex = outlet.Operator.Outlets.IndexOf(outlet);

            _bundleIndexStack.Push(outletIndex);

            double value = Calculate(inlet.InputOutlet, time);

            _bundleIndexStack.Pop();

            return value;
        }

        private double CalculateWhiteNoise(Outlet outlet, double time)
        {
            Operator op = outlet.Operator;

            double offset;
            if (!_operatorID_whiteNoiseOffsetDictionary.TryGetValue(op.ID, out offset))
            {
                offset = _whiteNoiseCalculator.GetRandomOffset();
                _operatorID_whiteNoiseOffsetDictionary.Add(op.ID, offset);
            }

            double value = _whiteNoiseCalculator.GetValue(time + offset);

            return value;
        }

        // Helpers

        private string GetOutletPathKey()
        {
            string key = String.Join("|", _outletStack.Select(x => x.ID));
            return key;
        }
    }
}
