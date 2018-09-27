using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Tests.Helpers;

namespace JJ.Business.Synthesizer.Tests
{
    public abstract class Synthesizer_Interpolate_Tests_Base
    {
        protected void Test_Synthesizer_Interpolate_Base(
            CalculationEngineEnum calculationEngineEnum,
            InterpolationTypeEnum interpolationTypeEnum,
            FollowingModeEnum followingModeEnum,
            double rate,
            IList<(double dimensionValue, double outputValue)> points)
        {
            // Transform this method's input into something the TestExecutor wants.
            IList<DimensionEnum> inputDimensionEnums = new[] { TestConstants.DEFAULT_DIMENSION_ENUM, DimensionEnum.Rate };
            IList<(double, double)> inputTuples = points.Select(point => (point.dimensionValue, rate)).ToArray();
            IList<double> outputValues = points.Select(point => point.outputValue).ToArray();

            TestExecutor.ExecuteTest(
                x => x.Interpolate(
                    x.New(nameof(SystemPatchNames.Sin), x.PatchInlet(TestConstants.DEFAULT_DIMENSION_ENUM)),
                    x.PatchInlet(DimensionEnum.Rate),
                    interpolationTypeEnum,
                    TestConstants.DEFAULT_DIMENSION_ENUM,
                    "",
                    followingModeEnum),
                inputDimensionEnums,
                inputTuples,
                outputValues,
                calculationEngineEnum,
                new TestOptions(significantDigits: null, decimalDigits: 6, mustPlot: true, onlyUseOutputValuesForPlot: true));
        }
    }
}