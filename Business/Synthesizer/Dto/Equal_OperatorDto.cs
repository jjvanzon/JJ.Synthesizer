using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Equal_OperatorDto : Equal_OperatorDto_VarA_VarB
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Equal);

        public Equal_OperatorDto(OperatorDtoBase aOperatorDto, OperatorDtoBase bOperatorDto)
            : base(aOperatorDto, bOperatorDto)
        { }
    }

    internal class Equal_OperatorDto_ConstA_ConstB : OperatorDtoBase_ConstA_ConstB
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Equal);

        public Equal_OperatorDto_ConstA_ConstB(double a, double b)
            : base(a, b)
        { }
    }

    internal class Equal_OperatorDto_ConstA_VarB : OperatorDtoBase_ConstA_VarB
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Equal);

        public Equal_OperatorDto_ConstA_VarB(double a, OperatorDtoBase bOperatorDto)
            : base(a, bOperatorDto)
        { }
    }

    internal class Equal_OperatorDto_VarA_ConstB : OperatorDtoBase_VarA_ConstB
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Equal);

        public Equal_OperatorDto_VarA_ConstB(OperatorDtoBase aOperatorDto, double b)
            : base(aOperatorDto, b)
        { }
    }

    internal class Equal_OperatorDto_VarA_VarB : OperatorDtoBase_VarA_VarB
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Equal);

        public Equal_OperatorDto_VarA_VarB(OperatorDtoBase aOperatorDto, OperatorDtoBase bOperatorDto)
            : base(aOperatorDto, bOperatorDto)
        { }
    }
}