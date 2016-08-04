using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class PulseTrigger_OperatorWrapper : OperatorWrapperBase
    {
        private const int PASS_THROUGH_INDEX = 0;
        private const int RESET_INDEX = 1;
        private const int RESULT_INDEX = 0;

        public PulseTrigger_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet PassThrough
        {
            get { return PassThroughInlet.InputOutlet; }
            set { PassThroughInlet.LinkTo(value); }
        }

        public Inlet PassThroughInlet => OperatorHelper.GetInlet(WrappedOperator, PASS_THROUGH_INDEX);

        public Outlet Reset
        {
            get { return ResetInlet.InputOutlet; }
            set { ResetInlet.LinkTo(value); }
        }

        public Inlet ResetInlet => OperatorHelper.GetInlet(WrappedOperator, RESET_INDEX);

        public Outlet Result => OperatorHelper.GetOutlet(WrappedOperator, RESULT_INDEX);

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case PASS_THROUGH_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => PassThrough);
                        return name;
                    }

                case RESET_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Reset);
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

        public static implicit operator Outlet(PulseTrigger_OperatorWrapper wrapper) => wrapper?.Result;
    }
}