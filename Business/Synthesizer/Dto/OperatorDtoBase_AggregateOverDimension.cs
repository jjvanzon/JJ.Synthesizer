﻿using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_AggregateOverDimension : OperatorDtoBase
    {
        public OperatorDtoBase SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase FromOperatorDto => InputOperatorDtos[1];
        public OperatorDtoBase TillOperatorDto => InputOperatorDtos[2];
        public OperatorDtoBase StepOperatorDto => InputOperatorDtos[3];

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }
        public CollectionRecalculationEnum CollectionRecalculationEnum { get; set; }

        public OperatorDtoBase_AggregateOverDimension(
            OperatorDtoBase signalOperatorDto,
            OperatorDtoBase fromOperatorDto,
            OperatorDtoBase tillOperatorDto,
            OperatorDtoBase stepOperatorDto)
            : base(new OperatorDtoBase[] { signalOperatorDto, fromOperatorDto, tillOperatorDto, stepOperatorDto })
        { }
    }
}