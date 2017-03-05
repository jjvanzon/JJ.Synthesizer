using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class RangeOverDimension_OperatorWrapper : OperatorWrapperBase_WithResult
    {
        private const int FROM_INDEX = 0;
        private const int TILL_INDEX = 1;
        private const int STEP_INDEX = 2;

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

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case FROM_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => From);
                        return name;
                    }

                case TILL_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => Till);
                        return name;
                    }

                case STEP_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => Step);
                        return name;
                    }

                default:
                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            }
        }
    }
}