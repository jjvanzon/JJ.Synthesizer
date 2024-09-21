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

		public void Test_FM_With_Detune_And_Pitch_Modulation()
		{
			// Arrange
			var x = _operatorFactory;

			// Extreme
			//Outlet modulator = x.Sine(x.Value(10), x.Value(Frequencies.A4 / 2.0));
			//Outlet sound = x.Sine(x.Value(1), x.Add(x.Value(Frequencies.A4), modulator));

			// Nice
			//Outlet modulator = x.Sine(x.Value(5), x.Value(Frequencies.A4 / 2.0));
			//Outlet sound = x.Sine(x.Value(1), x.Add(x.Value(Frequencies.A4), modulator));

			// Classic
			//Outlet modulator = x.Sine(x.Value(0.005), x.Value(Frequencies.A4 / 2.0));
			//Outlet sound = x.Sine(x.Value(1), x.Multiply(x.Value(Frequencies.A4), modulator));

			// Variablize
			double soundVolume = 1;
			double noteFrequency = Frequencies.A4;
			double modulationDepth = 0.005;
			double modulationSpeed = noteFrequency / 2.0;

			Outlet modulator = x.Sine(x.Value(modulationDepth), x.Value(modulationSpeed));
			Outlet sound = x.Sine(x.Value(soundVolume), x.Multiply(x.Value(noteFrequency), modulator));

			Outlet outlet = sound;
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
