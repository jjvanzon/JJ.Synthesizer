//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Framework.Reflection.Exceptions;
//using JJ.Framework.Validation;
//using JJ.Data.Synthesizer;
//using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
//using JJ.Business.Synthesizer.Calculation.Samples;
//using JJ.Business.Synthesizer.EntityWrappers;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Business.Synthesizer.Validation;
//using JJ.Framework.Common;
//using JJ.Business.Synthesizer.Calculation.Curves;
//using JJ.Framework.Mathematics;

//namespace JJ.Business.Synthesizer.Calculation.Patches
//{
//    internal class InterpretedPatchCalculator : IPatchCalculator
//    {
//        // The InterpretedPatchCalculator may not have the exact same behavior as the OptimizedPatchCalculator,
//        // because it is used much less and is not maintained as well.

//        // TODO: Implement SetValue methods.

//        private const double SAMPLE_BASE_FREQUENCY = 440.0;

//        private readonly ICurveRepository _curveRepository;
//        private readonly ISampleRepository _sampleRepository;
//        private readonly IPatchRepository _patchRepository;

//        private readonly WhiteNoiseCalculator _whiteNoiseCalculator;

//        /// <summary> Is set in the Calculate method and used in other methods. </summary>
//        private int _channelIndex;

//        /// <summary> Can contain nulls. </summary>
//        private readonly Outlet[] _channelOutlets;
//        private readonly Dictionary<OperatorTypeEnum, Func<Outlet, double, double>> _funcDictionary;

//        private readonly Stack<Outlet> _outletStack = new Stack<Outlet>();
//        private readonly Stack<int> _bundleIndexStack = new Stack<int>();

//        /// <summary> Key is a composite string with the path of operator ID's in it. </summary>
//        private readonly Dictionary<string, double> _previousTimeDictionary = new Dictionary<string, double>();
//        /// <summary> Key is a composite string with the path of operator ID's in it. </summary>
//        private readonly Dictionary<string, double> _phaseDictionary = new Dictionary<string, double>();
//        /// <summary> Value can be null of Curve Operator's Curve is not set. </summary>
//        private readonly Dictionary<string, ICurveCalculator> _curveIDString_To_CurveCalculator_Dictionary = new Dictionary<string, ICurveCalculator>();
//        private readonly Dictionary<Operator, double> _numberOperator_To_Value_Dictionary = new Dictionary<Operator, double>();
//        /// <summary> Value can be null of Sample Operator's Sample is not set. </summary>
//        private readonly Dictionary<string, ISampleCalculator> _sampleIDString_To_SampleCalculator_Dictionary = new Dictionary<string, ISampleCalculator>();
//        /// <summary> Value is offset in seconds. </summary>
//        private readonly Dictionary<Operator, double> _operator_WhiteNoiseOffset_Dictionary = new Dictionary<Operator, double>();

//        /// <param name="channelOutlets">Can contain nulls.</param>
//        public InterpretedPatchCalculator(
//            IList<Outlet> channelOutlets,
//            WhiteNoiseCalculator whiteNoiseCalculator,
//            ICurveRepository curveRepository,
//            ISampleRepository sampleRepository,
//            IPatchRepository patchRepository)
//        {
//            if (channelOutlets == null) throw new NullException(() => channelOutlets);
//            if (whiteNoiseCalculator == null) throw new NullException(() => whiteNoiseCalculator);
//            if (curveRepository == null) throw new NullException(() => curveRepository);
//            if (sampleRepository == null) throw new NullException(() => sampleRepository);
//            if (patchRepository == null) throw new NullException(() => patchRepository);

//            _curveRepository = curveRepository;
//            _sampleRepository = sampleRepository;
//            _patchRepository = patchRepository;
//            _whiteNoiseCalculator = whiteNoiseCalculator;

//            foreach (Outlet channelOutlet in channelOutlets)
//            {
//                IValidator validator = new OperatorValidator_Recursive(
//                    channelOutlet.Operator,
//                    _curveRepository,
//                    _sampleRepository,
//                    _patchRepository,
//                    alreadyDone: new HashSet<object>());

//                validator.Assert();
//            }

//            _channelOutlets = channelOutlets.ToArray();

