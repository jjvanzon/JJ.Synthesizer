using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.AudioFileOutputs;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Warnings;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable LocalizableElement
// ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable

namespace JJ.Business.Synthesizer.Tests
{
	/// <summary>
	/// NOTE: Version 0.0.250 does not have time tracking in its oscillator,
	/// making the FM synthesis behave differently.
	/// </summary>
	[TestClass]
	public class SynthesizerTests_FM
	{
		private const double DEFAULT_TOTAL_TIME = 3.0;
		private const double DEFAULT_AMPLITUDE = 1.0;

		private readonly IContext _context;
		private readonly CurveFactory _curveFactory;
		private readonly OperatorFactory _operatorFactory;
		private readonly AudioFileOutputManager _audioFileOutputManager;

		/// <summary> Constructor for test runner. </summary>
		public SynthesizerTests_FM() { }

		/// <summary> Constructor allowing each test to run in its own instance. </summary>
		public SynthesizerTests_FM(IContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_curveFactory = TestHelper.CreateCurveFactory(context);
			_operatorFactory = TestHelper.CreateOperatorFactory(_context);
			_audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(_context);
		}

		// Tests

		[TestMethod]
		public void Test_Synthesizer_FM_Tuba()
		{
			using (IContext context = PersistenceHelper.CreateContext())
				new SynthesizerTests_FM(context).Test_FM_Tuba();
		}

		private void Test_FM_Tuba()
		{
			Outlet melody = _operatorFactory.Adder
			(
				TubaNote(Frequencies.A1,       volume: 1.0),
				TubaNote(Frequencies.E2,       volume: 1.0, delay: 1.2),
				TubaNote(Frequencies.F1_Sharp, volume: 0.7, delay: 2.4)
			);

			WrapUp_Test(melody, totalTime: 2.4 + 2.0);
		}

		// Flute Tests

		[TestMethod]
		public void Test_Synthesizer_FM_Flute1()
		{
			using (IContext context = PersistenceHelper.CreateContext())
				new SynthesizerTests_FM(context).Test_FM_Flute1();
		}

		private void Test_FM_Flute1()
			=> WrapUp_Test(FluteNote1(Frequencies.A4));

		[TestMethod]
		public void Test_Synthesizer_FM_Flute2()
		{
			using (IContext context = PersistenceHelper.CreateContext())
				new SynthesizerTests_FM(context).Test_FM_Flute2();
		}

		private void Test_FM_Flute2()
			=> WrapUp_Test(FluteNote2(Frequencies.A4));

		[TestMethod]
		public void Test_Synthesizer_FM_Flute3()
		{
			using (IContext context = PersistenceHelper.CreateContext())
				new SynthesizerTests_FM(context).Test_FM_Flute3();
		}

		private void Test_FM_Flute3()
			=> WrapUp_Test(FluteNote3(Frequencies.A4));

		[TestMethod]
		public void Test_Synthesizer_FM_Flute4()
		{
			using (IContext context = PersistenceHelper.CreateContext())
				new SynthesizerTests_FM(context).Test_FM_Flute4();
		}

		private void Test_FM_Flute4()
			=> WrapUp_Test(FluteNote4(Frequencies.A4));

		// FM Ripple Effects

		[TestMethod]
		public void Test_Synthesizer_FM_RippleNote_DeepMetallic()
		{
			using (IContext context = PersistenceHelper.CreateContext())
				new SynthesizerTests_FM(context).Test_FM_RippleNote_DeepMetallic();
		}

		private void Test_FM_RippleNote_DeepMetallic()
			=> WrapUp_Test(RippleNote_DeepMetallic(Frequencies.A4));

		[TestMethod]
		public void Test_Synthesizer_FM_RippleNote_SharpMetallic()
		{
			using (IContext context = PersistenceHelper.CreateContext())
				new SynthesizerTests_FM(context).Test_FM_RippleNote_SharpMetallic();
		}

