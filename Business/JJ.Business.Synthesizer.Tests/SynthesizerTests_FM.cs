using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.AudioFileOutputs;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Warnings;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable LocalizableElement

namespace JJ.Business.Synthesizer.Tests
{
    /// <summary>
    /// NOTE: Version 0.0.250 does not have time tracking in its oscillator,
    /// making the FM synthesis behave differently.
    /// </summary>
    [TestClass]
    public class SynthesizerTests_FM : SynthesizerSugarBase
    {
        private const double DEFAULT_TOTAL_TIME = 1.0 + DEEP_ECHO_TIME;
        private const double DEFAULT_TOTAL_VOLUME = 0.5;
        private const double DEFAULT_AMPLITUDE = 1.0;
        private const double BEAT = 0.4;
        private const double BAR = 1.6;

        /// <summary> Constructor for test runner. </summary>
        public SynthesizerTests_FM() { }

        /// <summary> Constructor allowing each test to run in its own instance. </summary>
        public SynthesizerTests_FM(IContext context)
            : base(context, BAR, BEAT)
        { }

        // Composition Test
        
        [TestMethod]
        public void Test_Synthesizer_FM_Composition()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Composition();
        }

        private void Test_FM_Composition() 
            => WrapUp_Test(MildEcho(Composition()), duration: Bar[8] + BEAT + DEEP_ECHO_TIME, volume: 0.20);

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
            => WrapUp_Test(MildEcho(FluteMelody2), BAR * 2.5 + MILD_ECHO_TIME, volume: 0.3);