//            _funcDictionary = new Dictionary<OperatorTypeEnum, Func<Outlet, double, double>>
//            {
//                { OperatorTypeEnum.Add, CalculateAdd },
//                { OperatorTypeEnum.Adder, CalculateAdder },
//                { OperatorTypeEnum.Bundle, CalculateBundle },
//                { OperatorTypeEnum.Curve, CalculateCurveOperator },
//                { OperatorTypeEnum.CustomOperator, CalculateCustomOperator },
//                { OperatorTypeEnum.Divide, CalculateDivide },
//                { OperatorTypeEnum.Exponent, CalculateExponent },
//                { OperatorTypeEnum.Loop, CalculateLoop },
//                { OperatorTypeEnum.Multiply, CalculateMultiply },
//                { OperatorTypeEnum.PatchInlet, CalculatePatchInlet },
//                { OperatorTypeEnum.PatchOutlet, CalculatePatchOutlet },
//                { OperatorTypeEnum.Power, CalculatePower },
//                { OperatorTypeEnum.Sample, CalculateSampleOperator },
//                { OperatorTypeEnum.SawTooth, CalculateSawTooth },
//                { OperatorTypeEnum.Select, CalculateSelect },
//                { OperatorTypeEnum.Sine, CalculateSine },
//                { OperatorTypeEnum.SquareWave, CalculateSquareWave },
//                { OperatorTypeEnum.Subtract, CalculateSubtract },
//                { OperatorTypeEnum.Delay, CalculateDelay },
//                { OperatorTypeEnum.SpeedUp, CalculateSpeedUp },
//                { OperatorTypeEnum.SlowDown, CalculateSlowDown },
//                { OperatorTypeEnum.TimePower, CalculateTimePower },
//                { OperatorTypeEnum.Earlier, CalculateEarlier },
//                { OperatorTypeEnum.TriangleWave, CalculateTriangleWave },
//                { OperatorTypeEnum.Number, CalculateNumberOperator },
//                { OperatorTypeEnum.Unbundle, CalculateUnbundle },
//                { OperatorTypeEnum.WhiteNoise, CalculateWhiteNoise },
//            };
//        }

//        public double Calculate(double time, int channelIndex)
//        {
//            _channelIndex = channelIndex;
//            return Calculate(_channelOutlets[_channelIndex], time);
//        }

//        /// <param name="outlet">nullable</param>
//        private double Calculate(Outlet outlet, double time)
//        {
//            // Outlet is null if channel not filled in
//            if (outlet == null)
//            {
//                return 0;
//            }

//            _outletStack.Push(outlet);

//            OperatorTypeEnum operatorTypeEnum = outlet.Operator.GetOperatorTypeEnum();

//            // Resample operator is missing from interpreted mode too.
//            Func<Outlet, double, double> func;
//            if (!_funcDictionary.TryGetValue(operatorTypeEnum, out func))
//            {
//                throw new ValueNotSupportedException(operatorTypeEnum);
//            }
//            double value = func(outlet, time);

//            _outletStack.Pop();

//            return value;
//        }

//        private double CalculateAdd(Outlet outlet, double time)
//        {
//            Operator op = outlet.Operator;

//            var wrapper = new Add_OperatorWrapper(op);

//            Outlet operandAOutlet = wrapper.OperandA;
//            Outlet operandBOutlet = wrapper.OperandB;

//            if (operandAOutlet == null || operandBOutlet == null) return 0;

//            double a = Calculate(operandAOutlet, time);
//            double b = Calculate(operandBOutlet, time);
//            return a + b;
//        }

//        private double CalculateAdder(Outlet outlet, double time)
//        {
//            Operator op = outlet.Operator;

//            var wrapper = new Adder_OperatorWrapper(op);

//            Outlet[] operands = wrapper.Operands.ToArray();

//            double result = 0;

//            for (int i = 0; i < operands.Length; i++)
//            {
//                Outlet operand = operands[i];

//                if (operand != null)
//                {
//                    result += Calculate(operand, time);
//                }
//            }

//            return result;
//        }

//        private double CalculateBundle(Outlet outlet, double time)
//        {
//            if (_bundleIndexStack.Count == 0)
//            {
//                throw new NotSupportedException(String.Format("Bundle Operator with ID '{0}' encountered without first encountering an Unbundle Operator. This is not yet supported.", outlet.Operator.ID));
//            }

//            int bundleIndex = _bundleIndexStack.Pop();

//            if (bundleIndex >= outlet.Operator.Inlets.Count)
//            {
//                throw new Exception(String.Format("Index '{0}' does not exist in Bundle Operator with ID '{0}'.", bundleIndex, outlet.Operator.ID));
//            }

