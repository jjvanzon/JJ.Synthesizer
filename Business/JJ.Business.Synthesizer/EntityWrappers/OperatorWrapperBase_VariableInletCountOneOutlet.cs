using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using System.Collections.Generic;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Resources;
using System;
using System.Linq;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_VariableInletCountOneOutlet : OperatorWrapperBase
    {
        private const int RESULT_INDEX = 0;

        public OperatorWrapperBase_VariableInletCountOneOutlet(Operator op)
            : base(op)
        { }

        /// <summary> Executes a loop, so prevent calling it multiple times. summary>
        public IList<Outlet> Operands => OperatorHelper.GetSortedInputOutlets(WrappedOperator);

        /// <summary> Executes a loop, so prevent calling it multiple times. summary>
        public IList<Inlet> Inlets => OperatorHelper.GetSortedInlets(WrappedOperator);

        public Outlet Result => OperatorHelper.GetOutlet(WrappedOperator, RESULT_INDEX);

        public override string GetInletDisplayName(int listIndex)
        {
            if (listIndex < 0) throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            if (listIndex > WrappedOperator.Inlets.Count) throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);

            string name = String.Format("{0} {1}", PropertyDisplayNames.Inlet, listIndex + 1);
            return name;
        }

        public override string GetOutletDisplayName(int listIndex)
        {
            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

            string name = ResourceHelper.GetPropertyDisplayName(() => Result);
            return name;
        }
    }
}