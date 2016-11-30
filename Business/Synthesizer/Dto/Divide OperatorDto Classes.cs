using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Divide_OperatorDto : Divide_OperatorDto_VarA_VarB_VarOrigin
    { }

    internal class Divide_OperatorDto_VarA_VarB_VarOrigin : OperatorDtoBase_VarA_VarB_VarOrigin
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);
    }

    internal class Divide_OperatorDto_VarA_VarB_ZeroOrigin : OperatorDtoBase_VarA_VarB_ZeroOrigin
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);
    }

    internal class Divide_OperatorDto_VarA_VarB_ConstOrigin : OperatorDtoBase_VarA_VarB_ConstOrigin
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);
    }

    internal class Divide_OperatorDto_VarA_ConstB_VarOrigin : OperatorDtoBase_VarA_ConstB_VarOrigin
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);
    }

    internal class Divide_OperatorDto_VarA_ConstB_ZeroOrigin : OperatorDtoBase_VarA_ConstB_ZeroOrigin
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);
    }

    internal class Divide_OperatorDto_VarA_ConstB_ConstOrigin : OperatorDtoBase_VarA_ConstB_ConstOrigin
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);
    }

    internal class Divide_OperatorDto_ConstA_VarB_VarOrigin : OperatorDtoBase_ConstA_VarB_VarOrigin
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);
    }

    internal class Divide_OperatorDto_ConstA_VarB_ZeroOrigin : OperatorDtoBase_ConstA_VarB_ZeroOrigin
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);
    }

    internal class Divide_OperatorDto_ConstA_VarB_ConstOrigin : OperatorDtoBase_ConstA_VarB_ConstOrigin
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);
    }

    internal class Divide_OperatorDto_ConstA_ConstB_VarOrigin : OperatorDtoBase_ConstA_ConstB_VarOrigin
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);
    }

    internal class Divide_OperatorDto_ConstA_ConstB_ZeroOrigin : OperatorDtoBase_ConstA_ConstB_ZeroOrigin
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);
    }

    internal class Divide_OperatorDto_ConstA_ConstB_ConstOrigin : OperatorDtoBase_ConstA_ConstB_ConstOrigin
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Divide);
    }
}
