using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class RangeOverOutlets_OperatorDto : RangeOverOutlets_OperatorDto_VarFrom_VarStep
    { }

    internal class RangeOverOutlets_OperatorDto_VarFrom_VarStep : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.RangeOverOutlets);

        public OperatorDtoBase FromOperatorDto { get; set; }
        public OperatorDtoBase StepOperatorDto { get; set; }
        public int OutletIndex { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { FromOperatorDto, StepOperatorDto }; }
            set { FromOperatorDto = value[0]; StepOperatorDto = value[1]; }
        }
    }

    internal class RangeOverOutlets_OperatorDto_VarFrom_ConstStep : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.RangeOverOutlets);

        public double From { get; set; }
        public OperatorDtoBase StepOperatorDto { get; set; }
        public int OutletIndex { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { StepOperatorDto }; }
            set { StepOperatorDto = value[0]; }
        }
    }

    internal class RangeOverOutlets_OperatorDto_ConstFrom_VarStep : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.RangeOverOutlets);

        public double From { get; set; }
        public OperatorDtoBase StepOperatorDto { get; set; }
        public int OutletIndex { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { StepOperatorDto }; }
            set { StepOperatorDto = value[0]; }
        }
    }

    internal class RangeOverOutlets_OperatorDto_ConstFrom_ConstStep : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.RangeOverOutlets);

        public double From { get; set; }
        public double Step { get; set; }
        public int OutletIndex { get; set; }
    }
}