namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDto_VarA_VarB : OperatorDto
    {
        public OperatorDto AOperatorDto => InputOperatorDtos[0];
        public OperatorDto BOperatorDto => InputOperatorDtos[1];

        public OperatorDto_VarA_VarB(OperatorDto aOperatorDto, OperatorDto bOperatorDto)
            : base(new OperatorDto[] { aOperatorDto, bOperatorDto })
        { }
    }
}
