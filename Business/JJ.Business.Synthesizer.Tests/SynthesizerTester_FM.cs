using JJ.Business.Synthesizer.Calculation.AudioFileOutputs;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Warnings;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

// ReSharper disable LocalizableElement
// ReSharper disable BuiltInTypeReferenceStyleForMemberAccess

#pragma warning disable CS0219 // Variable is assigned but its value is never used

namespace JJ.Business.Synthesizer.Tests
{
	/// <summary>
	/// NOTE: Version 0.0.250 does not have time tracking in its oscillator,
	/// making the FM synthesis behave differently.
	/// </summary>
	internal class SynthesizerTester_FM
	{
		private const double TOTAL_TIME = 3.0;
		private const double DEFAULT_AMPLITUDE = 1.0;

		private readonly IContext _context;
		private readonly AudioFileOutputManager _audioFileOutputManager;
		private readonly OperatorFactory _operatorFactory;


		public SynthesizerTester_FM(IContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(_context);
			_operatorFactory = TestHelper.CreateOperatorFactory(_context);
		}

		// Tests

		public void Test_Synthesizer_FM_Tuba() 
			=> TestOutlet(CreateTuba());

		public void Test_Synthesizer_FM_Flute_HardModulated() 
			=> TestOutlet(CreateFlute_HardModulated());

		public void Test_Synthesizer_FM_Flute_HardHigh() 
			=> TestOutlet(CreateFlute_HardHigh());

		public void Test_Synthesizer_FM_Flute_AnotherOne()
			=> TestOutlet(CreateFlute_AnotherOne());

		public void Test_Synthesizer_FM_Flute_YetAnotherOne()
			=> TestOutlet(CreateFlute_YetAnotherOne());
		
		public void Test_Synthesizer_FM_Ripple_FatMetallic()
			=> TestOutlet(CreateRipple_FatMetallic());

		public void Test_Synthesizer_FM_Ripple_DeepMetallic()
			=> TestOutlet(CreateRipple_DeepMetallic());
		
		public void Test_Synthesizer_FM_Ripple_FantasyEffect()
			=> TestOutlet(CreateRipple_FantasyEffect());

		public void Test_Synthesizer_FM_Ripple_Clean()
			=> TestOutlet(CreateRipple_Clean());

		public void Test_Synthesizer_FM_Ripple_CoolDouble()
			=> TestOutlet(CreateRipple_CoolDouble());

		public void Test_Synthesizer_FM_Noise_Beating()
			=> TestOutlet(CreateNoise_Beating());

		// Generic Method

		public void TestOutlet(Outlet outlet, [CallerMemberName] string callerMemberName = null)
		{
			// Configure AudioFileOutput
			AudioFileOutput audioFileOutput = ConfigureAudioFileOutput($"{callerMemberName}.wav", outlet);

			// Verify
			AssertEntities(audioFileOutput, outlet);

			// Calculate
			Stopwatch stopWatch = Calculate(audioFileOutput);

			// Report
			Console.WriteLine($"Calculation time: {stopWatch.ElapsedMilliseconds}ms{Environment.NewLine}" +
					  		  $"Output file: {Path.GetFullPath(audioFileOutput.FilePath)}");
		}

		// Create Instruments

		// Tuba

		/// <summary> Tuba at beginning: mod speed below sound freq, changes sound freq to +/- 5Hz </summary>
		private Outlet CreateTuba() 
			=> FMInHertz(soundFreq: 440, modSpeed: 220, modDepth: 5);

		// Flutes

		/// <summary> Modulated hard flute: mod speed below sound freq, changes sound freq * [-0.005, 0.005] (erroneously) </summary>
		private Outlet CreateFlute_HardModulated()
			=> FMAround0(soundFreq: 440, modSpeed: 220, modDepth: 0.005);

		/// <summary> High hard flute: mod speed above sound freq, changes sound freq * [-0.005, 0.005] (erroneously) </summary>
		private Outlet CreateFlute_HardHigh()
			=> FMAround0(soundFreq: 440, modSpeed: 880, modDepth: 0.005);

		/// <summary> Yet another flute: mod speed above sound freq, changes sound freq * 1 +/- 0.005 </summary>
		private Outlet CreateFlute_AnotherOne() 
			=> FMAroundFreq(soundFreq: 440, modSpeed: 880, modDepth: 0.005);

