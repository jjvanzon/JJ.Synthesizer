using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
// ReSharper disable MemberCanBePrivate.Global

namespace JJ.Business.Synthesizer.Dto.Operators
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
