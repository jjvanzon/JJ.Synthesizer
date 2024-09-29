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
// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable
// ReSharper disable once InconsistentNaming
// ReSharper disable ParameterHidesMember

namespace JJ.Business.Synthesizer.Tests
{
    /// <summary>
    /// NOTE: Version 0.0.250 does not have time tracking in its oscillator,
    /// making the FM synthesis behave differently.
    /// </summary>
    [TestClass]
    public class SynthesizerTests_FM : SynthesizerFacade
    {
        private const double DEFAULT_TOTAL_TIME = 1.0 + DEEP_ECHO_TIME;
        private const double DEFAULT_TOTAL_VOLUME = 0.5;
        private const double DEFAULT_AMPLITUDE = 1.0;
        private const double BEAT = 0.4;
        private const double BAR = 1.6;

        private readonly CurveFactory _curveFactory;
        private readonly AudioFileOutputManager _audioFileOutputManager;

        #region Shorthand for syntactic sugar

        /// <summary>
        /// Shorthand for OperatorFactor.Value(123), x.Value(123) or Value(123). Allows using _[123] instead.
        /// Literal numbers need to be wrapped inside a Value Operator so they can always be substituted by
        /// a whole formula / graph / calculation / curve over time.
        /// </summary>
        private ValueIndexer _ { get; }

        private Outlet t(double bar, double beat) => Value(bar * BAR + beat * BEAT);
        private Outlet t(double beat) => Value(beat * BEAT);

        #endregion

        /// <summary> Constructor for test runner. </summary>
        public SynthesizerTests_FM() { }

        /// <summary> Constructor allowing each test to run in its own instance. </summary>
        public SynthesizerTests_FM(IContext context)
            : base(context)
        {
            _curveFactory = TestHelper.CreateCurveFactory(context);
            _audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(context);

            // Shorthand for syntactic sugar.
            _ = new ValueIndexer(this);
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

        private void Test_FM_LowModulation() =>
            WrapUp_Test(duration: BAR * 8 + MILD_ECHO_TIME,
                outlet: MildEcho(
                    LowModulation(_[Frequencies.A4], 
                        duration: _[BAR * 8 + MILD_ECHO_TIME]))
            );

        // Flute Tests

        [TestMethod]
        public void Test_Synthesizer_FM_Flute_Melody1()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Flute_Melody1();
        }
        
        private void Test_FM_Flute_Melody1() 
            => WrapUp_Test(MildEcho(FluteMelody1(portato: 1)), duration: BAR * 4 + MILD_ECHO_TIME, volume: 0.6);

        [TestMethod]
        public void Test_Synthesizer_FM_Flute_Melody2()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Flute_Melody2();
        }

        private void Test_FM_Flute_Melody2() 
            => WrapUp_Test(MildEcho(FluteMelody2()), BAR * 2.5 + MILD_ECHO_TIME, volume: 0.3);

