using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Scaler_OperatorWrapper : OperatorWrapperBase_WithResult
    {
        private const int SIGNAL_INDEX = 0;
        private const int SOURCE_VALUE_A_INDEX = 1;
        private const int SOURCE_VALUE_B_INDEX = 2;
        private const int TARGET_VALUE_A_INDEX = 3;
        private const int TARGET_VALUE_B_INDEX = 4;

        public Scaler_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return SignalInlet.InputOutlet; }
            set { SignalInlet.LinkTo(value); }
        }

        public Inlet SignalInlet => OperatorHelper.GetInlet(WrappedOperator, SIGNAL_INDEX);

        public Outlet SourceValueA
        {
            get { return SourceValueAInlet.InputOutlet; }
            set { SourceValueAInlet.LinkTo(value); }
        }

        public Inlet SourceValueAInlet => OperatorHelper.GetInlet(WrappedOperator, SOURCE_VALUE_A_INDEX);

        public Outlet SourceValueB
        {
            get { return SourceValueBInlet.InputOutlet; }
            set { SourceValueBInlet.LinkTo(value); }
        }

        public Inlet SourceValueBInlet => OperatorHelper.GetInlet(WrappedOperator, SOURCE_VALUE_B_INDEX);

        public Outlet TargetValueA
        {
            get { return TargetValueAInlet.InputOutlet; }
            set { TargetValueAInlet.LinkTo(value); }
        }

        public Inlet TargetValueAInlet => OperatorHelper.GetInlet(WrappedOperator, TARGET_VALUE_A_INDEX);

        public Outlet TargetValueB
        {
            get { return TargetValueBInlet.InputOutlet; }
            set { TargetValueBInlet.LinkTo(value); }
        }

        public Inlet TargetValueBInlet => OperatorHelper.GetInlet(WrappedOperator, TARGET_VALUE_B_INDEX);

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case SIGNAL_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => Signal);
                        return name;
                    }

                case SOURCE_VALUE_A_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => SourceValueA);
                        return name;
                    }

                case SOURCE_VALUE_B_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => SourceValueB);
                        return name;
                    }

                case TARGET_VALUE_A_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => TargetValueA);
                        return name;
                    }

                case TARGET_VALUE_B_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => TargetValueB);
                        return name;
                    }

                default:
                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            }
        }
    }
}