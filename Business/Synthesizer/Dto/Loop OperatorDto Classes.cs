using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Loop_OperatorDto : OperatorDtoBase_WithDimension, IOperatorDto_WithSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Loop;

        public InputDto Signal { get; set; }
        public InputDto Skip { get; set; }
        public InputDto LoopStartMarker { get; set; }
        public InputDto LoopEndMarker { get; set; }
        public InputDto ReleaseEndMarker { get; set; }
        public InputDto NoteDuration { get; set; }

        public override IEnumerable<InputDto> Inputs
        {
            get => new[]
            {
                Signal,
                Skip,
                LoopStartMarker,
                LoopEndMarker,
                ReleaseEndMarker,
                NoteDuration
            };
            set
            {
                var array = value.ToArray();
                Signal = array[0];
                Skip = array[1];
                LoopStartMarker = array[2];
                LoopEndMarker = array[3];
                ReleaseEndMarker = array[4];
                NoteDuration = array[5];
            }
        }

    }

    internal class Loop_OperatorDto_ConstSignal : Loop_OperatorDto
    { }

    internal class Loop_OperatorDto_NoSkipOrRelease_ManyConstants : Loop_OperatorDto
    { }

    internal class Loop_OperatorDto_ManyConstants : Loop_OperatorDto
    { }

    internal class Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration : Loop_OperatorDto
    { }

    internal class Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration : Loop_OperatorDto
    { }

    internal class Loop_OperatorDto_NoSkipOrRelease : Loop_OperatorDto
    { }

    internal class Loop_OperatorDto_SoundVarOrConst_OtherInputsVar : Loop_OperatorDto
    { }
}