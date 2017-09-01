using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SetDimension_OperatorDto : OperatorDtoBase_WithPositionOutput
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SetDimension;

        public InputDto PassThrough
        {
            get => Signal;
            set => Signal = value;
        }

        public InputDto Number { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Signal, Number, Position };
            set
            {
                Signal = value.ElementAtOrDefault(0);
                Number = value.ElementAtOrDefault(1);
                Position = value.ElementAtOrDefault(2);
            }
        }
    }
}