        [TestMethod]
        public void Test_Synthesizer_FM_Flute1()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Flute1();
        }
        
        private void Test_FM_Flute1()
            => WrapUp_Test(MildEcho(Flute1(_[Notes.A4])));

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
            => WrapUp_Test(MildEcho(Flute3(_[Notes.A4])));

        [TestMethod]
        public void Test_Synthesizer_FM_Flute4()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Flute4();
        }

        private void Test_FM_Flute4()
            => WrapUp_Test(MildEcho(Flute4(_[Notes.A4])));

        // Pad Tests

        [TestMethod]
        public void Test_Synthesizer_FM_Pad()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Pad();
        }

        private void Test_FM_Pad()
            => WrapUp_Test(MildEcho(Pad(duration: _[1.5])), duration: 1.5 + MILD_ECHO_TIME);

        // Electric Note Tests

        [TestMethod]
        public void Test_Synthesizer_FM_ElectricNote()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_ElectricNote();
        }

        private void Test_FM_ElectricNote()
            => WrapUp_Test(MildEcho(ElectricNote(duration: _[1.5])), duration: 1.5 + MILD_ECHO_TIME);

        // Evolving Organ Test

        [TestMethod]
        public void Test_Synthesizer_FM_EvolvingOrgan()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_EvolvingOrgan();
        }

        private void Test_FM_EvolvingOrgan() =>
            WrapUp_Test(duration: BAR * 8 + MILD_ECHO_TIME,
                outlet: MildEcho(
                    EvolvingOrgan(_[Notes.A4], 
                        duration: _[BAR * 8 + MILD_ECHO_TIME]))
            );
        
        [TestMethod]
        public void Test_Synthesizer_FM_EvolvingOrgan_Chords()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_EvolvingOrgan_Chords();
        }

        private void Test_FM_EvolvingOrgan_Chords()
            => WrapUp_Test(MildEcho(EvolvingOrganChords), duration: BAR * 8 + MILD_ECHO_TIME, volume: 0.22);

        // Tube Tests

        [TestMethod]
        public void Test_Synthesizer_FM_Tuba()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Tuba();
        }

        private void Test_FM_Tuba() 
            => WrapUp_Test(MildEcho(Tuba(_[Notes.E2])));

        [TestMethod]
        public void Test_Synthesizer_FM_Tuba_Melody1()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Tuba_Melody1();
        }

        private void Test_FM_Tuba_Melody1()
            => WrapUp_Test(MildEcho(TubaMelody1), duration: BAR * 4 + MILD_ECHO_TIME, volume: 0.45);

        [TestMethod]
        public void Test_Synthesizer_FM_Tuba_Melody2()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Tuba_Melody2();
        }

        private void Test_FM_Tuba_Melody2()
            => WrapUp_Test(MildEcho(TubaMelody2), duration:  BAR * 2.5 + MILD_ECHO_TIME, volume: 0.75);

        [TestMethod]
        public void Test_Synthesizer_FM_Tuba_Melody3()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Tuba_Melody3();
        }

        private void Test_FM_Tuba_Melody3() 
            => WrapUp_Test(MildEcho(TubaMelody3), duration: BAR * 1.5 + MILD_ECHO_TIME);

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
            => WrapUp_Test(DeepEcho(RippleBassMelody1), duration: BAR * 5 + DEEP_ECHO_TIME, volume: 0.3);
        
        [TestMethod]
        public void Test_Synthesizer_FM_RippleBass_Melody2()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_RippleBass_Melody2();
        }

        private void Test_FM_RippleBass_Melody2() 
            => WrapUp_Test(DeepEcho(RippleBassMelody2), duration: BAR * 4.0 + DEEP_ECHO_TIME, volume: 0.3);

        [TestMethod]
        public void Test_Synthesizer_FM_RippleNote_SharpMetallic()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_RippleNote_SharpMetallic();
        }

        private void Test_FM_RippleNote_SharpMetallic()
            => WrapUp_Test
            (
                DeepEcho
                (
                    RippleNote_SharpMetallic(_[Notes.E3], duration: _[2.2])
                ),
                duration: 2.2 + DEEP_ECHO_TIME  
            );

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
            => WrapUp_Test(MildEcho(RippleSound_FantasyEffect(_[Notes.A5])), duration: 3);

        [TestMethod]
        public void Test_Synthesizer_FM_RippleSound_CoolDouble()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_RippleSound_CoolDouble();
        }

        private void Test_FM_RippleSound_CoolDouble()
            => WrapUp_Test(MildEcho(RippleSound_CoolDouble(_[Notes.A5])), duration: 3);

        // FM Noise Tests

        [TestMethod]
        public void Test_Synthesizer_FM_Noise_Beating()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Noise_Beating();
        }

        private void Test_FM_Noise_Beating()
            => WrapUp_Test(MildEcho(Create_FM_Noise_Beating(_[Notes.A4])), duration: 3);

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
                Multiply(_[padVolume]  , EvolvingOrganChords),
                Multiply(_[tubaVolume] , TubaMelody1)
                //Multiply(_[rippleBassVolume], RippleBassMelody1())
            );

            var pattern2 = Adder
            (
                Multiply(_[fluteVolume]     , FluteMelody2),
                Multiply(_[tubaVolume]      , TubaMelody2),
                Multiply(_[rippleBassVolume], RippleBassMelody2)
            );

            var composition = Adder
            (
                pattern1,
                TimeAdd(pattern2, Bar[4])
                //RippleSound_Clean(_[Frequencies.A4], delay: Bar(2), volume: _[0.50], duration: Bar(2))
            );

            return composition;
        }

        // Melodies

        private Outlet FluteMelody1(double portato = 1.3636)
        {
            return Adder
            (
                Flute1(_[Notes.E4], t[bar: 0, beat: 0.0], volume: _[0.80], duration: _[Beat[2.00] * portato]),
                Flute2(_[Notes.F4], t[bar: 0, beat: 1.5], volume: _[0.70], duration: _[Beat[2.17] * portato]),
                Flute1(_[Notes.G4], t[bar: 0, beat: 3.0], volume: _[0.60], duration: _[Beat[1.00] * portato]),
                Flute1(_[Notes.A4], t[bar: 1, beat: 0.0], volume: _[0.80], duration: _[Beat[2.33] * portato]),
                Flute3(_[Notes.B4], t[bar: 1, beat: 1.5], volume: _[0.50], duration: _[Beat[1.00] * portato]),
                Flute1(_[Notes.A4], t[bar: 1, beat: 3.0], volume: _[0.55], duration: _[Beat[1.67] * portato]),
                Flute2(_[Notes.C4], t[bar: 2, beat: 0.0], volume: _[1.00], duration: _[Beat[2.00] * portato]),
                Flute1(_[Notes.G4], t[bar: 2, beat: 1.5], volume: _[0.80], duration: _[Beat[2.50] * portato])
            );
        }

        private Outlet FluteMelody2 => Adder
        (
            Flute1(_[Notes.E4], t[bar: 0, beat: 0.0], volume: _[1.00]),
            Flute2(_[Notes.F4], t[bar: 0, beat: 1.5], volume: _[1.15]),
            Flute2(_[Notes.F4], t[bar: 0, beat: 1.5], volume: _[1.15]),
            Flute3(_[Notes.G4], t[bar: 0, beat: 3.0], volume: _[1.25]),
            Flute4(_[Notes.A4], t[bar: 1, beat: 0.0], volume: _[1.40]),
            Flute3(_[Notes.B4], t[bar: 1, beat: 1.5], volume: _[1.25]),
            Flute2(_[Notes.G4], t[bar: 1, beat: 3.0], volume: _[1.15]),
            Flute4(_[Notes.A4], t[bar: 2, beat: 0.0], volume: _[1.70], duration: _[1.66])
        );

        private Outlet EvolvingOrganChords =>
            /*
            Adder
            (
                Sine(_[DEFAULT_AMPLITUDE], StretchCurve(PadPitchCurve1, BAR)),
                Sine(_[DEFAULT_AMPLITUDE], StretchCurve(PadPitchCurve2, BAR)),
                Sine(_[DEFAULT_AMPLITUDE], StretchCurve(PadPitchCurve3, BAR))
            );
            */
            /*
            Adder
            (
                Pad(StretchCurve(PadPitchCurve1, BAR), duration: BAR * 8),
                Pad(StretchCurve(PadPitchCurve2, BAR), duration: BAR * 8),
                Pad(StretchCurve(PadPitchCurve3, BAR), duration: BAR * 8)
            );
            */
            Adder
            (
                EvolvingOrgan(StretchCurve(PadPitchCurve1, Bar[1]), duration: Bar[8]),
                EvolvingOrgan(StretchCurve(PadPitchCurve2, Bar[1]), duration: Bar[8]),
                EvolvingOrgan(StretchCurve(PadPitchCurve3, Bar[1]), duration: Bar[8])
            );

        private Outlet TubaMelody1 => Adder
        (
            Tuba(_[Notes.A2], Beat[00]),
            Tuba(_[Notes.E3], Beat[02]),
            Tuba(_[Notes.F2], Beat[04]),
            Tuba(_[Notes.C3], Beat[06]),
            Tuba(_[Notes.C2], Beat[08]),
            Tuba(_[Notes.G2], Beat[10]),
            Tuba(_[Notes.G1], Beat[12]),
            Tuba(_[Notes.D3], Beat[14])
        );

        private Outlet TubaMelody2 => Adder
        (
            Tuba(_[Notes.A2], Beat[0]),
            Tuba(_[Notes.E3], Beat[2]),
            Tuba(_[Notes.F2], Beat[4]),
            Tuba(_[Notes.C3], Beat[6]),
            Tuba(_[Notes.A1], Beat[8])
        );
        
        private Outlet TubaMelody3 => Adder
        (
            Tuba(_[Notes.A1]),
            Tuba(_[Notes.E2],       Beat[2]),
            Tuba(_[Notes.F1_Sharp], Beat[4], volume: _[0.7])
        );

        private Outlet RippleBassMelody1 =>
            DeepEcho(RippleBass(_[Notes.A2], delay: Bar[0], duration: Bar[2]));

        private Outlet RippleBassMelody2 =>
            DeepEcho(RippleBass(_[Notes.A1], delay: Bar[2.5], duration: Bar[1.5]));

        // Instruments

        /// <summary> High hard flute: mod speed above sound freq, changes sound freq * [-0.005, 0.005] (erroneously) </summary>
        private Outlet Flute1(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[Notes.A4];

            var fmSignal = FMAround0(Divide(freq, _[2]), freq, _[0.005]);
            var envelope = StretchCurve(FluteCurve, duration);
            var modulatedSound = Multiply(fmSignal, envelope);
            var note = StrikeNote(modulatedSound, delay, volume);
    
            return note;
        }

        /// <summary> Yet another flute: mod speed above sound freq, changes sound freq * 1 +/- 0.005 </summary>
        private Outlet Flute2(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[Notes.A4];
            volume = volume ?? _[1];

            var fmSignal = FMAroundFreq(freq, Multiply(freq, _[2]), _[0.005]);
            var envelope = StretchCurve(FluteCurve, duration);
            var modulatedSound = Multiply(fmSignal, envelope);
            var adjustedVolume = Multiply(volume, _[0.85]);
            var note = StrikeNote(modulatedSound, delay, adjustedVolume);

            return note;
        }

        /// <summary> Yet another flute: mod speed above sound freq, changes sound freq * 1 +/- 0.005 </summary>
        private Outlet Flute3(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[Notes.A4];
            volume = volume ?? _[1];

            var fmSignal = FMAroundFreq(freq, Multiply(freq, _[4]), _[0.005]);
            var envelope = StretchCurve(FluteCurve, duration);
            var sound = Multiply(fmSignal, envelope);
            var adjustedVolume = Multiply(volume, _[0.8]);
            var note = StrikeNote(sound, delay, adjustedVolume);

            return note;
        }

        /// <summary> Modulated hard flute: mod speed below sound freq, changes sound freq * [-0.005, 0.005] (erroneously) </summary>
        private Outlet Flute4(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[Notes.A4];
            volume = volume ?? _[1];

            var fmSignal = FMAround0(Multiply(freq, _[2]), freq, _[0.005]);
            var envelope = StretchCurve(FluteCurve, duration);
            var sound = Multiply(fmSignal, envelope);
            var adjustedVolume = Multiply(volume, _[0.70]);
            var note = StrikeNote(sound, delay, adjustedVolume);

            return note;
        }

        private Outlet Pad(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[Notes.A4];
            volume = volume ?? _[1];
            duration = duration ?? _[1];

            var modCurve = StretchCurve(LineDownCurve, Multiply(duration, _[1.1]));
            var fmSignal = Add
            (
                FMAroundFreq(freq, Multiply(freq, _[2]), Multiply(_[0.004], modCurve)),
                FMAroundFreq(freq, Multiply(freq, _[3]), Multiply(_[0.003], modCurve))
            );

            var envelope = StretchCurve(DampedBlockCurve, duration);
            var modulatedSound = Multiply(fmSignal, envelope);
            var adjustedVolume = Multiply(volume, _[0.6]);
            var note = StrikeNote(modulatedSound, delay, adjustedVolume);

            return note;
        }

        private Outlet ElectricNote(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[Notes.A4];
            volume = volume ?? _[1];

            var modDepth = Multiply(_[0.02], StretchCurve(LineDownCurve, duration));
            var fmSignal = Add
            (
                FMAroundFreq(freq, Multiply(freq, _[1.5]), modDepth),
                FMAroundFreq(freq, Multiply(freq, _[2.0]), modDepth)
            );
            
            var envelope = StretchCurve(DampedBlockCurve, duration);
            var modulatedSound = Multiply(fmSignal, envelope);
            var adjustedVolume = Multiply(volume, _[0.6]);
            var note = StrikeNote(modulatedSound, delay, adjustedVolume);

            return note;
        }
        
        private Outlet EvolvingOrgan(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[Notes.A4];
            duration = duration ?? _[1.0];

            var modCurve = StretchCurve(LineDownCurve, Multiply(duration, _[1.1]));
            var modDepth = Multiply(modCurve, _[0.00005]);
            var fmSignal = FMAroundFreq(freq, Multiply(freq, _[2.0]), modDepth);
            var note = StrikeNote(fmSignal, delay, volume);
            
            return note;
        }

        /// <summary>
        /// Sounds like Tuba at beginning.
        /// FM with mod speed below sound freq, changes sound freq to +/- 5Hz.
        /// Volume curve is applied.
        /// Higher notes are shorter, lower notes are much longer.
        /// </summary>
        private Outlet Tuba(Outlet freq = null, Outlet delay = null, Outlet volume = null)
        {
            freq = freq ?? _[Notes.A1];

            var fmSignal = FMInHertz(Multiply(freq, _[2]), freq, _[5]);

            // Exaggerate Duration when Lower
            var durationOfA1 = 0.8;
            var baseFrequency = _[Notes.A1];
            var frequencyRatio = Divide(baseFrequency, freq);
            var duration = Multiply(_[durationOfA1], Power(frequencyRatio, _[1.5]));

            var envelope = TimeMultiply(CurveIn(TubaCurve), duration);
            var sound = Multiply(fmSignal, envelope);
            var note = StrikeNote(sound, delay, volume);

            return note;
        }

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.005 </summary>
        private Outlet RippleBass(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[Notes.A1];

            var fmSignal = FMAroundFreq(Multiply(freq, _[8]), Divide(freq, _[2]), _[0.005]);
            var envelope = StretchCurve(RippleCurve, duration);
            var sound = Multiply(fmSignal, envelope);
            var note = StrikeNote(sound, delay, volume);
            
            return note;
        }

        /// <summary> Mod speed below sound freq, changes sound freq ±10Hz </summary>
        private Outlet RippleNote_SharpMetallic(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[Notes.A3];
            var fmSignal = FMInHertz(freq, Divide(freq, _[2]), modDepth: _[10]);
            var envelope = StretchCurve(RippleCurve, duration);
            var sound = Multiply(fmSignal, envelope);
            var note = StrikeNote(sound, delay, volume);

            return note;
        }

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.005 </summary>
        private Outlet RippleSound_Clean(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[Notes.A4];

            var fmSignal = FMAroundFreq(freq, _[20], _[0.005]);
            var envelope = StretchCurve(RippleCurve, duration);
            var sound = Multiply(fmSignal, envelope);
            var note = StrikeNote(sound, delay, volume);

            return note;
        }

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.02 </summary>
        private Outlet RippleSound_FantasyEffect(Outlet freq = null)
            => FMAroundFreq(freq ?? _[Notes.A5], _[10], _[0.02]);

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.05 </summary>
        private Outlet RippleSound_CoolDouble(Outlet freq = null)
            => FMAroundFreq(freq ?? _[Notes.A5], _[10], _[0.05]);

        /// <summary>
        /// Beating audible further along the sound.
        /// Mod speed much below sound freq, changes sound freq drastically * [0.5, 1.5]
        /// </summary>
        private Outlet Create_FM_Noise_Beating(Outlet pitch = null)
            => FMAroundFreq(pitch ?? _[Notes.A4], _[55], _[0.5]);

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
                    _fluteCurve = CurveFactory.CreateCurve
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
                    _tubaCurve = CurveFactory.CreateCurve
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
                    _rippleCurve = CurveFactory.CreateCurve
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
                    _dampedBlockCurve = CurveFactory.CreateCurve
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
                    _lineDownCurve = CurveFactory.CreateCurve
                    (
                        new NodeInfo(time: 0, value: 1),
                        new NodeInfo(time: 1, value: 0)
                    );
                }
                return _lineDownCurve;
            }
        }

        private static readonly (double time, double frequency1, double frequency2, double frequency3)[] 
            _padFrequencies = 
        {
            (0.0, Notes.A4, Notes.C5, Notes.E5),
            (1.0, Notes.A4, Notes.C5, Notes.F5),
            (2.0, Notes.G4, Notes.C5, Notes.E5),
            (3.0, Notes.G4, Notes.B4, Notes.D5),
            (4.0, Notes.F4, Notes.A4, Notes.D5),
            (5.0, Notes.A4, Notes.D5, Notes.F5),
            (6.0, Notes.A4, Notes.C5, Notes.E5),
            (7.0, Notes.A4, Notes.C5, Notes.E5),
        };

        private Curve _padPitchCurve1;
        private Curve PadPitchCurve1
        {
            get
            {
                if (_padPitchCurve1 == null)
                {
                    _padPitchCurve1 = CurveFactory.CreateCurve(
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
                    _padPitchCurve2 = CurveFactory.CreateCurve(
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
                    _padPitchCurve3 = CurveFactory.CreateCurve(
                        _padFrequencies.Select(x => new NodeInfo(x.time, x.frequency3, NodeTypeEnum.Block)).ToArray());
                }

                return _padPitchCurve3;
            }
        }

        // Steps

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

        private AudioFileOutput ConfigureAudioFileOutput(string fileName, Outlet outlet, double totalTime, double volume)
        {
            AudioFileOutput audioFileOutput = AudioFileOutputManager.CreateAudioFileOutput();
            audioFileOutput.Duration = totalTime;
            audioFileOutput.Amplifier = short.MaxValue * volume;
            audioFileOutput.FilePath = fileName;
            audioFileOutput.AudioFileOutputChannels[0].Outlet = outlet;
            return audioFileOutput;
        }

        private void AssertEntities(AudioFileOutput audioFileOutput, Outlet outlet)
        {
            AudioFileOutputManager.ValidateAudioFileOutput(audioFileOutput).Verify();
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
