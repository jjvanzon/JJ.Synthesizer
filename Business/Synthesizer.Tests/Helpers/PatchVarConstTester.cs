using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;
using JJ.Framework.Data;
using JJ.Framework.Testing.Data;

// ReSharper disable ParameterTypeCanBeEnumerable.Global

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal class PatchVarConstTester : IDisposable
    {
        private static readonly double?[] _specialConstsToTest = { null, 0, 1, 2 };

        private IContext _context;
        private readonly SystemFacade _systemFacade;
        private readonly PatchFacade _patchFacade;
        private readonly RepositoryWrapper _repositories;

        public PatchVarConstTester()
        {
            AssertInconclusiveHelper.WithConnectionInconclusiveAssertion(() => _context = PersistenceHelper.CreateContext());

            _repositories = PersistenceHelper.CreateRepositories(_context);

            _systemFacade = new SystemFacade(_repositories.DocumentRepository);
            _patchFacade = new PatchFacade(_repositories);
        }

        ~PatchVarConstTester() => Dispose();

        public void Dispose() => _context?.Dispose();

        public (IList<string>, IList<string>) ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            IList<double> expectedOutputValues,
            IList<DimensionEnum> inputDimensionEnums,
            IList<double[]> inputPoints,
            CalculationEngineEnum calculationEngineEnum,
            bool mustCompareZeroAndNonZeroOnly)
        {
            // Create Patch
            Patch patch = _patchFacade.CreatePatch();
            var operatorFactory = new OperatorFactory(patch, _repositories);
            Outlet outlet = operatorFactoryDelegate(operatorFactory);

            // Execute test
            var testExecutor = new OutletTester(outlet, _patchFacade, calculationEngineEnum, mustCompareZeroAndNonZeroOnly);
            return testExecutor.ExecuteTest(inputDimensionEnums, inputPoints, expectedOutputValues);
        }

        public (IList<string> logMessages, IList<string> errorMessages) ExecuteTest(
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

                // Create Patch
                Patch patch = _patchFacade.CreatePatch();
                var operatorFactory = new OperatorFactory(patch, _repositories);
                Outlet outlet = operatorFactoryDelegate(operatorFactory);

                // Replace Vars with Consts
                var varConstReplacer = new PatchVarConstReplacer(_systemFacade, _patchFacade);
                varConstReplacer.ReplaceVarsWithConstsIfNeeded(patch, consts);

                // Execute test
                var outletTester = new OutletTester(outlet, _patchFacade, calculationEngineEnum, mustCompareZeroAndNonZeroOnly);

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

            logMessages.Add(TestMessageFormatter.Note);

            if (errorMessages.Any())
            {
                errorMessages.Add(TestMessageFormatter.Note);
            }

            return (logMessages, errorMessages);
        }
    }
}