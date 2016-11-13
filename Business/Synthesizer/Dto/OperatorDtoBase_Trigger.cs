namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_Trigger : OperatorDtoBase
    {
        public OperatorDtoBase PassThroughInputOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase ResetOperatorDto => InputOperatorDtos[1];

        public OperatorDtoBase_Trigger(OperatorDtoBase passThroughInputOperatorDto, OperatorDtoBase resetOperatorDto)
            : base(new OperatorDtoBase[] { passThroughInputOperatorDto, resetOperatorDto })
        { }
    }
}
