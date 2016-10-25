//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

//namespace JJ.Demos.Synthesizer.NanoOptimization.Calculation.Operators.WithStructs
//{
//    [DebuggerDisplay("{DebuggerDisplay}")]
//    internal struct Add_OperatorCalculator_8Vars
//        <
//            TChildCalculator1,
//            TChildCalculator2,
//            TChildCalculator3,
//            TChildCalculator4,
//            TChildCalculator5,
//            TChildCalculator6,
//            TChildCalculator7,
//            TChildCalculator8
//        > : IOperatorCalculator_8Vars
//        where TChildCalculator1 : IOperatorCalculator
//        where TChildCalculator2 : IOperatorCalculator
//        where TChildCalculator3 : IOperatorCalculator
//        where TChildCalculator4 : IOperatorCalculator
//        where TChildCalculator5 : IOperatorCalculator
//        where TChildCalculator6 : IOperatorCalculator
//        where TChildCalculator7 : IOperatorCalculator
//        where TChildCalculator8 : IOperatorCalculator
//    {
//        private TChildCalculator1 _calculator1;
//        public IOperatorCalculator Calculator1
//        {
//            get { return _calculator1; }
//            set { _calculator1 = (TChildCalculator1)value; }
//        }

//        private TChildCalculator2 _calculator2;
//        public IOperatorCalculator Calculator2
//        {
//            get { return _calculator2; }
//            set { _calculator2 = (TChildCalculator2)value; }
//        }

//        private TChildCalculator3 _calculator3;
//        public IOperatorCalculator Calculator3
//        {
//            get { return _calculator3; }
//            set { _calculator3 = (TChildCalculator3)value; }
//        }

//        private TChildCalculator4 _calculator4;
//        public IOperatorCalculator Calculator4
//        {
//            get { return _calculator4; }
//            set { _calculator4 = (TChildCalculator4)value; }
//        }

//        private TChildCalculator5 _calculator5;
//        public IOperatorCalculator Calculator5
//        {
//            get { return _calculator5; }
//            set { _calculator5 = (TChildCalculator5)value; }
//        }

//        private TChildCalculator6 _calculator6;
//        public IOperatorCalculator Calculator6
//        {
//            get { return _calculator6; }
//            set { _calculator6 = (TChildCalculator6)value; }
//        }

//        private TChildCalculator7 _calculator7;
//        public IOperatorCalculator Calculator7
//        {
//            get { return _calculator7; }
//            set { _calculator7 = (TChildCalculator7)value; }
//        }

//        private TChildCalculator8 _calculator8;
//        public IOperatorCalculator Calculator8
//        {
//            get { return _calculator8; }
//            set { _calculator8 = (TChildCalculator8)value; }
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public double Calculate()
//        {
//            double value1 = _calculator1.Calculate();
//            double value2 = _calculator2.Calculate();
//            double value3 = _calculator3.Calculate();
//            double value4 = _calculator4.Calculate();
//            double value5 = _calculator5.Calculate();
//            double value6 = _calculator6.Calculate();
//            double value7 = _calculator7.Calculate();
//            double value8 = _calculator8.Calculate();

//            return value1 + 
//                   value2 + 
//                   value3 + 
//                   value4 + 
//                   value5 + 
//                   value6 + 
//                   value7 + 
//                   value8;
//        }

//        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
//    }
//}
