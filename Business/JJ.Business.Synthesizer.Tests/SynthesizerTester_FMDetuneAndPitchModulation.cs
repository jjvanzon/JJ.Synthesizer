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

namespace JJ.Business.Synthesizer.Tests
{
	internal class SynthesizerTester_FMDetuneAndPitchModulation
	{
		private const double TOTAL_TIME = 3;

		private readonly IContext _context;
		private readonly AudioFileOutputManager _audioFileOutputManager;
		private readonly OperatorFactory _operatorFactory;

		private AudioFileOutput _audioFileOutput;

		public SynthesizerTester_FMDetuneAndPitchModulation(IContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(_context);
			_operatorFactory = TestHelper.CreateOperatorFactory(_context);
		}

		/// <summary>
		/// NOTE: Version 0.0.250 does not have time tracking in its oscillator,
		/// making the FM synthesis behave much different.
		/// </summary>
		public void Test_FM_With_Detune_And_Pitch_Modulation()
		{
			// Arrange
			var x = _operatorFactory;

			double noteFreq;
			double modDepth;
			double modSpeed;

			// Extreme
			//Outlet modulator = x.Sine(x.Value(10), x.Value(Frequencies.A4 / 2.0));
			//Outlet sound = x.Sine(x.Value(1), x.Add(x.Value(Frequencies.A4), modulator));

			// Nice: modulation speed below sound freq, changes sound freq up +/- 5Hz
			// Outlet modulator = x.Sine(x.Value(modDepth = 5), x.Value(modSpeed = 220));
			// Outlet sound = x.Sine(x.Value(1.0), x.Add(x.Value(noteFreq = 440), modulator));

			// Nice: modulation speed below sound freq, changes sound freq * [-0.005, 0.005] (why that works?)
			Outlet modulator = x.Sine(x.Value(modDepth = 0.005), x.Value(modSpeed = 220));
			Outlet sound = x.Sine(x.Value(1.0), x.Multiply(x.Value(noteFreq = 440), modulator));

			// Also nice
			/*
			double soundVolume = 1;
			double noteFrequency = Frequencies.A4;
			double modulationDepth = 0.005;
			double modulationSpeed = noteFrequency * 2.0;
			Outlet modulator = x.Sine(x.Value(modulationDepth), x.Value(modulationSpeed));
			Outlet sound = x.Sine(x.Value(soundVolume), x.Multiply(x.Value(noteFrequency), modulator));
			*/

			// Works a little
			/*
			double noteFrequency = Frequencies.A4;
			double modulationDepth = 0.005;
			double modulationSpeed = noteFrequency * 2.0;
			Outlet modulator = x.Add(x.Value(1.0), x.Sine(x.Value(modulationDepth), x.Value(modulationSpeed)));
			Outlet sound = x.Sine(x.Value(1.0), x.Multiply(x.Value(noteFrequency), modulator));
			*/

			// From ChatGTP math formula
			/*
			double carrFreq = Frequencies.A4;
			double modFreq = carrFreq / 2.0;
			Outlet sound = x.Sine(x.Value(1.0), 
				x.Add(
					x.Value(carrFreq), 
					x.Multiply(
						x.Value(modFreq), 
						x.Sine(x.Value(1.0), x.Value(modFreq)))));
			*/

			// ChatGPT (broken)
			/*
			double carrFreq = Frequencies.A4;
			double modDepth = 0.5; // Adjust this for modulation depth
			double modFreq = carrFreq / 2.0;

			Outlet modulator = x.Sine(x.Value(modDepth), x.Value(modFreq));
			Outlet sound = x.Sine(x.Value(1.0), x.Multiply(x.Value(carrFreq), x.Add(x.Value(1.0), modulator)));
			*/

			// ChatGPT 2nd try
			/*
			double carrFreq = Frequencies.A4; // Carrier frequency
			double modDepth = 0.5; // Modulation depth
			double modFreq = carrFreq / 2.0; // Modulator frequency

			// Create the modulator
			Outlet modulator = x.Sine(x.Value(modDepth), x.Value(modFreq));

			// Calculate the final sound using the correct FM formula
			Outlet sound = x.Sine(x.Value(1.0),
				x.Multiply(
					x.Value(carrFreq),
					x.Add(x.Value(1.0), modulator)
				)
			);
			*/


			// ChatGPT 2nd try modified
			/*
			double carrFreq = Frequencies.A4; // Carrier frequency
			double modDepth = 0.02; // Modulation depth
			double modFreq = carrFreq * 2.0; // Modulator frequency

			// Create the modulator
			Outlet modulator = x.Sine(x.Value(modDepth), x.Value(modFreq));

			// Calculate the final sound using the correct FM formula
			Outlet sound = x.Sine(x.Value(1.0),
				x.Multiply(
					x.Value(carrFreq),
					x.Add(x.Value(1.0), modulator)
				)
			);
			*/

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
			audioFileOutput.Amplifier = Int16.MaxValue / 3.5;
			audioFileOutput.FilePath = $"{nameof(Test_FM_With_Detune_And_Pitch_Modulation)}.wav";
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
