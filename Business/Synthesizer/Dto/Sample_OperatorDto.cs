using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Sample_OperatorDto : OperatorDto_VarFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Sample);

        public Sample Sample { get; }

        public Sample_OperatorDto(Sample sample, OperatorDto frequencyOperatorDto)
            : base(frequencyOperatorDto)
        {
            Sample = sample;
        }
    }
}