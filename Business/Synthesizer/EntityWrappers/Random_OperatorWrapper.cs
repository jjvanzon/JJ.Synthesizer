using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Random_OperatorWrapper : OperatorWrapperBase_WithResult
    {
        private const int RATE_INDEX = 0;
        
        public Random_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Rate
        {
            get { return RateInlet.InputOutlet; }
            set { RateInlet.LinkTo(value); }
        }

        public Inlet RateInlet => OperatorHelper.GetInlet(WrappedOperator, RATE_INDEX);

        public ResampleInterpolationTypeEnum InterpolationType
        {
            get { return DataPropertyParser.GetEnum<ResampleInterpolationTypeEnum>(WrappedOperator, PropertyNames.InterpolationType); }
            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.InterpolationType, value); }
        }

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case RATE_INDEX:
                    {
                        string name = ResourceFormatter.GetText(() => Rate);
                        return name;
                    }

                default:
                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            }
        }
    }
}
