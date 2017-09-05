using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Spectrum_OperatorDto : OperatorDtoBase_PositionReader
    { 
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Spectrum;

        public InputDto Sound { get; set; }
        public InputDto Start { get; set; }
        public InputDto End { get; set; }
        public InputDto FrequencyCount { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Sound, Start, End, FrequencyCount, Position };
            set
            {
                Sound = value.ElementAtOrDefault(0);
                Start = value.ElementAtOrDefault(1);
                End = value.ElementAtOrDefault(2);
                FrequencyCount = value.ElementAtOrDefault(3);
                Position = value.ElementAtOrDefault(4);
            }
        }
    }
}