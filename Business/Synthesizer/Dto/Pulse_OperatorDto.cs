using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Pulse_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Pulse);

        public OperatorDtoBase FrequencyOperatorDto { get; set; }
        public OperatorDtoBase WidthOperatorDto { get; set; }
        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { FrequencyOperatorDto, WidthOperatorDto }; }
            set { FrequencyOperatorDto = value[0]; WidthOperatorDto = value[1]; }
        }
    }

    internal class Pulse_OperatorDto_ConstFrequency_HalfWidth_WithOriginShifting : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Pulse);

        public double FrequencyOperatorDto { get; set; }
        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }
    }

    internal class Pulse_OperatorDto_ConstFrequency_ConstWidth_WithOriginShifting : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Pulse);

        public double Frequency { get; set; }
        public double Width { get; set; }
        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }
    }

    internal class Pulse_OperatorDto_ConstFrequency_VarWidth_WithOriginShifting : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Pulse);

        public double Frequency { get; set; }
        public OperatorDtoBase WidthOperatorDto { get; set; }
        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { WidthOperatorDto }; }
            set { WidthOperatorDto = value[0]; }
        }
    }

    internal class Pulse_OperatorDto_VarFrequency_HalfWidth_WithPhaseTracking : OperatorDtoBase_VarFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Pulse);
    }

    internal class Pulse_OperatorDto_VarFrequency_ConstWidth_WithPhaseTracking : OperatorDtoBase_VarFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Pulse);

        public double Width { get; set; }
    }

    internal class Pulse_OperatorDto_VarFrequency_VarWidth_WithPhaseTracking : Pulse_OperatorDto
    { }

    internal class Pulse_OperatorDto_ConstFrequency_HalfWidth_NoOriginShifting : Pulse_OperatorDto_ConstFrequency_HalfWidth_WithOriginShifting
    { }

    internal class Pulse_OperatorDto_ConstFrequency_ConstWidth_NoOriginShifting : Pulse_OperatorDto_ConstFrequency_ConstWidth_WithOriginShifting
    { }

    internal class Pulse_OperatorDto_ConstFrequency_VarWidth_NoOriginShifting : Pulse_OperatorDto_ConstFrequency_VarWidth_WithOriginShifting
    { }

    internal class Pulse_OperatorDto_VarFrequency_HalfWidth_NoPhaseTracking : Pulse_OperatorDto_VarFrequency_HalfWidth_WithPhaseTracking
    { }

    internal class Pulse_OperatorDto_VarFrequency_ConstWidth_NoPhaseTracking : Pulse_OperatorDto_VarFrequency_ConstWidth_WithPhaseTracking
    { }

    internal class Pulse_OperatorDto_VarFrequency_VarWidth_NoPhaseTracking : Pulse_OperatorDto_VarFrequency_VarWidth_WithPhaseTracking
    { }
}