using System;
using System.Collections.Generic;
using System.Diagnostics;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal abstract class OperatorDtoBase
    {
        public int DimensionStackLevel { get; set; }
        public abstract string OperatorTypeName { get; }
        public abstract IList<OperatorDtoBase> InputOperatorDtos { get; set; } 
        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
