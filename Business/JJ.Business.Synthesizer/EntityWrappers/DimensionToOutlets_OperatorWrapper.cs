using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Resources;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class DimensionToOutlets_OperatorWrapper : OperatorWrapperBase_VariableOutletCount
    {
        private const int OPERAND_INDEX = 0;

        public DimensionToOutlets_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Operand
        {
            get { return Inlet.InputOutlet; }
            set { Inlet.LinkTo(value); }
        }

        public Inlet Inlet => OperatorHelper.GetInlet(WrappedOperator, OPERAND_INDEX);

        public override string GetInletDisplayName(int listIndex)
        {
            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

            string name = ResourceHelper.GetPropertyDisplayName(() => Operand);
            return name;
        }
    }
}