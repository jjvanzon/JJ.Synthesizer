//using JJ.Framework.Reflection;
//using JJ.Business.Synthesizer.Calculation.Operators;
//using JJ.Framework.Reflection.Exceptions;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace JJ.Business.Synthesizer.Tests.Accessors
//{
//    /// <summary>
//    /// Currently not used.
//    /// </summary>
//    public class OperatorCalculatorNew_Accessor
//    {
//        private Accessor _accessor;

//        public OperatorCalculatorNew_Accessor(OptimizedOperatorCalculator operatorCalculatorNew)
//        {
//            _accessor = new Accessor(operatorCalculatorNew);
//        }

//        public object[] _rootOperatorCalculators
//        {
//            get
//            {
//                object[] objs = _accessor.GetFieldValue(() => _rootOperatorCalculators);
//                var accessors = new OperatorCalculatorBase_Accessor[objs.Length];

//                for (int i = 0; i < objs.Length; i++)
//                {
//                    object obj = objs[i];
//                    var accessor2 = new OperatorCalculatorBase_Accessor(obj);
//                    accessors[i] = accessor2;
//                }

//                return accessors;
//            }
//        }
//    }
//}
