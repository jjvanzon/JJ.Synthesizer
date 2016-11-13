using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_ConstFrequency : OperatorDtoBase
    {
        public double Frequency { get; set; }

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public OperatorDtoBase_ConstFrequency(double frequency)
            : base (new OperatorDtoBase[0])
        {
            Frequency = frequency;
        }
    }
}