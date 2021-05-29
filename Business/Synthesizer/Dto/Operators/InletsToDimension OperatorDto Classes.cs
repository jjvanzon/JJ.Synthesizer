using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Collections;
// ReSharper disable MemberCanBePrivate.Global

namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal class InletsToDimension_OperatorDto : OperatorDtoBase_PositionReader, IOperatorDto_WithInterpolation
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.InletsToDimension;

        public InterpolationTypeEnum InterpolationTypeEnum { get; set; }
        public IList<InputDto> InputsExceptPosition { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => InputsExceptPosition.Concat(Position).ToArray();
            set
            {
                InputsExceptPosition = value.Take(value.Count - 1).ToArray();
                Position = value.LastOrDefault();
            }
        }
    }

    internal class InletsToDimension_OperatorDto_Block : InletsToDimension_OperatorDto
    { }

    internal class InletsToDimension_OperatorDto_Cubic_LagBehind : InletsToDimension_OperatorDto
    { }

    internal class InletsToDimension_OperatorDto_Hermite_LagBehind : InletsToDimension_OperatorDto
    { }

    internal class InletsToDimension_OperatorDto_Line : InletsToDimension_OperatorDto
    { }

    internal class InletsToDimension_OperatorDto_Stripe_LagBehind : InletsToDimension_OperatorDto
    { }
}