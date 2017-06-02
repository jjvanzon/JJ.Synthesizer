using System.Collections.Generic;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_VariableOutletCount : OperatorWrapperBase
    {
        public OperatorWrapperBase_VariableOutletCount(Operator op)
            : base(op)
        { }

        public IList<Outlet> Results => WrappedOperator.Outlets;

        public override string GetOutletDisplayName(Outlet outlet)
        {
            if (outlet == null) throw new NullException(() => outlet);

            string name = $"{ResourceFormatter.Outlet} {outlet.ListIndex + 1}";
            return name;
        }
    }
}