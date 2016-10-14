using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.Inlining.Dto
{
    internal class InletDto
    {
        public InletDto()
        { }

        public InletDto(OperatorDto inputOperatorDto)
        {
            InputOperatorDto = inputOperatorDto;
        }

        public OperatorDto InputOperatorDto { get; set; }
    }
}
