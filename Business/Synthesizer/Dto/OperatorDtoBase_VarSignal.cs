using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_VarSignal : OperatorDtoBase, IOperatorDto_VarSignal
    {
        public IOperatorDto SignalOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get { return new[] { SignalOperatorDto }; }
            set { SignalOperatorDto = value[0]; }
        }
    }
}
