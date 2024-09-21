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
		private const double NOTE_FADE_TIME = 2.5;
		private const double TOTAL_DURATION = 5.74; //3.1;

		private readonly IContext _context;
		private readonly SampleManager _sampleManager;
		private readonly CurveFactory _curveFactory;
		private readonly OperatorFactory _operatorFactory;
		private readonly AudioFileOutputManager _audioFileOutputManager;

		private Sample _sample;
		private Curve _sine1VolumeCurve;
		private Curve _sine2VolumeCurve;
		private Curve _sine3VolumeCurve;
		private Curve _sampleVolumeCurve;
		private Outlet _melodyOutlet;
		private AudioFileOutput _audioFileOutput;

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
			_sample = CreateSample();
			_sine1VolumeCurve = CreateSine1Curve();
			_sine2VolumeCurve = CreateSine2Curve();
			_sine3VolumeCurve = CreateSine3Curve();
			_sampleVolumeCurve = CreateSampleCurve();

			Outlet outlet = CreateMelody();
			outlet = EntityFactory.CreateEcho(_operatorFactory, outlet, count: 5, denominator: 2, delay: 0.66);

			_audioFileOutput = CreateAudioFileOutput();
			_audioFileOutput.AudioFileOutputChannels[0].Outlet = outlet;

			// Verify
			AssertEntities(outlet);

			// Calculate
			IAudioFileOutputCalculator audioFileOutputCalculator =
				AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(_audioFileOutput);
			var stopWatch = Stopwatch.StartNew();
			audioFileOutputCalculator.Execute();
			stopWatch.Stop();

			// Report
			Assert.Inconclusive($"Calculation time: {stopWatch.ElapsedMilliseconds}ms{Environment.NewLine}" +
								$"Output file: {Path.GetFullPath(_audioFileOutput.FilePath)}");
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

			return sample;
		}

		private Curve CreateSine1Curve() => _curveFactory.CreateCurve(
			NOTE_FADE_TIME,
			0.00, 0.80, 1.00, null, null, null, null, null,
			0.25, null, null, null, null, null, null, null,
			0.10, null, null, 0.02, null, null, null, 0.00);

		private Curve CreateSine2Curve() => _curveFactory.CreateCurve(
			NOTE_FADE_TIME,
			0.00, 1.00, 0.80, null, null, null, null, null,
			0.10, null, null, null, null, null, null, null,
			0.05, null, null, 0.01, null, null, null, 0.00);

		private Curve CreateSine3Curve() => _curveFactory.CreateCurve(
			NOTE_FADE_TIME,
			0.30, 0.00, 0.30, null, null, null, null, null,
			0.10, null, null, null, null, null, null, null,
			0.15, null, null, 0.05, null, null, null, 0.00);

		private Curve CreateSampleCurve() => _curveFactory.CreateCurve(
			NOTE_FADE_TIME,
			1.00, 0.50, 0.20, null, null, null, null, 0.00,
			null, null, null, null, null, null, null, null,
			null, null, null, null, null, null, null, null);

		private Outlet CreateMelody()
		{
			OperatorFactory x = _operatorFactory;

			double note1Volume = 0.9;
			double note2Volume = 1.0;
			double note3Volume = 0.5;
			double note4Volume = 0.7;

			double note2Delay = 0.2;
			double note3Delay = 0.4;
			double note4Delay = 0.6;

			Outlet outlet = x.Adder
			(
				x.Multiply(x.Value(note1Volume), CreateNote(NoteFrequencies.A4)),
				x.Multiply(x.Value(note2Volume), x.TimeAdd(CreateNote(NoteFrequencies.E4), x.Value(note2Delay))),
				x.Multiply(x.Value(note3Volume), x.TimeAdd(CreateNote(NoteFrequencies.B4), x.Value(note3Delay))),
				x.Multiply(x.Value(note4Volume), x.TimeAdd(CreateNote(NoteFrequencies.CSHARP4), x.Value(note4Delay)))
			);

			return outlet;
		}

		private Outlet CreateNote(double noteFrequency)
		{
			const double sine1Volume = 1.0;
			const double sine2Volume = 0.7;
			const double sine3Volume = 0.4;
			const double sample1Volume = 3.0;
			const double sample2Volume = 5.0;

			OperatorFactory x = _operatorFactory;

			return x.Adder
			(
				x.Sine
				(
					x.Multiply(x.CurveIn(_sine1VolumeCurve), x.Value(sine1Volume)),
					x.Value(noteFrequency)
				),
				x.Sine
				(
					x.Multiply(x.CurveIn(_sine2VolumeCurve), x.Value(sine2Volume)),
					x.Multiply(x.Value(noteFrequency), x.Value(2))
				),
				x.Sine
				(
					x.Multiply(x.CurveIn(_sine3VolumeCurve), x.Value(sine3Volume)),
					x.Multiply(x.Value(noteFrequency), x.Value(5))
				),
				x.TimeDivide
				(
					x.Multiply(x.Multiply
					(
						x.Value(sample1Volume),
						x.Sample(_sample)),
						x.CurveIn(_sampleVolumeCurve)
					),
					x.Value(2.0 * noteFrequency / 440.0)
				),
				x.TimeDivide
				(
					x.Multiply(x.Multiply
					(
						x.Value(sample2Volume),
						x.Sample(_sample)),
						x.CurveIn(_sampleVolumeCurve)
					),
					x.Value(7.0 * noteFrequency / 440.0)
				)
			);
		}

		private AudioFileOutput CreateAudioFileOutput()
		{
			AudioFileOutput audioFileOutput = _audioFileOutputManager.CreateAudioFileOutput();
			audioFileOutput.Duration = TOTAL_DURATION;
			audioFileOutput.Amplifier = Int16.MaxValue / 3;
			audioFileOutput.FilePath = $"{nameof(Test_Synthesizer_Additive_Sines_And_Samples)}.wav";

			return audioFileOutput;
		}

		private void AssertEntities(Outlet outlet)
		{
			_sampleManager.ValidateSample(_sample).Verify();
			new CurveValidator(_sine1VolumeCurve).Verify();
			new CurveValidator(_sine2VolumeCurve).Verify();
			new CurveValidator(_sine3VolumeCurve).Verify();
			new VersatileOperatorValidator(outlet.Operator).Verify();
			_audioFileOutputManager.ValidateAudioFileOutput(_audioFileOutput).Verify();

			// Warnings
			new AudioFileOutputWarningValidator(_audioFileOutput).Verify();
			new VersatileOperatorWarningValidator(outlet.Operator).Verify();
		}
	}
}