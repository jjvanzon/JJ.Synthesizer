using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal interface IOperatorDto
    {
        OperatorTypeEnum OperatorTypeEnum { get; }
        IList<IOperatorDto> InputOperatorDtos { get; set; }
    }
}
