using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;
using JJ.Framework.Data;
using JJ.Framework.Testing.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal static class TestExecutor
    {
        public static double CalculateOneValue(IPatchCalculator patchCalculator, double time = 0.0)
        {
            const int frameCount = 1;
            var buffer = new float[1];
            patchCalculator.Calculate(buffer, frameCount, time);
            return buffer[0];
        }

        // With Func

        /// <summary> 1-dimensional with func. </summary>
        public static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double, double> func,
            DimensionEnum dimensionEnum,
            double[] inputValues,
            CalculationEngineEnum calculationEngineEnum,
            int? significantDigits = TestConstants.DEFAULT_SIGNIFICANT_DIGITS,
            int? decimalDigits = null,
            bool mustCompareZeroAndNonZeroOnly = false)
            => ExecuteTest(
                operatorFactoryDelegate,
                arr => func(arr[0]),
                new[] { dimensionEnum },
                new[] { inputValues },
                calculationEngineEnum,
                significantDigits,
                decimalDigits,
                mustCompareZeroAndNonZeroOnly);

        /// <summary> 2-dimensional cartesian product with func. </summary>
        public static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double, double, double> func,
            DimensionEnum xDimensionEnum,
            double[] xValues,
            DimensionEnum yDimensionEnum,
            double[] yValues,
            CalculationEngineEnum calculationEngineEnum,
            int? significantDigits = TestConstants.DEFAULT_SIGNIFICANT_DIGITS,
            int? decimalDigits = null,
            bool mustCompareZeroAndNonZeroOnly = false)
            => ExecuteTest(
                operatorFactoryDelegate,
                arr => func(arr[0], arr[1]),
                new[] { xDimensionEnum, yDimensionEnum },
                new[] { xValues, yValues },
                calculationEngineEnum,
                significantDigits,
                decimalDigits,
                mustCompareZeroAndNonZeroOnly);

        /// <summary> 3-dimensional cartesian product with func. </summary>
        public static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double, double, double, double> func,
            DimensionEnum xDimensionEnum,
            double[] xValues,
            DimensionEnum yDimensionEnum,
            double[] yValues,
            DimensionEnum zDimensionEnum,
            double[] zValues,
            CalculationEngineEnum calculationEngineEnum,
            int? significantDigits = TestConstants.DEFAULT_SIGNIFICANT_DIGITS,
            int? decimalDigits = null,
            bool mustCompareZeroAndNonZeroOnly = false)
            => ExecuteTest(
                operatorFactoryDelegate,
                arr => func(arr[0], arr[1], arr[2]),
                new[] { xDimensionEnum, yDimensionEnum, zDimensionEnum },
                new[] { xValues, yValues, zValues },
                calculationEngineEnum,
                significantDigits,
                decimalDigits,
                mustCompareZeroAndNonZeroOnly);

        /// <summary> N-dimensional cartesian product with func. </summary>
        private static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double[], double> funcWithArray,
            IList<DimensionEnum> inputDimensionEnums,
            IList<double[]> inputValues,
            CalculationEngineEnum calculationEngineEnum,
            int? significantDigits,
            int? decimalDigits,
            bool mustCompareZeroAndNonZeroOnly)
            => AssertInconclusiveHelper.WithConnectionInconclusiveAssertion(
                () =>
                {
                    // Infrastructure
                    using (IContext context = PersistenceHelper.CreateContext())
                    {
                        RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);

                        // Business
                        IList<double[]> inputPoints = inputValues.CrossJoin(x => x.ToArray()).ToArray();

                        // Execute Test
                        var patchVarConstTester = new PatchVarConstTester(
                            repositories,
                            calculationEngineEnum,
                            significantDigits,
                            decimalDigits,
                            mustCompareZeroAndNonZeroOnly);

                        (IList<string> logMessages, IList<string> errorMessages) =
                            patchVarConstTester.ExecuteTest(
                                operatorFactoryDelegate,
                                funcWithArray,
                                inputDimensionEnums,
                                inputPoints);

                        // Log Results
                        LogResults(logMessages, errorMessages, significantDigits, decimalDigits, mustCompareZeroAndNonZeroOnly);
                    }
                });

        // With ExpectedOutputValues

        /// <summary> 0-dimensional with expected output value. </summary>
        public static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            double expectedOutputValue,
            CalculationEngineEnum calculationEngineEnum,
            int? significantDigits = TestConstants.DEFAULT_SIGNIFICANT_DIGITS,
            int? decimalDigits = null,
            bool mustCompareZeroAndNonZeroOnly = false)
            => ExecuteTest(
                operatorFactoryDelegate,
                Array.Empty<DimensionEnum>(),
                Array.Empty<double[]>(),
                expectedOutputValue.AsArray(),
                calculationEngineEnum,
                significantDigits,
                decimalDigits,
                mustCompareZeroAndNonZeroOnly);

        /// <summary> 2-part tuples with expected output values. </summary>
        public static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            IList<DimensionEnum> inputDimensionEnums,
            IList<(double, double)> inputTuples,
            IList<double> expectedOutputValues,
            CalculationEngineEnum calculationEngineEnum,
            int? significantDigits = TestConstants.DEFAULT_SIGNIFICANT_DIGITS,
            int? decimalDigits = null,
            bool mustCompareZeroAndNonZeroOnly = false)
        {
            if (inputTuples == null) throw new ArgumentNullException(nameof(inputTuples));

            ExecuteTest(
                operatorFactoryDelegate,
                inputDimensionEnums,
                inputTuples.Select(x => new[] { x.Item1, x.Item2 }).ToArray(),
                expectedOutputValues,
                calculationEngineEnum,
                significantDigits,
                decimalDigits,
                mustCompareZeroAndNonZeroOnly);
        }

        /// <summary> N-dimensional tuples with expected output values. </summary>
        private static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            IList<DimensionEnum> inputDimensionEnums,
            IList<double[]> inputPoints,
            IList<double> expectedOutputValues,
            CalculationEngineEnum calculationEngineEnum,
            int? significantDigits,
            int? decimalDigits,
            bool mustCompareZeroAndNonZeroOnly)
            => AssertInconclusiveHelper.WithConnectionInconclusiveAssertion(
                () =>
                {
                    // Infrastructure
                    using (IContext context = PersistenceHelper.CreateContext())
                    {
                        RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);

                        // Business
                        var patchFacade = new PatchFacade(repositories);
                        Patch patch = patchFacade.CreatePatch();
                        var operatorFactory = new OperatorFactory(patch, repositories);
                        Outlet outlet = operatorFactoryDelegate(operatorFactory);

                        // Execute Test
                        var testExecutor = new OutletTester(
                            outlet,
                            patchFacade,
                            calculationEngineEnum,
                            significantDigits,
                            decimalDigits,
                            mustCompareZeroAndNonZeroOnly);

                        (IList<string> logMessages, IList<string> errorMessages) = testExecutor.ExecuteTest(
                            inputDimensionEnums,
                            inputPoints,
                            expectedOutputValues);

                        // Log Results
                        LogResults(logMessages, errorMessages, significantDigits, decimalDigits, mustCompareZeroAndNonZeroOnly);
                    }
                });

        // Helpers

        private static void LogResults(
            IList<string> logMessages,
            IList<string> errorMessages,
            int? significantDigits,
            int? decimalDigits,
            bool mustCompareZeroAndNonZeroOnly)
        {
            string note = MessageFormatter.GetNote(significantDigits, decimalDigits, mustCompareZeroAndNonZeroOnly);

            logMessages.ForEach(Console.WriteLine);

            if (!string.IsNullOrEmpty(logMessages.LastOrDefault()))
            {
                Console.WriteLine("");
            }

            Console.WriteLine(note);

            if (errorMessages.Any())
            {
                Assert.Fail(string.Join(Environment.NewLine, errorMessages.Concat(note)));
            }
        }
    }
}