using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Pulse_OperatorDto : OperatorDtoBase_WithFrequency
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Pulse;

        public InputDto Width { get; set; }

        public override IEnumerable<InputDto> Inputs
        {
            get => new[] { Frequency, Width };
            set
            {
                var array = value.ToArray();
                Frequency = array[0];
                Width = array[1];
            }
        }
    }

    internal class Pulse_OperatorDto_ConstFrequency_WithOriginShifting : Pulse_OperatorDto
    { }

    internal class Pulse_OperatorDto_VarFrequency_WithPhaseTracking : Pulse_OperatorDto
    { }

    internal class Pulse_OperatorDto_ConstFrequency_NoOriginShifting : Pulse_OperatorDto
    { }

    internal class Pulse_OperatorDto_VarFrequency_NoPhaseTracking : Pulse_OperatorDto
    { }
}