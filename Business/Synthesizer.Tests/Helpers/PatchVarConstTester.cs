using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;

// ReSharper disable ParameterTypeCanBeEnumerable.Global

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal class PatchVarConstTester
    {
        private static readonly double?[] _specialConstsToTest = { null, 0, 1, 2 };

        private readonly RepositoryWrapper _repositories;
        private readonly CalculationEngineEnum _calculationEngineEnum;
        private readonly TestOptions _testOptions;
        private readonly SystemFacade _systemFacade;
        private readonly PatchFacade _patchFacade;

        public PatchVarConstTester(RepositoryWrapper repositories, CalculationEngineEnum calculationEngineEnum, TestOptions testOptions)
        {
            _calculationEngineEnum = calculationEngineEnum;
            _testOptions = testOptions ?? throw new ArgumentNullException(nameof(testOptions));
            _repositories = repositories ?? throw new ArgumentNullException(nameof(repositories));
            _systemFacade = new SystemFacade(repositories.DocumentRepository);
            _patchFacade = new PatchFacade(repositories);
        }

        public (IList<string> logMessages, IList<string> errorMessages) ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double[], double> func,
            IList<DimensionEnum> inputDimensionEnums,
            IList<double[]> inputPoints)
        {
            if (inputDimensionEnums == null) throw new ArgumentNullException(nameof(inputDimensionEnums));

            var logMessages = new List<string>();
            var errorMessages = new List<string>();

            IList<double?[]> constsLists =
                // Repeat a special consts list for each dimension.
                CollectionHelper.Repeat(inputDimensionEnums.Count, () => _specialConstsToTest)
                                // Produce all combinations of all special consts for all dimensions.
                                .CrossJoin(x => x.ToArray()) // (the ToArray is trivial.)
                                // Make this method work even when there are no input values.
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

                string varConstMessage = MessageFormatter.TryGetVarConstMessage(inputDimensionEnums, consts);

                // Create Patch
                Patch patch = _patchFacade.CreatePatch();
                var operatorFactory = new OperatorFactory(patch, _repositories);
                Outlet outlet = operatorFactoryDelegate(operatorFactory);

                // Replace Vars with Consts
                var varConstReplacer = new PatchVarConstReplacer(_systemFacade, _patchFacade);
                varConstReplacer.ReplaceVarsWithConstsIfNeeded(patch, consts);

                // Execute test
                var outletTester = new OutletTester(outlet, _patchFacade, _calculationEngineEnum, _testOptions);

                (IList<string> logMessages2, IList<string> errorMessages2) =
                    outletTester.ExecuteTest(
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

                logMessages.Add("");
            }

            return (logMessages, errorMessages);
        }
    }
}