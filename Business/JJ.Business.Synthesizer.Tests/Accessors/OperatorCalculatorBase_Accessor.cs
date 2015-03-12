using JJ.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    public class OperatorCalculatorBase_Accessor
    {
        private Accessor _accessor;

        public OperatorCalculatorBase_Accessor(object rootOperatorCalculator)
        {
            _accessor = new Accessor(rootOperatorCalculator);
        }

        //protected OperatorCalculatorBase_Accessor[] _operands
        //{
        //    get
        //    {
        //        object obj = _accessor.GetFieldValue(() => _operands);

        //        var accessor = new OperatorCalculatorBase_Accessor(obj);
        //        return accessor;
        //    }
        //}
    }
}
