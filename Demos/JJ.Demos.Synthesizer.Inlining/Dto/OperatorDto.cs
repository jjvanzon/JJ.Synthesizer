using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.Inlining.Dto
{
    internal abstract class OperatorDto
    {
        public OperatorDto(IList<InletDto> inletDtos)
        {
            if (inletDtos == null) throw new NullException(() => inletDtos);

            InletDtos = inletDtos;
        }

        public IList<InletDto> InletDtos { get; set; }
    }
}
