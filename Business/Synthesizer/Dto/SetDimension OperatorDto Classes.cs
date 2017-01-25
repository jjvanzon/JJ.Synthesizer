using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SetDimension_OperatorDto : SetDimension_OperatorDto_VarPassThrough_VarValue
    { }

    internal class SetDimension_OperatorDto_VarPassThrough_VarValue : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SetDimension;

        public OperatorDtoBase PassThroughInputOperatorDto { get; set; }
        public OperatorDtoBase ValueOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { PassThroughInputOperatorDto, ValueOperatorDto }; }
            set { PassThroughInputOperatorDto = value[0]; ValueOperatorDto = value[1]; }
        }

        public OperatorDtoBase SignalOperatorDto
        {
            get { return PassThroughInputOperatorDto; }
            set { PassThroughInputOperatorDto = value; }
        }
    }

    internal class SetDimension_OperatorDto_VarPassThrough_ConstValue : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SetDimension;

        public OperatorDtoBase PassThroughInputOperatorDto { get; set; }
        public double Value { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { PassThroughInputOperatorDto }; }
            set { PassThroughInputOperatorDto = value[0]; }
        }

        public OperatorDtoBase SignalOperatorDto
        {
            get { return PassThroughInputOperatorDto; }
            set { PassThroughInputOperatorDto = value; }
        }
    }

    internal class SetDimension_OperatorDto_ConstPassThrough_VarValue : OperatorDtoBase_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SetDimension;

        public double PassThrough { get; set; }
        public OperatorDtoBase ValueOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { ValueOperatorDto }; }
            set { ValueOperatorDto = value[0]; }
        }
    }

    internal class SetDimension_OperatorDto_ConstPassThrough_ConstValue : OperatorDtoBase_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SetDimension;

        public double PassThrough { get; set; }
        public double Value { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos { get; set; } = new OperatorDtoBase[0];
    }
}
