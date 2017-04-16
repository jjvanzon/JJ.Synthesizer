using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    /// <summary> Not abstract, so we can create the same wrapper for several types of Operator. </summary>
    public class OperatorWrapperBase_AggregateOverDimension : OperatorWrapperBase_WithResult
    {
        private const int SIGNAL_INDEX = 0;
        private const int FROM_INDEX = 1;
        private const int TILL_INDEX = 2;
        private const int STEP_INDEX = 3;

        public OperatorWrapperBase_AggregateOverDimension(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return SignalInlet.InputOutlet; }
            set { SignalInlet.LinkTo(value); }
        }

        public Inlet SignalInlet => OperatorHelper.GetInlet(WrappedOperator, SIGNAL_INDEX);

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

        public CollectionRecalculationEnum CollectionRecalculation
        {
            get { return DataPropertyParser.GetEnum<CollectionRecalculationEnum>(WrappedOperator, PropertyNames.CollectionRecalculation); }
            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.CollectionRecalculation, value); }
        }

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case SIGNAL_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => Signal);
                        return name;
                    }

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