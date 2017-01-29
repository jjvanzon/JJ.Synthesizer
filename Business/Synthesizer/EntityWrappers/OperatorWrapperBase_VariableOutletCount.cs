using JJ.Data.Synthesizer;
using System.Collections.Generic;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_VariableOutletCount : OperatorWrapperBase
    {
        public OperatorWrapperBase_VariableOutletCount(Operator op)
            : base(op)
        { }

        public IList<Outlet> Results => WrappedOperator.Outlets;

        public override string GetOutletDisplayName(int listIndex)
        {
            if (listIndex < 0) throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Outlets.Count);
            if (listIndex > WrappedOperator.Outlets.Count) throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Outlets.Count);

            string name = $"{PropertyDisplayNames.Outlet} {listIndex + 1}";
            return name;
        }
    }
}