using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;

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

    internal class Pulse_OperatorDto_ZeroFrequency : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Pulse;

        private readonly IList<InputDto> _inputs = new[] { InputDtoFactory.CreateInputDto(0), InputDtoFactory.CreateInputDto(0) };

        public override IEnumerable<InputDto> Inputs
        {
            get => _inputs;
            set { }
        }
    }

    internal class Pulse_OperatorDto_ConstFrequency_HalfWidth_WithOriginShifting : Pulse_OperatorDtoBase_WithoutWidth
    { }

    internal class Pulse_OperatorDto_ConstFrequency_ConstWidth_WithOriginShifting : Pulse_OperatorDto
    { }

    internal class Pulse_OperatorDto_ConstFrequency_VarWidth_WithOriginShifting : Pulse_OperatorDto
    { }

    internal class Pulse_OperatorDto_VarFrequency_HalfWidth_WithPhaseTracking : Pulse_OperatorDtoBase_WithoutWidth
    { }

    internal class Pulse_OperatorDto_VarFrequency_ConstWidth_WithPhaseTracking : Pulse_OperatorDto
    { }

    internal class Pulse_OperatorDto_VarFrequency_VarWidth_WithPhaseTracking : Pulse_OperatorDto
    { }

    internal class Pulse_OperatorDto_ConstFrequency_HalfWidth_NoOriginShifting : Pulse_OperatorDtoBase_WithoutWidth
    { }

    internal class Pulse_OperatorDto_ConstFrequency_ConstWidth_NoOriginShifting : Pulse_OperatorDto
    { }

    internal class Pulse_OperatorDto_ConstFrequency_VarWidth_NoOriginShifting : Pulse_OperatorDto
    { }

    internal class Pulse_OperatorDto_VarFrequency_HalfWidth_NoPhaseTracking : Pulse_OperatorDtoBase_WithoutWidth
    { }

    internal class Pulse_OperatorDto_VarFrequency_ConstWidth_NoPhaseTracking : Pulse_OperatorDto
    { }

    internal class Pulse_OperatorDto_VarFrequency_VarWidth_NoPhaseTracking : Pulse_OperatorDto
    { }

    /// <summary> Base class. </summary>
    internal abstract class Pulse_OperatorDtoBase_WithoutWidth : Pulse_OperatorDto
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Pulse;
    }
}