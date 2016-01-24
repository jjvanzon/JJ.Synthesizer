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
using JJ.Business.Synthesizer.Calculation.Curves;
using JJ.Business.Synthesizer.Calculation.Samples;

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
        public class Result
        {
            public Result(
                IList<OperatorCalculatorBase> output_OperatorCalculators,
                IList<VariableInput_OperatorCalculator> input_OperatorCalculators)
            {
                Output_OperatorCalculators = output_OperatorCalculators;
                Input_OperatorCalculators = input_OperatorCalculators;
            }

            public IList<OperatorCalculatorBase> Output_OperatorCalculators { get; private set; }
            public IList<VariableInput_OperatorCalculator> Input_OperatorCalculators { get; private set; }
        }

        private readonly ICurveRepository _curveRepository;
        private readonly ISampleRepository _sampleRepository;
        private readonly IPatchRepository _patchRepository;

        private WhiteNoiseCalculator _whiteNoiseCalculator;

        private int _channelCount;
        private Stack<OperatorCalculatorBase> _stack;
        private Stack<int> _bundleIndexStack;

        /// <summary>
        /// This dictionary is about reusing the same CurveCalculator in multiple OperatorCalculator_Curve's
        /// in case they uses the same Curve, more than optimizing things by using a dictionary.
        /// </summary>
        private Dictionary<Curve, OptimizedCurveCalculator> _curve_CurveCalculator_Dictionary;

        /// <summary>
        /// This dictionary is about reusing the same SampleCalculator in multiple OperatorCalculator_Sample's
        /// in case they uses the same Sample, more than optimizing things by using a dictionary.
        /// </summary>
        private Dictionary<Sample, ISampleCalculator> _sample_SampleCalculator_Dictionary;

        private Dictionary<Operator, double> _operator_WhiteNoiseOffsetInSeconds_Dictionary;
        private Outlet _currentChannelOutlet;
        private Dictionary<Operator, VariableInput_OperatorCalculator> _patchInlet_Calculator_Dictionary;

        public OptimizedPatchCalculatorVisitor(ICurveRepository curveRepository, ISampleRepository sampleRepository, IPatchRepository patchRepository)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (patchRepository == null) throw new NullException(() => patchRepository);

            _curveRepository = curveRepository;
            _sampleRepository = sampleRepository;
            _patchRepository = patchRepository;
        }

        /// <summary> Returns an OperatorCalculator for each channel. Null-channels will get an OperatorCalculator too. </summary>
        /// <param name="channelOutlets">Can contain nulls.</param>
        public Result Execute(IList<Outlet> channelOutlets, WhiteNoiseCalculator whiteNoiseCalculator)
        {
            if (whiteNoiseCalculator == null) throw new NullException(() => whiteNoiseCalculator);
            if (channelOutlets == null) throw new NullException(() => channelOutlets);

            _whiteNoiseCalculator = whiteNoiseCalculator;

            foreach (Outlet channelOutlet in channelOutlets)
            {
                if (channelOutlet == null)
                {
                    continue;
                }

                IValidator validator = new OperatorValidator_Recursive(
                    channelOutlet.Operator, 
                    _curveRepository, _sampleRepository, _patchRepository,
                    alreadyDone: new HashSet<object>());
                validator.Assert();
            }

            _stack = new Stack<OperatorCalculatorBase>();
            _bundleIndexStack = new Stack<int>();
            _curve_CurveCalculator_Dictionary = new Dictionary<Curve, OptimizedCurveCalculator>();
            _sample_SampleCalculator_Dictionary = new Dictionary<Sample, ISampleCalculator>();
            _operator_WhiteNoiseOffsetInSeconds_Dictionary = new Dictionary<Operator, double>();
            _patchInlet_Calculator_Dictionary = new Dictionary<Operator, VariableInput_OperatorCalculator>();

            _channelCount = channelOutlets.Count;

            var outputOperatorCalculators = new List<OperatorCalculatorBase>(_channelCount);

            foreach (Outlet channelOutlet in channelOutlets)
            {
                if (channelOutlet == null)
                {
                    outputOperatorCalculators.Add(new Zero_OperatorCalculator());
                    continue;
                }

                _currentChannelOutlet = channelOutlet;

                VisitOutlet(channelOutlet);

                OperatorCalculatorBase operatorCalculator = _stack.Pop();

                if (_stack.Count != 0)
                {
                    throw new Exception("_stack.Count should have been 0.");
                }

                operatorCalculator = operatorCalculator ?? new Zero_OperatorCalculator(); 

                outputOperatorCalculators.Add(operatorCalculator);
            }

            return new Result(outputOperatorCalculators, _patchInlet_Calculator_Dictionary.Values.ToArray()); 
        }

        protected override void VisitAdd(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase operandACalculator = _stack.Pop();
            OperatorCalculatorBase operandBCalculator = _stack.Pop();

            operandACalculator = operandACalculator ?? new Zero_OperatorCalculator();
            operandBCalculator = operandBCalculator ?? new Zero_OperatorCalculator();

            double a = operandACalculator.Calculate(0, 0);
            double b = operandBCalculator.Calculate(0, 0);
            bool operandAIsConst = operandACalculator is Number_OperatorCalculator;
            bool operandBIsConst = operandBCalculator is Number_OperatorCalculator;
            bool operandAIsConstZero = operandAIsConst && a == 0;
            bool operandBIsConstZero = operandBIsConst && b == 0;

            if (operandAIsConstZero && operandBIsConstZero)
            {
                calculator = new Zero_OperatorCalculator();
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

            var wrapper = new Curve_OperatorWrapper(op, _curveRepository);
            Curve curve = wrapper.Curve;

            if (curve == null)
            {
                calculator = new Zero_OperatorCalculator();
            }
            else
            {
                OptimizedCurveCalculator curveCalculator;
                if (!_curve_CurveCalculator_Dictionary.TryGetValue(curve, out curveCalculator))
                {
                    curveCalculator = new OptimizedCurveCalculator(curve);
                    _curve_CurveCalculator_Dictionary.Add(curve, curveCalculator);
                }

                calculator = new Curve_OperatorCalculator(curveCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitDelay(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeDifferenceCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            timeDifferenceCalculator = timeDifferenceCalculator ?? new Zero_OperatorCalculator();
            double signal = signalCalculator.Calculate(0, 0);
            double timeDifference = timeDifferenceCalculator.Calculate(0, 0);
            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool timeDifferenceIsConst = timeDifferenceCalculator is Number_OperatorCalculator;
            bool signalIsConstZero = signalIsConst && signal == 0;
            bool timeDifferenceIsConstZero = timeDifferenceIsConst && timeDifference == 0;

            if (signalIsConstZero)
            {
                calculator = new Zero_OperatorCalculator();
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

            numeratorCalculator = numeratorCalculator ?? new Zero_OperatorCalculator();
            denominatorCalculator = denominatorCalculator ?? new One_OperatorCalculator();
            originCalculator = originCalculator ?? new Zero_OperatorCalculator();

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
                calculator = new Zero_OperatorCalculator();
            }
            else if (numeratorIsConstZero)
            {
                calculator = new Zero_OperatorCalculator();
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

            lowCalculator = lowCalculator ?? new Zero_OperatorCalculator();
            highCalculator = highCalculator ?? new Zero_OperatorCalculator();
            ratioCalculator = ratioCalculator ?? new Zero_OperatorCalculator();

            double low = lowCalculator.Calculate(0, 0);
            double high = highCalculator.Calculate(0, 0);
            double ratio = ratioCalculator.Calculate(0, 0);
            bool lowIsConst = lowCalculator is Number_OperatorCalculator;
            bool highIsConst = highCalculator is Number_OperatorCalculator;
            bool ratioIsConst = ratioCalculator is Number_OperatorCalculator;

            // TODO: Program more specialized cases?
            bool lowIsConstZero = lowIsConst && low == 0;
            bool highIsConstZero = lowIsConst && high == 0;
            //bool ratioIsConstZero = ratioIsConst && ratio == 0;
            //bool lowIsConstOne = lowIsConst && low == 1;
            //bool highIsConstOne = lowIsConst && high == 1;
            //bool ratioIsConstOne = ratioIsConst && ratio == 1;

            if (lowIsConstZero)
            {
                // Weird number.
                calculator = new Zero_OperatorCalculator();
            }
            else if (highIsConstZero)
            {
                // Would result in 0. See formula further down
                calculator = new Zero_OperatorCalculator();
            }
            else if (lowIsConst && highIsConst && ratioIsConst)
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

            operandACalculator = operandACalculator ?? new One_OperatorCalculator();
            operandBCalculator = operandBCalculator ?? new One_OperatorCalculator();
            originCalculator = originCalculator ?? new Zero_OperatorCalculator();

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
                calculator = new Zero_OperatorCalculator();
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
            else if (operandAIsConst && operandBIsConst && originIsConst)
            {
                double value = (a - origin) * b + origin;
                calculator = new Number_OperatorCalculator(value);
            }
            else if (operandAIsConst && !operandBIsConst && originIsConstZero)
            {
                calculator = new Multiply_ConstOperandA_VarOperandB_NoOrigin_OperatorCalculator(a, operandBCalculator);
            }
            else if (!operandAIsConst && operandBIsConst && originIsConstZero)
            {
                calculator = new Multiply_VarOperandA_ConstOperandB_NoOrigin_OperatorCalculator(operandACalculator, b);
            }
            else if (!operandAIsConst && !operandBIsConst && originIsConstZero)
            {
                calculator = new Multiply_VarOperandA_VarOperandB_NoOrigin_OperatorCalculator(operandACalculator, operandBCalculator);
            }
            else if (operandAIsConst && !operandBIsConst && originIsConst)
            {
                calculator = new Multiply_ConstOperandA_VarOperandB_ConstOrigin_OperatorCalculator(a, operandBCalculator, origin);
            }
            else if (!operandAIsConst && operandBIsConst && originIsConst)
            {
                calculator = new Multiply_VarOperandA_ConstOperandB_ConstOrigin_OperatorCalculator(operandACalculator, b, origin);
            }
            else if (!operandAIsConst && !operandBIsConst && originIsConst)
            {
                calculator = new Multiply_VarOperandA_VarOperandB_ConstOrigin_OperatorCalculator(operandACalculator, operandBCalculator, origin);
            }
            else if (operandAIsConst && operandBIsConst && !originIsConst)
            {
                calculator = new Multiply_ConstOperandA_ConstOperandB_VarOrigin_OperatorCalculator(a, b, originCalculator);
            }
            else if (operandAIsConst && !operandBIsConst && !originIsConst)
            {
                calculator = new Multiply_ConstOperandA_VarOperandB_VarOrigin_OperatorCalculator(a, operandBCalculator, originCalculator);
            }
            else if (!operandAIsConst && operandBIsConst && !originIsConst)
            {
                calculator = new Multiply_VarOperandA_ConstOperandB_VarOrigin_OperatorCalculator(operandACalculator, b, originCalculator);
            }
            else
            {
                calculator = new Multiply_VarOperandA_VarOperandB_VarOrigin_OperatorCalculator(operandACalculator, operandBCalculator, originCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitNarrower(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase factorCalculator = _stack.Pop();
            OperatorCalculatorBase originCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            factorCalculator = factorCalculator ?? new One_OperatorCalculator();
            originCalculator = originCalculator ?? new Zero_OperatorCalculator();

            double signal = signalCalculator.Calculate(0, 0);
            double factor = factorCalculator.Calculate(0, 0);
            double origin = originCalculator.Calculate(0, 0);

            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool factorIsConst = factorCalculator is Number_OperatorCalculator;
            bool originIsConst = originCalculator is Number_OperatorCalculator;

            bool signalIsConstZero = signalIsConst && signal == 0;
            bool factorIsConstZero = factorIsConst && factor == 0;
            bool originIsConstZero = originIsConst && origin == 0;

            bool factorIsConstOne = factorIsConst && factor == 1;

            bool signalIsConstSpecialNumber = signalIsConst && Double.IsNaN(signal) || Double.IsInfinity(signal);
            bool factorIsConstSpecialNumber = factorIsConst && Double.IsNaN(factor) || Double.IsInfinity(factor);
            bool originIsConstSpecialNumber = originIsConst && Double.IsNaN(origin) || Double.IsInfinity(origin);

            if (signalIsConstSpecialNumber || factorIsConstSpecialNumber || originIsConstSpecialNumber)
            {
                // Wierd number
                calculator = new Number_OperatorCalculator(Double.NaN);
            }
            else if (factorIsConstZero)
            {
                // Weird number
                // I cannot hack this one other than to return NaN.
                // Slow down of 0 means speed up to infinity, wich only renders a number if the signal time = origin,
                // which is very rare.
                calculator = new Number_OperatorCalculator(Double.NaN);
            }
            else if (signalIsConstZero)
            {
                calculator = new Zero_OperatorCalculator();
            }
            else if (factorIsConstOne)
            {
                calculator = signalCalculator;
            }
            else if (signalIsConst)
            {
                calculator = signalCalculator;
            }
            else if (!signalIsConst && factorIsConst && originIsConstZero)
            {
                calculator = new Narrower_VarSignal_ConstFactor_ZeroOrigin_OperatorCalculator(signalCalculator, factor);
            }
            else if (!signalIsConst && !factorIsConst && originIsConstZero)
            {
                calculator = new Narrower_VarSignal_VarFactor_ZeroOrigin_OperatorCalculator(signalCalculator, factorCalculator);
            }
            else if (!signalIsConst && factorIsConst && originIsConst)
            {
                calculator = new Narrower_VarSignal_ConstFactor_ConstOrigin_OperatorCalculator(signalCalculator, factor, origin);
            }
            else if (!signalIsConst && factorIsConst && !originIsConst)
            {
                calculator = new Narrower_VarSignal_ConstFactor_VarOrigin_OperatorCalculator(signalCalculator, factor, originCalculator);
            }
            else if (!signalIsConst && !factorIsConst && originIsConst)
            {
                calculator = new Narrower_VarSignal_VarFactor_ConstOrigin_OperatorCalculator(signalCalculator, factorCalculator, origin);
            }
            else if (!signalIsConst && !factorIsConst && !originIsConst)
            {
                calculator = new Narrower_VarSignal_VarFactor_VarOrigin_OperatorCalculator(signalCalculator, factorCalculator, originCalculator);
            }
            else
            {
                throw new Exception("Error in Narrower Operator optimization. No approproate variation on the calculation was found.");
            }

            _stack.Push(calculator);
        }

        protected override void VisitNumber(Operator op)
        {
            var wrapper = new Number_OperatorWrapper(op);
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

            baseCalculator = baseCalculator ?? new Zero_OperatorCalculator();
            exponentCalculator = exponentCalculator ?? new Zero_OperatorCalculator();
            double @base = baseCalculator.Calculate(0, 0);
            double exponent = exponentCalculator.Calculate(0, 0);
            bool baseIsConst = baseCalculator is Number_OperatorCalculator;
            bool exponentIsConst = exponentCalculator is Number_OperatorCalculator;
            bool baseIsConstZero = baseIsConst && @base == 0;
            bool exponentIsConstZero = exponentIsConst && exponent == 0;

            if (baseIsConstZero)
            {
                calculator = new Zero_OperatorCalculator();
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

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            samplingRateCalculator = samplingRateCalculator ?? new Zero_OperatorCalculator();

            double signal = signalCalculator.Calculate(0, 0);
            double samplingRate = samplingRateCalculator.Calculate(0, 0);
            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool samplingRateIsConst = samplingRateCalculator is Number_OperatorCalculator;
            bool signalIsConstZero = signalIsConst && signal == 0;
            bool samplingRateIsConstZero = samplingRateIsConst && samplingRate == 0;

            if (samplingRateIsConstZero)
            {
                // Weird number
                calculator = new Zero_OperatorCalculator();
            }
            else if (signalIsConstZero)
            {
                calculator = new Zero_OperatorCalculator();
            }
            else if (signalIsConst)
            {
                calculator = new Number_OperatorCalculator(signal);
            }
            else
            {
                calculator = new Resample_OperatorCalculator(signalCalculator, samplingRateCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitSampleOperator(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase frequencyCalculator = _stack.Pop();

            frequencyCalculator = frequencyCalculator ?? new Zero_OperatorCalculator();
            double frequency = frequencyCalculator.Calculate(0, 0);
            bool frequencyIsConst = frequencyCalculator is Number_OperatorCalculator;
            bool frequencyIsConstZero = frequencyIsConst && frequency == 0.0;

            var wrapper = new Sample_OperatorWrapper(op, _sampleRepository);

            SampleInfo sampleInfo = wrapper.SampleInfo;
            if (sampleInfo.Sample == null)
            {
                calculator = new Zero_OperatorCalculator();
            }
            else if (frequencyIsConstZero)
            {
                // Weird number
                calculator = new Zero_OperatorCalculator();
            }
            else
            {
                // Get SampleCalculator
                ISampleCalculator sampleCalculator;
                if (!_sample_SampleCalculator_Dictionary.TryGetValue(sampleInfo.Sample, out sampleCalculator))
                {
                    sampleCalculator = SampleCalculatorFactory.CreateSampleCalculator(sampleInfo.Sample, sampleInfo.Bytes);
                    _sample_SampleCalculator_Dictionary.Add(sampleInfo.Sample, sampleCalculator);
                }

                int sampleChannelCount = sampleInfo.Sample.GetChannelCount();

                if (sampleChannelCount == _channelCount)
                {
                    if (frequencyIsConst)
                    {
                        calculator = new Sample_WithConstFrequency_OperatorCalculator(frequency, sampleCalculator);
                    }
                    else
                    {
                        calculator = new Sample_WithVarFrequency_OperatorCalculator(frequencyCalculator, sampleCalculator);
                    }
                }
                else if (sampleChannelCount == 1 && _channelCount == 2)
                {
                    if (frequencyIsConst)
                    {
                        calculator = new Sample_WithConstFrequency_MonoToStereo_OperatorCalculator(frequency, sampleCalculator);
                    }
                    else
                    {
                        calculator = new Sample_WithVarFrequency_MonoToStereo_OperatorCalculator(frequencyCalculator, sampleCalculator);
                    }
                }
                else if (sampleChannelCount == 2 && _channelCount == 1)
                {
                    if (frequencyIsConst)
                    {
                        calculator = new Sample_WithConstFrequency_StereoToMono_OperatorCalculator(frequency, sampleCalculator);
                    }
                    else
                    {
                        calculator = new Sample_WithVarFrequency_StereoToMono_OperatorCalculator(frequencyCalculator, sampleCalculator);
                    }
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

            OperatorCalculatorBase frequencyCalculator = _stack.Pop();
            OperatorCalculatorBase phaseShiftCalculator = _stack.Pop();

            frequencyCalculator = frequencyCalculator ?? new Zero_OperatorCalculator();
            phaseShiftCalculator = phaseShiftCalculator ?? new Zero_OperatorCalculator();
            double frequency = frequencyCalculator.Calculate(0, 0);
            double phaseShift = phaseShiftCalculator.Calculate(0, 0);
            bool frequencyIsConst = frequencyCalculator is Number_OperatorCalculator;
            bool phaseShiftIsConst = phaseShiftCalculator is Number_OperatorCalculator;
            bool frequencyIsConstZero = frequencyIsConst && frequency == 0;
            bool phaseShiftIsConstZero = phaseShiftIsConst && phaseShift % 1 == 0;

            if (frequencyIsConstZero)
            {
                // Weird number
                calculator = new Zero_OperatorCalculator();
            }
            else if (frequencyIsConst && phaseShiftIsConst)
            {
                calculator = new SawTooth_WithConstFrequency_WithConstPhaseShift_OperatorCalculator(frequency, phaseShift);
            }
            else if (!frequencyIsConst && phaseShiftIsConst)
            {
                calculator = new SawTooth_WithVarFrequency_WithConstPhaseShift_OperatorCalculator(frequencyCalculator, phaseShift);
            }
            else if (frequencyIsConst && !phaseShiftIsConst)
            {
                calculator = new SawTooth_WithConstFrequency_WithVarPhaseShift_OperatorCalculator(frequency, phaseShiftCalculator);
            }
            else
            {
                calculator = new SawTooth_WithVarFrequency_WithVarPhaseShift_OperatorCalculator(frequencyCalculator, phaseShiftCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitSelect(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            timeCalculator = timeCalculator ?? new Zero_OperatorCalculator();

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

            OperatorCalculatorBase frequencyCalculator = _stack.Pop();
            OperatorCalculatorBase phaseShiftCalculator = _stack.Pop();

            frequencyCalculator = frequencyCalculator ?? new Zero_OperatorCalculator();
            phaseShiftCalculator = phaseShiftCalculator ?? new Zero_OperatorCalculator();
            double frequency = frequencyCalculator.Calculate(0, 0);
            double phaseShift = phaseShiftCalculator.Calculate(0, 0);
            bool frequencyIsConst = frequencyCalculator is Number_OperatorCalculator;
            bool phaseShiftIsConst = phaseShiftCalculator is Number_OperatorCalculator;
            bool frequencyIsConstZero = frequencyIsConst && frequency == 0.0;
            bool phaseShiftIsConstZero = phaseShiftIsConst && phaseShift % 1.0 == 0.0;

            if (frequencyIsConstZero)
            {
                // Weird number
                calculator = new Zero_OperatorCalculator();
            }
            else if (frequencyIsConst && phaseShiftIsConstZero)
            {
                calculator = new Sine_WithConstFrequency_WithoutPhaseShift_OperatorCalculator(frequency);
            }
            else if (frequencyIsConst && phaseShiftIsConst)
            {
                calculator = new Sine_WithConstFrequency_WithConstPhaseShift_OperatorCalculator(frequency, phaseShift);
            }
            else if (frequencyIsConst && !phaseShiftIsConst)
            {
                calculator = new Sine_WithConstFrequency_WithVarPhaseShift_OperatorCalculator(frequency, phaseShiftCalculator);
            }
            else if (!frequencyIsConst && phaseShiftIsConstZero)
            {
                calculator = new Sine_WithVarFrequency_WithoutPhaseShift_OperatorCalculator(frequencyCalculator);
            }
            else if (!frequencyIsConst && phaseShiftIsConst)
            {
                calculator = new Sine_WithVarFrequency_WithConstPhaseShift_OperatorCalculator(frequencyCalculator, phaseShift);
            }
            else
            {
                calculator = new Sine_WithVarFrequency_WithVarPhaseShift_OperatorCalculator(frequencyCalculator, phaseShiftCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitSlowDown(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase factorCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            factorCalculator = factorCalculator ?? new One_OperatorCalculator();

            double signal = signalCalculator.Calculate(0, 0);
            double factor = factorCalculator.Calculate(0, 0);

            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool factorIsConst = factorCalculator is Number_OperatorCalculator;

            bool signalIsConstZero = signalIsConst && signal == 0;
            bool factorIsConstZero = factorIsConst && factor == 0;

            bool factorIsConstOne = factorIsConst && factor == 1;

            bool signalIsConstSpecialNumber = signalIsConst && Double.IsNaN(signal) || Double.IsInfinity(signal);
            bool factorIsConstSpecialNumber = factorIsConst && Double.IsNaN(factor) || Double.IsInfinity(factor);

            if (factorIsConstSpecialNumber)
            {
                // Weird number
                // Slow down to inifinity, means time stands still. (Consider: 2x as slow, 100x as slow, inifity as slow...)
                calculator = new Number_OperatorCalculator(signal);
            }
            if (signalIsConstSpecialNumber)
            {
                // Weird number
                calculator = new Number_OperatorCalculator(signal);
            }
            else if (factorIsConstZero)
            {
                // Weird number
                // Slow down 0 times, means speed up to infinity, equals undefined.
                calculator = new Number_OperatorCalculator(Double.NaN);
            }
            else if (signalIsConstZero)
            {
                calculator = new Zero_OperatorCalculator();
            }
            else if (factorIsConstOne)
            {
                calculator = signalCalculator;
            }
            else if (signalIsConst)
            {
                calculator = signalCalculator;
            }
            else if (factorIsConst)
            {
                calculator = new SlowDown_WithConstFactor_OperatorCalculator(signalCalculator, factor);
            }
            else
            {
                calculator = new SlowDown_WithVarFactor_OperatorCalculator(signalCalculator, factorCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitSpeedUp(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase factorCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            factorCalculator = factorCalculator ?? new One_OperatorCalculator();

            double signal = signalCalculator.Calculate(0, 0);
            double factor = factorCalculator.Calculate(0, 0);
            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool factorIsConst = factorCalculator is Number_OperatorCalculator;
            bool signalIsConstZero = signalIsConst && signal == 0;
            bool factorIsConstZero = factorIsConst && factor == 0;
            bool factorIsConstOne = factorIsConst && factor == 1;
            bool signalIsConstSpecialNumber = signalIsConst && Double.IsNaN(signal) || Double.IsInfinity(signal);
            bool factorIsConstSpecialNumber = factorIsConst && Double.IsNaN(factor) || Double.IsInfinity(factor);

            if (factorIsConstSpecialNumber)
            {
                // Weird number
                calculator = new Number_OperatorCalculator(Double.NaN);
            }
            else if (signalIsConstSpecialNumber)
            {
                // Weird number
                calculator = new Number_OperatorCalculator(signal);
            }
            else if (factorIsConstZero)
            {
                // Weird number
                // Speed-up of 0 means time stands still.
                calculator = new Number_OperatorCalculator(signal);
            }
            else if (signalIsConstZero)
            {
                calculator = new Zero_OperatorCalculator();
            }
            else if (factorIsConstOne)
            {
                calculator = signalCalculator;
            }
            else if (signalIsConst)
            {
                calculator = signalCalculator;
            }
            else if (factorIsConst)
            {
                calculator = new SpeedUp_WithConstFactor_OperatorCalculator(signalCalculator, factor);
            }
            else 
            {
                calculator = new SpeedUp_WithVarFactor_OperatorCalculator(signalCalculator, factorCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitSquareWave(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase frequencyCalculator = _stack.Pop();
            OperatorCalculatorBase phaseShiftCalculator = _stack.Pop();

            frequencyCalculator = frequencyCalculator ?? new Zero_OperatorCalculator();
            phaseShiftCalculator = phaseShiftCalculator ?? new Zero_OperatorCalculator();
            double frequency = frequencyCalculator.Calculate(0, 0);
            double phaseShift = phaseShiftCalculator.Calculate(0, 0);
            bool frequencyIsConst = frequencyCalculator is Number_OperatorCalculator;
            bool phaseShiftIsConst = phaseShiftCalculator is Number_OperatorCalculator;
            bool frequencyIsConstZero = frequencyIsConst && frequency == 0;
            bool phaseShiftIsConstZero = phaseShiftIsConst && phaseShift % 1 == 0;

            if (frequencyIsConstZero)
            {
                // Weird number
                calculator = new Zero_OperatorCalculator();
            }
            else if (frequencyIsConst && phaseShiftIsConst)
            {
                calculator = new SquareWave_WithConstFrequency_WithConstPhaseShift_OperatorCalculator(frequency, phaseShift);
            }
            else if (!frequencyIsConst && phaseShiftIsConst)
            {
                calculator = new SquareWave_WithVarFrequency_WithConstPhaseShift_OperatorCalculator(frequencyCalculator, phaseShift);
            }
            else if (frequencyIsConst && !phaseShiftIsConst)
            {
                calculator = new SquareWave_WithConstFrequency_WithVarPhaseShift_OperatorCalculator(frequency, phaseShiftCalculator);
            }
            else
            {
                calculator = new SquareWave_WithVarFrequency_WithVarPhaseShift_OperatorCalculator(frequencyCalculator, phaseShiftCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitSubtract(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase operandACalculator = _stack.Pop();
            OperatorCalculatorBase operandBCalculator = _stack.Pop();

            operandACalculator = operandACalculator ?? new Zero_OperatorCalculator();
            operandBCalculator = operandBCalculator ?? new Zero_OperatorCalculator();

            double a = operandACalculator.Calculate(0, 0);
            double b = operandBCalculator.Calculate(0, 0);
            bool operandAIsConst = operandACalculator is Number_OperatorCalculator;
            bool operandBIsConst = operandBCalculator is Number_OperatorCalculator;
            bool operandAIsConstZero = operandAIsConst && a == 0;
            bool operandBIsConstZero = operandBIsConst && b == 0;

            if (operandAIsConstZero && operandBIsConstZero)
            {
                calculator = new Zero_OperatorCalculator();
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
                calculator = new Subtract_WithConstOperandA_OperatorCalculator(a, operandBCalculator);
            }
            else if (operandBIsConst)
            {
                calculator = new Subtract_WithConstOperandB_OperatorCalculator(operandACalculator, b);
            }
            else
            {
                calculator = new Subtract_OperatorCalculator(operandACalculator, operandBCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitStretch(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase factorCalculator = _stack.Pop();
            OperatorCalculatorBase originCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            factorCalculator = factorCalculator ?? new One_OperatorCalculator();
            originCalculator = originCalculator ?? new Zero_OperatorCalculator();

            double signal = signalCalculator.Calculate(0, 0);
            double factor = factorCalculator.Calculate(0, 0);
            double origin = originCalculator.Calculate(0, 0);

            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool factorIsConst = factorCalculator is Number_OperatorCalculator;
            bool originIsConst = originCalculator is Number_OperatorCalculator;

            bool signalIsConstZero = signalIsConst && signal == 0;
            bool factorIsConstZero = factorIsConst && factor == 0;
            bool originIsConstZero = originIsConst && origin == 0;

            bool factorIsConstOne = factorIsConst && factor == 1;

            // TODO: Handle const special numbers.

            if (factorIsConstZero)
            {
                // Weird number
                calculator = new Number_OperatorCalculator(Double.NaN);
            }
            else if (signalIsConstZero)
            {
                calculator = new Zero_OperatorCalculator();
            }
            else if (factorIsConstOne)
            {
                calculator = signalCalculator;
            }
            else if (signalIsConst)
            {
                calculator = signalCalculator;
            }
            else if (!signalIsConst && factorIsConst && originIsConstZero)
            {
                calculator = new Stretch_VarSignal_ConstFactor_ZeroOrigin_OperatorCalculator(signalCalculator, factor);
            }
            else if (!signalIsConst && !factorIsConst && originIsConstZero)
            {
                calculator = new Stretch_VarSignal_VarFactor_ZeroOrigin_OperatorCalculator(signalCalculator, factorCalculator);
            }
            else if (!signalIsConst && factorIsConst && originIsConst)
            {
                calculator = new Stretch_VarSignal_ConstFactor_ConstOrigin_OperatorCalculator(signalCalculator, factor, origin);
            }
            else if (!signalIsConst && factorIsConst && !originIsConst)
            {
                calculator = new Stretch_VarSignal_ConstFactor_VarOrigin_OperatorCalculator(signalCalculator, factor, originCalculator);
            }
            else if (!signalIsConst && !factorIsConst && originIsConst)
            {
                calculator = new Stretch_VarSignal_VarFactor_ConstOrigin_OperatorCalculator(signalCalculator, factorCalculator, origin);
            }
            else if (!signalIsConst && !factorIsConst && !originIsConst)
            {
                calculator = new Stretch_VarSignal_VarFactor_VarOrigin_OperatorCalculator(signalCalculator, factorCalculator, originCalculator);
            }
            else
            {
                throw new Exception("Error in Stretch Operator optimization. No approproate variation on the calculation was found.");
            }

            _stack.Push(calculator);
        }

        protected override void VisitEarlier(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeDifferenceCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            timeDifferenceCalculator = timeDifferenceCalculator ?? new Zero_OperatorCalculator();
            double signal = signalCalculator.Calculate(0, 0);
            double timeDifference = timeDifferenceCalculator.Calculate(0, 0);
            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool timeDifferenceIsConst = timeDifferenceCalculator is Number_OperatorCalculator;
            bool signalIsConstZero = signalIsConst && signal == 0;
            bool timeDifferenceIsConstZero = timeDifferenceIsConst && signal == 0;

            if (signalIsConstZero)
            {
                calculator = new Zero_OperatorCalculator();
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
                calculator = new Earlier_WithConstTimeDifference_OperatorCalculator(signalCalculator, timeDifference);
            }
            else
            {
                calculator = new Earlier_OperatorCalculator(signalCalculator, timeDifferenceCalculator);
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

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            exponentCalculator = exponentCalculator ?? new Zero_OperatorCalculator();
            originCalculator = originCalculator ?? new Zero_OperatorCalculator();

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
                calculator = new Zero_OperatorCalculator();
            }
            else if (exponentIsConstZero)
            {
                calculator = new One_OperatorCalculator(); // TODO: I cannot image this one... Look into later.
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

            OperatorCalculatorBase frequencyCalculator = _stack.Pop();
            OperatorCalculatorBase phaseShiftCalculator = _stack.Pop();

            frequencyCalculator = frequencyCalculator ?? new Zero_OperatorCalculator();
            phaseShiftCalculator = phaseShiftCalculator ?? new Zero_OperatorCalculator();
            double frequency = frequencyCalculator.Calculate(0, 0);
            double phaseShift = phaseShiftCalculator.Calculate(0, 0);
            bool frequencyIsConst = frequencyCalculator is Number_OperatorCalculator;
            bool phaseShiftIsConst = phaseShiftCalculator is Number_OperatorCalculator;
            bool frequencyIsConstZero = frequencyIsConst && frequency == 0;

            if (frequencyIsConstZero)
            {
                // Weird number
                calculator = new Zero_OperatorCalculator();
            }
            else if (frequencyIsConst && phaseShiftIsConst)
            {
                calculator = new TriangleWave_WithConstFrequency_WithConstPhaseShift_OperatorCalculator(frequency, phaseShift);
            }
            else if (!frequencyIsConst && phaseShiftIsConst)
            {
                calculator = new TriangleWave_WithVarFrequency_WithConstPhaseShift_OperatorCalculator(frequencyCalculator, phaseShift);
            }
            else if (frequencyIsConst && !phaseShiftIsConst)
            {
                calculator = new TriangleWave_WithConstFrequency_WithVarPhaseShift_OperatorCalculator(frequency, phaseShiftCalculator);
            }
            else
            {
                calculator = new TriangleWave_WithVarFrequency_WithVarPhaseShift_OperatorCalculator(frequencyCalculator, phaseShiftCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitWhiteNoise(Operator op)
        {
            double offset;
            if (!_operator_WhiteNoiseOffsetInSeconds_Dictionary.TryGetValue(op, out offset))
            {
                offset = _whiteNoiseCalculator.GetRandomOffset();
                _operator_WhiteNoiseOffsetInSeconds_Dictionary.Add(op, offset);
            }

            var calculator = new WhiteNoise_OperatorCalculator(_whiteNoiseCalculator, offset);
            _stack.Push(calculator);
        }

        // Special Visitation

        /// <summary> Overridden to push null-inlets or default values for those inlets. </summary>
        protected override void VisitInlet(Inlet inlet)
        {
            if (inlet.InputOutlet == null)
            {
                if (inlet.DefaultValue.HasValue)
                {
                    _stack.Push(new Number_OperatorCalculator(inlet.DefaultValue.Value));
                }
                else
                {
                    _stack.Push(null);
                }
            }

            base.VisitInlet(inlet);
        }

        protected override void VisitPatchInlet(Operator patchInlet)
        {
            var wrapper = new PatchInlet_OperatorWrapper(patchInlet);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase inputCalculator = _stack.Pop();

            inputCalculator = inputCalculator ?? new Zero_OperatorCalculator();
            double input = inputCalculator.Calculate(0, 0);
            double defaultValue = wrapper.Inlet.DefaultValue ?? 0.0;
            bool inputIsConst = inputCalculator is Number_OperatorCalculator;
            bool inputIsConstDefaultValue = inputIsConst && input == defaultValue;
            bool isTopLevelPatchInlet = IsTopLevelPatchInlet(patchInlet);

            // Only top-level PatchInlets are values controllable from the outside.
            // For foreward compatibility we only apply that rule if nothing else was filled in.
            if (isTopLevelPatchInlet && inputIsConstDefaultValue)
            {
                VariableInput_OperatorCalculator variableInputCalculator;
                if (!_patchInlet_Calculator_Dictionary.TryGetValue(patchInlet, out variableInputCalculator))
                {
                    Inlet inlet = wrapper.Inlet;

                    variableInputCalculator = new VariableInput_OperatorCalculator
                    (
                        inletTypeEnum: inlet.GetInletTypeEnum(),
                        name: wrapper.Name,
                        listIndex: wrapper.ListIndex ?? 0,
                        defaultValue: inlet.DefaultValue ?? 0.0
                    );

                    _patchInlet_Calculator_Dictionary.Add(patchInlet, variableInputCalculator);
                }

                calculator = variableInputCalculator;
            }
            else
            {
                calculator = inputCalculator;
            }

            _stack.Push(calculator);
        }

        private bool IsTopLevelPatchInlet(Operator op)
        {
            if (op.GetOperatorTypeEnum() != OperatorTypeEnum.PatchInlet)
            {
                return false;
            }

            return op.Patch.ID == _currentChannelOutlet.Operator.Patch.ID;
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
            Outlet patchOutlet_Outlet = PatchCalculationHelper.TryApplyCustomOperatorToUnderlyingPatch(customOperatorOutlet, _patchRepository);

            if (patchOutlet_Outlet == null)
            {
                throw new Exception("patchOutlet_Outlet was null after TryApplyCustomOperatorToUnderlyingPatch.");
            }

            VisitOperator(patchOutlet_Outlet.Operator);
        }

        private void VisitUnbundleOperatorOutlet(Outlet outlet)
        {
            Operator op = outlet.Operator;
            Inlet inlet = op.Inlets.Single();
            Outlet inputOutlet = inlet.InputOutlet;
            if (inputOutlet == null)
            {
                _stack.Push(new Zero_OperatorCalculator());
                return;
            }

            int outletIndex = outlet.Operator.Outlets.IndexOf(outlet);

            _bundleIndexStack.Push(outletIndex);

            VisitOutlet(inputOutlet);

            _bundleIndexStack.Pop();
        }

        private void VisitBundleOperatorOutlet(Outlet outlet)
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
                double defaultValue = inlet.DefaultValue ?? 0.0;
                _stack.Push(new Number_OperatorCalculator(defaultValue));
            }
            else
            {
                VisitOutlet(inlet.InputOutlet);
            }

            _bundleIndexStack.Push(bundleIndex);
        }
    }
}
