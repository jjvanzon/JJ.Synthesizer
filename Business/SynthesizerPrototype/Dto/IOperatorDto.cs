using System.Collections.Generic;

namespace JJ.Business.SynthesizerPrototype.Dto
{
    public interface IOperatorDto
    {
        /// <summary> Only used to add comment to output generated C# code. </summary>
        string OperatorTypeName { get; }
        IList<IOperatorDto> InputOperatorDtos { get; set; }
        int DimensionStackLevel { get; set; }
    }
}
