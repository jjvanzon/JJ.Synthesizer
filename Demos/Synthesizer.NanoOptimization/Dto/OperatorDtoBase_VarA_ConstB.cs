using System.Collections.Generic;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal abstract class OperatorDtoBase_VarA_ConstB : OperatorDtoBase
    {
        public OperatorDtoBase AOperatorDto { get; set; }
        public double B { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { AOperatorDto }; }
            set { AOperatorDto = value[0]; }
        }
    }
}