//            Inlet inlet = outlet.Operator.Inlets[bundleIndex];
//            if (inlet.InputOutlet == null)
//            {
//                return 0.0;
//            }

//            double value = Calculate(inlet.InputOutlet, time);

//            _bundleIndexStack.Push(bundleIndex);

//            return value;
//        }

//        private double CalculateCurveOperator(Outlet outlet, double time)
//        {
//            Operator op = outlet.Operator;

//            ICurveCalculator curveCalculator;
//            if (!_curveIDString_To_CurveCalculator_Dictionary.TryGetValue(op.Data, out curveCalculator))
//            {
//                var wrapper = new Curve_OperatorWrapper(op, _curveRepository);
//                Curve curve = wrapper.Curve;

//                if (curve != null)
//                {
//                    curveCalculator = new OptimizedCurveCalculator(curve);
//                }

//                _curveIDString_To_CurveCalculator_Dictionary.Add(op.Data, curveCalculator);
//            }

//            if (curveCalculator == null)
//            {
//                return 0;
//            }

//            double result = curveCalculator.CalculateValue(time);
//            return result;
//        }

//        private double CalculateCustomOperator(Outlet customOperatorOutlet, double time)
//        {
//            Outlet outlet = PatchCalculationHelper.TryApplyCustomOperatorToUnderlyingPatch(customOperatorOutlet, _patchRepository);

//            if (outlet == null)
//            {
//                return 0.0;
//            }

//            double result = Calculate(outlet, time);
//            return result;
//        }

//        private double CalculateDelay(Outlet outlet, double time)
//        {
//            Operator op = outlet.Operator;

//            var wrapper = new Delay_OperatorWrapper(op);

//            Outlet signalOutlet = wrapper.Signal;
//            if (signalOutlet == null) return 0;

//            Outlet timeDifferenceOutlet = wrapper.TimeDifference;
//            if (timeDifferenceOutlet == null)
//            {
//                double result = Calculate(signalOutlet, time);
//                return result;
//            }

//            // IMPORTANT: To add time to the output, you have subtract time from the input.
//            double timeDifference = Calculate(timeDifferenceOutlet, time);
//            double transformedTime = time - timeDifference;
//            double result2 = Calculate(signalOutlet, transformedTime);
//            return result2;
//        }

//        private double CalculateDivide(Outlet outlet, double time)
//        {
//            Operator op = outlet.Operator;

//            var wrapper = new Divide_OperatorWrapper(op);

//            Outlet originOutlet = wrapper.Origin;
//            Outlet numeratorOutlet = wrapper.Numerator;
//            Outlet denominatorOutlet = wrapper.Denominator;

//            // Without Origin
//            if (originOutlet == null)
//            {
//                if (numeratorOutlet == null || denominatorOutlet == null) return 0;

//                double denominator = Calculate(denominatorOutlet, time);

//                if (denominator == 0) return 0;

//                double numerator = Calculate(numeratorOutlet, time);

//                return numerator / denominator;
//            }

//            // With Origin
//            else
//            {
//                double origin = Calculate(originOutlet, time);

//                if (numeratorOutlet == null || denominatorOutlet == null) return origin;

//                double denominator = Calculate(denominatorOutlet, time);

//                if (denominator == 0) return origin;

//                double numerator = Calculate(numeratorOutlet, time);

//                return (numerator - origin) / denominator + origin;
//            }
//        }

//        private double CalculateExponent(Outlet outlet, double time)
//        {
//            Operator op = outlet.Operator;

//            var wrapper = new Exponent_OperatorWrapper(op);

//            Outlet lowOutlet = wrapper.Low;
//            Outlet highOutlet = wrapper.High;
//            Outlet ratioOutlet = wrapper.Ratio;

//            if (lowOutlet == null)
//            {
//                return 0.0;
//            }

//            if (highOutlet == null)
//            {
//                return Calculate(lowOutlet, time);
//            }

//            if (ratioOutlet == null)
//            {
//                return Calculate(lowOutlet, time);
//            }

//            double low = Calculate(lowOutlet, time);
//            double high = Calculate(highOutlet, time);
//            double ratio = Calculate(ratioOutlet, time);

//            if (low == 0.0)
//            {
//                // Prevent NaN out of division by 0.
//                return 0.0;
//            }

