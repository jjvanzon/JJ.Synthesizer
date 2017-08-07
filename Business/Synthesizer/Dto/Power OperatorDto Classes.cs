using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Power_OperatorDto : Power_OperatorDto_VarBase_VarExponent
    { }


    internal class Power_OperatorDto_VarBase_VarExponent : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Power;

        public IOperatorDto BaseOperatorDto { get; set; }
        public IOperatorDto ExponentOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { BaseOperatorDto, ExponentOperatorDto };
            set { BaseOperatorDto = value[0]; ExponentOperatorDto = value[1]; }
        }

        public override IEnumerable<InputDto> InputDtos => new[]
        {
            new InputDto(BaseOperatorDto),
            new InputDto(ExponentOperatorDto)
        };
    }

    internal class Power_OperatorDto_VarBase_ConstExponent : Power_OperatorDtoBase_VarBase
    {
        public double Exponent { get; set; }

        public override IEnumerable<InputDto> InputDtos => new[]
        {
            new InputDto(BaseOperatorDto),
            new InputDto(Exponent)
        };
    }

    internal class Power_OperatorDto_ConstBase_VarExponent : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Power;

        public double Base { get; set; }
        public IOperatorDto ExponentOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { ExponentOperatorDto };
            set => ExponentOperatorDto = value[0];
        }

        public override IEnumerable<InputDto> InputDtos => new[]
        {
            new InputDto(Base),
            new InputDto(ExponentOperatorDto)
        };
    }

    internal class Power_OperatorDto_ConstBase_ConstExponent : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Power;

        public double Base { get; set; }
        public double Exponent { get; set; }

        public override IEnumerable<InputDto> InputDtos => new[]
        {
            new InputDto(Base),
            new InputDto(Exponent)
        };
    }

    /// <summary> For Machine Optimization </summary>
    internal class Power_OperatorDto_VarBase_Exponent2 : Power_OperatorDtoBase_VarBase
    {
        public override IEnumerable<InputDto> InputDtos => new[]
        {
            new InputDto(BaseOperatorDto),
            new InputDto(2)
        };
    }

    /// <summary> For Machine Optimization </summary>
    internal class Power_OperatorDto_VarBase_Exponent3 : Power_OperatorDtoBase_VarBase
    {
        public override IEnumerable<InputDto> InputDtos => new[]
        {
            new InputDto(BaseOperatorDto),
            new InputDto(3)
        };

    }

    /// <summary> For Machine Optimization </summary>
    internal class Power_OperatorDto_VarBase_Exponent4 : Power_OperatorDtoBase_VarBase
    {
        public override IEnumerable<InputDto> InputDtos => new[]
        {
            new InputDto(BaseOperatorDto),
            new InputDto(4)
        };
    }

    /// <summary> Base class </summary>
    internal abstract class Power_OperatorDtoBase_VarBase : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Power;

        public IOperatorDto BaseOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { BaseOperatorDto };
            set => BaseOperatorDto = value[0];
        }
    }
}
