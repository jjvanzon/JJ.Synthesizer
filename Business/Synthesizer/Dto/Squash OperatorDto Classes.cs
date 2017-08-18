using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Squash_OperatorDto : Squash_OperatorDtoBase_WithOrigin
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Squash;
    }

    internal class Squash_OperatorDto_WithOrigin : Squash_OperatorDtoBase_WithOrigin
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Squash;
    }

    internal class Squash_OperatorDto_ZeroOrigin : Squash_OperatorDtoBase_ZeroOrigin
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Squash;
    }

    internal class Squash_OperatorDto_ConstSignal : Squash_OperatorDtoBase_NoOrigin
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Squash;
    }

    internal class Squash_OperatorDto_FactorZero : Squash_OperatorDtoBase_NoOrigin
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Squash;
    }

    // For Time Dimension

    internal class Squash_OperatorDto_ConstFactor_WithOriginShifting : Squash_OperatorDtoBase_NoOrigin
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Squash;
    }

    internal class Squash_OperatorDto_VarFactor_WithPhaseTracking : Squash_OperatorDtoBase_NoOrigin
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Squash;
    }

    // Base Classes

    /// <summary> Base class. </summary>
    internal abstract class Squash_OperatorDtoBase_WithOrigin : OperatorDtoBase_WithDimension, IOperatorDto_WithSignal_WithDimension
    {
        public InputDto Signal { get; set; }
        public InputDto Factor { get; set; }
        public InputDto Origin { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Signal, Factor, Origin };
            set
            {
                var array = value.ToArray();
                Signal = array[0];
                Factor = array[1];
                Origin = array[2];
            }
        }
    }

    /// <summary> Base class. </summary>
    internal abstract class Squash_OperatorDtoBase_ZeroOrigin : OperatorDtoBase_WithDimension, IOperatorDto_WithSignal_WithDimension
    {
        public InputDto Signal { get; set; }
        public InputDto Factor { get; set; }
        private static readonly InputDto _origin = 0;

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Signal, Factor, _origin };
            set
            {
                var array = value.ToArray();
                Signal = array[0];
                Factor = array[1];
            }
        }
    }

    /// <summary> Base class. </summary>
    internal abstract class Squash_OperatorDtoBase_NoOrigin : OperatorDtoBase_WithDimension, IOperatorDto_WithSignal_WithDimension
    {
        public InputDto Signal { get; set; }
        public InputDto Factor { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Signal, Factor };
            set
            {
                var array = value.ToArray();
                Signal = array[0];
                Factor = array[1];
            }
        }
    }
}
