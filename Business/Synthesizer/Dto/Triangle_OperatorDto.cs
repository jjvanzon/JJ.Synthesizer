using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Triangle_OperatorDto : OperatorDto_VarFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Triangle);

        public Triangle_OperatorDto(OperatorDto frequencyOperatorDto)
            : base(frequencyOperatorDto)
        { }
    }
}