using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal static class PatchTester_MultipleConstVarVariations
    {
        private static readonly double?[] _specialConstsToTest = { null, 0, 1, 2 };

        public static (IList<string>, IList<string>) ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            IList<double> expectedOutputValues,
            IList<DimensionEnum> inputDimensionEnums,
            IList<double[]> inputPoints,
            CalculationEngineEnum calculationEngineEnum,
            bool mustCompareZeroAndNonZeroOnly)
        {
            // No const-var variations for now. Maybe later.
            IList<double?> consts = new double?[inputDimensionEnums.Count];

            // Execute test
            using (var testExecutor = new PatchTester_SingleConstVarVariation(
                calculationEngineEnum,
                operatorFactoryDelegate,
                consts,
                mustCompareZeroAndNonZeroOnly))
            {
                return testExecutor.ExecuteTest(inputDimensionEnums, inputPoints, expectedOutputValues);
            }
        }

        public static (IList<string> logMessages, IList<string> errorMessages) ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double[], double> func,
            IList<DimensionEnum> inputDimensionEnums,
            IList<double[]> inputPoints,
            CalculationEngineEnum calculationEngineEnum,
            bool mustCompareZeroAndNonZeroOnly)
        {
            if (inputDimensionEnums == null) throw new ArgumentNullException(nameof(inputDimensionEnums));

            var logMessages = new List<string>();
            var errorMessages = new List<string>();

            IList<double?[]> constsLists = CollectionHelper.Repeat(inputDimensionEnums.Count, () => _specialConstsToTest)
                                                           .CrossJoin(x => x.ToArray())
                                                           .DefaultIfEmpty(Array.Empty<double?>())
                                                           .ToArray();
            
            // Loop through special constants
            foreach (double?[] consts in constsLists)
            {
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

                string varConstMessage = TestMessageFormatter.TryGetVarConstMessage(inputDimensionEnums, consts);

                // Execute test
                using (var testExecutor = new PatchTester_SingleConstVarVariation(
                    calculationEngineEnum,
                    operatorFactoryDelegate,
                    consts,
                    mustCompareZeroAndNonZeroOnly))
                {
                    (IList<string> logMessages2, IList<string> errorMessages2) = testExecutor.ExecuteTest(
                        inputDimensionEnums,
                        inputPointsWithConsts,
                        expectedOutputValues);

                    if (!string.IsNullOrEmpty(varConstMessage)) logMessages.Add(varConstMessage);
                    logMessages.AddRange(logMessages2);

                    if (errorMessages2.Any())
                    {
                        errorMessages.Add("");
                        if (!string.IsNullOrEmpty(varConstMessage)) errorMessages.Add(varConstMessage);
                        errorMessages.AddRange(errorMessages2);
                    }
                }

                logMessages.Add("");
            }

            logMessages.Add(TestMessageFormatter.Note);

            if (errorMessages.Any())
            {
                errorMessages.Add(TestMessageFormatter.Note);
            }

            return (logMessages, errorMessages);
        }
    }
}