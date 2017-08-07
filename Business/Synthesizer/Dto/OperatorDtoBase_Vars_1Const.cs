using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_Vars_1Const : OperatorDtoBase
    {
        public IList<IOperatorDto> Vars { get; set; }
        public double Const { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => Vars;
            set => Vars = value;
        }

        public override IEnumerable<InputDto> InputDtos
        {
            get
            {
                foreach (IOperatorDto @var in Vars)
                {
                    yield return new InputDto(@var);
                }

                yield return new InputDto(Const);
            }
        }
    }
}