//            double value = low * Math.Pow(high / low, ratio);
//            return value;
//        }

//        private double CalculateLoop(Outlet outlet, double time)
//        {
//            Operator op = outlet.Operator;

//            var wrapper = new Loop_OperatorWrapper(op);

//            double outputTime = time;
//            double inputAttack = Calculate(wrapper.Attack, outputTime);
//            double inputTime = time + inputAttack;

//            bool isBeforeAttack = inputTime < inputAttack;
//            if (isBeforeAttack)
//            {
//                return 0;
//            }

//            double inputStart = Calculate(wrapper.Start, outputTime);
//            bool isInAttack = inputTime < inputStart;
//            if (isInAttack)
//            {
//                double value =  Calculate(wrapper.Signal, inputTime);
//                return value;
//            }

//            double inputEnd = Calculate(wrapper.End, outputTime);
//            double outputSustain = Calculate(wrapper.Sustain, outputTime);
//            double outputEnd = inputStart - inputAttack + outputSustain;
//            bool isInLoop = outputTime < outputEnd;
//            if (isInLoop)
//            {
//                double inputSustain = inputEnd - inputStart;
//                double positionInCycle = (inputTime - inputStart) % inputSustain;
//                inputTime = inputStart + positionInCycle;
//                double value = Calculate(wrapper.Signal, inputTime);
//                return value;
//            }

//            double inputRelease = Calculate(wrapper.Release, outputTime);
//            double releaseDuration = inputRelease - inputEnd;
//            double outputRelease = outputEnd + releaseDuration;
//            bool isInRelease = outputTime < outputRelease;
//            if (isInRelease)
//            {
//                double positionInRelease = outputTime - outputEnd;
//                inputTime = inputEnd + positionInRelease;
//                double value = Calculate(wrapper.Signal, inputTime);
//                return value;
//            }

//            // IsAfterRelease
//            return 0;
//        }

//        private double CalculateMultiply(Outlet outlet, double time)
//        {
//            Operator op = outlet.Operator;

//            var wrapper = new Multiply_OperatorWrapper(op);

//            Outlet originOutlet = wrapper.Origin;
//            Outlet operandAOutlet = wrapper.OperandA;
//            Outlet operandBOutlet = wrapper.OperandB;

//            if (originOutlet == null)
//            {
//                if (operandAOutlet == null || operandBOutlet == null) return 0;

//                double a = Calculate(operandAOutlet, time);
//                double b = Calculate(operandBOutlet, time);
//                return a * b;
//            }
//            else
//            {
//                double origin = Calculate(originOutlet, time);

//                if (operandAOutlet == null || operandBOutlet == null) return origin;

//                double a = Calculate(operandAOutlet, time);
//                double b = Calculate(operandBOutlet, time);
//                return (a - origin) * b + origin;
//            }
//        }

//        private double CalculateNumberOperator(Outlet outlet, double time)
//        {
//            Operator op = outlet.Operator;

//            double value;
//            if (!_numberOperator_To_Value_Dictionary.TryGetValue(op, out value))
//            {
//                var wrapper = new Number_OperatorWrapper(op);
//                value = wrapper.Number;
//                _numberOperator_To_Value_Dictionary.Add(op, value);
//            }
//            return value;
//        }

//        private double CalculatePower(Outlet outlet, double time)
//        {
//            Operator op = outlet.Operator;

//            var wrapper = new Power_OperatorWrapper(op);

//            Outlet baseOutlet = wrapper.Base;
//            Outlet exponentOutlet = wrapper.Exponent;

//            if (baseOutlet == null || exponentOutlet == null) return 0;

//            double @base = Calculate(baseOutlet, time);
//            double exponent = Calculate(exponentOutlet, time);

//            return Math.Pow(@base, exponent);
//        }

//        private double CalculatePatchInlet(Outlet outlet, double time)
//        {
//            Operator op = outlet.Operator;

//            var wrapper = new PatchInlet_OperatorWrapper(op);

//            Outlet inputOutlet = wrapper.Input;

//            if (inputOutlet == null) return 0;

//            return Calculate(inputOutlet, time);
//        }

//        private double CalculatePatchOutlet(Outlet outlet, double time)
//        {
//            Operator op = outlet.Operator;

//            var wrapper = new PatchOutlet_OperatorWrapper(op);

//            Outlet inputOutlet = wrapper.Input;

