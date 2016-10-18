using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JJ.Demos.Synthesizer.Inlining.Helpers;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.Inlining.Dto
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal abstract class OperatorDto
    {
        public IList<InletDto> InletDtos { get; set; }

        public OperatorDto(IList<InletDto> inletDtos)
        {
            if (inletDtos == null) throw new NullException(() => inletDtos);

            InletDtos = inletDtos;
        }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
