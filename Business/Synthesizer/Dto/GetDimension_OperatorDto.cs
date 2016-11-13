using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class GetDimension_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.GetDimension);

        public GetDimension_OperatorDto() 
            : base(new OperatorDtoBase[0])
        { }
    }
}
