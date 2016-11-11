using System;
using System.Collections.Generic;
using System.Diagnostics;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Dto
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal abstract class OperatorDto
    {
        public int DimensionStackLevel { get; set; }
        public abstract string OperatorTypeName { get; }
        public IList<OperatorDto> ChildOperatorDtos { get; set; }

        public OperatorDto(IList<OperatorDto> childOperatorDtos)
        {
            if (childOperatorDtos == null) throw new NullException(() => childOperatorDtos);
            ChildOperatorDtos = childOperatorDtos;
        }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
