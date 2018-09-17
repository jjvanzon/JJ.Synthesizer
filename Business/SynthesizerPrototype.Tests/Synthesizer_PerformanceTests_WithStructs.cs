using System;
using System.Diagnostics;
using JJ.Business.SynthesizerPrototype.Dto;
using JJ.Business.SynthesizerPrototype.Tests.Helpers;
using JJ.Business.SynthesizerPrototype.Tests.Helpers.WithStructs;
using JJ.Business.SynthesizerPrototype.WithStructs.Calculation;
using JJ.Business.SynthesizerPrototype.WithStructs.CopiedCode.From_JJ_Business_SynthesizerPrototype;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.SynthesizerPrototype.Tests
{
	[TestClass]
	public class Synthesizer_PerformanceTests_WithStructs
	{
		[TestMethod]
		public void PerformanceTest_SynthesizerPrototype_WithStructs_NoDto()
		{
			var dimensionStack = new DimensionStack();
			dimensionStack.Push(0.0);

			// ReSharper disable once SuggestVarOrType_Elsewhere
			var calculator = OperatorCalculatorFactory.CreateOperatorCalculatorStructure_8Partials(dimensionStack);

			Stopwatch stopWatch = Stopwatch.StartNew();

			for (int i = 0; i < 500000; i++)
			{
				calculator.Calculate();
			}

			stopWatch.Stop();

			string message = TestHelper.GetPerformanceInfoMessage(500000, stopWatch.Elapsed);

		    Console.WriteLine(message);
		}

        [TestMethod]
		public void PerformanceTest_SynthesizerPrototype_WithStructs_WithDto()
		{
			var dimensionStack = new DimensionStack();
			dimensionStack.Push(0.0);

			IOperatorDto dto = OperatorDtoFactory.CreateOperatorDto_8Partials();
			IOperatorCalculator calculator = OperatorCalculatorFactory.CreateOperatorCalculatorFromDto(dto, dimensionStack);

			Stopwatch stopWatch = Stopwatch.StartNew();

			for (int i = 0; i < 500000; i++)
			{
				calculator.Calculate();
			}

			stopWatch.Stop();

			string message = TestHelper.GetPerformanceInfoMessage(500000, stopWatch.Elapsed);

		    Console.WriteLine(message);
		}

        [TestMethod]
		public void Debug_SynthesizerPrototype_WithStructs()
		{
			var dimensionStack = new DimensionStack();
			dimensionStack.Push(0.0);

			IOperatorDto dto = OperatorDtoFactory.CreateOperatorDto_8Partials();
			IOperatorCalculator calculator = OperatorCalculatorFactory.CreateOperatorCalculatorFromDto(dto, dimensionStack);

			double t = 0.0;
			const double dt = 1.0 / 500000.0;

			while (t <= 1.0)
			{
				dimensionStack.Set(t);

				// ReSharper disable once UnusedVariable
				double value = calculator.Calculate();

				t += dt;
			}
		}

		[TestMethod]
		public void Debug_SynthesizerPrototype_RuntimeGeneric_DoesNotInline_ButEquates_CompileTimeGeneric_WhichDoesInline()
		{
			var dimensionStack = new DimensionStack();

			dimensionStack.Push(0.123456789);

			// ReSharper disable once SuggestVarOrType_Elsewhere
			var compileTimeStruct = OperatorCalculatorFactory.CreateOperatorCalculatorStructure_SinglePartial(dimensionStack);

			IOperatorDto dto = OperatorDtoFactory.CreateOperatorDto_SinglePartial();
			IOperatorCalculator runTimeStruct = OperatorCalculatorFactory.CreateOperatorCalculatorFromDto(dto, dimensionStack);

			Type compileTimeStructType = compileTimeStruct.GetType();
			Type runTimeStructType = runTimeStruct.GetType();
			bool typesAreEqual = compileTimeStructType == runTimeStructType;
			Assert.IsTrue(typesAreEqual, "designTimeStruct is not equal to runTimeStruct.");

			// ReSharper disable once UnusedVariable
			double compileTimeStructValue = compileTimeStruct.Calculate();
			// ReSharper disable once UnusedVariable
			double runTimeStructValue = runTimeStruct.Calculate();
		}
	}
}
