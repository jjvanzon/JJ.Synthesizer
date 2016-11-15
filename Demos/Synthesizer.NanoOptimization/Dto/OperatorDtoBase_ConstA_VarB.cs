using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal abstract class OperatorDtoBase_ConstA_VarB : OperatorDtoBase
    {
        public double A { get; set; }
        public OperatorDtoBase BOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { BOperatorDto }; }
            set { BOperatorDto = value[0]; }
        }
    }
}
