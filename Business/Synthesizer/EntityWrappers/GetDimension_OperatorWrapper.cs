using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Exceptions;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class GetDimension_OperatorWrapper : OperatorWrapperBase_WithSingleOutlet
    {
        private const int VALUE_INDEX = 0;

        public GetDimension_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Value => OperatorHelper.GetOutlet(WrappedOperator, VALUE_INDEX);

        public override string GetInletDisplayName(int listIndex)
        {
            throw new Exception("This operator does not have Inlets, so you cannot get an InletDisplayName.");
        }

        public override string GetOutletDisplayName(int listIndex)
        {
            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

            string name = ResourceHelper.GetPropertyDisplayName(() => Value);
            return name;
        }
    }
}