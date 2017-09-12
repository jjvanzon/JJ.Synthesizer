using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Loop_OperatorDto : OperatorDtoBase_PositionTransformation
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Loop;

        public InputDto Skip { get; set; }
        public InputDto LoopStartMarker { get; set; }
        public InputDto LoopEndMarker { get; set; }
        public InputDto ReleaseEndMarker { get; set; }
        public InputDto NoteDuration { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[]
            {
                Signal,
                Skip,
                LoopStartMarker,
                LoopEndMarker,
                ReleaseEndMarker,
                NoteDuration,
                Position
            };
            set
            {
                Signal = value.ElementAtOrDefault(0);
                Skip = value.ElementAtOrDefault(1);
                LoopStartMarker = value.ElementAtOrDefault(2);
                LoopEndMarker = value.ElementAtOrDefault(3);
                ReleaseEndMarker = value.ElementAtOrDefault(4);
                NoteDuration = value.ElementAtOrDefault(5);
                Position = value.ElementAtOrDefault(6);
            }
        }
    }
}
