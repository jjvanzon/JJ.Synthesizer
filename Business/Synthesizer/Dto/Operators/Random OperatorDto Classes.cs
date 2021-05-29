using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal class Random_OperatorDto : OperatorDtoBase_PositionReader, IOperatorDto_WithInterpolation
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Random;

        public InputDto Rate { get; set; }
        public InterpolationTypeEnum InterpolationTypeEnum { get; set; }
        public ArrayDto ArrayDto { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Rate, Position };
            set
            {
                Rate = value.ElementAtOrDefault(0);
                Position = value.ElementAtOrDefault(1);
            }
        }
    }

    internal class Random_OperatorDto_Block : Random_OperatorDto
    { }

    internal class Random_OperatorDto_Stripe_LagBehind : Random_OperatorDto
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.RandomStripe;
    }
}
