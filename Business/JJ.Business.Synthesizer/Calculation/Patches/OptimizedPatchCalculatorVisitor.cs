using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Calculation.Curves;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Calculation.Random;
using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation.Operators;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Common;
using JJ.Framework.Common.Exceptions;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;

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
                DimensionStackCollection dimensionStackCollection,
                OperatorCalculatorBase output_OperatorCalculator,
                IList<VariableInput_OperatorCalculator> input_OperatorCalculators,
                IList<ResettableOperatorTuple> resettableOperatorTuples)
            {
                DimensionStackCollection = dimensionStackCollection;
                Output_OperatorCalculator = output_OperatorCalculator;
                Input_OperatorCalculators = input_OperatorCalculators;
                ResettableOperatorTuples = resettableOperatorTuples;
            }

            public DimensionStackCollection DimensionStackCollection { get; }
            public OperatorCalculatorBase Output_OperatorCalculator { get; }
            public IList<VariableInput_OperatorCalculator> Input_OperatorCalculators { get; }
            public IList<ResettableOperatorTuple> ResettableOperatorTuples { get; }
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

            public OperatorCalculatorBase OperatorCalculator { get; }
            public string Name { get; }
            public int? ListIndex { get; }
        }

        /// <summary>
        /// Feature switch: not being able to vary bundle dimension values
        /// during the calculation is faster, but would not give the expected behavior.
        /// However, we keep the code alive, to be able to experiment with the performance impact.
        /// </summary>
        private const bool BUNDLE_POSITIONS_ARE_INVARIANT = false;

        private const double DEFAULT_PULSE_WIDTH = 0.5;
        private const double DEFAULT_DIMENSION_VALUE = 0.0;

        private readonly ICurveRepository _curveRepository;
        private readonly ISampleRepository _sampleRepository;
        private readonly IPatchRepository _patchRepository;
        private readonly ISpeakerSetupRepository _speakerSetupRepository;
        private readonly CalculatorCache _calculatorCache;

        private int _channelCount;
        private Stack<OperatorCalculatorBase> _stack;
        private DimensionStackCollection _dimensionStackCollection;

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
            _dimensionStackCollection = new DimensionStackCollection();
            _operator_NoiseOffsetInSeconds_Dictionary = new Dictionary<Operator, double>();
            _operator_RandomOffsetInSeconds_Dictionary = new Dictionary<Operator, int>();
            _patchInlet_Calculator_Dictionary = new Dictionary<Operator, VariableInput_OperatorCalculator>();
            _resettableOperatorTuples = new List<ResettableOperatorTuple>();

            _outlet = outlet;
            _channelCount = channelCount;

            VisitOutlet(outlet);

            OperatorCalculatorBase outputOperatorCalculator = _stack.Pop();

            if (_stack.Count != 0)
            {
                throw new NotEqualException(() => _stack.Count, 0);
            }

            foreach (DimensionEnum dimensionEnum in EnumHelper.GetValues<DimensionEnum>())
            {
                DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);
                if (dimensionStack.Count != 1) // 1, because a single item is added by default as when the DimensionStackCollection is initialized.
                {
                    throw new Exception(String.Format("DimensionStack.Count for DimensionEnum '{0}' should be 1 but it is {1}.", dimensionEnum, dimensionStack.Count));
                }
            }

            return new Result(
                _dimensionStackCollection,
                outputOperatorCalculator,
                _patchInlet_Calculator_Dictionary.Values.ToArray(),
                _resettableOperatorTuples);
        }

        protected override void VisitAbsolute(Operator op)
        {
            base.VisitAbsolute(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase calculatorX = _stack.Pop();
            calculatorX = calculatorX ?? new Zero_OperatorCalculator();
            double x = calculatorX.Calculate();
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
            base.VisitAdd(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase aCalculator = _stack.Pop();
            OperatorCalculatorBase bCalculator = _stack.Pop();

            aCalculator = aCalculator ?? new Zero_OperatorCalculator();
            bCalculator = bCalculator ?? new Zero_OperatorCalculator();

            double a = aCalculator.Calculate();
            double b = bCalculator.Calculate();
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
                calculator = new Add_OperatorCalculator_ConstA_VarB(a, bCalculator);
            }
            else if (bIsConst)
            {
                calculator = new Add_OperatorCalculator_VarA_ConstB(aCalculator, b);
            }
            else
            {
                calculator = new Add_OperatorCalculator_VarA_VarB(aCalculator, bCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitAnd(Operator op)
        {
            base.VisitAnd(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase calculatorA = _stack.Pop();
            OperatorCalculatorBase calculatorB = _stack.Pop();

            calculatorA = calculatorA ?? new Zero_OperatorCalculator();
            calculatorB = calculatorB ?? new Zero_OperatorCalculator();

            double a = calculatorA.Calculate();
            double b = calculatorB.Calculate();

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
            var wrapper = new Average_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);

            base.VisitAverage(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeSliceDurationCalculator = _stack.Pop();
            OperatorCalculatorBase sampleCountCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            double signal = signalCalculator.Calculate();
            bool signalIsConst = signalCalculator is Number_OperatorCalculator;

            // TODO: Lower priority: Do not use these magic defaults, but give standard operators default inlet value functionality.
            timeSliceDurationCalculator = timeSliceDurationCalculator ?? new Number_OperatorCalculator(0.02);
            sampleCountCalculator = sampleCountCalculator ?? new Number_OperatorCalculator(100.0);

            if (signalIsConst)
            {
                calculator = signalCalculator;
            }
            else
            {
                calculator = new Average_OperatorCalculator(signalCalculator, timeSliceDurationCalculator, sampleCountCalculator, dimensionStack);
            }

            _stack.Push(calculator);
        }

        protected override void VisitBundle(Operator op)
        {
            if (BUNDLE_POSITIONS_ARE_INVARIANT)
            {
                throw new Exception("VisitBundle should not execute if BUNDLE_POSITIONS_ARE_INVARIANT.");
            }

            var wrapper = new Bundle_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);

            // No pushing and popping from the dimension stack here.

            base.VisitBundle(op);

            var operandCalculators = new List<OperatorCalculatorBase>(op.Inlets.Count);

            for (int i = 0; i < op.Inlets.Count; i++)
            {
                OperatorCalculatorBase operandCalculator = _stack.Pop();

                operandCalculator = operandCalculator ?? new Zero_OperatorCalculator();

                operandCalculators.Add(operandCalculator);
            }

            OperatorCalculatorBase calculator = new Bundle_OperatorCalculator(dimensionStack, operandCalculators);

            _stack.Push(calculator);
        }

        protected override void VisitCache(Operator op)
        {
            var wrapper = new Cache_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);
            DimensionStack channelDimensionStack = _dimensionStackCollection.GetDimensionStack(DimensionEnum.Channel);

            base.VisitCache(op);

            OperatorCalculatorBase calculator = null;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase startDateCalculator = _stack.Pop();
            OperatorCalculatorBase endDateCalculator = _stack.Pop();
            OperatorCalculatorBase samplingRateCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            // TODO: Lower priority: Do not use these magic defaults, but give standard operators default inlet value functionality.
            startDateCalculator = startDateCalculator ?? new Number_OperatorCalculator(0.0);
            endDateCalculator = endDateCalculator ?? new Number_OperatorCalculator(1.0);
            samplingRateCalculator = samplingRateCalculator ?? new Number_OperatorCalculator(44100.0);

            double signal = signalCalculator.Calculate();
            double startTime = startDateCalculator.Calculate();
            double endTime = endDateCalculator.Calculate();
            double samplingRate = samplingRateCalculator.Calculate();

            bool signalIsConst = signalCalculator is Number_OperatorCalculator;

            // We need a lot of lenience in this code, because validity is dependent on user input,
            // and this validity cannot be checked on the entity level, only when starting the calculation.
            // In theory I could generate additional messages in the calculation optimization process,
            // but we should keep it possible to reoptimize in runtime, and we cannot obtrusively interrupt
            // the user with validation messages, because he is busy making music and the show must go on.
            bool startTimeIsValid = !DoubleHelper.IsSpecialNumber(startTime);
            bool endTimeIsValid = !DoubleHelper.IsSpecialNumber(endTime);
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
                                calculator = new Cache_OperatorCalculator_SingleChannel<ArrayCalculator_MinPosition_Block>(castedArrayCalculator, dimensionStack);
                            }
                        }
                        {
                            var castedArrayCalculator = arrayCalculator as ArrayCalculator_MinPosition_Cubic;
                            if (castedArrayCalculator != null)
                            {
                                calculator = new Cache_OperatorCalculator_SingleChannel<ArrayCalculator_MinPosition_Cubic>(castedArrayCalculator, dimensionStack);
                            }
                        }
                        {
                            var castedArrayCalculator = arrayCalculator as ArrayCalculator_MinPosition_Hermite;
                            if (castedArrayCalculator != null)
                            {
                                calculator = new Cache_OperatorCalculator_SingleChannel<ArrayCalculator_MinPosition_Hermite>(castedArrayCalculator, dimensionStack);
                            }
                        }
                        {
                            var castedArrayCalculator = arrayCalculator as ArrayCalculator_MinPosition_Line;
                            if (castedArrayCalculator != null)
                            {
                                calculator = new Cache_OperatorCalculator_SingleChannel<ArrayCalculator_MinPosition_Line>(castedArrayCalculator, dimensionStack);
                            }
                        }
                        {
                            var castedArrayCalculator = arrayCalculator as ArrayCalculator_MinPosition_Stripe;
                            if (castedArrayCalculator != null)
                            {
                                calculator = new Cache_OperatorCalculator_SingleChannel<ArrayCalculator_MinPosition_Stripe>(castedArrayCalculator, dimensionStack);
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
                                    calculator = new Cache_OperatorCalculator_MultiChannel<ArrayCalculator_MinPosition_Block>(castedArrayCalculators, dimensionStack, channelDimensionStack);
                                    break;
                                }

                            case InterpolationTypeEnum.Cubic:
                                {
                                    IList<ArrayCalculator_MinPosition_Cubic> castedArrayCalculators = arrayCalculators.Select(x => (ArrayCalculator_MinPosition_Cubic)x).ToArray();
                                    calculator = new Cache_OperatorCalculator_MultiChannel<ArrayCalculator_MinPosition_Cubic>(castedArrayCalculators, dimensionStack, channelDimensionStack);
                                    break;
                                }

                            case InterpolationTypeEnum.Hermite:
                                {
                                    IList<ArrayCalculator_MinPosition_Hermite> castedArrayCalculators = arrayCalculators.Select(x => (ArrayCalculator_MinPosition_Hermite)x).ToArray();
                                    calculator = new Cache_OperatorCalculator_MultiChannel<ArrayCalculator_MinPosition_Hermite>(castedArrayCalculators, dimensionStack, channelDimensionStack);
                                    break;
                                }

                            case InterpolationTypeEnum.Line:
                                {
                                    IList<ArrayCalculator_MinPosition_Line> castedArrayCalculators = arrayCalculators.Select(x => (ArrayCalculator_MinPosition_Line)x).ToArray();
                                    calculator = new Cache_OperatorCalculator_MultiChannel<ArrayCalculator_MinPosition_Line>(castedArrayCalculators, dimensionStack, channelDimensionStack);
                                    break;
                                }

                            case InterpolationTypeEnum.Stripe:
                                {
                                    IList<ArrayCalculator_MinPosition_Stripe> castedArrayCalculators = arrayCalculators.Select(x => (ArrayCalculator_MinPosition_Stripe)x).ToArray();
                                    calculator = new Cache_OperatorCalculator_MultiChannel<ArrayCalculator_MinPosition_Stripe>(castedArrayCalculators, dimensionStack, channelDimensionStack);
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
                                calculator = new Cache_OperatorCalculator_SingleChannel<ArrayCalculator_MinPositionZero_Block>(castedArrayCalculator, dimensionStack);
                            }
                        }
                        {
                            var castedArrayCalculator = arrayCalculator as ArrayCalculator_MinPositionZero_Cubic;
                            if (castedArrayCalculator != null)
                            {
                                calculator = new Cache_OperatorCalculator_SingleChannel<ArrayCalculator_MinPositionZero_Cubic>(castedArrayCalculator, dimensionStack);
                            }
                        }
                        {
                            var castedArrayCalculator = arrayCalculator as ArrayCalculator_MinPositionZero_Hermite;
                            if (castedArrayCalculator != null)
                            {
                                calculator = new Cache_OperatorCalculator_SingleChannel<ArrayCalculator_MinPositionZero_Hermite>(castedArrayCalculator, dimensionStack);
                            }
                        }
                        {
                            var castedArrayCalculator = arrayCalculator as ArrayCalculator_MinPositionZero_Line;
                            if (castedArrayCalculator != null)
                            {
                                calculator = new Cache_OperatorCalculator_SingleChannel<ArrayCalculator_MinPositionZero_Line>(castedArrayCalculator, dimensionStack);
                            }
                        }
                        {
                            var castedArrayCalculator = arrayCalculator as ArrayCalculator_MinPositionZero_Stripe;
                            if (castedArrayCalculator != null)
                            {
                                calculator = new Cache_OperatorCalculator_SingleChannel<ArrayCalculator_MinPositionZero_Stripe>(castedArrayCalculator, dimensionStack);
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
                                    calculator = new Cache_OperatorCalculator_MultiChannel<ArrayCalculator_MinPositionZero_Block>(castedArrayCalculators, dimensionStack, channelDimensionStack);
                                    break;
                                }

                            case InterpolationTypeEnum.Cubic:
                                {
                                    IList<ArrayCalculator_MinPositionZero_Cubic> castedArrayCalculators = arrayCalculators.Select(x => (ArrayCalculator_MinPositionZero_Cubic)x).ToArray();
                                    calculator = new Cache_OperatorCalculator_MultiChannel<ArrayCalculator_MinPositionZero_Cubic>(castedArrayCalculators, dimensionStack, channelDimensionStack);
                                    break;
                                }

                            case InterpolationTypeEnum.Hermite:
                                {
                                    IList<ArrayCalculator_MinPositionZero_Hermite> castedArrayCalculators = arrayCalculators.Select(x => (ArrayCalculator_MinPositionZero_Hermite)x).ToArray();
                                    calculator = new Cache_OperatorCalculator_MultiChannel<ArrayCalculator_MinPositionZero_Hermite>(castedArrayCalculators, dimensionStack, channelDimensionStack);
                                    break;
                                }

                            case InterpolationTypeEnum.Line:
                                {
                                    IList<ArrayCalculator_MinPositionZero_Line> castedArrayCalculators = arrayCalculators.Select(x => (ArrayCalculator_MinPositionZero_Line)x).ToArray();
                                    calculator = new Cache_OperatorCalculator_MultiChannel<ArrayCalculator_MinPositionZero_Line>(castedArrayCalculators, dimensionStack, channelDimensionStack);
                                    break;
                                }

                            case InterpolationTypeEnum.Stripe:
                                {
                                    IList<ArrayCalculator_MinPositionZero_Stripe> castedArrayCalculators = arrayCalculators.Select(x => (ArrayCalculator_MinPositionZero_Stripe)x).ToArray();
                                    calculator = new Cache_OperatorCalculator_MultiChannel<ArrayCalculator_MinPositionZero_Stripe>(castedArrayCalculators, dimensionStack, channelDimensionStack);
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

        protected override void VisitChangeTrigger(Operator op)
        {
            base.VisitChangeTrigger(op);

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

        protected override void VisitCurveOperator(Operator op)
        {
            var wrapper = new Curve_OperatorWrapper(op, _curveRepository);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);

            base.VisitCurveOperator(op);

            OperatorCalculatorBase calculator = null;

            Curve curve = wrapper.Curve;
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
                    if (dimensionEnum == DimensionEnum.Time)
                    {
                        calculator = new Curve_OperatorCalculator_MinX_WithOriginShifting(curveCalculator_MinTime, dimensionStack);
                    }
                    else
                    {
                        calculator = new Curve_OperatorCalculator_MinX_NoOriginShifting(curveCalculator_MinTime, dimensionStack);
                    }
                }

                var curveCalculator_MinTimeZero = curveCalculator as CurveCalculator_MinXZero;
                if (curveCalculator_MinTimeZero != null)
                {
                    if (dimensionEnum == DimensionEnum.Time)
                    {
                        calculator = new Curve_OperatorCalculator_MinXZero_WithOriginShifting(curveCalculator_MinTimeZero, dimensionStack);
                    }
                    else
                    {
                        calculator = new Curve_OperatorCalculator_MinXZero_NoOriginShifting(curveCalculator_MinTimeZero, dimensionStack);
                    }
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
            var wrapper = new Delay_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);
            dimensionStack.Push(DEFAULT_DIMENSION_VALUE);

            base.VisitDelay(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeDifferenceCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            timeDifferenceCalculator = timeDifferenceCalculator ?? new Zero_OperatorCalculator();
            double signal = signalCalculator.Calculate();
            double timeDifference = timeDifferenceCalculator.Calculate();
            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool timeDifferenceIsConst = timeDifferenceCalculator is Number_OperatorCalculator;
            bool signalIsConstZero = signalIsConst && signal == 0;
            bool timeDifferenceIsConstZero = timeDifferenceIsConst && timeDifference == 0;

            dimensionStack.Pop();

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
                calculator = new Delay_OperatorCalculator_VarSignal_ConstDistance(signalCalculator, timeDifference, dimensionStack);
            }
            else
            {
                calculator = new Delay_OperatorCalculator_VarSignal_VarDistance(signalCalculator, timeDifferenceCalculator, dimensionStack);
            }

            _stack.Push(calculator);
        }

        protected override void VisitDivide(Operator op)
        {
            base.VisitDivide(op);

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

            double numerator = numeratorCalculator.Calculate();
            double denominator = denominatorCalculator.Calculate();
            double origin = originCalculator.Calculate();
            bool denominatorIsConst = denominatorCalculator is Number_OperatorCalculator;
            bool numeratorIsConst = numeratorCalculator is Number_OperatorCalculator;
            bool originIsConst = originCalculator is Number_OperatorCalculator;
            bool numeratorIsConstZero = numeratorIsConst && numerator == 0;
            bool denominatorIsConstZero = denominatorIsConst && denominator == 0;
            bool originIsConstZero = originIsConst && origin == 0;
            bool denominatorIsConstOne = denominatorIsConst && denominator == 1;

            if (denominatorIsConstZero)
            {
                // Special number
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
            var wrapper = new Earlier_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);
            dimensionStack.Push(DEFAULT_DIMENSION_VALUE);

            base.VisitEarlier(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeDifferenceCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            timeDifferenceCalculator = timeDifferenceCalculator ?? new Zero_OperatorCalculator();
            double signal = signalCalculator.Calculate();
            double timeDifference = timeDifferenceCalculator.Calculate();
            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool timeDifferenceIsConst = timeDifferenceCalculator is Number_OperatorCalculator;
            bool signalIsConstZero = signalIsConst && signal == 0;
            bool timeDifferenceIsConstZero = timeDifferenceIsConst && signal == 0;

            dimensionStack.Pop();

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
                calculator = new Earlier_OperatorCalculator_VarSignal_ConstDistance(signalCalculator, timeDifference, dimensionStack);
            }
            else
            {
                calculator = new Earlier_OperatorCalculator_VarSignal_VarDistance(signalCalculator, timeDifferenceCalculator, dimensionStack);
            }

            _stack.Push(calculator);
        }

        protected override void VisitEqual(Operator op)
        {
            base.VisitEqual(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase calculatorA = _stack.Pop();
            OperatorCalculatorBase calculatorB = _stack.Pop();

            calculatorA = calculatorA ?? new Zero_OperatorCalculator();
            calculatorB = calculatorB ?? new Zero_OperatorCalculator();

            double a = calculatorA.Calculate();
            double b = calculatorB.Calculate();

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
            base.VisitExponent(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase lowCalculator = _stack.Pop();
            OperatorCalculatorBase highCalculator = _stack.Pop();
            OperatorCalculatorBase ratioCalculator = _stack.Pop();

            lowCalculator = lowCalculator ?? new Zero_OperatorCalculator();
            highCalculator = highCalculator ?? new Zero_OperatorCalculator();
            ratioCalculator = ratioCalculator ?? new Zero_OperatorCalculator();

            double low = lowCalculator.Calculate();
            double high = highCalculator.Calculate();
            double ratio = ratioCalculator.Calculate();
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
                // Special number.
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
            base.VisitFilter(op);

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

            double signal = signalCalculator.Calculate();
            double frequency = frequencyCalculator.Calculate();
            double bandWidth = bandWidthCalculator.Calculate();
            double dbGain = dbGainCalculator.Calculate();
            double shelfSlope = shelfSlopeCalculator.Calculate();

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
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);

            base.VisitGetDimension(op);

            var calculator = new GetDimension_OperatorCalculator(dimensionStack);
            _stack.Push(calculator);
        }

        protected override void VisitGreaterThan(Operator op)
        {
            base.VisitGreaterThan(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase calculatorA = _stack.Pop();
            OperatorCalculatorBase calculatorB = _stack.Pop();

            calculatorA = calculatorA ?? new Zero_OperatorCalculator();
            calculatorB = calculatorB ?? new Zero_OperatorCalculator();

            double a = calculatorA.Calculate();
            double b = calculatorB.Calculate();

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
            base.VisitGreaterThanOrEqual(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase calculatorA = _stack.Pop();
            OperatorCalculatorBase calculatorB = _stack.Pop();

            calculatorA = calculatorA ?? new Zero_OperatorCalculator();
            calculatorB = calculatorB ?? new Zero_OperatorCalculator();

            double a = calculatorA.Calculate();
            double b = calculatorB.Calculate();

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
            base.VisitHighPassFilter(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase minFrequencyCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            minFrequencyCalculator = minFrequencyCalculator ?? new Zero_OperatorCalculator();

            double signal = signalCalculator.Calculate();
            double minFrequency = minFrequencyCalculator.Calculate();

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

        protected override void VisitHold(Operator op)
        {
            base.VisitHold(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            bool signalIsConst = signalCalculator is Number_OperatorCalculator;

            if (signalIsConst)
            {
                calculator = signalCalculator;
            }
            else
            {
                calculator = new Hold_OperatorCalculator(signalCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitIf(Operator op)
        {
            base.VisitIf(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase conditionCalculator = _stack.Pop();
            OperatorCalculatorBase thenCalculator = _stack.Pop();
            OperatorCalculatorBase elseCalculator = _stack.Pop();

            conditionCalculator = conditionCalculator ?? new Zero_OperatorCalculator();
            thenCalculator = thenCalculator ?? new Zero_OperatorCalculator();
            elseCalculator = elseCalculator ?? new Zero_OperatorCalculator();

            double condition = conditionCalculator.Calculate();
            double then = thenCalculator.Calculate();
            double @else = elseCalculator.Calculate();

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
            base.VisitLessThan(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase calculatorA = _stack.Pop();
            OperatorCalculatorBase calculatorB = _stack.Pop();

            calculatorA = calculatorA ?? new Zero_OperatorCalculator();
            calculatorB = calculatorB ?? new Zero_OperatorCalculator();

            double a = calculatorA.Calculate();
            double b = calculatorB.Calculate();

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
            base.VisitLessThanOrEqual(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase calculatorA = _stack.Pop();
            OperatorCalculatorBase calculatorB = _stack.Pop();

            calculatorA = calculatorA ?? new Zero_OperatorCalculator();
            calculatorB = calculatorB ?? new Zero_OperatorCalculator();

            double a = calculatorA.Calculate();
            double b = calculatorB.Calculate();

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
            var wrapper = new Loop_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);
            dimensionStack.Push(DEFAULT_DIMENSION_VALUE);

            base.VisitLoop(op);

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

            double signal = signalCalculator.Calculate();
            double skip = skipCalculator.Calculate();
            double loopStartMarker = loopStartMarkerCalculator.Calculate();
            double loopEndMarker = loopEndMarkerCalculator.Calculate();
            double releaseEndMarker = releaseEndMarkerCalculator.Calculate();
            double noteDuration = noteDurationCalculator.Calculate();

            bool signalConstZero = signalIsConst && signal == 0.0;
            bool skipIsConstZero = skipIsConst && skip == 0.0;
            bool loopStartMarkerIsConstZero = loopStartMarkerIsConst && loopStartMarker == 0.0;
            bool loopEndMarkerIsConstZero = loopEndMarkerIsConst && loopEndMarker == 0.0;
            bool noteDurationIsConstVeryHighValue = noteDurationIsConst && noteDuration == CalculationHelper.VERY_HIGH_VALUE;
            bool releaseEndMarkerIsConstVeryHighValue = releaseEndMarkerIsConst && releaseEndMarker == CalculationHelper.VERY_HIGH_VALUE;

            dimensionStack.Pop();

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
                            signalCalculator, loopStartMarker, loopEndMarker, dimensionStack);
                    }
                    else
                    {
                        calculator = new Loop_OperatorCalculator_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration(
                            signalCalculator, loopStartMarker, loopEndMarkerCalculator, dimensionStack);
                    }
                }
                else if (skipIsConstZero && releaseEndMarkerIsConstVeryHighValue)
                {
                    if (loopStartMarkerIsConst && loopEndMarkerIsConst)
                    {
                        calculator = new Loop_OperatorCalculator_NoSkipOrRelease_ManyConstants(
                            signalCalculator, loopStartMarker, loopEndMarker, noteDurationCalculator, dimensionStack);
                    }
                    else
                    {
                        calculator = new Loop_OperatorCalculator_NoSkipOrRelease(
                            signalCalculator, loopStartMarkerCalculator, loopEndMarkerCalculator, noteDurationCalculator, dimensionStack);
                    }
                }
                else
                {
                    if (skipIsConst && loopStartMarkerIsConst && loopEndMarkerIsConst && releaseEndMarkerIsConst)
                    {
                        calculator = new Loop_OperatorCalculator_ManyConstants(
                            signalCalculator, skip, loopStartMarker, loopEndMarker, releaseEndMarker, noteDurationCalculator, dimensionStack);
                    }
                    else
                    {
                        calculator = new Loop_OperatorCalculator_ManyVariables(
                            signalCalculator,
                            skipCalculator,
                            loopStartMarkerCalculator,
                            loopEndMarkerCalculator,
                            releaseEndMarkerCalculator,
                            noteDurationCalculator,
                            dimensionStack);
                    }
                }
            }

            _stack.Push(calculator);
        }

        protected override void VisitLowPassFilter(Operator op)
        {
            base.VisitLowPassFilter(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase maxFrequencyCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            maxFrequencyCalculator = maxFrequencyCalculator ?? new Zero_OperatorCalculator();

            double signal = signalCalculator.Calculate();
            double maxFrequency = maxFrequencyCalculator.Calculate();

            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool maxFrequencyIsConst = maxFrequencyCalculator is Number_OperatorCalculator;

            bool maxFrequencyIsConstZero = maxFrequencyIsConst && maxFrequency == 0.0;

            if (signalIsConst)
            {
                calculator = signalCalculator;
            }
            else if (maxFrequencyIsConstZero)
            {
                // Special number: time stands still.
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
            var wrapper = new Maximum_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);
            dimensionStack.Push(DEFAULT_DIMENSION_VALUE);

            base.VisitMaximum(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeSliceDurationCalculator = _stack.Pop();
            OperatorCalculatorBase sampleCountCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            double signal = signalCalculator.Calculate();
            bool signalIsConst = signalCalculator is Number_OperatorCalculator;

            // TODO: Lower priority: Do not use these magic defaults, but give standard operators default inlet value functionality.
            timeSliceDurationCalculator = timeSliceDurationCalculator ?? new Number_OperatorCalculator(0.02f);
            sampleCountCalculator = sampleCountCalculator ?? new Number_OperatorCalculator(100f);

            dimensionStack.Pop();

            if (signalIsConst)
            {
                calculator = signalCalculator;
            }
            else
            {
                calculator = new Maximum_OperatorCalculator(signalCalculator, timeSliceDurationCalculator, sampleCountCalculator, dimensionStack);
            }

            _stack.Push(calculator);
        }

        protected override void VisitMinimum(Operator op)
        {
            var wrapper = new Minimum_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);
            dimensionStack.Push(DEFAULT_DIMENSION_VALUE);

            base.VisitMinimum(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeSliceDurationCalculator = _stack.Pop();
            OperatorCalculatorBase sampleCountCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            double signal = signalCalculator.Calculate();
            bool signalIsConst = signalCalculator is Number_OperatorCalculator;

            // TODO: Lower priority: Do not use these magic defaults, but give standard operators default inlet value functionality.
            timeSliceDurationCalculator = timeSliceDurationCalculator ?? new Number_OperatorCalculator(0.02f);
            sampleCountCalculator = sampleCountCalculator ?? new Number_OperatorCalculator(100f);

            dimensionStack.Pop();

            if (signalIsConst)
            {
                calculator = signalCalculator;
            }
            else
            {
                calculator = new Minimum_OperatorCalculator(signalCalculator, timeSliceDurationCalculator, sampleCountCalculator, dimensionStack);
            }

            _stack.Push(calculator);
        }

        protected override void VisitMultiply(Operator op)
        {
            base.VisitMultiply(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase aCalculator = _stack.Pop();
            OperatorCalculatorBase bCalculator = _stack.Pop();
            OperatorCalculatorBase originCalculator = _stack.Pop();

            aCalculator = aCalculator ?? new One_OperatorCalculator();
            bCalculator = bCalculator ?? new One_OperatorCalculator();
            originCalculator = originCalculator ?? new Zero_OperatorCalculator();

            double a = aCalculator.Calculate();
            double b = bCalculator.Calculate();
            double origin = originCalculator.Calculate();
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
                calculator = new Multiply_OperatorCalculator_ConstA_VarB_NoOrigin(a, bCalculator);
            }
            else if (!aIsConst && bIsConst && originIsConstZero)
            {
                var castedACalculator1 = aCalculator as Multiply_OperatorCalculator_VarA_VarB_NoOrigin;
                var castedACalculator2 = aCalculator as Multiply_OperatorCalculator_VarA_ConstB_NoOrigin;
                if (castedACalculator1 != null)
                {
                    calculator = new Multiply_OperatorCalculator_MulWithVarB_ConstB_NoOrigin(castedACalculator1, b);
                }
                else if (castedACalculator2 != null)
                {
                    calculator = new Multiply_OperatorCalculator_MulWithConstB_ConstB_NoOrigin(castedACalculator2, b);
                }
                else
                {
                    calculator = new Multiply_OperatorCalculator_VarA_ConstB_NoOrigin(aCalculator, b);
                }
            }
            else if (!aIsConst && !bIsConst && originIsConstZero)
            {
                var castedACalculator1 = aCalculator as Multiply_OperatorCalculator_VarA_VarB_NoOrigin;
                var castedACalculator2 = aCalculator as Multiply_OperatorCalculator_VarA_ConstB_NoOrigin;
                if (castedACalculator1 != null)
                {
                    calculator = new Multiply_OperatorCalculator_MulWithVarB_VarB_NoOrigin(castedACalculator1, bCalculator);
                }
                else if (castedACalculator2 != null)
                {
                    calculator = new Multiply_OperatorCalculator_MulWithConstB_VarB_NoOrigin(castedACalculator2, bCalculator);
                }
                else
                {
                    calculator = new Multiply_OperatorCalculator_VarA_VarB_NoOrigin(aCalculator, bCalculator);
                }
            }
            else if (aIsConst && !bIsConst && originIsConst)
            {
                calculator = new Multiply_OperatorCalculator_ConstA_VarB_ConstOrigin(a, bCalculator, origin);
            }
            else if (!aIsConst && bIsConst && originIsConst)
            {
                calculator = new Multiply_OperatorCalculator_VarA_ConstB_ConstOrigin(aCalculator, b, origin);
            }
            else if (!aIsConst && !bIsConst && originIsConst)
            {
                calculator = new Multiply_OperatorCalculator_VarA_VarB_ConstOrigin(aCalculator, bCalculator, origin);
            }
            else if (aIsConst && bIsConst && !originIsConst)
            {
                calculator = new Multiply_OperatorCalculator_ConstA_ConstB_VarOrigin(a, b, originCalculator);
            }
            else if (aIsConst && !bIsConst && !originIsConst)
            {
                calculator = new Multiply_OperatorCalculator_ConstA_VarB_VarOrigin(a, bCalculator, originCalculator);
            }
            else if (!aIsConst && bIsConst && !originIsConst)
            {
                calculator = new Multiply_OperatorCalculator_VarA_ConstB_VarOrigin(aCalculator, b, originCalculator);
            }
            else
            {
                calculator = new Multiply_OperatorCalculator_VarA_VarB_VarOrigin(aCalculator, bCalculator, originCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitNarrower(Operator op)
        {
            var wrapper = new Narrower_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);
            dimensionStack.Push(DEFAULT_DIMENSION_VALUE);

            base.VisitNarrower(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase factorCalculator = _stack.Pop();
            OperatorCalculatorBase originCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            factorCalculator = factorCalculator ?? new One_OperatorCalculator();
            originCalculator = originCalculator ?? new Zero_OperatorCalculator();

            double signal = signalCalculator.Calculate();
            double factor = factorCalculator.Calculate();
            double origin = originCalculator.Calculate();

            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool factorIsConst = factorCalculator is Number_OperatorCalculator;
            bool originIsConst = originCalculator is Number_OperatorCalculator;

            bool signalIsConstZero = signalIsConst && signal == 0;
            bool factorIsConstZero = factorIsConst && factor == 0;
            bool originIsConstZero = originIsConst && origin == 0;

            bool factorIsConstOne = factorIsConst && factor == 1;

            bool signalIsConstSpecialNumber = signalIsConst && DoubleHelper.IsSpecialNumber(signal);
            bool factorIsConstSpecialNumber = factorIsConst && DoubleHelper.IsSpecialNumber(factor);
            bool originIsConstSpecialNumber = originIsConst && DoubleHelper.IsSpecialNumber(origin);

            dimensionStack.Pop();

            if (signalIsConstSpecialNumber || factorIsConstSpecialNumber || originIsConstSpecialNumber)
            {
                // Wierd number
                calculator = new Number_OperatorCalculator(Double.NaN);
            }
            else if (factorIsConstZero)
            {
                // Special number
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
                calculator = new Narrower_OperatorCalculator_VarSignal_ConstFactor_ZeroOrigin(signalCalculator, factor, dimensionStack);
            }
            else if (!signalIsConst && !factorIsConst && originIsConstZero)
            {
                calculator = new Narrower_OperatorCalculator_VarSignal_VarFactor_ZeroOrigin(signalCalculator, factorCalculator, dimensionStack);
            }
            else if (!signalIsConst && factorIsConst && originIsConst)
            {
                calculator = new Narrower_OperatorCalculator_VarSignal_ConstFactor_ConstOrigin(signalCalculator, factor, origin, dimensionStack);
            }
            else if (!signalIsConst && factorIsConst && !originIsConst)
            {
                calculator = new Narrower_OperatorCalculator_VarSignal_ConstFactor_VarOrigin(signalCalculator, factor, originCalculator, dimensionStack);
            }
            else if (!signalIsConst && !factorIsConst && originIsConst)
            {
                calculator = new Narrower_OperatorCalculator_VarSignal_VarFactor_ConstOrigin(signalCalculator, factorCalculator, origin, dimensionStack);
            }
            else if (!signalIsConst && !factorIsConst && !originIsConst)
            {
                calculator = new Narrower_OperatorCalculator_VarSignal_VarFactor_VarOrigin(signalCalculator, factorCalculator, originCalculator, dimensionStack);
            }
            else
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
            }

            _stack.Push(calculator);
        }

        protected override void VisitNegative(Operator op)
        {
            base.VisitNegative(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase xCalculator = _stack.Pop();

            xCalculator = xCalculator ?? new Zero_OperatorCalculator();
            double x = xCalculator.Calculate();
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
            var wrapper = new Noise_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);

            base.VisitNoise(op);

            double offset;
            if (!_operator_NoiseOffsetInSeconds_Dictionary.TryGetValue(op, out offset))
            {
                offset = _calculatorCache.NoiseCalculator.GetRandomOffset();
                _operator_NoiseOffsetInSeconds_Dictionary.Add(op, offset);
            }

            var calculator = new Noise_OperatorCalculator(_calculatorCache.NoiseCalculator, offset, dimensionStack);
            _stack.Push(calculator);
        }

        protected override void VisitNot(Operator op)
        {
            base.VisitNot(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase xCalculator = _stack.Pop();

            xCalculator = xCalculator ?? new Zero_OperatorCalculator();

            double x = xCalculator.Calculate();

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
            base.VisitNotEqual(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase calculatorA = _stack.Pop();
            OperatorCalculatorBase calculatorB = _stack.Pop();

            calculatorA = calculatorA ?? new Zero_OperatorCalculator();
            calculatorB = calculatorB ?? new Zero_OperatorCalculator();

            double a = calculatorA.Calculate();
            double b = calculatorB.Calculate();

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
            base.VisitNumber(op);

            var wrapper = new Number_OperatorWrapper(op);
            double number = wrapper.Number;

            var calculator = new Number_OperatorCalculator(number);
            _stack.Push(calculator);
        }

        protected override void VisitOneOverX(Operator op)
        {
            base.VisitOneOverX(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase xCalculator = _stack.Pop();

            xCalculator = xCalculator ?? new One_OperatorCalculator();
            double x = xCalculator.Calculate();
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
            base.VisitOr(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase calculatorA = _stack.Pop();
            OperatorCalculatorBase calculatorB = _stack.Pop();

            calculatorA = calculatorA ?? new Zero_OperatorCalculator();
            calculatorB = calculatorB ?? new Zero_OperatorCalculator();

            double a = calculatorA.Calculate();
            double b = calculatorB.Calculate();

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
            base.VisitPower(op);

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
            double @base = baseCalculator.Calculate();
            double exponent = exponentCalculator.Calculate();
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
            var wrapper = new Pulse_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);

            base.VisitPulse(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase frequencyCalculator = _stack.Pop();
            OperatorCalculatorBase widthCalculator = _stack.Pop();
            OperatorCalculatorBase phaseShiftCalculator = _stack.Pop();

            frequencyCalculator = frequencyCalculator ?? new Zero_OperatorCalculator();
            widthCalculator = widthCalculator ?? new Number_OperatorCalculator(0.5);
            phaseShiftCalculator = phaseShiftCalculator ?? new Zero_OperatorCalculator();

            double frequency = frequencyCalculator.Calculate();
            double width = widthCalculator.Calculate() % 1.0;
            double phaseShift = phaseShiftCalculator.Calculate() % 1.0;

            bool frequencyIsConst = frequencyCalculator is Number_OperatorCalculator;
            bool widthIsConst = widthCalculator is Number_OperatorCalculator;
            bool phaseShiftIsConst = phaseShiftCalculator is Number_OperatorCalculator;

            bool frequencyIsConstZero = frequencyIsConst && frequency == 0.0;
            bool widthIsConstZero = widthIsConst && width == 0.0;

            bool frequencyIsConstSpecialNumber = frequencyIsConst && DoubleHelper.IsSpecialNumber(frequency);
            bool widthIsConstSpecialNumber = widthIsConst && DoubleHelper.IsSpecialNumber(width);
            bool phaseShiftIsConstSpecialNumber = phaseShiftIsConst && DoubleHelper.IsSpecialNumber(phaseShift);

            if (frequencyIsConstSpecialNumber || widthIsConstSpecialNumber || phaseShiftIsConstSpecialNumber)
            {
                // Special number
                calculator = new Number_OperatorCalculator(Double.NaN);
            }
            else if (frequencyIsConstZero)
            {
                // Special number
                // (Frequency 0 means time stands still. In theory this could produce a different value than 0.
                //  but I feel I would have to make disproportionate effort to take phase shift and width into account.)
                calculator = new Zero_OperatorCalculator();
            }
            else if (widthIsConstZero)
            {
                calculator = new Zero_OperatorCalculator();
            }
            else if (frequencyIsConst && widthIsConst && phaseShiftIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new Pulse_OperatorCalculator_ConstFrequency_ConstWidth_ConstPhaseShift_WithOriginShifting(frequency, width, phaseShift, dimensionStack);
            }
            else if (frequencyIsConst && widthIsConst && phaseShiftIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new Pulse_OperatorCalculator_ConstFrequency_ConstWidth_ConstPhaseShift_NoOriginShifting(frequency, width, phaseShift, dimensionStack);
            }
            else if (frequencyIsConst && widthIsConst && !phaseShiftIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new Pulse_OperatorCalculator_ConstFrequency_ConstWidth_VarPhaseShift_WithOriginShifting(frequency, width, phaseShiftCalculator, dimensionStack);
            }
            else if (frequencyIsConst && widthIsConst && !phaseShiftIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new Pulse_OperatorCalculator_ConstFrequency_ConstWidth_VarPhaseShift_NoOriginShifting(frequency, width, phaseShiftCalculator, dimensionStack);
            }
            else if (frequencyIsConst && !widthIsConst && phaseShiftIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new Pulse_OperatorCalculator_ConstFrequency_VarWidth_ConstPhaseShift_WithOriginShifting(frequency, widthCalculator, phaseShift, dimensionStack);
            }
            else if (frequencyIsConst && !widthIsConst && phaseShiftIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new Pulse_OperatorCalculator_ConstFrequency_VarWidth_ConstPhaseShift_NoOriginShifting(frequency, widthCalculator, phaseShift, dimensionStack);
            }
            else if (frequencyIsConst && !widthIsConst && !phaseShiftIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new Pulse_OperatorCalculator_ConstFrequency_VarWidth_VarPhaseShift_WithOriginShifting(frequency, widthCalculator, phaseShiftCalculator, dimensionStack);
            }
            else if (frequencyIsConst && !widthIsConst && !phaseShiftIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new Pulse_OperatorCalculator_ConstFrequency_VarWidth_VarPhaseShift_NoOriginShifting(frequency, widthCalculator, phaseShiftCalculator, dimensionStack);
            }
            else if (!frequencyIsConst && widthIsConst && phaseShiftIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new Pulse_OperatorCalculator_VarFrequency_ConstWidth_ConstPhaseShift_WithPhaseTracking(frequencyCalculator, width, phaseShift, dimensionStack);
            }
            else if (!frequencyIsConst && widthIsConst && phaseShiftIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new Pulse_OperatorCalculator_VarFrequency_ConstWidth_ConstPhaseShift_NoPhaseTracking(frequencyCalculator, width, phaseShift, dimensionStack);
            }
            else if (!frequencyIsConst && widthIsConst && !phaseShiftIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new Pulse_OperatorCalculator_VarFrequency_ConstWidth_VarPhaseShift_WithPhaseTracking(frequencyCalculator, width, phaseShiftCalculator, dimensionStack);
            }
            else if (!frequencyIsConst && widthIsConst && !phaseShiftIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new Pulse_OperatorCalculator_VarFrequency_ConstWidth_VarPhaseShift_NoPhaseTracking(frequencyCalculator, width, phaseShiftCalculator, dimensionStack);
            }
            else if (!frequencyIsConst && !widthIsConst && phaseShiftIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new Pulse_OperatorCalculator_VarFrequency_VarWidth_ConstPhaseShift_WithPhaseTracking(frequencyCalculator, widthCalculator, phaseShift, dimensionStack);
            }
            else if (!frequencyIsConst && !widthIsConst && phaseShiftIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new Pulse_OperatorCalculator_VarFrequency_VarWidth_ConstPhaseShift_NoPhaseTracking(frequencyCalculator, widthCalculator, phaseShift, dimensionStack);
            }
            else if (!frequencyIsConst && !widthIsConst && !phaseShiftIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new Pulse_OperatorCalculator_VarFrequency_VarWidth_VarPhaseShift_WithPhaseTracking(frequencyCalculator, widthCalculator, phaseShiftCalculator, dimensionStack);
            }
            else if (!frequencyIsConst && !widthIsConst && !phaseShiftIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new Pulse_OperatorCalculator_VarFrequency_VarWidth_VarPhaseShift_NoPhaseTracking(frequencyCalculator, widthCalculator, phaseShiftCalculator, dimensionStack);
            }
            else
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
            }

            _stack.Push(calculator);
        }

        protected override void VisitPulseTrigger(Operator op)
        {
            base.VisitPulseTrigger(op);

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
            var wrapper = new Random_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);

            base.VisitRandom(op);

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

            double rate = rateCalculator.Calculate();
            double phaseShift = phaseShiftCalculator.Calculate();

            bool rateIsConst = rateCalculator is Number_OperatorCalculator;
            bool phaseShiftIsConst = phaseShiftCalculator is Number_OperatorCalculator;

            bool rateIsConstZero = rateIsConst && rate == 0;
            bool phaseShiftIsConstZero = phaseShiftIsConst && phaseShift == 0;

            bool rateIsConstSpecialNumber = rateIsConst && DoubleHelper.IsSpecialNumber(rate);
            bool phaseShiftIsConstSpecialNumber = phaseShiftIsConst && DoubleHelper.IsSpecialNumber(phaseShift);

            if (rateIsConstSpecialNumber || phaseShiftIsConstSpecialNumber)
            {
                // Special number
                calculator = new Number_OperatorCalculator(Double.NaN);
            }
            else if (rateIsConstZero)
            {
                // Special number
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
                                dimensionStack);

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
                                dimensionStack);

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
                                dimensionStack);

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
                                dimensionStack);

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
                                dimensionStack);

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
                                dimensionStack);

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
                                dimensionStack);

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
                                dimensionStack);

                            break;
                        }

                    default:
                        throw new ValueNotSupportedException(resampleInterpolationTypeEnum);
                }
            }

            _stack.Push(calculator);
        }

        protected override void VisitRange(Operator op)
        {
            var wrapper = new Random_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);

            base.VisitRange(op);

            OperatorCalculatorBase operatorCalculator;

            OperatorCalculatorBase fromCalculator = _stack.Pop();
            OperatorCalculatorBase tillCalculator = _stack.Pop();
            OperatorCalculatorBase stepCalculator = _stack.Pop();

            // TODO: Lower priority: Do not use these magic defaults, but give standard operators default inlet value functionality.
            fromCalculator = fromCalculator ?? new Number_OperatorCalculator(1.0);
            tillCalculator = tillCalculator ?? new Number_OperatorCalculator(16.0);
            stepCalculator = stepCalculator ?? new Number_OperatorCalculator(1.0);

            bool fromIsConst = fromCalculator is Number_OperatorCalculator;
            bool tillIsConst = tillCalculator is Number_OperatorCalculator;
            bool stepIsConst = stepCalculator is Number_OperatorCalculator;

            double from = fromIsConst ? fromCalculator.Calculate() : 0.0;
            double till = tillIsConst ? tillCalculator.Calculate() : 0.0;
            double step = stepIsConst ? stepCalculator.Calculate() : 0.0;

            bool stepIsConstZero = stepIsConst && step == 0.0;
            bool stepIsConstOne = stepIsConst && step == 1.0;
            bool fromIsConstSpecialNumber = fromIsConst && DoubleHelper.IsSpecialNumber(from);
            bool tillIsConstSpecialNumber = fromIsConst && DoubleHelper.IsSpecialNumber(from);
            bool stepIsConstSpecialNumber = fromIsConst && DoubleHelper.IsSpecialNumber(from);

            if (stepIsConstZero)
            {
                // Would eventually lead to divide by zero and an infinite amount of index positions.
                operatorCalculator = new Number_OperatorCalculator(Double.NaN);
            }
            if (fromIsConstSpecialNumber || tillIsConstSpecialNumber || stepIsConstSpecialNumber)
            {
                operatorCalculator = new Number_OperatorCalculator(Double.NaN);
            }
            else if (fromIsConst && tillIsConst && stepIsConstOne)
            {
                operatorCalculator = new Range_OperatorCalculator_WithConstants_AndStepOne(from, till, dimensionStack);
            }
            else if (fromIsConst && tillIsConst && stepIsConst)
            {
                operatorCalculator = new Range_OperatorCalculator_WithConstants(from, till, step, dimensionStack);
            }
            else if (!fromIsConst && !tillIsConst && !stepIsConst)
            {
                operatorCalculator = new Range_OperatorCalculator_WithVariables(fromCalculator, tillCalculator, stepCalculator, dimensionStack);
            }
            else
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
            }

            _stack.Push(operatorCalculator);
        }

        protected override void VisitResample(Operator op)
        {
            var wrapper = new Resample_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);
            dimensionStack.Push(DEFAULT_DIMENSION_VALUE);

            base.VisitResample(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase samplingRateCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            samplingRateCalculator = samplingRateCalculator ?? new Zero_OperatorCalculator();

            double signal = signalCalculator.Calculate();
            double samplingRate = samplingRateCalculator.Calculate();

            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool samplingRateIsConst = samplingRateCalculator is Number_OperatorCalculator;

            bool signalIsConstZero = signalIsConst && signal == 0;
            bool samplingRateIsConstZero = samplingRateIsConst && samplingRate == 0;

            bool signalIsConstSpecialNumber = signalIsConst && DoubleHelper.IsSpecialNumber(signal);
            bool samplingRateIsConstSpecialNumber = samplingRateIsConst && DoubleHelper.IsSpecialNumber(samplingRate);

            dimensionStack.Pop();

            if (signalIsConst)
            {
                // Const signal Preceeds weird numbers.
                calculator = new Number_OperatorCalculator(signal);
            }
            else if (samplingRateIsConstZero)
            {
                // Special number: Time stands still.
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
                        calculator = new Resample_OperatorCalculator_Block(signalCalculator, samplingRateCalculator, dimensionStack);
                        break;

                    case ResampleInterpolationTypeEnum.Stripe:
                        calculator = new Resample_OperatorCalculator_Stripe(signalCalculator, samplingRateCalculator, dimensionStack);
                        break;

                    case ResampleInterpolationTypeEnum.LineRememberT0:
                        calculator = new Resample_OperatorCalculator_LineRememberT0(signalCalculator, samplingRateCalculator, dimensionStack);
                        break;

                    case ResampleInterpolationTypeEnum.LineRememberT1:
                        calculator = new Resample_OperatorCalculator_LineRememberT1(signalCalculator, samplingRateCalculator, dimensionStack);
                        break;

                    case ResampleInterpolationTypeEnum.CubicEquidistant:
                        calculator = new Resample_OperatorCalculator_CubicEquidistant(signalCalculator, samplingRateCalculator, dimensionStack);
                        break;

                    case ResampleInterpolationTypeEnum.CubicAbruptSlope:
                        calculator = new Resample_OperatorCalculator_CubicAbruptSlope(signalCalculator, samplingRateCalculator, dimensionStack);
                        break;

                    case ResampleInterpolationTypeEnum.CubicSmoothSlope:
                        calculator = new Resample_OperatorCalculator_CubicSmoothSlope(signalCalculator, samplingRateCalculator, dimensionStack);
                        break;

                    case ResampleInterpolationTypeEnum.Hermite:
                        calculator = new Resample_OperatorCalculator_Hermite(signalCalculator, samplingRateCalculator, dimensionStack);
                        break;

                    default:
                        throw new ValueNotSupportedException(resampleInterpolationTypeEnum);
                }
            }

            _stack.Push(calculator);
        }

        protected override void VisitReverse(Operator op)
        {
            var wrapper = new Reverse_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);
            dimensionStack.Push(DEFAULT_DIMENSION_VALUE);

            base.VisitReverse(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase speedCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            speedCalculator = speedCalculator ?? new One_OperatorCalculator();

            double signal = signalCalculator.Calculate();
            double speed = speedCalculator.Calculate();

            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool speedIsConst = speedCalculator is Number_OperatorCalculator;

            bool speedIsConstZero = speedIsConst && speed == 0;

            bool signalIsConstSpecialNumber = signalIsConst && DoubleHelper.IsSpecialNumber(signal);
            bool speedIsConstSpecialNumber = speedIsConst && DoubleHelper.IsSpecialNumber(speed);

            dimensionStack.Pop();

            if (speedIsConstSpecialNumber)
            {
                // Special number
                calculator = new Number_OperatorCalculator(Double.NaN);
            }
            else if (signalIsConstSpecialNumber)
            {
                // Special number
                calculator = new Number_OperatorCalculator(signal);
            }
            else if (speedIsConstZero)
            {
                // Special number
                // Speed-up of 0 means time stands still.
                calculator = new Number_OperatorCalculator(signal);
            }
            else if (signalIsConst)
            {
                calculator = signalCalculator;
            }
            else if (speedIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new Reverse_OperatorCalculator_ConstSpeed_WithOriginShifting(signalCalculator, speed, dimensionStack);
            }
            else if (speedIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new Reverse_OperatorCalculator_ConstSpeed_NoOriginShifting(signalCalculator, speed, dimensionStack);
            }
            else if (!speedIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new Reverse_OperatorCalculator_VarSpeed_WithPhaseTracking(signalCalculator, speedCalculator, dimensionStack);
            }
            else if (!speedIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new Reverse_OperatorCalculator_VarSpeed_NoPhaseTracking(signalCalculator, speedCalculator, dimensionStack);
            }
            else
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
            }

            _stack.Push(calculator);
        }

        protected override void VisitRound(Operator op)
        {
            base.VisitRound(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase stepCalculator = _stack.Pop();
            OperatorCalculatorBase offsetCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            stepCalculator = stepCalculator ?? new One_OperatorCalculator();
            offsetCalculator = offsetCalculator ?? new Zero_OperatorCalculator();

            double signal = signalCalculator.Calculate();
            double step = stepCalculator.Calculate();
            double offset = offsetCalculator.Calculate();

            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool stepIsConst = stepCalculator is Number_OperatorCalculator;
            bool offsetIsConst = offsetCalculator is Number_OperatorCalculator;

            bool signalIsConstZero = signalIsConst && signal == 0;
            bool stepIsConstZero = stepIsConst && step == 0;
            bool offsetIsConstZero = offsetIsConst && offset % 1.0 == 0;

            bool stepIsConstOne = stepIsConst && step == 1;

            bool signalIsConstSpecialNumber = signalIsConst && DoubleHelper.IsSpecialNumber(signal);
            bool stepIsConstSpecialNumber = stepIsConst && DoubleHelper.IsSpecialNumber(step);
            bool offsetIsConstSpecialNumber = offsetIsConst && DoubleHelper.IsSpecialNumber(offset);

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
            var wrapper = new Sample_OperatorWrapper(op, _sampleRepository);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);
            DimensionStack channelDimensionStack = _dimensionStackCollection.GetDimensionStack(DimensionEnum.Channel);

            base.VisitSampleOperator(op);

            OperatorCalculatorBase calculator = null;

            OperatorCalculatorBase frequencyCalculator = _stack.Pop();

            frequencyCalculator = frequencyCalculator ?? new Zero_OperatorCalculator();
            double frequency = frequencyCalculator.Calculate();
            bool frequencyIsConst = frequencyCalculator is Number_OperatorCalculator;
            bool frequencyIsConstZero = frequencyIsConst && frequency == 0.0;

            SampleInfo sampleInfo = wrapper.SampleInfo;
            if (sampleInfo.Sample == null)
            {
                calculator = new Zero_OperatorCalculator();
            }
            else if (frequencyIsConstZero)
            {
                // Special number
                calculator = new Zero_OperatorCalculator();
            }
            else
            {
                ISampleCalculator sampleCalculator = _calculatorCache.GetSampleCalculator(sampleInfo);

                int sampleChannelCount = sampleInfo.Sample.GetChannelCount();

                if (sampleChannelCount == _channelCount)
                {
                    if (frequencyIsConst && dimensionEnum == DimensionEnum.Time)
                    {
                        calculator = new Sample_OperatorCalculator_ConstFrequency_WithOriginShifting(frequency, sampleCalculator, dimensionStack, channelDimensionStack);
                    }
                    else if (frequencyIsConst && dimensionEnum != DimensionEnum.Time)
                    {
                        calculator = new Sample_OperatorCalculator_ConstFrequency_NoOriginShifting(frequency, sampleCalculator, dimensionStack, channelDimensionStack);
                    }
                    else if (!frequencyIsConst && dimensionEnum == DimensionEnum.Time)
                    {
                        calculator = new Sample_OperatorCalculator_VarFrequency_WithPhaseTracking(frequencyCalculator, sampleCalculator, dimensionStack, channelDimensionStack);
                    }
                    else if (!frequencyIsConst && dimensionEnum != DimensionEnum.Time)
                    {
                        calculator = new Sample_OperatorCalculator_VarFrequency_NoPhaseTracking(frequencyCalculator, sampleCalculator, dimensionStack, channelDimensionStack);
                    }
                }
                else if (sampleChannelCount == 1 && _channelCount == 2)
                {
                    if (frequencyIsConst && dimensionEnum == DimensionEnum.Time)
                    {
                        calculator = new Sample_OperatorCalculator_ConstFrequency_MonoToStereo_WithOriginShifting(frequency, sampleCalculator, dimensionStack);
                    }
                    else if (frequencyIsConst && dimensionEnum != DimensionEnum.Time)
                    {
                        calculator = new Sample_OperatorCalculator_ConstFrequency_MonoToStereo_NoOriginShifting(frequency, sampleCalculator, dimensionStack);
                    }
                    else if (!frequencyIsConst && dimensionEnum == DimensionEnum.Time)
                    {
                        calculator = new Sample_OperatorCalculator_VarFrequency_MonoToStereo_WithPhaseTracking(frequencyCalculator, sampleCalculator, dimensionStack);
                    }
                    else if (!frequencyIsConst && dimensionEnum != DimensionEnum.Time)
                    {
                        calculator = new Sample_OperatorCalculator_VarFrequency_MonoToStereo_NoPhaseTracking(frequencyCalculator, sampleCalculator, dimensionStack);
                    }
                }
                else if (sampleChannelCount == 2 && _channelCount == 1)
                {
                    if (frequencyIsConst && dimensionEnum == DimensionEnum.Time)
                    {
                        calculator = new Sample_OperatorCalculator_ConstFrequency_StereoToMono_WithOriginShifting(frequency, sampleCalculator, dimensionStack);
                    }
                    else if (frequencyIsConst && dimensionEnum != DimensionEnum.Time)
                    {
                        calculator = new Sample_OperatorCalculator_ConstFrequency_StereoToMono_NoOriginShifting(frequency, sampleCalculator, dimensionStack);
                    }
                    else if (!frequencyIsConst && dimensionEnum == DimensionEnum.Time)
                    {
                        calculator = new Sample_OperatorCalculator_VarFrequency_StereoToMono_WithPhaseTracking(frequencyCalculator, sampleCalculator, dimensionStack);
                    }
                    else if (!frequencyIsConst && dimensionEnum != DimensionEnum.Time)
                    {
                        calculator = new Sample_OperatorCalculator_VarFrequency_StereoToMono_NoPhaseTracking(frequencyCalculator, sampleCalculator, dimensionStack);
                    }
                }
            }

            if (calculator == null)
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
            }

            _stack.Push(calculator);
        }

        protected override void VisitSawDown(Operator op)
        {
            var wrapper = new SawDown_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);

            base.VisitSawDown(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase frequencyCalculator = _stack.Pop();
            OperatorCalculatorBase phaseShiftCalculator = _stack.Pop();

            frequencyCalculator = frequencyCalculator ?? new Zero_OperatorCalculator();
            phaseShiftCalculator = phaseShiftCalculator ?? new Zero_OperatorCalculator();
            double frequency = frequencyCalculator.Calculate();
            double phaseShift = phaseShiftCalculator.Calculate();
            bool frequencyIsConst = frequencyCalculator is Number_OperatorCalculator;
            bool phaseShiftIsConst = phaseShiftCalculator is Number_OperatorCalculator;
            bool frequencyIsConstZero = frequencyIsConst && frequency == 0;
            bool phaseShiftIsConstZero = phaseShiftIsConst && phaseShift % 1 == 0;

            if (frequencyIsConstZero)
            {
                // Special number
                calculator = new Zero_OperatorCalculator();
            }
            else if (frequencyIsConst && phaseShiftIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new SawDown_OperatorCalculator_ConstFrequency_ConstPhaseShift_WithOriginShifting(frequency, phaseShift, dimensionStack);
            }
            else if (frequencyIsConst && phaseShiftIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new SawDown_OperatorCalculator_ConstFrequency_ConstPhaseShift_NoOriginShifting(frequency, phaseShift, dimensionStack);
            }
            else if (!frequencyIsConst && phaseShiftIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new SawDown_OperatorCalculator_VarFrequency_ConstPhaseShift_WithPhaseTracking(frequencyCalculator, phaseShift, dimensionStack);
            }
            else if (!frequencyIsConst && phaseShiftIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new SawDown_OperatorCalculator_VarFrequency_ConstPhaseShift_NoPhaseTracking(frequencyCalculator, phaseShift, dimensionStack);
            }
            else if (frequencyIsConst && !phaseShiftIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new SawDown_OperatorCalculator_ConstFrequency_VarPhaseShift_WithOriginShifting(frequency, phaseShiftCalculator, dimensionStack);
            }
            else if (frequencyIsConst && !phaseShiftIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new SawDown_OperatorCalculator_ConstFrequency_VarPhaseShift_NoOriginShifting(frequency, phaseShiftCalculator, dimensionStack);
            }
            else if (!frequencyIsConst && !phaseShiftIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new SawDown_OperatorCalculator_VarFrequency_VarPhaseShift_WithPhaseTracking(frequencyCalculator, phaseShiftCalculator, dimensionStack);
            }
            else if (!frequencyIsConst && !phaseShiftIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new SawDown_OperatorCalculator_VarFrequency_VarPhaseShift_NoPhaseTracking(frequencyCalculator, phaseShiftCalculator, dimensionStack);
            }
            else
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
            }

            _stack.Push(calculator);
        }

        protected override void VisitSawUp(Operator op)
        {
            var wrapper = new SawUp_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);

            base.VisitSawUp(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase frequencyCalculator = _stack.Pop();
            OperatorCalculatorBase phaseShiftCalculator = _stack.Pop();

            frequencyCalculator = frequencyCalculator ?? new Zero_OperatorCalculator();
            phaseShiftCalculator = phaseShiftCalculator ?? new Zero_OperatorCalculator();
            double frequency = frequencyCalculator.Calculate();
            double phaseShift = phaseShiftCalculator.Calculate();
            bool frequencyIsConst = frequencyCalculator is Number_OperatorCalculator;
            bool phaseShiftIsConst = phaseShiftCalculator is Number_OperatorCalculator;
            bool frequencyIsConstZero = frequencyIsConst && frequency == 0;
            bool phaseShiftIsConstZero = phaseShiftIsConst && phaseShift % 1 == 0;

            if (frequencyIsConstZero)
            {
                // Special number
                calculator = new Zero_OperatorCalculator();
            }
            else if (frequencyIsConst && phaseShiftIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new SawUp_OperatorCalculator_ConstFrequency_ConstPhaseShift_WithOriginShifting(frequency, phaseShift, dimensionStack);
            }
            else if (frequencyIsConst && phaseShiftIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new SawUp_OperatorCalculator_ConstFrequency_ConstPhaseShift_NoOriginShifting(frequency, phaseShift, dimensionStack);
            }
            else if (!frequencyIsConst && phaseShiftIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new SawUp_OperatorCalculator_VarFrequency_ConstPhaseShift_WithPhaseTracking(frequencyCalculator, phaseShift, dimensionStack);
            }
            else if (!frequencyIsConst && phaseShiftIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new SawUp_OperatorCalculator_VarFrequency_ConstPhaseShift_NoPhaseTracking(frequencyCalculator, phaseShift, dimensionStack);
            }
            else if (frequencyIsConst && !phaseShiftIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new SawUp_OperatorCalculator_ConstFrequency_VarPhaseShift_WithOriginShifting(frequency, phaseShiftCalculator, dimensionStack);
            }
            else if (frequencyIsConst && !phaseShiftIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new SawUp_OperatorCalculator_ConstFrequency_VarPhaseShift_NoOriginShifting(frequency, phaseShiftCalculator, dimensionStack);
            }
            else if (!frequencyIsConst && !phaseShiftIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new SawUp_OperatorCalculator_VarFrequency_VarPhaseShift_WithPhaseTracking(frequencyCalculator, phaseShiftCalculator, dimensionStack);
            }
            else if (!frequencyIsConst && !phaseShiftIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new SawUp_OperatorCalculator_VarFrequency_VarPhaseShift_NoPhaseTracking(frequencyCalculator, phaseShiftCalculator, dimensionStack);
            }
            else
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
            }

            _stack.Push(calculator);
        }

        protected override void VisitScaler(Operator op)
        {
            base.VisitScaler(op);

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

            bool sourceValueAIsConst = sourceValueACalculator is Number_OperatorCalculator;
            bool sourceValueBIsConst = sourceValueBCalculator is Number_OperatorCalculator;
            bool targetValueAIsConst = targetValueACalculator is Number_OperatorCalculator;
            bool targetValueBIsConst = targetValueBCalculator is Number_OperatorCalculator;

            double sourceValueA = sourceValueAIsConst ? sourceValueACalculator.Calculate() : 0.0;
            double sourceValueB = sourceValueBIsConst ? sourceValueBCalculator.Calculate() : 0.0;
            double targetValueA = targetValueAIsConst ? targetValueACalculator.Calculate() : 0.0;
            double targetValueB = targetValueBIsConst ? targetValueBCalculator.Calculate() : 0.0;

            if (sourceValueAIsConst && sourceValueBIsConst && targetValueAIsConst && targetValueBIsConst)
            {
                calculator = new Scaler_OperatorCalculator_ManyConstants(signalCalculator, sourceValueA, sourceValueB, targetValueA, targetValueB);
            }
            else
            {
                calculator = new Scaler_OperatorCalculator_AllVariables(signalCalculator, sourceValueACalculator, sourceValueBCalculator, targetValueACalculator, targetValueBCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitSelect(Operator op)
        {
            var wrapper = new Select_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);
            dimensionStack.Push(DEFAULT_DIMENSION_VALUE);

            base.VisitSelect(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase positionCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            positionCalculator = positionCalculator ?? new Zero_OperatorCalculator();

            double signal = signalCalculator.Calculate();
            double position = positionCalculator.Calculate();
            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool positionIsConst = positionCalculator is Number_OperatorCalculator;

            dimensionStack.Pop();

            if (signalIsConst)
            {
                calculator = new Number_OperatorCalculator(signal);
            }
            else if (positionIsConst)
            {
                calculator = new Select_OperatorCalculator_ConstPosition(signalCalculator, position, dimensionStack);
            }
            else
            {
                calculator = new Select_OperatorCalculator_VarPosition(signalCalculator, positionCalculator, dimensionStack);
            }

            _stack.Push(calculator);
        }

        protected override void VisitSetDimension(Operator op)
        {
            var wrapper = new SetDimension_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);
            dimensionStack.Push(DEFAULT_DIMENSION_VALUE);

            base.VisitSetDimension(op);

            OperatorCalculatorBase operatorCalculator;

            OperatorCalculatorBase calculationCalculator = _stack.Pop();
            OperatorCalculatorBase valueCalculator = _stack.Pop();

            calculationCalculator = calculationCalculator ?? new Zero_OperatorCalculator();
            valueCalculator = valueCalculator ?? new Zero_OperatorCalculator();

            bool calculationIsConst = calculationCalculator is Number_OperatorCalculator;
            bool valueIsConst = valueCalculator is Number_OperatorCalculator;

            double value = valueCalculator.Calculate();

            dimensionStack.Pop();

            if (calculationIsConst)
            {
                operatorCalculator = calculationCalculator;
            }
            else if (valueIsConst)
            {
                operatorCalculator = new SetDimension_OperatorCalculator_ConstValue(calculationCalculator, value, dimensionStack);
            }
            else
            {
                operatorCalculator = new SetDimension_OperatorCalculator_VarValue(calculationCalculator, valueCalculator, dimensionStack);
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
            var wrapper = new Sine_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);

            base.VisitSine(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase frequencyCalculator = _stack.Pop();
            OperatorCalculatorBase phaseShiftCalculator = _stack.Pop();

            frequencyCalculator = frequencyCalculator ?? new Zero_OperatorCalculator();
            phaseShiftCalculator = phaseShiftCalculator ?? new Zero_OperatorCalculator();
            double frequency = frequencyCalculator.Calculate();
            double phaseShift = phaseShiftCalculator.Calculate();
            bool frequencyIsConst = frequencyCalculator is Number_OperatorCalculator;
            bool phaseShiftIsConst = phaseShiftCalculator is Number_OperatorCalculator;
            bool frequencyIsConstZero = frequencyIsConst && frequency == 0.0;
            bool phaseShiftIsConstZero = phaseShiftIsConst && phaseShift % 1.0 == 0.0;

            if (frequencyIsConstZero)
            {
                // Special number
                // Frequency 0 means time stands still.
                double value = SineCalculator.Sin(phaseShift);
                calculator = new Number_OperatorCalculator(value);
            }
            else if (frequencyIsConst && phaseShiftIsConstZero && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new Sine_OperatorCalculator_ConstFrequency_PhaseShiftZero_WithOriginShifting(frequency, dimensionStack);
            }
            else if (frequencyIsConst && phaseShiftIsConstZero && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new Sine_OperatorCalculator_ConstFrequency_PhaseShiftZero_NoOriginShifting(frequency, dimensionStack);
            }
            else if (frequencyIsConst && phaseShiftIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new Sine_OperatorCalculator_ConstFrequency_ConstPhaseShift_WithOriginShifting(frequency, phaseShift, dimensionStack);
            }
            else if (frequencyIsConst && phaseShiftIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new Sine_OperatorCalculator_ConstFrequency_ConstPhaseShift_NoOriginShifting(frequency, phaseShift, dimensionStack);
            }
            else if (frequencyIsConst && !phaseShiftIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new Sine_OperatorCalculator_ConstFrequency_VarPhaseShift_WithOriginShifting(frequency, phaseShiftCalculator, dimensionStack);
            }
            else if (frequencyIsConst && !phaseShiftIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new Sine_OperatorCalculator_ConstFrequency_VarPhaseShift_NoOriginShifting(frequency, phaseShiftCalculator, dimensionStack);
            }
            else if (!frequencyIsConst && phaseShiftIsConstZero && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new Sine_OperatorCalculator_VarFrequency_PhaseShiftZero_WithPhaseTracking(frequencyCalculator, dimensionStack);
            }
            else if (!frequencyIsConst && phaseShiftIsConstZero && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new Sine_OperatorCalculator_VarFrequency_PhaseShiftZero_NoPhaseTracking(frequencyCalculator, dimensionStack);
            }
            else if (!frequencyIsConst && phaseShiftIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new Sine_OperatorCalculator_VarFrequency_ConstPhaseShift_WithPhaseTracking(frequencyCalculator, phaseShift, dimensionStack);
            }
            else if (!frequencyIsConst && phaseShiftIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new Sine_OperatorCalculator_VarFrequency_ConstPhaseShift_NoPhaseTracking(frequencyCalculator, phaseShift, dimensionStack);
            }
            else if (!frequencyIsConst && !phaseShiftIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new Sine_OperatorCalculator_VarFrequency_VarPhaseShift_WithPhaseTracking(frequencyCalculator, phaseShiftCalculator, dimensionStack);
            }
            else if (!frequencyIsConst && !phaseShiftIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new Sine_OperatorCalculator_VarFrequency_VarPhaseShift_NoPhaseTracking(frequencyCalculator, phaseShiftCalculator, dimensionStack);
            }
            else
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
            }

            _stack.Push(calculator);
        }

        protected override void VisitSlowDown(Operator op)
        {
            var wrapper = new SlowDown_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);
            dimensionStack.Push(DEFAULT_DIMENSION_VALUE);

            base.VisitSlowDown(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase factorCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            factorCalculator = factorCalculator ?? new One_OperatorCalculator();

            double signal = signalCalculator.Calculate();
            double factor = factorCalculator.Calculate();

            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool factorIsConst = factorCalculator is Number_OperatorCalculator;

            bool signalIsConstZero = signalIsConst && signal == 0;
            bool factorIsConstZero = factorIsConst && factor == 0;

            bool factorIsConstOne = factorIsConst && factor == 1;

            bool signalIsConstSpecialNumber = signalIsConst && DoubleHelper.IsSpecialNumber(signal);
            bool factorIsConstSpecialNumber = factorIsConst && DoubleHelper.IsSpecialNumber(factor);

            dimensionStack.Pop();

            if (factorIsConstSpecialNumber)
            {
                // Special number
                // Slow down to inifinity, means time stands still. (Consider: 2x as slow, 100x as slow, inifity as slow...)
                calculator = new Number_OperatorCalculator(signal);
            }
            if (signalIsConstSpecialNumber)
            {
                // Special number
                calculator = new Number_OperatorCalculator(signal);
            }
            else if (factorIsConstZero)
            {
                // Special number
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
            else if (factorIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new SlowDown_OperatorCalculator_ConstFactor_WithOriginShifting(signalCalculator, factor, dimensionStack);
            }
            else if (factorIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new SlowDown_OperatorCalculator_ConstFactor_NoOriginShifting(signalCalculator, factor, dimensionStack);
            }
            else if (!factorIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new SlowDown_OperatorCalculator_VarFactor_WithPhaseTracking(signalCalculator, factorCalculator, dimensionStack);
            }
            else if (!factorIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new SlowDown_OperatorCalculator_VarFactor_NoPhaseTracking(signalCalculator, factorCalculator, dimensionStack);
            }
            else
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
            }

            _stack.Push(calculator);
        }

        protected override void VisitSpectrum(Operator op)
        {
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(DimensionEnum.Time);
            dimensionStack.Push(DEFAULT_DIMENSION_VALUE);

            base.VisitSpectrum(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase startTimeCalculator = _stack.Pop();
            OperatorCalculatorBase endTimeCalculator = _stack.Pop();
            OperatorCalculatorBase frequencyCountCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            // TODO: Lower priority: Do not use these magic defaults, but give standard operators default inlet value functionality.
            startTimeCalculator = startTimeCalculator ?? new Number_OperatorCalculator(0.0);
            endTimeCalculator = endTimeCalculator ?? new Number_OperatorCalculator(1.0);
            frequencyCountCalculator = frequencyCountCalculator ?? new Number_OperatorCalculator(16);

            double signal = signalCalculator.Calculate();
            double startTime = startTimeCalculator.Calculate();
            double endTime = endTimeCalculator.Calculate();
            double frequencyCount = frequencyCountCalculator.Calculate();

            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool signalIsConstZero = signalIsConst && signal == 0;
            bool signalIsConstSpecialNumber = signalIsConst && DoubleHelper.IsSpecialNumber(signal);

            dimensionStack.Pop();

            if (signalIsConstSpecialNumber)
            {
                // Special number
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
                    frequencyCountCalculator,
                    dimensionStack);
            }

            _stack.Push(calculator);
        }

        protected override void VisitSpeedUp(Operator op)
        {
            var wrapper = new SpeedUp_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);
            dimensionStack.Push(DEFAULT_DIMENSION_VALUE);

            base.VisitSpeedUp(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase factorCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            factorCalculator = factorCalculator ?? new One_OperatorCalculator();

            double signal = signalCalculator.Calculate();
            double factor = factorCalculator.Calculate();

            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool factorIsConst = factorCalculator is Number_OperatorCalculator;

            bool signalIsConstZero = signalIsConst && signal == 0;
            bool factorIsConstZero = factorIsConst && factor == 0;

            bool factorIsConstOne = factorIsConst && factor == 1;

            bool signalIsConstSpecialNumber = signalIsConst && DoubleHelper.IsSpecialNumber(signal);
            bool factorIsConstSpecialNumber = factorIsConst && DoubleHelper.IsSpecialNumber(factor);

            dimensionStack.Pop();

            if (factorIsConstSpecialNumber)
            {
                // Special number
                calculator = new Number_OperatorCalculator(Double.NaN);
            }
            else if (signalIsConstSpecialNumber)
            {
                // Special number
                calculator = new Number_OperatorCalculator(signal);
            }
            else if (factorIsConstZero)
            {
                // Special number
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
            else if (factorIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new SpeedUp_OperatorCalculator_ConstFactor_WithOriginShifting(signalCalculator, factor, dimensionStack);
            }
            else if (factorIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new SpeedUp_OperatorCalculator_ConstFactor_NoOriginShifting(signalCalculator, factor, dimensionStack);
            }
            else if (!factorIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new SpeedUp_OperatorCalculator_VarFactor_WithPhaseTracking(signalCalculator, factorCalculator, dimensionStack);
            }
            else if (!factorIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new SpeedUp_OperatorCalculator_VarFactor_NoPhaseTracking(signalCalculator, factorCalculator, dimensionStack);
            }
            else
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
            }

            _stack.Push(calculator);
        }

        protected override void VisitSquare(Operator op)
        {
            var wrapper = new Square_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);

            base.VisitSquare(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase frequencyCalculator = _stack.Pop();
            OperatorCalculatorBase phaseShiftCalculator = _stack.Pop();

            frequencyCalculator = frequencyCalculator ?? new Zero_OperatorCalculator();
            phaseShiftCalculator = phaseShiftCalculator ?? new Zero_OperatorCalculator();

            double frequency = frequencyCalculator.Calculate();
            double phaseShift = phaseShiftCalculator.Calculate() % 1;

            bool frequencyIsConst = frequencyCalculator is Number_OperatorCalculator;
            bool phaseShiftIsConst = phaseShiftCalculator is Number_OperatorCalculator;

            bool frequencyIsConstZero = frequencyIsConst && frequency == 0;

            bool frequencyIsConstSpecialNumber = frequencyIsConst && DoubleHelper.IsSpecialNumber(frequency);
            bool phaseShiftIsConstSpecialNumber = phaseShiftIsConst && DoubleHelper.IsSpecialNumber(phaseShift);

            if (frequencyIsConstSpecialNumber || phaseShiftIsConstSpecialNumber)
            {
                // Special number
                calculator = new Number_OperatorCalculator(Double.NaN);
            }
            if (frequencyIsConstZero)
            {
                // Special number
                calculator = new Zero_OperatorCalculator();
            }
            else if (frequencyIsConst && phaseShiftIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new Pulse_OperatorCalculator_ConstFrequency_ConstWidth_ConstPhaseShift_WithOriginShifting(frequency, DEFAULT_PULSE_WIDTH, phaseShift, dimensionStack);
            }
            else if (frequencyIsConst && phaseShiftIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new Pulse_OperatorCalculator_ConstFrequency_ConstWidth_ConstPhaseShift_NoOriginShifting(frequency, DEFAULT_PULSE_WIDTH, phaseShift, dimensionStack);
            }
            else if (!frequencyIsConst && phaseShiftIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new Pulse_OperatorCalculator_VarFrequency_ConstWidth_ConstPhaseShift_WithPhaseTracking(frequencyCalculator, DEFAULT_PULSE_WIDTH, phaseShift, dimensionStack);
            }
            else if (!frequencyIsConst && phaseShiftIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new Pulse_OperatorCalculator_VarFrequency_ConstWidth_ConstPhaseShift_NoPhaseTracking(frequencyCalculator, DEFAULT_PULSE_WIDTH, phaseShift, dimensionStack);
            }
            else if (frequencyIsConst && !phaseShiftIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new Pulse_OperatorCalculator_ConstFrequency_ConstWidth_VarPhaseShift_WithOriginShifting(frequency, DEFAULT_PULSE_WIDTH, phaseShiftCalculator, dimensionStack);
            }
            else if (frequencyIsConst && !phaseShiftIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new Pulse_OperatorCalculator_ConstFrequency_ConstWidth_VarPhaseShift_NoOriginShifting(frequency, DEFAULT_PULSE_WIDTH, phaseShiftCalculator, dimensionStack);
            }
            else if (!frequencyIsConst && !phaseShiftIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new Pulse_OperatorCalculator_VarFrequency_ConstWidth_VarPhaseShift_WithPhaseTracking(frequencyCalculator, DEFAULT_PULSE_WIDTH, phaseShiftCalculator, dimensionStack);
            }
            else if (!frequencyIsConst && !phaseShiftIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new Pulse_OperatorCalculator_VarFrequency_ConstWidth_VarPhaseShift_NoPhaseTracking(frequencyCalculator, DEFAULT_PULSE_WIDTH, phaseShiftCalculator, dimensionStack);
            }
            else
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
            }

            _stack.Push(calculator);
        }

        protected override void VisitStretch(Operator op)
        {
            var wrapper = new Stretch_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);
            dimensionStack.Push(DEFAULT_DIMENSION_VALUE);

            base.VisitStretch(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase factorCalculator = _stack.Pop();
            OperatorCalculatorBase originCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Zero_OperatorCalculator();
            factorCalculator = factorCalculator ?? new One_OperatorCalculator();
            originCalculator = originCalculator ?? new Zero_OperatorCalculator();

            double signal = signalCalculator.Calculate();
            double factor = factorCalculator.Calculate();
            double origin = originCalculator.Calculate();

            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool factorIsConst = factorCalculator is Number_OperatorCalculator;
            bool originIsConst = originCalculator is Number_OperatorCalculator;

            bool signalIsConstZero = signalIsConst && signal == 0;
            bool factorIsConstZero = factorIsConst && factor == 0;
            bool originIsConstZero = originIsConst && origin == 0;

            bool factorIsConstOne = factorIsConst && factor == 1;

            // TODO: Handle const special numbers.

            dimensionStack.Pop();

            if (factorIsConstZero)
            {
                // Special number
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
                calculator = new Stretch_OperatorCalculator_VarSignal_ConstFactor_ZeroOrigin(signalCalculator, factor, dimensionStack);
            }
            else if (!signalIsConst && !factorIsConst && originIsConstZero)
            {
                calculator = new Stretch_OperatorCalculator_VarSignal_VarFactor_ZeroOrigin(signalCalculator, factorCalculator, dimensionStack);
            }
            else if (!signalIsConst && factorIsConst && originIsConst)
            {
                calculator = new Stretch_OperatorCalculator_VarSignal_ConstFactor_ConstOrigin(signalCalculator, factor, origin, dimensionStack);
            }
            else if (!signalIsConst && factorIsConst && !originIsConst)
            {
                calculator = new Stretch_OperatorCalculator_VarSignal_ConstFactor_VarOrigin(signalCalculator, factor, originCalculator, dimensionStack);
            }
            else if (!signalIsConst && !factorIsConst && originIsConst)
            {
                calculator = new Stretch_OperatorCalculator_VarSignal_VarFactor_ConstOrigin(signalCalculator, factorCalculator, origin, dimensionStack);
            }
            else if (!signalIsConst && !factorIsConst && !originIsConst)
            {
                calculator = new Stretch_OperatorCalculator_VarSignal_VarFactor_VarOrigin(signalCalculator, factorCalculator, originCalculator, dimensionStack);
            }
            else
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
            }

            _stack.Push(calculator);
        }

        protected override void VisitSubtract(Operator op)
        {
            base.VisitSubtract(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase aCalculator = _stack.Pop();
            OperatorCalculatorBase bCalculator = _stack.Pop();

            aCalculator = aCalculator ?? new Zero_OperatorCalculator();
            bCalculator = bCalculator ?? new Zero_OperatorCalculator();

            double a = aCalculator.Calculate();
            double b = bCalculator.Calculate();
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
            var wrapper = new TimePower_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);
            dimensionStack.Push(DEFAULT_DIMENSION_VALUE);

            base.VisitTimePower(op);

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

            double signal = signalCalculator.Calculate();
            double exponent = exponentCalculator.Calculate();
            double origin = originCalculator.Calculate();
            bool signalIsConst = signalCalculator is Number_OperatorCalculator;
            bool exponentIsConst = exponentCalculator is Number_OperatorCalculator;
            bool originIsConst = originCalculator is Number_OperatorCalculator;
            bool signalIsConstZero = signalIsConst && signal == 0;
            bool exponentIsConstZero = exponentIsConst && exponent == 0;
            bool originIsConstZero = originIsConst && origin == 0;
            bool exponentIsConstOne = exponentIsConst && exponent == 1;

            dimensionStack.Pop();

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
                calculator = new TimePower_OperatorCalculator_NoOrigin(signalCalculator, exponentCalculator, dimensionStack);
            }
            else
            {
                calculator = new TimePower_OperatorCalculator_WithOrigin(signalCalculator, exponentCalculator, originCalculator, dimensionStack);
            }

            _stack.Push(calculator);
        }

        protected override void VisitToggleTrigger(Operator op)
        {
            base.VisitToggleTrigger(op);

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
            var wrapper = new Triangle_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);

            base.VisitTriangle(op);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase frequencyCalculator = _stack.Pop();
            OperatorCalculatorBase phaseShiftCalculator = _stack.Pop();

            frequencyCalculator = frequencyCalculator ?? new Zero_OperatorCalculator();
            phaseShiftCalculator = phaseShiftCalculator ?? new Zero_OperatorCalculator();
            double frequency = frequencyCalculator.Calculate();
            double phaseShift = phaseShiftCalculator.Calculate();
            bool frequencyIsConst = frequencyCalculator is Number_OperatorCalculator;
            bool phaseShiftIsConst = phaseShiftCalculator is Number_OperatorCalculator;
            bool frequencyIsConstZero = frequencyIsConst && frequency == 0;

            if (frequencyIsConstZero)
            {
                // Special number
                // Frequency 0 means time stands still.
                calculator = new Number_OperatorCalculator(phaseShift);
            }
            else if (frequencyIsConst && phaseShiftIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new Triangle_OperatorCalculator_ConstFrequency_ConstPhaseShift_WithOriginShifting(frequency, phaseShift, dimensionStack);
            }
            else if (frequencyIsConst && phaseShiftIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new Triangle_OperatorCalculator_ConstFrequency_ConstPhaseShift_NoOriginShifting(frequency, phaseShift, dimensionStack);
            }
            else if (!frequencyIsConst && phaseShiftIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new Triangle_OperatorCalculator_VarFrequency_ConstPhaseShift_WithPhaseTracking(frequencyCalculator, phaseShift, dimensionStack);
            }
            else if (!frequencyIsConst && phaseShiftIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new Triangle_OperatorCalculator_VarFrequency_ConstPhaseShift_NoPhaseTracking(frequencyCalculator, phaseShift, dimensionStack);
            }
            else if (frequencyIsConst && !phaseShiftIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new Triangle_OperatorCalculator_ConstFrequency_VarPhaseShift_WithOriginShifting(frequency, phaseShiftCalculator, dimensionStack);
            }
            else if (frequencyIsConst && !phaseShiftIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new Triangle_OperatorCalculator_ConstFrequency_VarPhaseShift_NoOriginShifting(frequency, phaseShiftCalculator, dimensionStack);
            }
            else if (!frequencyIsConst && !phaseShiftIsConst && dimensionEnum == DimensionEnum.Time)
            {
                calculator = new Triangle_OperatorCalculator_VarFrequency_VarPhaseShift_WithPhaseTracking(frequencyCalculator, phaseShiftCalculator, dimensionStack);
            }
            else if (!frequencyIsConst && !phaseShiftIsConst && dimensionEnum != DimensionEnum.Time)
            {
                calculator = new Triangle_OperatorCalculator_VarFrequency_VarPhaseShift_NoPhaseTracking(frequencyCalculator, phaseShiftCalculator, dimensionStack);
            }
            else
            {
                throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
            }

            _stack.Push(calculator);
        }

        protected override void VisitUnbundle(Operator op)
        {
            if (BUNDLE_POSITIONS_ARE_INVARIANT)
            {
                throw new Exception("VisitUnbundle should not execute if BUNDLE_POSITIONS_ARE_INVARIANT.");
            }

            var wrapper = new Unbundle_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);

            // NOTE: Pushing and popping from the dimension stack is done in VisitUnbundleOutlet_WithVariableBundlePositions.

            base.VisitUnbundle(op);

            OperatorCalculatorBase operatorCalculator;

            OperatorCalculatorBase operandCalculator = _stack.Pop();

            operandCalculator = operandCalculator ?? new Zero_OperatorCalculator();

            bool operandIsConst = operandCalculator is Number_OperatorCalculator;
            
            if (operandIsConst)
            {
                operatorCalculator = operandCalculator;
            }   
            else
            {
                double position = dimensionStack.Get();

                operatorCalculator = new Unbundle_OperatorCalculator(operandCalculator, position, dimensionStack);
            }

            _stack.Push(operatorCalculator);
        }

        // Special Visitation

        /// <summary> Overridden to push null-inlets or default values for those inlets. </summary>
        [DebuggerHidden]
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
            base.VisitPatchInlet(patchInlet);

            var wrapper = new PatchInlet_OperatorWrapper(patchInlet);

            OperatorCalculatorBase calculator;

            OperatorCalculatorBase inputCalculator = _stack.Pop();

            inputCalculator = inputCalculator ?? new Zero_OperatorCalculator();
            double input = inputCalculator.Calculate();
            double defaultValue = wrapper.Inlet.DefaultValue ?? 0.0;
            bool inputIsConst = inputCalculator is Number_OperatorCalculator;
            bool inputIsConstDefaultValue = inputIsConst && input == defaultValue;
            bool isTopLevelPatchInlet = IsTopLevelPatchInlet(patchInlet);

            // Only top-level PatchInlets are values controllable from the outside.
            // For compatibility we only apply that rule if nothing else was filled in
            // into the (non-visible) PatchInlet Inlet.
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

        [DebuggerHidden]
        protected override void VisitOutlet(Outlet outlet)
        {
            OperatorTypeEnum operatorTypeEnum = outlet.Operator.GetOperatorTypeEnum();

            if (operatorTypeEnum == OperatorTypeEnum.CustomOperator)
            {
                VisitCustomOperatorOutlet(outlet);
                return;
            }
            else if (operatorTypeEnum == OperatorTypeEnum.Bundle)
            {
                VisitBundleOutlet(outlet);
                return;
            }
            else if (operatorTypeEnum == OperatorTypeEnum.Unbundle)
            {
                VisitUnbundleOutlet(outlet);
                return;
            }

            base.VisitOutlet(outlet);
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

            VisitOperatorPolymorphic(patchOutlet_Outlet.Operator);
        }

        private void VisitUnbundleOutlet(Outlet outlet)
        {
            if (BUNDLE_POSITIONS_ARE_INVARIANT)
            {
                VisitUnbundleOutlet_WithInvariantBundlePositions(outlet);
            }
            else
            {
                VisitUnbundleOutlet_WithVariableBundlePositions(outlet);
            }
        }

        private void VisitUnbundleOutlet_WithInvariantBundlePositions(Outlet outlet)
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

            var wrapper = new Unbundle_OperatorWrapper(op);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);

            dimensionStack.Push(outletIndex);

            VisitOutlet(inputOutlet);

            dimensionStack.Pop();
        }

        private void VisitUnbundleOutlet_WithVariableBundlePositions(Outlet outlet)
        {
            int outletIndex = outlet.Operator.Outlets.IndexOf(outlet);

            var wrapper = new Unbundle_OperatorWrapper(outlet.Operator);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);

            dimensionStack.Push(outletIndex);

            base.VisitOutlet(outlet);

            dimensionStack.Pop();
        }

        private void VisitBundleOutlet(Outlet outlet)
        {
            if (BUNDLE_POSITIONS_ARE_INVARIANT)
            {
                VisitBundleOutlet_WithInvariantBundlePositions(outlet);
            }
            else
            {
                VisitBundleOutlet_WithVariableBundlePositions(outlet);
            }
        }

        private void VisitBundleOutlet_WithInvariantBundlePositions(Outlet outlet)
        {
            var wrapper = new Bundle_OperatorWrapper(outlet.Operator);
            DimensionEnum dimensionEnum = wrapper.Dimension;
            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);

            if (dimensionStack.Count == 0)
            {
                throw new NotSupportedException(String.Format(
                    "Bundle Operator with ID '{0}' and Dimension '{1}' encountered without first encountering an Unbundle Operator. This is not yet supported.",
                    outlet.Operator.ID,
                    wrapper.Dimension));
            }

            double bundleIndexDouble = dimensionStack.PopAndGet();

            if (!ConversionHelper.CanCastToNonNegativeInt32(bundleIndexDouble))
            {
                throw new Exception(String.Format(
                    "Index '{0}' cannot be cast to non-negative Int32 for Bundle Operator with ID '{1}' and Dimension '{2}'.",
                    bundleIndexDouble,
                    outlet.Operator.ID,
                    wrapper.Dimension));
            }

            if (bundleIndexDouble >= outlet.Operator.Inlets.Count)
            {
                throw new Exception(String.Format(
                    "Index '{0}' does not exist in Bundle Operator with ID '{1}' and Dimension '{2}'.",
                    bundleIndexDouble,
                    outlet.Operator.ID,
                    wrapper.Dimension));
            }

            int bundleIndexInt32 = (int)bundleIndexDouble;

            Inlet inlet = outlet.Operator.Inlets[bundleIndexInt32];
            if (inlet.InputOutlet == null)
            {
                double defaultValue = inlet.DefaultValue ?? 0.0;
                _stack.Push(new Number_OperatorCalculator(defaultValue));
            }
            else
            {
                VisitOutlet(inlet.InputOutlet);
            }

            dimensionStack.Push(bundleIndexDouble);
        }

        private void VisitBundleOutlet_WithVariableBundlePositions(Outlet outlet)
        {
            base.VisitOutlet(outlet);
        }

        protected override void VisitReset(Operator op)
        {
            base.VisitReset(op);

            var wrapper = new Reset_OperatorWrapper(op);

            OperatorCalculatorBase calculator = _stack.Peek();

            // Be forgiving when it comes to name being filled in. A warning is generated instead.
            _resettableOperatorTuples.Add(new ResettableOperatorTuple(calculator, op.Name, wrapper.ListIndex));
        }
    }
}