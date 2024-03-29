﻿namespace JJ.Business.Synthesizer.Tests.Helpers
{
    public class TestOptions
    {
        public TestOptions(
            int? significantDigits = TestConstants.DEFAULT_SIGNIFICANT_DIGITS,
            int? decimalDigits = null,
            bool mustCompareZeroAndNonZeroOnly = false,
            bool mustPlot = false,
            bool onlyUseOutputValuesForPlot = false,
            int plotLineCount = 7)
        {
            SignificantDigits = significantDigits;
            DecimalDigits = decimalDigits;
            MustCompareZeroAndNonZeroOnly = mustCompareZeroAndNonZeroOnly;
            MustPlot = mustPlot;
            OnlyUseOutputValuesForPlot = onlyUseOutputValuesForPlot;
            PlotLineCount = plotLineCount;
        }

        public int? SignificantDigits { get; }
        public int? DecimalDigits { get; }
        public bool MustCompareZeroAndNonZeroOnly { get; }
        public bool MustPlot { get; }
        public bool OnlyUseOutputValuesForPlot { get; }
        public int PlotLineCount { get; }
    }
}