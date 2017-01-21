using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal interface IOperatorDto
    {
        /// <summary> Only used to add comment to output generated C# code. </summary>
        OperatorTypeEnum OperatorTypeEnum { get; }
        IList<OperatorDtoBase> InputOperatorDtos { get; set; }
        int DimensionStackLevel { get; set; }
    }
}
