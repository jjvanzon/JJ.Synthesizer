using System;
using System.Globalization;
using JJ.Framework.Exceptions;

namespace JJ.Business.SynthesizerPrototype.Roslyn.Helpers
{
    internal class VariableInputInfo
    {
        public string NameCamelCase { get; }
        public double Value { get; }

        public VariableInputInfo(string nameCamelCase, double value)
        {
            if (String.IsNullOrEmpty(nameCamelCase)) throw new NullOrEmptyException(() => nameCamelCase);

            NameCamelCase = nameCamelCase;
            Value = value;
        }
    }
}
