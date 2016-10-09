using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Demos.Synthesizer.Inlining.WithGenericMutableStructsAndHelpers
{
    internal struct Add_OperatorCalculator_8Vars
        <
            TChildCalculator1, 
            TChildCalculator2, 
            TChildCalculator3, 
            TChildCalculator4, 
            TChildCalculator5, 
            TChildCalculator6,
            TChildCalculator7,
            TChildCalculator8
        > : IOperatorCalculator
        where TChildCalculator1 : IOperatorCalculator
        where TChildCalculator2 : IOperatorCalculator
        where TChildCalculator3 : IOperatorCalculator
        where TChildCalculator4 : IOperatorCalculator
        where TChildCalculator5 : IOperatorCalculator
        where TChildCalculator6 : IOperatorCalculator
        where TChildCalculator7 : IOperatorCalculator
        where TChildCalculator8 : IOperatorCalculator
    {
        public TChildCalculator1 _calculator1;
        public TChildCalculator2 _calculator2;
        public TChildCalculator3 _calculator3;
        public TChildCalculator4 _calculator4;
        public TChildCalculator5 _calculator5;
        public TChildCalculator6 _calculator6;
        public TChildCalculator7 _calculator7;
        public TChildCalculator8 _calculator8;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate()
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
