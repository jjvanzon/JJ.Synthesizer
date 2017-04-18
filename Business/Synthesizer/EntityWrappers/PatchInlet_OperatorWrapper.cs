using JetBrains.Annotations;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class PatchInlet_OperatorWrapper : OperatorWrapperBase_WithResult
    {
        private const int INPUT_INDEX = 0;

        public PatchInlet_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Input
        {
            get => Inlet.InputOutlet;
            set => Inlet.LinkTo(value);
        }

        [NotNull]
        public Inlet Inlet => OperatorHelper.GetInlet(WrappedOperator, INPUT_INDEX);

        public int? ListIndex
        {
            get => DataPropertyParser.TryGetInt32(WrappedOperator, PropertyNames.ListIndex);
            set => DataPropertyParser.SetValue(WrappedOperator, PropertyNames.ListIndex, value);
        }

        public Dimension Dimension
        {
            get => Inlet.Dimension;
            set => Inlet.LinkTo(value);
        }

        public DimensionEnum DimensionEnum => Inlet.GetDimensionEnum();
        public void SetDimensionEnum(DimensionEnum enumValue, IDimensionRepository repository) => Inlet.SetDimensionEnum(enumValue, repository);

        public double? DefaultValue
        {
            get => Inlet.DefaultValue;
            set => Inlet.DefaultValue = value;
        }

        public override string GetInletDisplayName(int listIndex)
        {
            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

            string name = ResourceFormatter.GetDisplayName(() => Input);
            return name;
        }
    }
}
