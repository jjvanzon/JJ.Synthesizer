using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_Trigger : OperatorDtoBase
    {
        public OperatorDtoBase PassThroughInputOperatorDto { get; set; }
        public OperatorDtoBase ResetOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { PassThroughInputOperatorDto, ResetOperatorDto }; }
            set { PassThroughInputOperatorDto = value[0]; ResetOperatorDto = value[1]; }
        }
    }
}
