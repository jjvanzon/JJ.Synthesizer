using System.Collections.Generic;

namespace JJ.Business.SynthesizerPrototype.Dto
{
    internal interface IOperatorDto
    {
        /// <summary> Only used to add comment to output generated C# code. </summary>
        string OperatorTypeName { get; }
        IList<OperatorDtoBase> InputOperatorDtos { get; set; }
        int DimensionStackLevel { get; set; }
    }
}
