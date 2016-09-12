using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class RangeOverOutlets_OperatorCalculator_ConstFrom_VarStep : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _from;
        private readonly OperatorCalculatorBase _stepCalculator;
        private readonly double _position;

        public RangeOverOutlets_OperatorCalculator_ConstFrom_VarStep(
            double from,
            OperatorCalculatorBase stepCalculator,
            double position)
            : base(new OperatorCalculatorBase[] { stepCalculator })
        {
            _from = from;
            _stepCalculator = stepCalculator;
            _position = position;
        }

        public override double Calculate()
        {
            double step = _stepCalculator.Calculate();

            double result = _from + step * _position;

            return result;
        }
    }

    internal class RangeOverOutlets_OperatorCalculator_VarFrom_ConstStep : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _fromCalculator;
        private readonly double _stepTimesPosition;

        public RangeOverOutlets_OperatorCalculator_VarFrom_ConstStep(
            OperatorCalculatorBase fromCalculator,
            double step,
            double position)
            : base(new OperatorCalculatorBase[] { fromCalculator })
        {
            _fromCalculator = fromCalculator;

            _stepTimesPosition = step * position;
        }

        public override double Calculate()
        {
            double from = _fromCalculator.Calculate();

            double result = from + _stepTimesPosition;

            return result;
        }
    }

    internal class RangeOverOutlets_OperatorCalculator_VarFrom_VarStep : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _fromCalculator;
        private readonly OperatorCalculatorBase _stepCalculator;
        private readonly double _position;

        public RangeOverOutlets_OperatorCalculator_VarFrom_VarStep(
            OperatorCalculatorBase fromCalculator, 
            OperatorCalculatorBase stepCalculator, 
            double position)
            : base(new OperatorCalculatorBase[] { fromCalculator, stepCalculator })
        {
            _fromCalculator = fromCalculator;
            _stepCalculator = stepCalculator;
            _position = position;
        }

        public override double Calculate()
        {
            double from = _fromCalculator.Calculate();
            double step = _stepCalculator.Calculate();

            double result = from + step * _position;

            return result;
        }
    }
}
