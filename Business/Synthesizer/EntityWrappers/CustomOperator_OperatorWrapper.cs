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
