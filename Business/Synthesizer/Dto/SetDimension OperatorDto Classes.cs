using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SetDimension_OperatorDto : SetDimension_OperatorDto_VarValue
    { }

    internal class SetDimension_OperatorDto_VarValue : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SetDimension);

        public OperatorDtoBase PassThroughInputOperatorDto { get; set; }
        public OperatorDtoBase ValueOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { PassThroughInputOperatorDto, ValueOperatorDto }; }
            set { PassThroughInputOperatorDto = value[0]; ValueOperatorDto = value[1]; }
        }

        /// <summary> HACK: Signal operator in this case is the PassThroughInputOperatorDto. </summary>
        OperatorDtoBase IOperatorDto_VarSignal.SignalOperatorDto
        {
            get { return PassThroughInputOperatorDto; }
            set { PassThroughInputOperatorDto = value; }
        }
    }

    internal class SetDimension_OperatorDto_ConstValue : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SetDimension);

        public OperatorDtoBase PassThroughInputOperatorDto { get; set; }
        public double Value { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { PassThroughInputOperatorDto }; }
            set { PassThroughInputOperatorDto = value[0]; }
        }

        /// <summary> HACK: Signal operator in this case is the PassThroughInputOperatorDto. </summary>
        OperatorDtoBase IOperatorDto_VarSignal.SignalOperatorDto
        {
            get { return PassThroughInputOperatorDto; }
            set { PassThroughInputOperatorDto = value; }
        }
    }
}