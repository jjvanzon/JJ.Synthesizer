using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal static class PatchTester_WithConstVarVariations
    {
        private static readonly double?[] _specialConstsToTest = { null, 0, 1, 2 };

        public static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double[], double> func,
            IList<DimensionInfo> dimensionInfoList,
            CalculationMethodEnum calculationMethodEnum,
            bool mustCompareZeroAndNonZeroOnly)
        {
            var failureMessages = new List<string>();

            IList<DimensionEnum> inputDimensionEnums = dimensionInfoList.Select(x => x.DimensionEnum).ToArray();
            IList<double[]> inputPoints = dimensionInfoList.Select(x => x.InputValues).CrossJoin(x => x.ToArray()).ToArray();

            IList<double?[]> constsLists = CollectionHelper.Repeat(dimensionInfoList.Count, () => _specialConstsToTest)
                                                           .CrossJoin(x => x.ToArray())
                                                           .DefaultIfEmpty(Array.Empty<double?>())
                                                           .ToArray();

            // Loop through special constants
            foreach (double?[] consts in constsLists)
            {
                string varConstMessage = TestMessageFormatter.TryGetVarConstMessage(inputDimensionEnums, consts);

                if (!string.IsNullOrEmpty(varConstMessage))
                {
                    Console.WriteLine(varConstMessage);
                }

                // Replace input with constants
                IList<double[]> inputPointsWithConsts = inputPoints
                                                        .Select(point => consts.Zip(point, (x, y) => x ?? y).ToArray())
                                                        .DistinctMany()
                                                        .ToArray();

                IList<double> expectedOutputValues = inputPointsWithConsts.Select(func).ToArray();

                if (expectedOutputValues.Count == 0)
                {
                    expectedOutputValues = new List<double> { func(null) };
                }

                // Execute test
                using (var testExecutor = new PatchTester(calculationMethodEnum, operatorFactoryDelegate, consts, mustCompareZeroAndNonZeroOnly))
                {
                    List<string> failureMessages2 = testExecutor.ExecuteTest(inputDimensionEnums, inputPointsWithConsts, expectedOutputValues);

                    if (failureMessages2.Any())
                    {
                        failureMessages.Add("");
                        failureMessages.Add(TestMessageFormatter.TryGetVarConstMessage(inputDimensionEnums, consts));
                        failureMessages.AddRange(failureMessages2);
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine(TestMessageFormatter.Note);

            if (failureMessages.Any())
            {
                Assert.Fail(string.Join(Environment.NewLine, failureMessages) + " " + TestMessageFormatter.Note);
            }
        }
    }
}