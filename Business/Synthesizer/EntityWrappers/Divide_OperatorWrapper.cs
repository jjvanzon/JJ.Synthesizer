using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Divide_OperatorWrapper : OperatorWrapperBase_WithResult
    {
        private const int NUMERATOR_INDEX = 0;
        private const int DENOMINATOR_INDEX = 1;
        private const int ORIGIN_INDEX = 2;

        public Divide_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Numerator
        {
            get { return NumeratorInlet.InputOutlet; }
            set { NumeratorInlet.LinkTo(value); }
        }

        public Inlet NumeratorInlet => OperatorHelper.GetInlet(WrappedOperator, NUMERATOR_INDEX);

        public Outlet Denominator
        {
            get { return DenomimatorInlet.InputOutlet; }
            set { DenomimatorInlet.LinkTo(value); }
        }

        public Inlet DenomimatorInlet => OperatorHelper.GetInlet(WrappedOperator, DENOMINATOR_INDEX);

        public Outlet Origin
        {
            get { return OriginInlet.InputOutlet; }
            set { OriginInlet.LinkTo(value); }
        }

        public Inlet OriginInlet => OperatorHelper.GetInlet(WrappedOperator, ORIGIN_INDEX);

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case NUMERATOR_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Numerator);
                        return name;
                    }

                case DENOMINATOR_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Denominator);
                        return name;
                    }

                case ORIGIN_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Origin);
                        return name;
                    }

                default:
                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            }
        }
    }
}