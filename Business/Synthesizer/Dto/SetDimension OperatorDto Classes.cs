using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SetDimension_OperatorDto : SetDimension_OperatorDto_VarPassThrough_VarNumber
    { }

    internal class SetDimension_OperatorDto_VarPassThrough_VarNumber : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SetDimension;

        public IOperatorDto PassThroughInputOperatorDto { get; set; }
        public IOperatorDto NumberOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { PassThroughInputOperatorDto, NumberOperatorDto };
            set { PassThroughInputOperatorDto = value[0]; NumberOperatorDto = value[1]; }
        }

        public IOperatorDto SignalOperatorDto
        {
            get => PassThroughInputOperatorDto;
            set => PassThroughInputOperatorDto = value;
        }
    }

    internal class SetDimension_OperatorDto_VarPassThrough_ConstNumber : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SetDimension;

        public IOperatorDto PassThroughInputOperatorDto { get; set; }
        public double Number { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { PassThroughInputOperatorDto };
            set => PassThroughInputOperatorDto = value[0];
        }

        public IOperatorDto SignalOperatorDto
        {
            get => PassThroughInputOperatorDto;
            set => PassThroughInputOperatorDto = value;
        }
    }

    internal class SetDimension_OperatorDto_ConstPassThrough_VarNumber : OperatorDtoBase_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SetDimension;

        public double PassThrough { get; set; }
        public IOperatorDto NumberOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { NumberOperatorDto };
            set => NumberOperatorDto = value[0];
        }
    }

    internal class SetDimension_OperatorDto_ConstPassThrough_ConstNumber : OperatorDtoBase_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SetDimension;

        public double PassThrough { get; set; }
        public double Number { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos { get; set; } = new IOperatorDto[0];
    }
}
