using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Spectrum_OperatorDto : Spectrum_OperatorDto_AllVars
    { }

    internal class Spectrum_OperatorDto_ConstSound : OperatorDtoBase_ConstSound
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Spectrum;
    }

    internal class Spectrum_OperatorDto_AllVars : OperatorDtoBase_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Spectrum;

        public InputDto Sound { get; set; }
        public InputDto Start { get; set; }
        public InputDto End { get; set; }
        public InputDto FrequencyCount { get; set; }

        public override IEnumerable<InputDto> Inputs
        {
            get => new[] { Sound, Start, End, FrequencyCount };
            set
            {
                var array = value.ToArray();
                Sound = array[0];
                Start = array[1];
                End = array[2];
                FrequencyCount = array[3];
            }
        }
    }
}