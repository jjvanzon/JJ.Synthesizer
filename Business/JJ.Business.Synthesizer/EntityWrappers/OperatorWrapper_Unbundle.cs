using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.LinkTo;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapper_Unbundle : OperatorWrapperBase
    {
        public OperatorWrapper_Unbundle(Operator op)
            : base(op)
        { }

        public Outlet Operand
        {
            get { return GetInlet(OperatorConstants.UNBUNDLE_OPERAND_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.UNBUNDLE_OPERAND_INDEX).LinkTo(value); }
        }

        public IList<Outlet> Results
        {
            get { return _operator.Outlets; }
        }
    }
}