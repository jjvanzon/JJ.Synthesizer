using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_VarA_ConstB : OperatorDtoBase
    {
        public IOperatorDto AOperatorDto { get; set; }
        public double B { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { AOperatorDto };
            set => AOperatorDto = value[0];
        }
    }
}