//            if (inputOutlet == null) return 0;

//            return Calculate(inputOutlet, time);
//        }

//        private double CalculateSlowDown(Outlet outlet, double time)
//        {
//            Operator op = outlet.Operator;

//            var wrapper = new SlowDown_OperatorWrapper(op);

//            // No signal? Return 0.
//            Outlet signalOutlet = wrapper.Signal;
//            if (signalOutlet == null)
//            {
//                return 0.0;
//            }

//            // No factor? Just pass through signal.
//            Outlet factorOutlet = wrapper.Factor;
//            if (factorOutlet == null)
//            {
//                return Calculate(signalOutlet, time);
//            }

//            double factor = Calculate(factorOutlet, time);

//            // Weird number
//            // Slow down by a factor of 0 equals speed up to infinity, which makes the result undefined.
//            if (factor == 0.0)
//            {
//                return Double.NaN;
//            }

//            // Get Phase Variables
//            string key = GetOutletPathKey();
//            double previousTime = 0;
//            _previousTimeDictionary.TryGetValue(key, out previousTime);
//            double phase = 0;
//            _phaseDictionary.TryGetValue(key, out phase);

//            // Weird numbers
//            // Slow down to infinity means time stands still, so just do not advance phase.
//            if (!Double.IsInfinity(factor) && !Double.IsNaN(factor))
//            {
//                double dt = time - previousTime;
//                // IMPORTANT: To multiply the time in the output, you have to divide the time of the input.
//                phase = phase + dt / factor;
//            }

//            double result = Calculate(signalOutlet, phase);

//            // Store phase variables
//            _phaseDictionary[key] = phase;
//            _previousTimeDictionary[key] = time;

//            return result;
//        }

//        private double CalculateSubtract(Outlet outlet, double time)
//        {
//            Operator op = outlet.Operator;

//            var wrapper = new Subtract_OperatorWrapper(op);

//            Outlet operandAOutlet = wrapper.OperandA;
//            Outlet operandBOutlet = wrapper.OperandB;

//            if (operandAOutlet == null || operandBOutlet == null) return 0;

//            double a = Calculate(operandAOutlet, time);
//            double b = Calculate(operandBOutlet, time);

//            return a - b;
//        }

//        private double CalculateSpeedUp(Outlet outlet, double time)
//        {
//            Operator op = outlet.Operator;

//            var wrapper = new SpeedUp_OperatorWrapper(op);

//            // No signal? Return 0.
//            Outlet signalOutlet = wrapper.Signal;
//            if (signalOutlet == null)
//            {
//                return 0.0;
//            }

//            // No factor? Just pass through signal.
//            Outlet factorOutlet = wrapper.Factor;
//            if (factorOutlet == null)
//            {
//                return Calculate(signalOutlet, time);
//            }

//            double factor = Calculate(factorOutlet, time);

//            // Weird numbers
//            if (Double.IsInfinity(factor) || Double.IsNaN(factor))
//            {
//                // Speed up to infinity is undefined.
//                return Double.NaN;
//            }

//            // Get Phase Variables
//            string key = GetOutletPathKey();
//            double previousTime = 0;
//            _previousTimeDictionary.TryGetValue(key, out previousTime);
//            double phase = 0;
//            _phaseDictionary.TryGetValue(key, out phase);

//            // Weird number
//            // Speed up of 0 means time stands still, so just do not advance phase.
//            if (factor != 0.0)
//            {
//                double dt = time - previousTime;
//                // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
//                phase = phase + dt * factor;
//            }

//            double result = Calculate(signalOutlet, phase);

//            // Store phase variables
//            _phaseDictionary[key] = phase;
//            _previousTimeDictionary[key] = time;

//            return result;
//        }

//        private double CalculateSawTooth(Outlet outlet, double time)
//        {
//            Operator op = outlet.Operator;

//            var wrapper = new SawTooth_OperatorWrapper(op);

//            Outlet phaseShiftOutlet = wrapper.PhaseShift;
//            Outlet frequencyOutlet = wrapper.Frequency;

//            double frequency = 0;
//            double phaseShift = 0;

//            if (frequencyOutlet != null)
//            {
//                frequency = Calculate(frequencyOutlet, time);
//            }

//            if (phaseShiftOutlet != null)
//            {
//                phaseShift = Calculate(phaseShiftOutlet, time);
//            }

