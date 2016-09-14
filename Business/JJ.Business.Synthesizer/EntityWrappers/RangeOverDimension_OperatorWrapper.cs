using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class RangeOverDimension_OperatorWrapper : OperatorWrapperBase
    {
        private const int FROM_INDEX = 0;
        private const int TILL_INDEX = 1;
        private const int STEP_INDEX = 2;
        private const int RESULT_INDEX = 0;

        public RangeOverDimension_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet From
        {
            get { return FromInlet.InputOutlet; }
            set { FromInlet.LinkTo(value); }
        }

        public Inlet FromInlet => OperatorHelper.GetInlet(WrappedOperator, FROM_INDEX);

        public Outlet Till
        {
            get { return TillInlet.InputOutlet; }
            set { TillInlet.LinkTo(value); }
        }

        public Inlet TillInlet => OperatorHelper.GetInlet(WrappedOperator, TILL_INDEX);

        public Outlet Step
        {
            get { return StepInlet.InputOutlet; }
            set { StepInlet.LinkTo(value); }
        }

        public Inlet StepInlet => OperatorHelper.GetInlet(WrappedOperator, STEP_INDEX);

        public Outlet Result => OperatorHelper.GetOutlet(WrappedOperator, RESULT_INDEX);

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case FROM_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => From);
                        return name;
                    }

                case TILL_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Till);
                        return name;
                    }

                case STEP_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Step);
                        return name;
                    }

                default:
                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            }
        }

        public override string GetOutletDisplayName(int listIndex)
        {
            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

            string name = ResourceHelper.GetPropertyDisplayName(() => Result);
            return name;
        }

        public static implicit operator Outlet(RangeOverDimension_OperatorWrapper wrapper) => wrapper?.Result;
    }
}