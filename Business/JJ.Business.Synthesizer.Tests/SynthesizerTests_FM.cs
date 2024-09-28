using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.AudioFileOutputs;
using JJ.Business.Synthesizer.Enums;
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
        private const double DEFAULT_TOTAL_VOLUME = 0.5;
        private const double DEFAULT_AMPLITUDE = 1.0;
        private const double BEAT = 0.6;
        private const double BAR = 2.4;

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

        // Composition Test
        
        [TestMethod]
        public void Test_Synthesizer_FM_Composition()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Composition();
        }

        private void Test_FM_Composition() 
            => WrapUp_Test(MildEcho(Composition()), duration: 19.2, volume: 0.27);

        // Flute Tests

        [TestMethod]
        public void Test_Synthesizer_FM_Flute_Melody1()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Flute_Melody1();
        }
        
        private void Test_FM_Flute_Melody1() 
            => WrapUp_Test(MildEcho(FluteMelody1()), duration: 10.8, volume: 0.6);

        [TestMethod]
        public void Test_Synthesizer_FM_Flute_Melody2()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Flute_Melody2();
        }

        private void Test_FM_Flute_Melody2() 
            => WrapUp_Test(MildEcho(FluteMelody2()), 8.46, volume: 0.47);

        [TestMethod]
        public void Test_Synthesizer_FM_Flute1()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Flute1();
        }

        private void Test_FM_Flute1()
            => WrapUp_Test(MildEcho(Flute1()));

        [TestMethod]
        public void Test_Synthesizer_FM_Flute2()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Flute2();
        }

        private void Test_FM_Flute2()
            => WrapUp_Test(MildEcho(Flute2()));

        [TestMethod]
        public void Test_Synthesizer_FM_Flute3()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Flute3();
        }

        private void Test_FM_Flute3()
            => WrapUp_Test(MildEcho(Flute3()));

        [TestMethod]
        public void Test_Synthesizer_FM_Flute4()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Flute4();
        }

        private void Test_FM_Flute4()
            => WrapUp_Test(MildEcho(Flute4()));

        // Pad Tests

        [TestMethod]
        public void Test_Synthesizer_FM_Pad()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Pad();
        }

        private void Test_FM_Pad()
            => WrapUp_Test(MildEcho(Pad(DEFAULT_TOTAL_TIME)));

        // Tube Tests

        [TestMethod]
        public void Test_Synthesizer_FM_Tuba()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Tuba();
        }

        private void Test_FM_Tuba() 
            => WrapUp_Test(MildEcho(Tuba(Frequencies.E2)));

        [TestMethod]
        public void Test_Synthesizer_FM_Tuba_Melody1()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Tuba_Melody1();
        }

        private void Test_FM_Tuba_Melody1() 
            => WrapUp_Test(MildEcho(TubaMelody1()), duration: 4.4);

        [TestMethod]
        public void Test_Synthesizer_FM_Tuba_Melody2()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Tuba_Melody2();
        }

        private void Test_FM_Tuba_Melody2()
            => WrapUp_Test(MildEcho(TubaMelody2()), duration: 11.6, volume: 0.75);

        [TestMethod]
        public void Test_Synthesizer_FM_Tuba_Melody3()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Tuba_Melody3();
        }

        private void Test_FM_Tuba_Melody3()
            => WrapUp_Test(MildEcho(TubaMelody3()), duration: 8, volume: 0.75);

        // Ripple Effect Tests

        [TestMethod]
        public void Test_Synthesizer_FM_RippleNote_DeepMetallic()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_RippleNote_DeepMetallic();
        }

        private void Test_FM_RippleNote_DeepMetallic()
            => WrapUp_Test(DeepEcho(RippleNote_DeepMetallic(DEFAULT_TOTAL_TIME)));

        [TestMethod]
        public void Test_Synthesizer_FM_RippleNote_DeepMetallic_Melody1()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_RippleNote_DeepMetallic_Melody1();
        }

        private void Test_FM_RippleNote_DeepMetallic_Melody1() 
            => WrapUp_Test(DeepEcho(RippleMelody1_DeepMetallic()), duration: 11.4, volume: 0.5);

        [TestMethod]
        public void Test_Synthesizer_FM_RippleNote_DeepMetallic_Melody2()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_RippleNote_DeepMetallic_Melody2();
        }
        
        private void Test_FM_RippleNote_DeepMetallic_Melody2() 
            => WrapUp_Test(DeepEcho(RippleMelody2_DeepMetallic()), duration: 14.3, volume: 0.5);

        [TestMethod]
        public void Test_Synthesizer_FM_RippleNote_SharpMetallic()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_RippleNote_SharpMetallic();
        }

        private void Test_FM_RippleNote_SharpMetallic()
            => WrapUp_Test(MildEcho(RippleNote_SharpMetallic()));

        [TestMethod]
        public void Test_Synthesizer_FM_RippleSound_Clean()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_RippleSound_Clean();
        }

        private void Test_FM_RippleSound_Clean()
            => WrapUp_Test(MildEcho(RippleSound_Clean(duration: DEFAULT_TOTAL_TIME)));

        [TestMethod]
        public void Test_Synthesizer_FM_RippleSound_FantasyEffect()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_RippleSound_FantasyEffect();
        }

        private void Test_FM_RippleSound_FantasyEffect()
            => WrapUp_Test(MildEcho(RippleSound_FantasyEffect()));

        [TestMethod]
        public void Test_Synthesizer_FM_RippleSound_CoolDouble()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_RippleSound_CoolDouble();
        }

        private void Test_FM_RippleSound_CoolDouble()
            => WrapUp_Test(MildEcho(RippleSound_CoolDouble()));

        // FM Noise Tests

        [TestMethod]
        public void Test_Synthesizer_FM_Noise_Beating()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Noise_Beating();
        }

        private void Test_FM_Noise_Beating()
            => WrapUp_Test(MildEcho(Create_FM_Noise_Beating()));

        // Generic Method

        /// <summary>
        /// Runs a test for FM synthesis and outputs the result to a file.
        /// Also, the entity data will be verified.
        /// </summary>
        private void WrapUp_Test(
            Outlet outlet,
            double duration = DEFAULT_TOTAL_TIME,
            double volume = DEFAULT_TOTAL_VOLUME,
            [CallerMemberName] string callerMemberName = null)
        {
            // Configure AudioFileOutput
            AudioFileOutput audioFileOutput = ConfigureAudioFileOutput($"{callerMemberName}.wav", outlet, duration, volume);

            // Verify
            AssertEntities(audioFileOutput, outlet);

            // Calculate
            Stopwatch stopWatch = Calculate(audioFileOutput);

            // Report
            Console.WriteLine($"Calculation time: {stopWatch.ElapsedMilliseconds}ms{Environment.NewLine}" +
                              $"Output file: {Path.GetFullPath(audioFileOutput.FilePath)}");
        }

        // Composition

        private Outlet Composition()
        {
            var x = _operatorFactory;

            var pattern1 = x.Adder
            (
                FluteMelody1(),
                TubaMelody2(),
                RippleMelody2_DeepMetallic()
            );

            var pattern2 = x.Adder
            (
                FluteMelody2(),
                TubaMelody3(),
                RippleMelody1_DeepMetallic()
            );

            var composition = x.Adder(
                pattern1,
                x.TimeAdd(pattern2, x.Value(BAR * 4)));

            return composition;
        }

        // Melodies

        private Outlet FluteMelody1() => _operatorFactory.Adder
        (
            Flute1(Frequencies.E4,       BAR * 0 + BEAT * 0.0, volume: 0.80, duration: 1.2),
            Flute2(Frequencies.F4_Sharp, BAR * 0 + BEAT * 1.5, volume: 0.70, duration: 1.3),
            Flute1(Frequencies.G4_Sharp, BAR * 0 + BEAT * 3.0, volume: 0.60, duration: 0.6),
            Flute1(Frequencies.A4,       BAR * 1 + BEAT * 0.0, volume: 0.80, duration: 1.4),
            Flute3(Frequencies.B4,       BAR * 1 + BEAT * 1.5, volume: 0.50, duration: 0.6),
            Flute1(Frequencies.A4,       BAR * 1 + BEAT * 3.0, volume: 0.55, duration: 1.0),
            //RippleSound_Clean(Frequencies.A4, BAR * 2, volume: 0.50, duration: BAR * 2),
            Flute2(Frequencies.G4,       BAR * 2 + BEAT * 0.0, volume: 1.00, duration: 1.2),
            Flute1(Frequencies.E5,       BAR * 2 + BEAT * 1.5, volume: 0.80, duration: 1.5)
        );

        private Outlet FluteMelody2() => _operatorFactory.Adder
        (
            Flute1(Frequencies.E4, BAR * 0 + BEAT * 0.0, volume: 1.0),
            Flute2(Frequencies.F4, BAR * 0 + BEAT * 1.5, volume: 1.0 / 0.85),
            Flute3(Frequencies.G4, BAR * 0 + BEAT * 3.0, volume: 1.0 / 0.80),
            Flute4(Frequencies.A4, BAR * 1 + BEAT * 0.0, volume: 1.0 / 0.70),
            Flute3(Frequencies.B4, BAR * 1 + BEAT * 1.5, volume: 1.0 / 0.80),
            Flute2(Frequencies.G4, BAR * 1 + BEAT * 3.0, volume: 1.0 / 0.85),
            Flute4(Frequencies.A4, BAR * 2 + BEAT * 0.0, volume: 1.2 / 0.70, duration: 1.66)
        );
                                        
        private Outlet TubaMelody1() => _operatorFactory.Adder
        (
            Tuba(Frequencies.A1),
            Tuba(Frequencies.E2,       BEAT * 2),
            Tuba(Frequencies.F1_Sharp, BEAT * 4, volume: 0.7)
        );

        private Outlet TubaMelody2() => _operatorFactory.Adder
        (
            Tuba(Frequencies.A2, BEAT * 00),
            Tuba(Frequencies.E3, BEAT * 02),
            Tuba(Frequencies.F2, BEAT * 04),
            Tuba(Frequencies.C3, BEAT * 06),
            Tuba(Frequencies.C2, BEAT * 08),
            Tuba(Frequencies.G2, BEAT * 10),
            Tuba(Frequencies.G1, BEAT * 12),
            Tuba(Frequencies.D3, BEAT * 14)
        );

        private Outlet TubaMelody3() => _operatorFactory.Adder
        (
            Tuba(Frequencies.A2, BEAT * 0),
            Tuba(Frequencies.E3, BEAT * 2),
            Tuba(Frequencies.F2, BEAT * 4),
            Tuba(Frequencies.C3, BEAT * 6),
            Tuba(Frequencies.A1, BEAT * 8)
        );

        private Outlet RippleMelody1_DeepMetallic() => _operatorFactory.Adder
        (
            RippleNote_DeepMetallic(Frequencies.A2, delay: 0.0, duration: 3.0),
            RippleNote_DeepMetallic(Frequencies.F2, delay: 2.4, duration: 3.0),
            RippleNote_DeepMetallic(Frequencies.A1, delay: 4.8, duration: 3.6)
        );

        private Outlet RippleMelody2_DeepMetallic() => _operatorFactory.Adder
        (
            RippleNote_DeepMetallic(Frequencies.A2, delay: 0.0,     duration: 3.0),
            RippleNote_DeepMetallic(Frequencies.F2, delay: BAR,     duration: 3.0),
            RippleNote_DeepMetallic(Frequencies.C2, delay: BAR * 2, duration: 3.0),
            RippleNote_DeepMetallic(Frequencies.G2, delay: BAR * 3, duration: 3.6, volume: 0.7)
        );

        // Instruments

        /// <summary> High hard flute: mod speed above sound freq, changes sound freq * [-0.005, 0.005] (erroneously) </summary>
        private Outlet Flute1(double freq = Frequencies.A4, double delay = 0, double volume = 1, double duration = 1)
        {
            var x = _operatorFactory;

            // FM Algorithm
            Outlet outlet = FMAround0(soundFreq: freq / 2, modSpeed: freq, modDepth: 0.005);

            // Curve
            outlet = x.Multiply(outlet, StretchCurve(FluteCurve, duration));

            // Volume and Delay
            outlet = StrikeNote(outlet, delay, volume);
            
            return outlet;
        }

        /// <summary> Yet another flute: mod speed above sound freq, changes sound freq * 1 +/- 0.005 </summary>
        private Outlet Flute2(double freq = Frequencies.A4, double delay = 0, double volume = 1, double duration = 1)
        {
            var x = _operatorFactory;

            // FM Algorithm
            Outlet outlet = FMAroundFreq(soundFreq: freq, modSpeed: freq * 2, modDepth: 0.005);
            
            // Curve
            outlet = x.Multiply(outlet, StretchCurve(FluteCurve, duration));

            // Volume and Delay
            double normalizer = 0.85;
            outlet = StrikeNote(outlet, delay, volume * normalizer);

            return outlet;
        }

        /// <summary> Yet another flute: mod speed above sound freq, changes sound freq * 1 +/- 0.005 </summary>
        private Outlet Flute3(double freq = Frequencies.A4, double delay = 0, double volume = 1, double duration = 1)
        {
            var x = _operatorFactory;

            // FM Algorithm
            Outlet outlet = FMAroundFreq(soundFreq: freq, modSpeed: freq * 4, modDepth: 0.005);

            // Curve
            outlet = x.Multiply(outlet, StretchCurve(FluteCurve, duration));

            // Volume and Delay
            double normalizer = 0.80;
            outlet = StrikeNote(outlet, delay, volume * normalizer);

            return outlet;
        }

        /// <summary> Modulated hard flute: mod speed below sound freq, changes sound freq * [-0.005, 0.005] (erroneously) </summary>
        private Outlet Flute4(double freq = Frequencies.A4, double delay = 0, double volume = 1, double duration = 1)
        {
            var x = _operatorFactory;
           
            // FM Algorithm
            Outlet outlet = FMAround0(soundFreq: freq * 2, modSpeed: freq, modDepth: 0.005);
            
            // Volume Curve
            outlet = x.Multiply(outlet, StretchCurve(FluteCurve, duration));

            // Apply Volume and Delay
            double normalizer = 0.70;
            outlet = StrikeNote(outlet, delay, volume * normalizer);

            return outlet;
        }
        
        private Outlet Pad(double freq = Frequencies.A4, double delay = 0, double volume = 1, double duration = 1)
        {
            var x = _operatorFactory;

            // FM Algorithm
            Outlet outlet = FMAroundFreq(freq, freq * 1.5, x.CurveIn(PadModCurve));
                        
            // Volume Curve
            outlet = x.Multiply(outlet, StretchCurve(PadCurve, duration));

            // Apply Volume and Delay
            outlet = StrikeNote(outlet, delay, volume);

            return outlet;
        }


        /// <summary>
        /// Sounds like Tuba at beginning.
        /// FM with mod speed below sound freq, changes sound freq to +/- 5Hz.
        /// Volume curve is applied.
        /// Higher notes are shorter, lower notes are much longer.
        /// </summary>
        private Outlet Tuba(double freq = Frequencies.A1, double delay = 0, double volume = 1)
        {
            var x = _operatorFactory;

            // FM Algorithm
            var outlet = FMInHertz(soundFreq: freq * 2, modSpeed: freq, modDepth: 5);

            // Stretch Volume Curve (longer when lower)
            const double durationA1 = 0.8;
            double stretch = durationA1 * Math.Pow(Frequencies.A1 / freq, 1.5);
            var curveOutlet = x.TimeMultiply(x.CurveIn(TubaCurve), x.Value(stretch));

            // Apply Curve
            outlet = x.Multiply(outlet, curveOutlet);

            // Volume and Delay
            outlet = StrikeNote(outlet, delay, volume);

            return outlet;
        }

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.005 </summary>
        private Outlet RippleNote_DeepMetallic(double freq = Frequencies.A1, double delay = 0, double volume = 1, double duration = 1)
        {
            var x = _operatorFactory;

            // FM algorithm
            Outlet outlet = FMAroundFreq(soundFreq: freq * 8, modSpeed: freq / 2, modDepth: 0.005);

            // Curve
            outlet = x.Multiply(outlet, StretchCurve(RippleCurve, duration));

            // Volume and Delay
            outlet = StrikeNote(outlet, delay, volume);

            return outlet;
        }

        /// <summary> Mod speed below sound freq, changes sound freq ±10Hz </summary>
        private Outlet RippleNote_SharpMetallic(double freq = Frequencies.A3)
            => FMInHertz(soundFreq: freq, modSpeed: freq / 2, modDepth: 10);

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.005 </summary>
        private Outlet RippleSound_Clean(double freq = Frequencies.A4, double delay = 0, double volume = 1, double duration = 1)
        {
            var x = _operatorFactory;

            // FM algorithm
            Outlet outlet = FMAroundFreq(soundFreq: freq, modSpeed: 20, modDepth: 0.005);

            // Curve
            outlet = x.Multiply(outlet, StretchCurve(RippleCurve, duration));

            // Volume and Delay
            outlet = StrikeNote(outlet, delay, volume);

            return outlet;
        }

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.02 </summary>
        private Outlet RippleSound_FantasyEffect(double freq = Frequencies.A5)
            => FMAroundFreq(soundFreq: freq, modSpeed: 10, modDepth: 0.02);

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.05 </summary>
        private Outlet RippleSound_CoolDouble(double freq = Frequencies.A5)
            => FMAroundFreq(soundFreq: freq, modSpeed: 10, modDepth: 0.05);

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
            => FMAroundFreq(soundFreq, modSpeed, (Outlet)_operatorFactory.Value(modDepth));

        /// <summary> FM with multiplication around 1. </summary>
        private Outlet FMAroundFreq(double soundFreq, double modSpeed, Outlet modDepth)
        {
            OperatorFactory x = _operatorFactory;

            Outlet modulator = x.Add(x.Value(1), x.Sine(modDepth, x.Value(modSpeed)));
            Outlet sound = x.Sine(x.Value(DEFAULT_AMPLITUDE), x.Multiply(x.Value(soundFreq), modulator));
            return sound;
        }

        private Outlet StrikeNote(Outlet outlet, double delay = 0.0, double volume = 1.0)
        {
            OperatorFactory x = _operatorFactory;
            // Note Start
            if (delay != 0.0) outlet = x.TimeAdd(outlet, x.Value(delay));
            // Note Volume
            if (volume != 1.0) outlet = x.Multiply(outlet, x.Value(volume));
            return outlet;
        }
        
        private Outlet StretchCurve(Curve curve, double duration)
        {
            var x = _operatorFactory;

            Outlet outlet = x.CurveIn(curve);
            if (duration != 1)
            {
                outlet = x.TimeMultiply(outlet, x.Value(duration));
            }

            return outlet;
        }
        
        private Outlet MildEcho(Outlet outlet)
            => EntityFactory.CreateEcho(_operatorFactory, outlet, count: 6, denominator: 4, delay: 0.33);

        private Outlet DeepEcho(Outlet melody)
            => EntityFactory.CreateEcho(_operatorFactory, melody, count: 6, denominator: 2, delay: 0.5);

        // Curves

        private Curve _fluteCurve;
        private Curve FluteCurve
        {
            get
            {
                if (_fluteCurve == null)
                {
                    _fluteCurve = _curveFactory.CreateCurve
                    (
                        new NodeInfo(time: 0.00, value: 0.0),
                        new NodeInfo(time: 0.05, value: 0.8),
                        new NodeInfo(time: 0.10, value: 1.0),
                        new NodeInfo(time: 0.90, value: 0.7),
                        new NodeInfo(time: 1.00, value: 0.0)
                    );
                }
                return _fluteCurve;
            }
        }

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

        private Curve _rippleCurve;
        private Curve RippleCurve
        {
            get
            {
                if (_rippleCurve == null)
                {
                    _rippleCurve = _curveFactory.CreateCurve
                    (
                        new NodeInfo(time: 0.00, value: 0.00),
                        new NodeInfo(time: 0.01, value: 0.75),
                        new NodeInfo(time: 0.05, value: 0.50),
                        new NodeInfo(time: 0.25, value: 1.00),
                        new NodeInfo(time: 1.00, value: 0.00)
                    );
                }
                return _rippleCurve;
            }
        }

        private Curve _padCurve;
        private Curve PadCurve
        {
            get
            {
                if (_padCurve == null)
                {
                    _padCurve = _curveFactory.CreateCurve
                    (
                        new NodeInfo(time: 0, value: 1, NodeTypeEnum.Block),
                        new NodeInfo(time: 1, value: 0, NodeTypeEnum.Block)
                    );
                }
                return _padCurve;
            }
        }

        private Curve _padModCurve;
        private Curve PadModCurve
        {
            get
            {
                if (_padModCurve == null)
                {
                    _padModCurve = _curveFactory.CreateCurve
                    (
                        new NodeInfo(time: 0, value: 0.002),
                        new NodeInfo(time: 1, value: 0)
                    );
                }
                return _padModCurve;
            }
        }

        // Steps

        private AudioFileOutput ConfigureAudioFileOutput(string fileName, Outlet outlet, double totalTime, double volume)
        {
            AudioFileOutput audioFileOutput = _audioFileOutputManager.CreateAudioFileOutput();
            audioFileOutput.Duration = totalTime;
            audioFileOutput.Amplifier = short.MaxValue * volume;
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
