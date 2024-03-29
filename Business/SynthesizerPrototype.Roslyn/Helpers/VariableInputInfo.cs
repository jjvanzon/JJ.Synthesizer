﻿using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.SynthesizerPrototype.Roslyn.Helpers
{
    internal class VariableInputInfo
    {
        public string NameCamelCase { get; }
        public double Value { get; }

        public VariableInputInfo(string nameCamelCase, double value)
        {
            if (string.IsNullOrEmpty(nameCamelCase)) throw new NullOrEmptyException(() => nameCamelCase);

            NameCamelCase = nameCamelCase;
            Value = value;
        }
    }
}
