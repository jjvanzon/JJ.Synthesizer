using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Round_OperatorWrapper : OperatorWrapperBase_WithResult
    {
        private const int SIGNAL_INDEX = 0;
        private const int STEP_INDEX = 1;
        private const int OFFSET_INDEX = 2;

        public Round_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get => SignalInlet.InputOutlet;
            set => SignalInlet.LinkTo(value);
        }

        public Inlet SignalInlet => OperatorHelper.GetInlet(WrappedOperator, SIGNAL_INDEX);

        public Outlet Step
        {
            get => StepInlet.InputOutlet;
            set => StepInlet.LinkTo(value);
        }

        public Inlet StepInlet => OperatorHelper.GetInlet(WrappedOperator, STEP_INDEX);

        public Outlet Offset
        {
            get => OffsetInlet.InputOutlet;
            set => OffsetInlet.LinkTo(value);
        }

        public Inlet OffsetInlet => OperatorHelper.GetInlet(WrappedOperator, OFFSET_INDEX);

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case SIGNAL_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => Signal);
                        return name;
                    }

                case STEP_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => Step);
                        return name;
                    }

                case OFFSET_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => Offset);
                        return name;
                    }

                default:
                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            }
        }
   }
}