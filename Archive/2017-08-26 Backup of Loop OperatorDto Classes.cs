//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Enums;

//namespace JJ.Business.Synthesizer.Dto
//{
//    internal class Loop_OperatorDto : OperatorDtoBase_WithPositionOutput
//    {
//        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Loop;

//        public InputDto Skip { get; set; }
//        public InputDto LoopStartMarker { get; set; }
//        public InputDto LoopEndMarker { get; set; }
//        public InputDto ReleaseEndMarker { get; set; }
//        public InputDto NoteDuration { get; set; }

//        public override IReadOnlyList<InputDto> Inputs
//        {
//            get => new[]
//            {
//                Signal,
//                Skip,
//                LoopStartMarker,
//                LoopEndMarker,
//                ReleaseEndMarker,
//                NoteDuration,
//                Position
//            };
//            set
//            {
//                Signal = value.ElementAtOrDefault(0);
//                Skip = value.ElementAtOrDefault(1);
//                LoopStartMarker = value.ElementAtOrDefault(2);
//                LoopEndMarker = value.ElementAtOrDefault(3);
//                ReleaseEndMarker = value.ElementAtOrDefault(4);
//                NoteDuration = value.ElementAtOrDefault(5);
//                Position = value.ElementAtOrDefault(6);
//            }
//        }
//    }

//    internal class Loop_OperatorDto_ConstSignal : Loop_OperatorDto
//    { }

//    internal class Loop_OperatorDto_NoSkipOrRelease_ManyConstants : Loop_OperatorDto
//    { }

//    internal class Loop_OperatorDto_ManyConstants : Loop_OperatorDto
//    { }

//    internal class Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration : Loop_OperatorDto
//    { }

//    internal class Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration : Loop_OperatorDto
//    { }

//    internal class Loop_OperatorDto_NoSkipOrRelease : Loop_OperatorDto
//    { }

//    internal class Loop_OperatorDto_AllVars : Loop_OperatorDto
//    { }
//}