using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Power_OperatorDto : Power_OperatorDto_VarBase_VarExponent
    { }


    internal class Power_OperatorDto_VarBase_VarExponent : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Power);

        public OperatorDtoBase BaseOperatorDto { get; set; }
        public OperatorDtoBase ExponentOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { BaseOperatorDto, ExponentOperatorDto }; }
            set { BaseOperatorDto = value[0]; ExponentOperatorDto = value[1]; }
        }
    }

    internal class Power_OperatorDto_VarBase_ConstExponent : Power_OperatorDtoBase_VarBase
    {
        public double Exponent { get; set; }
    }

    internal class Power_OperatorDto_ConstBase_VarExponent : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Power);

        public double Base { get; set; }
        public OperatorDtoBase ExponentOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { ExponentOperatorDto }; }
            set { ExponentOperatorDto = value[0]; }
        }
    }

    internal class Power_OperatorDto_ConstBase_ConstExponent : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Power);

        public double Base { get; set; }
        public double Exponent { get; set; }
    }

    /// <summary> For Machine Optimization </summary>
    internal class Power_OperatorDto_VarBase_Exponent2 : Power_OperatorDtoBase_VarBase
    { }

    /// <summary> For Machine Optimization </summary>
    internal class Power_OperatorDto_VarBase_Exponent3 : Power_OperatorDtoBase_VarBase
    { }

    /// <summary> For Machine Optimization </summary>
    internal class Power_OperatorDto_VarBase_Exponent4 : Power_OperatorDtoBase_VarBase
    { }

    /// <summary> Base class </summary>
    internal abstract class Power_OperatorDtoBase_VarBase : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Power);

        public OperatorDtoBase BaseOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { BaseOperatorDto }; }
            set { BaseOperatorDto = value[0]; }
        }
    }
}
