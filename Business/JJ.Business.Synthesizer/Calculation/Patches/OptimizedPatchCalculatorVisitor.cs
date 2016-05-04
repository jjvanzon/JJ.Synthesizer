using System;
using System.Collections.Generic;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Curves;
using JJ.Business.Synthesizer.Calculation.Samples;
using System.Reflection;
using JJ.Business.Synthesizer.Calculation.Random;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Framework.Common.Exceptions;
using JJ.Business.Synthesizer.Validation.Operators;

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
                OperatorCalculatorBase output_OperatorCalculator,
                IList<VariableInput_OperatorCalculator> input_OperatorCalculators,
                IList<ResettableOperatorTuple> resettableOperatorTuples)
            {
                Output_OperatorCalculator = output_OperatorCalculator;
                Input_OperatorCalculators = input_OperatorCalculators;
                ResettableOperatorTuples = resettableOperatorTuples;
            }

            public OperatorCalculatorBase Output_OperatorCalculator { get; private set; }
            public IList<VariableInput_OperatorCalculator> Input_OperatorCalculators { get; private set; }
            public IList<ResettableOperatorTuple> ResettableOperatorTuples { get; private set; }
        }

        public class ResettableOperatorTuple
        {
            public ResettableOperatorTuple(OperatorCalculatorBase operatorCalculator, string name, int? listIndex)
            {
                if (operatorCalculator == null) throw new NullException(() => operatorCalculator);
                // Name is optional.

                Name = name;
                ListIndex = listIndex;
                OperatorCalculator = operatorCalculator;
            }

            public OperatorCalculatorBase OperatorCalculator { get; private set; }
            public string Name { get; private set; }
            public int? ListIndex { get; private set; }
        }

        private const double DEFAULT_PULSE_WIDTH = 0.5;

        private readonly ICurveRepository _curveRepository;
        private readonly ISampleRepository _sampleRepository;
        private readonly IPatchRepository _patchRepository;
        private readonly ISpeakerSetupRepository _speakerSetupRepository;
        private readonly CalculatorCache _calculatorCache;

        private DimensionStack _defaultDimensionStack;
        private int _channelCount;
        private Stack<OperatorCalculatorBase> _stack;
        private Stack<int> _bundleIndexStack;

        private Dictionary<Operator, double> _operator_NoiseOffsetInSeconds_Dictionary;
        private Dictionary<Operator, int> _operator_RandomOffsetInSeconds_Dictionary;
        private Outlet _outlet;
        private Dictionary<Operator, VariableInput_OperatorCalculator> _patchInlet_Calculator_Dictionary;
        private IList<ResettableOperatorTuple> _resettableOperatorTuples;

        public OptimizedPatchCalculatorVisitor(
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            IPatchRepository patchRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            CalculatorCache calculatorCache)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (patchRepository == null) throw new NullException(() => patchRepository);
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);
            if (calculatorCache == null) throw new NullException(() => calculatorCache);

            _curveRepository = curveRepository;
            _sampleRepository = sampleRepository;
            _patchRepository = patchRepository;
            _speakerSetupRepository = speakerSetupRepository;
            _calculatorCache = calculatorCache;
        }

        /// <param name="channelCount">Used for e.g. mixing channels of samples into one channel.</param>
        public Result Execute(Outlet outlet, int channelCount = 2)
        {
            if (outlet == null) throw new NullException(() => outlet);

            IValidator validator = new Recursive_OperatorValidator(
                outlet.Operator,
                _curveRepository, _sampleRepository, _patchRepository,
                alreadyDone: new HashSet<object>());
            validator.Assert();

            _stack = new Stack<OperatorCalculatorBase>();
            _bundleIndexStack = new Stack<int>();
            _operator_NoiseOffsetInSeconds_Dictionary = new Dictionary<Operator, double>();
            _operator_RandomOffsetInSeconds_Dictionary = new Dictionary<Operator, int>();
            _patchInlet_Calculator_Dictionary = new Dictionary<Operator, VariableInput_OperatorCalculator>();
            _resettableOperatorTuples = new List<ResettableOperatorTuple>();
            _defaultDimensionStack = new DimensionStack();

            _outlet = outlet;
            _channelCount = channelCount;

            VisitOutlet(outlet);

            OperatorCalculatorBase outputOperatorCalculator = _stack.Pop();

            if (_stack.Count != 0)
            {
                throw new Exception("_stack.Count should have been 0.");
            }

            return new Result(
                outputOperatorCalculator,
                _patchInlet_Calculator_Dictionary.Values.ToArray(),
                _resettableOperatorTuples);
        }

        protected override void VisitAbsolute(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase calculatorX = _stack.Pop();

            calculatorX = calculatorX ?? new Zero_OperatorCalculator();

            double x = calculatorX.Calculate(_defaultDimensionStack);

            bool xIsConst = calculatorX is Number_OperatorCalculator;

            if (xIsConst)
            {
                double value;

                if (x >= 0.0)
                {
                    value = x;
                }
                else
                {
                    value = -x;
                }

                calculator = new Number_OperatorCalculator(value);
            }
            else if (!xIsConst)
            {
                calculator = new Absolute_OperatorCalculator(calculatorX);
            }
            else
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
            }

            _stack.Push(calculator);
        }

        protected override void VisitAdd(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase aCalculator = _stack.Pop();
            OperatorCalculatorBase bCalculator = _stack.Pop();

            aCalculator = aCalculator ?? new Zero_OperatorCalculator();
            bCalculator = bCalculator ?? new Zero_OperatorCalculator();

            double a = aCalculator.Calculate(_defaultDimensionStack);
            double b = bCalculator.Calculate(_defaultDimensionStack);
            bool aIsConst = aCalculator is Number_OperatorCalculator;
            bool bIsConst = bCalculator is Number_OperatorCalculator;
            bool aIsConstZero = aIsConst && a == 0;
            bool bIsConstZero = bIsConst && b == 0;

            if (aIsConstZero && bIsConstZero)
            {
                calculator = new Zero_OperatorCalculator();
            }
            else if (aIsConstZero)
            {
                calculator = bCalculator;
            }
            else if (bIsConstZero)
            {
                calculator = aCalculator;
            }
            else if (aIsConst && bIsConst)
            {
                calculator = new Number_OperatorCalculator(a + b);
            }
            else if (aIsConst)
            {
                calculator = new Add_ConstA_VarB_OperatorCalculator(a, bCalculator);
            }
            else if (bIsConst)
            {
                calculator = new Add_VarA_ConstB_OperatorCalculator(aCalculator, b);
            }
            else
            {
                calculator = new Add_OperatorCalculator(aCalculator, bCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitAnd(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase calculatorA = _stack.Pop();
            OperatorCalculatorBase calculatorB = _stack.Pop();

            calculatorA = calculatorA ?? new Zero_OperatorCalculator();
            calculatorB = calculatorB ?? new Zero_OperatorCalculator();

            double a = calculatorA.Calculate(_defaultDimensionStack);
            double b = calculatorB.Calculate(_defaultDimensionStack);

            bool aIsConst = calculatorA is Number_OperatorCalculator;
            bool bIsConst = calculatorB is Number_OperatorCalculator;

            if (aIsConst && bIsConst)
            {
                double value;

                bool aIsTrue = a != 0.0;
                bool bIsTrue = b != 0.0;

                if (aIsTrue && bIsTrue) value = 1.0;
                else value = 0.0;

                calculator = new Number_OperatorCalculator(value);
            }
            else if (!aIsConst && bIsConst)
            {
                calculator = new And_VarA_ConstB_OperatorCalculator(calculatorA, b);
            }
            else if (aIsConst && !bIsConst)
            {
                calculator = new And_ConstA_VarB_OperatorCalculator(a, calculatorB);
            }
            else if (!aIsConst && !bIsConst)
            {
                calculator = new And_VarA_VarB_OperatorCalculator(calculatorA, calculatorB);
            }
            else
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
            }

            _stack.Push(calculator);
        }

        protected override void VisitAverage(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeSliceDurationCalculator = _stack.Pop();
            OperatorCalculatorBase sampleCountCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            double signal = signalCalculator.Calculate(_defaultDimensionStack);
            bool signalIsConst = signalCalculator is Number_OperatorCalculator;

            // TODO: Do not use these magic defaults, but give standard operators default inlet value functionality.
            timeSliceDurationCalculator = timeSliceDurationCalculator ?? new Number_OperatorCalculator(0.02);
            sampleCountCalculator = sampleCountCalculator ?? new Number_OperatorCalculator(100.0);

            var wrapper = new Average_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;

            if (signalIsConst)
            {
                calculator = signalCalculator;
            }
            else
            {
                calculator = new Average_OperatorCalculator(signalCalculator, timeSliceDurationCalculator, sampleCountCalculator, dimensionEnum);
            }

            _stack.Push(calculator);
        }

        protected override void VisitChangeTrigger(Operator op)
        {
            OperatorCalculatorBase operatorCalculator;

            OperatorCalculatorBase calculationCalculator = _stack.Pop();
            OperatorCalculatorBase resetCalculator = _stack.Pop();

            calculationCalculator = calculationCalculator ?? new Zero_OperatorCalculator();
            resetCalculator = resetCalculator ?? new Zero_OperatorCalculator();

            bool calculationIsConst = calculationCalculator is Number_OperatorCalculator;
            bool resetIsConst = resetCalculator is Number_OperatorCalculator;

            if (calculationIsConst)
            {
                operatorCalculator = calculationCalculator;
            }
            else if (resetIsConst)
            {
                operatorCalculator = calculationCalculator;
            }
            else
            {
                operatorCalculator = new ChangeTrigger_OperatorCalculator(calculationCalculator, resetCalculator);
            }

            _stack.Push(operatorCalculator);
        }

        protected override void VisitCache(Operator op)
        {
            OperatorCalculatorBase calculator = null;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase startDateCalculator = _stack.Pop();
            OperatorCalculatorBase endDateCalculator = _stack.Pop();
            OperatorCalculatorBase samplingRateCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            // TODO: Do not use these magic defaults, but give standard operators default inlet value functionality.
            startDateCalculator = startDateCalculator ?? new Number_OperatorCalculator(0.0);
            endDateCalculator = endDateCalculator ?? new Number_OperatorCalculator(1.0);
            samplingRateCalculator = samplingRateCalculator ?? new Number_OperatorCalculator(44100.0);

            double signal = signalCalculator.Calculate(_defaultDimensionStack);
            double startTime = startDateCalculator.Calculate(_defaultDimensionStack);
            double endTime = endDateCalculator.Calculate(_defaultDimensionStack);
            double samplingRate = samplingRateCalculator.Calculate(_defaultDimensionStack);

            bool signalIsConst = signalCalculator is Number_OperatorCalculator;

            var wrapper = new Cache_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;

            // We need a lot of lenience in this code, because validity is dependent on user input,
            // and this validity cannot be checked on the entity level, only when starting the calculation.
            // In theory I could generate additional messages in the calculation optimization process,
            // but we should keep it possible to reoptimize in runtime, and we cannot obtrusively interrupt
            // the user with validation messages, because he is busy making music and the show must go on.
            bool startTimeIsValid = !Double.IsNaN(startTime) && !Double.IsInfinity(startTime);
            bool endTimeIsValid = !Double.IsNaN(endTime) && !Double.IsInfinity(endTime);
            bool samplingRateIsValid = ConversionHelper.CanCastToInt32(samplingRate) && (int)samplingRate > 0;
            bool startTimeComparedToEndTimeIsValid = endTime > startTime;
            bool valuesAreValid = startTimeIsValid &&
                                  endTimeIsValid &&
                                  samplingRateIsValid &&
                                  startTimeComparedToEndTimeIsValid;
            if (!valuesAreValid)
            {
                calculator = new Number_OperatorCalculator(Double.NaN);
            }
            else if (signalIsConst)
            {
                calculator = signalCalculator;
            }
            else
            {
                IList<ArrayCalculatorBase> arrayCalculators = _calculatorCache.GetCacheArrayCalculators(
                    op, signalCalculator, startTime, endTime, (int)samplingRate, _speakerSetupRepository);

                bool hasMinTime = startTime != 0.0;
                InterpolationTypeEnum interpolationTypeEnum = wrapper.InterpolationType;

                if (hasMinTime)
                {
                    if (arrayCalculators.Count == 1)
                    {
                        ArrayCalculatorBase arrayCalculator = arrayCalculators[0];
                        {
                            var castedArrayCalculator = arrayCalculator as ArrayCalculator_MinPosition_Block;
                            if (castedArrayCalculator != null)
                            {
                                calculator = new Cache_OperatorCalculator_SingleChannel<ArrayCalculator_MinPosition_Block>(castedArrayCalculator, dimensionEnum);
                            }
                        }
                        {
                            var castedArrayCalculator = arrayCalculator as ArrayCalculator_MinPosition_Cubic;
                            if (castedArrayCalculator != null)
                            {
                                calculator = new Cache_OperatorCalculator_SingleChannel<ArrayCalculator_MinPosition_Cubic>(castedArrayCalculator, dimensionEnum);
                            }
                        }
                        {
                            var castedArrayCalculator = arrayCalculator as ArrayCalculator_MinPosition_Hermite;
                            if (castedArrayCalculator != null)
                            {
                                calculator = new Cache_OperatorCalculator_SingleChannel<ArrayCalculator_MinPosition_Hermite>(castedArrayCalculator, dimensionEnum);
                            }
                        }
                        {
                            var castedArrayCalculator = arrayCalculator as ArrayCalculator_MinPosition_Line;
                            if (castedArrayCalculator != null)
                            {
                                calculator = new Cache_OperatorCalculator_SingleChannel<ArrayCalculator_MinPosition_Line>(castedArrayCalculator, dimensionEnum);
                            }
                        }
                        {
                            var castedArrayCalculator = arrayCalculator as ArrayCalculator_MinPosition_Stripe;
                            if (castedArrayCalculator != null)
                            {
                                calculator = new Cache_OperatorCalculator_SingleChannel<ArrayCalculator_MinPosition_Stripe>(castedArrayCalculator, dimensionEnum);
                            }
                        }
                    }
                    else
                    {
                        // arrayCalculators.Count != 1

                        switch (interpolationTypeEnum)
                        {
                            case InterpolationTypeEnum.Block:
                                {
                                    IList<ArrayCalculator_MinPosition_Block> castedArrayCalculators = arrayCalculators.Select(x => (ArrayCalculator_MinPosition_Block)x).ToArray();
                                    calculator = new Cache_OperatorCalculator_MultiChannel<ArrayCalculator_MinPosition_Block>(castedArrayCalculators, dimensionEnum);
                                    break;
                                }

                            case InterpolationTypeEnum.Cubic:
                                {
                                    IList<ArrayCalculator_MinPosition_Cubic> castedArrayCalculators = arrayCalculators.Select(x => (ArrayCalculator_MinPosition_Cubic)x).ToArray();
                                    calculator = new Cache_OperatorCalculator_MultiChannel<ArrayCalculator_MinPosition_Cubic>(castedArrayCalculators, dimensionEnum);
                                    break;
                                }

                            case InterpolationTypeEnum.Hermite:
                                {
                                    IList<ArrayCalculator_MinPosition_Hermite> castedArrayCalculators = arrayCalculators.Select(x => (ArrayCalculator_MinPosition_Hermite)x).ToArray();
                                    calculator = new Cache_OperatorCalculator_MultiChannel<ArrayCalculator_MinPosition_Hermite>(castedArrayCalculators, dimensionEnum);
                                    break;
                                }

                            case InterpolationTypeEnum.Line:
                                {
                                    IList<ArrayCalculator_MinPosition_Line> castedArrayCalculators = arrayCalculators.Select(x => (ArrayCalculator_MinPosition_Line)x).ToArray();
                                    calculator = new Cache_OperatorCalculator_MultiChannel<ArrayCalculator_MinPosition_Line>(castedArrayCalculators, dimensionEnum);
                                    break;
                                }

                            case InterpolationTypeEnum.Stripe:
                                {
                                    IList<ArrayCalculator_MinPosition_Stripe> castedArrayCalculators = arrayCalculators.Select(x => (ArrayCalculator_MinPosition_Stripe)x).ToArray();
                                    calculator = new Cache_OperatorCalculator_MultiChannel<ArrayCalculator_MinPosition_Stripe>(castedArrayCalculators, dimensionEnum);
                                    break;
                                }

                            default:
                                throw new ValueNotSupportedException(interpolationTypeEnum);
                        }
                    }
                }
                else
                {
                    // !hasMinTime
                    if (arrayCalculators.Count == 1)
                    {
                        ArrayCalculatorBase arrayCalculator = arrayCalculators[0];
                        {
                            var castedArrayCalculator = arrayCalculator as ArrayCalculator_MinPositionZero_Block;
                            if (castedArrayCalculator != null)
                            {
                                calculator = new Cache_OperatorCalculator_SingleChannel<ArrayCalculator_MinPositionZero_Block>(castedArrayCalculator, dimensionEnum);
                            }
                        }
                        {
                            var castedArrayCalculator = arrayCalculator as ArrayCalculator_MinPositionZero_Cubic;
                            if (castedArrayCalculator != null)
                            {
                                calculator = new Cache_OperatorCalculator_SingleChannel<ArrayCalculator_MinPositionZero_Cubic>(castedArrayCalculator, dimensionEnum);
                            }
                        }
                        {
                            var castedArrayCalculator = arrayCalculator as ArrayCalculator_MinPositionZero_Hermite;
                            if (castedArrayCalculator != null)
                            {
                                calculator = new Cache_OperatorCalculator_SingleChannel<ArrayCalculator_MinPositionZero_Hermite>(castedArrayCalculator, dimensionEnum);
                            }
                        }
                        {
                            var castedArrayCalculator = arrayCalculator as ArrayCalculator_MinPositionZero_Line;
                            if (castedArrayCalculator != null)
                            {
                                calculator = new Cache_OperatorCalculator_SingleChannel<ArrayCalculator_MinPositionZero_Line>(castedArrayCalculator, dimensionEnum);
                            }
                        }
                        {
                            var castedArrayCalculator = arrayCalculator as ArrayCalculator_MinPositionZero_Stripe;
                            if (castedArrayCalculator != null)
                            {
                                calculator = new Cache_OperatorCalculator_SingleChannel<ArrayCalculator_MinPositionZero_Stripe>(castedArrayCalculator, dimensionEnum);
                            }
                        }
                    }
                    else
                    {
                        // arrayCalculators.Count != 1

                        switch (interpolationTypeEnum)
                        {
                            case InterpolationTypeEnum.Block:
                                {
                                    IList<ArrayCalculator_MinPositionZero_Block> castedArrayCalculators = arrayCalculators.Select(x => (ArrayCalculator_MinPositionZero_Block)x).ToArray();
                                    calculator = new Cache_OperatorCalculator_MultiChannel<ArrayCalculator_MinPositionZero_Block>(castedArrayCalculators, dimensionEnum);
                                    break;
                                }

                            case InterpolationTypeEnum.Cubic:
                                {
                                    IList<ArrayCalculator_MinPositionZero_Cubic> castedArrayCalculators = arrayCalculators.Select(x => (ArrayCalculator_MinPositionZero_Cubic)x).ToArray();
                                    calculator = new Cache_OperatorCalculator_MultiChannel<ArrayCalculator_MinPositionZero_Cubic>(castedArrayCalculators, dimensionEnum);
                                    break;
                                }

                            case InterpolationTypeEnum.Hermite:
                                {
                                    IList<ArrayCalculator_MinPositionZero_Hermite> castedArrayCalculators = arrayCalculators.Select(x => (ArrayCalculator_MinPositionZero_Hermite)x).ToArray();
                                    calculator = new Cache_OperatorCalculator_MultiChannel<ArrayCalculator_MinPositionZero_Hermite>(castedArrayCalculators, dimensionEnum);
                                    break;
                                }

                            case InterpolationTypeEnum.Line:
                                {
                                    IList<ArrayCalculator_MinPositionZero_Line> castedArrayCalculators = arrayCalculators.Select(x => (ArrayCalculator_MinPositionZero_Line)x).ToArray();
                                    calculator = new Cache_OperatorCalculator_MultiChannel<ArrayCalculator_MinPositionZero_Line>(castedArrayCalculators, dimensionEnum);
                                    break;
                                }

                            case InterpolationTypeEnum.Stripe:
                                {
                                    IList<ArrayCalculator_MinPositionZero_Stripe> castedArrayCalculators = arrayCalculators.Select(x => (ArrayCalculator_MinPositionZero_Stripe)x).ToArray();
                                    calculator = new Cache_OperatorCalculator_MultiChannel<ArrayCalculator_MinPositionZero_Stripe>(castedArrayCalculators, dimensionEnum);
                                    break;
                                }

                            default:
                                throw new ValueNotSupportedException(interpolationTypeEnum);
                        }
                    }
                }
            }

            if (calculator == null)
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
            }

            _stack.Push(calculator);
        }

        protected override void VisitCurveOperator(Operator op)
        {
            OperatorCalculatorBase calculator = null;

            var wrapper = new Curve_OperatorWrapper(op, _curveRepository);
            Curve curve = wrapper.Curve;
            DimensionEnum dimensionEnum = wrapper.Dimension;

            if (curve == null)
            {
                calculator = new Zero_OperatorCalculator();
            }
            else
            {
                ICurveCalculator curveCalculator = _calculatorCache.GetCurveCalculator(curve);

                var curveCalculator_MinTime = curveCalculator as CurveCalculator_MinX;
                if (curveCalculator_MinTime != null)
                {
                    calculator = new Curve_MinX_OperatorCalculator(curveCalculator_MinTime, dimensionEnum);
                }

                var curveCalculator_MinTimeZero = curveCalculator as CurveCalculator_MinXZero;
                if (curveCalculator_MinTimeZero != null)
                {
                    calculator = new Curve_MinXZero_OperatorCalculator(curveCalculator_MinTimeZero, dimensionEnum);
                }
            }

            if (calculator == null)
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
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
            double signal = signalCalculator.Calculate(_defaultDimensionStack);
            double timeDifference = timeDifferenceCalculator.Calculate(_defaultDimensionStack);
            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool timeDifferenceIsConst = timeDifferenceCalculator is Number_OperatorCalculator;
            bool signalIsConstZero = signalIsConst && signal == 0;
            bool timeDifferenceIsConstZero = timeDifferenceIsConst && timeDifference == 0;

            var wrapper = new Delay_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;

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
                calculator = new Delay_VarSignal_ConstTimeDifference_OperatorCalculator(signalCalculator, timeDifference, dimensionEnum);
            }
            else
            {
                calculator = new Delay_OperatorCalculator(signalCalculator, timeDifferenceCalculator, dimensionEnum);
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

            double numerator = numeratorCalculator.Calculate(_defaultDimensionStack);
            double denominator = denominatorCalculator.Calculate(_defaultDimensionStack);
            double origin = originCalculator.Calculate(_defaultDimensionStack);
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
                calculator = new Divide_ConstNumerator_VarDenominator_ZeroOrigin_OperatorCalculator(numerator, denominatorCalculator);
            }
            else if (originIsConstZero && !numeratorIsConst && denominatorIsConst)
            {
                calculator = new Divide_VarNumerator_ConstDenominator_ZeroOrigin_OperatorCalculator(numeratorCalculator, denominator);
            }
            else if (originIsConstZero && !numeratorIsConst && !denominatorIsConst)
            {
                calculator = new Divide_WithoutOrigin_OperatorCalculator(numeratorCalculator, denominatorCalculator);
            }
            else if (originIsConst && numeratorIsConst && !denominatorIsConst)
            {
                calculator = new Divide_ConstNumerator_VarDenominator_ConstOrigin_OperatorCalculator(numerator, denominatorCalculator, origin);
            }
            else if (originIsConst && !numeratorIsConst && denominatorIsConst)
            {
                calculator = new Divide_VarNumerator_ConstDenominator_ConstOrigin_OperatorCalculator(numeratorCalculator, denominator, origin);
            }
            else if (originIsConst && !numeratorIsConst && !denominatorIsConst)
            {
                calculator = new Divide_VarNumerator_VarDenominator_ConstOrigin_OperatorCalculator(numeratorCalculator, denominatorCalculator, origin);
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

        protected override void VisitEarlier(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeDifferenceCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            timeDifferenceCalculator = timeDifferenceCalculator ?? new Zero_OperatorCalculator();
            double signal = signalCalculator.Calculate(_defaultDimensionStack);
            double timeDifference = timeDifferenceCalculator.Calculate(_defaultDimensionStack);
            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool timeDifferenceIsConst = timeDifferenceCalculator is Number_OperatorCalculator;
            bool signalIsConstZero = signalIsConst && signal == 0;
            bool timeDifferenceIsConstZero = timeDifferenceIsConst && signal == 0;

            var wrapper = new Earlier_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;

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
                calculator = new Earlier_WithConstTimeDifference_OperatorCalculator(signalCalculator, timeDifference, dimensionEnum);
            }
            else
            {
                calculator = new Earlier_OperatorCalculator(signalCalculator, timeDifferenceCalculator, dimensionEnum);
            }

            _stack.Push(calculator);
        }

        protected override void VisitEqual(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase calculatorA = _stack.Pop();
            OperatorCalculatorBase calculatorB = _stack.Pop();

            calculatorA = calculatorA ?? new Zero_OperatorCalculator();
            calculatorB = calculatorB ?? new Zero_OperatorCalculator();

            double a = calculatorA.Calculate(_defaultDimensionStack);
            double b = calculatorB.Calculate(_defaultDimensionStack);

            bool aIsConst = calculatorA is Number_OperatorCalculator;
            bool bIsConst = calculatorB is Number_OperatorCalculator;

            if (aIsConst && bIsConst)
            {
                double value;

                if (a == b) value = 1.0;
                else value = 0.0;

                calculator = new Number_OperatorCalculator(value);
            }
            else if (!aIsConst && bIsConst)
            {
                calculator = new Equal_VarA_ConstB_OperatorCalculator(calculatorA, b);
            }
            else if (aIsConst && !bIsConst)
            {
                calculator = new Equal_ConstA_VarB_OperatorCalculator(a, calculatorB);
            }
            else if (!aIsConst && !bIsConst)
            {
                calculator = new Equal_VarA_VarB_OperatorCalculator(calculatorA, calculatorB);
            }
            else
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
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

            double low = lowCalculator.Calculate(_defaultDimensionStack);
            double high = highCalculator.Calculate(_defaultDimensionStack);
            double ratio = ratioCalculator.Calculate(_defaultDimensionStack);
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

        protected override void VisitFilter(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase frequencyCalculator = _stack.Pop();
            OperatorCalculatorBase bandWidthCalculator = _stack.Pop();
            OperatorCalculatorBase dbGainCalculator = _stack.Pop();
            OperatorCalculatorBase shelfSlopeCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            frequencyCalculator = frequencyCalculator ?? new Zero_OperatorCalculator();
            bandWidthCalculator = bandWidthCalculator ?? new Zero_OperatorCalculator();
            dbGainCalculator = dbGainCalculator ?? new Zero_OperatorCalculator();
            shelfSlopeCalculator = shelfSlopeCalculator ?? new Zero_OperatorCalculator();

            double signal = signalCalculator.Calculate(_defaultDimensionStack);
            double frequency = frequencyCalculator.Calculate(_defaultDimensionStack);
            double bandWidth = bandWidthCalculator.Calculate(_defaultDimensionStack);
            double dbGain = dbGainCalculator.Calculate(_defaultDimensionStack);
            double shelfSlope = shelfSlopeCalculator.Calculate(_defaultDimensionStack);

            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool frequencyIsConst = frequencyCalculator is Number_OperatorCalculator;
            bool bandWidthIsConst = bandWidthCalculator is Number_OperatorCalculator;
            bool dbGainIsConst = dbGainCalculator is Number_OperatorCalculator;
            bool shelfSlopeIsConst = shelfSlopeCalculator is Number_OperatorCalculator;

            if (signalIsConst)
            {
                // There are no frequencies. So you a filter should do nothing.
                calculator = signalCalculator;
            }
            else
            {
                var wrapper = new Filter_OperatorWrapper(op);
                calculator = new Filter_ManyConstants_OperatorCalculator(
                    signalCalculator,
                    frequency,
                    bandWidth,
                    dbGain,
                    shelfSlope,
                    wrapper.FilterTypeEnum);
            }

            _stack.Push(calculator);
        }

        protected override void VisitGetDimension(Operator op)
        {
            var wrapper = new GetDimension_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;

            var calculator = new GetDimension_OperatorCalculator(dimensionEnum);
            _stack.Push(calculator);
        }

        protected override void VisitGreaterThan(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase calculatorA = _stack.Pop();
            OperatorCalculatorBase calculatorB = _stack.Pop();

            calculatorA = calculatorA ?? new Zero_OperatorCalculator();
            calculatorB = calculatorB ?? new Zero_OperatorCalculator();

            double a = calculatorA.Calculate(_defaultDimensionStack);
            double b = calculatorB.Calculate(_defaultDimensionStack);

            bool aIsConst = calculatorA is Number_OperatorCalculator;
            bool bIsConst = calculatorB is Number_OperatorCalculator;

            if (aIsConst && bIsConst)
            {
                double value;

                if (a > b) value = 1.0;
                else value = 0.0;

                calculator = new Number_OperatorCalculator(value);
            }
            else if (!aIsConst && bIsConst)
            {
                calculator = new GreaterThan_VarA_ConstB_OperatorCalculator(calculatorA, b);
            }
            else if (aIsConst && !bIsConst)
            {
                calculator = new GreaterThan_ConstA_VarB_OperatorCalculator(a, calculatorB);
            }
            else if (!aIsConst && !bIsConst)
            {
                calculator = new GreaterThan_VarA_VarB_OperatorCalculator(calculatorA, calculatorB);
            }
            else
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
            }

            _stack.Push(calculator);
        }

        protected override void VisitGreaterThanOrEqual(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase calculatorA = _stack.Pop();
            OperatorCalculatorBase calculatorB = _stack.Pop();

            calculatorA = calculatorA ?? new Zero_OperatorCalculator();
            calculatorB = calculatorB ?? new Zero_OperatorCalculator();

            double a = calculatorA.Calculate(_defaultDimensionStack);
            double b = calculatorB.Calculate(_defaultDimensionStack);

            bool aIsConst = calculatorA is Number_OperatorCalculator;
            bool bIsConst = calculatorB is Number_OperatorCalculator;

            if (aIsConst && bIsConst)
            {
                double value;

                if (a >= b) value = 1.0;
                else value = 0.0;

                calculator = new Number_OperatorCalculator(value);
            }
            else if (!aIsConst && bIsConst)
            {
                calculator = new GreaterThanOrEqual_VarA_ConstB_OperatorCalculator(calculatorA, b);
            }
            else if (aIsConst && !bIsConst)
            {
                calculator = new GreaterThanOrEqual_ConstA_VarB_OperatorCalculator(a, calculatorB);
            }
            else if (!aIsConst && !bIsConst)
            {
                calculator = new GreaterThanOrEqual_VarA_VarB_OperatorCalculator(calculatorA, calculatorB);
            }
            else
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
            }

            _stack.Push(calculator);
        }

        protected override void VisitHighPassFilter(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase minFrequencyCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            minFrequencyCalculator = minFrequencyCalculator ?? new Zero_OperatorCalculator();

            double signal = signalCalculator.Calculate(_defaultDimensionStack);
            double minFrequency = minFrequencyCalculator.Calculate(_defaultDimensionStack);

            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool minFrequencyIsConst = minFrequencyCalculator is Number_OperatorCalculator;

            bool minFrequencyIsConstZero = minFrequencyIsConst && minFrequency == 0.0;

            if (signalIsConst)
            {
                calculator = signalCalculator;
            }
            else if (minFrequencyIsConstZero)
            {
                // No filtering
                calculator = signalCalculator;
            }
            else if (minFrequencyIsConst)
            {
                calculator = new HighPassFilter_ConstMinFrequency_OperatorCalculator(signalCalculator, minFrequency);
            }
            else
            {
                calculator = new HighPassFilter_VarMinFrequency_OperatorCalculator(signalCalculator, minFrequencyCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitIf(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase conditionCalculator = _stack.Pop();
            OperatorCalculatorBase thenCalculator = _stack.Pop();
            OperatorCalculatorBase elseCalculator = _stack.Pop();

            conditionCalculator = conditionCalculator ?? new Zero_OperatorCalculator();
            thenCalculator = thenCalculator ?? new Zero_OperatorCalculator();
            elseCalculator = elseCalculator ?? new Zero_OperatorCalculator();

            double condition = conditionCalculator.Calculate(_defaultDimensionStack);
            double then = thenCalculator.Calculate(_defaultDimensionStack);
            double @else = elseCalculator.Calculate(_defaultDimensionStack);

            bool conditionIsConst = conditionCalculator is Number_OperatorCalculator;
            bool thenIsConst = thenCalculator is Number_OperatorCalculator;
            bool elseIsConst = elseCalculator is Number_OperatorCalculator;

            if (conditionIsConst)
            {
                bool conditionIsTrue = condition != 0.0;
                if (conditionIsTrue)
                {
                    calculator = thenCalculator;
                }
                else
                {
                    calculator = elseCalculator;
                }
            }
            else if (thenIsConst && elseIsConst)
            {
                calculator = new If_VarCondition_ConstThen_ConstElse_OperatorCalculator(conditionCalculator, then, @else);
            }
            else if (thenIsConst && !elseIsConst)
            {
                calculator = new If_VarCondition_ConstThen_VarElse_OperatorCalculator(conditionCalculator, then, elseCalculator);
            }
            else if (!thenIsConst && elseIsConst)
            {
                calculator = new If_VarCondition_VarThen_ConstElse_OperatorCalculator(conditionCalculator, thenCalculator, @else);
            }
            else if (!thenIsConst && !elseIsConst)
            {
                calculator = new If_VarCondition_VarThen_VarElse_OperatorCalculator(conditionCalculator, thenCalculator, elseCalculator);
            }
            else
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
            }

            _stack.Push(calculator);
        }

        protected override void VisitLessThan(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase calculatorA = _stack.Pop();
            OperatorCalculatorBase calculatorB = _stack.Pop();

            calculatorA = calculatorA ?? new Zero_OperatorCalculator();
            calculatorB = calculatorB ?? new Zero_OperatorCalculator();

            double a = calculatorA.Calculate(_defaultDimensionStack);
            double b = calculatorB.Calculate(_defaultDimensionStack);

            bool aIsConst = calculatorA is Number_OperatorCalculator;
            bool bIsConst = calculatorB is Number_OperatorCalculator;

            if (aIsConst && bIsConst)
            {
                double value;

                if (a < b) value = 1.0;
                else value = 0.0;

                calculator = new Number_OperatorCalculator(value);
            }
            else if (!aIsConst && bIsConst)
            {
                calculator = new LessThan_VarA_ConstB_OperatorCalculator(calculatorA, b);
            }
            else if (aIsConst && !bIsConst)
            {
                calculator = new LessThan_ConstA_VarB_OperatorCalculator(a, calculatorB);
            }
            else if (!aIsConst && !bIsConst)
            {
                calculator = new LessThan_VarA_VarB_OperatorCalculator(calculatorA, calculatorB);
            }
            else
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
            }

            _stack.Push(calculator);
        }

        protected override void VisitLessThanOrEqual(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase calculatorA = _stack.Pop();
            OperatorCalculatorBase calculatorB = _stack.Pop();

            calculatorA = calculatorA ?? new Zero_OperatorCalculator();
            calculatorB = calculatorB ?? new Zero_OperatorCalculator();

            double a = calculatorA.Calculate(_defaultDimensionStack);
            double b = calculatorB.Calculate(_defaultDimensionStack);

            bool aIsConst = calculatorA is Number_OperatorCalculator;
            bool bIsConst = calculatorB is Number_OperatorCalculator;

            if (aIsConst && bIsConst)
            {
                double value;

                if (a <= b) value = 1.0;
                else value = 0.0;

                calculator = new Number_OperatorCalculator(value);
            }
            else if (!aIsConst && bIsConst)
            {
                calculator = new LessThanOrEqual_VarA_ConstB_OperatorCalculator(calculatorA, b);
            }
            else if (aIsConst && !bIsConst)
            {
                calculator = new LessThanOrEqual_ConstA_VarB_OperatorCalculator(a, calculatorB);
            }
            else if (!aIsConst && !bIsConst)
            {
                calculator = new LessThanOrEqual_VarA_VarB_OperatorCalculator(calculatorA, calculatorB);
            }
            else
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
            }

            _stack.Push(calculator);
        }

        protected override void VisitLoop(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase skipCalculator = _stack.Pop();
            OperatorCalculatorBase loopStartMarkerCalculator = _stack.Pop();
            OperatorCalculatorBase loopEndMarkerCalculator = _stack.Pop();
            OperatorCalculatorBase releaseEndMarkerCalculator = _stack.Pop();
            OperatorCalculatorBase noteDurationCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            skipCalculator = skipCalculator ?? new Zero_OperatorCalculator();
            loopStartMarkerCalculator = loopStartMarkerCalculator ?? new Zero_OperatorCalculator();
            loopEndMarkerCalculator = loopEndMarkerCalculator ?? new Zero_OperatorCalculator();
            releaseEndMarkerCalculator = releaseEndMarkerCalculator ?? new Number_OperatorCalculator(CalculationHelper.VERY_HIGH_VALUE);
            noteDurationCalculator = noteDurationCalculator ?? new Number_OperatorCalculator(CalculationHelper.VERY_HIGH_VALUE);

            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool skipIsConst = skipCalculator is Number_OperatorCalculator;
            bool loopStartMarkerIsConst = loopStartMarkerCalculator is Number_OperatorCalculator;
            bool loopEndMarkerIsConst = loopEndMarkerCalculator is Number_OperatorCalculator;
            bool releaseEndMarkerIsConst = releaseEndMarkerCalculator is Number_OperatorCalculator;
            bool noteDurationIsConst = noteDurationCalculator is Number_OperatorCalculator;

            double signal = signalCalculator.Calculate(_defaultDimensionStack);
            double skip = skipCalculator.Calculate(_defaultDimensionStack);
            double loopStartMarker = loopStartMarkerCalculator.Calculate(_defaultDimensionStack);
            double loopEndMarker = loopEndMarkerCalculator.Calculate(_defaultDimensionStack);
            double releaseEndMarker = releaseEndMarkerCalculator.Calculate(_defaultDimensionStack);
            double noteDuration = noteDurationCalculator.Calculate(_defaultDimensionStack);

            bool signalConstZero = signalIsConst && signal == 0.0;
            bool skipIsConstZero = skipIsConst && skip == 0.0;
            bool loopStartMarkerIsConstZero = loopStartMarkerIsConst && loopStartMarker == 0.0;
            bool loopEndMarkerIsConstZero = loopEndMarkerIsConst && loopEndMarker == 0.0;
            bool noteDurationIsConstVeryHighValue = noteDurationIsConst && noteDuration == CalculationHelper.VERY_HIGH_VALUE;
            bool releaseEndMarkerIsConstVeryHighValue = releaseEndMarkerIsConst && releaseEndMarker == CalculationHelper.VERY_HIGH_VALUE;

            var wrapper = new Loop_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;

            if (signalIsConst)
            {
                // Note that the signalCalculator is not used here,
                // because behavior consistent with the other loop
                // calculators would mean that before and after the
                // attack, loop and release, the signal is 0,
                // while during attack, loop and release it would be
                // the value of constant value.
                // To make this behavior consistent would require another calculator,
                // that is just not worth is, because it makes no sense to apply a loop to a constant.
                // So to not return e.g. the number 2 before and after the loop
                // just return 0. Nobody wants to loop throught a constant.
                calculator = new Zero_OperatorCalculator();
            }
            else
            {
                if (skipIsConst && 
                    loopStartMarkerIsConst && 
                    skip == loopStartMarker && 
                    noteDurationIsConstVeryHighValue)
                {
                    if (loopEndMarkerIsConst)
                    {
                        calculator = new Loop_OperatorCalculator_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration(
                            signalCalculator, loopStartMarker, loopEndMarker, dimensionEnum);
                    }
                    else
                    {
                        calculator = new Loop_OperatorCalculator_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration(
                            signalCalculator, loopStartMarker, loopEndMarkerCalculator, dimensionEnum);
                    }
                }
                else if (skipIsConstZero && releaseEndMarkerIsConstVeryHighValue)
                {
                    if (loopStartMarkerIsConst && loopEndMarkerIsConst)
                    {
                        calculator = new Loop_OperatorCalculator_NoSkipOrRelease_ManyConstants(
                            signalCalculator, loopStartMarker, loopEndMarker, noteDurationCalculator, dimensionEnum);
                    }
                    else
                    {
                        calculator = new Loop_OperatorCalculator_NoSkipOrRelease(
                            signalCalculator, loopStartMarkerCalculator, loopEndMarkerCalculator, noteDurationCalculator, dimensionEnum);
                    }
                }
                else
                {
                    if (skipIsConst && loopStartMarkerIsConst && loopEndMarkerIsConst && releaseEndMarkerIsConst)
                    {
                        calculator = new Loop_OperatorCalculator_ManyConstants(
                            signalCalculator, skip, loopStartMarker, loopEndMarker, releaseEndMarker, noteDurationCalculator, dimensionEnum);
                    }
                    else
                    {
                        calculator = new Loop_OperatorCalculator(
                            signalCalculator, 
                            skipCalculator, 
                            loopStartMarkerCalculator, 
                            loopEndMarkerCalculator, 
                            releaseEndMarkerCalculator, 
                            noteDurationCalculator, 
                            dimensionEnum);
                    }
                }
            }

            _stack.Push(calculator);
        }

        protected override void VisitLowPassFilter(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase maxFrequencyCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            maxFrequencyCalculator = maxFrequencyCalculator ?? new Zero_OperatorCalculator();

            double signal = signalCalculator.Calculate(_defaultDimensionStack);
            double maxFrequency = maxFrequencyCalculator.Calculate(_defaultDimensionStack);

            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool maxFrequencyIsConst = maxFrequencyCalculator is Number_OperatorCalculator;

            bool maxFrequencyIsConstZero = maxFrequencyIsConst && maxFrequency == 0.0;

            if (signalIsConst)
            {
                calculator = signalCalculator;
            }
            else if (maxFrequencyIsConstZero)
            {
                // Weird number: time stands still.
                calculator = new Number_OperatorCalculator(signal);
            }
            else if (maxFrequencyIsConst)
            {
                calculator = new LowPassFilter_ConstMaxFrequency_OperatorCalculator(signalCalculator, maxFrequency);
            }
            else
            {
                calculator = new LowPassFilter_VarMaxFrequency_OperatorCalculator(signalCalculator, maxFrequencyCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitMaximum(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeSliceDurationCalculator = _stack.Pop();
            OperatorCalculatorBase sampleCountCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            double signal = signalCalculator.Calculate(_defaultDimensionStack);
            bool signalIsConst = signalCalculator is Number_OperatorCalculator;

            // TODO: Do not use these magic defaults, but give standard operators default inlet value functionality.
            timeSliceDurationCalculator = timeSliceDurationCalculator ?? new Number_OperatorCalculator(0.02f);
            sampleCountCalculator = sampleCountCalculator ?? new Number_OperatorCalculator(100f);

            var wrapper = new Maximum_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;

            if (signalIsConst)
            {
                calculator = signalCalculator;
            }
            else
            {
                calculator = new Maximum_OperatorCalculator(signalCalculator, timeSliceDurationCalculator, sampleCountCalculator, dimensionEnum);
            }

            _stack.Push(calculator);
        }

        protected override void VisitMinimum(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeSliceDurationCalculator = _stack.Pop();
            OperatorCalculatorBase sampleCountCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            double signal = signalCalculator.Calculate(_defaultDimensionStack);
            bool signalIsConst = signalCalculator is Number_OperatorCalculator;

            // TODO: Do not use these magic defaults, but give standard operators default inlet value functionality.
            timeSliceDurationCalculator = timeSliceDurationCalculator ?? new Number_OperatorCalculator(0.02f);
            sampleCountCalculator = sampleCountCalculator ?? new Number_OperatorCalculator(100f);

            var wrapper = new Minimum_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;

            if (signalIsConst)
            {
                calculator = signalCalculator;
            }
            else
            {
                calculator = new Minimum_OperatorCalculator(signalCalculator, timeSliceDurationCalculator, sampleCountCalculator, dimensionEnum);
            }

            _stack.Push(calculator);
        }

        protected override void VisitMultiply(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase aCalculator = _stack.Pop();
            OperatorCalculatorBase bCalculator = _stack.Pop();
            OperatorCalculatorBase originCalculator = _stack.Pop();

            aCalculator = aCalculator ?? new One_OperatorCalculator();
            bCalculator = bCalculator ?? new One_OperatorCalculator();
            originCalculator = originCalculator ?? new Zero_OperatorCalculator();

            double a = aCalculator.Calculate(_defaultDimensionStack);
            double b = bCalculator.Calculate(_defaultDimensionStack);
            double origin = originCalculator.Calculate(_defaultDimensionStack);
            bool aIsConst = aCalculator is Number_OperatorCalculator;
            bool bIsConst = bCalculator is Number_OperatorCalculator;
            bool originIsConst = originCalculator is Number_OperatorCalculator;
            bool aIsConstZero = aIsConst && a == 0;
            bool bIsConstZero = bIsConst && b == 0;
            bool originIsConstZero = originIsConst && origin == 0;
            bool aIsConstOne = aIsConst && a == 1;
            bool bIsConstOne = bIsConst && b == 1;

            if (aIsConstZero || bIsConstZero)
            {
                calculator = new Zero_OperatorCalculator();
            }
            else if (aIsConstOne)
            {
                calculator = bCalculator;
            }
            else if (bIsConstOne)
            {
                calculator = aCalculator;
            }
            else if (originIsConstZero && aIsConst && bIsConst)
            {
                calculator = new Number_OperatorCalculator(a * b);
            }
            else if (aIsConst && bIsConst && originIsConst)
            {
                double value = (a - origin) * b + origin;
                calculator = new Number_OperatorCalculator(value);
            }
            else if (aIsConst && !bIsConst && originIsConstZero)
            {
                calculator = new Multiply_ConstA_VarB_NoOrigin_OperatorCalculator(a, bCalculator);
            }
            else if (!aIsConst && bIsConst && originIsConstZero)
            {
                calculator = new Multiply_VarA_ConstB_NoOrigin_OperatorCalculator(aCalculator, b);
            }
            else if (!aIsConst && !bIsConst && originIsConstZero)
            {
                calculator = new Multiply_VarA_VarB_NoOrigin_OperatorCalculator(aCalculator, bCalculator);
            }
            else if (aIsConst && !bIsConst && originIsConst)
            {
                calculator = new Multiply_ConstA_VarB_ConstOrigin_OperatorCalculator(a, bCalculator, origin);
            }
            else if (!aIsConst && bIsConst && originIsConst)
            {
                calculator = new Multiply_VarA_ConstB_ConstOrigin_OperatorCalculator(aCalculator, b, origin);
            }
            else if (!aIsConst && !bIsConst && originIsConst)
            {
                calculator = new Multiply_VarA_VarB_ConstOrigin_OperatorCalculator(aCalculator, bCalculator, origin);
            }
            else if (aIsConst && bIsConst && !originIsConst)
            {
                calculator = new Multiply_ConstA_ConstB_VarOrigin_OperatorCalculator(a, b, originCalculator);
            }
            else if (aIsConst && !bIsConst && !originIsConst)
            {
                calculator = new Multiply_ConstA_VarB_VarOrigin_OperatorCalculator(a, bCalculator, originCalculator);
            }
            else if (!aIsConst && bIsConst && !originIsConst)
            {
                calculator = new Multiply_VarA_ConstB_VarOrigin_OperatorCalculator(aCalculator, b, originCalculator);
            }
            else
            {
                calculator = new Multiply_VarA_VarB_VarOrigin_OperatorCalculator(aCalculator, bCalculator, originCalculator);
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

            double signal = signalCalculator.Calculate(_defaultDimensionStack);
            double factor = factorCalculator.Calculate(_defaultDimensionStack);
            double origin = originCalculator.Calculate(_defaultDimensionStack);

            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool factorIsConst = factorCalculator is Number_OperatorCalculator;
            bool originIsConst = originCalculator is Number_OperatorCalculator;

            bool signalIsConstZero = signalIsConst && signal == 0;
            bool factorIsConstZero = factorIsConst && factor == 0;
            bool originIsConstZero = originIsConst && origin == 0;

            bool factorIsConstOne = factorIsConst && factor == 1;

            bool signalIsConstSpecialNumber = signalIsConst && (Double.IsNaN(signal) || Double.IsInfinity(signal));
            bool factorIsConstSpecialNumber = factorIsConst && (Double.IsNaN(factor) || Double.IsInfinity(factor));
            bool originIsConstSpecialNumber = originIsConst && (Double.IsNaN(origin) || Double.IsInfinity(origin));

            var wrapper = new Narrower_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;

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
                calculator = new Narrower_VarSignal_ConstFactor_ZeroOrigin_OperatorCalculator(signalCalculator, factor, dimensionEnum);
            }
            else if (!signalIsConst && !factorIsConst && originIsConstZero)
            {
                calculator = new Narrower_VarSignal_VarFactor_ZeroOrigin_OperatorCalculator(signalCalculator, factorCalculator, dimensionEnum);
            }
            else if (!signalIsConst && factorIsConst && originIsConst)
            {
                calculator = new Narrower_VarSignal_ConstFactor_ConstOrigin_OperatorCalculator(signalCalculator, factor, origin, dimensionEnum);
            }
            else if (!signalIsConst && factorIsConst && !originIsConst)
            {
                calculator = new Narrower_VarSignal_ConstFactor_VarOrigin_OperatorCalculator(signalCalculator, factor, originCalculator, dimensionEnum);
            }
            else if (!signalIsConst && !factorIsConst && originIsConst)
            {
                calculator = new Narrower_VarSignal_VarFactor_ConstOrigin_OperatorCalculator(signalCalculator, factorCalculator, origin, dimensionEnum);
            }
            else if (!signalIsConst && !factorIsConst && !originIsConst)
            {
                calculator = new Narrower_VarSignal_VarFactor_VarOrigin_OperatorCalculator(signalCalculator, factorCalculator, originCalculator, dimensionEnum);
            }
            else
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
            }

            _stack.Push(calculator);
        }

        protected override void VisitNegative(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase xCalculator = _stack.Pop();

            xCalculator = xCalculator ?? new Zero_OperatorCalculator();
            double x = xCalculator.Calculate(_defaultDimensionStack);
            bool xIsConst = xCalculator is Number_OperatorCalculator;

            if (xIsConst)
            {
                calculator = new Number_OperatorCalculator(-x);
            }
            else
            {
                calculator = new Negative_OperatorCalculator(xCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitNoise(Operator op)
        {
            double offset;
            if (!_operator_NoiseOffsetInSeconds_Dictionary.TryGetValue(op, out offset))
            {
                offset = _calculatorCache.NoiseCalculator.GetRandomOffset();
                _operator_NoiseOffsetInSeconds_Dictionary.Add(op, offset);
            }

            var wrapper = new Noise_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;

            var calculator = new Noise_OperatorCalculator(_calculatorCache.NoiseCalculator, offset, dimensionEnum);
            _stack.Push(calculator);
        }

        protected override void VisitNot(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase xCalculator = _stack.Pop();

            xCalculator = xCalculator ?? new Zero_OperatorCalculator();

            double x = xCalculator.Calculate(_defaultDimensionStack);

            bool xIsConst = xCalculator is Number_OperatorCalculator;

            if (xIsConst)
            {
                double value;

                bool aIsFalse = x == 0.0;

                if (aIsFalse) value = 1.0;
                else value = 0.0;

                calculator = new Number_OperatorCalculator(value);
            }
            else if (!xIsConst)
            {
                calculator = new Not_OperatorCalculator(xCalculator);
            }
            else
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
            }

            _stack.Push(calculator);
        }

        protected override void VisitNotEqual(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase calculatorA = _stack.Pop();
            OperatorCalculatorBase calculatorB = _stack.Pop();

            calculatorA = calculatorA ?? new Zero_OperatorCalculator();
            calculatorB = calculatorB ?? new Zero_OperatorCalculator();

            double a = calculatorA.Calculate(_defaultDimensionStack);
            double b = calculatorB.Calculate(_defaultDimensionStack);

            bool aIsConst = calculatorA is Number_OperatorCalculator;
            bool bIsConst = calculatorB is Number_OperatorCalculator;

            if (aIsConst && bIsConst)
            {
                double value;

                if (a != b) value = 1.0;
                else value = 0.0;

                calculator = new Number_OperatorCalculator(value);
            }
            else if (!aIsConst && bIsConst)
            {
                calculator = new NotEqual_VarA_ConstB_OperatorCalculator(calculatorA, b);
            }
            else if (aIsConst && !bIsConst)
            {
                calculator = new NotEqual_ConstA_VarB_OperatorCalculator(a, calculatorB);
            }
            else if (!aIsConst && !bIsConst)
            {
                calculator = new NotEqual_VarA_VarB_OperatorCalculator(calculatorA, calculatorB);
            }
            else
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
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

        protected override void VisitOneOverX(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase xCalculator = _stack.Pop();

            xCalculator = xCalculator ?? new One_OperatorCalculator();
            double x = xCalculator.Calculate(_defaultDimensionStack);
            bool xIsConst = xCalculator is Number_OperatorCalculator;

            if (xIsConst)
            {
                calculator = new Number_OperatorCalculator(1 / x);
            }
            else
            {
                calculator = new OneOverX_OperatorCalculator(xCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitOr(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase calculatorA = _stack.Pop();
            OperatorCalculatorBase calculatorB = _stack.Pop();

            calculatorA = calculatorA ?? new Zero_OperatorCalculator();
            calculatorB = calculatorB ?? new Zero_OperatorCalculator();

            double a = calculatorA.Calculate(_defaultDimensionStack);
            double b = calculatorB.Calculate(_defaultDimensionStack);

            bool aIsConst = calculatorA is Number_OperatorCalculator;
            bool bIsConst = calculatorB is Number_OperatorCalculator;

            if (aIsConst && bIsConst)
            {
                double value;

                bool aIsTrue = a != 0.0;
                bool bIsTrue = b != 0.0;

                if (aIsTrue || bIsTrue) value = 1.0;
                else value = 0.0;

                calculator = new Number_OperatorCalculator(value);
            }
            else if (!aIsConst && bIsConst)
            {
                calculator = new Or_VarA_ConstB_OperatorCalculator(calculatorA, b);
            }
            else if (aIsConst && !bIsConst)
            {
                calculator = new Or_ConstA_VarB_OperatorCalculator(a, calculatorB);
            }
            else if (!aIsConst && !bIsConst)
            {
                calculator = new Or_VarA_VarB_OperatorCalculator(calculatorA, calculatorB);
            }
            else
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
            }

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
            double @base = baseCalculator.Calculate(_defaultDimensionStack);
            double exponent = exponentCalculator.Calculate(_defaultDimensionStack);
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

        protected override void VisitPulse(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase frequencyCalculator = _stack.Pop();
            OperatorCalculatorBase widthCalculator = _stack.Pop();
            OperatorCalculatorBase phaseShiftCalculator = _stack.Pop();

            frequencyCalculator = frequencyCalculator ?? new Zero_OperatorCalculator();
            widthCalculator = widthCalculator ?? new Number_OperatorCalculator(0.5);
            phaseShiftCalculator = phaseShiftCalculator ?? new Zero_OperatorCalculator();

            double frequency = frequencyCalculator.Calculate(_defaultDimensionStack);
            double width = widthCalculator.Calculate(_defaultDimensionStack) % 1.0;
            double phaseShift = phaseShiftCalculator.Calculate(_defaultDimensionStack) % 1.0;

            bool frequencyIsConst = frequencyCalculator is Number_OperatorCalculator;
            bool widthIsConst = widthCalculator is Number_OperatorCalculator;
            bool phaseShiftIsConst = phaseShiftCalculator is Number_OperatorCalculator;

            bool frequencyIsConstZero = frequencyIsConst && frequency == 0.0;
            bool widthIsConstZero = widthIsConst && width == 0.0;

            bool frequencyIsConstSpecialNumber = frequencyIsConst && (Double.IsNaN(frequency) || Double.IsInfinity(frequency));
            bool widthIsConstSpecialNumber = widthIsConst && (Double.IsNaN(width) || Double.IsInfinity(width));
            bool phaseShiftIsConstSpecialNumber = phaseShiftIsConst && (Double.IsNaN(phaseShift) || Double.IsInfinity(phaseShift));

            var wrapper = new Pulse_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;

            if (frequencyIsConstSpecialNumber || widthIsConstSpecialNumber || phaseShiftIsConstSpecialNumber)
            {
                // Weird Number
                calculator = new Number_OperatorCalculator(Double.NaN);
            }
            else if (frequencyIsConstZero)
            {
                // Weird Number
                // (Frequency 0 means time stands still. In theory this could produce a different value than 0.
                //  but I feel I would have to make disproportionate effort to take phase shift and width into account.)
                calculator = new Zero_OperatorCalculator();
            }
            else if (widthIsConstZero)
            {
                calculator = new Zero_OperatorCalculator();
            }
            else if (frequencyIsConst && widthIsConst && phaseShiftIsConst)
            {
                calculator = new Pulse_ConstFrequency_ConstWidth_ConstPhaseShift_OperatorCalculator(frequency, width, phaseShift, dimensionEnum);
            }
            else if (frequencyIsConst && widthIsConst && !phaseShiftIsConst)
            {
                calculator = new Pulse_ConstFrequency_ConstWidth_VarPhaseShift_OperatorCalculator(frequency, width, phaseShiftCalculator, dimensionEnum);
            }
            else if (frequencyIsConst && !widthIsConst && phaseShiftIsConst)
            {
                calculator = new Pulse_ConstFrequency_VarWidth_ConstPhaseShift_OperatorCalculator(frequency, widthCalculator, phaseShift, dimensionEnum);
            }
            else if (frequencyIsConst && !widthIsConst && !phaseShiftIsConst)
            {
                calculator = new Pulse_ConstFrequency_VarWidth_VarPhaseShift_OperatorCalculator(frequency, widthCalculator, phaseShiftCalculator, dimensionEnum);
            }
            else if (!frequencyIsConst && widthIsConst && phaseShiftIsConst)
            {
                calculator = new Pulse_VarFrequency_ConstWidth_ConstPhaseShift_OperatorCalculator(frequencyCalculator, width, phaseShift, dimensionEnum);
            }
            else if (!frequencyIsConst && widthIsConst && !phaseShiftIsConst)
            {
                calculator = new Pulse_VarFrequency_ConstWidth_VarPhaseShift_OperatorCalculator(frequencyCalculator, width, phaseShiftCalculator, dimensionEnum);
            }
            else if (!frequencyIsConst && !widthIsConst && phaseShiftIsConst)
            {
                calculator = new Pulse_VarFrequency_VarWidth_ConstPhaseShift_OperatorCalculator(frequencyCalculator, widthCalculator, phaseShift, dimensionEnum);
            }
            else if (!frequencyIsConst && !widthIsConst && !phaseShiftIsConst)
            {
                calculator = new Pulse_VarFrequency_VarWidth_VarPhaseShift_OperatorCalculator(frequencyCalculator, widthCalculator, phaseShiftCalculator, dimensionEnum);
            }
            else
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
            }

            _stack.Push(calculator);
        }

        protected override void VisitPulseTrigger(Operator op)
        {
            OperatorCalculatorBase operatorCalculator;

            OperatorCalculatorBase calculationCalculator = _stack.Pop();
            OperatorCalculatorBase resetCalculator = _stack.Pop();

            calculationCalculator = calculationCalculator ?? new Zero_OperatorCalculator();
            resetCalculator = resetCalculator ?? new Zero_OperatorCalculator();

            bool calculationIsConst = calculationCalculator is Number_OperatorCalculator;
            bool resetIsConst = resetCalculator is Number_OperatorCalculator;

            if (calculationIsConst)
            {
                operatorCalculator = calculationCalculator;
            }
            else if (resetIsConst)
            {
                operatorCalculator = calculationCalculator;
            }
            else
            {
                operatorCalculator = new PulseTrigger_OperatorCalculator(calculationCalculator, resetCalculator);
            }

            _stack.Push(operatorCalculator);
        }

        protected override void VisitRandom(Operator op)
        {
            int randomCalculatorOffset;
            if (!_operator_RandomOffsetInSeconds_Dictionary.TryGetValue(op, out randomCalculatorOffset))
            {
                randomCalculatorOffset = RandomCalculatorBase.GetRandomOffset();
                _operator_RandomOffsetInSeconds_Dictionary.Add(op, randomCalculatorOffset);
            }

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase rateCalculator = _stack.Pop();
            OperatorCalculatorBase phaseShiftCalculator = _stack.Pop();

            rateCalculator = rateCalculator ?? new Zero_OperatorCalculator();
            phaseShiftCalculator = phaseShiftCalculator ?? new Zero_OperatorCalculator();

            double rate = rateCalculator.Calculate(_defaultDimensionStack);
            double phaseShift = phaseShiftCalculator.Calculate(_defaultDimensionStack);

            bool rateIsConst = rateCalculator is Number_OperatorCalculator;
            bool phaseShiftIsConst = phaseShiftCalculator is Number_OperatorCalculator;

            bool rateIsConstZero = rateIsConst && rate == 0;
            bool phaseShiftIsConstZero = phaseShiftIsConst && phaseShift == 0;

            bool rateIsConstSpecialNumber = rateIsConst && (Double.IsNaN(rate) || Double.IsInfinity(rate));
            bool phaseShiftIsConstSpecialNumber = phaseShiftIsConst && (Double.IsNaN(phaseShift) || Double.IsInfinity(phaseShift));

            var wrapper = new Random_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;

            if (rateIsConstSpecialNumber || phaseShiftIsConstSpecialNumber)
            {
                // Weird number
                calculator = new Number_OperatorCalculator(Double.NaN);
            }
            else if (rateIsConstZero)
            {
                // Weird number
                calculator = new Zero_OperatorCalculator();
            }
            // TODO: Add more variations.
            else
            {
                ResampleInterpolationTypeEnum resampleInterpolationTypeEnum = wrapper.ResampleInterpolationType;

                switch (resampleInterpolationTypeEnum)
                {
                    case ResampleInterpolationTypeEnum.Block:
                        {
                            var randomCalculator = new RandomCalculator_BlockInterpolation();

                            calculator = new Random_OperatorCalculator_BlockAndStripe_VarFrequency_VarPhaseShift(
                                randomCalculator,
                                randomCalculatorOffset,
                                rateCalculator,
                                phaseShiftCalculator,
                                dimensionEnum);

                            break;
                        }

                    case ResampleInterpolationTypeEnum.Stripe:
                        {
                            var randomCalculator = new RandomCalculator_StripeInterpolation();

                            calculator = new Random_OperatorCalculator_BlockAndStripe_VarFrequency_VarPhaseShift(
                                randomCalculator,
                                randomCalculatorOffset,
                                rateCalculator,
                                phaseShiftCalculator,
                                dimensionEnum);

                            break;
                        }

                    case ResampleInterpolationTypeEnum.LineRememberT0:
                        {
                            var randomCalculator = new RandomCalculator_StripeInterpolation();

                            calculator = new Random_OperatorCalculator_LineRememberT0(
                                randomCalculator,
                                randomCalculatorOffset,
                                rateCalculator,
                                phaseShiftCalculator,
                                dimensionEnum);

                            break;
                        }


                    case ResampleInterpolationTypeEnum.LineRememberT1:
                        {
                            var randomCalculator = new RandomCalculator_StripeInterpolation();

                            calculator = new Random_OperatorCalculator_LineRememberT1(
                                randomCalculator,
                                randomCalculatorOffset,
                                rateCalculator,
                                phaseShiftCalculator,
                                dimensionEnum);

                            break;
                        }

                    case ResampleInterpolationTypeEnum.CubicEquidistant:
                        {
                            var randomCalculator = new RandomCalculator_StripeInterpolation();

                            calculator = new Random_OperatorCalculator_CubicEquidistant(
                                randomCalculator,
                                randomCalculatorOffset,
                                rateCalculator,
                                phaseShiftCalculator,
                                dimensionEnum);

                            break;
                        }

                    case ResampleInterpolationTypeEnum.CubicAbruptSlope:
                        {
                            var randomCalculator = new RandomCalculator_StripeInterpolation();

                            calculator = new Random_OperatorCalculator_CubicAbruptSlope(
                                randomCalculator,
                                randomCalculatorOffset,
                                rateCalculator,
                                phaseShiftCalculator,
                                dimensionEnum);

                            break;
                        }

                    case ResampleInterpolationTypeEnum.CubicSmoothSlope:
                        {
                            var randomCalculator = new RandomCalculator_StripeInterpolation();

                            calculator = new Random_OperatorCalculator_CubicSmoothSlope(
                                randomCalculator,
                                randomCalculatorOffset,
                                rateCalculator,
                                phaseShiftCalculator,
                                dimensionEnum);

                            break;
                        }

                    case ResampleInterpolationTypeEnum.Hermite:
                        {
                            var randomCalculator = new RandomCalculator_StripeInterpolation();

                            calculator = new Random_OperatorCalculator_Hermite(
                                randomCalculator,
                                randomCalculatorOffset,
                                rateCalculator,
                                phaseShiftCalculator,
                                dimensionEnum);

                            break;
                        }

                    default:
                        throw new ValueNotSupportedException(resampleInterpolationTypeEnum);
                }
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

            double signal = signalCalculator.Calculate(_defaultDimensionStack);
            double samplingRate = samplingRateCalculator.Calculate(_defaultDimensionStack);

            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool samplingRateIsConst = samplingRateCalculator is Number_OperatorCalculator;

            bool signalIsConstZero = signalIsConst && signal == 0;
            bool samplingRateIsConstZero = samplingRateIsConst && samplingRate == 0;

            bool signalIsConstSpecialNumber = signalIsConst && (Double.IsNaN(signal) || Double.IsInfinity(signal));
            bool samplingRateIsConstSpecialNumber = samplingRateIsConst && (Double.IsNaN(samplingRate) || Double.IsInfinity(samplingRate));

            var wrapper = new Resample_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;

            if (signalIsConst)
            {
                // Const signal Preceeds weird numbers.
                calculator = new Number_OperatorCalculator(signal);
            }
            else if (samplingRateIsConstZero)
            {
                // Weird number: Time stands still.
                calculator = new Number_OperatorCalculator(signal);
            }
            else if (samplingRateIsConstSpecialNumber)
            {
                // Wierd number
                // Note that if signal is const,
                // an indeterminate sampling rate can still render a determinite result.
                calculator = new Number_OperatorCalculator(Double.NaN);
            }
            else
            {
                ResampleInterpolationTypeEnum resampleInterpolationTypeEnum = wrapper.InterpolationType;

                switch (resampleInterpolationTypeEnum)
                {
                    case ResampleInterpolationTypeEnum.Block:
                        calculator = new Resample_OperatorCalculator_Block(signalCalculator, samplingRateCalculator, dimensionEnum);
                        break;

                    case ResampleInterpolationTypeEnum.Stripe:
                        calculator = new Resample_OperatorCalculator_Stripe(signalCalculator, samplingRateCalculator, dimensionEnum);
                        break;

                    case ResampleInterpolationTypeEnum.LineRememberT0:
                        calculator = new Resample_OperatorCalculator_LineRememberT0(signalCalculator, samplingRateCalculator, dimensionEnum);
                        break;

                    case ResampleInterpolationTypeEnum.LineRememberT1:
                        calculator = new Resample_OperatorCalculator_LineRememberT1(signalCalculator, samplingRateCalculator, dimensionEnum);
                        break;

                    case ResampleInterpolationTypeEnum.CubicEquidistant:
                        calculator = new Resample_OperatorCalculator_CubicEquidistant(signalCalculator, samplingRateCalculator, dimensionEnum);
                        break;

                    case ResampleInterpolationTypeEnum.CubicAbruptSlope:
                        calculator = new Resample_OperatorCalculator_CubicAbruptSlope(signalCalculator, samplingRateCalculator, dimensionEnum);
                        break;

                    case ResampleInterpolationTypeEnum.CubicSmoothSlope:
                        calculator = new Resample_OperatorCalculator_CubicSmoothSlope(signalCalculator, samplingRateCalculator, dimensionEnum);
                        break;

                    case ResampleInterpolationTypeEnum.Hermite:
                        calculator = new Resample_OperatorCalculator_Hermite(signalCalculator, samplingRateCalculator, dimensionEnum);
                        break;

                    default:
                        throw new ValueNotSupportedException(resampleInterpolationTypeEnum);
                }
            }
            _stack.Push(calculator);
        }

        protected override void VisitReverse(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase speedCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            speedCalculator = speedCalculator ?? new One_OperatorCalculator();

            double signal = signalCalculator.Calculate(_defaultDimensionStack);
            double speed = speedCalculator.Calculate(_defaultDimensionStack);

            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool speedIsConst = speedCalculator is Number_OperatorCalculator;

            bool speedIsConstZero = speedIsConst && speed == 0;

            bool signalIsConstSpecialNumber = signalIsConst && (Double.IsNaN(signal) || Double.IsInfinity(signal));
            bool speedIsConstSpecialNumber = speedIsConst && (Double.IsNaN(speed) || Double.IsInfinity(speed));

            var wrapper = new Reverse_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;

            if (speedIsConstSpecialNumber)
            {
                // Weird number
                calculator = new Number_OperatorCalculator(Double.NaN);
            }
            else if (signalIsConstSpecialNumber)
            {
                // Weird number
                calculator = new Number_OperatorCalculator(signal);
            }
            else if (speedIsConstZero)
            {
                // Weird number
                // Speed-up of 0 means time stands still.
                calculator = new Number_OperatorCalculator(signal);
            }
            else if (signalIsConst)
            {
                calculator = signalCalculator;
            }
            else if (speedIsConst)
            {
                calculator = new Reverse_WithConstSpeed_OperatorCalculator(signalCalculator, speed, dimensionEnum);
            }
            else
            {
                calculator = new Reverse_WithVarSpeed_OperatorCalculator(signalCalculator, speedCalculator, dimensionEnum);
            }

            _stack.Push(calculator);
        }

        protected override void VisitRound(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase stepCalculator = _stack.Pop();
            OperatorCalculatorBase offsetCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            stepCalculator = stepCalculator ?? new One_OperatorCalculator();
            offsetCalculator = offsetCalculator ?? new Zero_OperatorCalculator();

            double signal = signalCalculator.Calculate(_defaultDimensionStack);
            double step = stepCalculator.Calculate(_defaultDimensionStack);
            double offset = offsetCalculator.Calculate(_defaultDimensionStack);

            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool stepIsConst = stepCalculator is Number_OperatorCalculator;
            bool offsetIsConst = offsetCalculator is Number_OperatorCalculator;

            bool signalIsConstZero = signalIsConst && signal == 0;
            bool stepIsConstZero = stepIsConst && step == 0;
            bool offsetIsConstZero = offsetIsConst && offset % 1.0 == 0;

            bool stepIsConstOne = stepIsConst && step == 1;

            bool signalIsConstSpecialNumber = signalIsConst && (Double.IsNaN(signal) || Double.IsInfinity(signal));
            bool stepIsConstSpecialNumber = stepIsConst && (Double.IsNaN(step) || Double.IsInfinity(step));
            bool offsetIsConstSpecialNumber = offsetIsConst && (Double.IsNaN(offset) || Double.IsInfinity(offset));

            if (signalIsConstSpecialNumber || stepIsConstSpecialNumber || offsetIsConstSpecialNumber)
            {
                calculator = new Number_OperatorCalculator(Double.NaN);
            }
            else if (signalIsConst)
            {
                calculator = new Round_ConstSignal_OperatorCalculator(signal, stepCalculator, offsetCalculator);
            }
            else if (stepIsConstOne && offsetIsConstZero)
            {
                calculator = new Round_VarSignal_StepOne_OffsetZero(signalCalculator);
            }
            else
            {
                if (stepIsConst && offsetIsConstZero)
                {
                    calculator = new Round_ConstStep_ZeroOffSet_OperatorCalculator(signalCalculator, step);
                }
                else if (stepIsConst && offsetIsConst)
                {
                    calculator = new Round_ConstStep_ConstOffSet_OperatorCalculator(signalCalculator, step, offset);
                }
                else if (stepIsConst && !offsetIsConst)
                {
                    calculator = new Round_ConstStep_VarOffSet_OperatorCalculator(signalCalculator, step, offsetCalculator);
                }
                else if (!stepIsConst && offsetIsConstZero)
                {
                    calculator = new Round_VarStep_ZeroOffSet_OperatorCalculator(signalCalculator, stepCalculator);
                }
                else if (!stepIsConst && offsetIsConst)
                {
                    calculator = new Round_VarStep_ConstOffSet_OperatorCalculator(signalCalculator, stepCalculator, offset);
                }
                else if (!stepIsConst && !offsetIsConst)
                {
                    calculator = new Round_VarStep_VarOffSet_OperatorCalculator(signalCalculator, stepCalculator, offsetCalculator);
                }
                else
                {
                    throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
                }
            }

            _stack.Push(calculator);
        }

        protected override void VisitSampleOperator(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase frequencyCalculator = _stack.Pop();

            frequencyCalculator = frequencyCalculator ?? new Zero_OperatorCalculator();
            double frequency = frequencyCalculator.Calculate(_defaultDimensionStack);
            bool frequencyIsConst = frequencyCalculator is Number_OperatorCalculator;
            bool frequencyIsConstZero = frequencyIsConst && frequency == 0.0;

            var wrapper = new Sample_OperatorWrapper(op, _sampleRepository);
            DimensionEnum dimensionEnum = wrapper.Dimension;

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
                ISampleCalculator sampleCalculator = _calculatorCache.GetSampleCalculator(sampleInfo);

                int sampleChannelCount = sampleInfo.Sample.GetChannelCount();

                if (sampleChannelCount == _channelCount)
                {
                    if (frequencyIsConst)
                    {
                        calculator = new Sample_WithConstFrequency_OperatorCalculator(frequency, sampleCalculator, dimensionEnum);
                    }
                    else
                    {
                        calculator = new Sample_WithVarFrequency_OperatorCalculator(frequencyCalculator, sampleCalculator, dimensionEnum);
                    }
                }
                else if (sampleChannelCount == 1 && _channelCount == 2)
                {
                    if (frequencyIsConst)
                    {
                        calculator = new Sample_WithConstFrequency_MonoToStereo_OperatorCalculator(frequency, sampleCalculator, dimensionEnum);
                    }
                    else
                    {
                        calculator = new Sample_WithVarFrequency_MonoToStereo_OperatorCalculator(frequencyCalculator, sampleCalculator, dimensionEnum);
                    }
                }
                else if (sampleChannelCount == 2 && _channelCount == 1)
                {
                    if (frequencyIsConst)
                    {
                        calculator = new Sample_WithConstFrequency_StereoToMono_OperatorCalculator(frequency, sampleCalculator, dimensionEnum);
                    }
                    else
                    {
                        calculator = new Sample_WithVarFrequency_StereoToMono_OperatorCalculator(frequencyCalculator, sampleCalculator, dimensionEnum);
                    }
                }
                else
                {
                    throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
                }
            }

            _stack.Push(calculator);
        }

        protected override void VisitSawDown(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase frequencyCalculator = _stack.Pop();
            OperatorCalculatorBase phaseShiftCalculator = _stack.Pop();

            frequencyCalculator = frequencyCalculator ?? new Zero_OperatorCalculator();
            phaseShiftCalculator = phaseShiftCalculator ?? new Zero_OperatorCalculator();
            double frequency = frequencyCalculator.Calculate(_defaultDimensionStack);
            double phaseShift = phaseShiftCalculator.Calculate(_defaultDimensionStack);
            bool frequencyIsConst = frequencyCalculator is Number_OperatorCalculator;
            bool phaseShiftIsConst = phaseShiftCalculator is Number_OperatorCalculator;
            bool frequencyIsConstZero = frequencyIsConst && frequency == 0;
            bool phaseShiftIsConstZero = phaseShiftIsConst && phaseShift % 1 == 0;

            var wrapper = new SawDown_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;

            if (frequencyIsConstZero)
            {
                // Weird number
                calculator = new Zero_OperatorCalculator();
            }
            else if (frequencyIsConst && phaseShiftIsConst)
            {
                calculator = new SawDown_WithConstFrequency_WithConstPhaseShift_OperatorCalculator(frequency, phaseShift, dimensionEnum);
            }
            else if (!frequencyIsConst && phaseShiftIsConst)
            {
                calculator = new SawDown_WithVarFrequency_WithConstPhaseShift_OperatorCalculator(frequencyCalculator, phaseShift, dimensionEnum);
            }
            else if (frequencyIsConst && !phaseShiftIsConst)
            {
                calculator = new SawDown_WithConstFrequency_WithVarPhaseShift_OperatorCalculator(frequency, phaseShiftCalculator, dimensionEnum);
            }
            else
            {
                calculator = new SawDown_WithVarFrequency_WithVarPhaseShift_OperatorCalculator(frequencyCalculator, phaseShiftCalculator, dimensionEnum);
            }

            _stack.Push(calculator);
        }

        protected override void VisitSawUp(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase frequencyCalculator = _stack.Pop();
            OperatorCalculatorBase phaseShiftCalculator = _stack.Pop();

            frequencyCalculator = frequencyCalculator ?? new Zero_OperatorCalculator();
            phaseShiftCalculator = phaseShiftCalculator ?? new Zero_OperatorCalculator();
            double frequency = frequencyCalculator.Calculate(_defaultDimensionStack);
            double phaseShift = phaseShiftCalculator.Calculate(_defaultDimensionStack);
            bool frequencyIsConst = frequencyCalculator is Number_OperatorCalculator;
            bool phaseShiftIsConst = phaseShiftCalculator is Number_OperatorCalculator;
            bool frequencyIsConstZero = frequencyIsConst && frequency == 0;
            bool phaseShiftIsConstZero = phaseShiftIsConst && phaseShift % 1 == 0;

            var wrapper = new SawUp_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;

            if (frequencyIsConstZero)
            {
                // Weird number
                calculator = new Zero_OperatorCalculator();
            }
            else if (frequencyIsConst && phaseShiftIsConst)
            {
                calculator = new SawUp_WithConstFrequency_WithConstPhaseShift_OperatorCalculator(frequency, phaseShift, dimensionEnum);
            }
            else if (!frequencyIsConst && phaseShiftIsConst)
            {
                calculator = new SawUp_WithVarFrequency_WithConstPhaseShift_OperatorCalculator(frequencyCalculator, phaseShift, dimensionEnum);
            }
            else if (frequencyIsConst && !phaseShiftIsConst)
            {
                calculator = new SawUp_WithConstFrequency_WithVarPhaseShift_OperatorCalculator(frequency, phaseShiftCalculator, dimensionEnum);
            }
            else
            {
                calculator = new SawUp_WithVarFrequency_WithVarPhaseShift_OperatorCalculator(frequencyCalculator, phaseShiftCalculator, dimensionEnum);
            }

            _stack.Push(calculator);
        }

        protected override void VisitScaler(Operator obj)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase sourceValueACalculator = _stack.Pop();
            OperatorCalculatorBase sourceValueBCalculator = _stack.Pop();
            OperatorCalculatorBase targetValueACalculator = _stack.Pop();
            OperatorCalculatorBase targetValueBCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            sourceValueACalculator = sourceValueACalculator ?? new Zero_OperatorCalculator();
            sourceValueBCalculator = sourceValueBCalculator ?? new Zero_OperatorCalculator();
            targetValueACalculator = targetValueACalculator ?? new Zero_OperatorCalculator();
            targetValueBCalculator = targetValueBCalculator ?? new Zero_OperatorCalculator();

            calculator = new Scaler_OperatorCalculator(signalCalculator, sourceValueACalculator, sourceValueBCalculator, targetValueACalculator, targetValueBCalculator);

            _stack.Push(calculator);
        }

        protected override void VisitSelect(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase dimensionValueCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            dimensionValueCalculator = dimensionValueCalculator ?? new Zero_OperatorCalculator();

            double signal = signalCalculator.Calculate(_defaultDimensionStack);
            double dimensionValue = dimensionValueCalculator.Calculate(_defaultDimensionStack);
            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool dimensionValueIsConst = dimensionValueCalculator is Number_OperatorCalculator;

            var wrapper = new Select_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;

            if (signalIsConst)
            {
                calculator = new Number_OperatorCalculator(signal);
            }
            else if (dimensionValueIsConst)
            {
                calculator = new Select_WithConstDimensionValue_OperatorCalculator(signalCalculator, dimensionValue, dimensionEnum);
            }
            else
            {
                calculator = new Select_OperatorCalculator(signalCalculator, dimensionValueCalculator, dimensionEnum);
            }

            _stack.Push(calculator);
        }

        protected override void VisitSetDimension(Operator op)
        {
            OperatorCalculatorBase operatorCalculator;

            OperatorCalculatorBase calculationCalculator = _stack.Pop();
            OperatorCalculatorBase valueCalculator = _stack.Pop();

            calculationCalculator = calculationCalculator ?? new Zero_OperatorCalculator();
            valueCalculator = valueCalculator ?? new Zero_OperatorCalculator();

            bool calculationIsConst = calculationCalculator is Number_OperatorCalculator;
            bool valueIsConst = valueCalculator is Number_OperatorCalculator;

            double value = valueCalculator.Calculate(_defaultDimensionStack);

            var wrapper = new SetDimension_OperatorWrapper(op);

            if (calculationIsConst)
            {
                operatorCalculator = calculationCalculator;
            }
            else if (valueIsConst)
            {
                operatorCalculator = new SetDimension_OperatorCalculator_ConstValue(calculationCalculator, value, wrapper.Dimension);
            }
            else
            {
                operatorCalculator = new SetDimension_OperatorCalculator_VarValue(calculationCalculator, valueCalculator, wrapper.Dimension);
            }

            _stack.Push(operatorCalculator);
        }

        protected override void VisitShift(Operator op)
        {
            // Shift is a synonym for Delay, that makes more sense in case the x-axis does not represent time.
            VisitDelay(op);
        }

        protected override void VisitSine(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase frequencyCalculator = _stack.Pop();
            OperatorCalculatorBase phaseShiftCalculator = _stack.Pop();

            frequencyCalculator = frequencyCalculator ?? new Zero_OperatorCalculator();
            phaseShiftCalculator = phaseShiftCalculator ?? new Zero_OperatorCalculator();
            double frequency = frequencyCalculator.Calculate(_defaultDimensionStack);
            double phaseShift = phaseShiftCalculator.Calculate(_defaultDimensionStack);
            bool frequencyIsConst = frequencyCalculator is Number_OperatorCalculator;
            bool phaseShiftIsConst = phaseShiftCalculator is Number_OperatorCalculator;
            bool frequencyIsConstZero = frequencyIsConst && frequency == 0.0;
            bool phaseShiftIsConstZero = phaseShiftIsConst && phaseShift % 1.0 == 0.0;

            var wrapper = new Sine_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;

            if (frequencyIsConstZero)
            {
                // Weird number
                calculator = new Zero_OperatorCalculator();
            }
            else if (frequencyIsConst && phaseShiftIsConstZero)
            {
                calculator = new Sine_WithConstFrequency_WithoutPhaseShift_OperatorCalculator(frequency, dimensionEnum);
            }
            else if (frequencyIsConst && phaseShiftIsConst)
            {
                calculator = new Sine_WithConstFrequency_WithConstPhaseShift_OperatorCalculator(frequency, phaseShift, dimensionEnum);
            }
            else if (frequencyIsConst && !phaseShiftIsConst)
            {
                calculator = new Sine_WithConstFrequency_WithVarPhaseShift_OperatorCalculator(frequency, phaseShiftCalculator, dimensionEnum);
            }
            else if (!frequencyIsConst && phaseShiftIsConstZero)
            {
                calculator = new Sine_WithVarFrequency_WithoutPhaseShift_OperatorCalculator(frequencyCalculator, dimensionEnum);
            }
            else if (!frequencyIsConst && phaseShiftIsConst)
            {
                calculator = new Sine_WithVarFrequency_WithConstPhaseShift_OperatorCalculator(frequencyCalculator, phaseShift, dimensionEnum);
            }
            else
            {
                calculator = new Sine_WithVarFrequency_WithVarPhaseShift_OperatorCalculator(frequencyCalculator, phaseShiftCalculator, dimensionEnum);
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

            double signal = signalCalculator.Calculate(_defaultDimensionStack);
            double factor = factorCalculator.Calculate(_defaultDimensionStack);

            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool factorIsConst = factorCalculator is Number_OperatorCalculator;

            bool signalIsConstZero = signalIsConst && signal == 0;
            bool factorIsConstZero = factorIsConst && factor == 0;

            bool factorIsConstOne = factorIsConst && factor == 1;

            bool signalIsConstSpecialNumber = signalIsConst && (Double.IsNaN(signal) || Double.IsInfinity(signal));
            bool factorIsConstSpecialNumber = factorIsConst && (Double.IsNaN(factor) || Double.IsInfinity(factor));

            var wrapper = new SlowDown_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;

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
                calculator = new SlowDown_WithConstFactor_OperatorCalculator(signalCalculator, factor, dimensionEnum);
            }
            else
            {
                calculator = new SlowDown_WithVarFactor_OperatorCalculator(signalCalculator, factorCalculator, dimensionEnum);
            }

            _stack.Push(calculator);
        }

        protected override void VisitSpectrum(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase startTimeCalculator = _stack.Pop();
            OperatorCalculatorBase endTimeCalculator = _stack.Pop();
            OperatorCalculatorBase frequencyCountCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            // TODO: Do not use these magic defaults, but give standard operators default inlet value functionality.
            startTimeCalculator = startTimeCalculator ?? new Number_OperatorCalculator(0.0);
            endTimeCalculator = endTimeCalculator ?? new Number_OperatorCalculator(1.0);
            frequencyCountCalculator = frequencyCountCalculator ?? new Number_OperatorCalculator(16);

            double signal = signalCalculator.Calculate(_defaultDimensionStack);
            double startTime = startTimeCalculator.Calculate(_defaultDimensionStack);
            double endTime = endTimeCalculator.Calculate(_defaultDimensionStack);
            double frequencyCount = frequencyCountCalculator.Calculate(_defaultDimensionStack);

            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool signalIsConstZero = signalIsConst && signal == 0;
            bool signalIsConstSpecialNumber = signalIsConst && (Double.IsNaN(signal) || Double.IsInfinity(signal));

            if (signalIsConstSpecialNumber)
            {
                // Weird number
                calculator = new Number_OperatorCalculator(Double.NaN);
            }
            else if (signalIsConstZero)
            {
                calculator = new Zero_OperatorCalculator();
            }
            else if (signalIsConst)
            {
                calculator = new Zero_OperatorCalculator();
            }
            else
            {
                calculator = new Spectrum_OperatorCalculator(
                    signalCalculator,
                    startTimeCalculator,
                    endTimeCalculator,
                    frequencyCountCalculator);
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

            double signal = signalCalculator.Calculate(_defaultDimensionStack);
            double factor = factorCalculator.Calculate(_defaultDimensionStack);

            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool factorIsConst = factorCalculator is Number_OperatorCalculator;

            bool signalIsConstZero = signalIsConst && signal == 0;
            bool factorIsConstZero = factorIsConst && factor == 0;

            bool factorIsConstOne = factorIsConst && factor == 1;

            bool signalIsConstSpecialNumber = signalIsConst && (Double.IsNaN(signal) || Double.IsInfinity(signal));
            bool factorIsConstSpecialNumber = factorIsConst && (Double.IsNaN(factor) || Double.IsInfinity(factor));

            var wrapper = new SpeedUp_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;

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
                calculator = new SpeedUp_WithConstFactor_OperatorCalculator(signalCalculator, factor, dimensionEnum);
            }
            else
            {
                calculator = new SpeedUp_WithVarFactor_OperatorCalculator(signalCalculator, factorCalculator, dimensionEnum);
            }

            _stack.Push(calculator);
        }

        protected override void VisitSquare(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase frequencyCalculator = _stack.Pop();
            OperatorCalculatorBase phaseShiftCalculator = _stack.Pop();

            frequencyCalculator = frequencyCalculator ?? new Zero_OperatorCalculator();
            phaseShiftCalculator = phaseShiftCalculator ?? new Zero_OperatorCalculator();

            double frequency = frequencyCalculator.Calculate(_defaultDimensionStack);
            double phaseShift = phaseShiftCalculator.Calculate(_defaultDimensionStack) % 1;

            bool frequencyIsConst = frequencyCalculator is Number_OperatorCalculator;
            bool phaseShiftIsConst = phaseShiftCalculator is Number_OperatorCalculator;

            bool frequencyIsConstZero = frequencyIsConst && frequency == 0;

            bool frequencyIsConstSpecialNumber = frequencyIsConst && (Double.IsNaN(frequency) || Double.IsInfinity(frequency));
            bool phaseShiftIsConstSpecialNumber = phaseShiftIsConst && (Double.IsNaN(phaseShift) || Double.IsInfinity(phaseShift));

            var wrapper = new Square_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;

            if (frequencyIsConstSpecialNumber || phaseShiftIsConstSpecialNumber)
            {
                // Weird number
                calculator = new Number_OperatorCalculator(Double.NaN);
            }
            if (frequencyIsConstZero)
            {
                // Weird number
                calculator = new Zero_OperatorCalculator();
            }
            else if (frequencyIsConst && phaseShiftIsConst)
            {
                calculator = new Pulse_ConstFrequency_ConstWidth_ConstPhaseShift_OperatorCalculator(frequency, DEFAULT_PULSE_WIDTH, phaseShift, dimensionEnum);
            }
            else if (!frequencyIsConst && phaseShiftIsConst)
            {
                calculator = new Pulse_VarFrequency_ConstWidth_ConstPhaseShift_OperatorCalculator(frequencyCalculator, DEFAULT_PULSE_WIDTH, phaseShift, dimensionEnum);
            }
            else if (frequencyIsConst && !phaseShiftIsConst)
            {
                calculator = new Pulse_ConstFrequency_ConstWidth_VarPhaseShift_OperatorCalculator(frequency, DEFAULT_PULSE_WIDTH, phaseShiftCalculator, dimensionEnum);
            }
            else if (!frequencyIsConst && !phaseShiftIsConst)
            {
                calculator = new Pulse_VarFrequency_ConstWidth_VarPhaseShift_OperatorCalculator(frequencyCalculator, DEFAULT_PULSE_WIDTH, phaseShiftCalculator, dimensionEnum);
            }
            else
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
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

            double signal = signalCalculator.Calculate(_defaultDimensionStack);
            double factor = factorCalculator.Calculate(_defaultDimensionStack);
            double origin = originCalculator.Calculate(_defaultDimensionStack);

            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool factorIsConst = factorCalculator is Number_OperatorCalculator;
            bool originIsConst = originCalculator is Number_OperatorCalculator;

            bool signalIsConstZero = signalIsConst && signal == 0;
            bool factorIsConstZero = factorIsConst && factor == 0;
            bool originIsConstZero = originIsConst && origin == 0;

            bool factorIsConstOne = factorIsConst && factor == 1;

            // TODO: Handle const special numbers.

            var wrapper = new Stretch_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;

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
                calculator = new Stretch_VarSignal_ConstFactor_ZeroOrigin_OperatorCalculator(signalCalculator, factor, dimensionEnum);
            }
            else if (!signalIsConst && !factorIsConst && originIsConstZero)
            {
                calculator = new Stretch_VarSignal_VarFactor_ZeroOrigin_OperatorCalculator(signalCalculator, factorCalculator, dimensionEnum);
            }
            else if (!signalIsConst && factorIsConst && originIsConst)
            {
                calculator = new Stretch_VarSignal_ConstFactor_ConstOrigin_OperatorCalculator(signalCalculator, factor, origin, dimensionEnum);
            }
            else if (!signalIsConst && factorIsConst && !originIsConst)
            {
                calculator = new Stretch_VarSignal_ConstFactor_VarOrigin_OperatorCalculator(signalCalculator, factor, originCalculator, dimensionEnum);
            }
            else if (!signalIsConst && !factorIsConst && originIsConst)
            {
                calculator = new Stretch_VarSignal_VarFactor_ConstOrigin_OperatorCalculator(signalCalculator, factorCalculator, origin, dimensionEnum);
            }
            else if (!signalIsConst && !factorIsConst && !originIsConst)
            {
                calculator = new Stretch_VarSignal_VarFactor_VarOrigin_OperatorCalculator(signalCalculator, factorCalculator, originCalculator, dimensionEnum);
            }
            else
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
            }

            _stack.Push(calculator);
        }

        protected override void VisitSubtract(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase aCalculator = _stack.Pop();
            OperatorCalculatorBase bCalculator = _stack.Pop();

            aCalculator = aCalculator ?? new Zero_OperatorCalculator();
            bCalculator = bCalculator ?? new Zero_OperatorCalculator();

            double a = aCalculator.Calculate(_defaultDimensionStack);
            double b = bCalculator.Calculate(_defaultDimensionStack);
            bool aIsConst = aCalculator is Number_OperatorCalculator;
            bool bIsConst = bCalculator is Number_OperatorCalculator;
            bool aIsConstZero = aIsConst && a == 0;
            bool bIsConstZero = bIsConst && b == 0;

            if (aIsConstZero && bIsConstZero)
            {
                calculator = new Zero_OperatorCalculator();
            }
            else if (bIsConstZero)
            {
                calculator = aCalculator;
            }
            else if (aIsConst && bIsConst)
            {
                calculator = new Number_OperatorCalculator(a - b);
            }
            else if (aIsConst)
            {
                calculator = new Subtract_ConstA_VarB_OperatorCalculator(a, bCalculator);
            }
            else if (bIsConst)
            {
                calculator = new Subtract_VarA_ConstB_OperatorCalculator(aCalculator, b);
            }
            else
            {
                calculator = new Subtract_VarA_VarB_OperatorCalculator(aCalculator, bCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitTimePower(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop(); ;
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

            double signal = signalCalculator.Calculate(_defaultDimensionStack);
            double exponent = exponentCalculator.Calculate(_defaultDimensionStack);
            double origin = originCalculator.Calculate(_defaultDimensionStack);
            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool exponentIsConst = exponentCalculator is Number_OperatorCalculator;
            bool originIsConst = originCalculator is Number_OperatorCalculator;
            bool signalIsConstZero = signalIsConst && signal == 0;
            bool exponentIsConstZero = exponentIsConst && exponent == 0;
            bool originIsConstZero = originIsConst && origin == 0;
            bool exponentIsConstOne = exponentIsConst && exponent == 1;

            var wrapper = new TimePower_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;

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
                calculator = new TimePower_WithoutOrigin_OperatorCalculator(signalCalculator, exponentCalculator, dimensionEnum);
            }
            else
            {
                calculator = new TimePower_WithOrigin_OperatorCalculator(signalCalculator, exponentCalculator, originCalculator, dimensionEnum);
            }

            _stack.Push(calculator);
        }

        protected override void VisitToggleTrigger(Operator op)
        {
            OperatorCalculatorBase operatorCalculator;

            OperatorCalculatorBase calculationCalculator = _stack.Pop();
            OperatorCalculatorBase resetCalculator = _stack.Pop();

            calculationCalculator = calculationCalculator ?? new Zero_OperatorCalculator();
            resetCalculator = resetCalculator ?? new Zero_OperatorCalculator();

            bool calculationIsConst = calculationCalculator is Number_OperatorCalculator;
            bool resetIsConst = resetCalculator is Number_OperatorCalculator;

            if (calculationIsConst)
            {
                operatorCalculator = calculationCalculator;
            }
            else if (resetIsConst)
            {
                operatorCalculator = calculationCalculator;
            }
            else
            {
                operatorCalculator = new ToggleTrigger_OperatorCalculator(calculationCalculator, resetCalculator);
            }

            _stack.Push(operatorCalculator);
        }

        protected override void VisitTriangle(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase frequencyCalculator = _stack.Pop();
            OperatorCalculatorBase phaseShiftCalculator = _stack.Pop();

            frequencyCalculator = frequencyCalculator ?? new Zero_OperatorCalculator();
            phaseShiftCalculator = phaseShiftCalculator ?? new Zero_OperatorCalculator();
            double frequency = frequencyCalculator.Calculate(_defaultDimensionStack);
            double phaseShift = phaseShiftCalculator.Calculate(_defaultDimensionStack);
            bool frequencyIsConst = frequencyCalculator is Number_OperatorCalculator;
            bool phaseShiftIsConst = phaseShiftCalculator is Number_OperatorCalculator;
            bool frequencyIsConstZero = frequencyIsConst && frequency == 0;

            var wrapper = new Triangle_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;

            if (frequencyIsConstZero)
            {
                // Weird number
                calculator = new Zero_OperatorCalculator();
            }
            else if (frequencyIsConst && phaseShiftIsConst)
            {
                calculator = new Triangle_WithConstFrequency_WithConstPhaseShift_OperatorCalculator(frequency, phaseShift, dimensionEnum);
            }
            else if (!frequencyIsConst && phaseShiftIsConst)
            {
                calculator = new Triangle_WithVarFrequency_WithConstPhaseShift_OperatorCalculator(frequencyCalculator, phaseShift, dimensionEnum);
            }
            else if (frequencyIsConst && !phaseShiftIsConst)
            {
                calculator = new Triangle_WithConstFrequency_WithVarPhaseShift_OperatorCalculator(frequency, phaseShiftCalculator, dimensionEnum);
            }
            else
            {
                calculator = new Triangle_WithVarFrequency_WithVarPhaseShift_OperatorCalculator(frequencyCalculator, phaseShiftCalculator, dimensionEnum);
            }

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
            double input = inputCalculator.Calculate(_defaultDimensionStack);
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
                        dimensionEnum: inlet.GetDimensionEnum(),
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

            return op.Patch.ID == _outlet.Operator.Patch.ID;
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

        protected override void VisitReset(Operator op)
        {
            var wrapper = new Reset_OperatorWrapper(op);

            OperatorCalculatorBase calculator = _stack.Peek();

            // Be forgiving when it comes to name being filled in. A warning is generated instead.
            _resettableOperatorTuples.Add(new ResettableOperatorTuple(calculator, op.Name, wrapper.ListIndex));
        }
    }
}