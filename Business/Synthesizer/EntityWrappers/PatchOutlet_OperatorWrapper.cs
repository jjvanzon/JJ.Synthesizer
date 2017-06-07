using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class PatchOutlet_OperatorWrapper : OperatorWrapperBase_WithOneOutlet
    {
        private const int INPUT_INDEX = 0;
        private const int OUTLET_INDEX = 0;

        public PatchOutlet_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Input
        {
            get => Inlet.InputOutlet;
            set => Inlet.LinkTo(value);
        }

        public Inlet Inlet => InletOutletSelector.GetInlet(WrappedOperator, INPUT_INDEX);

        public Outlet Outlet => InletOutletSelector.GetOutlet(WrappedOperator, OUTLET_INDEX);

        public int? ListIndex
        {
            get => DataPropertyParser.TryGetInt32(WrappedOperator, nameof(ListIndex));
            set => DataPropertyParser.SetValue(WrappedOperator, nameof(ListIndex), value);
        }

        public Dimension Dimension
        {
            get => Outlet.Dimension;
            set => Outlet.LinkTo(value);
        }

        public DimensionEnum DimensionEnum => Outlet.GetDimensionEnum();
        public void SetDimensionEnum(DimensionEnum enumValue, IDimensionRepository repository) => Outlet.SetDimensionEnum(enumValue, repository);

        public override string GetInletDisplayName(Inlet inlet) => ResourceFormatter.Input;
    }
}
