using System.Collections.Generic;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal abstract class OperatorDtoBase_Vars : OperatorDtoBase
    {
        public IList<OperatorDtoBase> Vars { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return Vars; }
            set { Vars = value; }
        }
    }
}
