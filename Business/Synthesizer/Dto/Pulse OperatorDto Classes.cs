using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Pulse_OperatorDto : OperatorDtoBase_VarFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Pulse);

        public OperatorDtoBase WidthOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { FrequencyOperatorDto, WidthOperatorDto }; }
            set { FrequencyOperatorDto = value[0]; WidthOperatorDto = value[1]; }
        }
    }

    internal class Pulse_OperatorDto_ConstFrequency_HalfWidth_WithOriginShifting : OperatorDtoBase_ConstFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Pulse);
    }

    internal class Pulse_OperatorDto_ConstFrequency_ConstWidth_WithOriginShifting : OperatorDtoBase_ConstFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Pulse);

        public double Width { get; set; }
    }

    internal class Pulse_OperatorDto_ConstFrequency_VarWidth_WithOriginShifting : OperatorDtoBase_ConstFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Pulse);

        public OperatorDtoBase WidthOperatorDto { get; set; }

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