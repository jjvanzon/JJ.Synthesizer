using JJ.Business.Synthesizer.Calculation.AudioFileOutputs;
using JJ.Business.Synthesizer.Factories;
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
	/// More tests written upon retro-actively isolating older synthesizer versions.
	/// </summary>
	internal class AdditiveTester
	{
		private readonly IContext _context;
		private readonly SampleManager _sampleManager;
		private readonly CurveFactory _curveFactory;
		private readonly OperatorFactory _operatorFactory;

		public AdditiveTester(IContext context)
		{
			_context = context;
			_sampleManager = TestHelper.CreateSampleManager(_context);
			_curveFactory = TestHelper.CreateCurveFactory(_context);
			_operatorFactory = TestHelper.CreateOperatorFactory(_context);
		}

		/// <summary>
		/// Arpeggio sound with harmonics, a high-pitch sample for attack,
		/// separate curves for each partial, triggers a wav header auto-detect.
		/// </summary>
		public void Test_Synthesizer_Additive_Sines_And_Samples()
		{
			// Arrange
			double noteDuration = 2.5;
			double totalDuration = 3.1;

			Sample sample = CreateSample();

			Curve sine1Curve = _curveFactory.CreateCurve(noteDuration,
				0.00, 0.80, 1.00, null, null, null, null, null,
				0.25, null, null, null, null, null, null, null,
				0.10, null, null, 0.02, null, null, null, 0.00);

			Curve sine2Curve = _curveFactory.CreateCurve(noteDuration,
				0.00, 1.00, 0.80, null, null, null, null, null,
				0.10, null, null, null, null, null, null, null,
				0.05, null, null, 0.01, null, null, null, 0.00);

			Curve sine3Curve = _curveFactory.CreateCurve(noteDuration,
				0.30, 0.00, 0.30, null, null, null, null, null,
				0.10, null, null, null, null, null, null, null,
				0.15, null, null, 0.05, null, null, null, 0.00);

			Curve sampleCurve = _curveFactory.CreateCurve(noteDuration,
				1.00, 0.50, 0.20, null, null, null, null, 0.00,
				null, null, null, null, null, null, null, null,
				null, null, null, null, null, null, null, null);

			OperatorFactory x = _operatorFactory;

			double sine1Volume = 1.0;
			double sine2Volume = 0.7;
			double sine3Volume = 0.4;
			double sample1Volume = 3.0;
			double sample2Volume = 5.0;

			Outlet buildNote(double frequency)
			{
				return x.Adder
				(
					x.Sine
					(
						x.Multiply(x.CurveIn(sine1Curve), x.Value(sine1Volume)),
						x.Value(frequency)
					),
					x.Sine
					(
						x.Multiply(x.CurveIn(sine2Curve), x.Value(sine2Volume)),
						x.Multiply(x.Value(frequency), x.Value(2))
					),
					x.Sine
					(
						x.Multiply(x.CurveIn(sine3Curve), x.Value(sine3Volume)),
						x.Multiply(x.Value(frequency), x.Value(5))
					),
					x.TimeDivide
					(
						x.Multiply(x.Multiply
						(
							x.Value(sample1Volume),
							x.Sample(sample)),
							x.CurveIn(sampleCurve)
						),
						x.Value(2.0 * frequency / 440.0)
					),
					x.TimeDivide
					(
						x.Multiply(x.Multiply
						(
							x.Value(sample2Volume),
							x.Sample(sample)),
							x.CurveIn(sampleCurve)
						),
						x.Value(7.0 * frequency / 440.0)
					)
				);
			}

			// Build melody
			double frequencyA4 = 440.0;
			double frequencyB4 = 440.0 * Math.Pow(2.0, 2.0 / 12.0);
			double frequencyCSharp4 = 440.0 * Math.Pow(2.0, 4.0 / 12.0);
			double frequencyE4 = 440.0 * Math.Pow(2.0, 7.0 / 12.0); // (2 ^ 1/12 = frequency factor for a semi-tone.)
			
			double note1Volume = 0.9;
			double note2Volume = 1.0;
			double note3Volume = 0.5;
			double note4Volume = 0.7;

			Outlet outlet = x.Adder
			(
				x.Multiply(x.Value(note1Volume), buildNote(frequencyA4)),
				x.Multiply(x.Value(note2Volume), x.TimeAdd(buildNote(frequencyE4), x.Value(0.2))),
				x.Multiply(x.Value(note3Volume), x.TimeAdd(buildNote(frequencyB4), x.Value(0.4))),
				x.Multiply(x.Value(note4Volume), x.TimeAdd(buildNote(frequencyCSharp4), x.Value(0.6)))
			);

			AudioFileOutput audioFileOutput;
			{
				AudioFileOutputManager audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(_context);
				audioFileOutput = audioFileOutputManager.CreateAudioFileOutput();
				audioFileOutput.AudioFileOutputChannels[0].Outlet = outlet;
				audioFileOutput.FilePath = $"{MethodBase.GetCurrentMethod().Name}.wav";
				audioFileOutput.Duration = totalDuration;
				audioFileOutput.Amplifier = Int16.MaxValue / 3;
			}

			// Verify
			IValidator[] validators =
			{
				new CurveValidator(sine1Curve),
				new CurveValidator(sine2Curve),
				new CurveValidator(sine3Curve),
				new VersatileOperatorValidator(outlet.Operator),
				new AudioFileOutputValidator(audioFileOutput),
				new AudioFileOutputWarningValidator(audioFileOutput),
				new VersatileOperatorWarningValidator(outlet.Operator)
			};
			validators.ForEach(v => v.Verify());

			// Calculate
			IAudioFileOutputCalculator audioFileOutputCalculator = AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(audioFileOutput);
			var sw = Stopwatch.StartNew();
			audioFileOutputCalculator.Execute();
			sw.Stop();

			// Report
			string filePath = Path.GetFullPath(audioFileOutput.FilePath);
			Assert.Inconclusive($"{sw.ElapsedMilliseconds}ms{Environment.NewLine}Output file: {filePath}");
		}

		private Sample CreateSample()
		{
			Sample sample = _sampleManager.CreateSample(TestHelper.GetViolin16BitMono44100WavStream());

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

			// Validation
			_sampleManager.ValidateSample(sample).Verify();

			return sample;
		}
	}
}
