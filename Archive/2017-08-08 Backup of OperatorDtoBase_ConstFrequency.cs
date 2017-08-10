//using System.Collections.Generic;
//using JJ.Business.Synthesizer.Helpers;

//namespace JJ.Business.Synthesizer.Dto
//{
//    internal abstract class OperatorDtoBase_ConstFrequency : OperatorDtoBase_WithDimension
//    {
//        public double Frequency { get; set; }

//        public override IList<IOperatorDto> InputOperatorDtos { get; set; } = new IOperatorDto[0];

//        public override IEnumerable<InputDto> Inputs => new[] { InputDtoFactory.CreateInputDto(Frequency) };
//    }
//}