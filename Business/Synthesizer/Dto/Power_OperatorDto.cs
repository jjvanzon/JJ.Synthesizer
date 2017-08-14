using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Power_OperatorDto : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Power;

        public InputDto Base { get; set; }
        public InputDto Exponent { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Base, Exponent };
            set
            {
                var array = value.ToArray();
                Base = array[0];
                Exponent = array[1];
            }
        }
    }
}
