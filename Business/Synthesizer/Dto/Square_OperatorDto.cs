using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Square_OperatorDto : OperatorDto_VarFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Square);

        public Square_OperatorDto(OperatorDto frequencyOperatorDto)
            : base(frequencyOperatorDto)
        { }
    }
}