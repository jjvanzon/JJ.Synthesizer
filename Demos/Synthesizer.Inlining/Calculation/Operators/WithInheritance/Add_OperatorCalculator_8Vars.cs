using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.NanoOptimization.Calculation.Operators.WithInheritance
{
    internal class Add_OperatorCalculator_8Vars : OperatorCalculatorBase
    {
        private readonly OperatorCalculatorBase _calculator1;
        private readonly OperatorCalculatorBase _calculator2;
        private readonly OperatorCalculatorBase _calculator3;
        private readonly OperatorCalculatorBase _calculator4;
        private readonly OperatorCalculatorBase _calculator5;
        private readonly OperatorCalculatorBase _calculator6;
        private readonly OperatorCalculatorBase _calculator7;
        private readonly OperatorCalculatorBase _calculator8;

        public Add_OperatorCalculator_8Vars(
            OperatorCalculatorBase calculator1,
            OperatorCalculatorBase calculator2,
            OperatorCalculatorBase calculator3,
            OperatorCalculatorBase calculator4,
            OperatorCalculatorBase calculator5,
            OperatorCalculatorBase calculator6,
            OperatorCalculatorBase calculator7,
            OperatorCalculatorBase calculator8) 
        {
            if (calculator1 == null) throw new NullException(() => calculator1);
            if (calculator2 == null) throw new NullException(() => calculator2);
            if (calculator3 == null) throw new NullException(() => calculator3);
            if (calculator4 == null) throw new NullException(() => calculator4);
            if (calculator5 == null) throw new NullException(() => calculator5);
            if (calculator6 == null) throw new NullException(() => calculator6);
            if (calculator7 == null) throw new NullException(() => calculator7);
            if (calculator8 == null) throw new NullException(() => calculator8);

            _calculator1 = calculator1;
            _calculator2 = calculator2;
            _calculator3 = calculator3;
            _calculator4 = calculator4;
            _calculator5 = calculator5;
            _calculator6 = calculator6;
            _calculator7 = calculator7;
            _calculator8 = calculator8;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double value1 = _calculator1.Calculate();
            double value2 = _calculator2.Calculate();
            double value3 = _calculator3.Calculate();
            double value4 = _calculator4.Calculate();
            double value5 = _calculator5.Calculate();
            double value6 = _calculator6.Calculate();
            double value7 = _calculator7.Calculate();
            double value8 = _calculator8.Calculate();

            return value1 +
                   value2 +
                   value3 +
                   value4 +
                   value5 +
                   value6 +
                   value7 +
                   value8;
        }
    }
}
