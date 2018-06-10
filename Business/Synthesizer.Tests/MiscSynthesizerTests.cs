using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Common;
using JJ.Framework.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable SuggestVarOrType_SimpleTypes
// ReSharper disable UnusedVariable
// ReSharper disable RedundantAssignment
// ReSharper disable CollectionNeverQueried.Local
// ReSharper disable NotAccessedVariable

namespace JJ.Business.Synthesizer.Tests
{
	[TestClass]
	public class MiscSynthesizerTests
	{
		private const int DEFAULT_SAMPLING_RATE = 44100;
		private const int DEFAULT_CHANNEL_COUNT = 1;
		private const int DEFAULT_CHANNEL_INDEX = 0;

		[TestMethod]
		public void Test_Synthesizer()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);

				var patchFacade = new PatchFacade(repositories);
				Patch patch = patchFacade.CreatePatch();
				var x = new OperatorFactory(patch, repositories);

				var add = x.Add(x.Number(2), x.Number(3));
				var subtract = x.Subtract(add, x.Number(1));

				IPatchCalculator calculator1 = patchFacade.CreateCalculator(
					add,
					DEFAULT_SAMPLING_RATE,
					DEFAULT_CHANNEL_COUNT,
					DEFAULT_CHANNEL_INDEX,
					new CalculatorCache());
				double value = TestHelper.CalculateOneValue(calculator1);
				Assert.AreEqual(5, value, 0.0001);

				IPatchCalculator calculator2 = patchFacade.CreateCalculator(
					subtract,
					DEFAULT_SAMPLING_RATE,
					DEFAULT_CHANNEL_COUNT,
					DEFAULT_CHANNEL_INDEX,
					new CalculatorCache());
				value = TestHelper.CalculateOneValue(calculator2);
				Assert.AreEqual(4, value, 0.0001);

				// Test recursive validator
				CultureHelper.SetCurrentCultureName("nl-NL");

				add.Inlets.First().InputOutlet = null;
				var valueOperatorWrapper = new Number_OperatorWrapper(subtract.Inputs[DimensionEnum.B].Operator)
				{
					Number = 0
				};
				subtract.WrappedOperator.Inlets[0].Name = "134";

