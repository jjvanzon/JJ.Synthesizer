using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using System;
using System.Globalization;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Number_OperatorWrapper : OperatorWrapperBase
    {
        private const int RESULT_INDEX = 0;

        public Number_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Result => OperatorHelper.GetOutlet(WrappedOperator, RESULT_INDEX);

        public double Number
        {
            get { return DataPropertyParser.GetDouble(WrappedOperator, PropertyNames.Number); }
            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.Number, value); }
        }

        public override string GetInletDisplayName(int listIndex)
        {
            throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
        }

        public override string GetOutletDisplayName(int listIndex)
        {
            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

            string name = ResourceHelper.GetPropertyDisplayName(() => Result);
            return name;
        }

        public static implicit operator Outlet(Number_OperatorWrapper wrapper) => wrapper?.Result;

        public static implicit operator double(Number_OperatorWrapper wrapper)
        {
            if (wrapper == null) throw new NullException(() => wrapper);

            return wrapper.Number;
        }
    }
}
