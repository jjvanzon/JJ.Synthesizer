using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_VarFrequency : OperatorDtoBase
    {
        public OperatorDtoBase FrequencyOperatorDto => InputOperatorDtos[0];

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public OperatorDtoBase_VarFrequency(OperatorDtoBase frequencyOperatorDto)
            : base(new OperatorDtoBase[] { frequencyOperatorDto })
        { }
    }
}
