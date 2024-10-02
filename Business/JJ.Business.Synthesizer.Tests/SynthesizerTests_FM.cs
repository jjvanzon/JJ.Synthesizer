using System;
using System.Linq;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Tests.Helpers;
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
        /// <summary> Constructor for test runner. </summary>
        public SynthesizerTests_FM()
        { }

        /// <summary> Constructor allowing each test to run in its own instance. </summary>
        public SynthesizerTests_FM(IContext context)
            : base(context, beat: 0.55, bar: 4 * 0.55)
        { }

        #region Tests

        // Composition Test

        [TestMethod]
        public void Test_Synthesizer_FM_Composition()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Composition();
        }

        private void Test_FM_Composition()
            => CreateAudioFile(DeepEcho(Composition), volume: 0.20, duration: t[bar: 8, beat: 1] + DEEP_ECHO_TIME);

        // Flute Tests

        [TestMethod]
        public void Test_Synthesizer_FM_Flute_Melody1()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Flute_Melody1();
        }

        private void Test_FM_Flute_Melody1()
            => CreateAudioFile(MildEcho(FluteMelody1(portato: 1)), volume: 0.6, duration: Bar[4] + MILD_ECHO_TIME);

        [TestMethod]
        public void Test_Synthesizer_FM_Flute_Melody2()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Flute_Melody2();
        }

        private void Test_FM_Flute_Melody2()
            => CreateAudioFile(MildEcho(FluteMelody2), volume: 0.3, duration: Bar[2.5] + MILD_ECHO_TIME);

        [TestMethod]
        public void Test_Synthesizer_FM_Flute1()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Flute1();
        }

        private void Test_FM_Flute1()
            => CreateAudioFile(MildEcho(Flute1()), duration: 1 + MILD_ECHO_TIME);

        [TestMethod]
        public void Test_Synthesizer_FM_Flute2()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Flute2();
        }

        private void Test_FM_Flute2()
            => CreateAudioFile(MildEcho(Flute2()), duration: 1 + MILD_ECHO_TIME);

        [TestMethod]
        public void Test_Synthesizer_FM_Flute3()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Flute3();
        }

        private void Test_FM_Flute3()
            => CreateAudioFile(MildEcho(Flute3()), duration: 1 + MILD_ECHO_TIME);

        [TestMethod]
        public void Test_Synthesizer_FM_Flute4()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Flute4();
        }

        private void Test_FM_Flute4()
            => CreateAudioFile(MildEcho(Flute4()), duration: 1 + MILD_ECHO_TIME);
        
        // Evolving Organ Test

        [TestMethod]
        public void Test_Synthesizer_FM_EvolvingOrgan()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_EvolvingOrgan();
        }

        private void Test_FM_EvolvingOrgan()
            => CreateAudioFile(MildEcho(EvolvingOrgan(duration: Bar[8])), duration: Bar[8] + MILD_ECHO_TIME);

        // Pad Tests

        [TestMethod]
        public void Test_Synthesizer_FM_Pad()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Pad();
        }

        private void Test_FM_Pad()
            => CreateAudioFile(MildEcho(Pad(duration: _[1.5])), duration: 1.5 + MILD_ECHO_TIME);
        
        [TestMethod]
        public void Test_Synthesizer_FM_Pad_Chords()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Pad_Chords();
        }

        private void Test_FM_Pad_Chords()
            => CreateAudioFile(MildEcho(PadChords), volume: 0.1, duration: Bar[8] + MILD_ECHO_TIME);

        // Tuba Tests

        [TestMethod]
        public void Test_Synthesizer_FM_Tuba()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Tuba();
        }

        private void Test_FM_Tuba()
            => CreateAudioFile(MildEcho(Tuba(_[Notes.E2])));

        [TestMethod]
        public void Test_Synthesizer_FM_Horn()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Horn();
        }

        private void Test_FM_Horn()
            => CreateAudioFile(MildEcho(Horn(duration: _[1])), duration: 1 + MILD_ECHO_TIME);

        [TestMethod]
        public void Test_Synthesizer_FM_Tuba_Melody1()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Tuba_Melody1();
        }

        private void Test_FM_Tuba_Melody1()
            => CreateAudioFile(MildEcho(TubaMelody1), volume: 0.45, duration: Bar[4] + MILD_ECHO_TIME);

        [TestMethod]
        public void Test_Synthesizer_FM_Tuba_Melody2()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Tuba_Melody2();
        }

        private void Test_FM_Tuba_Melody2()
            => CreateAudioFile(MildEcho(TubaMelody2), volume: 0.75, duration: Bar[3.5] + MILD_ECHO_TIME);

        [TestMethod]
        public void Test_Synthesizer_FM_Tuba_Melody3()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Tuba_Melody3();
        }

        private void Test_FM_Tuba_Melody3()
            => CreateAudioFile(MildEcho(TubaMelody3), duration: Bar[1.5] + MILD_ECHO_TIME);

        [TestMethod]
        public void Test_Synthesizer_FM_Horn_Melody1()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Horn_Melody1();
        }

        private void Test_FM_Horn_Melody1()
            => CreateAudioFile(MildEcho(HornMelody1), volume: 0.2, duration: Bar[4] + MILD_ECHO_TIME);

        [TestMethod]
        public void Test_Synthesizer_FM_Horn_Melody2()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Horn_Melody2();
        }

        private void Test_FM_Horn_Melody2()
            => CreateAudioFile(MildEcho(HornMelody2), volume: 0.2, duration: Bar[3.5] + MILD_ECHO_TIME);

        // Electric Note Tests

        [TestMethod]
        public void Test_Synthesizer_FM_ElectricNote()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_ElectricNote();
        }

        private void Test_FM_ElectricNote()
            => CreateAudioFile(MildEcho(ElectricNote(duration: _[1.5])), duration: 1.5 + MILD_ECHO_TIME);

        [TestMethod]
        public void Test_Synthesizer_FM_EvolvingOrgan_Chords()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_EvolvingOrgan_Chords();
        }

        private void Test_FM_EvolvingOrgan_Chords()
            => CreateAudioFile(MildEcho(EvolvingOrganChords), volume: 0.22, duration: Bar[8] + MILD_ECHO_TIME);

        // Ripple Effect Tests

        [TestMethod]
        public void Test_Synthesizer_FM_RippleBass()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_RippleBass();
        }

        private void Test_FM_RippleBass()
            => CreateAudioFile(DeepEcho(RippleBass(duration: _[3])), duration: 3 + DEEP_ECHO_TIME);

        [TestMethod]
        public void Test_Synthesizer_FM_RippleBass_Melody1()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_RippleBass_Melody1();
        }

        private void Test_FM_RippleBass_Melody1()
            => CreateAudioFile(DeepEcho(RippleBassMelody1), volume: 0.3, duration: Bar[5] + DEEP_ECHO_TIME);

        [TestMethod]
        public void Test_Synthesizer_FM_RippleBass_Melody2()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_RippleBass_Melody2();
        }

        private void Test_FM_RippleBass_Melody2()
            => CreateAudioFile(DeepEcho(RippleBassMelody2), volume: 0.3, duration: Bar[4] + DEEP_ECHO_TIME);

        [TestMethod]
        public void Test_Synthesizer_FM_RippleNote_SharpMetallic()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_RippleNote_SharpMetallic();
        }

        private void Test_FM_RippleNote_SharpMetallic()
            => CreateAudioFile(DeepEcho(RippleNote_SharpMetallic(duration: _[2.2])), duration: 2.2 + DEEP_ECHO_TIME);

        [TestMethod]
        public void Test_Synthesizer_FM_RippleSound_Clean()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_RippleSound_Clean();
        }

        private void Test_FM_RippleSound_Clean()
            => CreateAudioFile(outlet: DeepEcho(RippleSound_Clean(duration: _[3])), duration: 3 + DEEP_ECHO_TIME);

        [TestMethod]
        public void Test_Synthesizer_FM_RippleSound_FantasyEffect()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_RippleSound_FantasyEffect();
        }

        private void Test_FM_RippleSound_FantasyEffect()
            => CreateAudioFile(DeepEcho(RippleSound_FantasyEffect(duration: _[3])), duration: 3 + DEEP_ECHO_TIME);

        [TestMethod]
        public void Test_Synthesizer_FM_RippleSound_CoolDouble()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_RippleSound_CoolDouble();
        }

        private void Test_FM_RippleSound_CoolDouble()
            => CreateAudioFile(DeepEcho(RippleSound_CoolDouble(duration: _[3])), duration: 3 + DEEP_ECHO_TIME);

        // FM Noise Tests

        [TestMethod]
        public void Test_Synthesizer_FM_Noise_Beating()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_FM(context).Test_FM_Noise_Beating();
        }

        private void Test_FM_Noise_Beating()
            => CreateAudioFile(MildEcho(Create_FM_Noise_Beating(_[Notes.A4])), duration: 3);

        #endregion

        #region Composition

        private Outlet Composition
        {
            get
            {
                double fluteVolume = 1.1;
                double chordsVolume = 0.3;
                double tubaVolume = 0.7;
                double hornVolume = 0.7;
                double rippleBassVolume = 0.7;

                var pattern1 = Adder
                (
                    Multiply(_[fluteVolume], FluteMelody1(portato: 1.1)),
                    Multiply(_[chordsVolume], PadChords),
                    Multiply(_[tubaVolume], TubaMelody1),
                    Multiply(_[hornVolume], HornMelody1)
                    //Multiply(_[rippleBassVolume], RippleBassMelody1())
                );

                var pattern2 = Adder
                (
                    Multiply(_[fluteVolume], FluteMelody2),
                    Multiply(_[tubaVolume], TubaMelody2),
                    Multiply(_[hornVolume], HornMelody2)
                    //Multiply(_[rippleBassVolume], RippleBassMelody2)
                );

                var composition = Adder
                (
                    pattern1,
                    TimeAdd(pattern2, Bar[4])
                    //RippleSound_Clean(_[Frequencies.A4], delay: Bar(2), volume: _[0.50], duration: Bar(2))
                );

                return composition;
            }
        }

        #endregion

        #region Melodies

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

        private Outlet EvolvingOrganChords => Multiply
        (
            StretchCurve(ChordVolumeCurve, Bar[1]),
            Adder
            (
                EvolvingOrgan(StretchCurve(ChordPitchCurve1, Bar[1]), duration: Bar[8]),
                EvolvingOrgan(StretchCurve(ChordPitchCurve2, Bar[1]), duration: Bar[8]),
                EvolvingOrgan(StretchCurve(ChordPitchCurve3, Bar[1]), duration: Bar[8])
            )
        );

        private Outlet PadChords => Multiply
        (
            StretchCurve(ChordVolumeCurve, Bar[1]),
            Adder
            (
                Pad(StretchCurve(ChordPitchCurve1, Bar[1]), duration: Bar[8]),
                Pad(StretchCurve(ChordPitchCurve2, Bar[1]), duration: Bar[8]),
                Pad(StretchCurve(ChordPitchCurve3, Bar[1]), duration: Bar[8])
            )
        );
        
        private Outlet HornMelody1 => Adder
        (
            Horn(_[Notes.A2], Beat[00], duration: Beat[2]),
            Horn(_[Notes.E3], Beat[02]),
            Horn(_[Notes.F2], Beat[04], duration: Beat[3]),
            Horn(_[Notes.C3], Beat[06]),
            Horn(_[Notes.C2], Beat[08], duration: Beat[3]),
            Horn(_[Notes.G2], Beat[10]),
            Horn(_[Notes.G1], Beat[12], duration: Beat[4])//,
            //Horn(_[Notes.D3], Beat[14])
        );
        
        private Outlet HornMelody2 => Adder
        (
            Horn(_[Notes.A2], Beat[0], duration: Beat[3]),
            //Horn(_[Notes.E3], Beat[2]),
            Horn(_[Notes.F2], Beat[4], duration: Beat[3]),
            //Horn(_[Notes.C3], Beat[6]),
            Horn(_[Notes.A1], Beat[8], duration: Beat[5])
        );

        private Outlet TubaMelody1 => Adder
        (
            //Tuba(_[Notes.A2], Beat[00]),
            Tuba(_[Notes.E3], Beat[02]),
            //Tuba(_[Notes.F2], Beat[04]),
            Tuba(_[Notes.C3], Beat[06]),
            //Tuba(_[Notes.C2], Beat[08]),
            Tuba(_[Notes.G2], Beat[10]),
            //Tuba(_[Notes.G1], Beat[12]),
            Tuba(_[Notes.B3], Beat[14])
        );

        private Outlet TubaMelody2 => Adder
        (
            //Tuba(_[Notes.A2], Beat[0]),
            Tuba(_[Notes.E3], Beat[2]),
            //Tuba(_[Notes.F2], Beat[4]),
            Tuba(_[Notes.C3], Beat[6]),
            Tuba(_[Notes.A1], Beat[8])
        );

        private Outlet TubaMelody3 => Adder
        (
            Tuba(_[Notes.A1]),
            Tuba(_[Notes.E2], Beat[2]),
            Tuba(_[Notes.F1_Sharp], Beat[4], volume: _[0.7])
        );

        private Outlet RippleBassMelody1 =>
            DeepEcho(RippleBass(_[Notes.A2], delay: Bar[0], duration: Bar[2]));

        private Outlet RippleBassMelody2 =>
            DeepEcho(RippleBass(_[Notes.A1], delay: Bar[2.5], duration: Bar[1.5]));

        #endregion

        #region Instruments

        /// <summary> High hard flute: mod speed above sound freq, changes sound freq * [-0.005, 0.005] (erroneously) </summary>
        /// <inheritdoc cref="DefaultDoc" />
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
        /// <inheritdoc cref="DefaultDoc" />
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
        /// <inheritdoc cref="DefaultDoc" />
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
        /// <inheritdoc cref="DefaultDoc" />
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

        /// <inheritdoc cref="DefaultDoc" />
        private Outlet EvolvingOrgan(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[Notes.A4];
            duration = duration ?? _[1.0];

            var modDepth = Multiply(_[0.0001], StretchCurve(ModTamingCurve, Multiply(duration, _[1.1])));
            var fmSignal = FMAroundFreq(freq, Multiply(freq, _[2.0]), modDepth);
            var note = StrikeNote(fmSignal, delay, volume);

            return note;
        }
        
        /// <inheritdoc cref="DefaultDoc" />
        private Outlet Pad(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[Notes.A4];
            volume = volume ?? _[1];
            duration = duration ?? _[1];

            // Tame modulation
            var modCurveLength = Bar[8];
            Outlet modCurve = StretchCurve(ModTamingCurveRepeated8Times, modCurveLength);
            modCurve = Multiply(modCurve, StretchCurve(ModTamingCurve, modCurveLength));
            modCurve = Multiply(modCurve, StretchCurve(LineDownCurve, modCurveLength));

            var fmSignal = Add
            (
                FMAroundFreq(freq, Multiply(freq, _[2]), Multiply(_[0.00020], modCurve)),
                FMAroundFreq(freq, Multiply(freq, _[3]), Multiply(_[0.00015], modCurve))
            );

            //var envelope = StretchCurve(ChordVolumeCurve, duration);
            //var modulatedSound = Multiply(fmSignal, envelope);
            var adjustedVolume = Multiply(volume, _[0.6]);
            var note = StrikeNote(fmSignal, delay, adjustedVolume);

            return note;
        }

        /// <summary>
        /// Sounds like Tuba at beginning.
        /// FM with mod speed below sound freq, changes sound freq to +/- 5Hz.
        /// Volume curve is applied.
        /// Higher notes are shorter, lower notes are much longer.
        /// </summary>
        /// <param name="freq">The base frequency of the sound in Hz (default A1/55Hz).</param>
        /// <inheritdoc cref="DefaultDoc" />
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
                
        /// <summary>
        /// Sounds like Horn.
        /// FM with mod speed below sound freq, changes sound freq to +/- 5Hz.
        /// Volume curve is applied.
        /// FM modulator is attempted to be tamed with curves.
        /// </summary>
        /// <param name="freq">The base frequency of the sound in Hz (default A2/110Hz).</param>
        /// <inheritdoc cref="DefaultDoc" />
        private Outlet Horn(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[Notes.A2];

            var tamedMod = Multiply(_[5], StretchCurve(ModTamingCurve2, duration));

            var fmSignal = FMInHertz(Multiply(freq, _[2]), freq, tamedMod);
            var envelope = TimeMultiply(CurveIn(TubaCurve), duration);
            var sound = Multiply(fmSignal, envelope);
            var note = StrikeNote(sound, delay, volume);

            return note;
        }

        /// <inheritdoc cref="DefaultDoc" />
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

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.005 </summary>
        /// <param name="freq">The base frequency of the sound in Hz (default A1/55Hz).</param>
        /// <inheritdoc cref="DefaultDoc" />
        private Outlet RippleBass(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[Notes.A1];

            var fmSignal = FMAroundFreq(Multiply(freq, _[8]), Divide(freq, _[2]), _[0.005]);
            var note = ShapeRippleSound(fmSignal, delay, volume, duration);

            return note;
        }

        /// <summary> Mod speed below sound freq, changes sound freq ±10Hz </summary>
        /// <param name="freq">The base frequency of the sound in Hz (default A3/220Hz).</param>
        /// <inheritdoc cref="DefaultDoc" />
        private Outlet RippleNote_SharpMetallic(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[Notes.A3];
            var fmSignal = FMInHertz(freq, Divide(freq, _[2]), _[10]);
            var sound = ShapeRippleSound(fmSignal, delay, volume, duration);
            return sound;
        }

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.005 </summary>
        private Outlet RippleSound_Clean(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[Notes.A4];

            var fmSignal = FMAroundFreq(freq, _[20], _[0.005]);
            var sound = ShapeRippleSound(fmSignal, delay, volume, duration);

            return sound;
        }

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.02 </summary>
        /// <param name="duration">The duration of the sound in seconds (default is 2.5).</param>
        /// <inheritdoc cref="DefaultDoc" />
        private Outlet RippleSound_FantasyEffect(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[Notes.A5];

            var fmSignal = FMAroundFreq(freq, _[10], _[0.02]);
            var sound = ShapeRippleSound(fmSignal, delay, volume, duration);

            return sound;
        }

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.05 </summary>
        /// <inheritdoc cref="ShapeRippleSound" />
        private Outlet RippleSound_CoolDouble(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[Notes.A5];

            var fmSignal = FMAroundFreq(freq, _[10], _[0.05]);
            var sound = ShapeRippleSound(fmSignal, delay, volume, duration);

            return sound;
        }

        /// <summary> Shapes a ripple effect sound giving it a volume envelope and a delay, volume and duration. </summary>
        /// <param name="duration">The duration of the sound in seconds (default is 2.5).</param>
        /// <param name="fmSignal">A ripple sound to be shaped</param>
        /// <inheritdoc cref="DefaultDoc" />
        private Outlet ShapeRippleSound(Outlet fmSignal, Outlet delay, Outlet volume, Outlet duration)
        {
            duration = duration ?? _[2.5];
            var envelope = StretchCurve(RippleCurve, duration);
            var sound = Multiply(fmSignal, envelope);
            var strike = StrikeNote(sound, delay, volume);
            return strike;
        }

        /// <summary>
        /// Beating audible further along the sound.
        /// Mod speed much below sound freq, changes sound freq drastically * [0.5, 1.5]
        /// </summary>
        private Outlet Create_FM_Noise_Beating(Outlet pitch = null)
            => FMAroundFreq(pitch ?? _[Notes.A4], _[55], _[0.5]);

        #endregion

        #region Algorithms

        /// <summary> FM sound synthesis modulating with addition. Modulates sound freq to +/- a number of Hz. </summary>
        /// <param name="modDepth">In Hz</param>
        /// <inheritdoc cref="DefaultDoc" />
        private Outlet FMInHertz(Outlet soundFreq, Outlet modSpeed, Outlet modDepth)
        {
            var modulator = Sine(modDepth, modSpeed);
            var sound = Sine(_[1], Add(soundFreq, modulator));
            return sound;
        }

        /// <summary> FM with (faulty) multiplication around 0. </summary>
        /// <inheritdoc cref="DefaultDoc" />
        private Outlet FMAround0(Outlet soundFreq, Outlet modSpeed, Outlet modDepth)
        {
            var modulator = Sine(modDepth, modSpeed);
            var sound = Sine(_[1], Multiply(soundFreq, modulator));
            return sound;
        }

        /// <summary> FM with multiplication around 1. </summary>
        /// <inheritdoc cref="DefaultDoc" />
        private Outlet FMAroundFreq(Outlet soundFreq, Outlet modSpeed, Outlet modDepth)
        {
            var modulator = Add(_[1], Sine(modDepth, modSpeed));
            var sound = Sine(_[1], Multiply(soundFreq, modulator));
            return sound;
        }

        /// <inheritdoc cref="DefaultDoc" />
        private Outlet StrikeNote(Outlet sound, Outlet delay = null, Outlet volume = null)
        {
            if (delay != null) sound = TimeAdd(sound, delay);
            if (volume != null) sound = Multiply(sound, volume);
            return sound;
        }

        private Outlet StretchCurve(Curve curve, Outlet duration)
            => TimeMultiply(CurveIn(curve), duration);

        private const double MILD_ECHO_TIME = 0.33 * 5;

        private Outlet MildEcho(Outlet outlet)
            => EntityFactory.CreateEcho(this, outlet, count: 6, denominator: 4, delay: 0.33);

        private const double DEEP_ECHO_TIME = 0.5 * 5;

        private Outlet DeepEcho(Outlet melody)
            => EntityFactory.CreateEcho(this, melody, count: 6, denominator: 2, delay: 0.5);

        #endregion

        #region Curves

        private Curve FluteCurve => CurveFactory.CreateCurve
        (
            new NodeInfo(time: 0.00, value: 0.0),
            new NodeInfo(time: 0.05, value: 0.8),
            new NodeInfo(time: 0.10, value: 1.0),
            new NodeInfo(time: 0.90, value: 0.7),
            new NodeInfo(time: 1.00, value: 0.0)
        );

        private Curve TubaCurve => CurveFactory.CreateCurve
        (
            new NodeInfo(time: 0.00, value: 1),
            new NodeInfo(time: 0.93, value: 1),
            new NodeInfo(time: 1.00, value: 0)
        );

        private Curve RippleCurve => CurveFactory.CreateCurve
        (
            new NodeInfo(time: 0.00, value: 0.00),
            new NodeInfo(time: 0.01, value: 0.75),
            new NodeInfo(time: 0.05, value: 0.50),
            new NodeInfo(time: 0.25, value: 1.00),
            new NodeInfo(time: 1.00, value: 0.00)
        );

        private Curve DampedBlockCurve => CurveFactory.CreateCurve
        (
            new NodeInfo(time: 0.00, value: 0),
            new NodeInfo(time: 0.01, value: 1),
            new NodeInfo(time: 0.99, value: 1),
            new NodeInfo(time: 1.00, value: 0)
        );

        private Curve LineDownCurve => CurveFactory.CreateCurve
        (
            new NodeInfo(time: 0, value: 1),
            new NodeInfo(time: 1, value: 0)
        );

        /// <summary>
        /// A curve that can be applied to the modulator depth to tame the modulation.
        /// In this version of FM synthesis, the modulation depth accumulates over time without such taming.
        /// This is because of a lack of time tracking in the oscillators in this version.
        /// </summary>
        private Curve ModTamingCurve => CurveFactory.CreateCurve
        (
            timeSpan: 1,
            0.3, 1.0, 0.3, 0.0
        );

        /// <inheritdoc cref="ModTamingCurve"/>
        private Curve ModTamingCurve2 => CurveFactory.CreateCurve
        (
            timeSpan: 1,
            1.0, 0.5, 0.2, 0.0
        );

        /// <inheritdoc cref="ModTamingCurve"/>
        private Curve ModTamingCurveRepeated8Times => CurveFactory.CreateCurve
        (
            timeSpan: 1,
            0.3, 1.0, 0.3, 0.0,
            0.3, 1.0, 0.3, 0.0,
            0.3, 1.0, 0.3, 0.0,
            0.3, 1.0, 0.3, 0.0,
            0.3, 1.0, 0.3, 0.0,
            0.3, 1.0, 0.3, 0.0,
            0.3, 1.0, 0.3, 0.0,
            0.3, 1.0, 0.3, 0.0
        );

        private static readonly (double time, double frequency1, double frequency2, double frequency3)[]
            _chordFrequencies =
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

        private Curve ChordPitchCurve1 => CurveFactory.CreateCurve(
            _chordFrequencies.Select(x => new NodeInfo(x.time,
                                                     x.frequency1,
                                                     NodeTypeEnum.Block)).ToArray());

        private Curve ChordPitchCurve2 => CurveFactory.CreateCurve(
            _chordFrequencies.Select(x => new NodeInfo(x.time,
                                                     x.frequency2,
                                                     NodeTypeEnum.Block)).ToArray());

        private Curve ChordPitchCurve3 => CurveFactory.CreateCurve(
            _chordFrequencies.Select(x => new NodeInfo(x.time,
                                                     x.frequency3,
                                                     NodeTypeEnum.Block)).ToArray());

        private Curve ChordVolumeCurve => CurveFactory.CreateCurve(
            new NodeInfo(0.0, 0.0), new NodeInfo(0.1, 1.0), new NodeInfo(0.9, 1.0), 
            new NodeInfo(1.0, 0.0), new NodeInfo(1.1, 1.0), new NodeInfo(1.9, 1.0), 
            new NodeInfo(2.0, 0.0), new NodeInfo(2.1, 1.0), new NodeInfo(2.9, 1.0), 
            new NodeInfo(3.0, 0.0), new NodeInfo(3.1, 1.0), new NodeInfo(3.9, 1.0), 
            new NodeInfo(4.0, 0.0), new NodeInfo(4.1, 1.0), new NodeInfo(4.9, 1.0), 
            new NodeInfo(5.0, 0.0), new NodeInfo(5.1, 1.0), new NodeInfo(5.9, 1.0), 
            new NodeInfo(6.0, 0.0), new NodeInfo(6.1, 1.0), new NodeInfo(6.9, 1.0), 
            new NodeInfo(7.0, 0.0), new NodeInfo(7.1, 1.0), new NodeInfo(7.9, 1.0), 
            new NodeInfo(8.0, 0.0));

        #endregion

        #region Helpers

        /// <param name="freq">The base frequency of the sound in Hz (default is A4/440Hz).</param>
        /// <param name="delay">The time delay in seconds before the sound starts (default is 0).</param>
        /// <param name="volume">The volume of the sound (default is 1).</param>
        /// <param name="duration">The duration of the sound in seconds (default is 1).</param>
        /// <param name="soundFreq">The base frequency in Hz for the carrier signal for the FM synthesis.</param> 
        /// <param name="modSpeed">The speed of the modulator in Hz. Determines much of the timbre.</param>
        /// <param name="modDepth">The depth of the modulator. The higher the value, the more harmonic complexity.</param>
        /// <param name="sound">The sound to be shaped.</param>
        /// <returns>An Outlet representing the sound.</returns>
        [UsedImplicitly] private Outlet DefaultDoc(
            Outlet freq, Outlet delay, Outlet volume, Outlet duration,
            Outlet soundFreq, Outlet modSpeed, Outlet modDepth,
            Outlet sound) => throw new NotSupportedException();

        #endregion
    }
}