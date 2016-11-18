using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Square_OperatorDto : OperatorDtoBase_VarFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Square);
    }

    internal class Square_OperatorDto_ConstFrequency_WithOriginShifting : OperatorDtoBase_ConstFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Square);
    }

    internal class Square_OperatorDto_VarFrequency_WithPhaseTracking : OperatorDtoBase_VarFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Square);
    }

    internal class Square_OperatorDto_ConstFrequency_NoOriginShifting : Square_OperatorDto_ConstFrequency_WithOriginShifting
    { }

    internal class Square_OperatorDto_VarFrequency_NoPhaseTracking : Square_OperatorDto_VarFrequency_WithPhaseTracking
    { }
}