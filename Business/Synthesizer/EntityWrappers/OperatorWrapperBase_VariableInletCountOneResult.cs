using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using System.Collections.Generic;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_VariableInletCountOneResult : OperatorWrapperBase_WithResult
    {
        public OperatorWrapperBase_VariableInletCountOneResult(Operator op)
            : base(op)
        { }

        /// <summary> Executes a loop, so prevent calling it multiple times. </summary>
        public IList<Outlet> Operands => OperatorHelper.GetSortedInputOutlets(WrappedOperator);

        /// <summary> Executes a loop, so prevent calling it multiple times. </summary>
        public IList<Inlet> Inlets => OperatorHelper.GetSortedInlets(WrappedOperator);

        public override string GetInletDisplayName(int listIndex)
        {
            if (listIndex < 0) throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            if (listIndex > WrappedOperator.Inlets.Count) throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);

            string name = $"{ResourceFormatter.Inlet} {listIndex + 1}";
            return name;
        }
    }
}