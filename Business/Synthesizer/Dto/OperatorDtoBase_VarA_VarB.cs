namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_VarA_VarB : OperatorDtoBase
    {
        public OperatorDtoBase AOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase BOperatorDto => InputOperatorDtos[1];

        public OperatorDtoBase_VarA_VarB(OperatorDtoBase aOperatorDto, OperatorDtoBase bOperatorDto)
            : base(new OperatorDtoBase[] { aOperatorDto, bOperatorDto })
        { }
    }
}
