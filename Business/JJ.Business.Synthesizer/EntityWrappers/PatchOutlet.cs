using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class PatchOutlet : OperatorWrapperBase
    {
        public const int INPUT_INDEX = 0;
        public const int RESULT_INDEX = 0;

        public PatchOutlet(Operator op)
            : base(op)
        { }

        public Outlet Input
        {
            get { return _operator.Inlets[INPUT_INDEX].InputOutlet; }
            set { _operator.Inlets[INPUT_INDEX].LinkTo(value); }
        }

        public Outlet Result
        {
            get { return _operator.Outlets[RESULT_INDEX]; }
        }

        public static implicit operator Outlet(PatchOutlet wrapper)
        {
            return wrapper.Result;
        }
    }
}
