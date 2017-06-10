using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class CustomOperator_OperatorWrapper : OperatorWrapperBase
    {
        public CustomOperator_OperatorWrapper(Operator op)
            : base(op)
        {
            Operands = new CustomOperator_OperatorWrapper_Operands(op);
            Inlets = new CustomOperator_OperatorWrapper_Inlets(op);
            Outlets = new CustomOperator_OperatorWrapper_Outlets(op);
        }

        public string Name
        {
            get => WrappedOperator.Name;
            set => WrappedOperator.Name = value;
        }

        public CustomOperator_OperatorWrapper_Operands Operands { get; }

        public CustomOperator_OperatorWrapper_Inlets Inlets { get; }

        public CustomOperator_OperatorWrapper_Outlets Outlets { get; }

        public override string GetInletDisplayName(Inlet inlet)
        {
            // Use Name
            if (!string.IsNullOrEmpty(inlet.Name))
            {
                return inlet.Name;
            }

            // Use Dimension
            DimensionEnum dimensionEnum = inlet.GetDimensionEnum();
            if (dimensionEnum != DimensionEnum.Undefined)
            {
                return ResourceFormatter.GetDisplayName(dimensionEnum);
            }

            // Use List Position (not ListIndex, becuase it does not have to be consecutive).
            int listPosition = WrappedOperator.Inlets.IndexOf(inlet);
            string displayName = $"{ResourceFormatter.Inlet} {listPosition + 1}";
            return displayName;
        }

        public override string GetOutletDisplayName(Outlet outlet)
        {
            // Use Name
            if (!string.IsNullOrEmpty(outlet.Name))
            {
                return outlet.Name;
            }

            // Use Dimension
            DimensionEnum dimensionEnum = outlet.GetDimensionEnum();
            if (dimensionEnum != DimensionEnum.Undefined)
            {
                return ResourceFormatter.GetDisplayName(dimensionEnum);
            }

            // Use List Position (not ListIndex, becuase it does not have to be consecutive).
            int listPosition = WrappedOperator.Outlets.IndexOf(outlet);
            string displayName = $"{ResourceFormatter.Outlet} {listPosition + 1}";
            return displayName;

        }

        //// TODO: These operations must enfore rules and should be integrated in the members above.

        //private void SetUnderlyingPatch(Operator op, Patch patch)
        //{
        //    if (op == null) throw new NullException(() => op);
        //    if (patch == null) throw new NullException(() => patch);
        //    if (op.GetOperatorTypeEnum() != OperatorTypeEnum.CustomOperator) throw new NotEqualException(() => op.GetOperatorTypeEnum(), OperatorTypeEnum.CustomOperator);

        //    // What can go wrong? Everything.
        //    throw new NotImplementedException();
        //}

        //private void SetName(Patch patch, string name)
        //{
        //    if (patch == null) throw new NullException(() => patch);

        //    //if (patch.Name 
        //}
    }
}
