using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal class NthRoot_OperatorDto : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.NthRoot;

        public InputDto X { get; set; }
        public InputDto Y { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { X, Y };
            set
            {
                X = value.ElementAtOrDefault(0);
                Y = value.ElementAtOrDefault(1);
            }
        }
    }
}