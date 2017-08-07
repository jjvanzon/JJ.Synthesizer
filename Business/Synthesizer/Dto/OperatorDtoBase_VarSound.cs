using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_VarSound : OperatorDtoBase, IOperatorDto_VarSound
    {
        public IOperatorDto SoundOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SoundOperatorDto };
            set => SoundOperatorDto = value[0];
        }

        // Outcommented to force making a decision in derived classes.
        //public override IEnumerable<InputDto> InputDtos => new[] { new InputDto(SoundOperatorDto) };
    }
}
