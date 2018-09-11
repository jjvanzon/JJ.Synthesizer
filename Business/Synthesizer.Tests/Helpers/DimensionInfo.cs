using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal class DimensionInfo
    {
        public DimensionInfo(DimensionEnum dimensionEnum, IList<double> values)
        {
            DimensionEnum = dimensionEnum;
            Values = values ?? throw new ArgumentNullException(nameof(values));
        }

        public DimensionEnum DimensionEnum { get; }
        public IList<double> Values { get; }
    }
}