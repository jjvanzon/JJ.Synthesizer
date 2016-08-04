using System;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Random_OperatorWrapper : OperatorWrapperBase
    {
        private const int RATE_INDEX = 0;
        private const int PHASE_SHIFT_INDEX = 1;
        private const int RESULT_INDEX = 0;

        public Random_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Rate
        {
            get { return RateInlet.InputOutlet; }
            set { RateInlet.LinkTo(value); }
        }

        public Inlet RateInlet
        {
            get { return OperatorHelper.GetInlet(WrappedOperator, RATE_INDEX); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(WrappedOperator, RESULT_INDEX); }
        }

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
                        string name = ResourceHelper.GetPropertyDisplayName(() => Rate);
                        return name;
                    }

                default:
                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            }
        }

        public override string GetOutletDisplayName(int listIndex)
        {
            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

            string name = ResourceHelper.GetPropertyDisplayName(() => Result);
            return name;
        }

        public static implicit operator Outlet(Random_OperatorWrapper wrapper) => wrapper?.Result;
    }
}
