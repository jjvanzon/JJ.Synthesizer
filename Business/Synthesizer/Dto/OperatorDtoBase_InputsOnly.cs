﻿using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_InputsOnly : OperatorDtoBase
    {
        public override IEnumerable<InputDto> Inputs { get; set; }
    }
}