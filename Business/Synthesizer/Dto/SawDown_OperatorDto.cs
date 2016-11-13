using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SawDown_OperatorDto : OperatorDto_VarFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SawDown);

        public SawDown_OperatorDto(OperatorDto frequencyOperatorDto)
            : base(frequencyOperatorDto)
        { }
    }
}