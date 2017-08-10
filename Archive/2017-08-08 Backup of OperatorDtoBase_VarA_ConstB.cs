//using System.Collections.Generic;
//using JJ.Business.Synthesizer.Helpers;

//namespace JJ.Business.Synthesizer.Dto
//{
//    internal abstract class OperatorDtoBase_VarA_ConstB : OperatorDtoBase
//    {
//        public IOperatorDto AOperatorDto { get; set; }
//        public double B { get; set; }

//        public override IList<IOperatorDto> InputOperatorDtos
//        {
//            get => new[] { AOperatorDto };
//            set => AOperatorDto = value[0];
//        }

//        public override IEnumerable<InputDto> Inputs => new[] { InputDtoFactory.CreateInputDto(AOperatorDto), InputDtoFactory.CreateInputDto(B) };
//    }
//}
