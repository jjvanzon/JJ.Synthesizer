﻿using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_AggregateFollower_AllVars : OperatorDtoBase_WithDimension
    {
        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase SliceLengthOperatorDto { get; set; }
        public OperatorDtoBase SampleCountOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, SliceLengthOperatorDto, SampleCountOperatorDto }; }
            set { SignalOperatorDto = value[0]; SliceLengthOperatorDto = value[1]; SampleCountOperatorDto = value[2]; }
        }
    }
}