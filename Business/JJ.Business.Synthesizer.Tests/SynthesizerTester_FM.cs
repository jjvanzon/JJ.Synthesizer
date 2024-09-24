using JJ.Business.Synthesizer.Calculation.AudioFileOutputs;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Warnings;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;

#pragma warning disable CS0219 // Variable is assigned but its value is never used

namespace JJ.Business.Synthesizer.Tests
{
	internal class SynthesizerTester_FM
	{
		private const double TOTAL_TIME = 3.0;
		private const double DEFAULT_AMPLITUDE = 1.0;

		private readonly IContext _context;
		private readonly AudioFileOutputManager _audioFileOutputManager;
		private readonly OperatorFactory _operatorFactory;

		private AudioFileOutput _audioFileOutput;

		public SynthesizerTester_FM(IContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(_context);
			_operatorFactory = TestHelper.CreateOperatorFactory(_context);
		}

		/// <summary>
		/// NOTE: Version 0.0.250 does not have time tracking in its oscillator,
		/// making the FM synthesis behave differently.
		/// </summary>
		public void Test_Synthesizer_FM()
		{
			// Arrange
			var x = _operatorFactory;

			double soundFreq;
			double modDepth;
			double modSpeed;

			Outlet sound;

			// Tuba at first: mod speed below sound freq, changes sound freq to +/- 5Hz
			sound = FMInHertz(soundFreq: 440, modSpeed: 220, modDepth: 5);

			// Flutes

			// Modulated hard flute: mod speed below sound freq, changes sound freq * [-0.005, 0.005]
			sound = FMAround0(soundFreq: 440, modSpeed: 220, modDepth: 0.005);

			// High hard flute: mod speed above sound freq, changes sound freq * [-0.005, 0.005]
			sound = FMAround0(soundFreq: 440, modSpeed: 880, modDepth: 0.005);

			// Yet another flute: mod speed above sound freq, changes sound freq * 1 +/- 0.005
			sound = FMAround1(soundFreq: 440, modSpeed: 880, modDepth: 0.005);

			// Yet another flute (same?): mod speed above sound freq, changes sound freq * 1 +/- 0.005
			sound = FMAround1(soundFreq: 220, modSpeed: 880, modDepth: 0.005);

			// Ripple Effects

			// Fat Metallic Ripple: mod speed below sound freq, changes sound freq +/- 10Hz
			{
				soundFreq = 440;
				modSpeed = 220;
				modDepth = 10;
				// FM with Addition
				Outlet modulator = x.Sine(x.Value(modDepth), x.Value(modSpeed));
				x.Sine(x.Value(1), x.Add(x.Value(soundFreq), modulator));
			}

			// Deep Metallic Ripple: mod speed way below sound freq, changes sound freq * 1 +/- 0.005
			{
				soundFreq = 880;
				modSpeed = 55;
				modDepth = 0.005;
				// FM with multiplication around 1
				Outlet modulator = x.Add(x.Value(1), x.Sine(x.Value(modDepth), x.Value(modSpeed)));
				sound = x.Sine(x.Value(1), x.Multiply(x.Value(soundFreq), modulator));
			}

			// Fantasy Ripple Effect: mod speed way below sound freq, changes sound freq * 1 +/- 0.02
			{
				soundFreq = 880;
				modSpeed = 10;
				modDepth = 0.02;
				// FM with multiplication around 1
				Outlet modulator = x.Add(x.Value(1), x.Sine(x.Value(modDepth), x.Value(modSpeed)));
				sound = x.Sine(x.Value(1), x.Multiply(x.Value(soundFreq), modulator));
			}

			// Clean Ripple: mod speed way below sound freq, changes sound freq * 1 +/- 0.005
			{
				soundFreq = 880;
				modSpeed = 20;
				modDepth = 0.005;
				// FM modulate around 1
				Outlet modulator = x.Add(x.Value(1), x.Sine(x.Value(modDepth), x.Value(modSpeed)));
				sound = x.Sine(x.Value(1), x.Multiply(x.Value(soundFreq), modulator));
			}

			// Extreme Ripple: Ensure modulator remains above 1 (ChatGPT being weird)
			{
				soundFreq = 880;
				modSpeed = 10;
				// FM with multiplication around 1
				//modDepth = 0.1;
				//Outlet modulator = x.Add(x.Value(1), x.Multiply(x.Sine(x.Value(modDepth), x.Value(modSpeed)), x.Value(0.5)));
				// Same?
				modDepth = 0.05;
				Outlet modulator = x.Add(x.Value(1), x.Sine(x.Value(modDepth), x.Value(modSpeed)));
				sound = x.Sine(x.Value(1), x.Multiply(x.Value(soundFreq), modulator));
			}

			// Noise

			// Beating Noise (further along the sound): mod speed much below sound freq, changes sound freq * [0.5, 1.5]
			{
				soundFreq = 880;
				modSpeed = 55;
				modDepth = 0.5;
				// FM with multiplication around 1
				Outlet modulator = x.Add(x.Value(1), x.Sine(x.Value(modDepth), x.Value(modSpeed)));
				sound = x.Sine(x.Value(1), x.Multiply(x.Value(soundFreq), modulator));
			}

			// TODO: Slowly sweeping timbre

			// Configure AudioFileOutput
			_audioFileOutput = ConfigureAudioFileOutput();
			_audioFileOutput.AudioFileOutputChannels[0].Outlet = sound;

			// Verify
			AssertEntities(sound);

			// Calculate
			Stopwatch stopWatch = Calculate();

			// Report
			Assert.Inconclusive($"Calculation time: {stopWatch.ElapsedMilliseconds}ms{Environment.NewLine}" +
								$"Output file: {Path.GetFullPath(_audioFileOutput.FilePath)}");
		}

