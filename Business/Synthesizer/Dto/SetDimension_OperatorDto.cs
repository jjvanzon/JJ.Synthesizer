using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SetDimension_OperatorDto : OperatorDtoBase_PositionTransformation
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SetDimension;

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Signal, Position };
            set
            {
                Signal = value.ElementAtOrDefault(0);
                Position = value.ElementAtOrDefault(1);
            }
        }
    }
}
