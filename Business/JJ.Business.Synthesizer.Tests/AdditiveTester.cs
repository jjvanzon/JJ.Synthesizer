﻿using JJ.Business.Synthesizer.Calculation.AudioFileOutputs;
using JJ.Business.Synthesizer.EntityWrappers;
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
		private const double TOTAL_TIME = 6.15; //3.1;

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
			outlet = EntityFactory.CreateEcho(_operatorFactory, outlet, count: 5, denominator: 3, delay: 0.66);

			_audioFileOutput = ConfigureAudioFileOutput();
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
			return _operatorFactory.Adder
			(
				CreateNote(NoteFrequencies.A4, volume: 0.9),
				CreateNote(NoteFrequencies.E5, volume: 1.0, delay: 0.2),
				CreateNote(NoteFrequencies.B4, volume: 0.5, delay: 0.4),
				CreateNote(NoteFrequencies.CSHARP5, volume: 0.7, delay: 0.6),
				CreateNote(NoteFrequencies.FSHARP4, volume: 0.4, delay: 1.2)
			);
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
				CreateSine(noteFrequency, sine1Volume, _sine1VolumeCurve),
				CreateSine(noteFrequency * 2, sine2Volume, _sine2VolumeCurve),
				CreateSine(noteFrequency * 5, sine3Volume, _sine3VolumeCurve),
				CreateSampleOutlet(noteFrequency * 2, sample1Volume, _sampleVolumeCurve),
				CreateSampleOutlet(noteFrequency * 7, sample2Volume, _sampleVolumeCurve)
			);

			outlet = x.Multiply(outlet, x.Value(volume));
			outlet = x.TimeAdd(outlet, x.Value(delay));

			return outlet;
		}

		private Outlet CreateSampleOutlet(double frequency, double volume, Curve curve)
		{
			var x = _operatorFactory;

			return x.TimeDivide
			(
				x.Multiply(x.Multiply
				(
					x.Sample(_sample),
					x.Value(volume)),
					x.CurveIn(curve)
				),
				x.Value(frequency / 440.0)
			);
		}

		private Sine CreateSine(double frequency, double volume, Curve curve)
		{
			var x = _operatorFactory;

			return x.Sine
			(
				x.Multiply(x.CurveIn(curve), x.Value(volume)),
				x.Value(frequency)
			);
		}

		private AudioFileOutput ConfigureAudioFileOutput()
		{
			AudioFileOutput audioFileOutput = _audioFileOutputManager.CreateAudioFileOutput();
			audioFileOutput.Duration = TOTAL_TIME;
			audioFileOutput.Amplifier = Int16.MaxValue / 3.5;
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