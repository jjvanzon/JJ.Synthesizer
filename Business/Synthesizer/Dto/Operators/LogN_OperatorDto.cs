using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal class LogN_OperatorDto : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.LogN;

        public InputDto Number { get; set; }
        public InputDto Base { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Number, Base };
            set
            {
                Number = value.ElementAtOrDefault(0);
                Base = value.ElementAtOrDefault(1);
            }
        }
    }
}