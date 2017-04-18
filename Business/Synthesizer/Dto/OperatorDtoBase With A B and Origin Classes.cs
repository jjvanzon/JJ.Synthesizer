using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_VarA_VarB_VarOrigin : OperatorDtoBase
    {
        public IOperatorDto AOperatorDto { get; set; }
        public IOperatorDto BOperatorDto { get; set; }
        public IOperatorDto OriginOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { AOperatorDto, BOperatorDto, OriginOperatorDto };
            set { AOperatorDto = value[0]; BOperatorDto = value[1]; OriginOperatorDto = value[2]; }
        }
    }

    internal abstract class OperatorDtoBase_VarA_VarB_ZeroOrigin : OperatorDtoBase_VarA_VarB
    { }

    internal abstract class OperatorDtoBase_VarA_VarB_ConstOrigin : OperatorDtoBase_VarA_VarB
    {
        public double Origin { get; set; }
    }

    internal abstract class OperatorDtoBase_VarA_ConstB_VarOrigin : OperatorDtoBase
    {
        public IOperatorDto AOperatorDto { get; set; }
        public double B { get; set; }
        public IOperatorDto OriginOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { AOperatorDto, OriginOperatorDto };
            set { AOperatorDto = value[0]; OriginOperatorDto = value[1]; }
        }
    }

    internal abstract class OperatorDtoBase_VarA_ConstB_ZeroOrigin : OperatorDtoBase_VarA_ConstB
    { }

    internal abstract class OperatorDtoBase_VarA_ConstB_ConstOrigin : OperatorDtoBase_VarA_ConstB
    {
        public double Origin { get; set; }
    }

    internal abstract class OperatorDtoBase_ConstA_VarB_VarOrigin : OperatorDtoBase
    {
        public double A { get; set; }
        public IOperatorDto BOperatorDto { get; set; }
        public IOperatorDto OriginOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { BOperatorDto, OriginOperatorDto };
            set { BOperatorDto = value[0]; OriginOperatorDto = value[1]; }
        }
    }

    internal abstract class OperatorDtoBase_ConstA_VarB_ZeroOrigin : OperatorDtoBase_ConstA_VarB
    { }

    internal abstract class OperatorDtoBase_ConstA_VarB_ConstOrigin : OperatorDtoBase_ConstA_VarB
    {
        public double Origin { get; set; }
    }

    internal abstract class OperatorDtoBase_ConstA_ConstB_VarOrigin : OperatorDtoBase_ConstA_ConstB
    {
        public IOperatorDto OriginOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { OriginOperatorDto };
            set => OriginOperatorDto = value[0];
        }
    }

    internal abstract class OperatorDtoBase_ConstA_ConstB_ZeroOrigin : OperatorDtoBase_ConstA_ConstB
    { }

    internal abstract class OperatorDtoBase_ConstA_ConstB_ConstOrigin : OperatorDtoBase_ConstA_ConstB
    {
        public double Origin { get; set; }
    }
}
