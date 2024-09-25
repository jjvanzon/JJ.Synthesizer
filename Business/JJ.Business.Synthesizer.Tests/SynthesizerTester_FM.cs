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
using JJ.Business.Synthesizer.Infos;

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
		private readonly CurveFactory _curveFactory;
		private readonly OperatorFactory _operatorFactory;
		private readonly AudioFileOutputManager _audioFileOutputManager;

		private Curve _tubaCurve;

		public SynthesizerTester_FM(IContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_curveFactory = TestHelper.CreateCurveFactory(context);
			_operatorFactory = TestHelper.CreateOperatorFactory(_context);
			_audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(_context);
		}

		// Tests

		public void Test_Synthesizer_FM_Tuba() 
			=> RunTest(CreateTuba(freq: 55), totalTime: 0.7);

		public void Test_Synthesizer_FM_Flute_HardModulated() 
			=> RunTest(CreateFlute_HardModulated());

		public void Test_Synthesizer_FM_Flute_HardHigh() 
			=> RunTest(CreateFlute_HardHigh());

		public void Test_Synthesizer_FM_Flute_AnotherOne()
			=> RunTest(CreateFlute_AnotherOne());

		public void Test_Synthesizer_FM_Flute_YetAnotherOne()
			=> RunTest(CreateFlute_YetAnotherOne());
		
		public void Test_Synthesizer_FM_Ripple_FatMetallic()
			=> RunTest(CreateRipple_FatMetallic());

		public void Test_Synthesizer_FM_Ripple_DeepMetallic()
			=> RunTest(CreateRipple_DeepMetallic());
		
		public void Test_Synthesizer_FM_Ripple_FantasyEffect()
			=> RunTest(CreateRipple_FantasyEffect());

		public void Test_Synthesizer_FM_Ripple_Clean()
			=> RunTest(CreateRipple_Clean());

		public void Test_Synthesizer_FM_Ripple_CoolDouble()
			=> RunTest(CreateRipple_CoolDouble());

		public void Test_Synthesizer_FM_Noise_Beating()
			=> RunTest(CreateNoise_Beating());

		// Generic Method

		public void RunTest(Outlet outlet, double totalTime = TOTAL_TIME, [CallerMemberName] string callerMemberName = null)
		{
			// Configure AudioFileOutput
			AudioFileOutput audioFileOutput = ConfigureAudioFileOutput($"{callerMemberName}.wav", outlet, totalTime);

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
		private Outlet CreateTuba(double freq = 220)
		{
			var x = _operatorFactory;

			// FM Algorithm
			var outlet = FMInHertz(soundFreq: freq * 2, modSpeed: freq, modDepth: 5);

			// Volume Curve
			outlet = x.Multiply(outlet, x.CurveIn(GetTubaCurve()));

			return outlet;
		}

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

		private Curve GetTubaCurve()
		{
			if (_tubaCurve == null)
			{
				_tubaCurve = _curveFactory.CreateCurve
				(
					new NodeInfo(time: 0.00, value: 1),
					new NodeInfo(time: 0.65, value: 1),
					new NodeInfo(time: 0.70, value: 0)
				);
			}
			return _tubaCurve;
		}

		private AudioFileOutput ConfigureAudioFileOutput(string fileName, Outlet outlet, double totalTime)
		{
			AudioFileOutput audioFileOutput = _audioFileOutputManager.CreateAudioFileOutput();
			audioFileOutput.Duration = totalTime;
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
