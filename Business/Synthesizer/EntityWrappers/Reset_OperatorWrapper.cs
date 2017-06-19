using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Reset_OperatorWrapper : OperatorWrapperBase_WithOneOutlet
    {
        public Reset_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet PassThroughInput
        {
            get => PassThroughInlet.InputOutlet;
            set => PassThroughInlet.LinkTo(value);
        }

        public Inlet PassThroughInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.PassThrough);

        public Outlet PassThroughOutlet => InletOutletSelector.GetOutlet(WrappedOperator, DimensionEnum.PassThrough);

        public int? Position
        {
            get => DataPropertyParser.TryGetInt32(WrappedOperator, nameof(Position));
            set => DataPropertyParser.SetValue(WrappedOperator, nameof(Position), value);
        }
    }
}