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
		private const double NOTE_DURATION = 2.5;
		private const double TOTAL_DURATION = 3.1;

		private const double A4 = 440.0;
		private const double B4 = 493.88330125612413;
		private const double CSHARP4 = 554.36526195374415;
		private const double E4 = 659.25511382573984;

		private readonly IContext _context;
		private readonly SampleManager _sampleManager;
		private readonly CurveFactory _curveFactory;
		private readonly OperatorFactory _operatorFactory;
		private readonly AudioFileOutputManager _audioFileOutputManager;

		public AdditiveTester(IContext context)
		{
			_context = context;
			_sampleManager = TestHelper.CreateSampleManager(_context);
			_curveFactory = TestHelper.CreateCurveFactory(_context);
			_operatorFactory = TestHelper.CreateOperatorFactory(_context);
			_audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(_context);
		}

		/// <summary>
		/// Arpeggio sound with harmonics, a high-pitch sample for attack,
		/// separate curves for each partial, triggers a wav header auto-detect.
		/// </summary>
		public void Test_Synthesizer_Additive_Sines_And_Samples()
		{
			// Arrange

			Sample sample = CreateSample();

			Curve sine1Curve = GetSine1VolumeCurve();
			Curve sine2Curve = GetSine2VolumeCurve();
			Curve sine3Curve = GetSine3VolumeCurve();
			Curve sampleCurve = GetSampleVolumeCurve();

			OperatorFactory x = _operatorFactory;

			Outlet SteelDrum(double noteFrequency)
			{
				const double sine1Volume = 1.0;
				const double sine2Volume = 0.7;
				const double sine3Volume = 0.4;
				const double sample1Volume = 3.0;
				const double sample2Volume = 5.0;

				return x.Adder
				(
					x.Sine
					(
						x.Multiply(x.CurveIn(sine1Curve), x.Value(sine1Volume)),
						x.Value(noteFrequency)
					),
					x.Sine
					(
						x.Multiply(x.CurveIn(sine2Curve), x.Value(sine2Volume)),
						x.Multiply(x.Value(noteFrequency), x.Value(2))
					),
					x.Sine
					(
						x.Multiply(x.CurveIn(sine3Curve), x.Value(sine3Volume)),
						x.Multiply(x.Value(noteFrequency), x.Value(5))
					),
					x.TimeDivide
					(
						x.Multiply(x.Multiply
						(
							x.Value(sample1Volume),
							x.Sample(sample)),
							x.CurveIn(sampleCurve)
						),
						x.Value(2.0 * noteFrequency / 440.0)
					),
					x.TimeDivide
					(
						x.Multiply(x.Multiply
						(
							x.Value(sample2Volume),
							x.Sample(sample)),
							x.CurveIn(sampleCurve)
						),
						x.Value(7.0 * noteFrequency / 440.0)
					)
				);
			}

			// Build melody
			double note1Volume = 0.9;
			double note2Volume = 1.0;
			double note3Volume = 0.5;
			double note4Volume = 0.7;

			double note2Delay = 0.2;
			double note3Delay = 0.4;
			double note4Delay = 0.6;

			Outlet outlet = x.Adder
			(
				x.Multiply(x.Value(note1Volume), SteelDrum(A4)),
				x.Multiply(x.Value(note2Volume), x.TimeAdd(SteelDrum(E4), x.Value(note2Delay))),
				x.Multiply(x.Value(note3Volume), x.TimeAdd(SteelDrum(B4), x.Value(note3Delay))),
				x.Multiply(x.Value(note4Volume), x.TimeAdd(SteelDrum(CSHARP4), x.Value(note4Delay)))
			);

			AudioFileOutput audioFileOutput = CreateAudioFileOutput(outlet);
			audioFileOutput.FilePath = $"{MethodBase.GetCurrentMethod().Name}.wav";

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
			IAudioFileOutputCalculator audioFileOutputCalculator =
				AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(audioFileOutput);

			var stopWatch = Stopwatch.StartNew();
			audioFileOutputCalculator.Execute();
			stopWatch.Stop();

			// Report
			Assert.Inconclusive($"Calculation time: {stopWatch.ElapsedMilliseconds}ms{Environment.NewLine}" +
								$"Output file: {Path.GetFullPath(audioFileOutput.FilePath)}");
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


		private Curve _sine1VolumeCurve;

		private Curve GetSine1VolumeCurve()
		{
			if (_sine1VolumeCurve == null)
			{
				_sine1VolumeCurve = _curveFactory.CreateCurve(
					NOTE_DURATION,
					0.00, 0.80, 1.00, null, null, null, null, null,
					0.25, null, null, null, null, null, null, null,
					0.10, null, null, 0.02, null, null, null, 0.00);
			}
			return _sine1VolumeCurve;
		}

		private Curve _sine2VolumeCurve;

		private Curve GetSine2VolumeCurve()
		{
			if (_sine2VolumeCurve == null)
			{
				_sine2VolumeCurve = _curveFactory.CreateCurve(
					NOTE_DURATION,
					0.00, 1.00, 0.80, null, null, null, null, null,
					0.10, null, null, null, null, null, null, null,
					0.05, null, null, 0.01, null, null, null, 0.00);
			}
			return _sine2VolumeCurve;
		}

		private Curve _sine3VolumeCurve;

		private Curve GetSine3VolumeCurve()
		{
			if (_sine3VolumeCurve == null)
			{
				_sine3VolumeCurve = _curveFactory.CreateCurve(
					NOTE_DURATION,
					0.30, 0.00, 0.30, null, null, null, null, null,
					0.10, null, null, null, null, null, null, null,
					0.15, null, null, 0.05, null, null, null, 0.00);
			}
			return _sine3VolumeCurve;
		}


		private Curve _sampleVolumeCurve;

		private Curve GetSampleVolumeCurve()
		{
			if (_sampleVolumeCurve == null)
			{
				Curve _sampleVolumeCurve = _curveFactory.CreateCurve(
					NOTE_DURATION,
					1.00, 0.50, 0.20, null, null, null, null, 0.00,
					null, null, null, null, null, null, null, null,
					null, null, null, null, null, null, null, null);
			}
			return _sampleVolumeCurve;
		}

		private AudioFileOutput CreateAudioFileOutput(Outlet outlet)
		{
			AudioFileOutput audioFileOutput = _audioFileOutputManager.CreateAudioFileOutput();
			audioFileOutput.AudioFileOutputChannels[0].Outlet = outlet;
			audioFileOutput.Duration = TOTAL_DURATION;
			audioFileOutput.Amplifier = Int16.MaxValue / 3;
			return audioFileOutput;
		}
	}
}
