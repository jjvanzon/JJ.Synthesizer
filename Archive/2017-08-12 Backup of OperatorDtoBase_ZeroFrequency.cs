//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Helpers;

//namespace JJ.Business.Synthesizer.Dto
//{
//    internal abstract class OperatorDtoBase_ZeroFrequency : OperatorDtoBase
//    {
//        public InputDto Frequency { get; set; } = InputDtoFactory.CreateInputDto(0);

//        public override IEnumerable<InputDto> Inputs
//        {
//            get => new[] { Frequency };
//            set => Frequency = value.ElementAt(0);
//        }
//    }
//}