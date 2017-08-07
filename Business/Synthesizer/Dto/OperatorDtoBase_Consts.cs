using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_Consts : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public IList<double> Consts { get; set; }

        public override IEnumerable<InputDto> InputDtos => Consts.Select(x => new InputDto(x)).ToArray();
    }
}
