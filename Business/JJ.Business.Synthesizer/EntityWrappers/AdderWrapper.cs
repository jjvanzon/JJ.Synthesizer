using JJ.Business.Synthesizer.Constants;
using JJ.Business.Synthesizer.Validation.Entities;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class AdderWrapper : OperatorWrapperBase
    {
        public AdderWrapper(Operator op)
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

        public static implicit operator Outlet(AdderWrapper wrapper)
        {
            return wrapper.Result;
        }
    }
}