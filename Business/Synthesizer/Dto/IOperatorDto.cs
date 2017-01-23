using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal interface IOperatorDto
    {
        OperatorTypeEnum OperatorTypeEnum { get; }
        IList<OperatorDtoBase> InputOperatorDtos { get; set; }
        int DimensionStackLevel { get; set; }
    }
}
