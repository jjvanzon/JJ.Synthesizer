using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Round_OperatorDto : OperatorDtoBase_WithSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Round;

        public InputDto Step { get; set; }
        public InputDto Offset { get; set; }

        public override IEnumerable<InputDto> Inputs
        {
            get => new[] { Signal, Step, Offset };
            set
            {
                var array = value.ToArray();
                Signal = array[0];
                Step = array[1];
                Offset = array[2];
            }
        }
    }

    internal class Round_OperatorDto_AllConsts : Round_OperatorDto
    { }

    internal class Round_OperatorDto_ConstSignal : Round_OperatorDto
    { }

    internal class Round_OperatorDto_VarSignal_VarStep_VarOffset : Round_OperatorDto
    { }

    internal class Round_OperatorDto_VarSignal_VarStep_ConstOffset : Round_OperatorDto
    { }

    internal class Round_OperatorDto_VarSignal_ConstStep_VarOffset : Round_OperatorDto
    { }

    internal class Round_OperatorDto_VarSignal_ConstStep_ConstOffset : Round_OperatorDto
    { }

    internal class Round_OperatorDto_ConstSignal_VarStep_VarOffset : Round_OperatorDto
    { }

    internal class Round_OperatorDto_VarSignal_VarStep_ZeroOffset : Round_OperatorDtoBase_ZeroOffset
    { }

    internal class Round_OperatorDto_VarSignal_ConstStep_ZeroOffset : Round_OperatorDtoBase_ZeroOffset
    { }

    /// <summary> Base class. </summary>
    internal abstract class Round_OperatorDtoBase_ZeroOffset : OperatorDtoBase_WithSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Round;

        public InputDto Step { get; set; }
        private static readonly InputDto _offset = 0;

        public override IEnumerable<InputDto> Inputs
        {
            get => new[] { Signal, Step, _offset };
            set
            {
                var array = value.ToArray();
                Signal = array[0];
                Step = array[1];
            }
        }
    }

    internal class Round_OperatorDto_VarSignal_StepOne_OffsetZero : OperatorDtoBase_WithSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Round;

        private static readonly InputDto _step = 1;
        private static readonly InputDto _offset = 0;

        public override IEnumerable<InputDto> Inputs
        {
            get => new[] { Signal, _step, _offset };
            set
            {
                var array = value.ToArray();
                Signal = array[0];
            }
        }
    }
}