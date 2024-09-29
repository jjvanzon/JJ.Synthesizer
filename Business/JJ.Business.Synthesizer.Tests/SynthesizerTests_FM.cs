using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
// ReSharper disable once InconsistentNaming
// ReSharper disable ParameterHidesMember

namespace JJ.Business.Synthesizer.Tests
{
    /// <summary>
    /// NOTE: Version 0.0.250 does not have time tracking in its oscillator,
    /// making the FM synthesis behave differently.
    /// </summary>
    [TestClass]
    public class SynthesizerTests_FM
    {
        private const double DEFAULT_TOTAL_TIME = 1.0 + DEEP_ECHO_TIME;
        private const double DEFAULT_TOTAL_VOLUME = 0.5;
        private const double DEFAULT_AMPLITUDE = 1.0;
        private const double BEAT = 0.4;
        private const double BAR = 1.6;

        private readonly IContext _context;
        private readonly CurveFactory _curveFactory;
        private readonly AudioFileOutputManager _audioFileOutputManager;
        
        /// <summary> x for syntactic sugar. </summary>
        private readonly OperatorFactory x;

        /// <summary> Constructor for test runner. </summary>
        public SynthesizerTests_FM() { }

        /// <summary> Constructor allowing each test to run in its own instance. </summary>
        public SynthesizerTests_FM(IContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _curveFactory = TestHelper.CreateCurveFactory(context);
            x = TestHelper.CreateOperatorFactory(_context);
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
            => WrapUp_Test(MildEcho(Composition()), duration: BAR * 8 + BEAT + DEEP_ECHO_TIME, volume: 0.20);

        // Low Modulation Test

        [TestMethod]
        public void Test_Synthesizer_FM_LowModulation()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_LowModulation();
        }
        
        private void Test_FM_LowModulation()
        {
            double duration = BAR * 8 + MILD_ECHO_TIME;
            WrapUp_Test(MildEcho(LowModulation(x.Value(Frequencies.A4), duration: duration)), duration);
        }

        // Flute Tests

        [TestMethod]
        public void Test_Synthesizer_FM_Flute_Melody1()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Flute_Melody1();
        }
        
        private void Test_FM_Flute_Melody1() 
            => WrapUp_Test(MildEcho(FluteMelody1()), duration: BAR * 4 + MILD_ECHO_TIME, volume: 0.6);

