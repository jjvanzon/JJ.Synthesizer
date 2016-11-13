using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Dto
{
    internal class CustomOperator_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.CustomOperator);

        // TODO: You need information to identify which outlet of the Custom Operator is represented here.

        public Patch UnderlyingPatch { get; }

        public CustomOperator_OperatorDto(Patch underlyingPatch, IList<OperatorDtoBase> inputOperatorDtos)
            : base(inputOperatorDtos)
        {
            UnderlyingPatch = underlyingPatch;
        }
    }
}
