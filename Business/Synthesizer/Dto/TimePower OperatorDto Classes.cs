﻿using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class TimePower_OperatorDto : TimePower_OperatorDto_VarOrigin
    { }

    internal class TimePower_OperatorDto_VarOrigin : OperatorDtoBase_WithDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.TimePower);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase ExponentOperatorDto { get; set; }
        public OperatorDtoBase OriginOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, ExponentOperatorDto, OriginOperatorDto }; }
            set { SignalOperatorDto = value[0]; ExponentOperatorDto = value[1]; OriginOperatorDto = value[2]; }
        }
    }

    internal class TimePower_OperatorDto_ConstOrigin : OperatorDtoBase_WithDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.TimePower);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase ExponentOperatorDto { get; set; }
        public double Origin { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, ExponentOperatorDto }; }
            set { SignalOperatorDto = value[0]; ExponentOperatorDto = value[1]; }
        }
    }
}