		private void Test_FM_RippleNote_SharpMetallic()
			=> WrapUp_Test(RippleNote_SharpMetallic(Frequencies.A4));

		[TestMethod]
		public void Test_Synthesizer_FM_RippleSound_Clean()
		{
			using (IContext context = PersistenceHelper.CreateContext())
				new SynthesizerTests_FM(context).Test_FM_RippleSound_Clean();
		}

		private void Test_FM_RippleSound_Clean()
			=> WrapUp_Test(RippleSound_Clean(Frequencies.A4));

		[TestMethod]
		public void Test_Synthesizer_FM_RippleSound_FantasyEffect()
		{
			using (IContext context = PersistenceHelper.CreateContext())
				new SynthesizerTests_FM(context).Test_FM_RippleSound_FantasyEffect();
		}

		private void Test_FM_RippleSound_FantasyEffect()
			=> WrapUp_Test(RippleSound_FantasyEffect(Frequencies.A4));

		[TestMethod]
		public void Test_Synthesizer_FM_RippleSound_CoolDouble()
		{
			using (IContext context = PersistenceHelper.CreateContext())
				new SynthesizerTests_FM(context).Test_FM_RippleSound_CoolDouble();
		}

		private void Test_FM_RippleSound_CoolDouble()
			=> WrapUp_Test(RippleSound_CoolDouble(Frequencies.A4));

		// FM Noise Tests

		[TestMethod]
		public void Test_Synthesizer_FM_Noise_Beating()
		{
			using (IContext context = PersistenceHelper.CreateContext())
				new SynthesizerTests_FM(context).Test_FM_Noise_Beating();
		}

		private void Test_FM_Noise_Beating()
			=> WrapUp_Test(Create_FM_Noise_Beating(Frequencies.A4));

		// Generic Method

