using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SawUp_OperatorDto_VarFrequency_NoPhaseTracking : OperatorDto_VarFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SawUp);

        public SawUp_OperatorDto_VarFrequency_NoPhaseTracking(OperatorDto frequencyOperatorDto)
            : base(frequencyOperatorDto)
        { }
    }
}
