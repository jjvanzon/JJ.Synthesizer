using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Visitors;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Patches
{
    internal partial class OptimizedPatchCalculatorVisitor : OperatorVisitorBase
    {
        private ICurveRepository _curveRepository;
        private ISampleRepository _sampleRepository;
        private IDocumentRepository _documentRepository;

        private WhiteNoiseCalculator _whiteNoiseCalculator;

        private int _channelCount;
        private Stack<OperatorCalculatorBase> _stack;
        private Dictionary<int, OperatorCalculatorBase> _dictionary = new Dictionary<int, OperatorCalculatorBase>();

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

                operatorCalculator = operatorCalculator ?? new Zero_OperatorCalculator(); 

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

        protected override void VisitValue(Operator op)
        {
            var wrapper = new Value_OperatorWrapper(op);
            double value = wrapper.Value;

            var calculator = new Value_OperatorCalculator(value);
            _stack.Push(calculator);
        }

        protected override void VisitAdd(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase operandACalculator = _stack.Pop();
            OperatorCalculatorBase operandBCalculator = _stack.Pop();

            operandACalculator = operandACalculator ?? new Value_OperatorCalculator(0);
            operandBCalculator = operandBCalculator ?? new Value_OperatorCalculator(0);

            double a = operandACalculator.Calculate(0, 0);
            double b = operandBCalculator.Calculate(0, 0);
            bool operandAIsConst = operandACalculator is Value_OperatorCalculator;
            bool operandBIsConst = operandBCalculator is Value_OperatorCalculator;
            bool operandAIsConstZero = operandAIsConst && a == 0;
            bool operandBIsConstZero = operandAIsConst && b == 0;

            if (operandAIsConstZero && operandBIsConstZero)
            {
                calculator = new Value_OperatorCalculator(0);
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
                calculator = new Value_OperatorCalculator(a + b);
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

        protected override void VisitSubstract(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase operandACalculator = _stack.Pop();
            OperatorCalculatorBase operandBCalculator = _stack.Pop();

            operandACalculator = operandACalculator ?? new Value_OperatorCalculator(0);
            operandBCalculator = operandBCalculator ?? new Value_OperatorCalculator(0);

            double a = operandACalculator.Calculate(0, 0);
            double b = operandBCalculator.Calculate(0, 0);
            bool operandAIsConst = operandACalculator is Value_OperatorCalculator;
            bool operandBIsConst = operandBCalculator is Value_OperatorCalculator;
            bool operandAIsConstZero = operandAIsConst && a == 0;
            bool operandBIsConstZero = operandBIsConst && a == 0;

            if (operandAIsConstZero && operandBIsConstZero)
            {
                calculator = new Value_OperatorCalculator(0);
            }
            else if (operandBIsConstZero)
            {
                calculator = operandACalculator;
            }
            else if (operandAIsConst && operandBIsConst)
            {
                calculator = new Value_OperatorCalculator(a - b);
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

        protected override void VisitMultiply(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase operandACalculator = _stack.Pop();
            OperatorCalculatorBase operandBCalculator = _stack.Pop();
            OperatorCalculatorBase originCalculator = _stack.Pop();

            operandACalculator = operandACalculator ?? new Value_OperatorCalculator(1);
            operandBCalculator = operandBCalculator ?? new Value_OperatorCalculator(1);
            originCalculator = originCalculator ?? new Value_OperatorCalculator(0);

            double a = operandACalculator.Calculate(0, 0);
            double b = operandBCalculator.Calculate(0, 0);
            double origin = originCalculator.Calculate(0, 0);
            bool operandAIsConst = operandACalculator is Value_OperatorCalculator;
            bool operandBIsConst = operandBCalculator is Value_OperatorCalculator;
            bool originIsConst = originCalculator is Value_OperatorCalculator;
            bool operandAIsConstZero = operandAIsConst && a == 0;
            bool operandBIsConstZero = operandBIsConst && b == 0;
            bool originIsConstZero = operandBIsConst && b == 0;
            bool operandAIsConstOne = operandAIsConst && a == 1;
            bool operandBIsConstOne = operandBIsConst && b == 1;

            if (operandAIsConstZero || operandBIsConstZero)
            {
                calculator = new Value_OperatorCalculator(0);
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
                calculator = new Value_OperatorCalculator(a * b);
            }
            else if (originIsConst && operandAIsConst && operandBIsConst)
            {
                double value = (a - origin) * b + origin;
                calculator = new Value_OperatorCalculator(value);
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

            numeratorCalculator = numeratorCalculator ?? new Value_OperatorCalculator(0);
            denominatorCalculator = denominatorCalculator ?? new Value_OperatorCalculator(1);
            originCalculator = originCalculator ?? new Value_OperatorCalculator(0);

            double numerator = numeratorCalculator.Calculate(0, 0);
            double denominator = denominatorCalculator.Calculate(0, 0);
            double origin = originCalculator.Calculate(0, 0);
            bool denominatorIsConst = denominatorCalculator is Value_OperatorCalculator;
            bool numeratorIsConst = numeratorCalculator is Value_OperatorCalculator;
            bool originIsConst = originCalculator is Value_OperatorCalculator;
            bool numeratorIsConstZero = numeratorIsConst && numerator == 0;
            bool denominatorIsConstZero = denominatorIsConst && denominator == 0;
            bool originIsConstZero = originIsConst && origin == 0;
            bool denominatorIsConstOne = denominatorIsConst && denominator == 1;

            if (denominatorIsConstZero)
            {
                // Weird number
                calculator = new Value_OperatorCalculator(0);
            }
            else if (numeratorIsConstZero)
            {
                calculator = new Value_OperatorCalculator(0);
            }
            else if (denominatorIsConstOne)
            {
                calculator = numeratorCalculator;
            }
            else if (originIsConstZero && numeratorIsConst & denominatorIsConst)
            {
                calculator = new Value_OperatorCalculator(numerator / denominator);
            }
            else if (originIsConst && numeratorIsConst && denominatorIsConst)
            {
                double value = (numerator - origin) / denominator + origin;
                calculator = new Value_OperatorCalculator(value);
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

            baseCalculator = baseCalculator ?? new Value_OperatorCalculator(0);
            exponentCalculator = exponentCalculator ?? new Value_OperatorCalculator(0);
            double @base = baseCalculator.Calculate(0, 0);
            double exponent = exponentCalculator.Calculate(0, 0);
            bool baseIsConst = baseCalculator is Value_OperatorCalculator;
            bool exponentIsConst = exponentCalculator is Value_OperatorCalculator;
            bool baseIsConstZero = baseIsConst && @base == 0;
            bool exponentIsConstZero = exponentIsConst && exponent == 0;

            if (baseIsConstZero)
            {
                calculator = new Value_OperatorCalculator(0);
            }
            else if (exponentIsConstZero)
            {
                calculator = baseCalculator;
            }
            else if (baseIsConst && exponentIsConst)
            {
                calculator = new Value_OperatorCalculator(Math.Pow(@base, exponent));
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

        protected override void VisitTimeAdd(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeDifferenceCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Value_OperatorCalculator(0);
            timeDifferenceCalculator = timeDifferenceCalculator ?? new Value_OperatorCalculator(0);
            double signal = signalCalculator.Calculate(0, 0);
            double timeDifference = timeDifferenceCalculator.Calculate(0, 0);
            bool signalIsConst = signalCalculator is Value_OperatorCalculator;
            bool timeDifferenceIsConst = timeDifferenceCalculator is Value_OperatorCalculator;
            bool signalIsConstZero = signalIsConst && signal == 0;
            bool timeDifferenceIsConstZero = timeDifferenceIsConst && timeDifference == 0;

            if (signalIsConstZero)
            {
                calculator = new Value_OperatorCalculator(0);
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
                calculator = new TimeAdd_WithConstTimeDifference_OperatorCalculator(signalCalculator, timeDifference);
            }
            else
            {
                calculator = new TimeAdd_OperatorCalculator(signalCalculator, timeDifferenceCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitTimeSubstract(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeDifferenceCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Value_OperatorCalculator(0);
            timeDifferenceCalculator = timeDifferenceCalculator ?? new Value_OperatorCalculator(0);
            double signal = signalCalculator.Calculate(0, 0);
            double timeDifference = timeDifferenceCalculator.Calculate(0, 0);
            bool signalIsConst = signalCalculator is Value_OperatorCalculator;
            bool timeDifferenceIsConst = timeDifferenceCalculator is Value_OperatorCalculator;
            bool signalIsConstZero = signalIsConst && signal == 0;
            bool timeDifferenceIsConstZero = timeDifferenceIsConst && signal == 0;

            if (signalIsConstZero)
            {
                calculator = new Value_OperatorCalculator(0);
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

        protected override void VisitTimeMultiply(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeMultiplierCalculator = _stack.Pop();
            OperatorCalculatorBase originCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Value_OperatorCalculator(0);
            timeMultiplierCalculator = timeMultiplierCalculator ?? new Value_OperatorCalculator(1);
            originCalculator = originCalculator ?? new Value_OperatorCalculator(0);

            double signal = signalCalculator.Calculate(0, 0);
            double timeMultiplier = timeMultiplierCalculator.Calculate(0, 0);
            double origin = originCalculator.Calculate(0, 0);
            bool signalIsConst = signalCalculator is Value_OperatorCalculator;
            bool timeMultiplierIsConst = timeMultiplierCalculator is Value_OperatorCalculator;
            bool originIsConst = originCalculator is Value_OperatorCalculator;
            bool signalIsConstZero = signalCalculator is Value_OperatorCalculator;
            bool timeMultiplierIsConstZero = timeMultiplierIsConst && timeMultiplier == 0;
            bool originIsConstZero = originIsConst && origin == 0;
            bool timeMultiplierIsConstOne = timeMultiplierIsConst && timeMultiplier == 0;

            if (timeMultiplierIsConstZero)
            {
                // Weird number
                calculator = new Value_OperatorCalculator(0);
            }
            else if (signalIsConstZero)
            {
                calculator = new Value_OperatorCalculator(0);
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
                calculator = new TimeMultiply_WithoutOrigin_WithConstTimeMultiplier_OperatorCalculator(signalCalculator, timeMultiplier);
            }
            else if (originIsConstZero && !timeMultiplierIsConst)
            {
                calculator = new TimeMultiply_WithoutOrigin_OperatorCalculator(signalCalculator, timeMultiplierCalculator);
            }
            else if (timeMultiplierIsConst)
            {
                calculator = new TimeMultiply_WithOrigin_WithConstTimeMultiplier_OperatorCalculator(signalCalculator, timeMultiplier, originCalculator);
            }
            else if (originIsConst)
            {
                calculator = new TimeMultiply_WithConstOrigin_OperatorCalculator(signalCalculator, timeMultiplierCalculator, origin);
            }
            else
            {
                calculator = new TimeMultiply_WithOrigin_OperatorCalculator(signalCalculator, timeMultiplierCalculator, originCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitTimeDivide(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeDividerCalculator = _stack.Pop();
            OperatorCalculatorBase originCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Value_OperatorCalculator(0);
            timeDividerCalculator = timeDividerCalculator ?? new Value_OperatorCalculator(1);
            originCalculator = originCalculator ?? new Value_OperatorCalculator(0);

            double signal = signalCalculator.Calculate(0, 0);
            double timeDivider = timeDividerCalculator.Calculate(0, 0);
            double origin = originCalculator.Calculate(0, 0);
            bool signalIsConst = signalCalculator is Value_OperatorCalculator;
            bool timeDividerIsConst = timeDividerCalculator is Value_OperatorCalculator;
            bool originIsConst = originCalculator is Value_OperatorCalculator;
            bool signalIsConstZero = signalIsConst && signal == 0;
            bool timeDividerIsConstZero = timeDividerIsConst && timeDivider == 0;
            bool originIsConstZero = originIsConst && origin == 0;
            bool timeDividerIsConstOne = timeDividerIsConst && timeDivider == 1;

            if (timeDividerIsConstZero)
            {
                calculator = new Value_OperatorCalculator(0);
            }
            else if (signalIsConstZero)
            {
                calculator = new Value_OperatorCalculator(0);
            }
            else if (signalIsConst)
            {
                calculator = signalCalculator;
            }
            else if (originIsConstZero && timeDividerIsConst)
            {
                calculator = new TimeDivide_WithoutOrigin_WithConstTimeDivider_OperatorCalculator(signalCalculator, timeDivider);
            }
            else if (originIsConstZero && !timeDividerIsConst)
            {
                calculator = new TimeDivide_WithoutOrigin_OperatorCalculator(signalCalculator, timeDividerCalculator);
            }
            else if (timeDividerIsConst)
            {
                calculator = new TimeDivide_WithOrigin_WithConstTimeDivider_OperatorCalculator(signalCalculator, timeDivider, originCalculator);
            }
            else if (originIsConst)
            {
                calculator = new TimeDivide_WithConstOrigin_OperatorCalculator(signalCalculator, timeDividerCalculator, origin);
            }
            else
            {
                calculator = new TimeDivide_WithOrigin_OperatorCalculator(signalCalculator, timeDividerCalculator, originCalculator);
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

            signalCalculator = signalCalculator ?? new Value_OperatorCalculator(0);
            exponentCalculator = exponentCalculator ?? new Value_OperatorCalculator(0);
            originCalculator = originCalculator ?? new Value_OperatorCalculator(0);

            double signal = signalCalculator.Calculate(0, 0);
            double exponent = exponentCalculator.Calculate(0, 0);
            double origin = originCalculator.Calculate(0, 0);
            bool signalIsConst = signalCalculator is Value_OperatorCalculator;
            bool exponentIsConst = exponentCalculator is Value_OperatorCalculator;
            bool originIsConst = originCalculator is Value_OperatorCalculator;
            bool signalIsConstZero = signalIsConst && signal == 0;
            bool exponentIsConstZero = exponentIsConst && exponent == 0;
            bool originIsConstZero = originIsConst && origin == 0;
            bool exponentIsConstOne = exponentIsConst && exponent == 1;

            if (signalIsConstZero)
            {
                calculator = new Value_OperatorCalculator(0);
            }
            else if (exponentIsConstZero)
            {
                calculator = new Value_OperatorCalculator(1); // TODO: I cannot image this one... Look into later.
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

        protected override void VisitSine(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase volumeCalculator = _stack.Pop();
            OperatorCalculatorBase pitchCalculator = _stack.Pop();
            OperatorCalculatorBase levelCalculator = _stack.Pop();
            OperatorCalculatorBase phaseStartCalculator = _stack.Pop();

            volumeCalculator = volumeCalculator ?? new Value_OperatorCalculator(0);
            pitchCalculator = pitchCalculator ?? new Value_OperatorCalculator(0);
            levelCalculator = levelCalculator ?? new Value_OperatorCalculator(0);
            phaseStartCalculator = phaseStartCalculator ?? new Value_OperatorCalculator(0);
            double volume = volumeCalculator.Calculate(0, 0);
            double pitch = pitchCalculator.Calculate(0, 0);
            double level = levelCalculator.Calculate(0, 0);
            double phaseStart = phaseStartCalculator.Calculate(0, 0);
            bool volumeIsConst = volumeCalculator is Value_OperatorCalculator;
            bool pitchIsConst = pitchCalculator is Value_OperatorCalculator;
            bool levelIsConst = levelCalculator is Value_OperatorCalculator;
            bool phaseStartIsConst = phaseStartCalculator is Value_OperatorCalculator;
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
                calculator = new Sine_OperatorCalculator(volumeCalculator, pitchCalculator);
            }
            else if (levelIsConstZero && !phaseStartIsConstZero)
            {
                calculator = new Sine_WithPhaseStart_OperatorCalculator(volumeCalculator, pitchCalculator, phaseStartCalculator);
            }
            else if (!levelIsConstZero && phaseStartIsConstZero)
            {
                calculator = new Sine_WithLevel_OperatorCalculator(volumeCalculator, pitchCalculator, levelCalculator);
            }
            else
            {
                calculator = new Sine_WithLevel_AndPhaseStart_OperatorCalculator(volumeCalculator, pitchCalculator, levelCalculator, phaseStartCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitCurveIn(Operator op)
        {
            OperatorCalculatorBase calculator;

            var wrapper = new CurveIn_OperatorWrapper(op, _curveRepository);

            if (wrapper.Curve == null)
            {
                calculator = new Value_OperatorCalculator(0);
            }
            else
            {
                calculator = new CurveIn_OperatorCalculator(wrapper.Curve);
            }

            _stack.Push(calculator);
        }

        protected override void VisitSampleOperator(Operator op)
        {
            OperatorCalculatorBase calculator;

            var wrapper = new Sample_OperatorWrapper(op, _sampleRepository);

            Sample sample = wrapper.Sample;
            if (sample == null)
            {
                calculator = new Value_OperatorCalculator(0);
            }
            else
            {
                int sampleChannelCount = sample.GetChannelCount();
                if (sampleChannelCount == _channelCount)
                {
                    calculator = new Sample_OperatorCalculator(sample);
                }
                else if (sampleChannelCount == 1 && _channelCount == 2)
                {
                    calculator = new Sample_MonoToStereo_OperatorCalculator(sample);
                }
                else if (sampleChannelCount == 2 && _channelCount == 1)
                {
                    calculator = new Sample_StereoToMono_OperatorCalculator(sample);
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
            // Do nothing. Not relevant until Patch reuse is programmed.
        }

        protected override void VisitPatchOutlet(Operator op)
        {
            // Do nothing. Not relevant until Patch reuse is programmed.
        }

        protected override void VisitWhiteNoise(Operator op)
        {
            var calculator = new WhiteNoise_OperatorCalculator(_whiteNoiseCalculator);
            _stack.Push(calculator);
        }

        protected override void VisitResample(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase samplingRateCalculator = _stack.Pop();

            signalCalculator = signalCalculator ?? new Value_OperatorCalculator(0);
            samplingRateCalculator = samplingRateCalculator ?? new Value_OperatorCalculator(0);

            double signal = signalCalculator.Calculate(0, 0);
            double samplingRate = samplingRateCalculator.Calculate(0, 0);
            bool signalIsConst = signalCalculator is Value_OperatorCalculator;
            bool samplingRateIsConst = samplingRateCalculator is Value_OperatorCalculator;
            bool signalIsConstZero = signalIsConst && signal == 0;
            bool samplingRateIsConstZero = samplingRateIsConst && samplingRate == 0;

            if (samplingRateIsConstZero)
            {
                // Weird number
                calculator = new Value_OperatorCalculator(0);
            }
            else if (signalIsConstZero)
            {
                calculator = new Value_OperatorCalculator(0);
            }
            else if (signalIsConst)
            {
                calculator = new Value_OperatorCalculator(signal);
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

        // TODO: Program Custom Operators in the InterpretedPatchCalculator first, before trying to do it in the optimized patch calculator.
        //protected override void VisitCustomOperator(Operator customOperator)
        //{
        //    var customOperatorWrapper = new Custom_OperatorWrapper(customOperator, _documentRepository);
        //    Document document = customOperatorWrapper.Document;

        //    if (document != null &&
        //        document.MainPatch != null)
        //    {
        //        // TODO: We do not know which outlet unless we have a VisitCustomOperatorOutlet method.
        //        Operator patchOutlet = document.MainPatch.Operators.FirstOrDefault(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.PatchOutlet);
        //        if (patchOutlet != null)
        //        {
        //            // TODO: A patch outlet really needs a manually assigned sort order.
        //            // TODO: We do not know which operand we need to use yet.
        //            // The index should already be verified by the validators.
        //            Outlet operand = customOperatorWrapper.Operands[0];

        //            //var patchOutletWrapper = new PatchOutlet_OperatorWrapper(patchOutlet);
        //            //patchOutletWrapper.Input = operand;
        //            //VisitPatchOutlet(patchOutlet);

        //            VisitOutlet(operand);
        //        }
        //    }
        //}

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
