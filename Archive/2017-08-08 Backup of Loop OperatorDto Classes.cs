//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Helpers;

//namespace JJ.Business.Synthesizer.Dto
//{
//    internal class Loop_OperatorDto : Loop_OperatorDto_AllVars
//    { }

//    internal class Loop_OperatorDto_ConstSignal : OperatorDtoBase_WithSignal
//    {
//        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Loop;
//    }

//    internal class Loop_OperatorDto_NoSkipOrRelease_ManyConstants : OperatorDtoBase_WithDimension, IOperatorDto_WithSignal
//    {
//        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Loop;

//        private static readonly InputDto _skip = InputDtoFactory.CreateInputDto(0);
//        private static readonly InputDto _releaseEndMarker = InputDtoFactory.CreateInputDto(CalculationHelper.VERY_HIGH_VALUE);

//        public InputDto Signal { get; set; }
//        public InputDto LoopStartMarker { get; set; }
//        public InputDto LoopEndMarker { get; set; }
//        public InputDto NoteDuration { get; set; }

//        public override IEnumerable<InputDto> Inputs
//        {
//            get => new[]
//            {
//                Signal,
//                _skip,
//                LoopStartMarker,
//                LoopEndMarker,
//                _releaseEndMarker,
//                NoteDuration
//            };
//            set
//            {
//                var array = value.ToArray();
//                Signal = array[0];
//                //_skip = array[1]
//                LoopStartMarker = array[2];
//                LoopEndMarker = array[3];
//                //_releaseEndMarker = array[4];
//                NoteDuration = array[5];
//            }
//        }
//    }

//    internal class Loop_OperatorDto_ManyConstants : OperatorDtoBase_WithDimension, IOperatorDto_WithSignal
//    {
//        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Loop;

//        public InputDto Signal { get; set; }
//        public InputDto Skip { get; set; }
//        public InputDto LoopStartMarker { get; set; }
//        public InputDto LoopEndMarker { get; set; }
//        public InputDto ReleaseEndMarker { get; set; }
//        public InputDto NoteDuration { get; set; }

//        public override IEnumerable<InputDto> Inputs
//        {
//            get => new[]
//            {
//                Signal,
//                Skip,
//                LoopStartMarker,
//                LoopEndMarker,
//                ReleaseEndMarker,
//                NoteDuration
//            };
//            set
//            {
//                var array = value.ToArray();
//                Signal = array[0];
//                Skip = array[1];
//                LoopStartMarker = array[2];
//                LoopEndMarker = array[3];
//                ReleaseEndMarker = array[4];
//                NoteDuration = array[5];
//            }
//        }
//    }

//    internal class Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration : OperatorDtoBase_WithDimension, IOperatorDto_WithSignal
//    {
//        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Loop;

//        private static readonly InputDto _noteDuration = InputDtoFactory.CreateInputDto(CalculationHelper.VERY_HIGH_VALUE);

//        public InputDto Signal { get; set; }
//        public InputDto SkipAndLoopStartMarker { get; set; }
//        public InputDto LoopEndMarker { get; set; }
//        public InputDto ReleaseEndMarker { get; set; }

//        public override IEnumerable<InputDto> Inputs
//        {
//            get => new[]
//            {
//                Signal,
//                SkipAndLoopStartMarker,
//                SkipAndLoopStartMarker,
//                LoopEndMarker,
//                ReleaseEndMarker,
//                _noteDuration
//            };
//            set
//            {
//                var array = value.ToArray();
//                Signal = array[0];
//                SkipAndLoopStartMarker = array[1];
//                SkipAndLoopStartMarker = array[2];
//                LoopEndMarker = array[3];
//                ReleaseEndMarker = array[4];
//            }
//        }

//    }

//    internal class Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration : OperatorDtoBase_WithDimension, IOperatorDto_WithSignal
//    {
//        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Loop;

//        private static readonly InputDto _noteDuration = InputDtoFactory.CreateInputDto(CalculationHelper.VERY_HIGH_VALUE);

//        public InputDto Signal { get; set; }
//        public InputDto SkipAndLoopStartMarker { get; set; }
//        public InputDto LoopEndMarker { get; set; }
//        public InputDto ReleaseEndMarker { get; set; }

//        public override IEnumerable<InputDto> Inputs
//        {
//            get => new[]
//            {
//                Signal,
//                SkipAndLoopStartMarker,
//                SkipAndLoopStartMarker,
//                LoopEndMarker,
//                ReleaseEndMarker,
//                _noteDuration
//            };
//            set
//            {
//                var array = value.ToArray();
//                Signal = array[0];
//                SkipAndLoopStartMarker = array[1];
//                SkipAndLoopStartMarker = array[2];
//                LoopEndMarker = array[3];
//                ReleaseEndMarker = array[4];
//            }
//        }
//    }

//    internal class Loop_OperatorDto_NoSkipOrRelease : OperatorDtoBase_WithDimension, IOperatorDto_WithSignal
//    {
//        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Loop;

//        private static readonly InputDto _skip = InputDtoFactory.CreateInputDto(0);
//        private static readonly InputDto _releaseEndMarker = InputDtoFactory.CreateInputDto(CalculationHelper.VERY_HIGH_VALUE);

//        public InputDto Signal { get; set; }
//        public InputDto LoopStartMarker { get; set; }
//        public InputDto LoopEndMarker { get; set; }
//        public InputDto NoteDuration { get; set; }

//        public override IEnumerable<InputDto> Inputs
//        {
//            get => new[]
//            {
//                Signal,
//                _skip,
//                LoopStartMarker,
//                LoopEndMarker,
//                _releaseEndMarker,
//                NoteDuration
//            };
//            set
//            {
//                var array = value.ToArray();
//                Signal = array[0];
//                //_skip = array[1];
//                LoopStartMarker = array[2];
//                LoopEndMarker = array[3];
//                //_releaseEndMarker = array[4];
//                NoteDuration = array[5];
//            }
//        }
//    }

//    internal class Loop_OperatorDto_AllVars : OperatorDtoBase_WithDimension, IOperatorDto_WithSignal
//    {
//        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Loop;

//        public InputDto Signal { get; set; }
//        public InputDto Skip { get; set; }
//        public InputDto LoopStartMarker { get; set; }
//        public InputDto LoopEndMarker { get; set; }
//        public InputDto ReleaseEndMarker { get; set; }
//        public InputDto NoteDuration { get; set; }

//        public override IEnumerable<InputDto> Inputs
//        {
//            get => new[]
//            {
//                Signal,
//                Skip,
//                LoopStartMarker,
//                LoopEndMarker,
//                ReleaseEndMarker,
//                NoteDuration
//            };
//            set
//            {
//                var array = value.ToArray();
//                Signal = array[0];
//                Skip = array[1];
//                LoopStartMarker = array[2];
//                LoopEndMarker = array[3];
//                ReleaseEndMarker = array[4];
//                NoteDuration = array[5];
//            }
//        }
//    }
//}