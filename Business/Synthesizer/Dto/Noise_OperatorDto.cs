using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Noise_OperatorDto : OperatorDtoBase_VarFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Noise);

        public Noise_OperatorDto(OperatorDtoBase frequencyOperatorDto)
            : base(frequencyOperatorDto)
        { }
    }
}