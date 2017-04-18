using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Pulse_OperatorWrapper : OperatorWrapperBase_WithResult
    {
        private const int FREQUENCY_INDEX = 0;
        private const int WIDTH_INDEX = 1;

        public Pulse_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Frequency
        {
            get => FrequencyInlet.InputOutlet;
            set => FrequencyInlet.LinkTo(value);
        }

        public Inlet FrequencyInlet => OperatorHelper.GetInlet(WrappedOperator, FREQUENCY_INDEX);

        public Outlet Width
        {
            get => WidthInlet.InputOutlet;
            set => WidthInlet.LinkTo(value);
        }

        public Inlet WidthInlet => OperatorHelper.GetInlet(WrappedOperator, WIDTH_INDEX);

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case FREQUENCY_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => Frequency);
                        return name;
                    }

                case WIDTH_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => Width);
                        return name;
                    }

                default:
                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            }
        }
   }
}