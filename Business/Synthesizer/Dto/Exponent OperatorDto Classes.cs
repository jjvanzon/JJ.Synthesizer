using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Exponent_OperatorDto : Exponent_OperatorDto_VarLow_VarHigh_VarRatio
    { }

    internal class Exponent_OperatorDto_VarLow_VarHigh_VarRatio : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Exponent);

        public OperatorDtoBase LowOperatorDto { get; set; }
        public OperatorDtoBase HighOperatorDto { get; set; }
        public OperatorDtoBase RatioOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { LowOperatorDto, HighOperatorDto, RatioOperatorDto }; }
            set { LowOperatorDto = value[0]; HighOperatorDto = value[1]; RatioOperatorDto = value[2]; }
        }
    }

    internal class Exponent_OperatorDto_VarLow_VarHigh_ConstRatio : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Exponent);

        public OperatorDtoBase LowOperatorDto { get; set; }
        public OperatorDtoBase HighOperatorDto { get; set; }
        public double Ratio { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { LowOperatorDto, HighOperatorDto }; }
            set { LowOperatorDto = value[0]; HighOperatorDto = value[1]; }
        }
    }

    internal class Exponent_OperatorDto_VarLow_ConstHigh_VarRatio : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Exponent);

        public OperatorDtoBase LowOperatorDto { get; set; }
        public double High { get; set; }
        public OperatorDtoBase RatioOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { LowOperatorDto, RatioOperatorDto }; }
            set { LowOperatorDto = value[0]; RatioOperatorDto = value[1]; }
        }
    }

    internal class Exponent_OperatorDto_VarLow_ConstHigh_ConstRatio : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Exponent);

        public OperatorDtoBase LowOperatorDto { get; set; }
        public double High { get; set; }
        public double Ratio { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { LowOperatorDto }; }
            set { LowOperatorDto = value[0]; }
        }
    }

    internal class Exponent_OperatorDto_ConstLow_VarHigh_VarRatio : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Exponent);

        public double Low { get; set; }
        public OperatorDtoBase HighOperatorDto { get; set; }
        public OperatorDtoBase RatioOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { HighOperatorDto, RatioOperatorDto }; }
            set { HighOperatorDto = value[0]; RatioOperatorDto = value[1]; }
        }
    }

    internal class Exponent_OperatorDto_ConstLow_VarHigh_ConstRatio : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Exponent);

        public double Low { get; set; }
        public OperatorDtoBase HighOperatorDto { get; set; }
        public double Ratio { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { HighOperatorDto }; }
            set { HighOperatorDto = value[0]; }
        }
    }

    internal class Exponent_OperatorDto_ConstLow_ConstHigh_VarRatio : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Exponent);

        public double Low { get; set; }
        public double High { get; set; }
        public OperatorDtoBase RatioOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { RatioOperatorDto }; }
            set { RatioOperatorDto = value[0]; }
        }
    }

    internal class Exponent_OperatorDto_ConstLow_ConstHigh_ConstRatio : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Exponent);

        public double Low { get; set; }
        public double High { get; set; }
        public double Ratio { get; set; }
    }
}