        [TestMethod]
        public void Test_Synthesizer_FM_Flute_Melody2()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Flute_Melody2();
        }

        private void Test_FM_Flute_Melody2() 
            => WrapUp_Test(MildEcho(FluteMelody2()), BAR * 2.5 + MILD_ECHO_TIME, volume: 0.4);

        [TestMethod]
        public void Test_Synthesizer_FM_Flute1()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Flute1();
        }

        private void Test_FM_Flute1()
            => WrapUp_Test(MildEcho(Flute1(Frequencies.A4)));

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
            => WrapUp_Test(MildEcho(Flute3(Frequencies.A4)));

        [TestMethod]
        public void Test_Synthesizer_FM_Flute4()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Flute4();
        }

        private void Test_FM_Flute4()
            => WrapUp_Test(MildEcho(Flute4(Frequencies.A4)));

        // Pad Tests

        [TestMethod]
        public void Test_Synthesizer_FM_Pad()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Pad();
        }

        private void Test_FM_Pad()
            => WrapUp_Test(MildEcho(Pad(duration: 1.5)), duration: 1.5 + MILD_ECHO_TIME);

        [TestMethod]
        public void Test_Synthesizer_FM_Pad_ChordProgression()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Pad_ChordProgression();
        }

        private void Test_FM_Pad_ChordProgression()
            => WrapUp_Test(MildEcho(PadChordProgression()), duration: BAR * 8 + MILD_ECHO_TIME, volume: 0.22);

        // Electric Note Tests

        [TestMethod]
        public void Test_Synthesizer_FM_ElectricNote()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_ElectricNote();
        }

        private void Test_FM_ElectricNote()
            => WrapUp_Test(MildEcho(ElectricNote(duration: 1.5)), duration: 1.5 + MILD_ECHO_TIME);

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
            => WrapUp_Test(MildEcho(TubaMelody1()), duration: BAR * 4 + MILD_ECHO_TIME, volume: 0.5);

        [TestMethod]
        public void Test_Synthesizer_FM_Tuba_Melody2()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Tuba_Melody2();
        }

        private void Test_FM_Tuba_Melody2()
            => WrapUp_Test(MildEcho(TubaMelody2()), duration:  BAR * 2.5 + MILD_ECHO_TIME, volume: 0.75);

        [TestMethod]
        public void Test_Synthesizer_FM_Tuba_Melody3()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Tuba_Melody3();
        }

        private void Test_FM_Tuba_Melody3() 
            => WrapUp_Test(MildEcho(TubaMelody3()), duration: BAR * 1.5 + MILD_ECHO_TIME);

        // Ripple Effect Tests

        [TestMethod]
        public void Test_Synthesizer_FM_RippleBass()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_RippleBass();
        }

        private void Test_FM_RippleBass()
            => WrapUp_Test(DeepEcho(RippleBass(duration: 3.0)), duration: 3.0 + DEEP_ECHO_TIME);

        [TestMethod]
        public void Test_Synthesizer_FM_RippleBass_Melody1()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_RippleBass_Melody1();
        }
        
        private void Test_FM_RippleBass_Melody1() 
            => WrapUp_Test(DeepEcho(RippleBassMelody1()), duration: BAR * 5 + DEEP_ECHO_TIME, volume: 0.3);
        
        [TestMethod]
        public void Test_Synthesizer_FM_RippleBass_Melody2()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_RippleBass_Melody2();
        }

        private void Test_FM_RippleBass_Melody2() 
            => WrapUp_Test(DeepEcho(RippleBassMelody2()), duration: BAR * 4.0 + DEEP_ECHO_TIME, volume: 0.3);

        [TestMethod]
        public void Test_Synthesizer_FM_RippleNote_SharpMetallic()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_RippleNote_SharpMetallic();
        }

        private void Test_FM_RippleNote_SharpMetallic()
            => WrapUp_Test(MildEcho(RippleNote_SharpMetallic(Frequencies.A3)));

        [TestMethod]
        public void Test_Synthesizer_FM_RippleSound_Clean()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_RippleSound_Clean();
        }

        private void Test_FM_RippleSound_Clean()
            => WrapUp_Test(DeepEcho(RippleSound_Clean(duration: 3.0)), duration: 3.0 + DEEP_ECHO_TIME);

        [TestMethod]
        public void Test_Synthesizer_FM_RippleSound_FantasyEffect()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_RippleSound_FantasyEffect();
        }

        private void Test_FM_RippleSound_FantasyEffect()
            => WrapUp_Test(MildEcho(RippleSound_FantasyEffect(Frequencies.A5)), duration: 3);

        [TestMethod]
        public void Test_Synthesizer_FM_RippleSound_CoolDouble()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_RippleSound_CoolDouble();
        }

        private void Test_FM_RippleSound_CoolDouble()
            => WrapUp_Test(MildEcho(RippleSound_CoolDouble(Frequencies.A5)), duration: 3);

        // FM Noise Tests

        [TestMethod]
        public void Test_Synthesizer_FM_Noise_Beating()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Noise_Beating();
        }

        private void Test_FM_Noise_Beating()
            => WrapUp_Test(MildEcho(Create_FM_Noise_Beating(Frequencies.A4)), duration: 3);

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
            double fluteVolume = 1.1;
            double padVolume = 0.3;
            double tubaVolume = 0.7;
            double rippleBassVolume = 0.7;

            var pattern1 = x.Adder
            (
                x.Multiply(x.Value(fluteVolume), FluteMelody1()),
                x.Multiply(x.Value(padVolume), PadChordProgression()),
                x.Multiply(x.Value(tubaVolume), TubaMelody1())
                //x.Multiply(x.Value(rippleBassVolume), RippleBassMelody1())
            );

            var pattern2 = x.Adder
            (
                x.Multiply(x.Value(fluteVolume), FluteMelody2()),
                x.Multiply(x.Value(tubaVolume), TubaMelody2()),
                x.Multiply(x.Value(rippleBassVolume), RippleBassMelody2())
            );

            var composition = x.Adder
            (
                pattern1,
                x.TimeAdd(pattern2, x.Value(BAR * 4))
                //RippleSound_Clean(Frequencies.A4, delay: BAR * 2, volume: 0.50, duration: BAR * 2),
            );

            return composition;
        }

        // Melodies

        private Outlet FluteMelody1()
        {
            double originalBeat = 0.6;
            double beatFactor = BEAT / originalBeat; // E.g. 0.4 / 0.6 = 0.66
            double portato = 0.9 / 0.66; // 1.3636..

            return x.Adder
            (
                Flute1(Frequencies.E4, BAR * 0 + BEAT * 0.0, volume: 0.80, duration: 1.2 * beatFactor * portato),
                Flute2(Frequencies.F4, BAR * 0 + BEAT * 1.5, volume: 0.70, duration: 1.3 * beatFactor * portato),
                Flute1(Frequencies.G4, BAR * 0 + BEAT * 3.0, volume: 0.60, duration: 0.6 * beatFactor * portato),
                Flute1(Frequencies.A4, BAR * 1 + BEAT * 0.0, volume: 0.80, duration: 1.4 * beatFactor * portato),
                Flute3(Frequencies.B4, BAR * 1 + BEAT * 1.5, volume: 0.50, duration: 0.6 * beatFactor * portato),
                Flute1(Frequencies.A4, BAR * 1 + BEAT * 3.0, volume: 0.55, duration: 1.0 * beatFactor * portato),
                Flute2(Frequencies.C4, BAR * 2 + BEAT * 0.0, volume: 1.00, duration: 1.2 * beatFactor * portato),
                Flute1(Frequencies.G4, BAR * 2 + BEAT * 1.5, volume: 0.80, duration: 1.5 * beatFactor * portato)
            );
        }

        private Outlet FluteMelody2() => x.Adder
        (
            Flute1(Frequencies.E4, BAR * 0 + BEAT * 0.0, volume: 1.0),
            Flute2(Frequencies.F4, BAR * 0 + BEAT * 1.5, volume: 1.15),
            Flute3(Frequencies.G4, BAR * 0 + BEAT * 3.0, volume: 1.25),
            Flute4(Frequencies.A4, BAR * 1 + BEAT * 0.0, volume: 1.40),
            Flute3(Frequencies.B4, BAR * 1 + BEAT * 1.5, volume: 1.25),
            Flute2(Frequencies.G4, BAR * 1 + BEAT * 3.0, volume: 1.15),
            Flute4(Frequencies.A4, BAR * 2 + BEAT * 0.0, volume: 1.70, duration: 1.66)
        );
        
        private Outlet PadChordProgression()
        {
            /*
            return x.Adder
            (
                x.Sine(x.Value(DEFAULT_AMPLITUDE), StretchCurve(PadPitchCurve1, BAR)),
                x.Sine(x.Value(DEFAULT_AMPLITUDE), StretchCurve(PadPitchCurve2, BAR)),
                x.Sine(x.Value(DEFAULT_AMPLITUDE), StretchCurve(PadPitchCurve3, BAR))
            );
            */

            /*
            return x.Adder
            (
                Pad(StretchCurve(PadPitchCurve1, BAR), duration: BAR * 8),
                Pad(StretchCurve(PadPitchCurve2, BAR), duration: BAR * 8),
                Pad(StretchCurve(PadPitchCurve3, BAR), duration: BAR * 8)
            );
            */

            return x.Adder
            (
                LowModulation(StretchCurve(PadPitchCurve1, BAR), duration: x.Value(BAR * 8)),
                LowModulation(StretchCurve(PadPitchCurve2, BAR), duration: x.Value(BAR * 8)),
                LowModulation(StretchCurve(PadPitchCurve3, BAR), duration: x.Value(BAR * 8))
            );
        }

        private Outlet TubaMelody1() => x.Adder
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

        private Outlet TubaMelody2() => x.Adder
        (
            Tuba(Frequencies.A2, BEAT * 0),
            Tuba(Frequencies.E3, BEAT * 2),
            Tuba(Frequencies.F2, BEAT * 4),
            Tuba(Frequencies.C3, BEAT * 6),
            Tuba(Frequencies.A1, BEAT * 8)
        );
        
        private Outlet TubaMelody3() => x.Adder
        (
            Tuba(Frequencies.A1),
            Tuba(Frequencies.E2,       BEAT * 2),
            Tuba(Frequencies.F1_Sharp, BEAT * 4, volume: 0.7)
        );

        private Outlet RippleBassMelody1() =>
            RippleBass(Frequencies.A2, delay: BAR * 0, duration: BAR * 2);

        private Outlet RippleBassMelody2() =>
            DeepEcho(RippleBass(Frequencies.A1, delay: BAR * 2.5, duration: BAR * 1.5));

        // Instruments

        private Outlet LowModulation(double freq = Frequencies.A4, double delay = 0, double volume = 1, double duration = 1)
            => LowModulation((Outlet)x.Value(freq), x.Value(delay), x.Value(volume), x.Value(duration));

        private Outlet LowModulation(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? x.Value(Frequencies.A4);

            // FM Algorithm
            Outlet curveDown = StretchCurve(LineDownCurve, x.Multiply(duration, x.Value(1.1)));
            Outlet outlet = FMAroundFreq(
                soundFreq: freq, 
                modSpeed: x.Multiply(freq, x.Value(2.0)), 
                modDepth: x.Multiply(curveDown, x.Value(0.00005)));

            // Volume and Delay
            outlet = StrikeNote(outlet, delay, volume);
            
            return outlet;
        }

        private Outlet Flute1(double freq = Frequencies.A4, double delay = 0, double volume = 1, double duration = 1)
            => Flute1((Outlet)x.Value(freq), x.Value(delay), x.Value(volume), x.Value(duration));

        /// <summary> High hard flute: mod speed above sound freq, changes sound freq * [-0.005, 0.005] (erroneously) </summary>
        private Outlet Flute1(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? x.Value(Frequencies.A4);

            // FM Algorithm
            Outlet outlet = FMAround0(x.Divide(freq, x.Value(2.0)), freq, x.Value(0.005));

            // Curve
            outlet = x.Multiply(outlet, StretchCurve(FluteCurve, duration));

            // Volume and Delay
            outlet = StrikeNote(outlet, delay, volume);
            
            return outlet;
        }

        private Outlet Flute2(double freq = Frequencies.A4, double delay = 0, double volume = 1, double duration = 1)
            => Flute2((Outlet)x.Value(freq), x.Value(delay), x.Value(volume), x.Value(duration));

        /// <summary> Yet another flute: mod speed above sound freq, changes sound freq * 1 +/- 0.005 </summary>
        private Outlet Flute2(Outlet freq, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? x.Value(Frequencies.A4);
            volume = volume ?? x.Value(1.0);

            // FM Algorithm
            Outlet outlet = FMAroundFreq(freq, x.Multiply(freq, x.Value(2.0)), x.Value(0.005));
            
            // Curve
            outlet = x.Multiply(outlet, StretchCurve(FluteCurve, duration));

            // Equalize Volume
            Outlet equalizedVolume = x.Multiply(volume, x.Value(0.85));

            // Volume and Delay
            outlet = StrikeNote(outlet, delay, equalizedVolume);

            return outlet;
        }

        private Outlet Flute3(double freq = Frequencies.A4, double delay = 0, double volume = 1, double duration = 1)
            => Flute3((Outlet)x.Value(freq), x.Value(delay), x.Value(volume), x.Value(duration));

        /// <summary> Yet another flute: mod speed above sound freq, changes sound freq * 1 +/- 0.005 </summary>
        private Outlet Flute3(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? x.Value(Frequencies.A4);
            volume = volume ?? x.Value(1.0);

            // FM Algorithm
            Outlet outlet = FMAroundFreq(freq, x.Multiply(freq, x.Value(4)), x.Value(0.005));

            // Curve
            outlet = x.Multiply(outlet, StretchCurve(FluteCurve, duration));

            // Equalize Volume
            var equalizedVolume = x.Multiply(volume, x.Value(0.80));

            // Volume and Delay
            outlet = StrikeNote(outlet, delay, equalizedVolume);

            return outlet;
        }

        private Outlet Flute4(double freq = Frequencies.A4, double delay = 0, double volume = 1, double duration = 1)
            => Flute4((Outlet)x.Value(freq), x.Value(delay), x.Value(volume), x.Value(duration));

        /// <summary> Modulated hard flute: mod speed below sound freq, changes sound freq * [-0.005, 0.005] (erroneously) </summary>
        private Outlet Flute4(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? x.Value(Frequencies.A4);
            volume = volume ?? x.Value(1.0);

            // FM Algorithm
            Outlet outlet = FMAround0(x.Multiply(freq, x.Value(2)), freq, x.Value(0.005));
            
            // Volume Curve
            outlet = x.Multiply(outlet, StretchCurve(FluteCurve, duration));

            // Equalize Volume
            var equalizedVolume = x.Multiply(volume, x.Value(0.70));

            // Apply Volume and Delay
            outlet = StrikeNote(outlet, delay, equalizedVolume);

            return outlet;
        }

        private Outlet Pad(double freq = Frequencies.A4, double delay = 0, double volume = 1, double duration = 1)
            => Pad((Outlet)x.Value(freq), x.Value(delay), x.Value(volume), x.Value(duration));

        private Outlet Pad(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? x.Value(Frequencies.A4);
            duration = duration ?? x.Value(1.0);

            // FM Algorithm
            Outlet curveDown = StretchCurve(LineDownCurve, x.Multiply(duration, x.Value(1.1)));

            Outlet outlet = x.Add
            (
                FMAroundFreq(freq, x.Multiply(freq, x.Value(2)), x.Multiply(x.Value(0.004), curveDown)),
                FMAroundFreq(freq, x.Multiply(freq, x.Value(3)), x.Multiply(x.Value(0.003), curveDown))
            );

            // Volume Curve
            outlet = x.Multiply(outlet, StretchCurve(DampedBlockCurve, duration));

            // Equalize Loudness
            var equalizedVolume = x.Multiply(volume, x.Value(0.6));

            // Apply Volume and Delay
            outlet = StrikeNote(outlet, delay, equalizedVolume);

            return outlet;
        }
        
        private Outlet ElectricNote(double freq = Frequencies.A4, double delay = 0, double volume = 1, double duration = 1)
            => ElectricNote((Outlet)x.Value(freq), x.Value(delay), x.Value(volume), x.Value(duration));

        private Outlet ElectricNote(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? x.Value(Frequencies.A4);
            volume = volume ?? x.Value(1);

            // FM Algorithm
            Outlet outlet = x.Add
            (
                FMAroundFreq(freq, x.Multiply(freq, x.Value(1.5)), x.Multiply(x.Value(0.02), StretchCurve(LineDownCurve, duration))),
                FMAroundFreq(freq, x.Multiply(freq, x.Value(2.0)), x.Multiply(x.Value(0.02), StretchCurve(LineDownCurve, duration)))
            );
            
            // Volume Curve
            outlet = x.Multiply(outlet, StretchCurve(DampedBlockCurve, duration));

            // Equalize Loudness
            var equalizedVolume = x.Multiply(volume, x.Value(0.6));

            // Apply Volume and Delay
            outlet = StrikeNote(outlet, delay, equalizedVolume);

            return outlet;
        }

        private Outlet Tuba(double freq = Frequencies.A1, double delay = 0, double volume = 1)
            => Tuba((Outlet)x.Value(freq), x.Value(delay), x.Value(volume));

        /// <summary>
        /// Sounds like Tuba at beginning.
        /// FM with mod speed below sound freq, changes sound freq to +/- 5Hz.
        /// Volume curve is applied.
        /// Higher notes are shorter, lower notes are much longer.
        /// </summary>
        private Outlet Tuba(Outlet freq = null, Outlet delay = null, Outlet volume = null)
        {
            freq = freq ?? x.Value(Frequencies.A1);

            // FM Algorithm
            var outlet = FMInHertz(x.Multiply(freq, x.Value(2)), freq, x.Value(5));

            // Stretch Volume Curve (longer when lower)
            const double durationOfA1 = 0.8;
            var freqOfA1 = x.Value(Frequencies.A1);
            var stretch = x.Multiply(x.Value(durationOfA1), x.Power(x.Divide(freqOfA1, freq), x.Value(1.5)));

            var curveOutlet = x.TimeMultiply(x.CurveIn(TubaCurve), stretch);

            // Apply Curve
            outlet = x.Multiply(outlet, curveOutlet);

            // Volume and Delay
            outlet = StrikeNote(outlet, delay, volume);

            return outlet;
        }

        private Outlet RippleBass(double freq = Frequencies.A1, double delay = 0, double volume = 1, double duration = 1)
            => RippleBass((Outlet)x.Value(freq), x.Value(delay), x.Value(volume), x.Value(duration));

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.005 </summary>
        private Outlet RippleBass(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? x.Value(Frequencies.A1);

            // FM algorithm
            Outlet outlet = FMAroundFreq(
                soundFreq: x.Multiply(freq, x.Value(8)), 
                modSpeed: x.Divide(freq, x.Value(2)), 
                modDepth: x.Value(0.005));

            // Curve
            outlet = x.Multiply(outlet, StretchCurve(RippleCurve, duration));

            // Volume and Delay
            outlet = StrikeNote(outlet, delay, volume);

            return outlet;
        }

        private Outlet RippleNote_SharpMetallic(double freq = Frequencies.A3)
            => RippleNote_SharpMetallic((Outlet)x.Value(freq));

        /// <summary> Mod speed below sound freq, changes sound freq ±10Hz </summary>
        private Outlet RippleNote_SharpMetallic(Outlet freq = null)
        {
            freq = freq ?? x.Value(Frequencies.A3);
            return FMInHertz(freq, x.Divide(freq, x.Value(2)), modDepth: x.Value(10));
        }

        private Outlet RippleSound_Clean(double freq = Frequencies.A4, double delay = 0, double volume = 1, double duration = 1)
            => RippleSound_Clean((Outlet)x.Value(freq), x.Value(delay), x.Value(volume), x.Value(duration));

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.005 </summary>
        private Outlet RippleSound_Clean(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? x.Value(Frequencies.A4);

            // FM algorithm
            Outlet outlet = FMAroundFreq(freq, x.Value(20), x.Value(0.005));

            // Curve
            outlet = x.Multiply(outlet, StretchCurve(RippleCurve, duration));

            // Volume and Delay
            outlet = StrikeNote(outlet, delay, volume);

            return outlet;
        }

        private Outlet RippleSound_FantasyEffect(double freq = Frequencies.A5)
            => RippleSound_FantasyEffect((Outlet)x.Value(freq));

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.02 </summary>
        private Outlet RippleSound_FantasyEffect(Outlet freq = null)
            => FMAroundFreq(freq ?? x.Value(Frequencies.A5), x.Value(10), x.Value(0.02));

        private Outlet RippleSound_CoolDouble(double freq = Frequencies.A5)
            => RippleSound_CoolDouble((Outlet)x.Value(freq));

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.05 </summary>
        private Outlet RippleSound_CoolDouble(Outlet freq = null)
            => FMAroundFreq(freq ?? x.Value(Frequencies.A5), x.Value(10), x.Value(0.05));

        private Outlet Create_FM_Noise_Beating(double freq = Frequencies.A4)
            => Create_FM_Noise_Beating((Outlet)x.Value(freq));

        /// <summary>
        /// Beating audible further along the sound.
        /// Mod speed much below sound freq, changes sound freq drastically * [0.5, 1.5]
        /// </summary>
        private Outlet Create_FM_Noise_Beating(Outlet pitch = null)
            => FMAroundFreq(pitch ?? x.Value(Frequencies.A4), x.Value(55), x.Value(0.5));

        // Algorithms

        /// <summary> FM sound synthesis modulating with addition. Modulates sound freq to +/- a number of Hz. </summary>
        /// <param name="modDepth">In Hz</param>
        private Outlet FMInHertz(Outlet soundFreq, Outlet modSpeed, Outlet modDepth)
        {
            Outlet modulator = x.Sine(modDepth, modSpeed);
            Outlet sound = x.Sine(x.Value(DEFAULT_AMPLITUDE), x.Add(soundFreq, modulator));
            return sound;
        }

        /// <summary> FM with (faulty) multiplication around 0. </summary>
        private Outlet FMAround0(Outlet soundFreq, Outlet modSpeed, Outlet modDepth)
        {
            Outlet modulator = x.Sine(modDepth, modSpeed);
            Outlet sound = x.Sine(x.Value(DEFAULT_AMPLITUDE), x.Multiply(soundFreq, modulator));
            return sound;
        }

        /// <summary> FM with multiplication around 1. </summary>
        private Outlet FMAroundFreq(Outlet soundFreq, Outlet modSpeed, Outlet modDepth)
        {
            Outlet modulator = x.Add(x.Value(1), x.Sine(modDepth, modSpeed));
            Outlet sound = x.Sine(x.Value(DEFAULT_AMPLITUDE), x.Multiply(soundFreq, modulator));
            return sound;
        }

        private Outlet StrikeNote(Outlet sound, Outlet delay = null, Outlet volume = null)
        {
            // Note Start
            if (delay != null) sound = x.TimeAdd(sound, delay);

            // Note Volume
            if (volume != null) sound = x.Multiply(sound, volume);

            return sound;
        }
        private Outlet StretchCurve(Curve curve, double duration = 1)
            => StretchCurve(curve, (Outlet)x.Value(duration));
        
        private Outlet StretchCurve(Curve curve, Outlet duration = null)
        {
            Outlet outlet = x.CurveIn(curve);

            if (duration?.Operator.AsValueOperator?.Value != 1)
            {
                outlet = x.TimeMultiply(outlet, duration);
            }

            return outlet;
        }

        private const double MILD_ECHO_TIME = 0.33 * 5;

        private Outlet MildEcho(Outlet outlet)
            => EntityFactory.CreateEcho(x, outlet, count: 6, denominator: 4, delay: 0.33);

        private const double DEEP_ECHO_TIME = 0.5 * 5;

        private Outlet DeepEcho(Outlet melody)
            => EntityFactory.CreateEcho(x, melody, count: 6, denominator: 2, delay: 0.5);

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

        private Curve _dampedBlockCurve;
        private Curve DampedBlockCurve
        {
            get
            {
                if (_dampedBlockCurve == null)
                {
                    _dampedBlockCurve = _curveFactory.CreateCurve
                    (
                        new NodeInfo(time: 0.00, value: 0),
                        new NodeInfo(time: 0.01, value: 1),
                        new NodeInfo(time: 0.99, value: 1),
                        new NodeInfo(time: 1.00, value: 0)
                    );
                }
                return _dampedBlockCurve;
            }
        }

        private Curve _lineDownCurve;
        private Curve LineDownCurve
        {
            get
            {
                if (_lineDownCurve == null)
                {
                    _lineDownCurve = _curveFactory.CreateCurve
                    (
                        new NodeInfo(time: 0, value: 1),
                        new NodeInfo(time: 1, value: 0)
                    );
                }
                return _lineDownCurve;
            }
        }

        private static readonly (double time, double frequency1, double frequency2, double frequency3)[] _padFrequencies = 
        {
            (0.0, Frequencies.A4, Frequencies.C5, Frequencies.E5),
            (1.0, Frequencies.A4, Frequencies.C5, Frequencies.F5),
            (2.0, Frequencies.G4, Frequencies.C5, Frequencies.E5),
            (3.0, Frequencies.G4, Frequencies.B4, Frequencies.D5),
            (4.0, Frequencies.F4, Frequencies.A4, Frequencies.D5),
            (5.0, Frequencies.A4, Frequencies.D5, Frequencies.F5),
            (6.0, Frequencies.A4, Frequencies.C5, Frequencies.E5),
            (7.0, Frequencies.A4, Frequencies.C5, Frequencies.E5),
        };

        private Curve _padPitchCurve1;
        private Curve PadPitchCurve1
        {
            get
            {
                if (_padPitchCurve1 == null)
                {
                    _padPitchCurve1 = _curveFactory.CreateCurve(
                        _padFrequencies.Select(x => new NodeInfo(x.time, x.frequency1, NodeTypeEnum.Block)).ToArray());
                }
                return _padPitchCurve1;
            }
        }

        private Curve _padPitchCurve2;
        private Curve PadPitchCurve2
        {
            get
            {
                if (_padPitchCurve2 == null)
                {
                    _padPitchCurve2 = _curveFactory.CreateCurve(
                        _padFrequencies.Select(x => new NodeInfo(x.time, x.frequency2, NodeTypeEnum.Block)).ToArray());
                }
                return _padPitchCurve2;
            }
        }

        private Curve _padPitchCurve3;
        private Curve PadPitchCurve3
        {
            get
            {
                if (_padPitchCurve3 == null)
                {
                    _padPitchCurve3 = _curveFactory.CreateCurve(
                        _padFrequencies.Select(x => new NodeInfo(x.time, x.frequency3, NodeTypeEnum.Block)).ToArray());
                }

                return _padPitchCurve3;
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
