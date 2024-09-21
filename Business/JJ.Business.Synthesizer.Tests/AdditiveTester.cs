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
		private const double NOTE_TIME_WITH_FADE = 2.5;
		private const double TOTAL_TIME = 5.74; //3.1;

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
			_sample = ConfigureSample();
			_sine1VolumeCurve = CreateSine1VolumeCurve();
			_sine2VolumeCurve = CreateSine2VolumeCurve();
			_sine3VolumeCurve = CreateSine3VolumeCurve();
			_sampleVolumeCurve = CreateSampleVolumeCurve();

			Outlet outlet = CreateMelody();
			outlet = EntityFactory.CreateEcho(_operatorFactory, outlet, count: 5, denominator: 2, delay: 0.66);

			_audioFileOutput = CreateAudioFileOutput();
			_audioFileOutput.AudioFileOutputChannels[0].Outlet = outlet;

			// Verify
			AssertEntities(outlet);

			// Calculate
			Stopwatch stopWatch = Calculate();

			// Report
			Assert.Inconclusive($"Calculation time: {stopWatch.ElapsedMilliseconds}ms{Environment.NewLine}" +
								$"Output file: {Path.GetFullPath(_audioFileOutput.FilePath)}");
		}

		private Sample ConfigureSample()
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

		private Curve CreateSine1VolumeCurve() => _curveFactory.CreateCurve(
			NOTE_TIME_WITH_FADE,
			0.00, 0.80, 1.00, null, null, null, null, null,
			0.25, null, null, null, null, null, null, null,
			0.10, null, null, 0.02, null, null, null, 0.00);

		private Curve CreateSine2VolumeCurve() => _curveFactory.CreateCurve(
			NOTE_TIME_WITH_FADE,
			0.00, 1.00, 0.80, null, null, null, null, null,
			0.10, null, null, null, null, null, null, null,
			0.05, null, null, 0.01, null, null, null, 0.00);

		private Curve CreateSine3VolumeCurve() => _curveFactory.CreateCurve(
			NOTE_TIME_WITH_FADE,
			0.30, 0.00, 0.30, null, null, null, null, null,
			0.10, null, null, null, null, null, null, null,
			0.15, null, null, 0.05, null, null, null, 0.00);

		private Curve CreateSampleVolumeCurve() => _curveFactory.CreateCurve(
			NOTE_TIME_WITH_FADE,
			1.00, 0.50, 0.20, null, null, null, null, 0.00,
			null, null, null, null, null, null, null, null,
			null, null, null, null, null, null, null, null);

		private Outlet CreateMelody()
		{
			OperatorFactory x = _operatorFactory;

			double delay2 = 0.2;
			double delay3 = 0.4;
			double delay4 = 0.6;

			Outlet outlet = x.Adder
			(
				CreateNote(NoteFrequencies.A4, volume: 0.9),
				x.TimeAdd(CreateNote(NoteFrequencies.E4, volume: 1.0), x.Value(delay2)),
				x.TimeAdd(CreateNote(NoteFrequencies.B4, volume: 0.5), x.Value(delay3)),
				x.TimeAdd(CreateNote(NoteFrequencies.CSHARP4, volume: 0.7), x.Value(delay4))
			);

			return outlet;
		}

		private Outlet CreateNote(double noteFrequency, double volume, double delay = 0)
		{
			const double sine1Volume = 1.0;
			const double sine2Volume = 0.7;
			const double sine3Volume = 0.4;
			const double sample1Volume = 3.0;
			const double sample2Volume = 5.0;

			OperatorFactory x = _operatorFactory;

			Outlet outlet = x.Adder
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
						x.Sample(_sample),
						x.Value(sample1Volume)),
						x.CurveIn(_sampleVolumeCurve)
					),
					x.Value(2.0 * noteFrequency / 440.0)
				),
				x.TimeDivide
				(
					x.Multiply(x.Multiply
					(
						x.Sample(_sample),
						x.Value(sample2Volume)),
						x.CurveIn(_sampleVolumeCurve)
					),
					x.Value(7.0 * noteFrequency / 440.0)
				)
			);

			outlet = x.Multiply(outlet, x.Value(volume));
			outlet = x.TimeAdd(outlet, x.Value(delay));

			return outlet;
		}

		private AudioFileOutput CreateAudioFileOutput()
		{
			AudioFileOutput audioFileOutput = _audioFileOutputManager.CreateAudioFileOutput();
			audioFileOutput.Duration = TOTAL_TIME;
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

		private Stopwatch Calculate()
		{
			IAudioFileOutputCalculator audioFileOutputCalculator =
							AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(_audioFileOutput);
			var stopWatch = Stopwatch.StartNew();
			audioFileOutputCalculator.Execute();
			stopWatch.Stop();
			return stopWatch;
		}
	}
}