//            // Get phase variables
//            string key = GetOutletPathKey();
//            double previousTime = 0;
//            _previousTimeDictionary.TryGetValue(key, out previousTime);
//            double phase = 0;
//            _phaseDictionary.TryGetValue(key, out phase);

//            // Calculate new phase
//            double dt = time - previousTime;
//            phase = phase + dt * frequency;

//            // Calculate value
//            double shiftedPhase = phase + phaseShift;
//            double value = -1 + (2 * shiftedPhase % 2);

//            // Store phase variables
//            _phaseDictionary[key] = phase;
//            _previousTimeDictionary[key] = time;

//            return value;
//        }

//        private double CalculateSelect(Outlet outlet, double time)
//        {
//            Operator op = outlet.Operator;

//            var wrapper = new Select_OperatorWrapper(op);

//            double time2  = Calculate(wrapper.Time, time);

//            double value = Calculate(wrapper.Signal, time2);
//            return value;
//        }

//        private double CalculateSquareWave(Outlet outlet, double time)
//        {
//            Operator op = outlet.Operator;

//            var wrapper = new SquareWave_OperatorWrapper(op);

//            Outlet phaseShiftOutlet = wrapper.PhaseShift;
//            Outlet frequencyOutlet = wrapper.Frequency;

//            double frequency = 0;
//            double phaseShift = 0;

//            if (frequencyOutlet != null)
//            {
//                frequency = Calculate(frequencyOutlet, time);
//            }

//            if (phaseShiftOutlet != null)
//            {
//                phaseShift = Calculate(phaseShiftOutlet, time);
//            }

//            // Get phase variables
//            string key = GetOutletPathKey();
//            double previousTime = 0;
//            _previousTimeDictionary.TryGetValue(key, out previousTime);
//            double phase = 0;
//            _phaseDictionary.TryGetValue(key, out phase);

//            // Calculate new phase
//            double dt = time - previousTime;
//            phase = phase + dt * frequency;

//            // Calculate value
//            double value;
//            double shiftedPhase = phase + phaseShift;
//            double relativePhase = shiftedPhase % 1;
//            if (relativePhase < 0.5)
//            {
//                value = -1;
//            }
//            else
//            {
//                value = 1;
//            }

//            // Store phase variables
//            _phaseDictionary[key] = phase;
//            _previousTimeDictionary[key] = time;

//            return value;
//        }

//        private double CalculateSine(Outlet outlet, double time)
//        {
//            Operator op = outlet.Operator;

//            var wrapper = new Sine_OperatorWrapper(op);

//            // Get Frequency
//            Outlet frequencyOutlet = wrapper.Frequency;
//            if (frequencyOutlet == null)
//            {
//                return 0;
//            }
//            double frequency = Calculate(frequencyOutlet, time);

//            // Get Phase Variables
//            string key = GetOutletPathKey();
//            double previousTime = 0;
//            _previousTimeDictionary.TryGetValue(key, out previousTime);
//            double phase = 0;
//            _phaseDictionary.TryGetValue(key, out phase);

//            // Change Phase
//            double dt = time - previousTime;
//            phase = phase + Maths.TWO_PI * dt * frequency;

//            // Apply Phase Shift
//            double shiftedPhase = phase;
//            Outlet phaseShiftOutlet = wrapper.PhaseShift;
//            if (phaseShiftOutlet != null)
//            {
//                double phaseShift = Calculate(phaseShiftOutlet, time);
//                shiftedPhase = shiftedPhase + Maths.TWO_PI * phaseShift;
//            }

//            // Get Result
//            double result = SineCalculator.Sin(phase);

//            // Store phase variables
//            _phaseDictionary[key] = phase;
//            _previousTimeDictionary[key] = time;

//            return result;
//        }

//        private double CalculateSampleOperator(Outlet outlet, double time)
//        {
//            Operator op = outlet.Operator;

//            var wrapper = new Sample_OperatorWrapper(op, _sampleRepository);

//            // Get SampleCalculator
//            ISampleCalculator sampleCalculator;
//            if (!_sampleIDString_To_SampleCalculator_Dictionary.TryGetValue(op.Data, out sampleCalculator))
//            {
//                SampleInfo sampleInfo = wrapper.SampleInfo;

//                if (sampleInfo.Sample != null)
//                {
//                    sampleCalculator = SampleCalculatorFactory.CreateSampleCalculator(sampleInfo.Sample, sampleInfo.Bytes);
//                }

