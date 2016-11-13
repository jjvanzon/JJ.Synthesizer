namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_VarX : OperatorDtoBase
    {
        public OperatorDtoBase XOperatorDto => InputOperatorDtos[0];

        public OperatorDtoBase_VarX(OperatorDtoBase xOperatorDto)
            : base(new OperatorDtoBase[] { xOperatorDto })
        { }
    }
}