		/// <summary>
		/// Runs a test for FM synthesis and outputs the result to a file.
		/// Also, the entity data will be verified.
		/// </summary>
		private void WrapUp_Test(
			Outlet outlet,
			double totalTime = DEFAULT_TOTAL_TIME,
			[CallerMemberName] string callerMemberName = null)
		{
			// Add Echo (for fun)
			outlet = EntityFactory.CreateEcho(_operatorFactory, outlet, count: 10, denominator: 4, delay: 0.33);

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

		// Instrument Patches

		// Tuba

		/// <summary>
		/// Sounds like Tuba at beginning.
		/// FM with mod speed below sound freq, changes sound freq to +/- 5Hz.
		/// Volume curve is applied.
		/// Higher notes are shorter, lower notes are much longer.
		/// </summary>
		private Outlet TubaNote(double freq = Frequencies.A1, double volume = 1, double delay = 0)
		{
			var x = _operatorFactory;

			// FM Algorithm
			var outlet = FMInHertz(soundFreq: freq * 2, modSpeed: freq, modDepth: 5);

			// Stretch Volume Curve (longer when lower)
			const double durationA1 = 0.8;
			double stretch = durationA1 * Math.Pow(Frequencies.A1 / freq, 1.5);
			var curveOutlet = x.TimeMultiply(x.CurveIn(TubaCurve), x.Value(stretch));

			// Apply Volume Curve
			outlet = x.Multiply(outlet, curveOutlet);

			// Note Volume
			outlet = x.Multiply(outlet, x.Value(volume));

			// Note Start
			outlet = x.TimeAdd(outlet, x.Value(delay));

			return outlet;
		}

		// Flutes

		/// <summary> High hard flute: mod speed above sound freq, changes sound freq * [-0.005, 0.005] (erroneously) </summary>
		private Outlet FluteNote1(double freq = Frequencies.A4)
			=> FMAround0(soundFreq: freq / 2, modSpeed: freq, modDepth: 0.005);

		/// <summary> Yet another flute: mod speed above sound freq, changes sound freq * 1 +/- 0.005 </summary>
		private Outlet FluteNote2(double freq = Frequencies.A4)
			=> FMAroundFreq(soundFreq: freq, modSpeed: freq * 2, modDepth: 0.005);

		/// <summary> Yet another flute: mod speed above sound freq, changes sound freq * 1 +/- 0.005 </summary>
		private Outlet FluteNote3(double freq = Frequencies.A4)
			=> FMAroundFreq(soundFreq: freq, modSpeed: freq * 4, modDepth: 0.005);

		/// <summary> Modulated hard flute: mod speed below sound freq, changes sound freq * [-0.005, 0.005] (erroneously) </summary>
		private Outlet FluteNote4(double freq = Frequencies.A4)
			=> FMAround0(soundFreq: freq * 2, modSpeed: freq, modDepth: 0.005);

		// Ripple Effects

		/// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.005 </summary>
		private Outlet RippleNote_DeepMetallic(double freq = Frequencies.A1)
			=> FMAroundFreq(soundFreq: freq * 8, modSpeed: freq / 2, modDepth: 0.005);

		/// <summary> Mod speed below sound freq, changes sound freq ±10Hz </summary>
		private Outlet RippleNote_SharpMetallic(double freq = Frequencies.A3)
			=> FMInHertz(soundFreq: freq, modSpeed: freq / 2, modDepth: 10);

		/// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.005 </summary>
		private Outlet RippleSound_Clean(double freq = Frequencies.A5)
			=> FMAroundFreq(soundFreq: freq, modSpeed: 20, modDepth: 0.005);

		/// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.02 </summary>
		private Outlet RippleSound_FantasyEffect(double freq = Frequencies.A5)
			=> FMAroundFreq(soundFreq: freq, modSpeed: 10, modDepth: 0.02);

		/// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.05 </summary>
		private Outlet RippleSound_CoolDouble(double freq = Frequencies.A5)
			=> FMAroundFreq(soundFreq: freq, modSpeed: 10, modDepth: 0.05);

		// Noise

		/// <summary>
		/// Beating audible further along the sound.
		/// Mod speed much below sound freq, changes sound freq drastically * [0.5, 1.5]
		/// </summary>
		private Outlet Create_FM_Noise_Beating(double pitch = Frequencies.A5)
			=> FMAroundFreq(soundFreq: pitch, modSpeed: 55, modDepth: 0.5);

		// Algorithms

		/// <summary> FM sound synthesis modulating with addition. Modulates sound freq to +/- a number of Hz. </summary>
		/// <param name="modDepth">In Hz</param>
		private Outlet FMInHertz(double soundFreq, double modSpeed, double modDepth)
		{
			OperatorFactory x = _operatorFactory;

			Outlet modulator = x.Sine(x.Value(modDepth),      x.Value(modSpeed));
			Outlet sound = x.Sine(x.Value(DEFAULT_AMPLITUDE), x.Add(x.Value(soundFreq), modulator));
			return sound;
		}

		/// <summary> FM with (faulty) multiplication around 0. </summary>
		private Outlet FMAround0(double soundFreq, double modSpeed, double modDepth)
		{
			OperatorFactory x = _operatorFactory;

			Outlet modulator = x.Sine(x.Value(modDepth),      x.Value(modSpeed));
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

		private Curve _tubaCurve;

		private Curve TubaCurve
		{
			get
			{
				if (_tubaCurve == null)
				{
					_tubaCurve = _curveFactory.CreateCurve
					(
						new NodeInfo(time: 0.00, value: 1),
						new NodeInfo(time: 0.93, value: 1),
						new NodeInfo(time: 1.00, value: 0)
					);
				}
				return _tubaCurve;
			}
		}

		private AudioFileOutput ConfigureAudioFileOutput(string fileName, Outlet outlet, double totalTime)
		{
			AudioFileOutput audioFileOutput = _audioFileOutputManager.CreateAudioFileOutput();
			audioFileOutput.Duration = totalTime;
			audioFileOutput.Amplifier = Int16.MaxValue / 2.0;
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
