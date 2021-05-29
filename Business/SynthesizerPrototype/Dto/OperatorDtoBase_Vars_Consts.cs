using System.Collections.Generic;

namespace JJ.Business.SynthesizerPrototype.Dto
{
    public abstract class OperatorDtoBase_Vars_Consts : OperatorDtoBase
    {
        public IList<IOperatorDto> Vars { get; set; }
        public IList<double> Consts { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => Vars;
            set => Vars = value;
        }
    }
}
