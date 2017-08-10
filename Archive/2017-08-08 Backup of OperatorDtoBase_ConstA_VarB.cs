//using System.Collections.Generic;
//using JJ.Business.Synthesizer.Helpers;

//namespace JJ.Business.Synthesizer.Dto
//{
//    internal abstract class OperatorDtoBase_ConstA_VarB : OperatorDtoBase
//    {
//        public double A { get; set; }
//        public IOperatorDto BOperatorDto { get; set; }

//        public override IList<IOperatorDto> InputOperatorDtos
//        {
//            get => new[] { BOperatorDto };
//            set => BOperatorDto = value[0];
//        }

//        public override IEnumerable<InputDto> Inputs => new[] { InputDtoFactory.CreateInputDto(A), InputDtoFactory.CreateInputDto(BOperatorDto) };
//    }
//}
