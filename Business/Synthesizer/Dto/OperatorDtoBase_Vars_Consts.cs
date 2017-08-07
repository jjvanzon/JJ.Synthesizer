using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_Vars_Consts : OperatorDtoBase
    {
        public IList<IOperatorDto> Vars { get; set; }
        public IList<double> Consts { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => Vars;
            set => Vars = value;
        }

        public override IEnumerable<InputDto> InputDtos
        {
            get
            {
                foreach (double @const in Consts)
                {
                    yield return new InputDto(@const);
                }

                foreach (IOperatorDto var in Vars)
                {
                    yield return new InputDto(var);
                }
            }
        }
    }
}
