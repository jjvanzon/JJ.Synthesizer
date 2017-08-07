using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class RangeOverOutlets_Outlet_OperatorDto : RangeOverOutlets_Outlet_OperatorDto_VarFrom_VarStep
    { }

    internal class RangeOverOutlets_Outlet_OperatorDto_ZeroStep : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.RangeOverOutlets;

        public IOperatorDto FromOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { FromOperatorDto };
            set => FromOperatorDto = value[0];
        }

        public override IEnumerable<InputDto> InputDtos => new[]
        {
            new InputDto(FromOperatorDto),
            new InputDto(0)
        };
    }

    internal class RangeOverOutlets_Outlet_OperatorDto_VarFrom_VarStep : OperatorDtoBase, IRangeOverOutlets_Outlet_OperatorDto_WithOutletPosition
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.RangeOverOutlets;

        public IOperatorDto FromOperatorDto { get; set; }
        public IOperatorDto StepOperatorDto { get; set; }
        public int OutletPosition { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { FromOperatorDto, StepOperatorDto };
            set { FromOperatorDto = value[0]; StepOperatorDto = value[1]; }
        }

        public override IEnumerable<InputDto> InputDtos => new[]
        {
            new InputDto(FromOperatorDto),
            new InputDto(StepOperatorDto)
        };
    }

    internal class RangeOverOutlets_Outlet_OperatorDto_VarFrom_ConstStep : OperatorDtoBase, IRangeOverOutlets_Outlet_OperatorDto_WithOutletPosition
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.RangeOverOutlets;

        public IOperatorDto FromOperatorDto { get; set; }
        public double Step { get; set; }
        public int OutletPosition { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { FromOperatorDto };
            set => FromOperatorDto = value[0];
        }

        public override IEnumerable<InputDto> InputDtos => new[]
        {
            new InputDto(FromOperatorDto),
            new InputDto(Step)
        };
    }

    internal class RangeOverOutlets_Outlet_OperatorDto_ConstFrom_VarStep : OperatorDtoBase, IRangeOverOutlets_Outlet_OperatorDto_WithOutletPosition
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.RangeOverOutlets;

        public double From { get; set; }
        public IOperatorDto StepOperatorDto { get; set; }
        public int OutletPosition { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { StepOperatorDto };
            set => StepOperatorDto = value[0];
        }

        public override IEnumerable<InputDto> InputDtos => new[]
        {
            new InputDto(From),
            new InputDto(StepOperatorDto)
        };
    }

    internal class RangeOverOutlets_Outlet_OperatorDto_ConstFrom_ConstStep : OperatorDtoBase_WithoutInputOperatorDtos, IRangeOverOutlets_Outlet_OperatorDto_WithOutletPosition
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.RangeOverOutlets;

        public double From { get; set; }
        public double Step { get; set; }
        public int OutletPosition { get; set; }

        public override IEnumerable<InputDto> InputDtos => new[]
        {
            new InputDto(From),
            new InputDto(Step)
        };
    }

    internal interface IRangeOverOutlets_Outlet_OperatorDto_WithOutletPosition : IOperatorDto
    {
        int OutletPosition { get; set; }
    }
}