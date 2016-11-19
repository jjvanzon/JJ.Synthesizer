using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class RangeOverDimension_OperatorDto : OperatorDtoBase_WithDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.RangeOverDimension);

        public OperatorDtoBase FromOperatorDto { get; set; }
        public OperatorDtoBase TillOperatorDto { get; set; }
        public OperatorDtoBase StepOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { FromOperatorDto, TillOperatorDto, StepOperatorDto }; }
            set { FromOperatorDto = value[0]; TillOperatorDto = value[1]; StepOperatorDto = value[2]; }
        }
    }

    internal class RangeOverDimension_OperatorCalculator_OnlyVars : RangeOverDimension_OperatorDto
    { }

    internal class RangeOverDimension_OperatorCalculator_OnlyConsts : OperatorDtoBase_WithoutInputOperatorDtos, IOperatorDto_WithDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.RangeOverDimension);

        public double From { get; set; }
        public double Till { get; set; }
        public double Step { get; set; }
        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }
    }
}