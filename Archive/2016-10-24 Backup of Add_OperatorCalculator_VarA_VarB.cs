//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

//namespace JJ.Demos.Synthesizer.NanoOptimization.Calculation.Operators.WithStructs
//{
//    [DebuggerDisplay("{DebuggerDisplay}")]
//    internal struct Add_OperatorCalculator_VarA_VarB<TACalculator, TBCalculator>
//        : IOperatorCalculator_VarA_VarB
//        where TACalculator : IOperatorCalculator
//        where TBCalculator : IOperatorCalculator
//    {
//        private TACalculator _aCalculator;
//        public IOperatorCalculator ACalculator
//        {
//            get { return _aCalculator; }
//            set { _aCalculator = (TACalculator)value; }
//        }

//        private TBCalculator _bCalculator;
//        public IOperatorCalculator BCalculator
//        {
//            get { return _bCalculator; }
//            set { _bCalculator = (TBCalculator)value; }
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public double Calculate()
//        {
//            double a = _aCalculator.Calculate();
//            double b = _bCalculator.Calculate();

//            return OperatorCalculatorHelper.Add(a, b);
//        }

//        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
//    }
//}