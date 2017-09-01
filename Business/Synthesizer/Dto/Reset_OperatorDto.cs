using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Reset_OperatorDto : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Reset;

        public InputDto PassThroughInput { get; set; }
        public string Name { get; set; }
        public int? Position { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { PassThroughInput };
            set => PassThroughInput = value.ElementAtOrDefault(0);
        }
    }
}
