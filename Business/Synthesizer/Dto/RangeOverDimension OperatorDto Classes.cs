using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Dto
{
    internal class RangeOverDimension_OperatorDto : OperatorDtoBase_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.RangeOverDimension;

        public InputDto From { get; set; }
        public InputDto Till { get; set; }
        public InputDto Step { get; set; }

        public override IEnumerable<InputDto> Inputs
        {
            get => new[] { From, Till, Step };
            set
            {
                var array = value.ToArray();
                From = array[0];
                Till = array[1];
                Step = array[2];
            }
        }
    }

    internal class RangeOverDimension_OperatorDto_OnlyVars : RangeOverDimension_OperatorDto
    { }

    internal class RangeOverDimension_OperatorDto_OnlyConsts : RangeOverDimension_OperatorDto
    { }

    /// <summary> For Machine Optimization </summary>
    internal class RangeOverDimension_OperatorDto_WithConsts_AndStepOne : OperatorDtoBase_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.RangeOverDimension;

        private static readonly InputDto _step = InputDtoFactory.CreateInputDto(1);

        public InputDto From { get; set; }
        public InputDto Till { get; set; }

        public override IEnumerable<InputDto> Inputs
        {
            get => new[] { From, Till, _step };
            set
            {
                var array = value.ToArray();
                From = array[0];
                Till = array[1];
            }
        }
    }
}