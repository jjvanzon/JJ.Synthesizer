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

        // A field is maintained for backward compatibility with the DTO's that use the constructor argument to specify InputOperatorDtos.
        // The preferred way is to override InputOperatorDtos.
        // But this will first be tried out in the demo project.
        private IList<OperatorDtoBase> _inputOperatorDtos;

        public virtual IList<OperatorDtoBase> InputOperatorDtos => _inputOperatorDtos;

        public OperatorDtoBase()
        { }

        public OperatorDtoBase(IList<OperatorDtoBase> inputOperatorDtos)
        {
            if (inputOperatorDtos == null) throw new NullException(() => inputOperatorDtos);

            _inputOperatorDtos = inputOperatorDtos;
        }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
