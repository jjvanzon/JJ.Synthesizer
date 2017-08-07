using System.Collections.Generic;
using System.Diagnostics;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Dto
{
    /// <summary> See IOperatorDto for member summaries. </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    internal abstract class OperatorDtoBase : IOperatorDto
    {
        public abstract OperatorTypeEnum OperatorTypeEnum { get; }
        public abstract IList<IOperatorDto> InputOperatorDtos { get; set; }
        public abstract IEnumerable<InputDto> InputDtos { get; }
        public int OperatorID { get; set; }
        public string OperationIdentity { get; set; }

        private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
    }
}
