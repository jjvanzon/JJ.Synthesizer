namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDto_AggregateFollower : OperatorDto
    {
        public OperatorDto SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDto SliceLengthOperatorDto => InputOperatorDtos[1];
        public OperatorDto SampleCountOperatorDto => InputOperatorDtos[2];

        public OperatorDto_AggregateFollower(
            OperatorDto signalOperatorDto, 
            OperatorDto sliceLengthOperatorDto,
            OperatorDto sampleCountOperatorDto)
            : base(new OperatorDto[] { signalOperatorDto, sliceLengthOperatorDto, sampleCountOperatorDto })
        { }
    }
}
