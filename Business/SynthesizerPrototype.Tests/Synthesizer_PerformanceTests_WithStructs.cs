using System;
using System.Diagnostics;
using JJ.Business.SynthesizerPrototype.Dto;
using JJ.Business.SynthesizerPrototype.Tests.Helpers;
using JJ.Business.SynthesizerPrototype.Tests.Helpers.WithStructs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Business.SynthesizerPrototype.WithStructs.CopiedCode.From_JJ_Business_SynthesizerPrototype;
using JJ.Business.SynthesizerPrototype.WithStructs.Calculation;

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

			var calculator = OperatorCalculatorFactory.CreateOperatorCalculatorStructure_8Partials(dimensionStack);

			var stopWatch = Stopwatch.StartNew();

			for (int i = 0; i < 500000; i++)
			{
				calculator.Calculate();
			}

			stopWatch.Stop();

			string message = TestHelper.GetPerformanceInfoMessage(500000, stopWatch.Elapsed);

			Assert.Inconclusive(message);
		}

		[TestMethod]
		public void PerformanceTest_SynthesizerPrototype_WithStructs_WithDto()
		{
			var dimensionStack = new DimensionStack();
			dimensionStack.Push(0.0);

			IOperatorDto dto = OperatorDtoFactory.CreateOperatorDto_8Partials();
			IOperatorCalculator calculator = OperatorCalculatorFactory.CreateOperatorCalculatorFromDto(dto, dimensionStack);

			var stopWatch = Stopwatch.StartNew();

			for (int i = 0; i < 500000; i++)
			{
				calculator.Calculate();
			}

			stopWatch.Stop();

			string message = TestHelper.GetPerformanceInfoMessage(500000, stopWatch.Elapsed);

			Assert.Inconclusive(message);
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

				double value = calculator.Calculate();

				t += dt;
			}
		}

		[TestMethod]
		public void Debug_SynthesizerPrototype_RuntimeGeneric_DoesNotInline_ButEquates_CompileTimeGeneric_WhichDoesInline()
		{
			var dimensionStack = new DimensionStack();

			dimensionStack.Push(0.123456789);

			var compileTimeStruct = OperatorCalculatorFactory.CreateOperatorCalculatorStructure_SinglePartial(dimensionStack);

			IOperatorDto dto = OperatorDtoFactory.CreateOperatorDto_SinglePartial();
			var runTimeStruct = OperatorCalculatorFactory.CreateOperatorCalculatorFromDto(dto, dimensionStack);

			Type compileTimeStructType = compileTimeStruct.GetType();
			Type runTimeStructType = runTimeStruct.GetType();
			bool typesAreEqual = compileTimeStructType == runTimeStructType;
			Assert.IsTrue(typesAreEqual, "designTimeStruct is not equal to runTimeStruct.");

			double compileTimeStructValue = compileTimeStruct.Calculate();
			double runTimeStructValue = runTimeStruct.Calculate();
		}
	}
}
