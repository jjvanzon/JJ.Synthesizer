namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_AggregateFollower : OperatorDtoBase
    {
        public OperatorDtoBase SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase SliceLengthOperatorDto => InputOperatorDtos[1];
        public OperatorDtoBase SampleCountOperatorDto => InputOperatorDtos[2];

        public OperatorDtoBase_AggregateFollower(
            OperatorDtoBase signalOperatorDto, 
            OperatorDtoBase sliceLengthOperatorDto,
            OperatorDtoBase sampleCountOperatorDto)
            : base(new OperatorDtoBase[] { signalOperatorDto, sliceLengthOperatorDto, sampleCountOperatorDto })
        { }
    }
}
