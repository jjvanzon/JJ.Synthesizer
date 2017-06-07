using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_WithFrequency : OperatorWrapperBase_WithNumberOutlet
    {
        public OperatorWrapperBase_WithFrequency(Operator op)
            : base(op)
        { }

        public Outlet Frequency
        {
            get => FrequencyInlet.InputOutlet;
            set => FrequencyInlet.LinkTo(value);
        }

        public Inlet FrequencyInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Frequency);
    }
}