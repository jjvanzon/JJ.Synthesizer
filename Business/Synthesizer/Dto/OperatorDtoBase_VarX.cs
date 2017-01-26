using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_VarX : OperatorDtoBase
    {
        public OperatorDtoBase XOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new[] { XOperatorDto }; }
            set { XOperatorDto = value[0]; }
        }
    }
}
