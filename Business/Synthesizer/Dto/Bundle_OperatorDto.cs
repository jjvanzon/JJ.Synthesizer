using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Bundle_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Bundle);

        public Bundle_OperatorDto(IList<OperatorDtoBase> inputOperatorDtos)
            : base(inputOperatorDtos)
        { }
    }
}
