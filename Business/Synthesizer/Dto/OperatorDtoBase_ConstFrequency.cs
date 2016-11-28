using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_ConstFrequency : OperatorDtoBase_WithDimension
    {
        public double Frequency { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos { get; set; } = new OperatorDtoBase[0];
    }
}