//                _sampleIDString_To_SampleCalculator_Dictionary.Add(op.Data, sampleCalculator);
//            }
//            if (sampleCalculator == null)
//            {
//                return 0.0;
//            }

//            // Get Frequency
//            Outlet frequencyOutlet = wrapper.Frequency;
//            if (frequencyOutlet == null)
//            {
//                return 0;
//            }
//            double frequency = Calculate(frequencyOutlet, time);
//            double rate = frequency / SAMPLE_BASE_FREQUENCY;

//            // Get Phase Variables
//            string key = GetOutletPathKey();
//            double previousTime = 0;
//            _previousTimeDictionary.TryGetValue(key, out previousTime);
//            double phase = 0;
//            _phaseDictionary.TryGetValue(key, out phase);

//            // Change Phase
//            double dt = time - previousTime;
//            phase = phase + dt * rate;

//            // This is a solution for when the sample channels do not match the channel we want.
//            // But this is only a fallback that will work for mono and stereo,
//            // and not for e.g. 5.1 surround sound.
//            // TODO: This is not the same behavior as the optimized calculation.
//            int channelIndex;
//            if (sampleCalculator.ChannelCount < _channelIndex)
//            {
//                channelIndex = 0;
//            }
//            else
//            {
//                channelIndex = _channelIndex;
//            }

//            // Get Result
//            double result = sampleCalculator.CalculateValue(phase, channelIndex);

//            // Store phase variables
//            _phaseDictionary[key] = phase;
//            _previousTimeDictionary[key] = time;

//            return result;
//        }

//        private double CalculateEarlier(Outlet outlet, double time)
//        {
//            Operator op = outlet.Operator;

//            var wrapper = new Earlier_OperatorWrapper(op);

//            Outlet signalOutlet = wrapper.Signal;
//            if (signalOutlet == null) return 0;

//            Outlet timeDifferenceOutlet = wrapper.TimeDifference;
//            if (timeDifferenceOutlet == null)
//            {
//                double result = Calculate(signalOutlet, time);
//                return result;
//            }

//            // IMPORTANT: To subtract time from the output, you have add time to the input.
//            double timeDifference = Calculate(timeDifferenceOutlet, time);
//            double transformedTime = time + timeDifference;
//            double result2 = Calculate(signalOutlet, transformedTime);
//            return result2;
//        }

//        private double CalculateTimePower(Outlet outlet, double time)
//        {
//            Operator op = outlet.Operator;

//            var wrapper = new TimePower_OperatorWrapper(op);

//            Outlet signalOutlet = wrapper.Signal;
//            Outlet exponentOutlet = wrapper.Exponent;
//            Outlet originOutlet = wrapper.Origin;

//            if (signalOutlet == null)
//            {
//                return 0;
//            }

//            if (exponentOutlet == null)
//            {
//                return Calculate(signalOutlet, time);
//            }

//            // IMPORTANT: 

//            // To increase time in the output, you have to decrease time of the input. 
//            // That is why the reciprocal of the exponent is used.

//            // Furthermore, you can not use a fractional exponent on a negative number.
//            // Time can be negative, that is why the sign is taken off the time 
//            // before taking the power and then added to it again after taking the power.

//            if (originOutlet == null)
//            {
//                // (time: -4, exponent: 2) => -1 * Pow(4, 1/2)
//                double timeAbs = Math.Abs(time);
//                double timeSign = Math.Sign(time);

//                double exponent = Calculate(exponentOutlet, time);

//                double transformedTime = timeSign * Math.Pow(timeAbs, 1 / exponent);

//                double result = Calculate(signalOutlet, transformedTime);
//                return result;
//            }
//            else
//            {
//                double origin = Calculate(originOutlet, time);

//                double timeAbs = System.Math.Abs(time - origin);
//                double timeSign = System.Math.Sign(time - origin);

//                double exponent = Calculate(exponentOutlet, time);

//                double transformedTime = timeSign * Math.Pow(timeAbs, 1 / exponent) + origin;

//                double result = Calculate(signalOutlet, transformedTime);
//                return result;
//            }
//        }

//        private double CalculateTriangleWave(Outlet outlet, double time)
//        {
//            Operator op = outlet.Operator;

//            var wrapper = new TriangleWave_OperatorWrapper(op);

//            Outlet phaseShiftOutlet = wrapper.PhaseShift;
//            Outlet frequencyOutlet = wrapper.Frequency;

