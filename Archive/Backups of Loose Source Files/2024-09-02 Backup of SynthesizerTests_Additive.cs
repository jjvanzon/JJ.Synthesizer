using JJ.Business.Synthesizer.Calculation.AudioFileOutputs;
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
	public class SynthesizerTests_Additive
	{
		private IContext context = PersistenceHelper.CreateContext();

		[TestCleanup]
		public void CleanUp()
		{
			context?.Dispose();
		}

		/// <summary>
		/// Arpeggio sound with harmonics, a high-pitch sample for attack,
		/// separate curves for each partial, triggers a wav header auto-detect.
		/// </summary>
		[TestMethod]
		public void Test_Synthesizer_Additive_Sines_And_Samples()
		{
			Sample sample = CreateSample();

			Curve curve1;
			Curve curve2;
			Curve curve3;
			Curve curve4;
			{
				var curveFactory = TestHelper.CreateCurveFactory(context);

				double noteDuration = 2.5;

				curve1 = curveFactory.CreateCurve(noteDuration,
					0.00, 0.80, 1.00, null, null, null, null, null,
					0.25, null, null, null, null, null, null, null,
					0.10, null, null, 0.02, null, null, null, 0.00);

				curve2 = curveFactory.CreateCurve(noteDuration,
					0.00, 1.00, 0.80, null, null, null, null, null,
					0.10, null, null, null, null, null, null, null,
					0.05, null, null, 0.01, null, null, null, 0.00);

				curve3 = curveFactory.CreateCurve(noteDuration,
					0.30, 0.00, 0.30, null, null, null, null, null,
					0.10, null, null, null, null, null, null, null,
					0.15, null, null, 0.05, null, null, null, 0.00);

				curve4 = curveFactory.CreateCurve(noteDuration,
					1.00, 0.50, 0.20, null, null, null, null, 0.00,
					null, null, null, null, null, null, null, null,
					null, null, null, null, null, null, null, null,
					null, null, null, null, null, null, null, 0.00);
			}

			var x = TestHelper.CreateOperatorFactory(context);

			Outlet buildNote(double frequency)
			{
				return x.Adder
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
					x.TimeDivide
					(
						x.Multiply(x.Multiply
						(
							x.Value(3),
							x.Sample(sample)),
							x.CurveIn(curve4)
						),
						x.Value(2.0 * frequency / 440.0)
					),
					x.TimeDivide
					(
						x.Multiply(x.Multiply
						(
							x.Value(1),
							x.Sample(sample)),
							x.CurveIn(curve4)
						),
						x.Value(7.0 * frequency / 440.0)
					)
				);
			}

			// Build melody
			double frequency1 = 440.0; 
			double volume1 = 0.9;
			double frequency2 = 440.0 * Math.Pow(2.0, 7.0 / 12.0); // (2 ^ 1/12 = frequency factor for a semi-tone.)
			double volume2 = 1.0;
			double frequency3 = 440.0 * Math.Pow(2.0, 2.0 / 12.0);
			double volume3 = 0.5;
			double frequency4 = 440.0 * Math.Pow(2.0, 4.0 / 12.0);
			double volume4 = 0.7;

			Outlet outlet = x.Adder
			(
				x.Multiply(x.Value(volume1), buildNote(frequency1)),
				x.Multiply(x.Value(volume2), x.TimeAdd(buildNote(frequency2), x.Value(0.2))),
				x.Multiply(x.Value(volume3), x.TimeAdd(buildNote(frequency3), x.Value(0.4))),
				x.Multiply(x.Value(volume4), x.TimeAdd(buildNote(frequency4), x.Value(0.6)))
			);

			// Crate AudioFileOutput
			AudioFileOutput audioFileOutput = CreateAudioFileOutput(outlet);

			// Assert Entities
			AssertEntities(sample, curve1, curve2, curve3, outlet, audioFileOutput);

			// Calculate
			IAudioFileOutputCalculator audioFileOutputCalculator = 
				AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(audioFileOutput);
			var sw = Stopwatch.StartNew();
			audioFileOutputCalculator.Execute();
			sw.Stop();

			// Report
			string filePath = Path.GetFullPath(audioFileOutput.FilePath);
			Assert.Inconclusive($"{sw.ElapsedMilliseconds}ms{Environment.NewLine}Output file: {filePath}");
		}

		private Sample CreateSample()
		{
			SampleManager sampleManager = TestHelper.CreateSampleManager(context);
			Sample sample = sampleManager.CreateSample(TestHelper.GetViolin16BitMono44100WavStream());

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

			return sample;
		}

		private AudioFileOutput CreateAudioFileOutput(Outlet outlet)
		{
			AudioFileOutputManager audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(context);
			AudioFileOutput audioFileOutput = audioFileOutputManager.CreateAudioFileOutput();
			audioFileOutput.AudioFileOutputChannels[0].Outlet = outlet;
			audioFileOutput.FilePath = $"{MethodBase.GetCurrentMethod().Name}.wav";
			audioFileOutput.Duration = 3.6;
			audioFileOutput.Amplifier = Int16.MaxValue / 8;

			return audioFileOutput;
		}

		private void AssertEntities(Sample sample, Curve curve1, Curve curve2, Curve curve3, Outlet outlet, AudioFileOutput audioFileOutput)
		{
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
		}
	}
}
