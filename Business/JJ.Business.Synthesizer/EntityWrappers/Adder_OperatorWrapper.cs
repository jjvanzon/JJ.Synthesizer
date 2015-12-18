using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Adder_OperatorWrapper : OperatorWrapperBase
    {
        public Adder_OperatorWrapper(Operator op)
            : base(op)
        { }

        /// <summary> Executes a loop, so prevent calling it multiple times. summary>
        public IList<Outlet> Operands
        {
            get
            {
                IList<Outlet> operands = new Outlet[_wrappedOperator.Inlets.Count];
                for (int i = 0; i < _wrappedOperator.Inlets.Count; i++)
                {
                    operands[i] = _wrappedOperator.Inlets[i].InputOutlet;
                }
                return operands;
            }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.ADDER_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Adder_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;
            
            return wrapper.Result;
        }
    }
}