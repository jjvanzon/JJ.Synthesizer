using System;
using System.Collections.Generic;
using System.Diagnostics;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Helpers;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Dto
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
