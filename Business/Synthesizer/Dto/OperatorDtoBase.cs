using System.Collections.Generic;
using System.Diagnostics;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Dto
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    internal abstract class OperatorDtoBase : IOperatorDto
    {
        public abstract OperatorTypeEnum OperatorTypeEnum { get; }
        public abstract IList<IOperatorDto> InputOperatorDtos { get; set; }

        public int OperatorID { get; set; }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
