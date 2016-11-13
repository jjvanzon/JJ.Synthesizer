using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SawUp_OperatorDto : OperatorDto_VarFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SawUp);

        public SawUp_OperatorDto(OperatorDto frequencyOperatorDto)
            : base(frequencyOperatorDto)
        { }
    }
}