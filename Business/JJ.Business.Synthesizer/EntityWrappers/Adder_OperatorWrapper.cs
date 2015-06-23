using JJ.Business.Synthesizer.Constants;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Adder_OperatorWrapper : OperatorWrapperBase
    {
        public Adder_OperatorWrapper(Operator op)
            : base(op)
        { }

        /// <summary>
        /// Executes a loop, so prevent calling it multiple times.
        /// </summary>
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
            get { return GetOutlet(OperatorConstants.ADDER_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Adder_OperatorWrapper wrapper)
        {
            if (wrapper == null) throw new NullException(() => wrapper);
            
            return wrapper.Result;
        }
    }
}