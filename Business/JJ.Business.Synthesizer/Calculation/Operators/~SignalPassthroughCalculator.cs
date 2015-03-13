//using JJ.Framework.Reflection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class SignalPassthroughCalculator : OperatorCalculatorBase
//    {
//        private OperatorCalculatorBase _signalCalculator;

//        public SignalPassthroughCalculator(OperatorCalculatorBase signalCalculator)
//        {
//            if (signalCalculator == null) throw new NullException(() => signalCalculator);

//            _signalCalculator = signalCalculator;
//        }

//        public override double Calculate(double time, int channelIndex)
//        {
//            double x = _signalCalculator.Calculate(time, channelIndex);
//            return x;
//        }
//    }
//}