//            double frequency = 0;
//            double phaseShift = 0;

//            if (frequencyOutlet != null)
//            {
//                frequency = Calculate(frequencyOutlet, time);
//            }

//            if (phaseShiftOutlet != null)
//            {
//                phaseShift = Calculate(phaseShiftOutlet, time);
//            }

//            // Get phase variables
//            string key = GetOutletPathKey();
//            double previousTime = 0;
//            _previousTimeDictionary.TryGetValue(key, out previousTime);
//            double phase = 0;
//            _phaseDictionary.TryGetValue(key, out phase);

//            // Calculate new phase
//            double dt = time - previousTime;
//            phase = phase + dt * frequency;

//            // Calculate value
//            double shiftedPhase = phase + phaseShift;
//            // Correct the phase, because our calculation starts with value -1, but in practice you want to start at value 0 going up.
//            shiftedPhase += 0.25;
//            double relativePhase = shiftedPhase % 1.0;
//            double value;
//            if (relativePhase < 0.5)
//            {
//                // Starts going up at a rate of 2 up over 1/2 a cycle.
//                value = -1.0 + 4.0 * relativePhase;
//            }
//            else
//            {
//                // And then going down at phase 1/2.
//                // (Extending the line to x = 0 it ends up at y = 3.)
//                value = 3.0 - 4.0 * relativePhase;
//            }

//            // Store phase variables
//            _phaseDictionary[key] = phase;
//            _previousTimeDictionary[key] = time;

//            return value;
//        }

//        private double CalculateUnbundle(Outlet outlet, double time)
//        {
//            Operator op = outlet.Operator;
//            Inlet inlet = op.Inlets.Single();
//            Outlet inputOutlet = inlet.InputOutlet;
//            if (inputOutlet == null)
//            {
//                return 0.0;
//            }

//            int outletIndex = outlet.Operator.Outlets.IndexOf(outlet);

//            _bundleIndexStack.Push(outletIndex);

//            double value = Calculate(inlet.InputOutlet, time);

//            _bundleIndexStack.Pop();

//            return value;
//        }

//        private double CalculateWhiteNoise(Outlet outlet, double time)
//        {
//            Operator op = outlet.Operator;

//            double offset;
//            if (!_operator_WhiteNoiseOffset_Dictionary.TryGetValue(op, out offset))
//            {
//                offset = _whiteNoiseCalculator.GetRandomOffset();
//                _operator_WhiteNoiseOffset_Dictionary.Add(op, offset);
//            }

//            double value = _whiteNoiseCalculator.GetValue(time + offset);

//            return value;
//        }

//        // Helpers

//        private string GetOutletPathKey()
//        {
//            string key = String.Join("|", _outletStack.Select(x => x.ID));
//            return key;
//        }

//        public void ResetPhases(double time)
//        {
//            // TODO: Low priority: this is error prone, because it will only work for phase-tracked operators that have been processed before.
//            foreach (var entry in _previousTimeDictionary)
//            {
//                _previousTimeDictionary[entry.Key] = time;
//            }

//            _phaseDictionary.Clear();
//        }

//        // TODO: Implement these methods.

//        public void SetValue(InletTypeEnum inletTypeEnum, double value)
//        {
//            throw new NotImplementedException();
//        }

//        public void SetValue(string name, double value)
//        {
//            throw new NotImplementedException();
//        }

//        public void SetValue(int listIndex, double value)
//        {
//            throw new NotImplementedException();
//        }

//        public void SetValue(string name, int listIndex, double value)
//        {
//            throw new NotImplementedException();
//        }

//        public void SetValue(InletTypeEnum inletTypeEnum, int listIndex, double value)
//        {
//            throw new NotImplementedException();
//        }

//        public double GetValue(int listIndex)
//        {
//            throw new NotImplementedException();
//        }

//        public double GetValue(string name)
//        {
//            throw new NotImplementedException();
//        }

//        public double GetValue(string name, int listIndex)
//        {
//            throw new NotImplementedException();
//        }

//        public double GetValue(InletTypeEnum inletTypeEnum)
//        {
//            throw new NotImplementedException();
//        }

//        public double GetValue(InletTypeEnum inletTypeEnum, int listIndex)
//        {
//            throw new NotImplementedException();
//        }

//        public void ResetPhase(int monophonicListIndex, double time)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
