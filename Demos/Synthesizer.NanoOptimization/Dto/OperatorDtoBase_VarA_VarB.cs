using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Dto
{
    internal abstract class OperatorDtoBase_VarA_VarB : OperatorDtoBase
    {
        public OperatorDtoBase AOperatorDto { get; set; }
        public OperatorDtoBase BOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { AOperatorDto, BOperatorDto }; }
            set { AOperatorDto = value[0]; BOperatorDto = value[1]; }
        }
    }
}
