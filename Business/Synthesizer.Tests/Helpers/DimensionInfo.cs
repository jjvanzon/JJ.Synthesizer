using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal class DimensionInfo
    {
        public DimensionInfo(DimensionEnum dimensionEnum, IList<double> inputValues)
        {
            DimensionEnum = dimensionEnum;
            InputValues = inputValues ?? throw new ArgumentNullException(nameof(inputValues));
        }

        public DimensionEnum DimensionEnum { get; }
        public IList<double> InputValues { get; }
    }
}