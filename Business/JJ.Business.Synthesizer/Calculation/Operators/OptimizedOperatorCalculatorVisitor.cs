using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Calculation.Operators.Entities;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Visitors;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal partial class OptimizedOperatorCalculatorVisitor : OperatorVisitorBase
    {
        private ICurveRepository _curveRepository;
        private ISampleRepository _sampleRepository;
        private int _channelCount;
        private Stack<OperatorCalculatorBase> _stack;
        private Dictionary<int, OperatorCalculatorBase> _dictionary = new Dictionary<int, OperatorCalculatorBase>();

        public IList<OperatorCalculatorBase> Execute(IList<Outlet> channelOutlets, ICurveRepository curveRepository, ISampleRepository sampleRepository)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (channelOutlets == null) throw new NullException(() => channelOutlets);

            _curveRepository = curveRepository;
            _sampleRepository = sampleRepository;

            foreach (Outlet channelOutlet in channelOutlets)
            {
                IValidator validator = new OperatorValidator_Recursive(channelOutlet.Operator, _curveRepository, _sampleRepository, alreadyDone: new HashSet<object>());
                validator.Verify();
            }

            _stack = new Stack<OperatorCalculatorBase>();
            _dictionary = new Dictionary<int, OperatorCalculatorBase>();
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

                operatorCalculator = operatorCalculator ?? new Zero_Calculator(); 

                list.Add(operatorCalculator);
            }

            return list;
        }

        protected override void VisitOperator(Operator op)
        {
            OperatorCalculatorBase calculator;
            if (_dictionary.TryGetValue(op.ID, out calculator))
            {
                _stack.Push(calculator);
                return;
            }

            base.VisitOperator(op);

            calculator = _stack.Peek();
            _dictionary.Add(op.ID, calculator);
        }

        protected override void VisitValueOperator(Operator op)
        {
            var wrapper = new ValueOperatorWrapper(op);
            double value = wrapper.Value;

            var calculator = new Value_Calculator(value);
            _stack.Push(calculator);
        }

        protected override void VisitAdd(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase operandACalculator = _stack.Pop();
            OperatorCalculatorBase operandBCalculator = _stack.Pop();

            operandACalculator = operandACalculator ?? new Value_Calculator(0);
            operandBCalculator = operandBCalculator ?? new Value_Calculator(0);

            double a = operandACalculator.Calculate(0, 0);
            double b = operandBCalculator.Calculate(0, 0);
            bool operandAIsConst = operandACalculator is Value_Calculator;
            bool operandBIsConst = operandBCalculator is Value_Calculator;
            bool operandAIsConstZero = operandAIsConst && a == 0;
            bool operandBIsConstZero = operandAIsConst && b == 0;

            if (operandAIsConstZero && operandBIsConstZero)
            {
                calculator = new Value_Calculator(0);
            }
            else if (operandAIsConstZero)
            {
                calculator = operandBCalculator;
            }
            else if (operandBIsConstZero)
            {
                calculator = operandACalculator;
            }
            if (operandAIsConst && operandBIsConst)
            {
                calculator = new Value_Calculator(a + b);
            }
            else if (operandAIsConst)
            {
                calculator = new Add_WithConstOperandA_Calculator(a, operandBCalculator);
            }
            else if (operandBIsConst)
            {
                calculator = new Add_WithConstOperandB_Calculator(operandACalculator, b);
            }
            else
            {
                calculator = new Add_Calculator(operandACalculator, operandBCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitSubstract(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase operandACalculator = _stack.Pop();
            OperatorCalculatorBase operandBCalculator = _stack.Pop();

            operandACalculator = operandACalculator ?? new Value_Calculator(0);
            operandBCalculator = operandBCalculator ?? new Value_Calculator(0);

            double a = operandACalculator.Calculate(0, 0);
            double b = operandBCalculator.Calculate(0, 0);
            bool operandAIsConst = operandACalculator is Value_Calculator;
            bool operandBIsConst = operandBCalculator is Value_Calculator;
            bool operandAIsConstZero = operandAIsConst && a == 0;
            bool operandBIsConstZero = operandBIsConst && a == 0;

            if (operandAIsConstZero && operandBIsConstZero)
            {
                calculator = new Value_Calculator(0);
            }
            else if (operandBIsConstZero)
            {
                calculator = operandACalculator;
            }
            else if (operandAIsConst && operandBIsConst)
            {
                calculator = new Value_Calculator(a - b);
            }
            else if (operandAIsConst)
            {
                calculator = new Substract_WithConstOperandA_Calculator(a, operandBCalculator);
            }
            else if (operandBIsConst)
            {
                calculator = new Substract_WithConstOperandB_Calculator(operandACalculator, b);
            }
            else
            {
                calculator = new Substract_Calculator(operandACalculator, operandBCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitMultiply(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase operandACalculator = _stack.Pop();
            OperatorCalculatorBase operandBCalculator = _stack.Pop();
            OperatorCalculatorBase originCalculator = _stack.Pop();

            operandACalculator = operandACalculator ?? new Value_Calculator(1);
            operandBCalculator = operandBCalculator ?? new Value_Calculator(1);
            originCalculator = originCalculator ?? new Value_Calculator(0);

            double a = operandACalculator.Calculate(0, 0);
            double b = operandBCalculator.Calculate(0, 0);
            double origin = originCalculator.Calculate(0, 0);
            bool operandAIsConst = operandACalculator is Value_Calculator;
            bool operandBIsConst = operandBCalculator is Value_Calculator;
            bool originIsConst = originCalculator is Value_Calculator;
            bool operandAIsConstZero = operandAIsConst && a == 0;
            bool operandBIsConstZero = operandBIsConst && b == 0;
            bool originIsConstZero = operandBIsConst && b == 0;
            bool operandAIsConstOne = operandAIsConst && a == 1;
            bool operandBIsConstOne = operandBIsConst && b == 1;

            if (operandAIsConstZero || operandBIsConstZero)
            {
                calculator = new Value_Calculator(0);
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
                calculator = new Value_Calculator(a * b);
            }
            else if (originIsConst && operandAIsConst && operandBIsConst)
            {
                double value = (a - origin) * b + origin;
                calculator = new Value_Calculator(value);
            }
            else if (originIsConstZero && operandAIsConst && !operandBIsConst)
            {
                calculator = new Multiply_WithoutOrigin_WithConstOperandA_Calculator(a, operandBCalculator);
            }
            else if (originIsConstZero && !operandAIsConst && operandBIsConst)
            {
                calculator = new Multiply_WithoutOrigin_WithConstOperandB_Calculator(operandACalculator, b);
            }
            else if (originIsConstZero && !operandAIsConst && !operandBIsConst)
            {
                calculator = new Multiply_WithoutOrigin_Calculator(operandACalculator, operandBCalculator);
            }
            else if (originIsConst && operandAIsConst && !operandBIsConst)
            {
                calculator = new Multiply_WithConstOrigin_AndOperandA_Calculator(a, operandBCalculator, origin);
            }
            else if (originIsConst && !operandAIsConst && operandBIsConst)
            {
                calculator = new Multiply_WithConstOrigin_AndOperandB_Calculator(operandACalculator, b, origin);
            }
            else if (!originIsConst && !operandAIsConst && !operandBIsConst)
            {
                calculator = new Multiply_WithConstOrigin_Calculator(operandACalculator, operandBCalculator, origin);
            }
            else if (!originIsConst && operandAIsConst && operandBIsConst)
            {
                calculator = new Multiply_WithOrigin_AndConstOperandA_AndOperandB_Calculator(a, b, originCalculator);
            }
            else if (!originIsConst && operandAIsConst && !operandBIsConst)
            {
                calculator = new Multiply_WithOrigin_AndConstOperandA_Calculator(a, operandBCalculator, originCalculator);
            }
            else if (!originIsConst && !operandAIsConst && operandBIsConst)
            {
                calculator = new Multiply_WithOrigin_AndConstOperandB_Calculator(operandACalculator, b, originCalculator);
            }
            else
            {
                calculator = new Multiply_WithOrigin_Calculator(operandACalculator, operandBCalculator, originCalculator);
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

            numeratorCalculator = numeratorCalculator ?? new Value_Calculator(0);
            denominatorCalculator = denominatorCalculator ?? new Value_Calculator(1);
            originCalculator = originCalculator ?? new Value_Calculator(0);

            double numerator = numeratorCalculator.Calculate(0, 0);
            double denominator = denominatorCalculator.Calculate(0, 0);
            double origin = originCalculator.Calculate(0, 0);
            bool denominatorIsConst = denominatorCalculator is Value_Calculator;
            bool numeratorIsConst = numeratorCalculator is Value_Calculator;
            bool originIsConst = originCalculator is Value_Calculator;
            bool numeratorIsConstZero = numeratorIsConst && numerator == 0;
            bool denominatorIsConstZero = denominatorIsConst && denominator == 0;
            bool originIsConstZero = originIsConst && origin == 0;
            bool denominatorIsConstOne = denominatorIsConst && denominator == 1;

            if (denominatorIsConstZero)
            {
                // Weird number
                calculator = new Value_Calculator(0);
            }
            else if (numeratorIsConstZero)
            {
                calculator = new Value_Calculator(0);
            }
            else if (denominatorIsConstOne)
            {
                calculator = numeratorCalculator;
            }
            else if (originIsConstZero && numeratorIsConst & denominatorIsConst)
            {
                calculator = new Value_Calculator(numerator / denominator);
            }
            else if (originIsConst && numeratorIsConst && denominatorIsConst)
            {
                double value = (numerator - origin) / denominator + origin;
                calculator = new Value_Calculator(value);
            }
            else if (originIsConstZero && numeratorIsConst && !denominatorIsConst)
            {
                calculator = new Divide_WithoutOrigin_WithConstNumerator_Calculator(numerator, denominatorCalculator);
            }
            else if (originIsConstZero && !numeratorIsConst && denominatorIsConst)
            {
                calculator = new Divide_WithoutOrigin_WithConstDenominator_Calculator(numeratorCalculator, denominator);
            }
            else if (originIsConstZero && !numeratorIsConst && !denominatorIsConst)
            {
                calculator = new Divide_WithoutOrigin_Calculator(numeratorCalculator, denominatorCalculator);
            }
            else if (originIsConst && numeratorIsConst && !denominatorIsConst)
            {
                calculator = new Divide_WithConstOrigin_AndNumerator_Calculator(numerator, denominatorCalculator, origin);
            }
            else if (originIsConst && !numeratorIsConst && denominatorIsConst)
            {
                calculator = new Divide_WithConstOrigin_AndDenominator_Calculator(numeratorCalculator, denominator, origin);
            }
            else if (originIsConst && !numeratorIsConst && !denominatorIsConst)
            {
                calculator = new Divide_WithConstOrigin_Calculator(numeratorCalculator, denominatorCalculator, origin);
            }
            else if (!originIsConst && numeratorIsConst && denominatorIsConst)
            {
                calculator = new Divide_WithOrigin_AndConstNumerator_AndDenominator_Calculator(numerator, denominator, originCalculator);
            }
            else if (!originIsConst && numeratorIsConst && !denominatorIsConst)
            {
                calculator = new Divide_WithOrigin_AndConstNumerator_Calculator(numerator, denominatorCalculator, originCalculator);
            }
            else if (!originIsConst && !numeratorIsConst && denominatorIsConst)
            {
                calculator = new Divide_WithOrigin_AndConstDenominator_Calculator(numeratorCalculator, denominator, originCalculator);
            }
            else
            {
                calculator = new Divide_WithOrigin_Calculator(numeratorCalculator, denominatorCalculator, originCalculator);
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

            baseCalculator = baseCalculator ?? new Value_Calculator(0);
            exponentCalculator = exponentCalculator ?? new Value_Calculator(0);
            double @base = baseCalculator.Calculate(0, 0);
            double exponent = exponentCalculator.Calculate(0, 0);
            bool baseIsConst = baseCalculator is Value_Calculator;
            bool exponentIsConst = exponentCalculator is Value_Calculator;
            bool baseIsConstZero = baseIsConst && @base == 0;
            bool exponentIsConstZero = exponentIsConst && exponent == 0;

            if (baseIsConstZero)
            {
                calculator = new Value_Calculator(0);
            }
            else if (exponentIsConstZero)
            {
                calculator = baseCalculator;
            }
            else if (baseIsConst && exponentIsConst)
            {
                calculator = new Value_Calculator(Math.Pow(@base, exponent));
            }
            else if (baseIsConst)
            {
                calculator = new Power_WithConstBase_Calculator(@base, exponentCalculator);
            }
            else if (exponentIsConst)
            {
                calculator = new Power_WithConstExponent_Calculator(baseCalculator, exponent);
            }
            else
            {
                calculator = new Power_Calculator(baseCalculator, exponentCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitTimeAdd(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeDifferenceCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Value_Calculator(0);
            timeDifferenceCalculator = timeDifferenceCalculator ?? new Value_Calculator(0);
            double signal = signalCalculator.Calculate(0, 0);
            double timeDifference = timeDifferenceCalculator.Calculate(0, 0);
            bool signalIsConst = signalCalculator is Value_Calculator;
            bool timeDifferenceIsConst = timeDifferenceCalculator is Value_Calculator;
            bool signalIsConstZero = signalIsConst && signal == 0;
            bool timeDifferenceIsConstZero = timeDifferenceIsConst && timeDifference == 0;

            if (signalIsConstZero)
            {
                calculator = new Value_Calculator(0);
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
                calculator = new TimeAdd_WithConstTimeDifference_Calculator(signalCalculator, timeDifference);
            }
            else
            {
                calculator = new TimeAdd_Calculator(signalCalculator, timeDifferenceCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitTimeSubstract(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeDifferenceCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Value_Calculator(0);
            timeDifferenceCalculator = timeDifferenceCalculator ?? new Value_Calculator(0);
            double signal = signalCalculator.Calculate(0, 0);
            double timeDifference = timeDifferenceCalculator.Calculate(0, 0);
            bool signalIsConst = signalCalculator is Value_Calculator;
            bool timeDifferenceIsConst = timeDifferenceCalculator is Value_Calculator;
            bool signalIsConstZero = signalIsConst && signal == 0;
            bool timeDifferenceIsConstZero = timeDifferenceIsConst && signal == 0;

            if (signalIsConstZero)
            {
                calculator = new Value_Calculator(0);
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
                calculator = new TimeSubstract_WithConstTimeDifference_Calculator(signalCalculator, timeDifference);
            }
            else
            {
                calculator = new TimeSubstract_Calculator(signalCalculator, timeDifferenceCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitTimeMultiply(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeMultiplierCalculator = _stack.Pop();
            OperatorCalculatorBase originCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Value_Calculator(0);
            timeMultiplierCalculator = timeMultiplierCalculator ?? new Value_Calculator(1);
            originCalculator = originCalculator ?? new Value_Calculator(0);

            double signal = signalCalculator.Calculate(0, 0);
            double timeMultiplier = timeMultiplierCalculator.Calculate(0, 0);
            double origin = originCalculator.Calculate(0, 0);
            bool signalIsConst = signalCalculator is Value_Calculator;
            bool timeMultiplierIsConst = timeMultiplierCalculator is Value_Calculator;
            bool originIsConst = originCalculator is Value_Calculator;
            bool signalIsConstZero = signalCalculator is Value_Calculator;
            bool timeMultiplierIsConstZero = timeMultiplierIsConst && timeMultiplier == 0;
            bool originIsConstZero = originIsConst && origin == 0;
            bool timeMultiplierIsConstOne = timeMultiplierIsConst && timeMultiplier == 0;

            if (timeMultiplierIsConstZero)
            {
                // Weird number
                calculator = new Value_Calculator(0);
            }
            else if (signalIsConstZero)
            {
                calculator = new Value_Calculator(0);
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
                calculator = new TimeMultiply_WithoutOrigin_WithConstTimeMultiplier_Calculator(signalCalculator, timeMultiplier);
            }
            else if (originIsConstZero && !timeMultiplierIsConst)
            {
                calculator = new TimeMultiply_WithoutOrigin_Calculator(signalCalculator, timeMultiplierCalculator);
            }
            else if (timeMultiplierIsConst)
            {
                calculator = new TimeMultiply_WithOrigin_WithConstTimeMultiplier_Calculator(signalCalculator, timeMultiplier, originCalculator);
            }
            else if (originIsConst)
            {
                calculator = new TimeMultiply_WithConstOrigin_Calculator(signalCalculator, timeMultiplierCalculator, origin);
            }
            else
            {
                calculator = new TimeMultiply_WithOrigin_Calculator(signalCalculator, timeMultiplierCalculator, originCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitTimeDivide(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeDividerCalculator = _stack.Pop();
            OperatorCalculatorBase originCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Value_Calculator(0);
            timeDividerCalculator = timeDividerCalculator ?? new Value_Calculator(1);
            originCalculator = originCalculator ?? new Value_Calculator(0);

            double signal = signalCalculator.Calculate(0, 0);
            double timeDivider = timeDividerCalculator.Calculate(0, 0);
            double origin = originCalculator.Calculate(0, 0);
            bool signalIsConst = signalCalculator is Value_Calculator;
            bool timeDividerIsConst = timeDividerCalculator is Value_Calculator;
            bool originIsConst = originCalculator is Value_Calculator;
            bool signalIsConstZero = signalIsConst && signal == 0;
            bool timeDividerIsConstZero = timeDividerIsConst && timeDivider == 0;
            bool originIsConstZero = originIsConst && origin == 0;
            bool timeDividerIsConstOne = timeDividerIsConst && timeDivider == 1;

            if (timeDividerIsConstZero)
            {
                calculator = new Value_Calculator(0);
            }
            else if (signalIsConstZero)
            {
                calculator = new Value_Calculator(0);
            }
            else if (signalIsConst)
            {
                calculator = signalCalculator;
            }
            else if (originIsConstZero && timeDividerIsConst)
            {
                calculator = new TimeDivide_WithoutOrigin_WithConstTimeDivider_Calculator(signalCalculator, timeDivider);
            }
            else if (originIsConstZero && !timeDividerIsConst)
            {
                calculator = new TimeDivide_WithoutOrigin_Calculator(signalCalculator, timeDividerCalculator);
            }
            else if (timeDividerIsConst)
            {
                calculator = new TimeDivide_WithOrigin_WithConstTimeDivider_Calculator(signalCalculator, timeDivider, originCalculator);
            }
            else if (originIsConst)
            {
                calculator = new TimeDivide_WithConstOrigin_Calculator(signalCalculator, timeDividerCalculator, origin);
            }
            else
            {
                calculator = new TimeDivide_WithOrigin_Calculator(signalCalculator, timeDividerCalculator, originCalculator);
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

            signalCalculator = signalCalculator ?? new Value_Calculator(0);
            exponentCalculator = exponentCalculator ?? new Value_Calculator(0);
            originCalculator = originCalculator ?? new Value_Calculator(0);

            double signal = signalCalculator.Calculate(0, 0);
            double exponent = exponentCalculator.Calculate(0, 0);
            double origin = originCalculator.Calculate(0, 0);
            bool signalIsConst = signalCalculator is Value_Calculator;
            bool exponentIsConst = exponentCalculator is Value_Calculator;
            bool originIsConst = originCalculator is Value_Calculator;
            bool signalIsConstZero = signalIsConst && signal == 0;
            bool exponentIsConstZero = exponentIsConst && exponent == 0;
            bool originIsConstZero = originIsConst && origin == 0;
            bool exponentIsConstOne = exponentIsConst && exponent == 1;

            if (signalIsConstZero)
            {
                calculator = new Value_Calculator(0);
            }
            else if (exponentIsConstZero)
            {
                calculator = new Value_Calculator(1); // TODO: I cannot image this one... Look into later.
            }
            else if (exponentIsConstOne)
            {
                calculator = signalCalculator;
            }
            else if (originIsConstZero)
            {
                calculator = new TimePower_WithoutOrigin_Calculator(signalCalculator, exponentCalculator);
            }
            else
            {
                calculator = new TimePower_WithOrigin_Calculator(signalCalculator, exponentCalculator, originCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitSine(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase volumeCalculator = _stack.Pop();
            OperatorCalculatorBase pitchCalculator = _stack.Pop();
            OperatorCalculatorBase levelCalculator = _stack.Pop();
            OperatorCalculatorBase phaseStartCalculator = _stack.Pop();

            volumeCalculator = volumeCalculator ?? new Value_Calculator(0);
            pitchCalculator = pitchCalculator ?? new Value_Calculator(0);
            levelCalculator = levelCalculator ?? new Value_Calculator(0);
            phaseStartCalculator = phaseStartCalculator ?? new Value_Calculator(0);
            double volume = volumeCalculator.Calculate(0, 0);
            double pitch = pitchCalculator.Calculate(0, 0);
            double level = levelCalculator.Calculate(0, 0);
            double phaseStart = phaseStartCalculator.Calculate(0, 0);
            bool volumeIsConst = volumeCalculator is Value_Calculator;
            bool pitchIsConst = pitchCalculator is Value_Calculator;
            bool levelIsConst = levelCalculator is Value_Calculator;
            bool phaseStartIsConst = phaseStartCalculator is Value_Calculator;
            bool volumeIsConstZero = volumeIsConst && volume == 0;
            bool pitchIsConstZero = pitchIsConst && pitch == 0;
            bool levelIsConstZero = levelIsConst && level == 0;
            bool phaseStartIsConstZero = phaseStartIsConst && phaseStart % (Math.PI * 2) == 0; // TODO: Precision problem in the comparison to 0?
            bool volumeIsConstOne = volumeIsConst && volume == 1; // Not used yet, but could be used for optimization too.

            if (volumeIsConstZero)
            {
                calculator = levelCalculator;
            }
            else if (pitchIsConstZero)
            {
                // Weird number
                calculator = levelCalculator;
            }
            else if (levelIsConstZero && phaseStartIsConstZero)
            {
                calculator = new Sine_Calculator(volumeCalculator, pitchCalculator);
            }
            else if (levelIsConstZero && !phaseStartIsConstZero)
            {
                calculator = new Sine_WithPhaseStart_Calculator(volumeCalculator, pitchCalculator, phaseStartCalculator);
            }
            else if (!levelIsConstZero && phaseStartIsConstZero)
            {
                calculator = new Sine_WithLevel_Calculator(volumeCalculator, pitchCalculator, levelCalculator);
            }
            else
            {
                calculator = new Sine_WithLevel_AndPhaseStart_Calculator(volumeCalculator, pitchCalculator, levelCalculator, phaseStartCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitCurveIn(Operator op)
        {
            OperatorCalculatorBase calculator;

            var wrapper = new CurveInWrapper(op, _curveRepository);

            if (wrapper.Curve == null)
            {
                calculator = new Value_Calculator(0);
            }
            else
            {
                calculator = new CurveIn_Calculator(wrapper.Curve);
            }

            _stack.Push(calculator);
        }

        protected override void VisitSampleOperator(Operator op)
        {
            OperatorCalculatorBase calculator;

            var wrapper = new SampleOperatorWrapper(op, _sampleRepository);

            Sample sample = wrapper.Sample;
            if (sample == null)
            {
                calculator = new Value_Calculator(0);
            }
            else
            {
                int sampleChannelCount = sample.GetChannelCount();
                if (sampleChannelCount == _channelCount)
                {
                    calculator = new SampleOperator_Calculator(sample);
                }
                else if (sampleChannelCount == 1 && _channelCount == 2)
                {
                    calculator = new SampleOperator_MonoToStereo_Calculator(sample);
                }
                else if (sampleChannelCount == 2 && _channelCount == 1)
                {
                    calculator = new SampleOperator_StereoToMono_Calculator(sample);
                }
                else
                {
                    throw new Exception(String.Format("Combination of sampleChannelCount '{0}' and _channelCount '{1}' not supported.", sampleChannelCount, _channelCount));
                }
            }

            _stack.Push(calculator);
        }

        protected override void VisitPatchInlet(Operator op)
        {
            // Do nothing. Not relevant until Patch is programmed.
        }

        protected override void VisitPatchOutlet(Operator op)
        {
            // Do nothing. Not relevant until Patch is programmed.
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
    }
}
