using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class PatchOutlet_OperatorWrapper : OperatorWrapperBase_WithResult
    {
        private const int INPUT_INDEX = 0;

        public PatchOutlet_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Input
        {
            get { return Inlet.InputOutlet; }
            set { Inlet.LinkTo(value); }
        }

        public Inlet Inlet => OperatorHelper.GetInlet(WrappedOperator, INPUT_INDEX);

        public int? ListIndex
        {
            get { return DataPropertyParser.TryGetInt32(WrappedOperator, PropertyNames.ListIndex); }
            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.ListIndex, value); }
        }

        public Dimension Dimension
        {
            get { return Result.Dimension; }
            set { Result.LinkTo(value); }
        }

        public DimensionEnum DimensionEnum => Result.GetDimensionEnum();
        public void SetDimensionEnum(DimensionEnum enumValue, IDimensionRepository repository) => Result.SetDimensionEnum(enumValue, repository);

        public override string GetInletDisplayName(int listIndex)
        {
            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

            string name = ResourceFormatter.GetDisplayName(() => Input);
            return name;
        }
    }
}
