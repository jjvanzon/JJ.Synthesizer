//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Enums;

//namespace JJ.Business.Synthesizer.Dto
//{
//    internal class Reverse_OperatorDto : OperatorDtoBase_WithDimension, IOperatorDto_WithSignal_WithDimension
//    {
//        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Reverse;

//        public InputDto Signal { get; set; }
//        public InputDto Factor { get; set; }

//        public override IEnumerable<InputDto> Inputs
//        {
//            get => new[] { Signal, Factor };
//            set
//            {
//                var array = value.ToArray();
//                Signal = array[0];
//                Factor = array[1];
//            }
//        }
//    }

//    internal class Reverse_OperatorDto_ConstSignal : OperatorDtoBase_WithSignal
//    {
//        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Reverse;
//    }

//    internal class Reverse_OperatorDto_VarFactor_WithPhaseTracking : Reverse_OperatorDto
//    { }

//    internal class Reverse_OperatorDto_VarFactor_NoPhaseTracking : Reverse_OperatorDto
//    { }

//    internal class Reverse_OperatorDto_ConstFactor_WithOriginShifting : Reverse_OperatorDto
//    { }

//    internal class Reverse_OperatorDto_ConstFactor_NoOriginShifting : Reverse_OperatorDto
//    { }

//    internal abstract class Reverse_OperatorDtoBase_ConstFactor : Reverse_OperatorDto
//    { }
//}