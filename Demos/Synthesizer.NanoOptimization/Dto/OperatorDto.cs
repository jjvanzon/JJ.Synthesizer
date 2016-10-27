using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
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
