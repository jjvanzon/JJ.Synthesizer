using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class RangeOverOutlets_Outlet_OperatorDto : RangeOverOutlets_Outlet_OperatorDto_VarFrom_VarStep
    { }

    internal class RangeOverOutlets_Outlet_OperatorDto_ZeroStep : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.RangeOverOutlets;

        public OperatorDtoBase FromOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new[] { FromOperatorDto }; }
            set { FromOperatorDto = value[0]; }
        }
    }

    internal class RangeOverOutlets_Outlet_OperatorDto_VarFrom_VarStep : OperatorDtoBase, IRangeOverOutlets_OperatorDto_WithOutletListIndex
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.RangeOverOutlets;

        public OperatorDtoBase FromOperatorDto { get; set; }
        public OperatorDtoBase StepOperatorDto { get; set; }
        public int OutletListIndex { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new[] { FromOperatorDto, StepOperatorDto }; }
            set { FromOperatorDto = value[0]; StepOperatorDto = value[1]; }
        }
    }

    internal class RangeOverOutlets_Outlet_OperatorDto_VarFrom_ConstStep : OperatorDtoBase, IRangeOverOutlets_OperatorDto_WithOutletListIndex
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.RangeOverOutlets;

        public OperatorDtoBase FromOperatorDto { get; set; }
        public double Step { get; set; }
        public int OutletListIndex { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new[] { FromOperatorDto }; }
            set { FromOperatorDto = value[0]; }
        }
    }

    internal class RangeOverOutlets_Outlet_OperatorDto_ConstFrom_VarStep : OperatorDtoBase, IRangeOverOutlets_OperatorDto_WithOutletListIndex
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.RangeOverOutlets;

        public double From { get; set; }
        public OperatorDtoBase StepOperatorDto { get; set; }
        public int OutletListIndex { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new[] { StepOperatorDto }; }
            set { StepOperatorDto = value[0]; }
        }
    }

    internal class RangeOverOutlets_Outlet_OperatorDto_ConstFrom_ConstStep : OperatorDtoBase_WithoutInputOperatorDtos, IRangeOverOutlets_OperatorDto_WithOutletListIndex
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.RangeOverOutlets;

        public double From { get; set; }
        public double Step { get; set; }
        public int OutletListIndex { get; set; }
    }

    internal interface IRangeOverOutlets_OperatorDto_WithOutletListIndex : IOperatorDto
    {
        int OutletListIndex { get; set; }
    }
}