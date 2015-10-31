using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapper_Bundle : OperatorWrapperBase
    {
        public OperatorWrapper_Bundle(Operator op)
            : base(op)
        { }

        /// <summary> Executes a loop, so prevent calling it multiple times. summary>
        public IList<Outlet> Operands
        {
            get
            {
                IList<Outlet> operands = new Outlet[_operator.Inlets.Count];
                for (int i = 0; i < _operator.Inlets.Count; i++)
                {
                    operands[i] = _operator.Inlets[i].InputOutlet;
                }
                return operands;
            }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.BUNDLE_RESULT_INDEX); }
        }

        public static implicit operator Outlet(OperatorWrapper_Bundle wrapper)
        {
            if (wrapper == null) return null;
            
            return wrapper.Result;
        }
    }
}