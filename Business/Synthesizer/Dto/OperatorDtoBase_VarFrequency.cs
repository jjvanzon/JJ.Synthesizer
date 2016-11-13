using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_VarFrequency : OperatorDtoBase
    {
        public OperatorDtoBase FrequencyOperatorDto => InputOperatorDtos[0];

        public OperatorDtoBase_VarFrequency(OperatorDtoBase frequencyOperatorDto)
            : base(new OperatorDtoBase[] { frequencyOperatorDto })
        { }
    }
}
