using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class If_OperatorWrapper : OperatorWrapperBase_WithResultOutlet
    {
        public If_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Condition
        {
            get => ConditionInlet.InputOutlet;
            set => ConditionInlet.LinkTo(value);
        }

        public Inlet ConditionInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.Condition);

        public Outlet Then
        {
            get => ThenInlet.InputOutlet;
            set => ThenInlet.LinkTo(value);
        }

        public Inlet ThenInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.Then);

        public Outlet Else
        {
            get => ElseInlet.InputOutlet;
            set => ElseInlet.LinkTo(value);
        }

        public Inlet ElseInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.Else);
    }
}