		/// <summary> Yet another flute: mod speed above sound freq, changes sound freq * 1 +/- 0.005 </summary>
		private Outlet CreateFlute_YetAnotherOne() 
			=> FMAroundFreq(soundFreq: 220, modSpeed: 880, modDepth: 0.005);

		// Ripple Effects

		/// <summary> Mod speed below sound freq, changes sound freq ±10Hz </summary>
		private Outlet CreateRipple_FatMetallic()
			=> FMInHertz(soundFreq: 440, modSpeed: 220, modDepth: 10);

		/// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.005 </summary>
		private Outlet CreateRipple_DeepMetallic()
			=> FMAroundFreq(soundFreq: 880, modSpeed: 55, modDepth: 0.005);

		/// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.02 </summary>
		private Outlet CreateRipple_FantasyEffect()
			=> FMAroundFreq(soundFreq: 880, modSpeed: 10, modDepth: 0.02);

		/// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.005 </summary>
		private Outlet CreateRipple_Clean()
			=> FMAroundFreq(soundFreq: 880, modSpeed: 20, modDepth: 0.005);

		/// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.05 </summary>
		private Outlet CreateRipple_CoolDouble()
			=> FMAroundFreq(soundFreq: 880, modSpeed: 10, modDepth: 0.05);

		// Noise

		/// <summary>
		/// Beating audible further along the sound.
		/// Mod speed much below sound freq, changes sound freq drastically * [0.5, 1.5]
		/// </summary>
		private Outlet CreateNoise_Beating()
			=> FMAroundFreq(soundFreq: 880, modSpeed: 55, modDepth: 0.5);

		// Algorithms

		/// <summary> FM sound synthesis modulating with addition. Modulates sound freq to +/- a number of Hz. </summary>
		/// <param name="modDepth">In Hz</param>
		private Outlet FMInHertz(double soundFreq, double modSpeed, double modDepth)
		{
			OperatorFactory x = _operatorFactory;

			Outlet modulator = x.Sine(x.Value(modDepth), x.Value(modSpeed));
			Outlet sound = x.Sine(x.Value(DEFAULT_AMPLITUDE), x.Add(x.Value(soundFreq), modulator));
			return sound;
		}

		/// <summary> FM with (faulty) multiplication around 0. </summary>
		private Outlet FMAround0(double soundFreq, double modSpeed, double modDepth)
		{
			OperatorFactory x = _operatorFactory;

			Outlet modulator = x.Sine(x.Value(modDepth), x.Value(modSpeed));
			Outlet sound = x.Sine(x.Value(DEFAULT_AMPLITUDE), x.Multiply(x.Value(soundFreq), modulator));
			return sound;
		}

		/// <summary> FM with multiplication around 1. </summary>
		private Outlet FMAroundFreq(double soundFreq, double modSpeed, double modDepth)
		{
			OperatorFactory x = _operatorFactory;

			Outlet modulator = x.Add(x.Value(1), x.Sine(x.Value(modDepth), x.Value(modSpeed)));
			Outlet sound = x.Sine(x.Value(DEFAULT_AMPLITUDE), x.Multiply(x.Value(soundFreq), modulator));
			return sound;
		}

		// Steps

		private AudioFileOutput ConfigureAudioFileOutput(string fileName, Outlet outlet)
		{
			AudioFileOutput audioFileOutput = _audioFileOutputManager.CreateAudioFileOutput();
			audioFileOutput.Duration = TOTAL_TIME;
			audioFileOutput.Amplifier = Int16.MaxValue;
			audioFileOutput.FilePath = fileName;
			audioFileOutput.AudioFileOutputChannels[0].Outlet = outlet;
			return audioFileOutput;
		}

		private void AssertEntities(AudioFileOutput audioFileOutput, Outlet outlet)
		{
			_audioFileOutputManager.ValidateAudioFileOutput(audioFileOutput).Verify();
			new VersatileOperatorValidator(outlet.Operator).Verify();
			new VersatileOperatorWarningValidator(outlet.Operator).Verify();
		}

		private Stopwatch Calculate(AudioFileOutput audioFileOutput)
		{
			var calculator = AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(audioFileOutput);
			var stopWatch = Stopwatch.StartNew();
			calculator.Execute();
			stopWatch.Stop();
			return stopWatch;
		}

	}
}
