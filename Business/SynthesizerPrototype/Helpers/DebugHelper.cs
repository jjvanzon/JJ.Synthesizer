using JJ.Business.SynthesizerPrototype.Dto;
using JJ.Framework.Exceptions;

namespace JJ.Business.SynthesizerPrototype.Helpers
{
    internal static class DebugHelper
    {
        public static string GetDebuggerDisplay(OperatorDtoBase operatorDto)
        {
            if (operatorDto == null) throw new NullException(() => operatorDto);

            return operatorDto.GetType().Name;
        }
    }
}
