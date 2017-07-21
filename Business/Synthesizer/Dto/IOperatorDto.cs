using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal interface IOperatorDto
    {
        /// <summary> Only used to add comment to output generated C# code. </summary>
        OperatorTypeEnum OperatorTypeEnum { get; }
        /// <summary> Assigning list items does not work. Only set the list as a whole. </summary>
        IList<IOperatorDto> InputOperatorDtos { get; set; }

        /// <summary>
        /// Regularly used as a cache key:
        /// For Cache operators.
        /// For Random and Noise it is the cache key for offsets.
        /// For uninlining methods in generated code, OperatorID is also the key for reusing the same generated method.
        /// That last bit make it required to store it with each OperatorDto, because for instance any 'else' or 'then'
        /// clause of an if operator could become a method on its own, and any operator can be tied to the 'then' and the 'else'.
        /// The only exception is that Number_OperatorDto's do not need an OperatorID, in case of which OperatorID is 0.
        /// </summary>
        int OperatorID { get; set; }
    }
}
