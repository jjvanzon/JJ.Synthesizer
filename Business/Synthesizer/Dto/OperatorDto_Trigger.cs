namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDto_Trigger : OperatorDto
    {
        public OperatorDto PassThroughOperatorDto => InputOperatorDtos[0];
        public OperatorDto ResetOperatorDto => InputOperatorDtos[1];

        public OperatorDto_Trigger(OperatorDto passThroughOperatorDto, OperatorDto resetOperatorDto)
            : base(new OperatorDto[] { passThroughOperatorDto, resetOperatorDto })
        { }
    }
}
