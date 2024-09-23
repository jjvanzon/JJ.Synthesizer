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
		private const double TOTAL_TIME = 3;

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

			// Tuba at first: mod speed below sound freq, changes sound freq up +/- 5Hz
			{
				Outlet modulator = x.Sine(x.Value(modDepth = 5), x.Value(modSpeed = 220));
				sound = x.Sine(x.Value(1), x.Add(x.Value(soundFreq = 440), modulator));
			}

			// Flutes

			// Modulated hard flute: mod speed below sound freq, changes sound freq * [-0.005, 0.005] (why that work?)
			{
				Outlet modulator = x.Sine(x.Value(modDepth = 0.005), x.Value(modSpeed = 220));
				sound = x.Sine(x.Value(1), x.Multiply(x.Value(soundFreq = 440), modulator));
			}

			// High hard flute: Why it work? mod speed above sound freq, changes sound freq * [-0.005, 0.005]
			{
				Outlet modulator = x.Sine(x.Value(modDepth = 0.005), x.Value(modSpeed = 880));
				sound = x.Sine(x.Value(1), x.Multiply(x.Value(soundFreq = 440), modulator));
			}

			// Yet another flute: mod speed above sound freq, changes sound freq * 1 +/- 0.005
			{
				Outlet modulator = x.Add(x.Value(1), x.Sine(x.Value(modDepth = 0.005), x.Value(modSpeed = 880)));
				sound = x.Sine(x.Value(1), x.Multiply(x.Value(soundFreq = 440), modulator));
			}

			// Yet another flute
			{
				Outlet modulator = x.Add(x.Value(1), x.Sine(x.Value(modDepth = 0.005), x.Value(modSpeed = 880)));
				sound = x.Sine(x.Value(1), x.Multiply(x.Value(soundFreq = 220), modulator));
			}

			// Ripple Effects

			// Fat Metallic Ripple: mod speed below sound freq, changes sound freq up +/- 10Hz
			{
				Outlet modulator = x.Sine(x.Value(modDepth = 10), x.Value(modSpeed = 220));
				sound = x.Sine(x.Value(1), x.Add(x.Value(soundFreq = 440), modulator));
			}

			// Deep Metallic Ripple: mod speed way below sound freq, changes sound freq * 1 +/- 0.005
			{
				Outlet modulator = x.Add(x.Value(1), x.Sine(x.Value(modDepth = 0.005), x.Value(modSpeed = 55)));
				sound = x.Sine(x.Value(1), x.Multiply(x.Value(soundFreq = 880), modulator));
			}

			// Fantasy Ripple Effect
			{
				Outlet modulator = x.Add(x.Value(1), x.Sine(x.Value(modDepth = 0.02), x.Value(modSpeed = 10)));
				sound = x.Sine(x.Value(1), x.Multiply(x.Value(soundFreq = 880), modulator));
			}

			// Clean Ripple
			{
				Outlet modulator = x.Add(x.Value(1), x.Sine(x.Value(modDepth = 0.005), x.Value(modSpeed = 20)));
				sound = x.Sine(x.Value(1), x.Multiply(x.Value(soundFreq = 880), modulator));
			}

			// Extreme Ripple: Ensure modulator remains above 1 (ChatGPT being weird)
			{
				Outlet modulator = x.Add(x.Value(1), x.Multiply(x.Sine(x.Value(modDepth = 0.1), x.Value(modSpeed = 10)), x.Value(0.5)));
				sound = x.Sine(x.Value(1), x.Multiply(x.Value(soundFreq = 880), modulator));
			}

			// Noise

			// Beating Noise (further down)
			{
				Outlet modulator = x.Add(x.Value(1), x.Sine(x.Value(modDepth = 0.5), x.Value(modSpeed = 55)));
				sound = x.Sine(x.Value(1), x.Multiply(x.Value(soundFreq = 880), modulator));
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
