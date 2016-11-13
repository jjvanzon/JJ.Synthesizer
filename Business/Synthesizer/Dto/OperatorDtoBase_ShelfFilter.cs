namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_ShelfFilter : OperatorDtoBase
    {
        public OperatorDtoBase SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase TransitionFrequencyOperatorDto => InputOperatorDtos[1];
        public OperatorDtoBase TransitionSlopeOperatorDto => InputOperatorDtos[2];
        public OperatorDtoBase DBGainOperatorDto => InputOperatorDtos[3];

        public OperatorDtoBase_ShelfFilter(
            OperatorDtoBase signalOperatorDto,
            OperatorDtoBase transitionFrequencyOperatorDto,
            OperatorDtoBase transitionSlopeOperatorDto,
            OperatorDtoBase dbGainOperatorDto)
            : base(new OperatorDtoBase[] 
            {
                signalOperatorDto,
                transitionFrequencyOperatorDto,
                transitionSlopeOperatorDto,
                dbGainOperatorDto
            })
        { }
    }
}
