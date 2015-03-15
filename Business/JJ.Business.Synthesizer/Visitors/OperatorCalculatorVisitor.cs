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

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorCalculatorVisitor : OperatorVisitorBase
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

            var calculator = new ValueCalculator(value);
            _stack.Push(calculator);
        }

        protected override void VisitAdd(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase operandACalculator = _stack.Pop();
            OperatorCalculatorBase operandBCalculator = _stack.Pop();

            if (operandACalculator == null || operandBCalculator == null)
            {
                calculator = new ZeroCalculator();
            }
            else
            {
                calculator = new AddCalculator(operandACalculator, operandBCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitSubstract(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase operandACalculator = _stack.Pop();
            OperatorCalculatorBase operandBCalculator = _stack.Pop();

            if (operandACalculator == null || operandBCalculator == null)
            {
                calculator = new ZeroCalculator();
            }
            else
            {
                calculator = new SubstractCalculator(operandACalculator, operandBCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitMultiply(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase operandACalculator = _stack.Pop();
            OperatorCalculatorBase operandBCalculator = _stack.Pop();
            OperatorCalculatorBase originCalculator = _stack.Pop();

            if (originCalculator == null)
            {
                if (operandACalculator == null || operandBCalculator == null)
                {
                    calculator = new ZeroCalculator();
                }
                else
                {
                    calculator = new MultiplyWithoutOriginCalculator(operandACalculator, operandBCalculator);
                }
            }
            else
            {
                if (operandACalculator == null || operandBCalculator == null)
                {
                    calculator = originCalculator;
                }
                else
                {
                    calculator = new MultiplyWithOriginCalculator(operandACalculator, operandBCalculator, originCalculator);
                }
            }

            _stack.Push(calculator);
        }

        protected override void VisitDivide(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase numeratorCalculator = _stack.Pop();
            OperatorCalculatorBase denominatorCalculator = _stack.Pop();
            OperatorCalculatorBase originCalculator = _stack.Pop();

            // Without Origin
            if (originCalculator == null)
            {
                if (numeratorCalculator == null || denominatorCalculator == null)
                {
                    calculator = new ZeroCalculator();
                }
                else
                {
                    calculator = new DivideWithoutOriginCalculator(numeratorCalculator, denominatorCalculator);
                }
            }

            // With Origin
            else
            {
                if (numeratorCalculator == null || denominatorCalculator == null)
                {
                    calculator = originCalculator;
                }
                else
                {
                    calculator = new DivideWithOriginCalculator(numeratorCalculator, denominatorCalculator, originCalculator);
                }
            }

            _stack.Push(calculator);
        }

        protected override void VisitPower(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase baseCalculator = _stack.Pop();
            OperatorCalculatorBase exponentCalculator = _stack.Pop();

            if (baseCalculator == null || exponentCalculator == null)
            {
                calculator = new ZeroCalculator();
            }
            else
            {
                calculator = new PowerCalculator(baseCalculator, exponentCalculator);
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
                calculator = new ZeroCalculator();
            }
            else if (timeDifferenceCalculator == null)
            {
                calculator = signalCalculator;
            }
            else
            {
                calculator = new TimeAddCalculator(signalCalculator, timeDifferenceCalculator);
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
                calculator = new ZeroCalculator();
            }
            else if (timeDifferenceCalculator == null)
            {
                calculator = signalCalculator;
            }
            else
            {
                calculator = new TimeSubstractCalculator(signalCalculator, timeDifferenceCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitTimeMultiply(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase timeMultiplierCalculator = _stack.Pop();
            OperatorCalculatorBase originOutletCalculator = _stack.Pop();

            if (signalCalculator == null)
            {
                calculator = new ZeroCalculator();
            }
            else
            {
                if (timeMultiplierCalculator == null)
                {
                    calculator = signalCalculator;
                }
                else
                {
                    if (originOutletCalculator != null)
                    {
                        calculator = new TimeMultiplyWithOriginCalculator(signalCalculator, timeMultiplierCalculator, originOutletCalculator);
                    }
                    else
                    {
                        calculator = new TimeMultiplyWithoutOriginCalculator(signalCalculator, timeMultiplierCalculator);
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
            OperatorCalculatorBase originOutletCalculator = _stack.Pop();

            if (signalCalculator == null)
            {
                calculator = new ZeroCalculator();
            }
            else
            {
                if (timeDividerCalculator == null)
                {
                    calculator = signalCalculator;
                }
                else
                {
                    if (originOutletCalculator != null)
                    {
                        calculator = new TimeDivideWithOriginCalculator(signalCalculator, timeDividerCalculator, originOutletCalculator);
                    }
                    else
                    {
                        calculator = new TimeDivideWithoutOriginCalculator(signalCalculator, timeDividerCalculator);
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
                calculator = new ZeroCalculator();
            }
            else if (exponentCalculator == null)
            {
                calculator = signalCalculator;
            }
            else if (originCalculator == null)
            {
                calculator = new TimePowerWithoutOriginCalculator(signalCalculator, exponentCalculator);
            }
            else
            {
                calculator = new TimePowerWithOriginCalculator(signalCalculator, exponentCalculator, originCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitAdder(Operator op)
        {
            OperatorCalculatorBase calculator;

            List<OperatorCalculatorBase> operandCalculators = new List<OperatorCalculatorBase>();

            for (int i = 0; i < op.Inlets.Count; i++)
            {
                OperatorCalculatorBase operandCalculator =  _stack.Pop();

                if (operandCalculator != null)
                {
                    operandCalculators.Add(operandCalculator);
                }
            }

            switch (operandCalculators.Count)
            {
                case 0:
                    calculator = new ZeroCalculator();
                    break;

                case 1:
                    calculator = operandCalculators[0];
                    break;

                case 2:
                    calculator = new AddCalculator(operandCalculators[0], operandCalculators[1]);
                    break;

                case 3:
                    calculator = new AdderCalculator3(operandCalculators[0], operandCalculators[1], operandCalculators[2]);
                    break;
                    
                // TODO: Make more adder calculators for different amounts of operands to prevent a loop.

                default:
                    calculator = new AdderCalculator(operandCalculators.ToArray());
                    break;
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

            if (volumeCalculator == null || pitchCalculator == null)
            {
                calculator = new ZeroCalculator();
            }
            else if (levelCalculator == null && phaseStartCalculator == null)
            {
                calculator = new SineCalculator(volumeCalculator, pitchCalculator);
            }
            else if (levelCalculator == null && phaseStartCalculator != null)
            {
                calculator = new SineWithPhaseStartCalculator(volumeCalculator, pitchCalculator, phaseStartCalculator);
            }
            else if (levelCalculator != null && phaseStartCalculator == null)
            {
                calculator = new SineWithLevelCalculator(volumeCalculator, pitchCalculator, levelCalculator);
            }
            else
            {
                calculator = new SineWithLevelAndPhaseStartCalculator(volumeCalculator, pitchCalculator, levelCalculator, phaseStartCalculator);
            }

            _stack.Push(calculator);
        }

        protected override void VisitCurveIn(Operator op)
        {
            OperatorCalculatorBase calculator;

            if (op.AsCurveIn.Curve == null)
            {
                calculator = new ZeroCalculator();
            }
            else
            {
                calculator = new CurveInCalculator(op.AsCurveIn.Curve);
            }

            _stack.Push(calculator);
        }

        protected override void VisitSampleOperator(Operator op)
        {
            OperatorCalculatorBase calculator;

            if (op.AsSampleOperator.Sample == null)
            {
                calculator = new ZeroCalculator();
            }
            else
            {
                int sampleChannelCount = op.AsSampleOperator.Sample.GetChannelCount();
                if (sampleChannelCount == _channelCount)
                {
                    calculator = new SampleOperatorCalculator(op.AsSampleOperator.Sample);
                }
                else if (sampleChannelCount == 1 && _channelCount == 2)
                {
                    calculator = new SampleOperatorMonoToStereoCalculator(op.AsSampleOperator.Sample);
                }
                else if (sampleChannelCount == 2 && _channelCount == 1)
                {
                    calculator = new SampleOperatorStereoToMonoCalculator(op.AsSampleOperator.Sample);
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
