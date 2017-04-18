using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class RangeOverDimension_OperatorDto : OperatorDtoBase_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.RangeOverDimension;

        public IOperatorDto FromOperatorDto { get; set; }
        public IOperatorDto TillOperatorDto { get; set; }
        public IOperatorDto StepOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { FromOperatorDto, TillOperatorDto, StepOperatorDto };
            set { FromOperatorDto = value[0]; TillOperatorDto = value[1]; StepOperatorDto = value[2]; }
        }
    }

    internal class RangeOverDimension_OperatorDto_OnlyVars : RangeOverDimension_OperatorDto
    { }

    internal class RangeOverDimension_OperatorDto_OnlyConsts : OperatorDtoBase_WithoutInputOperatorDtos, IOperatorDto_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.RangeOverDimension;

        public double From { get; set; }
        public double Till { get; set; }
        public double Step { get; set; }
        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CanonicalCustomDimensionName { get; set; }
        public int DimensionStackLevel { get; set; }
    }

    /// <summary> For Machine Optimization </summary>
    internal class RangeOverDimension_OperatorDto_WithConsts_AndStepOne : OperatorDtoBase_WithoutInputOperatorDtos, IOperatorDto_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.RangeOverDimension;

        public double From { get; set; }
        public double Till { get; set; }
        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CanonicalCustomDimensionName { get; set; }
        public int DimensionStackLevel { get; set; }
    }
}