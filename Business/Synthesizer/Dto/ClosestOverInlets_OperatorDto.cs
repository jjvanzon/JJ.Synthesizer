using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Dto
{
    internal class ClosestOverInlets_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.ClosestOverInlets);

        public OperatorDtoBase InputOperatorDto => InputOperatorDtos[0];
        public IList<OperatorDtoBase> ItemOperatorDtos => InputOperatorDtos.Skip(1).ToArray();

        public ClosestOverInlets_OperatorDto(
            OperatorDtoBase inputOperatorDto, 
            IList<OperatorDtoBase> itemOperatorDtos) 
            : base(inputOperatorDto.Union(itemOperatorDtos).ToArray()) // TODO: Low priority: Not null-safe.
        { }
    }
}
