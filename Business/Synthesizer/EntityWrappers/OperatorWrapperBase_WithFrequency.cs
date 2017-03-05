using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_WithFrequency : OperatorWrapperBase_WithResult
    {
        private const int FREQUENCY_INDEX = 0;

        public OperatorWrapperBase_WithFrequency(Operator op)
            : base(op)
        { }

        public Outlet Frequency
        {
            get { return FrequencyInlet.InputOutlet; }
            set { FrequencyInlet.LinkTo(value); }
        }

        public Inlet FrequencyInlet => OperatorHelper.GetInlet(WrappedOperator, FREQUENCY_INDEX);

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case FREQUENCY_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => Frequency);
                        return name;
                    }

                default:
                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            }
        }
    }
}