namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDto_VarX : OperatorDto
    {
        public OperatorDto XOperatorDto => InputOperatorDtos[0];

        public OperatorDto_VarX(OperatorDto xOperatorDto)
            : base(new OperatorDto[] { xOperatorDto })
        { }
    }
}
