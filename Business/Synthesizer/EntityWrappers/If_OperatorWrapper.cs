using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class If_OperatorWrapper : OperatorWrapperBase_WithResult
    {
        private const int CONDITION_INDEX = 0;
        private const int THEN_INDEX = 1;
        private const int ELSE_INDEX = 2;

        public If_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Condition
        {
            get { return ConditionInlet.InputOutlet; }
            set { ConditionInlet.LinkTo(value); }
        }

        public Inlet ConditionInlet => OperatorHelper.GetInlet(WrappedOperator, CONDITION_INDEX);

        public Outlet Then
        {
            get { return ThenInlet.InputOutlet; }
            set { ThenInlet.LinkTo(value); }
        }

        public Inlet ThenInlet => OperatorHelper.GetInlet(WrappedOperator, THEN_INDEX);

        public Outlet Else
        {
            get { return ElseInlet.InputOutlet; }
            set { ElseInlet.LinkTo(value); }
        }

        public Inlet ElseInlet => OperatorHelper.GetInlet(WrappedOperator, ELSE_INDEX);

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case CONDITION_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => Condition);
                        return name;
                    }

                case THEN_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => Then);
                        return name;
                    }

                case ELSE_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => Else);
                        return name;
                    }

                default:
                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            }
        }
    }
}