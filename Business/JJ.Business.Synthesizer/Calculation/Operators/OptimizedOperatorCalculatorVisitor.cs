using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Calculation.Operators.Entities;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Visitors;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal partial class OptimizedOperatorCalculatorVisitor : OperatorVisitorBase
    {
        private int _channelCount;
        private Stack<OperatorCalculatorBase> _stack;
        private Dictionary<int, OperatorCalculatorBase> _dictionary = new Dictionary<int, OperatorCalculatorBase>();

        public IList<OperatorCalculatorBase> Execute(IList<Outlet> channelOutlets)
        {
            if (channelOutlets == null) throw new NullException(() => channelOutlets);

            foreach (Outlet channelOutlet in channelOutlets)
            {
                IValidator validator = new RecursiveOperatorValidator(channelOutlet.Operator);
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
            double value = op.AsValueOperator.Value;

            var calculator = new Value_Calculator(value);
            _stack.Push(calculator);
        }

        protected override void VisitAdd(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase operandACalculator = _stack.Pop();
            OperatorCalculatorBase operandBCalculator = _stack.Pop();

            if (operandACalculator == null && 
                operandBCalculator == null)
            {
                calculator = null;
            }
            else if (operandACalculator == null)
            {
                calculator = operandBCalculator;
            }
            else if (operandBCalculator == null)
            {
                calculator = operandACalculator;
            }
            else
            {
                double a = operandACalculator.Calculate(0, 0);
                double b = operandBCalculator.Calculate(0, 0);
                bool operandAIsConst = operandACalculator is Value_Calculator;
                bool operandBIsConst = operandBCalculator is Value_Calculator;

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
            }

            _stack.Push(calculator);
        }

        protected override void VisitSubstract(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase operandACalculator = _stack.Pop();
            OperatorCalculatorBase operandBCalculator = _stack.Pop();

            if (operandACalculator == null && operandBCalculator == null)
            {
                calculator = null;
            }
            else if (operandBCalculator == null)
            {
                calculator = operandACalculator;
            }
            else
            {
                operandACalculator = operandACalculator ?? new Value_Calculator(0);

                // TODO: Try something
                //operandBCalculator = operandBCalculator ?? new Value_Calculator(0);

                double a = operandACalculator.Calculate(0, 0);
                double b = operandBCalculator.Calculate(0, 0);
                bool operandAIsConst = operandACalculator is Value_Calculator;
                bool operandBIsConst = operandBCalculator is Value_Calculator;

                if (operandAIsConst && operandBIsConst)
                {
                    calculator = new Value_Calculator(a - b);
                }
                else if (operandAIsConst)
                {
                    calculator = new Substract_WithConst_OperandA_Calculator(a, operandBCalculator);
                }
                else if (operandBIsConst)
                {
                    calculator = new Substract_WithConst_OperandB_Calculator(operandACalculator, b);
                }
                else
                {
                    calculator = new Substract_Calculator(operandACalculator, operandBCalculator);
                }
            }

            _stack.Push(calculator);
        }

        protected override void VisitMultiply(Operator op)
        {
            OperatorCalculatorBase operandACalculator = _stack.Pop();
            OperatorCalculatorBase operandBCalculator = _stack.Pop();
            OperatorCalculatorBase originCalculator = _stack.Pop();

            if (originCalculator == null)
            {
                VisitMultiplyWithoutOrigin(operandACalculator, operandBCalculator);
            }
            else
            {
                VisitMultiplyWithOrigin(operandACalculator, operandBCalculator, originCalculator);
            }
        }

        private void VisitMultiplyWithoutOrigin(
            OperatorCalculatorBase operandACalculator, 
            OperatorCalculatorBase operandBCalculator)
        {
            OperatorCalculatorBase calculator;

            if (operandACalculator == null && operandBCalculator == null)
            {
                calculator = null;
            }
            else if (operandACalculator == null)
            {
                calculator = operandBCalculator;
            }
            else if (operandBCalculator == null)
            {
                calculator = operandACalculator;
            }
            else
            {
                double a = operandACalculator.Calculate(0, 0);
                double b = operandBCalculator.Calculate(0, 0);
                bool operandAIsConst = operandACalculator is Value_Calculator;
                bool operandBIsConst = operandBCalculator is Value_Calculator;

                if (operandAIsConst && operandBIsConst)
                {
                    calculator = new Value_Calculator(a * b);
                }
                else if (operandAIsConst)
                {
                    calculator = new Multiply_WithoutOrigin_WithConstOperandA_Calculator(a, operandBCalculator);
                }
                else if (operandBIsConst)
                {
                    calculator = new Multiply_WithoutOrigin_WithConstOperandB_Calculator(operandACalculator, b);
                }
                else
                {
                    calculator = new Multiply_WithoutOrigin_Calculator(operandACalculator, operandBCalculator);
                }
            }

            _stack.Push(calculator);
        }

        private void VisitMultiplyWithOrigin(
            OperatorCalculatorBase operandACalculator,
            OperatorCalculatorBase operandBCalculator,
            OperatorCalculatorBase originCalculator)
        {
            if (originCalculator == null) throw new NullException(() => originCalculator);

            if (operandACalculator == null && 
                operandBCalculator == null)
            {
                _stack.Push(originCalculator);
            }
            else if (operandACalculator == null)
            {
                _stack.Push(operandBCalculator);
            }
            else if (operandBCalculator == null)
            {
                _stack.Push(operandACalculator);
            }
            else
            {
                double origin = originCalculator.Calculate(0, 0);
                bool originIsConst = originCalculator is Value_Calculator;

                if (originIsConst)
                {
                    VisitMultiplyWithConstOrigin(operandACalculator, operandBCalculator, origin);
                }
                else
                {
                    VisitMultiplyWithVariableOrigin(operandACalculator, operandBCalculator, originCalculator);
                }
            }
        }

        private void VisitMultiplyWithConstOrigin(
            OperatorCalculatorBase operandACalculator, 
            OperatorCalculatorBase operandBCalculator, 
            double origin)
        {
            if (operandACalculator == null) throw new NullException(() => operandACalculator);
            if (operandBCalculator == null) throw new NullException(() => operandBCalculator);

            OperatorCalculatorBase calculator;

            double a = operandACalculator.Calculate(0, 0);
            double b = operandBCalculator.Calculate(0, 0);
            bool operandAIsConst = operandACalculator is Value_Calculator;
            bool operandBIsConst = operandBCalculator is Value_Calculator;

            if (operandAIsConst && operandBIsConst)
            {
                double value = (a - origin) * b + origin;
                calculator = new Value_Calculator(value);
            }
            else if (operandAIsConst)
            {
                calculator = new Multiply_WithConstOrigin_AndOperandA_Calculator(a, operandBCalculator, origin);
            }
            else if (operandBIsConst)
            {
                calculator = new Multiply_WithConstOrigin_AndOperandB_Calculator(operandACalculator, b, origin);
            }
            else
            {
                calculator = new Multiply_WithConstOrigin_Calculator(operandACalculator, operandBCalculator, origin);
            }

            _stack.Push(calculator);
        }

        private void VisitMultiplyWithVariableOrigin(
            OperatorCalculatorBase operandACalculator, 
            OperatorCalculatorBase operandBCalculator, 
            OperatorCalculatorBase originCalculator)
        {
            if (operandACalculator == null) throw new NullException(() => operandACalculator);
            if (operandBCalculator == null) throw new NullException(() => operandBCalculator);
            if (originCalculator == null) throw new NullException(() => originCalculator);

            OperatorCalculatorBase calculator;

            double a = operandACalculator.Calculate(0, 0);
            double b = operandBCalculator.Calculate(0, 0);
            bool operandAIsConst = operandACalculator is Value_Calculator;
            bool operandBIsConst = operandBCalculator is Value_Calculator;
            
            if (operandAIsConst && operandBIsConst)
            {
                calculator = new Multiply_WithOrigin_AndConstOperandA_AndOperandB_Calculator(a, b, originCalculator);
            }
            else if (operandAIsConst)
            {
                calculator = new Multiply_WithOrigin_AndConstOperandA_Calculator(a, operandBCalculator, originCalculator);
            }
            else if (operandBIsConst)
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
            OperatorCalculatorBase numeratorCalculator = _stack.Pop();
            OperatorCalculatorBase denominatorCalculator = _stack.Pop();
            OperatorCalculatorBase originCalculator = _stack.Pop();

            if (originCalculator == null)
            {
                VisitDivideWithoutOrigin(numeratorCalculator, denominatorCalculator);
            }
            else
            {
                VisitDivideWithOrigin(numeratorCalculator, denominatorCalculator, originCalculator);
            }
        }

        private void VisitDivideWithoutOrigin(
            OperatorCalculatorBase numeratorCalculator,
            OperatorCalculatorBase denominatorCalculator)
        {
            OperatorCalculatorBase calculator;

            if (numeratorCalculator == null && 
                denominatorCalculator == null)
            {
                calculator = null;
            }
            else if (denominatorCalculator == null)
            {
                calculator = numeratorCalculator;
            }
            else
            {
                numeratorCalculator = numeratorCalculator ?? new Value_Calculator(0);

                double numerator = numeratorCalculator.Calculate(0, 0);
                double denominator = denominatorCalculator.Calculate(0, 0);
                bool numeratorIsConst = numeratorCalculator is Value_Calculator;
                bool denominatorIsConst = denominatorCalculator is Value_Calculator;

                if (denominatorIsConst && denominator == 0)
                {
                    calculator = numeratorCalculator;
                }
                if (numeratorIsConst & denominatorIsConst)
                {
                    calculator = new Value_Calculator(numerator / denominator);
                }
                else if (numeratorIsConst)
                {
                    calculator = new Divide_WithoutOrigin_WithConstNumerator_Calculator(numerator, denominatorCalculator);
                }
                else if (denominatorIsConst)
                {
                    calculator = new Divide_WithoutOrigin_WithConstDenominator_Calculator(numeratorCalculator, denominator);
                }
                else
                {
                    calculator = new Divide_WithoutOrigin_Calculator(numeratorCalculator, denominatorCalculator);
                }
            }

            _stack.Push(calculator);
        }

        private void VisitDivideWithOrigin(
            OperatorCalculatorBase numeratorCalculator,
            OperatorCalculatorBase denominatorCalculator,
            OperatorCalculatorBase originCalculator)
        {
            if (numeratorCalculator == null &&
                denominatorCalculator == null)
            {
                _stack.Push(originCalculator);
            }
            else
            {
                // TODO: This is much different than the other methods.
                numeratorCalculator = numeratorCalculator ?? new Value_Calculator(0);
                denominatorCalculator = denominatorCalculator ?? new Value_Calculator(1);

                double origin = originCalculator.Calculate(0, 0);
                bool originIsConst = originCalculator is Value_Calculator;

                if (originIsConst)
                {
                    VisitDivideWithConstOrigin(numeratorCalculator, denominatorCalculator, origin);
                }
                else
                {
                    VisitDivideWithVariableOrigin(numeratorCalculator, denominatorCalculator, originCalculator);
                }
            }
        }

        private void VisitDivideWithConstOrigin(
            OperatorCalculatorBase numeratorCalculator,
            OperatorCalculatorBase denominatorCalculator,
            double origin)
        {
            if (numeratorCalculator == null) throw new NullException(() => numeratorCalculator);
            if (denominatorCalculator == null) throw new NullException(() => denominatorCalculator);

            OperatorCalculatorBase calculator;

            double numerator = numeratorCalculator.Calculate(0, 0);
            double denominator = denominatorCalculator.Calculate(0, 0);
            bool numeratorIsConst = numeratorCalculator is Value_Calculator;
            bool denominatorIsConst = denominatorCalculator is Value_Calculator;

            if (denominatorIsConst && denominator == 0)
            {
                calculator = numeratorCalculator;
            }
            if (numeratorIsConst && denominatorIsConst)
            {
                double value = (numerator - origin) / denominator + origin;
                calculator = new Value_Calculator(value);
            }
            else if (numeratorIsConst)
            {
                calculator = new Divide_WithConstOrigin_AndNumerator_Calculator(numerator, denominatorCalculator, origin);
            }
            else if (denominatorIsConst)
            {
                calculator = new Divide_WithConstOrigin_AndDenominator_Calculator(numeratorCalculator, denominator, origin);
            }
            else
            {
                calculator = new Divide_WithConstOrigin_Calculator(numeratorCalculator, denominatorCalculator, origin);
            }

            _stack.Push(calculator);
        }

        private void VisitDivideWithVariableOrigin(
            OperatorCalculatorBase numeratorCalculator,
            OperatorCalculatorBase denominatorCalculator,
            OperatorCalculatorBase originCalculator)
        {
            if (numeratorCalculator == null) throw new NullException(() => numeratorCalculator);
            if (denominatorCalculator == null) throw new NullException(() => denominatorCalculator);
            if (originCalculator == null) throw new NullException(() => originCalculator);

            OperatorCalculatorBase calculator;

            double numerator = numeratorCalculator.Calculate(0, 0);
            double denominator = denominatorCalculator.Calculate(0, 0);
            bool numeratorIsConst = numeratorCalculator is Value_Calculator;
            bool denominatorIsConst = denominatorCalculator is Value_Calculator;

            if (numeratorIsConst && denominatorIsConst)
            {
                calculator = new Divide_WithOrigin_AndConstNumerator_AndDenominator_Calculator(numerator, denominator, originCalculator);
            }
            else if (numeratorIsConst)
            {
                calculator = new Divide_WithOrigin_AndConstNumerator_Calculator(numerator, denominatorCalculator, originCalculator);
            }
            else if (denominatorIsConst)
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

            if (baseCalculator == null &&
                exponentCalculator == null)
            {
                calculator = null;
            }
            else if (baseCalculator == null)
            {
                calculator = null;
            }
            else if (exponentCalculator == null)
            {
                calculator = baseCalculator;
            }
            else
            {
                double @base = baseCalculator.Calculate(0, 0);
                double exponent = exponentCalculator.Calculate(0, 0);
                bool baseIsConst = baseCalculator is Value_Calculator;
                bool exponentIsConst = exponentCalculator is Value_Calculator;

                if (baseIsConst && exponentIsConst)
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
            }

            _stack.Push(calculator);
        }

        protected override void VisitTimeAdd(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeDifferenceCalculator = _stack.Pop();

            if (signalCalculator == null)
            {
                calculator = null;
            }
            else if (timeDifferenceCalculator == null)
            {
                calculator = signalCalculator;
            }
            else
            {
                double timeDifference = timeDifferenceCalculator.Calculate(0, 0);
                bool signalIsConst = signalCalculator is Value_Calculator;
                bool timeDifferenceIsConst = timeDifferenceCalculator is Value_Calculator;

                if (signalIsConst)
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
            }

            _stack.Push(calculator);
        }

        protected override void VisitTimeSubstract(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeDifferenceCalculator = _stack.Pop();

            if (signalCalculator == null)
            {
                calculator = null;
            }
            else if (timeDifferenceCalculator == null)
            {
                calculator = signalCalculator;
            }
            else
            {
                double timeDifference = timeDifferenceCalculator.Calculate(0, 0);
                bool signalIsConst = signalCalculator is Value_Calculator;
                bool timeDifferenceIsConst = timeDifferenceCalculator is Value_Calculator;

                if (signalIsConst)
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
            }

            _stack.Push(calculator);
        }

        protected override void VisitTimeMultiply(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeMultiplierCalculator = _stack.Pop();
            OperatorCalculatorBase originCalculator = _stack.Pop();

            if (signalCalculator == null)
            {
                calculator = null;
            }
            else if (timeMultiplierCalculator == null)
            {
                calculator = signalCalculator;
            }
            else
            {
                double timeMultiplier = timeMultiplierCalculator.Calculate(0, 0);
                bool signalIsConst = signalCalculator is Value_Calculator;
                bool timeMultiplierIsConst = timeMultiplierCalculator is Value_Calculator;
                bool timeMultiplierIsConstZero = timeMultiplierIsConst && timeMultiplier == 0;

                if (signalIsConst)
                {
                    calculator = signalCalculator;
                }
                else if (timeMultiplierIsConstZero)
                {
                    calculator = signalCalculator;
                }
                else
                {
                    if (originCalculator != null)
                    {
                        // With Origin
                        double origin = originCalculator.Calculate(0, 0);
                        bool originIsConst = originCalculator is Value_Calculator;

                        if (timeMultiplierIsConst)
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
                    }
                    else
                    {
                        // Without origin
                        if (timeMultiplierIsConst)
                        {
                            calculator = new TimeMultiply_WithoutOrigin_WithConstTimeMultiplier_Calculator(signalCalculator, timeMultiplier);
                        }
                        else
                        {
                            calculator = new TimeMultiply_WithoutOrigin_Calculator(signalCalculator, timeMultiplierCalculator);
                        }
                    }
                }
            }

            _stack.Push(calculator);
        }

        protected override void VisitTimeDivide(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeDividerCalculator = _stack.Pop();
            OperatorCalculatorBase originCalculator = _stack.Pop();

            if (signalCalculator == null)
            {
                calculator = null;
            }
            else if (timeDividerCalculator == null)
            {
                calculator = signalCalculator;
            }
            else
            {
                double timeDivider = timeDividerCalculator.Calculate(0, 0);
                bool signalIsConst = signalCalculator is Value_Calculator;
                bool timeDividerIsConst = timeDividerCalculator is Value_Calculator;
                bool timeDividerIsConstZero = timeDividerIsConst && timeDivider == 0;

                if (signalIsConst)
                {
                    calculator = signalCalculator;
                }
                else if (timeDividerIsConstZero)
                {
                    calculator = signalCalculator;
                }
                else
                {
                    if (originCalculator != null)
                    {
                        // With Origin
                        double origin = originCalculator.Calculate(0, 0);
                        bool originIsConst = originCalculator is Value_Calculator;

                        if (timeDividerIsConst)
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
                    }
                    else
                    {
                        // Without origin
                        if (timeDividerIsConst)
                        {
                            calculator = new TimeDivide_WithoutOrigin_WithConstTimeDivider_Calculator(signalCalculator, timeDivider);
                        }
                        else
                        {
                            calculator = new TimeDivide_WithoutOrigin_Calculator(signalCalculator, timeDividerCalculator);
                        }
                    }
                }
            }
            
            _stack.Push(calculator);
        }

        protected override void VisitTimePower(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();;
            OperatorCalculatorBase exponentCalculator = _stack.Pop();
            OperatorCalculatorBase originCalculator = _stack.Pop();

            if (signalCalculator == null)
            {
                calculator = null;
            }
            else if (exponentCalculator == null)
            {
                calculator = signalCalculator;
            }
            else if (originCalculator == null)
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

            // A lot of combinations of nulls are pretty pointless to handle.
            if (volumeCalculator == null || pitchCalculator == null)
            {
                calculator = levelCalculator;
            }
            else if (levelCalculator == null && phaseStartCalculator == null)
            {
                calculator = new Sine_Calculator(volumeCalculator, pitchCalculator);
            }
            else if (levelCalculator == null && phaseStartCalculator != null)
            {
                calculator = new Sine_WithPhaseStart_Calculator(volumeCalculator, pitchCalculator, phaseStartCalculator);
            }
            else if (levelCalculator != null && phaseStartCalculator == null)
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

            if (op.AsCurveIn.Curve == null)
            {
                calculator = null;
            }
            else
            {
                calculator = new CurveIn_Calculator(op.AsCurveIn.Curve);
            }

            _stack.Push(calculator);
        }

        protected override void VisitSampleOperator(Operator op)
        {
            OperatorCalculatorBase calculator;

            if (op.AsSampleOperator.Sample == null)
            {
                calculator = null;
            }
            else
            {
                int sampleChannelCount = op.AsSampleOperator.Sample.GetChannelCount();
                if (sampleChannelCount == _channelCount)
                {
                    calculator = new SampleOperator_Calculator(op.AsSampleOperator.Sample);
                }
                else if (sampleChannelCount == 1 && _channelCount == 2)
                {
                    calculator = new SampleOperator_MonoToStereo_Calculator(op.AsSampleOperator.Sample);
                }
                else if (sampleChannelCount == 2 && _channelCount == 1)
                {
                    calculator = new SampleOperator_StereoToMono_Calculator(op.AsSampleOperator.Sample);
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
