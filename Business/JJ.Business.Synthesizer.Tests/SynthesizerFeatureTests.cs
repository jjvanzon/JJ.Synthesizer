using JJ.Business.Synthesizer.Calculation.AudioFileOutputs;
using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Validation.Entities;
using JJ.Business.Synthesizer.Warnings;
using JJ.Business.Synthesizer.Warnings.Entities;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace JJ.Business.Synthesizer.Tests
{
	/// <summary>
	/// Additional tests written upon retro-actively isolating older synthesizer versions.
	/// </summary>
	[TestClass]
	public class SynthesizerFeatureTests
	{
		/// <summary>
		/// Generates a Sine wave with a Volume Curve, testing both Block and Line Interpolation.
		/// Verifies data using (Warning)Validators and writes the output audio to a file.
		/// </summary>
		[TestMethod]
		public void Test_Synthesizer_Sine_With_Volume_Curve()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				// Arrange
				var curveFactory = TestHelper.CreateCurveFactory(context);
				Curve curve = curveFactory.CreateCurve
				(
					new NodeInfo(time: 0.00, value: 0.00, NodeTypeEnum.Line),
					new NodeInfo(time: 0.05, value: 0.95, NodeTypeEnum.Line),
					new NodeInfo(time: 0.10, value: 1.00, NodeTypeEnum.Line),
					new NodeInfo(time: 0.20, value: 0.60, NodeTypeEnum.Line),
					new NodeInfo(time: 0.80, value: 0.20, NodeTypeEnum.Block),
					new NodeInfo(time: 1.00, value: 0.00, NodeTypeEnum.Block),
					new NodeInfo(time: 1.20, value: 0.20, NodeTypeEnum.Block),
					new NodeInfo(time: 1.40, value: 0.08, NodeTypeEnum.Block),
					new NodeInfo(time: 1.60, value: 0.30, NodeTypeEnum.Line),
					new NodeInfo(time: 4.00, value: 0.00, NodeTypeEnum.Line)
				);

				Outlet outlet;
				{
					var x = TestHelper.CreateOperatorFactory(context);
					outlet = x.Sine(x.CurveIn(curve),x.Value(880));
				}

				AudioFileOutputManager audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(context);
				AudioFileOutput audioFileOutput = audioFileOutputManager.CreateAudioFileOutput();
				audioFileOutput.AudioFileOutputChannels[0].Outlet = outlet;
				audioFileOutput.FilePath = $"{MethodBase.GetCurrentMethod().Name}.wav";
				audioFileOutput.Duration = 4;
				audioFileOutput.Amplifier = 32000;

				// Verify
				IValidator[] validators =
				{
					new CurveValidator(curve),
					new VersatileOperatorValidator(outlet.Operator),
					new AudioFileOutputValidator(audioFileOutput),
					new AudioFileOutputWarningValidator(audioFileOutput),
					new VersatileOperatorWarningValidator(outlet.Operator)
				};
				validators.ForEach(x => x.Verify());

				// Calculate
				IAudioFileOutputCalculator audioFileOutputCalculator = AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(audioFileOutput);
				var sw = Stopwatch.StartNew();
				audioFileOutputCalculator.Execute();
				sw.Stop();

				// Report
				string filePath = Path.GetFullPath(audioFileOutput.FilePath);
				Assert.Inconclusive($"{sw.ElapsedMilliseconds}ms{Environment.NewLine}Output file: {filePath}");
			}
		}

		/// <summary>
		/// TODO: Some harmonics. and use high-pitch sample in attack. Use separate curves on each.
		/// Trigger wav header auto-detect.
		/// </summary>
		[TestMethod]
		public void Test_Synthesizer_Additive_Sines_And_Samples()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				// Arrange
				double duration = 4;
				double frequency = 440;
				double partialCount = 5;

				Sample sample;
				{
					SampleManager sampleManager = TestHelper.CreateSampleManager(context);
					sample = sampleManager.CreateSample(TestHelper.GetViolin16BitMono44100WavStream());

					// Skip over Header (from some other file format, that slipped into the audio data).
					sample.BytesToSkip = 62;

					// Skip for Sharper Attack
					sample.BytesToSkip += 1000;

					// Maximize and Normalize sample values (from 16-bit numbers to [-1, 1]).
					sample.Amplifier = 1.467 / Int16.MaxValue;

					// Tune to A 440Hz
					double octaveFactor = Math.Pow(2, -1);
					double intervalFactor = 4.0 / 5.0;
					double finetuneFactor = 0.94;
					sample.TimeMultiplier = 1.0 / (octaveFactor * intervalFactor * finetuneFactor);
				}

				Curve curve1;
				Curve curve2;
				Curve curve3;
				Curve curve4;
				{
					var curveFactory = TestHelper.CreateCurveFactory(context);

					curve1 = curveFactory.CreateCurve(duration,
						0.00, 0.80, 1.00, null, null, null, null, null,
						0.25, null, null, null, null, null, null, null,
						null, null, null, null, null, null, null, null,
						0.10, null, null, 0.02, null, null, null, 0.00);

					curve2 = curveFactory.CreateCurve(duration,
						0.00, 1.00, 0.80, null, null, null, null, null,
						0.10, null, null, null, null, null, null, null,
						null, null, null, null, null, null, null, null,
						0.05, null, null, 0.01, null, null, null, 0.00);

					curve3 = curveFactory.CreateCurve(duration,
						0.30, 0.00, 0.30, null, null, null, null, null,
						0.10, null, null, null, null, null, null, null,
						null, null, null, null, null, null, null, null,
						0.25, null, null, 0.10, null, null, null, 0.00);

					curve4 = curveFactory.CreateCurve(duration,
						1.00, 0.50, 0.20, null, null, null, null, 0.00,
						null, null, null, null, null, null, null, null,
						null, null, null, null, null, null, null, null,
						null, null, null, null, null, null, null, 0.00);
				}

				Outlet sampleOutlet;
				Outlet outlet;
				{
					var x = TestHelper.CreateOperatorFactory(context);

					outlet = x.Adder
					(
						x.Sine
						(
							x.Multiply(x.CurveIn(curve1), x.Value(1)), 
							x.Value(frequency)
						),
						x.Sine
						(
							x.Multiply(x.CurveIn(curve2), x.Value(0.7)), 
							x.Multiply(x.Value(frequency), x.Value(2))
						),
						x.Sine
						(
							x.Multiply(x.CurveIn(curve3), x.Value(0.4)), 
							x.Multiply(x.Value(frequency), x.Value(5))
						),
						sampleOutlet = x.TimeDivide
						(
							x.Multiply(x.Multiply
							(
								x.Value(2),
								x.Sample(sample)),
								x.CurveIn(curve4)
							),
							x.Value(2)
						),
						x.TimeDivide
						(
							x.Multiply(x.Multiply
							(
								x.Value(0.33),
								x.Sample(sample)),
								x.CurveIn(curve4)
							),
							x.Value(7)
						)
					);
				}

				AudioFileOutput audioFileOutput;
				{
					AudioFileOutputManager audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(context);
					audioFileOutput = audioFileOutputManager.CreateAudioFileOutput();
					audioFileOutput.AudioFileOutputChannels[0].Outlet = outlet;
					//audioFileOutput.AudioFileOutputChannels[0].Outlet = sampleOutlet;
					audioFileOutput.FilePath = $"{MethodBase.GetCurrentMethod().Name}.wav";
					audioFileOutput.Duration = duration;
					audioFileOutput.Amplifier = Int16.MaxValue / Math.Sqrt(partialCount);
				}

				// Verify
				IValidator[] validators =
				{
					new SampleValidator(sample),
					new CurveValidator(curve1),
					new CurveValidator(curve2),
					new CurveValidator(curve3),
					new VersatileOperatorValidator(outlet.Operator),
					new AudioFileOutputValidator(audioFileOutput),
					new AudioFileOutputWarningValidator(audioFileOutput),
					new VersatileOperatorWarningValidator(outlet.Operator)
				};
				validators.ForEach(x => x.Verify());

				// Calculate
				IAudioFileOutputCalculator audioFileOutputCalculator = AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(audioFileOutput);
				var sw = Stopwatch.StartNew();
				audioFileOutputCalculator.Execute();
				sw.Stop();

				// Report
				string filePath = Path.GetFullPath(audioFileOutput.FilePath);
				Assert.Inconclusive($"{sw.ElapsedMilliseconds}ms{Environment.NewLine}Output file: {filePath}");
			}
		}
	}
}
