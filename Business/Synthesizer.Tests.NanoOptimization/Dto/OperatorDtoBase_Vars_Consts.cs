using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Dto
{
    internal abstract class OperatorDtoBase_Vars_Consts : OperatorDtoBase
    {
        public IList<OperatorDtoBase> Vars { get; set; }
        public IList<double> Consts { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return Vars; }
            set { Vars = value; }
        }
    }
}
