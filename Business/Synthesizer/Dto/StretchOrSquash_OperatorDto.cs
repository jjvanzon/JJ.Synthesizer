using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class StretchOrSquash_OperatorDto : OperatorDtoBase
    {
        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase FactorOperatorDto { get; set; }
        public OperatorDtoBase OriginOperatorDto { get; set; }
        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, FactorOperatorDto, OriginOperatorDto }; }
            set { SignalOperatorDto = value[0]; FactorOperatorDto = value[1]; OriginOperatorDto = value[2]; }
        }
    }
}