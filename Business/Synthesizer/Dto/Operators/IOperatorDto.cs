﻿using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal interface IOperatorDto
    {
        /// <summary> Only used to add comment to output generated C# code. </summary>
        OperatorTypeEnum OperatorTypeEnum { get; }

        /// <summary>
        /// Returns the doubles and IOperatorDto's that are the
        /// inputs of the operator as a single enumerable,
        /// using a quasi-union type InputDto.
        /// </summary>
        IReadOnlyList<InputDto> Inputs { get; set; }

        /// <summary>
        /// A string that uniquely identifies all processing and input and output
        /// up until this point of the calculation.
        /// If two Dto's have the same OperationIdentity it means that it is a duplicate calculation.
        /// The process that assigns this OperationIdentity will assign a different key
        /// if similar calculations must still operate on separate state,
        /// such as the sine waves hiding behind a DimensionToOutlets operator.
        /// 
        /// Regularly used as a cache key:
        /// For Cache operators.
        /// For Random and Noise it is the cache key for offsets.
        /// For uninlining methods in generated code, OperationIdentity is also the key for reusing the same generated method.
        /// That last bit make it required to store it with each OperatorDto, because for instance any 'else' or 'then'
        /// clause of an if operator could become a method on its own, and any operator can be tied to the 'then' and the 'else'.
        /// </summary>
        string OperationIdentity { get; set; }
    }
}
