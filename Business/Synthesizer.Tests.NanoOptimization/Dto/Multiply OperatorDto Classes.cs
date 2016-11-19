using JJ.Business.Synthesizer.Tests.NanoOptimization.Helpers;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Dto
{
    internal class Multiply_OperatorDto : OperatorDtoBase_VarA_VarB
    {
        public override string OperatorTypeName => OperatorNames.Multiply;
    }

    internal class Multiply_OperatorDto_ConstA_ConstB : OperatorDtoBase_ConstA_ConstB
    {
        public override string OperatorTypeName => OperatorNames.Multiply;
    }

    internal class Multiply_OperatorDto_ConstA_VarB : OperatorDtoBase_ConstA_VarB
    {
        public override string OperatorTypeName => OperatorNames.Multiply;
    }

    internal class Multiply_OperatorDto_VarA_ConstB : OperatorDtoBase_VarA_ConstB
    {
        public override string OperatorTypeName => OperatorNames.Multiply;
    }

    internal class Multiply_OperatorDto_VarA_VarB : OperatorDtoBase_VarA_VarB
    {
        public override string OperatorTypeName => OperatorNames.Multiply;
    }
}
