using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Resources;
using System;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class MakeDiscrete_OperatorWrapper : OperatorWrapperBase
    {
        private const int OPERAND_INDEX = 0;

        public MakeDiscrete_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Operand
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, OPERAND_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, OPERAND_INDEX).LinkTo(value); }
        }

        public override string GetInletDisplayName(int listIndex)
        {
            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

            string name = ResourceHelper.GetPropertyDisplayName(() => Operand);
            return name;
        }

        public override string GetOutletDisplayName(int listIndex)
        {
            if (listIndex < 0) throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Outlets.Count);
            if (listIndex > WrappedOperator.Outlets.Count) throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Outlets.Count);

            string name = String.Format("{0} {1}", PropertyDisplayNames.Outlet, listIndex + 1);
            return name;
        }

        public IList<Outlet> Results
        {
            get { return WrappedOperator.Outlets; }
        }
    }
}