		/// <summary>
		/// FM sound synthesis modulating with addition. Modulates sound freq to +/- a number of Hz.
		/// </summary>
		/// <param name="modDepth">In Hz</param>
		private Outlet FMInHertz(double soundFreq, double modSpeed, double modDepth)
		{
			OperatorFactory x = _operatorFactory;

			Outlet modulator = x.Sine(x.Value(modDepth), x.Value(modSpeed));
			Outlet sound = x.Sine(x.Value(DEFAULT_AMPLITUDE), x.Add(x.Value(soundFreq), modulator));
			return sound;
		}

		/// <summary>
		/// FM with (faulty) multiplication around 0.
		/// </summary>
		private Outlet FMAround0(double soundFreq, double modSpeed, double modDepth)
		{
			OperatorFactory x = _operatorFactory;

			Outlet modulator = x.Sine(x.Value(modDepth), x.Value(modSpeed));
			Outlet sound = x.Sine(x.Value(DEFAULT_AMPLITUDE), x.Multiply(x.Value(soundFreq), modulator));
			return sound;
		}

		/// <summary>
		/// FM with multiplication around 1.
		/// </summary>
		private Outlet FMAround1(double soundFreq, double modSpeed, double modDepth)
		{
			OperatorFactory x = _operatorFactory;

			Outlet modulator = x.Add(x.Value(1), x.Sine(x.Value(modDepth), x.Value(modSpeed)));
			Outlet sound = x.Sine(x.Value(DEFAULT_AMPLITUDE), x.Multiply(x.Value(soundFreq), modulator));
			return sound;
		}

		private AudioFileOutput ConfigureAudioFileOutput()
		{
			AudioFileOutput audioFileOutput = _audioFileOutputManager.CreateAudioFileOutput();
			audioFileOutput.Duration = TOTAL_TIME;
			audioFileOutput.Amplifier = Int16.MaxValue;
			audioFileOutput.FilePath = $"{nameof(Test_Synthesizer_FM)}.wav";
			return audioFileOutput;
		}

		private void AssertEntities(Outlet outlet)
		{
			new VersatileOperatorValidator(outlet.Operator).Verify();
			_audioFileOutputManager.ValidateAudioFileOutput(_audioFileOutput).Verify();
			new VersatileOperatorWarningValidator(outlet.Operator).Verify();
		}

		private Stopwatch Calculate()
		{
			var calculator = AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(_audioFileOutput);
			var stopWatch = Stopwatch.StartNew();
			calculator.Execute();
			stopWatch.Stop();
			return stopWatch;
		}

	}
}
