using System;
using System.Collections.Generic;
using System.Diagnostics;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Dto
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal abstract class OperatorDtoBase
    {
        public int DimensionStackLevel { get; set; }
        public abstract string OperatorTypeName { get; }
        public IList<OperatorDtoBase> InputOperatorDtos { get; set; }

        public OperatorDtoBase(IList<OperatorDtoBase> inputOperatorDtos)
        {
            if (inputOperatorDtos == null) throw new NullException(() => inputOperatorDtos);
            InputOperatorDtos = inputOperatorDtos;
        }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
