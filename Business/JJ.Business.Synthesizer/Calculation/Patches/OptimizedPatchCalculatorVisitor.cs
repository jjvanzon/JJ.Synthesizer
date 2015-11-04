using System;
using System.Collections.Generic;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Patches
{
    /// <summary>
    /// The way this class works, is that the base visitor visits an Operator's Inlets,
    /// which will lead to Calculator objects to be put on a stack.
    /// Then the base class calls the appropriate specialized visit method for the Operator, e.g. VisitAdd,
    /// which can then pop its operands from this stack, 
    /// and decide which Calculator to push onto the stack again.
    /// </summary>
    internal partial class OptimizedPatchCalculatorVisitor : OperatorVisitorBase
    {
        private ICurveRepository _curveRepository;
        private ISampleRepository _sampleRepository;
        private IDocumentRepository _documentRepository;

        private WhiteNoiseCalculator _whiteNoiseCalculator;

        private int _channelCount;
        private Stack<OperatorCalculatorBase> _stack;
        private Stack<int> _bundleIndexStack = new Stack<int>();

        public IList<OperatorCalculatorBase> Execute(
            IList<Outlet> channelOutlets, 
            WhiteNoiseCalculator whiteNoiseCalculator, 
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            IDocumentRepository documentRepository)
        {
            if (whiteNoiseCalculator == null) throw new NullException(() => whiteNoiseCalculator);
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (documentRepository == null) throw new NullException(() => documentRepository);
            if (channelOutlets == null) throw new NullException(() => channelOutlets);

            _whiteNoiseCalculator = whiteNoiseCalculator;
            _curveRepository = curveRepository;
            _sampleRepository = sampleRepository;
            _documentRepository = documentRepository;

            foreach (Outlet channelOutlet in channelOutlets)
            {
                IValidator validator = new OperatorValidator_Recursive(
                    channelOutlet.Operator, 
                    _curveRepository, _sampleRepository, _documentRepository,
                    alreadyDone: new HashSet<object>());
                validator.Verify();
            }

            _stack = new Stack<OperatorCalculatorBase>();
            _channelCount = channelOutlets.Count;

            var list = new List<OperatorCalculatorBase>(_channelCount);

            foreach (Outlet channelOutlet in channelOutlets)
            {
                VisitOutlet(channelOutlet);

                OperatorCalculatorBase operatorCalculator = _stack.Pop();

                if (_stack.Count != 0)
                {
                    throw new Exception("_stack.Count should have been 0.");
                }

                operatorCalculator = operatorCalculator ?? new Zero_OperatorCalculator(); 

                list.Add(operatorCalculator);
            }

            return list;
        }

        protected override void VisitAdd(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase operandACalculator = _stack.Pop();
            OperatorCalculatorBase operandBCalculator = _stack.Pop();

            operandACalculator = operandACalculator ?? new Number_OperatorCalculator(0);
            operandBCalculator = operandBCalculator ?? new Number_OperatorCalculator(0);

            double a = operandACalculator.Calculate(0, 0);
            double b = operandBCalculator.Calculate(0, 0);
            bool operandAIsConst = operandACalculator is Number_OperatorCalculator;
            bool operandBIsConst = operandBCalculator is Number_OperatorCalculator;
            bool operandAIsConstZero = operandAIsConst && a == 0;
            bool operandBIsConstZero = operandBIsConst && b == 0;

            if (operandAIsConstZero && operandBIsConstZero)
            {
                calculator = new Number_OperatorCalculator(0);
            }
            else if (operandAIsConstZero)
            {
                calculator = operandBCalculator;
            }
            else if (operandBIsConstZero)
            {
                calculator = operandACalculator;
            }
            else if (operandAIsConst && operandBIsConst)
            {
                calculator = new Number_OperatorCalculator(a + b);
            }
            else if (operandAIsConst)
            {
                calculator = new Add_WithConstOperandA_OperatorCalculator(a, operandBCalculator);
            }
            else if (operandBIsConst)
            {
                calculator = new Add_WithConstOperandB_OperatorCalculator(operandACalculator, b);
            }
            else
            {
                calculator = new Add_OperatorCalculator(operandACalculator, operandBCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitCurveOperator(Operator op)
        {
            OperatorCalculatorBase calculator;

            var wrapper = new OperatorWrapper_Curve(op, _curveRepository);

            if (wrapper.Curve == null)
            {
                calculator = new Number_OperatorCalculator(0);
            }
            else
            {
                calculator = new Curve_OperatorCalculator(wrapper.Curve);
            }

            _stack.Push(calculator);
        }

        protected override void VisitDelay(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeDifferenceCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Number_OperatorCalculator(0);
            timeDifferenceCalculator = timeDifferenceCalculator ?? new Number_OperatorCalculator(0);
            double signal = signalCalculator.Calculate(0, 0);
            double timeDifference = timeDifferenceCalculator.Calculate(0, 0);
            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool timeDifferenceIsConst = timeDifferenceCalculator is Number_OperatorCalculator;
            bool signalIsConstZero = signalIsConst && signal == 0;
            bool timeDifferenceIsConstZero = timeDifferenceIsConst && timeDifference == 0;

            if (signalIsConstZero)
            {
                calculator = new Number_OperatorCalculator(0);
            }
            else if (timeDifferenceIsConstZero)
            {
                calculator = signalCalculator;
            }
            else if (signalIsConst)
            {
                calculator = signalCalculator;
            }
            else if (timeDifferenceIsConst)
            {
                calculator = new Delay_WithConstTimeDifference_OperatorCalculator(signalCalculator, timeDifference);
            }
            else
            {
                calculator = new Delay_OperatorCalculator(signalCalculator, timeDifferenceCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitDivide(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase numeratorCalculator = _stack.Pop();
            OperatorCalculatorBase denominatorCalculator = _stack.Pop();
            OperatorCalculatorBase originCalculator = _stack.Pop();

            // When nulls should make the operator do nothing but pass the signal.
            if (denominatorCalculator == null && numeratorCalculator != null)
            {
                _stack.Push(numeratorCalculator);
                return;
            }

            numeratorCalculator = numeratorCalculator ?? new Number_OperatorCalculator(0);
            denominatorCalculator = denominatorCalculator ?? new Number_OperatorCalculator(1);
            originCalculator = originCalculator ?? new Number_OperatorCalculator(0);

            double numerator = numeratorCalculator.Calculate(0, 0);
            double denominator = denominatorCalculator.Calculate(0, 0);
            double origin = originCalculator.Calculate(0, 0);
            bool denominatorIsConst = denominatorCalculator is Number_OperatorCalculator;
            bool numeratorIsConst = numeratorCalculator is Number_OperatorCalculator;
            bool originIsConst = originCalculator is Number_OperatorCalculator;
            bool numeratorIsConstZero = numeratorIsConst && numerator == 0;
            bool denominatorIsConstZero = denominatorIsConst && denominator == 0;
            bool originIsConstZero = originIsConst && origin == 0;
            bool denominatorIsConstOne = denominatorIsConst && denominator == 1;

            if (denominatorIsConstZero)
            {
                // Weird number
                calculator = new Number_OperatorCalculator(0);
            }
            else if (numeratorIsConstZero)
            {
                calculator = new Number_OperatorCalculator(0);
            }
            else if (denominatorIsConstOne)
            {
                calculator = numeratorCalculator;
            }
            else if (originIsConstZero && numeratorIsConst & denominatorIsConst)
            {
                calculator = new Number_OperatorCalculator(numerator / denominator);
            }
            else if (originIsConst && numeratorIsConst && denominatorIsConst)
            {
                double value = (numerator - origin) / denominator + origin;
                calculator = new Number_OperatorCalculator(value);
            }
            else if (originIsConstZero && numeratorIsConst && !denominatorIsConst)
            {
                calculator = new Divide_WithoutOrigin_WithConstNumerator_OperatorCalculator(numerator, denominatorCalculator);
            }
            else if (originIsConstZero && !numeratorIsConst && denominatorIsConst)
            {
                calculator = new Divide_WithoutOrigin_WithConstDenominator_OperatorCalculator(numeratorCalculator, denominator);
            }
            else if (originIsConstZero && !numeratorIsConst && !denominatorIsConst)
            {
                calculator = new Divide_WithoutOrigin_OperatorCalculator(numeratorCalculator, denominatorCalculator);
            }
            else if (originIsConst && numeratorIsConst && !denominatorIsConst)
            {
                calculator = new Divide_WithConstOrigin_AndNumerator_OperatorCalculator(numerator, denominatorCalculator, origin);
            }
            else if (originIsConst && !numeratorIsConst && denominatorIsConst)
            {
                calculator = new Divide_WithConstOrigin_AndDenominator_OperatorCalculator(numeratorCalculator, denominator, origin);
            }
            else if (originIsConst && !numeratorIsConst && !denominatorIsConst)
            {
                calculator = new Divide_WithConstOrigin_OperatorCalculator(numeratorCalculator, denominatorCalculator, origin);
            }
            else if (!originIsConst && numeratorIsConst && denominatorIsConst)
            {
                calculator = new Divide_WithOrigin_AndConstNumerator_AndDenominator_OperatorCalculator(numerator, denominator, originCalculator);
            }
            else if (!originIsConst && numeratorIsConst && !denominatorIsConst)
            {
                calculator = new Divide_WithOrigin_AndConstNumerator_OperatorCalculator(numerator, denominatorCalculator, originCalculator);
            }
            else if (!originIsConst && !numeratorIsConst && denominatorIsConst)
            {
                calculator = new Divide_WithOrigin_AndConstDenominator_OperatorCalculator(numeratorCalculator, denominator, originCalculator);
            }
            else
            {
                calculator = new Divide_WithOrigin_OperatorCalculator(numeratorCalculator, denominatorCalculator, originCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitExponent(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase lowCalculator = _stack.Pop();
            OperatorCalculatorBase highCalculator = _stack.Pop();
            OperatorCalculatorBase ratioCalculator = _stack.Pop();

            lowCalculator = lowCalculator ?? new Number_OperatorCalculator(0);
            highCalculator = highCalculator ?? new Number_OperatorCalculator(0);
            ratioCalculator = ratioCalculator ?? new Number_OperatorCalculator(0);

            double low = lowCalculator.Calculate(0, 0);
            double high = highCalculator.Calculate(0, 0);
            double ratio = ratioCalculator.Calculate(0, 0);
            bool lowIsConst = lowCalculator is Number_OperatorCalculator;
            bool highIsConst = highCalculator is Number_OperatorCalculator;
            bool ratioIsConst = ratioCalculator is Number_OperatorCalculator;

            // TODO: Program specialization in case of special values later.
            //bool lowIsConstZero = lowIsConst && low == 0;
            //bool highIsConstZero = lowIsConst && high == 0;
            //bool ratioIsConstZero = ratioIsConst && ratio == 0;
            //bool lowIsConstOne = lowIsConst && low == 1;
            //bool highIsConstOne = lowIsConst && high == 1;
            //bool ratioIsConstOne = ratioIsConst && ratio == 1;

            if (lowIsConst && highIsConst && ratioIsConst)
            {
                double value = low * Math.Pow(high / low, ratio);
                calculator = new Number_OperatorCalculator(value);
            }
            else if (!lowIsConst && highIsConst && ratioIsConst)
            {
                calculator = new Exponent_WithConstHighAndConstRatio_OperatorCalculator(lowCalculator, high, ratio);
            }
            else if (lowIsConst && !highIsConst && ratioIsConst)
            {
                calculator = new Exponent_WithConstLowAndConstRatio_OperatorCalculator(low, highCalculator, ratio);
            }
            else if (!lowIsConst && !highIsConst && ratioIsConst)
            {
                calculator = new Exponent_WithConstRatio_OperatorCalculator(lowCalculator, highCalculator, ratio);
            }
            else if (lowIsConst && highIsConst && !ratioIsConst)
            {
                calculator = new Exponent_WithConstLowAndConstHigh_OperatorCalculator(low, high, ratioCalculator);
            }
            else if (!lowIsConst && highIsConst && !ratioIsConst)
            {
                calculator = new Exponent_WithConstHigh_OperatorCalculator(lowCalculator, high, ratioCalculator);
            }
            else if (lowIsConst && !highIsConst && !ratioIsConst)
            {
                calculator = new Exponent_WithConstLow_OperatorCalculator(low, highCalculator, ratioCalculator);
            }
            else
            {
                calculator = new Exponent_OperatorCalculator(lowCalculator, highCalculator, ratioCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitLoop(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase attackCalculator = _stack.Pop();
            OperatorCalculatorBase startCalculator = _stack.Pop();
            OperatorCalculatorBase sustainCalculator = _stack.Pop();
            OperatorCalculatorBase endCalculator = _stack.Pop();
            OperatorCalculatorBase releaseCalculator = _stack.Pop();

            calculator = new Loop_OperatorCalculator(signalCalculator, attackCalculator, startCalculator, sustainCalculator, endCalculator, releaseCalculator);

            _stack.Push(calculator);
        }

        protected override void VisitMultiply(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase operandACalculator = _stack.Pop();
            OperatorCalculatorBase operandBCalculator = _stack.Pop();
            OperatorCalculatorBase originCalculator = _stack.Pop();

            operandACalculator = operandACalculator ?? new Number_OperatorCalculator(1);
            operandBCalculator = operandBCalculator ?? new Number_OperatorCalculator(1);
            originCalculator = originCalculator ?? new Number_OperatorCalculator(0);

            double a = operandACalculator.Calculate(0, 0);
            double b = operandBCalculator.Calculate(0, 0);
            double origin = originCalculator.Calculate(0, 0);
            bool operandAIsConst = operandACalculator is Number_OperatorCalculator;
            bool operandBIsConst = operandBCalculator is Number_OperatorCalculator;
            bool originIsConst = originCalculator is Number_OperatorCalculator;
            bool operandAIsConstZero = operandAIsConst && a == 0;
            bool operandBIsConstZero = operandBIsConst && b == 0;
            bool originIsConstZero = originIsConst && origin == 0;
            bool operandAIsConstOne = operandAIsConst && a == 1;
            bool operandBIsConstOne = operandBIsConst && b == 1;

            if (operandAIsConstZero || operandBIsConstZero)
            {
                calculator = new Number_OperatorCalculator(0);
            }
            else if (operandAIsConstOne)
            {
                calculator = operandBCalculator;
            }
            else if (operandBIsConstOne)
            {
                calculator = operandACalculator;
            }
            else if (originIsConstZero && operandAIsConst && operandBIsConst)
            {
                calculator = new Number_OperatorCalculator(a * b);
            }
            else if (originIsConst && operandAIsConst && operandBIsConst)
            {
                double value = (a - origin) * b + origin;
                calculator = new Number_OperatorCalculator(value);
            }
            else if (originIsConstZero && operandAIsConst && !operandBIsConst)
            {
                calculator = new Multiply_WithoutOrigin_WithConstOperandA_OperatorCalculator(a, operandBCalculator);
            }
            else if (originIsConstZero && !operandAIsConst && operandBIsConst)
            {
                calculator = new Multiply_WithoutOrigin_WithConstOperandB_OperatorCalculator(operandACalculator, b);
            }
            else if (originIsConstZero && !operandAIsConst && !operandBIsConst)
            {
                calculator = new Multiply_WithoutOrigin_OperatorCalculator(operandACalculator, operandBCalculator);
            }
            else if (originIsConst && operandAIsConst && !operandBIsConst)
            {
                calculator = new Multiply_WithConstOrigin_AndOperandA_OperatorCalculator(a, operandBCalculator, origin);
            }
            else if (originIsConst && !operandAIsConst && operandBIsConst)
            {
                calculator = new Multiply_WithConstOrigin_AndOperandB_OperatorCalculator(operandACalculator, b, origin);
            }
            else if (!originIsConst && !operandAIsConst && !operandBIsConst)
            {
                calculator = new Multiply_WithConstOrigin_OperatorCalculator(operandACalculator, operandBCalculator, origin);
            }
            else if (!originIsConst && operandAIsConst && operandBIsConst)
            {
                calculator = new Multiply_WithOrigin_AndConstOperandA_AndOperandB_OperatorCalculator(a, b, originCalculator);
            }
            else if (!originIsConst && operandAIsConst && !operandBIsConst)
            {
                calculator = new Multiply_WithOrigin_AndConstOperandA_OperatorCalculator(a, operandBCalculator, originCalculator);
            }
            else if (!originIsConst && !operandAIsConst && operandBIsConst)
            {
                calculator = new Multiply_WithOrigin_AndConstOperandB_OperatorCalculator(operandACalculator, b, originCalculator);
            }
            else
            {
                calculator = new Multiply_WithOrigin_OperatorCalculator(operandACalculator, operandBCalculator, originCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitNumber(Operator op)
        {
            var wrapper = new OperatorWrapper_Number(op);
            double number = wrapper.Number;

            var calculator = new Number_OperatorCalculator(number);
            _stack.Push(calculator);
        }

        protected override void VisitPower(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase baseCalculator = _stack.Pop();
            OperatorCalculatorBase exponentCalculator = _stack.Pop();

            // When nulls should make the operator do nothing but pass the signal.
            if (exponentCalculator == null && baseCalculator != null)
            {
                _stack.Push(baseCalculator);
                return;
            }

            baseCalculator = baseCalculator ?? new Number_OperatorCalculator(0);
            exponentCalculator = exponentCalculator ?? new Number_OperatorCalculator(0);
            double @base = baseCalculator.Calculate(0, 0);
            double exponent = exponentCalculator.Calculate(0, 0);
            bool baseIsConst = baseCalculator is Number_OperatorCalculator;
            bool exponentIsConst = exponentCalculator is Number_OperatorCalculator;
            bool baseIsConstZero = baseIsConst && @base == 0;
            bool exponentIsConstZero = exponentIsConst && exponent == 0;

            if (baseIsConstZero)
            {
                calculator = new Number_OperatorCalculator(0);
            }
            else if (exponentIsConstZero)
            {
                calculator = baseCalculator;
            }
            else if (baseIsConst && exponentIsConst)
            {
                calculator = new Number_OperatorCalculator(Math.Pow(@base, exponent));
            }
            else if (baseIsConst)
            {
                calculator = new Power_WithConstBase_OperatorCalculator(@base, exponentCalculator);
            }
            else if (exponentIsConst)
            {
                calculator = new Power_WithConstExponent_OperatorCalculator(baseCalculator, exponent);
            }
            else
            {
                calculator = new Power_OperatorCalculator(baseCalculator, exponentCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitResample(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase samplingRateCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Number_OperatorCalculator(0);
            samplingRateCalculator = samplingRateCalculator ?? new Number_OperatorCalculator(0);

            double signal = signalCalculator.Calculate(0, 0);
            double samplingRate = samplingRateCalculator.Calculate(0, 0);
            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool samplingRateIsConst = samplingRateCalculator is Number_OperatorCalculator;
            bool signalIsConstZero = signalIsConst && signal == 0;
            bool samplingRateIsConstZero = samplingRateIsConst && samplingRate == 0;

            if (samplingRateIsConstZero)
            {
                // Weird number
                calculator = new Number_OperatorCalculator(0);
            }
            else if (signalIsConstZero)
            {
                calculator = new Number_OperatorCalculator(0);
            }
            else if (signalIsConst)
            {
                calculator = new Number_OperatorCalculator(signal);
            }
            // TODO: Uncomment if the specialized calculator is up-to-date.
            //else if (samplingRateIsConst)
            //{
            //    calculator = new Resample_WithConstSamplingRate_OperatorCalculator(signalCalculator, samplingRate);
            //}
            else
            {
                calculator = new Resample_OperatorCalculator(signalCalculator, samplingRateCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitSampleOperator(Operator op)
        {
            OperatorCalculatorBase calculator;

            var wrapper = new OperatorWrapper_Sample(op, _sampleRepository);

            SampleInfo sampleInfo = wrapper.SampleInfo;
            if (sampleInfo.Sample == null)
            {
                calculator = new Number_OperatorCalculator(0);
            }
            else
            {
                int sampleChannelCount = sampleInfo.Sample.GetChannelCount();
                if (sampleChannelCount == _channelCount)
                {
                    calculator = new Sample_OperatorCalculator(sampleInfo.Sample, sampleInfo.Bytes);
                }
                else if (sampleChannelCount == 1 && _channelCount == 2)
                {
                    calculator = new Sample_MonoToStereo_OperatorCalculator(sampleInfo.Sample, sampleInfo.Bytes);
                }
                else if (sampleChannelCount == 2 && _channelCount == 1)
                {
                    calculator = new Sample_StereoToMono_OperatorCalculator(sampleInfo.Sample, sampleInfo.Bytes);
                }
                else
                {
                    throw new Exception(String.Format("Combination of sampleChannelCount '{0}' and _channelCount '{1}' not supported.", sampleChannelCount, _channelCount));
                }
            }

            _stack.Push(calculator);
        }

        protected override void VisitSawTooth(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase pitchCalculator = _stack.Pop();
            OperatorCalculatorBase phaseShiftCalculator = _stack.Pop();

            pitchCalculator = pitchCalculator ?? new Number_OperatorCalculator(0);
            phaseShiftCalculator = phaseShiftCalculator ?? new Number_OperatorCalculator(0);
            double pitch = pitchCalculator.Calculate(0, 0);
            double phaseShift = phaseShiftCalculator.Calculate(0, 0);
            bool pitchIsConst = pitchCalculator is Number_OperatorCalculator;
            bool phaseShiftIsConst = phaseShiftCalculator is Number_OperatorCalculator;
            bool pitchIsConstZero = pitchIsConst && pitch == 0;
            bool phaseShiftIsConstZero = phaseShiftIsConst && phaseShift % 1 == 0;

            if (pitchIsConstZero)
            {
                // Weird number
                calculator = new Number_OperatorCalculator(0);
            }
            else if (pitchIsConst && phaseShiftIsConst)
            {
                calculator = new SawTooth_WithConstPitch_WithConstPhaseShift_OperatorCalculator(pitch, phaseShift);
            }
            else if (!pitchIsConst && phaseShiftIsConst)
            {
                calculator = new SawTooth_WithVarPitch_WithConstPhaseShift_OperatorCalculator(pitchCalculator, phaseShift);
            }
            else if (pitchIsConst && !phaseShiftIsConst)
            {
                calculator = new SawTooth_WithConstPitch_WithVarPhaseShift_OperatorCalculator(pitch, phaseShiftCalculator);
            }
            else
            {
                calculator = new SawTooth_WithVarPitch_WithVarPhaseShift_OperatorCalculator(pitchCalculator, phaseShiftCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitSelect(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Number_OperatorCalculator(0);
            timeCalculator = timeCalculator ?? new Number_OperatorCalculator(0);

            double signal = signalCalculator.Calculate(0, 0);
            double time = timeCalculator.Calculate(0, 0);
            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool timeIsConst = timeCalculator is Number_OperatorCalculator;

            if (signalIsConst)
            {
                calculator = new Number_OperatorCalculator(signal);
            }
            else if (timeIsConst)
            {
                calculator = new Select_WithConstTime_OperatorCalculator(signalCalculator, time);
            }
            else
            {
                calculator = new Select_OperatorCalculator(signalCalculator, timeCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitSine(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase volumeCalculator = _stack.Pop();
            OperatorCalculatorBase pitchCalculator = _stack.Pop();
            OperatorCalculatorBase originCalculator = _stack.Pop();
            OperatorCalculatorBase phaseShiftCalculator = _stack.Pop();

            volumeCalculator = volumeCalculator ?? new Number_OperatorCalculator(0);
            pitchCalculator = pitchCalculator ?? new Number_OperatorCalculator(0);
            originCalculator = originCalculator ?? new Number_OperatorCalculator(0);
            phaseShiftCalculator = phaseShiftCalculator ?? new Number_OperatorCalculator(0);
            double volume = volumeCalculator.Calculate(0, 0);
            double pitch = pitchCalculator.Calculate(0, 0);
            double origin = originCalculator.Calculate(0, 0);
            double phaseShift = phaseShiftCalculator.Calculate(0, 0);
            bool volumeIsConst = volumeCalculator is Number_OperatorCalculator;
            bool pitchIsConst = pitchCalculator is Number_OperatorCalculator;
            bool originIsConst = originCalculator is Number_OperatorCalculator;
            bool phaseShiftIsConst = phaseShiftCalculator is Number_OperatorCalculator;
            bool volumeIsConstZero = volumeIsConst && volume == 0;
            bool pitchIsConstZero = pitchIsConst && pitch == 0;
            bool originIsConstZero = originIsConst && origin == 0;
            bool phaseShiftIsConstZero = phaseShiftIsConst && phaseShift % 1 == 0;
            bool volumeIsConstOne = volumeIsConst && volume == 1; // Not used yet, but could be used for optimization too.

            if (volumeIsConstZero)
            {
                calculator = originCalculator;
            }
            else if (pitchIsConstZero)
            {
                // Weird number
                calculator = originCalculator;
            }
            else if (originIsConstZero && phaseShiftIsConstZero)
            {
                calculator = new Sine_OperatorCalculator(volumeCalculator, pitchCalculator);
            }
            else if (originIsConstZero && !phaseShiftIsConstZero)
            {
                calculator = new Sine_WithPhaseShift_OperatorCalculator(volumeCalculator, pitchCalculator, phaseShiftCalculator);
            }
            else if (!originIsConstZero && phaseShiftIsConstZero)
            {
                calculator = new Sine_WithOrigin_OperatorCalculator(volumeCalculator, pitchCalculator, originCalculator);
            }
            else
            {
                calculator = new Sine_WithOrigin_AndPhaseShift_OperatorCalculator(volumeCalculator, pitchCalculator, originCalculator, phaseShiftCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitSlowDown(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeMultiplierCalculator = _stack.Pop();
            OperatorCalculatorBase originCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Number_OperatorCalculator(0);
            timeMultiplierCalculator = timeMultiplierCalculator ?? new Number_OperatorCalculator(1);
            originCalculator = originCalculator ?? new Number_OperatorCalculator(0);

            double signal = signalCalculator.Calculate(0, 0);
            double timeMultiplier = timeMultiplierCalculator.Calculate(0, 0);
            double origin = originCalculator.Calculate(0, 0);
            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool timeMultiplierIsConst = timeMultiplierCalculator is Number_OperatorCalculator;
            bool originIsConst = originCalculator is Number_OperatorCalculator;
            bool signalIsConstZero = signalIsConst && signal == 0;
            bool timeMultiplierIsConstZero = timeMultiplierIsConst && timeMultiplier == 0;
            bool originIsConstZero = originIsConst && origin == 0;
            bool timeMultiplierIsConstOne = timeMultiplierIsConst && timeMultiplier == 1;

            if (timeMultiplierIsConstZero)
            {
                // Weird number
                calculator = new Number_OperatorCalculator(0);
            }
            else if (signalIsConstZero)
            {
                calculator = new Number_OperatorCalculator(0);
            }
            else if (timeMultiplierIsConstOne)
            {
                calculator = signalCalculator;
            }
            else if (signalIsConst)
            {
                calculator = signalCalculator;
            }
            else if (originIsConstZero && timeMultiplierIsConst)
            {
                calculator = new SlowDown_WithoutOrigin_WithConstTimeMultiplier_OperatorCalculator(signalCalculator, timeMultiplier);
            }
            else if (originIsConstZero && !timeMultiplierIsConst)
            {
                calculator = new SlowDown_WithoutOrigin_OperatorCalculator(signalCalculator, timeMultiplierCalculator);
            }
            else if (timeMultiplierIsConst)
            {
                calculator = new SlowDown_WithOrigin_WithConstTimeMultiplier_OperatorCalculator(signalCalculator, timeMultiplier, originCalculator);
            }
            else if (originIsConst)
            {
                calculator = new SlowDown_WithConstOrigin_OperatorCalculator(signalCalculator, timeMultiplierCalculator, origin);
            }
            else
            {
                calculator = new SlowDown_WithOrigin_OperatorCalculator(signalCalculator, timeMultiplierCalculator, originCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitSpeedUp(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeDividerCalculator = _stack.Pop();
            OperatorCalculatorBase originCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Number_OperatorCalculator(0);
            timeDividerCalculator = timeDividerCalculator ?? new Number_OperatorCalculator(1);
            originCalculator = originCalculator ?? new Number_OperatorCalculator(0);

            double signal = signalCalculator.Calculate(0, 0);
            double timeDivider = timeDividerCalculator.Calculate(0, 0);
            double origin = originCalculator.Calculate(0, 0);
            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool timeDividerIsConst = timeDividerCalculator is Number_OperatorCalculator;
            bool originIsConst = originCalculator is Number_OperatorCalculator;
            bool signalIsConstZero = signalIsConst && signal == 0;
            bool timeDividerIsConstZero = timeDividerIsConst && timeDivider == 0;
            bool originIsConstZero = originIsConst && origin == 0;
            bool timeDividerIsConstOne = timeDividerIsConst && timeDivider == 1;

            if (timeDividerIsConstZero)
            {
                calculator = new Number_OperatorCalculator(0);
            }
            else if (signalIsConstZero)
            {
                calculator = new Number_OperatorCalculator(0);
            }
            else if (timeDividerIsConstOne)
            {
                calculator = signalCalculator;
            }
            else if (signalIsConst)
            {
                calculator = signalCalculator;
            }
            else if (originIsConstZero && timeDividerIsConst)
            {
                calculator = new SpeedUp_WithoutOrigin_WithConstTimeDivider_OperatorCalculator(signalCalculator, timeDivider);
            }
            else if (originIsConstZero && !timeDividerIsConst)
            {
                calculator = new SpeedUp_WithoutOrigin_OperatorCalculator(signalCalculator, timeDividerCalculator);
            }
            else if (timeDividerIsConst)
            {
                calculator = new SpeedUp_WithOrigin_WithConstTimeDivider_OperatorCalculator(signalCalculator, timeDivider, originCalculator);
            }
            else if (originIsConst)
            {
                calculator = new SpeedUp_WithConstOrigin_OperatorCalculator(signalCalculator, timeDividerCalculator, origin);
            }
            else
            {
                calculator = new SpeedUp_WithOrigin_OperatorCalculator(signalCalculator, timeDividerCalculator, originCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitSquareWave(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase pitchCalculator = _stack.Pop();
            OperatorCalculatorBase phaseShiftCalculator = _stack.Pop();

            pitchCalculator = pitchCalculator ?? new Number_OperatorCalculator(0);
            phaseShiftCalculator = phaseShiftCalculator ?? new Number_OperatorCalculator(0);
            double pitch = pitchCalculator.Calculate(0, 0);
            double phaseShift = phaseShiftCalculator.Calculate(0, 0);
            bool pitchIsConst = pitchCalculator is Number_OperatorCalculator;
            bool phaseShiftIsConst = phaseShiftCalculator is Number_OperatorCalculator;
            bool pitchIsConstZero = pitchIsConst && pitch == 0;
            bool phaseShiftIsConstZero = phaseShiftIsConst && phaseShift % 1 == 0;

            if (pitchIsConstZero)
            {
                // Weird number
                calculator = new Number_OperatorCalculator(0);
            }
            else if (pitchIsConst && phaseShiftIsConst)
            {
                calculator = new SquareWave_WithConstPitch_WithConstPhaseShift_OperatorCalculator(pitch, phaseShift);
            }
            else if (!pitchIsConst && phaseShiftIsConst)
            {
                calculator = new SquareWave_WithVarPitch_WithConstPhaseShift_OperatorCalculator(pitchCalculator, phaseShift);
            }
            else if (pitchIsConst && !phaseShiftIsConst)
            {
                calculator = new SquareWave_WithConstPitch_WithVarPhaseShift_OperatorCalculator(pitch, phaseShiftCalculator);
            }
            else
            {
                calculator = new SquareWave_WithVarPitch_WithVarPhaseShift_OperatorCalculator(pitchCalculator, phaseShiftCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitSubstract(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase operandACalculator = _stack.Pop();
            OperatorCalculatorBase operandBCalculator = _stack.Pop();

            operandACalculator = operandACalculator ?? new Number_OperatorCalculator(0);
            operandBCalculator = operandBCalculator ?? new Number_OperatorCalculator(0);

            double a = operandACalculator.Calculate(0, 0);
            double b = operandBCalculator.Calculate(0, 0);
            bool operandAIsConst = operandACalculator is Number_OperatorCalculator;
            bool operandBIsConst = operandBCalculator is Number_OperatorCalculator;
            bool operandAIsConstZero = operandAIsConst && a == 0;
            bool operandBIsConstZero = operandBIsConst && b == 0;

            if (operandAIsConstZero && operandBIsConstZero)
            {
                calculator = new Number_OperatorCalculator(0);
            }
            else if (operandBIsConstZero)
            {
                calculator = operandACalculator;
            }
            else if (operandAIsConst && operandBIsConst)
            {
                calculator = new Number_OperatorCalculator(a - b);
            }
            else if (operandAIsConst)
            {
                calculator = new Substract_WithConstOperandA_OperatorCalculator(a, operandBCalculator);
            }
            else if (operandBIsConst)
            {
                calculator = new Substract_WithConstOperandB_OperatorCalculator(operandACalculator, b);
            }
            else
            {
                calculator = new Substract_OperatorCalculator(operandACalculator, operandBCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitTimeSubstract(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeDifferenceCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Number_OperatorCalculator(0);
            timeDifferenceCalculator = timeDifferenceCalculator ?? new Number_OperatorCalculator(0);
            double signal = signalCalculator.Calculate(0, 0);
            double timeDifference = timeDifferenceCalculator.Calculate(0, 0);
            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool timeDifferenceIsConst = timeDifferenceCalculator is Number_OperatorCalculator;
            bool signalIsConstZero = signalIsConst && signal == 0;
            bool timeDifferenceIsConstZero = timeDifferenceIsConst && signal == 0;

            if (signalIsConstZero)
            {
                calculator = new Number_OperatorCalculator(0);
            }
            else if (timeDifferenceIsConstZero)
            {
                calculator = signalCalculator;
            }
            else if (signalIsConst)
            {
                calculator = signalCalculator;
            }
            else if (timeDifferenceIsConst)
            {
                calculator = new TimeSubstract_WithConstTimeDifference_OperatorCalculator(signalCalculator, timeDifference);
            }
            else
            {
                calculator = new TimeSubstract_OperatorCalculator(signalCalculator, timeDifferenceCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitTimePower(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();;
            OperatorCalculatorBase exponentCalculator = _stack.Pop();
            OperatorCalculatorBase originCalculator = _stack.Pop();

            // When nulls should make the operator do nothing but pass the signal.
            if (exponentCalculator == null && signalCalculator != null)
            {
                _stack.Push(signalCalculator);
                return;
            }

            signalCalculator = signalCalculator ?? new Number_OperatorCalculator(0);
            exponentCalculator = exponentCalculator ?? new Number_OperatorCalculator(0);
            originCalculator = originCalculator ?? new Number_OperatorCalculator(0);

            double signal = signalCalculator.Calculate(0, 0);
            double exponent = exponentCalculator.Calculate(0, 0);
            double origin = originCalculator.Calculate(0, 0);
            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool exponentIsConst = exponentCalculator is Number_OperatorCalculator;
            bool originIsConst = originCalculator is Number_OperatorCalculator;
            bool signalIsConstZero = signalIsConst && signal == 0;
            bool exponentIsConstZero = exponentIsConst && exponent == 0;
            bool originIsConstZero = originIsConst && origin == 0;
            bool exponentIsConstOne = exponentIsConst && exponent == 1;

            if (signalIsConstZero)
            {
                calculator = new Number_OperatorCalculator(0);
            }
            else if (exponentIsConstZero)
            {
                calculator = new Number_OperatorCalculator(1); // TODO: I cannot image this one... Look into later.
            }
            else if (exponentIsConstOne)
            {
                calculator = signalCalculator;
            }
            else if (originIsConstZero)
            {
                calculator = new TimePower_WithoutOrigin_OperatorCalculator(signalCalculator, exponentCalculator);
            }
            else
            {
                calculator = new TimePower_WithOrigin_OperatorCalculator(signalCalculator, exponentCalculator, originCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitTriangleWave(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase pitchCalculator = _stack.Pop();
            OperatorCalculatorBase phaseShiftCalculator = _stack.Pop();

            pitchCalculator = pitchCalculator ?? new Number_OperatorCalculator(0);
            phaseShiftCalculator = phaseShiftCalculator ?? new Number_OperatorCalculator(0);
            double pitch = pitchCalculator.Calculate(0, 0);
            double phaseShift = phaseShiftCalculator.Calculate(0, 0);
            bool pitchIsConst = pitchCalculator is Number_OperatorCalculator;
            bool phaseShiftIsConst = phaseShiftCalculator is Number_OperatorCalculator;
            bool pitchIsConstZero = pitchIsConst && pitch == 0;

            if (pitchIsConstZero)
            {
                // Weird number
                calculator = new Number_OperatorCalculator(0);
            }
            else if (pitchIsConst && phaseShiftIsConst)
            {
                calculator = new TriangleWave_WithConstPitch_WithConstPhaseShift_OperatorCalculator(pitch, phaseShift);
            }
            else if (!pitchIsConst && phaseShiftIsConst)
            {
                calculator = new TriangleWave_WithVarPitch_WithConstPhaseShift_OperatorCalculator(pitchCalculator, phaseShift);
            }
            else if (pitchIsConst && !phaseShiftIsConst)
            {
                calculator = new TriangleWave_WithConstPitch_WithVarPhaseShift_OperatorCalculator(pitch, phaseShiftCalculator);
            }
            else
            {
                calculator = new TriangleWave_WithVarPitch_WithVarPhaseShift_OperatorCalculator(pitchCalculator, phaseShiftCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitWhiteNoise(Operator op)
        {
            var calculator = new WhiteNoise_OperatorCalculator(_whiteNoiseCalculator);
            _stack.Push(calculator);
        }

        /// <summary>
        /// Overridden to push null-inlets.
        /// </summary>
        protected override void VisitInlet(Inlet inlet)
        {
            if (inlet.InputOutlet == null)
            {
                _stack.Push(null);
            }

            base.VisitInlet(inlet);
        }

        protected override void VisitOutlet(Outlet outlet)
        {
            OperatorTypeEnum operatorTypeEnum = outlet.Operator.GetOperatorTypeEnum();
            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.CustomOperator:
                    VisitCustomOperatorOutlet(outlet);
                    break;

                case OperatorTypeEnum.Bundle:
                    VisitBundleOperatorOutlet(outlet);
                    break;

                case OperatorTypeEnum.Unbundle:
                    VisitUnbundleOperatorOutlet(outlet);
                    break;

                default:
                    base.VisitOutlet(outlet);
                    break;
            }
        }

        // TODO: Low Priority: Get rid of the asymmetry in the Operators with one outlet and the ones with multiple outlets.

        private void VisitCustomOperatorOutlet(Outlet outlet)
        {
            // As soon as you encounter a CustomOperator's Outlet,
            // the evaluation has to take a completely different course.
            Outlet customOperatorOutlet = outlet;
            Outlet patchOutletOutlet = PatchCalculationHelper.TryApplyCustomOperatorToUnderlyingPatch(customOperatorOutlet, _documentRepository);
            VisitOperator(patchOutletOutlet.Operator);
        }

        private void VisitUnbundleOperatorOutlet(Outlet outlet)
        {
            //return;

            Operator op = outlet.Operator;
            Inlet inlet = op.Inlets.Single();
            Outlet inputOutlet = inlet.InputOutlet;
            if (inputOutlet == null)
            {
                _stack.Push(new Number_OperatorCalculator(0));
                return;
            }

            int outletIndex = outlet.Operator.Outlets.IndexOf(outlet);

            _bundleIndexStack.Push(outletIndex);

            VisitOutlet(inputOutlet);

            _bundleIndexStack.Pop();
        }

        private void VisitBundleOperatorOutlet(Outlet outlet)
        {
            //return;

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
                _stack.Push(new Number_OperatorCalculator(0));
                return;
            }

            VisitOutlet(inlet.InputOutlet);

            _bundleIndexStack.Push(bundleIndex);
        }
    }
}