        [TestMethod]
        public void Test_Synthesizer_FM_Flute1()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Flute1();
        }

        private void Test_FM_Flute1()
            => WrapUp_Test(MildEcho(Flute1(_[Frequencies.A4])));

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
            => WrapUp_Test(MildEcho(Flute3(_[Frequencies.A4])));

        [TestMethod]
        public void Test_Synthesizer_FM_Flute4()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Flute4();
        }

        private void Test_FM_Flute4()
            => WrapUp_Test(MildEcho(Flute4(_[Frequencies.A4])));

        // Pad Tests

        [TestMethod]
        public void Test_Synthesizer_FM_Pad()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Pad();
        }

        private void Test_FM_Pad()
            => WrapUp_Test(MildEcho(Pad(duration: _[1.5])), duration: 1.5 + MILD_ECHO_TIME);

        [TestMethod]
        public void Test_Synthesizer_FM_Pad_Chords()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Pad_Chords();
        }

        private void Test_FM_Pad_Chords()
            => WrapUp_Test(MildEcho(PadChords()), duration: BAR * 8 + MILD_ECHO_TIME, volume: 0.22);

        // Electric Note Tests

        [TestMethod]
        public void Test_Synthesizer_FM_ElectricNote()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_ElectricNote();
        }

        private void Test_FM_ElectricNote()
            => WrapUp_Test(MildEcho(ElectricNote(duration: _[1.5])), duration: 1.5 + MILD_ECHO_TIME);

        // Tube Tests

        [TestMethod]
        public void Test_Synthesizer_FM_Tuba()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Tuba();
        }

        private void Test_FM_Tuba() 
            => WrapUp_Test(MildEcho(Tuba(_[Frequencies.E2])));

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
            => WrapUp_Test(DeepEcho(RippleBass(duration: _[3])), duration: 3.0 + DEEP_ECHO_TIME);

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
            => WrapUp_Test(MildEcho(RippleNote_SharpMetallic(_[Frequencies.A3])));

        [TestMethod]
        public void Test_Synthesizer_FM_RippleSound_Clean()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_RippleSound_Clean();
        }

        private void Test_FM_RippleSound_Clean()
            => WrapUp_Test(DeepEcho(RippleSound_Clean(duration: _[3])), duration: 3.0 + DEEP_ECHO_TIME);

        [TestMethod]
        public void Test_Synthesizer_FM_RippleSound_FantasyEffect()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_RippleSound_FantasyEffect();
        }

        private void Test_FM_RippleSound_FantasyEffect()
            => WrapUp_Test(MildEcho(RippleSound_FantasyEffect(_[Frequencies.A5])), duration: 3);

        [TestMethod]
        public void Test_Synthesizer_FM_RippleSound_CoolDouble()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_RippleSound_CoolDouble();
        }

        private void Test_FM_RippleSound_CoolDouble()
            => WrapUp_Test(MildEcho(RippleSound_CoolDouble(_[Frequencies.A5])), duration: 3);

        // FM Noise Tests

        [TestMethod]
        public void Test_Synthesizer_FM_Noise_Beating()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Noise_Beating();
        }

        private void Test_FM_Noise_Beating()
            => WrapUp_Test(MildEcho(Create_FM_Noise_Beating(_[Frequencies.A4])), duration: 3);

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

            var pattern1 = Adder
            (
                Multiply(_[fluteVolume], FluteMelody1(portato: 1.1)),
                Multiply(_[padVolume]  , PadChords()),
                Multiply(_[tubaVolume] , TubaMelody1())
                //Multiply(_[rippleBassVolume], RippleBassMelody1())
            );

            var pattern2 = Adder
            (
                Multiply(_[fluteVolume]     , FluteMelody2()),
                Multiply(_[tubaVolume]      , TubaMelody2()),
                Multiply(_[rippleBassVolume], RippleBassMelody2())
            );

            var composition = Adder
            (
                pattern1,
                TimeAdd(pattern2, _[BAR * 4])
                //RippleSound_Clean(Frequencies.A4, delay: BAR * 2, volume: 0.50, duration: BAR * 2),
            );

            return composition;
        }

        // Melodies

        private Outlet FluteMelody1(double portato = 1.3636)
        {
            return Adder
            (
                Flute1(_[Frequencies.E4], t(bar: 0, beat: 0.0), volume: _[0.80], duration: _[2.00 * BEAT * portato]),
                Flute2(_[Frequencies.F4], t(bar: 0, beat: 1.5), volume: _[0.70], duration: _[2.17 * BEAT * portato]),
                Flute1(_[Frequencies.G4], t(bar: 0, beat: 3.0), volume: _[0.60], duration: _[1.00 * BEAT * portato]),
                Flute1(_[Frequencies.A4], t(bar: 1, beat: 0.0), volume: _[0.80], duration: _[2.33 * BEAT * portato]),
                Flute3(_[Frequencies.B4], t(bar: 1, beat: 1.5), volume: _[0.50], duration: _[1.00 * BEAT * portato]),
                Flute1(_[Frequencies.A4], t(bar: 1, beat: 3.0), volume: _[0.55], duration: _[1.67 * BEAT * portato]),
                Flute2(_[Frequencies.C4], t(bar: 2, beat: 0.0), volume: _[1.00], duration: _[2.00 * BEAT * portato]),
                Flute1(_[Frequencies.G4], t(bar: 2, beat: 1.5), volume: _[0.80], duration: _[2.50 * BEAT * portato])
            );
        }

        private Outlet FluteMelody2() => Adder
        (
            Flute1(_[Frequencies.E4], t(bar: 0, beat: 0.0), volume: _[1.00]),
            Flute2(_[Frequencies.F4], t(bar: 0, beat: 1.5), volume: _[1.15]),
            Flute2(_[Frequencies.F4], t(bar: 0, beat: 1.5), volume: _[1.15]),
            Flute3(_[Frequencies.G4], t(bar: 0, beat: 3.0), volume: _[1.25]),
            Flute4(_[Frequencies.A4], t(bar: 1, beat: 0.0), volume: _[1.40]),
            Flute3(_[Frequencies.B4], t(bar: 1, beat: 1.5), volume: _[1.25]),
            Flute2(_[Frequencies.G4], t(bar: 1, beat: 3.0), volume: _[1.15]),
            Flute4(_[Frequencies.A4], t(bar: 2, beat: 0.0), volume: _[1.70], duration: _[1.66])
        );
        
        private Outlet PadChords()
        {
            /*
            return Adder
            (
                Sine(_[DEFAULT_AMPLITUDE], StretchCurve(PadPitchCurve1, BAR)),
                Sine(_[DEFAULT_AMPLITUDE], StretchCurve(PadPitchCurve2, BAR)),
                Sine(_[DEFAULT_AMPLITUDE], StretchCurve(PadPitchCurve3, BAR))
            );
            */

            /*
            return Adder
            (
                Pad(StretchCurve(PadPitchCurve1, BAR), duration: BAR * 8),
                Pad(StretchCurve(PadPitchCurve2, BAR), duration: BAR * 8),
                Pad(StretchCurve(PadPitchCurve3, BAR), duration: BAR * 8)
            );
            */

            return Adder
            (
                LowModulation(StretchCurve(PadPitchCurve1, _[BAR]), duration: _[BAR * 8]),
                LowModulation(StretchCurve(PadPitchCurve2, _[BAR]), duration: _[BAR * 8]),
                LowModulation(StretchCurve(PadPitchCurve3, _[BAR]), duration: _[BAR * 8])
            );
        }

        private Outlet TubaMelody1() => Adder
        (
            Tuba(_[Frequencies.A2], _[BEAT * 00]),
            Tuba(_[Frequencies.E3], _[BEAT * 02]),
            Tuba(_[Frequencies.F2], _[BEAT * 04]),
            Tuba(_[Frequencies.C3], _[BEAT * 06]),
            Tuba(_[Frequencies.C2], _[BEAT * 08]),
            Tuba(_[Frequencies.G2], _[BEAT * 10]),
            Tuba(_[Frequencies.G1], _[BEAT * 12]),
            Tuba(_[Frequencies.D3], _[BEAT * 14])
        );

        private Outlet TubaMelody2() => Adder
        (
            Tuba(_[Frequencies.A2], _[BEAT * 0]),
            Tuba(_[Frequencies.E3], _[BEAT * 2]),
            Tuba(_[Frequencies.F2], _[BEAT * 4]),
            Tuba(_[Frequencies.C3], _[BEAT * 6]),
            Tuba(_[Frequencies.A1], _[BEAT * 8])
        );
        
        private Outlet TubaMelody3() => Adder
        (
            Tuba(_[Frequencies.A1]),
            Tuba(_[Frequencies.E2],       _[BEAT * 2]),
            Tuba(_[Frequencies.F1_Sharp], _[BEAT * 4], volume: _[0.7])
        );

        private Outlet RippleBassMelody1() =>
            DeepEcho(RippleBass(_[Frequencies.A2], delay: _[BAR * 0], duration: _[BAR * 2]));

        private Outlet RippleBassMelody2() =>
            DeepEcho(RippleBass(_[Frequencies.A1], delay: _[BAR * 2.5], duration: _[BAR * 1.5]));

        // Instruments

        private Outlet LowModulation(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[Frequencies.A4];

            // FM Algorithm
            Outlet curveDown = StretchCurve(LineDownCurve, Multiply(duration, _[1.1]));
            Outlet outlet = FMAroundFreq(
                soundFreq: freq, 
                modSpeed: Multiply(freq, _[2.0]), 
                modDepth: Multiply(curveDown, _[0.00005]));

            // Volume and Delay
            outlet = StrikeNote(outlet, delay, volume);
            
            return outlet;
        }

        /// <summary> High hard flute: mod speed above sound freq, changes sound freq * [-0.005, 0.005] (erroneously) </summary>
        private Outlet Flute1(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[Frequencies.A4];

            // FM Algorithm
            Outlet outlet = FMAround0(Divide(freq, _[2]), freq, _[0.005]);

            // Curve
            outlet = Multiply(outlet, StretchCurve(FluteCurve, duration));

            // Volume and Delay
            outlet = StrikeNote(outlet, delay, volume);
            
            return outlet;
        }

        /// <summary> Yet another flute: mod speed above sound freq, changes sound freq * 1 +/- 0.005 </summary>
        private Outlet Flute2(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[Frequencies.A4];
            volume = volume ?? _[1];

            // FM Algorithm
            Outlet outlet = FMAroundFreq(freq, Multiply(freq, _[2]), _[0.005]);
            
            // Curve
            outlet = Multiply(outlet, StretchCurve(FluteCurve, duration));

            // Equalize Volume
            Outlet equalizedVolume = Multiply(volume, _[0.85]);

            // Volume and Delay
            outlet = StrikeNote(outlet, delay, equalizedVolume);

            return outlet;
        }

        /// <summary> Yet another flute: mod speed above sound freq, changes sound freq * 1 +/- 0.005 </summary>
        private Outlet Flute3(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[Frequencies.A4];
            volume = volume ?? _[1];

            // FM Algorithm
            Outlet outlet = FMAroundFreq(freq, Multiply(freq, _[4]), _[0.005]);

            // Curve
            outlet = Multiply(outlet, StretchCurve(FluteCurve, duration));

            // Equalize Volume
            var equalizedVolume = Multiply(volume, _[0.8]);

            // Volume and Delay
            outlet = StrikeNote(outlet, delay, equalizedVolume);

            return outlet;
        }

        /// <summary> Modulated hard flute: mod speed below sound freq, changes sound freq * [-0.005, 0.005] (erroneously) </summary>
        private Outlet Flute4(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[Frequencies.A4];
            volume = volume ?? _[1];

            // FM Algorithm
            Outlet outlet = FMAround0(Multiply(freq, _[2]), freq, _[0.005]);
            
            // Volume Curve
            outlet = Multiply(outlet, StretchCurve(FluteCurve, duration));

            // Equalize Volume
            var equalizedVolume = Multiply(volume, _[0.70]);

            // Apply Volume and Delay
            outlet = StrikeNote(outlet, delay, equalizedVolume);

            return outlet;
        }

        private Outlet Pad(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[Frequencies.A4];
            volume = volume ?? _[1];
            duration = duration ?? _[1];

            // FM Algorithm
            Outlet curveDown = StretchCurve(LineDownCurve, Multiply(duration, _[1.1]));

            Outlet outlet = Add
            (
                FMAroundFreq(freq, Multiply(freq, _[2]), Multiply(_[0.004], curveDown)),
                FMAroundFreq(freq, Multiply(freq, _[3]), Multiply(_[0.003], curveDown))
            );

            // Volume Curve
            outlet = Multiply(outlet, StretchCurve(DampedBlockCurve, duration));

            // Equalize Loudness
            var equalizedVolume = Multiply(volume, _[0.6]);

            // Apply Volume and Delay
            outlet = StrikeNote(outlet, delay, equalizedVolume);

            return outlet;
        }

        private Outlet ElectricNote(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[Frequencies.A4];
            volume = volume ?? _[1];

            // FM Algorithm
            Outlet outlet = Add
            (
                FMAroundFreq(freq, Multiply(freq, _[1.5]), Multiply(_[0.02], StretchCurve(LineDownCurve, duration))),
                FMAroundFreq(freq, Multiply(freq, _[2.0]), Multiply(_[0.02], StretchCurve(LineDownCurve, duration)))
            );
            
            // Volume Curve
            outlet = Multiply(outlet, StretchCurve(DampedBlockCurve, duration));

            // Equalize Loudness
            var equalizedVolume = Multiply(volume, _[0.6]);

            // Apply Volume and Delay
            outlet = StrikeNote(outlet, delay, equalizedVolume);

            return outlet;
        }

        /// <summary>
        /// Sounds like Tuba at beginning.
        /// FM with mod speed below sound freq, changes sound freq to +/- 5Hz.
        /// Volume curve is applied.
        /// Higher notes are shorter, lower notes are much longer.
        /// </summary>
        private Outlet Tuba(Outlet freq = null, Outlet delay = null, Outlet volume = null)
        {
            freq = freq ?? _[Frequencies.A1];

            // FM Algorithm
            var outlet = FMInHertz(Multiply(freq, _[2]), freq, _[5]);

            // Stretch Volume Curve (longer when lower)
            const double durationOfA1 = 0.8;
            var freqOfA1 = _[Frequencies.A1];
            var stretch = Multiply(_[durationOfA1], Power(Divide(freqOfA1, freq), _[1.5]));

            var curveOutlet = TimeMultiply(CurveIn(TubaCurve), stretch);

            // Apply Curve
            outlet = Multiply(outlet, curveOutlet);

            // Volume and Delay
            outlet = StrikeNote(outlet, delay, volume);

            return outlet;
        }

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.005 </summary>
        private Outlet RippleBass(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[Frequencies.A1];

            // FM algorithm
            Outlet outlet = FMAroundFreq(
                soundFreq: Multiply(freq, _[8]), 
                modSpeed: Divide(freq, _[2]), 
                modDepth: _[0.005]);

            // Curve
            outlet = Multiply(outlet, StretchCurve(RippleCurve, duration));

            // Volume and Delay
            outlet = StrikeNote(outlet, delay, volume);

            return outlet;
        }

        /// <summary> Mod speed below sound freq, changes sound freq ±10Hz </summary>
        private Outlet RippleNote_SharpMetallic(Outlet freq = null)
        {
            freq = freq ?? _[Frequencies.A3];
            return FMInHertz(freq, Divide(freq, _[2]), modDepth: _[10]);
        }

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.005 </summary>
        private Outlet RippleSound_Clean(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[Frequencies.A4];

            // FM algorithm
            Outlet outlet = FMAroundFreq(freq, _[20], _[0.005]);

            // Curve
            outlet = Multiply(outlet, StretchCurve(RippleCurve, duration));

            // Volume and Delay
            outlet = StrikeNote(outlet, delay, volume);

            return outlet;
        }

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.02 </summary>
        private Outlet RippleSound_FantasyEffect(Outlet freq = null)
            => FMAroundFreq(freq ?? _[Frequencies.A5], _[10], _[0.02]);

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.05 </summary>
        private Outlet RippleSound_CoolDouble(Outlet freq = null)
            => FMAroundFreq(freq ?? _[Frequencies.A5], _[10], _[0.05]);

        /// <summary>
        /// Beating audible further along the sound.
        /// Mod speed much below sound freq, changes sound freq drastically * [0.5, 1.5]
        /// </summary>
        private Outlet Create_FM_Noise_Beating(Outlet pitch = null)
            => FMAroundFreq(pitch ?? _[Frequencies.A4], _[55], _[0.5]);

        // Algorithms

        /// <summary> FM sound synthesis modulating with addition. Modulates sound freq to +/- a number of Hz. </summary>
        /// <param name="modDepth">In Hz</param>
        private Outlet FMInHertz(Outlet soundFreq, Outlet modSpeed, Outlet modDepth)
        {
            Outlet modulator = Sine(modDepth, modSpeed);
            Outlet sound = Sine(_[DEFAULT_AMPLITUDE], Add(soundFreq, modulator));
            return sound;
        }

        /// <summary> FM with (faulty) multiplication around 0. </summary>
        private Outlet FMAround0(Outlet soundFreq, Outlet modSpeed, Outlet modDepth)
        {
            Outlet modulator = Sine(modDepth, modSpeed);
            Outlet sound = Sine(_[DEFAULT_AMPLITUDE], Multiply(soundFreq, modulator));
            return sound;
        }

        /// <summary> FM with multiplication around 1. </summary>
        private Outlet FMAroundFreq(Outlet soundFreq, Outlet modSpeed, Outlet modDepth)
        {
            Outlet modulator = Add(_[1], Sine(modDepth, modSpeed));
            Outlet sound = Sine(_[DEFAULT_AMPLITUDE], Multiply(soundFreq, modulator));
            return sound;
        }

        private Outlet StrikeNote(Outlet sound, Outlet delay = null, Outlet volume = null)
        {
            // Note Start
            if (delay != null) sound = TimeAdd(sound, delay);

            // Note Volume
            if (volume != null) sound = Multiply(sound, volume);

            return sound;
        }
        
        private Outlet StretchCurve(Curve curve, Outlet duration = null)
        {
            Outlet outlet = CurveIn(curve);

            if (duration?.Operator.AsValueOperator?.Value != 1)
            {
                outlet = TimeMultiply(outlet, duration);
            }

            return outlet;
        }

        private const double MILD_ECHO_TIME = 0.33 * 5;

        private Outlet MildEcho(Outlet outlet)
            => EntityFactory.CreateEcho(this, outlet, count: 6, denominator: 4, delay: 0.33);

        private const double DEEP_ECHO_TIME = 0.5 * 5;

        private Outlet DeepEcho(Outlet melody)
            => EntityFactory.CreateEcho(this, melody, count: 6, denominator: 2, delay: 0.5);

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
