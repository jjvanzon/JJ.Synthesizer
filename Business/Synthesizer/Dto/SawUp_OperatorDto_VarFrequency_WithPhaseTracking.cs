using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SawUp_OperatorDto_VarFrequency_WithPhaseTracking : OperatorDto_VarFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SawUp);

        public SawUp_OperatorDto_VarFrequency_WithPhaseTracking(OperatorDto frequencyOperatorDto)
            : base(frequencyOperatorDto)
        { }
    }
}
