using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Tests
{
    public abstract class Synthesizer_Interpolate_Tests_Base
    {
        /// <summary>
        /// Signal will have a cycle length of 8.
        /// Slow rate of sampling will be 1.
        /// </summary>
        protected void Test_Synthesizer_Interpolate_Base(
            CalculationEngineEnum calculationEngineEnum,
            InterpolationTypeEnum interpolationTypeEnum,
            FollowingModeEnum followingModeEnum,
            IList<(double dimensionValue, double outputValue)> points,
            int plotLineCount = 7)
            => Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                interpolationTypeEnum,
                followingModeEnum,
                slowRate: 1.0,
                points,
                plotLineCount);

        /// <summary>
        /// Signal will have a cycle length of 8.
        /// </summary>
        protected void Test_Synthesizer_Interpolate_Base(
            CalculationEngineEnum calculationEngineEnum,
            InterpolationTypeEnum interpolationTypeEnum,
            FollowingModeEnum followingModeEnum,
            double slowRate,
            IList<(double dimensionValue, double outputValue)> points,
            int plotLineCount = 7)
        {
            // Transform this method's input into something the TestExecutor wants.
            IList<DimensionEnum> inputDimensionEnums = new[] { TestConstants.DEFAULT_DIMENSION_ENUM, DimensionEnum.Rate };
            IList<(double, double)> inputTuples = points.Select(point => (point.dimensionValue, slowRate)).ToArray();
            IList<double> outputValues = points.Select(point => point.outputValue).ToArray();

            TestExecutor.ExecuteTest(
                x => x.Interpolate(
                    x.New(
                        nameof(SystemPatchNames.Sin),
                        // Convert 0-8 to radians.
                        x.NewWithItemInlets(nameof(SystemPatchNames.Multiply), 
                              x.PatchInlet(TestConstants.DEFAULT_DIMENSION_ENUM),
                              x.Number(1.0 / 8.0),
                              x.Number(MathHelper.TWO_PI))),
                    x.PatchInlet(DimensionEnum.Rate),
                    interpolationTypeEnum,
                    TestConstants.DEFAULT_DIMENSION_ENUM,
                    "",
                    followingModeEnum),
                inputDimensionEnums,
                inputTuples,
                outputValues,
                calculationEngineEnum,
                new TestOptions(
                    significantDigits: null,
                    decimalDigits: 6,
                    mustPlot: true,
                    onlyUseOutputValuesForPlot: true,
                    plotLineCount: plotLineCount));
        }
    }
}