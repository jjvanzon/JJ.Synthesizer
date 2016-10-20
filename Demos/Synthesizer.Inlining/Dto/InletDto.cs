using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal class InletDto
    {
        public InletDto()
        { }

        public InletDto(OperatorDto inputOperatorDto)
        {
            InputOperatorDto = inputOperatorDto;
        }

        public OperatorDto InputOperatorDto { get; set; }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
