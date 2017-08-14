using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SetDimension_OperatorDto : OperatorDtoBase_WithDimension, IOperatorDto_WithSignal_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SetDimension;

        public InputDto PassThrough { get; set; }
        public InputDto Number { get; set; }

        public InputDto Signal
        {
            get => PassThrough;
            set => PassThrough = value;
        }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { PassThrough, Number };
            set
            {
                var array = value.ToArray();
                PassThrough = array[0];
                Number = array[1];
            }
        }
    }

    internal class SetDimension_OperatorDto_VarPassThrough_VarNumber : SetDimension_OperatorDto
    { }

    internal class SetDimension_OperatorDto_VarPassThrough_ConstNumber : SetDimension_OperatorDto
    { }

    internal class SetDimension_OperatorDto_ConstPassThrough_VarNumber : SetDimension_OperatorDto
    { }

    internal class SetDimension_OperatorDto_ConstPassThrough_ConstNumber : SetDimension_OperatorDto
    { }
}
