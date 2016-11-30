using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class MultiplyWithOrigin_OperatorDto : MultiplyWithOrigin_OperatorDto_VarA_VarB_VarOrigin
    { }

    internal class MultiplyWithOrigin_OperatorDto_VarA_VarB_VarOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MultiplyWithOrigin);

        public OperatorDtoBase AOperatorDto { get; set; }
        public OperatorDtoBase BOperatorDto { get; set; }
        public OperatorDtoBase OriginOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { AOperatorDto, BOperatorDto, OriginOperatorDto }; }
            set { AOperatorDto = value[0]; BOperatorDto = value[1]; OriginOperatorDto = value[2]; }
        }
    }

    internal class MultiplyWithOrigin_OperatorDto_VarA_VarB_ZeroOrigin : OperatorDtoBase_VarA_VarB
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MultiplyWithOrigin);
    }

    internal class MultiplyWithOrigin_OperatorDto_VarA_VarB_ConstOrigin : OperatorDtoBase_VarA_VarB
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MultiplyWithOrigin);

        public double Origin { get; set; }
    }

    internal class MultiplyWithOrigin_OperatorDto_VarA_ConstB_VarOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MultiplyWithOrigin);

        public OperatorDtoBase AOperatorDto { get; set; }
        public double B { get; set; }
        public OperatorDtoBase OriginOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { AOperatorDto, OriginOperatorDto }; }
            set { AOperatorDto = value[0]; OriginOperatorDto = value[1]; }
        }
    }

    internal class MultiplyWithOrigin_OperatorDto_VarA_ConstB_ZeroOrigin : OperatorDtoBase_VarA_ConstB
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MultiplyWithOrigin);
    }

    internal class MultiplyWithOrigin_OperatorDto_VarA_ConstB_ConstOrigin : OperatorDtoBase_VarA_ConstB
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MultiplyWithOrigin);

        public double Origin { get; set; }
    }

    internal class MultiplyWithOrigin_OperatorDto_ConstA_VarB_VarOrigin : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MultiplyWithOrigin);

        public double A { get; set; }
        public OperatorDtoBase BOperatorDto { get; set; }
        public OperatorDtoBase OriginOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { BOperatorDto, OriginOperatorDto }; }
            set { BOperatorDto = value[0]; OriginOperatorDto = value[1]; }
        }
    }

    internal class MultiplyWithOrigin_OperatorDto_ConstA_VarB_ZeroOrigin : OperatorDtoBase_ConstA_VarB
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MultiplyWithOrigin);
    }

    internal class MultiplyWithOrigin_OperatorDto_ConstA_VarB_ConstOrigin : OperatorDtoBase_ConstA_VarB
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MultiplyWithOrigin);

        public double Origin { get; set; }
    }

    internal class MultiplyWithOrigin_OperatorDto_ConstA_ConstB_VarOrigin : OperatorDtoBase_ConstA_ConstB
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MultiplyWithOrigin);

        public OperatorDtoBase OriginOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { OriginOperatorDto }; }
            set { OriginOperatorDto = value[0]; }
        }
    }

    internal class MultiplyWithOrigin_OperatorDto_ConstA_ConstB_ZeroOrigin : OperatorDtoBase_ConstA_ConstB
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MultiplyWithOrigin);
    }

    internal class MultiplyWithOrigin_OperatorDto_ConstA_ConstB_ConstOrigin : OperatorDtoBase_ConstA_ConstB
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MultiplyWithOrigin);

        public double Origin { get; set; }
    }
}
