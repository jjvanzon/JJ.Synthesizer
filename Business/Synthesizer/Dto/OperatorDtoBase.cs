using System.Collections.Generic;
using System.Diagnostics;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Dto
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal abstract class OperatorDtoBase : IOperatorDto
    {
        /// <summary> Only used to add comment to output generated C# code. </summary>
        public abstract string OperatorTypeName { get; }
        public abstract IList<OperatorDtoBase> InputOperatorDtos { get; set; }

        public int DimensionStackLevel { get; set; }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
