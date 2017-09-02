using System.Collections.Generic;
using JJ.Business.Synthesizer.Dto;

namespace JJ.Business.Synthesizer.Roslyn.Helpers
{
    internal class VariableCollections
    {
        // Simple Sets of Variable Names

        public IList<string> LongLivedDoubleVariableNamesCamelCase { get; } = new List<string>();
        public IList<DoubleArrayVariableInfo> LongLivedDoubleArrayVariableInfos { get; } = new List<DoubleArrayVariableInfo>();

        // Information for Input Variables

        /// <summary> Dictionary for unicity. Key is variable name camel-case. </summary>
        public Dictionary<string, InputVariableInfo> VariableName_To_InputVariableInfo_Dictionary { get; } = new Dictionary<string, InputVariableInfo>();

        /// <summary> To maintain instance integrity of input variables when converting from DTO to C# code. </summary>
        public Dictionary<VariableInput_OperatorDto, string> VariableInput_OperatorDto_To_VariableName_Dictionary { get; } =
            new Dictionary<VariableInput_OperatorDto, string>();

        // Information about Satellite Calculators

        public Dictionary<ArrayDto, ArrayCalculationInfo> ArrayDto_To_ArrayCalculationInfo_Dictionary { get; } =
            new Dictionary<ArrayDto, ArrayCalculationInfo>();

        public Dictionary<int, string> RandomOrNoiseOperatorID_To_OffsetVariableNameCamelCase_Dictionary { get; } = new Dictionary<int, string>();
    }
}