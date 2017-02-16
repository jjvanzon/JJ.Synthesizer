using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_VarX : OperatorDtoBase
    {
        public IOperatorDto XOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get { return new[] { XOperatorDto }; }
            set { XOperatorDto = value[0]; }
        }
    }
}