				//IValidator validator2 = new OperatorValidator_Recursive(subtract.Operator, repositories.CurveRepository, repositories.SampleRepository, repositories.DocumentRepository, alreadyDone: new HashSet<object>());
				//IValidator warningValidator = new OperatorWarningValidator_Recursive(subtract.Operator, repositories.SampleRepository);
			}
		}

		[TestMethod]
		public void Test_Synthesizer_AddValidator_IsValidTrue() => Assert.Inconclusive("Test method was outcommented");

	    [TestMethod]
		public void Test_Synthesizer_Add()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);

				var patchFacade = new PatchFacade(repositories);
				Patch patch = patchFacade.CreatePatch();

				var x = new OperatorFactory(patch, repositories);

				Number_OperatorWrapper val1 = x.Number(1);
				Number_OperatorWrapper val2 = x.Number(2);
				Number_OperatorWrapper val3 = x.Number(3);
				OperatorWrapper add = x.Add(val1, val2, val3);

				//IValidator validator = new OperatorValidator_Adder(adder.Operator);
				//validator.Verify();

				IPatchCalculator calculator = patchFacade.CreateCalculator(
					add,
					DEFAULT_SAMPLING_RATE,
					DEFAULT_CHANNEL_COUNT,
					DEFAULT_CHANNEL_INDEX,
					new CalculatorCache());
				double value = TestHelper.CalculateOneValue(calculator);

				//adder.Operator.Inlets[0].Name = "qwer";
				//IValidator validator2 = new OperatorValidator_Adder(adder.Operator);
				//validator2.Verify();
			}
		}

		[TestMethod]
		public void Test_Synthesizer_ShorterCodeNotation()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);

				var patchFacade = new PatchFacade(repositories);
				Patch patch = patchFacade.CreatePatch();
				var x = new OperatorFactory(patch, repositories);

				var subtract = x.Subtract(x.Add(x.Number(2), x.Number(3)), x.Number(1));

				var subtract2 = x.Subtract(
					x.Add(
						x.Number(2),
						x.Number(3)
					),
					x.Number(1)
				);
			}
		}

		[TestMethod]
		public void Test_Synthesizer_SineWithCurve_InterpretedMode()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);

				var patchFacade = new PatchFacade(repositories);
				Patch patch = patchFacade.CreatePatch();
				var x = new OperatorFactory(patch, repositories);

				var outlet = x.MultiplyWithOrigin(x.Curve(1, DimensionEnum.Time, "", 0, 1, 0.8, null, null, 0.8, 0), x.Sine(x.Number(440)));

				CultureHelper.SetCurrentCultureName("nl-NL");

				//IValidator[] validators = 
				//{
				//	new OperatorValidator_Versatile(outlet.Operator, repositories.DocumentRepository),
				//	new OperatorWarningValidator_Versatile(outlet.Operator)
				//};
				//validators.ForEach(y => y.Verify());

				VoidResult result = patchFacade.SavePatch(patch);
				if (!result.Successful)
				{
					string messages = string.Join(", ", result.Messages);
					throw new Exception(messages);
				}

				var calculator = patchFacade.CreateCalculator(
					outlet,
					DEFAULT_SAMPLING_RATE,
					DEFAULT_CHANNEL_COUNT,
					DEFAULT_CHANNEL_INDEX,
					new CalculatorCache());

				var times = new[]
				{
					0.00,
					0.05,
					0.10,
					0.15,
					0.20,
					0.25,
					0.30,
					0.35,
					0.40,
					0.45,
					0.50,
					0.55,
					0.60,
					0.65,
					0.70,
					0.75,
					0.80,
					0.85,
					0.90,
					0.95,
					1.00
				};

				var values = new double[times.Length];

				foreach (double time in times)
				{
					values[0] = TestHelper.CalculateOneValue(calculator, time);
				}
			}
		}

		[TestMethod]
		public void Test_Synthesizer_OptimizedPatchCalculator()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);

				var patchFacade = new PatchFacade(repositories);
				Patch patch = patchFacade.CreatePatch();
				var x = new OperatorFactory(patch, repositories);

				Outlet outlet = x.Add(x.Number(1), x.Number(2));
				IPatchCalculator calculator = patchFacade.CreateCalculator(
					outlet,
					DEFAULT_SAMPLING_RATE,
					DEFAULT_CHANNEL_COUNT,
					DEFAULT_CHANNEL_INDEX,
					new CalculatorCache());
				double result = TestHelper.CalculateOneValue(calculator);
				Assert.AreEqual(3.0, result, 0.0001);
			}
		}

		[TestMethod]
		public void Test_Synthesizer_PatchCalculator_WithNullInlet()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);

				var patchFacade = new PatchFacade(repositories);
				Patch patch = patchFacade.CreatePatch();
				var x = new OperatorFactory(patch, repositories);

				Outlet outlet = x.Add(null, x.Number(2));
				IPatchCalculator calculator = patchFacade.CreateCalculator(
					outlet,
					DEFAULT_SAMPLING_RATE,
					DEFAULT_CHANNEL_COUNT,
					DEFAULT_CHANNEL_INDEX,
					new CalculatorCache());
				double result = TestHelper.CalculateOneValue(calculator);
				Assert.AreEqual(2.0, result, 0.000000001);
			}
		}

		[TestMethod]
		public void Test_Synthesizer_PatchCalculator_Nulls()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);

				var patchFacade = new PatchFacade(repositories);
				Patch patch = patchFacade.CreatePatch();
				var x = new OperatorFactory(patch, repositories);

				Outlet outlet = x.Add(x.Number(1), x.Add(x.Number(2), null));
				IPatchCalculator calculator = patchFacade.CreateCalculator(
					outlet,
					DEFAULT_SAMPLING_RATE,
					DEFAULT_CHANNEL_COUNT,
					DEFAULT_CHANNEL_INDEX,
					new CalculatorCache());
				double result = TestHelper.CalculateOneValue(calculator);
				Assert.AreEqual(3.0, result, 0.000000001);
			}
		}

		[TestMethod]
		public void Test_Synthesizer_PatchCalculator_NestedOperators()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);

				var patchFacade = new PatchFacade(repositories);
				Patch patch = patchFacade.CreatePatch();
				var x = new OperatorFactory(patch, repositories);

				Outlet outlet = x.Add(x.Add(x.Number(1), x.Number(2)), x.Number(4));
				IPatchCalculator calculator = patchFacade.CreateCalculator(
					outlet,
					DEFAULT_SAMPLING_RATE,
					DEFAULT_CHANNEL_COUNT,
					DEFAULT_CHANNEL_INDEX,
					new CalculatorCache());
				double result = TestHelper.CalculateOneValue(calculator);
				Assert.AreEqual(7.0, result, 0.000000001);
			}
		}

		[TestMethod]
		public void Test_Synthesizer_NoiseOperator()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				var repositories = PersistenceHelper.CreateRepositories(context);

				var audioFileOutputFacade = new AudioFileOutputFacade(new AudioFileOutputRepositories(repositories));
				var patchFacade = new PatchFacade(repositories);
				Patch patch = patchFacade.CreatePatch();
				var x = new OperatorFactory(patch, repositories);

				Outlet outlet = x.MultiplyWithOrigin(x.Noise(), x.Number(short.MaxValue));

				IPatchCalculator patchCalculator = patchFacade.CreateCalculator(
					outlet,
					DEFAULT_SAMPLING_RATE,
					DEFAULT_CHANNEL_COUNT,
					DEFAULT_CHANNEL_INDEX,
					new CalculatorCache());

				AudioFileOutput audioFileOutput = audioFileOutputFacade.Create();
				audioFileOutput.FilePath = "Test_Synthesizer_NoiseOperator.wav";
				audioFileOutput.Duration = 20;
				audioFileOutput.LinkTo(outlet);

				// Execute once to fill cache(s).
				audioFileOutputFacade.WriteFile(audioFileOutput, patchCalculator);

				Stopwatch sw = Stopwatch.StartNew();
				audioFileOutputFacade.WriteFile(audioFileOutput, patchCalculator);
				sw.Stop();

				double ratio = sw.Elapsed.TotalSeconds / audioFileOutput.Duration;
				string message = $"Ratio: {ratio * 100:0.00}%, {sw.ElapsedMilliseconds}ms.";

				// Also test interpreted calculator
				IPatchCalculator calculator = patchFacade.CreateCalculator(
					outlet,
					DEFAULT_SAMPLING_RATE,
					DEFAULT_CHANNEL_COUNT,
					DEFAULT_CHANNEL_INDEX,
					new CalculatorCache());

				// ReSharper disable once JoinDeclarationAndInitializer
				double value;

				value = TestHelper.CalculateOneValue(calculator, 0.2);
				value = TestHelper.CalculateOneValue(calculator, 0.2);

				value = TestHelper.CalculateOneValue(calculator, 0.3);
				value = TestHelper.CalculateOneValue(calculator, 0.3);

				Assert.Inconclusive(message);
			}
		}

		[TestMethod]
		public void Test_Synthesizer_InterpolateOperator_ConstantSamplingRate_Noise()
		{
			const double duration = 2;
			const int outputSamplingRate = 100;
			const int alternativeSamplingRate = 25;
			const int amplification = 20000;

			using (IContext context = PersistenceHelper.CreateContext())
			{
				RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);

				var audioFileOutputFacade = new AudioFileOutputFacade(new AudioFileOutputRepositories(repositories));
				var patchFacade = new PatchFacade(repositories);
				Patch patch = patchFacade.CreatePatch();
				var x = new OperatorFactory(patch, repositories);

				Outlet noise = x.MultiplyWithOrigin(x.Noise(), x.Number(amplification));
				Outlet interpolatedNoise = x.Interpolate(noise, x.Number(alternativeSamplingRate));

				// ReSharper disable once JoinDeclarationAndInitializer
				IPatchCalculator patchCalculator;

				AudioFileOutput audioFileOutput = audioFileOutputFacade.Create();
				audioFileOutput.Duration = duration;

				audioFileOutput.FilePath = "Test_Synthesizer_InterpolateOperator_ConstantSamplingRate_Noise_Input.wav";
				audioFileOutput.SamplingRate = outputSamplingRate;
				audioFileOutput.LinkTo(noise);
				patchCalculator = patchFacade.CreateCalculator(
					noise,
					DEFAULT_SAMPLING_RATE,
					DEFAULT_CHANNEL_COUNT,
					DEFAULT_CHANNEL_INDEX,
					new CalculatorCache());
				audioFileOutputFacade.WriteFile(audioFileOutput, patchCalculator);

				audioFileOutput.FilePath = "Test_Synthesizer_InterpolateOperator_ConstantSamplingRate_Noise_WithLowerSamplingRate.wav";
				audioFileOutput.SamplingRate = alternativeSamplingRate;
				audioFileOutput.LinkTo(noise);
				patchCalculator = patchFacade.CreateCalculator(
					noise,
					DEFAULT_SAMPLING_RATE,
					DEFAULT_CHANNEL_COUNT,
					DEFAULT_CHANNEL_INDEX,
					new CalculatorCache());
				audioFileOutputFacade.WriteFile(audioFileOutput, patchCalculator);

				audioFileOutput.FilePath = "Test_Synthesizer_InterpolateOperator_ConstantSamplingRate_Noise_WithInterpolateOperator.wav";
				audioFileOutput.SamplingRate = outputSamplingRate;
				audioFileOutput.LinkTo(interpolatedNoise);
				patchCalculator = patchFacade.CreateCalculator(
					interpolatedNoise,
					DEFAULT_SAMPLING_RATE,
					DEFAULT_CHANNEL_COUNT,
					DEFAULT_CHANNEL_INDEX,
					new CalculatorCache());

				// Only test performance here and not in the other tests.

				// Execute once to fill cache(s).
				audioFileOutputFacade.WriteFile(audioFileOutput, patchCalculator);

				Stopwatch sw = Stopwatch.StartNew();
				audioFileOutputFacade.WriteFile(audioFileOutput, patchCalculator);
				sw.Stop();

				double ratio = sw.Elapsed.TotalSeconds / audioFileOutput.Duration;
				string message = $"Ratio: {ratio * 100:0.00}%, {sw.ElapsedMilliseconds}ms.";

				//// Also test interpreted calculator
				//IPatchCalculator calculator = patchFacade.CreateCalculator(false, outlet);
				//double value = calculator.Calculate(0.2);
				//value = calculator.Calculate(0.2);
				//value = calculator.Calculate(0.3);
				//value = calculator.Calculate(0.3);

				Assert.Inconclusive(message);
			}
		}

		[TestMethod]
		public void Test_Synthesizer_InterpolateOperator_Sine()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);

				var audioFileOutputFacade = new AudioFileOutputFacade(new AudioFileOutputRepositories(repositories));

				var patchFacade = new PatchFacade(repositories);
				Patch patch = patchFacade.CreatePatch();
				var x = new OperatorFactory(patch, repositories);

				const double volume = 1;
				const double frequency = 1.0;
				Outlet sine = x.Multiply(x.Number(volume), x.Sine(x.Number(frequency)));

				const double newSamplingRate = 4;
				Outlet interpolated = x.Interpolate(sine, x.Number(newSamplingRate));

				// ReSharper disable once JoinDeclarationAndInitializer
				IPatchCalculator patchCalculator;

				AudioFileOutput audioFileOutput = audioFileOutputFacade.Create();
				audioFileOutput.Duration = 2;
				audioFileOutput.SamplingRate = 44100;

				audioFileOutput.FilePath = "Test_Synthesizer_InterpolateOperator_Sine_Input.wav";
				audioFileOutput.LinkTo(sine);
				patchCalculator = patchFacade.CreateCalculator(
					sine,
					DEFAULT_SAMPLING_RATE,
					DEFAULT_CHANNEL_COUNT,
					DEFAULT_CHANNEL_INDEX,
					new CalculatorCache());
				audioFileOutputFacade.WriteFile(audioFileOutput, patchCalculator);

				audioFileOutput.FilePath = "Test_Synthesizer_InterpolateOperator_Sine_Interpolated.wav";
				audioFileOutput.LinkTo(interpolated);
				patchCalculator = patchFacade.CreateCalculator(
					interpolated,
					DEFAULT_SAMPLING_RATE,
					DEFAULT_CHANNEL_COUNT,
					DEFAULT_CHANNEL_INDEX,
					new CalculatorCache());
				audioFileOutputFacade.WriteFile(audioFileOutput, patchCalculator);
			}
		}

		[TestMethod]
		public void Test_Synthesizer_SawUp()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);
				var patchFacade = new PatchFacade(repositories);
				Patch patch = patchFacade.CreatePatch();
				var x = new OperatorFactory(patch, repositories);

				var saw = x.SawUp(x.Number(0.5));

				IPatchCalculator patchCalculator = patchFacade.CreateCalculator(
					saw,
					DEFAULT_SAMPLING_RATE,
					DEFAULT_CHANNEL_COUNT,
					DEFAULT_CHANNEL_INDEX,
					new CalculatorCache());

				double[] times =
				{
					0.00,
					0.25,
					0.50,
					0.75,
					1.00,
					1.25,
					1.50,
					1.75,
					2.00
				};

				double[] values = times.Select(time => TestHelper.CalculateOneValue(patchCalculator, time)).ToArray();
			}
		}

		[TestMethod]
		public void Test_Synthesizer_Triangle()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);
				var patchFacade = new PatchFacade(repositories);
				Patch patch = patchFacade.CreatePatch();
				var operatorFactory = new OperatorFactory(patch, repositories);
				var outlet = operatorFactory.Triangle(operatorFactory.Number(1));

				IPatchCalculator patchCalculator = patchFacade.CreateCalculator(
					outlet,
					DEFAULT_SAMPLING_RATE,
					DEFAULT_CHANNEL_COUNT,
					DEFAULT_CHANNEL_INDEX,
					new CalculatorCache());

				double[] times =
				{
					0.000,
					0.125,
					0.250,
					0.375,
					0.500,
					0.625,
					0.750,
					0.875,
					1.000,
					1.125,
					1.250,
					1.375,
					1.500,
					1.625,
					1.750,
					1.875,
					2.000
				};

				double[] values = times.Select(time => TestHelper.CalculateOneValue(patchCalculator, time)).ToArray();
			}
		}

		[TestMethod]
		public void Test_Synthesizer_ValidateAllRootDocuments()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				var repositories = PersistenceHelper.CreateRepositories(context);
				var documentFacade = new DocumentFacade(repositories);

				IList<string> messages = new List<string>();

				IEnumerable<Document> rootDocuments = repositories.DocumentRepository.GetAll();
				foreach (Document rootDocument in rootDocuments)
				{
					IResult result = documentFacade.Save(rootDocument);
					messages.AddRange(result.Messages);
				}

				if (messages.Count > 0)
				{
					string formattedMessages = string.Join(" ", messages);
					throw new Exception(formattedMessages);
				}
			}
		}

		[TestMethod]
		public void Test_Synthesizer_OperatorFactory_GenericMethods()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				var repositories = PersistenceHelper.CreateRepositories(context);
				var patchFacade = new PatchFacade(repositories);

				Patch patch = patchFacade.CreatePatch();

				var x = new OperatorFactory(patch, repositories);
				x.New("DivideWithOrigin");
			}
		}
	}
}