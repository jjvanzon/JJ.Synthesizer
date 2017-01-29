using System.Collections.Generic;

namespace JJ.Business.SynthesizerPrototype.Dto
{
    public abstract class OperatorDtoBase_ConstA_VarB : OperatorDtoBase
    {
        public double A { get; set; }
        public OperatorDtoBase BOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new[] { BOperatorDto }; }
            set { BOperatorDto = value[0]; }
        }
    }
}
