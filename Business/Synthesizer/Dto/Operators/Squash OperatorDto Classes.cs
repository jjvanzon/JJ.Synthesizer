using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable SuggestVarOrType_Elsewhere

namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal class Squash_OperatorDto : OperatorDtoBase_PositionTransformation
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Squash;

        public InputDto Factor { get; set; }
        public InputDto Origin { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Signal, Factor, Origin, Position };
            set
            {
                var array = value.ToArray();
                Signal = array.ElementAtOrDefault(0);
                Factor = array.ElementAtOrDefault(1);
                Origin = array.ElementAtOrDefault(2);
                Position = array.ElementAtOrDefault(3);
            }
        }
    }

    internal class Squash_OperatorDto_WithOrigin : Squash_OperatorDto
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Squash;
    }

    internal class Squash_OperatorDto_ZeroOrigin : Squash_OperatorDto
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Squash;
    }

    internal class Squash_OperatorDto_FactorZero : Squash_OperatorDto
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Squash;
    }

    // For Time Dimension

    internal class Squash_OperatorDto_ConstFactor_WithOriginShifting : Squash_OperatorDto
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Squash;
    }

    internal class Squash_OperatorDto_VarFactor_WithPhaseTracking : Squash_OperatorDto
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Squash;
    }
}
