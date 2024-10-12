using System.Linq;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Tests.Wishes;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Tests.Wishes.Notes;

// ReSharper disable LocalizableElement

namespace JJ.Business.Synthesizer.Tests
{
    /// <summary>
    /// NOTE: Version 0.0.250 does not have time tracking in its oscillator,
    /// making the FM synthesis behave differently.
    /// </summary>
    [TestClass]
    public class FMTests : SynthSugarBase
    {
        /// <summary> Constructor for test runner. </summary>
        [UsedImplicitly]
        public FMTests()
        { }

        /// <summary> Constructor allowing each test to run in its own instance. </summary>
        public FMTests(IContext context)
            : base(context, beat: 0.45, bar: 4 * 0.45)
        { }

        #region Tests

        [TestMethod]
        [Ignore]
        public void FM_Jingle()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new FMTests(context).FM_Jingle_RunTest();
        }

        private void FM_Jingle_RunTest()
            => SaveWav(DeepEcho(Jingle()), volume: 0.18, duration: t[bar: 9, beat: 2] + DEEP_ECHO_TIME);

        [TestMethod]
        public void FM_Flute_Melody1()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new FMTests(context).FM_Flute_Melody1_RunTest();
        }

        private void FM_Flute_Melody1_RunTest()
            => SaveWav(MildEcho(FluteMelody1), volume: 0.6, duration: bars[4] + MILD_ECHO_TIME);

        [TestMethod]
        public void FM_Flute_Melody2()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new FMTests(context).FM_Flute_Melody2_RunTest();
        }

        private void FM_Flute_Melody2_RunTest()
            => SaveWav(MildEcho(FluteMelody2), volume: 0.3, duration: bars[2.5] + MILD_ECHO_TIME);

        [TestMethod]
        public void FM_Flute1()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new FMTests(context).FM_Flute1_RunTest();
        }

        private void FM_Flute1_RunTest()
            => SaveWav(MildEcho(Flute1()), duration: 1 + MILD_ECHO_TIME);

        [TestMethod]
        public void FM_Flute2()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new FMTests(context).FM_Flute2_RunTest();
        }

        private void FM_Flute2_RunTest()
            => SaveWav(MildEcho(Flute2()), duration: 1 + MILD_ECHO_TIME);

        [TestMethod]
        public void FM_Flute3()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new FMTests(context).FM_Flute3_RunTest();
        }

        private void FM_Flute3_RunTest()
            => SaveWav(MildEcho(Flute3()), duration: 1 + MILD_ECHO_TIME);

        [TestMethod]
        public void FM_Flute4()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new FMTests(context).FM_Flute4_RunTest();
        }

        private void FM_Flute4_RunTest()
            => SaveWav(MildEcho(Flute4()), duration: 1 + MILD_ECHO_TIME);

        [TestMethod]
        public void FM_Organ()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new FMTests(context).FM_Organ_RunTest();
        }

        private void FM_Organ_RunTest()
            => SaveWav(MildEcho(Organ(duration: bars[8])), duration: bars[8] + MILD_ECHO_TIME);

        [TestMethod]
        public void FM_Pad()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new FMTests(context).FM_Pad_RunTest();
        }

        private void FM_Pad_RunTest()
            => SaveWav(MildEcho(Pad()), duration: bars[8] + MILD_ECHO_TIME, volume: 0.2);

        [TestMethod]
        public void FM_Pad_Chords()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new FMTests(context).FM_Pad_Chords_RunTest();
        }

        private void FM_Pad_Chords_RunTest()
            => SaveWav(MildEcho(PadChords), volume: 0.15, duration: bars[8] + MILD_ECHO_TIME);

        [TestMethod]
        public void FM_Trombone()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new FMTests(context).FM_Trombone_RunTest();
        }

        private void FM_Trombone_RunTest()
            => SaveWav(MildEcho(Trombone(_[E2])));

        [TestMethod]
        public void FM_Horn()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new FMTests(context).FM_Horn_RunTest();
        }

        private void FM_Horn_RunTest()
            => SaveWav(MildEcho(Horn(duration: _[1])), duration: 1 + MILD_ECHO_TIME);

        [TestMethod]
        public void FM_Trombone_Melody1()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new FMTests(context).FM_Trombone_Melody1_RunTest();
        }

        private void FM_Trombone_Melody1_RunTest()
            => SaveWav(MildEcho(TromboneMelody1), volume: 0.45, duration: bars[4] + MILD_ECHO_TIME);

        [TestMethod]
        public void FM_Trombone_Melody2()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new FMTests(context).FM_Trombone_Melody2_RunTest();
        }

        private void FM_Trombone_Melody2_RunTest()
            => SaveWav(MildEcho(TromboneMelody2), volume: 0.75, duration: bars[3.5] + MILD_ECHO_TIME);

        [TestMethod]
        public void FM_Trombone_Melody3()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new FMTests(context).FM_Trombone_Melody3_RunTest();
        }

        private void FM_Trombone_Melody3_RunTest()
            => SaveWav(MildEcho(TromboneMelody3), duration: bars[1.5] + MILD_ECHO_TIME);

        [TestMethod]
        public void FM_Horn_Melody1()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new FMTests(context).FM_Horn_Melody1_RunTest();
        }

        private void FM_Horn_Melody1_RunTest()
            => SaveWav(MildEcho(HornMelody1), volume: 0.6, duration: bars[4] + MILD_ECHO_TIME);

        [TestMethod]
        public void FM_Horn_Melody2()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new FMTests(context).FM_Horn_Melody2_RunTest();
        }

        private void FM_Horn_Melody2_RunTest()
            => SaveWav(MildEcho(HornMelody2), volume: 0.6, duration: bars[3.5] + MILD_ECHO_TIME);

        [TestMethod]
        public void FM_ElectricNote()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new FMTests(context).FM_ElectricNote_RunTest();
        }

        private void FM_ElectricNote_RunTest()
            => SaveWav(MildEcho(ElectricNote(duration: _[1.5])), duration: 1.5 + MILD_ECHO_TIME);

        [TestMethod]
        public void FM_Organ_Chords()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new FMTests(context).FM_Organ_Chords_RunTest();
        }

        private void FM_Organ_Chords_RunTest()
            => SaveWav(MildEcho(OrganChords), volume: 0.22, duration: bars[8] + MILD_ECHO_TIME);

        [TestMethod]
        public void FM_RippleBass()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new FMTests(context).FM_RippleBass_RunTest();
        }

        private void FM_RippleBass_RunTest()
            => SaveWav(DeepEcho(RippleBass(duration: _[3])), duration: 3 + DEEP_ECHO_TIME);

        [TestMethod]
        public void FM_RippleBass_Melody1()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new FMTests(context).FM_RippleBass_Melody1_RunTest();
        }

        private void FM_RippleBass_Melody1_RunTest()
            => SaveWav(DeepEcho(RippleBassMelody1), volume: 0.3, duration: bars[5] + DEEP_ECHO_TIME);

        [TestMethod]
        public void FM_RippleBass_Melody2()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new FMTests(context).FM_RippleBass_Melody2_RunTest();
        }

        private void FM_RippleBass_Melody2_RunTest()
            => SaveWav(DeepEcho(RippleBassMelody2), volume: 0.3, duration: bars[4] + DEEP_ECHO_TIME);

        [TestMethod]
        public void FM_RippleNote_SharpMetallic()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new FMTests(context).FM_RippleNote_SharpMetallic_RunTest();
        }

        private void FM_RippleNote_SharpMetallic_RunTest()
            => SaveWav(DeepEcho(RippleNote_SharpMetallic(duration: _[2.2])), duration: 2.2 + DEEP_ECHO_TIME);

        [TestMethod]
        public void FM_RippleSound_Clean()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new FMTests(context).FM_RippleSound_Clean_RunTest();
        }

        private void FM_RippleSound_Clean_RunTest()
            => SaveWav(outlet: DeepEcho(RippleSound_Clean(duration: _[4])), duration: 4 + DEEP_ECHO_TIME);

        [TestMethod]
        public void FM_RippleSound_FantasyEffect()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new FMTests(context).FM_RippleSound_FantasyEffect_RunTest();
        }

        private void FM_RippleSound_FantasyEffect_RunTest()
            => SaveWav(DeepEcho(RippleSound_FantasyEffect(duration: _[4])), duration: 4 + DEEP_ECHO_TIME);

        [TestMethod]
        public void FM_RippleSound_CoolDouble()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new FMTests(context).FM_RippleSound_CoolDouble_RunTest();
        }

        private void FM_RippleSound_CoolDouble_RunTest()
            => SaveWav(DeepEcho(RippleSound_CoolDouble(duration: _[3])), duration: 3 + DEEP_ECHO_TIME);

        [TestMethod]
        public void FM_Noise_Beating()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new FMTests(context).FM_Noise_Beating_RunTest();
        }

        private void FM_Noise_Beating_RunTest()
            => SaveWav(MildEcho(Create_FM_Noise_Beating(_[A4])), duration: 5);

        #endregion

        #region Jingle

        private Outlet Jingle()
        {
            double fluteVolume = 1.2;
            double chordsVolume = 0.5;
            double tromboneVolume = 0.7;
            double hornVolume = 0.6;
            double rippleBassVolume = 0.7;

            var pattern1 = Adder
            (
                Multiply(_[fluteVolume]     , FluteMelody1),
                Multiply(_[chordsVolume]    , PadChords),
                Multiply(_[tromboneVolume]  , TromboneMelody1),
                Multiply(_[hornVolume]      , HornMelody1),
                Multiply(_[rippleBassVolume], RippleBassMelody1)
            );

            var pattern2 = Adder
            (
                Multiply(_[fluteVolume]     , FluteMelody2),
                Multiply(_[tromboneVolume]  , TromboneMelody2),
                Multiply(_[hornVolume]      , HornMelody2),
                Multiply(_[rippleBassVolume], RippleBassMelody2)
            );

            var composition = Adder
            (
                pattern1,
                TimeAdd(pattern2, bar[5])
                //RippleSound_Clean(_[Frequencies.A4], delay: Bar[3], volume: _[0.50], duration: Bar(2))
            );

            return composition;
        }

        #endregion

        #region Melodies

        private Outlet FluteMelody1 => Adder
        (
            Flute1(_[E4], t[bar: 1, beat: 1.0], volume: _[0.80], beats[2.00]),
            Flute1(_[F4], t[bar: 1, beat: 2.5], volume: _[0.70], beats[2.17]),
            Flute1(_[G4], t[bar: 1, beat: 4.0], volume: _[0.60], beats[1.00]),
            Flute1(_[A4], t[bar: 2, beat: 1.0], volume: _[0.80], beats[2.33]),
            Flute2(_[B4], t[bar: 2, beat: 2.5], volume: _[0.50], beats[1.00]),
            Flute2(_[A3], t[bar: 2, beat: 4.0], volume: _[0.50], beats[1.67]), // Flute1 would appear delayed because of destructive interference?
            Flute3(_[G3], t[bar: 3, beat: 1.0], volume: _[0.85], beats[2.00]),
            Flute1(_[G4], t[bar: 3, beat: 2.5], volume: _[0.80], beats[2.50])
        );

        private Outlet FluteMelody2 => Adder
        (
            Flute1(_[E4], t[bar: 1, beat: 1.0], volume: _[0.59], beats[1.8]),
            Flute2(_[F4], t[bar: 1, beat: 2.5], volume: _[0.68], beats[1.0]),
            Flute1(_[G4], t[bar: 1, beat: 4.0], volume: _[0.74], beats[0.6]),
            Flute2(_[A4], t[bar: 2, beat: 1.0], volume: _[0.82], beats[2.0]),
            Flute3(_[B4], t[bar: 2, beat: 2.5], volume: _[0.74], beats[1.0]),
            Flute2(_[G4], t[bar: 2, beat: 4.0], volume: _[0.90], beats[0.4]),
            Flute4(_[A4], t[bar: 3, beat: 1.0], volume: _[1.00], _[1.66])
        );

        Outlet OrganChords =>
            Multiply
            (
                Stretch(ChordVolumeCurve, bars[1]),
                Adder
                (
                    Organ(Stretch(ChordPitchCurve1, bars[1]), duration: bars[8]),
                    Organ(Stretch(ChordPitchCurve2, bars[1]), duration: bars[8]),
                    Organ(Stretch(ChordPitchCurve3, bars[1]), duration: bars[8])
                )
            );

        private Outlet PadChords => Multiply
        (
            Stretch(ChordVolumeCurve, bars[1]),
            Adder
            (
                Pad(Stretch(ChordPitchCurve1, bars[1]), duration: bars[8]),
                Pad(Stretch(ChordPitchCurve2, bars[1]), duration: bars[8]),
                Pad(Stretch(ChordPitchCurve3, bars[1]), duration: bars[8])
            )
        );

        private Outlet HornMelody1 => Adder
        (
            //Horn(_[A2], Beat[01], duration: Beats[2]),
            //Horn(_[E3], Beat[02]),
            //Horn(_[F2], Beat[05], duration: Beats[3]),
            //Horn(_[C3], Beat[07]),
            Horn(_[C2], beat[09], duration: beats[3], volume: _[0.7]),
            //Horn(_[G2], Beat[11]),
            Horn(_[G1], beat[13], duration: beats[4], volume: _[0.5]) //,
            //Horn(_[D3], Beat[15])
        );

        private Outlet HornMelody2 => Adder
        (
            Horn(_[A2], beat[1], duration: beat[3], volume: _[0.75]),
            //Horn(_[E3], Beat[3]),
            Horn(_[F2], beat[5], duration: beat[3], volume: _[0.85]),
            //Horn(_[C3], Beat[7]),
            Horn(_[A1], beat[9], duration: beat[5], volume: _[1.0])
        );

        private Outlet TromboneMelody1 => Adder
        (
            //Trombone(_[A3], Beat[00]),
            //Trombone(_[E4], Beat[02]),
            //Trombone(_[F3], Beat[04]),
            //Trombone(_[C4], Beat[06]),
            //Trombone(_[C3], Beat[08]),
            //Trombone(_[G3], Beat[10]),
            //Trombone(_[G2], Beat[12]),
            //Trombone(_[B3], Beat[14])
        );

        private Outlet TromboneMelody2 => Adder
        (
            //Trombone(_[A2], Beat[1]),
            Trombone(_[E4], beat[3], durationFactor: _[1.4]),
            //Trombone(_[F2], Beat[5]),
            Trombone(_[C4], beat[7], durationFactor: _[1.4]) //,
            //Trombone(_[A3], Beat[9])
        );

        private Outlet TromboneMelody3 => Adder
        (
            Trombone(_[A1]      , beat[1]),
            Trombone(_[E2]      , beat[3]),
            Trombone(_[F1_Sharp], beat[5], volume: _[0.7])
        );

        private Outlet RippleBassMelody1 => _[0];
        //RippleBass(_[A2], delay: Bar[1], duration: Bars[2]);

        private Outlet RippleBassMelody2 =>
            RippleBass(_[A1], delay: bar[3.5], duration: bars[0.8]);

        #endregion

        #region Instruments

        /// <summary> High hard flute: mod speed above sound freq, changes sound freq * [-0.005, 0.005] (erroneously) </summary>
        /// <inheritdoc cref="docs._default" />
        private Outlet Flute1(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[A4];

            var fmSignal = FMAround0(Divide(freq, _[2]), freq, _[0.005]);
            var envelope = Stretch(FluteCurve, duration);
            var modulatedSound = Multiply(fmSignal, envelope);
            var note = StrikeNote(modulatedSound, delay, volume);

            return note;
        }

        /// <summary> Yet another flute: mod speed above sound freq, changes sound freq * 1 +/- 0.005 </summary>
        /// <inheritdoc cref="docs._default" />
        private Outlet Flute2(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[A4];
            volume = volume ?? _[1];

            var fmSignal = FMAroundFreq(freq, Multiply(freq, _[2]), _[0.005]);
            var envelope = Stretch(FluteCurve, duration);
            var modulatedSound = Multiply(fmSignal, envelope);
            var adjustedVolume = Multiply(volume, _[0.85]);
            var note = StrikeNote(modulatedSound, delay, adjustedVolume);

            return note;
        }

        /// <summary> Yet another flute: mod speed above sound freq, changes sound freq * 1 +/- 0.005 </summary>
        /// <inheritdoc cref="docs._default" />
        private Outlet Flute3(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[A4];
            volume = volume ?? _[1];

            var fmSignal = FMAroundFreq(freq, Multiply(freq, _[4]), _[0.005]);
            var envelope = Stretch(FluteCurve, duration);
            var sound = Multiply(fmSignal, envelope);
            var adjustedVolume = Multiply(volume, _[0.8]);
            var note = StrikeNote(sound, delay, adjustedVolume);

            return note;
        }

        /// <summary> Modulated hard flute: mod speed below sound freq, changes sound freq * [-0.005, 0.005] (erroneously) </summary>
        /// <inheritdoc cref="docs._default" />
        private Outlet Flute4(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[A4];
            volume = volume ?? _[1];

            var fmSignal = FMAround0(Multiply(freq, _[2]), freq, _[0.005]);
            var envelope = Stretch(FluteCurve, duration);
            var sound = Multiply(fmSignal, envelope);
            var adjustedVolume = Multiply(volume, _[0.70]);
            var note = StrikeNote(sound, delay, adjustedVolume);

            return note;
        }

        /// <inheritdoc cref="docs._default" />
        private Outlet Organ(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[A4];
            duration = duration ?? _[1];

            var modCurve = Stretch(ModTamingCurve, duration);
            var modDepth = Multiply(_[0.0001], modCurve);
            var fmSignal = FMAroundFreq(freq, Multiply(freq, _[2.0]), modDepth);

            var volumeEvenOutCurve = Stretch(EvenOutCurve, duration);
            var soundWithEvenVolume = Multiply(fmSignal, volumeEvenOutCurve);

            var note = StrikeNote(soundWithEvenVolume, delay, volume);

            return note;
        }

        /// <inheritdoc cref="docs._default" />
        private Outlet Pad(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[A4];
            duration = duration ?? beats[1];

            // Tame modulation
            var modCurve = Stretch(ModTamingCurve8Times, duration);
            modCurve = Multiply(modCurve, Stretch(ModTamingCurve, duration));
            modCurve = Multiply(modCurve, Stretch(LineDownCurve, duration));

            var fmSignal = Add
            (
                FMAroundFreq(freq, Multiply(freq, _[2]), Multiply(_[0.00020], modCurve)),
                FMAroundFreq(freq, Multiply(freq, _[3]), Multiply(_[0.00015], modCurve))
            );

            var volumeEvenOutCurve = Stretch(EvenOutCurve, duration);
            var soundWithEvenVolume = Multiply(fmSignal, volumeEvenOutCurve);

            var note = StrikeNote(soundWithEvenVolume, delay, volume);

            return note;
        }

        /// <summary>
        /// Sounds like Trombone at beginning.
        /// FM with mod speed below sound freq, changes sound freq to +/- 5Hz.
        /// Volume curve is applied.
        /// Higher notes are shorter, lower notes are much longer.
        /// </summary>
        /// <param name="freq"> The base frequency of the sound in Hz (default A1/55Hz). </param>
        /// <param name="durationFactor"> Duration varies with pitch, but can be multiplied by this factor (default is 1). </param>
        /// <inheritdoc cref="docs._default" />
        private Outlet Trombone(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet durationFactor = null)
        {
            freq = freq ?? _[A1];
            durationFactor = durationFactor ?? _[1];

            var fmSignal = FMInHertz(Multiply(freq, _[2]), freq, _[5]);

            // Exaggerate Duration when Lower
            var baseNote = _[A1];
            var baseNoteDuration = Multiply(_[0.8], durationFactor);
            var ratio = Divide(baseNote, freq);
            var transformedDuration = Multiply(baseNoteDuration, Power(ratio, _[1.5]));

            var envelope = Stretch(BrassCurve, transformedDuration);
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
        /// <param name="freq"> The base frequency of the sound in Hz (default A2/110Hz). </param>
        /// <inheritdoc cref="docs._default" />
        private Outlet Horn(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[A2];

            var tamedMod = Multiply(_[5], Stretch(ModTamingCurve2, duration));

            var fmSignal = FMInHertz(Multiply(freq, _[2]), freq, tamedMod);
            var envelope = Stretch(BrassCurve, duration);
            var sound = Multiply(fmSignal, envelope);
            var note = StrikeNote(sound, delay, volume);

            return note;
        }

        /// <inheritdoc cref="docs._default" />
        private Outlet ElectricNote(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[A4];
            volume = volume ?? _[1];

            var modDepth = Multiply(_[0.02], Stretch(LineDownCurve, duration));
            var fmSignal = Add
            (
                FMAroundFreq(freq, Multiply(freq, _[1.5]), modDepth),
                FMAroundFreq(freq, Multiply(freq, _[2.0]), modDepth)
            );

            var envelope = Stretch(DampedBlockCurve, duration);
            var modulatedSound = Multiply(fmSignal, envelope);
            var adjustedVolume = Multiply(volume, _[0.6]);
            var note = StrikeNote(modulatedSound, delay, adjustedVolume);

            return note;
        }

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.005 </summary>
        /// <param name="freq"> The base frequency of the sound in Hz (default A1/55Hz). </param>
        /// <inheritdoc cref="docs._default" />
        private Outlet RippleBass(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[A1];

            var fmSignal = FMAroundFreq(Multiply(freq, _[8]), Divide(freq, _[2]), _[0.005]);
            var note = ShapeRippleSound(fmSignal, delay, volume, duration);

            return note;
        }

        /// <summary> Mod speed below sound freq, changes sound freq ±10Hz </summary>
        /// <param name="freq"> The base frequency of the sound in Hz (default A3/220Hz). </param>
        /// <inheritdoc cref="docs._default" />
        private Outlet RippleNote_SharpMetallic(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[A3];
            var fmSignal = FMInHertz(freq, Divide(freq, _[2]), _[10]);
            var sound = ShapeRippleSound(fmSignal, delay, volume, duration);
            return sound;
        }

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.005 </summary>
        private Outlet RippleSound_Clean(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[A4];

            var fmSignal = FMAroundFreq(freq, _[20], _[0.005]);
            var sound = ShapeRippleSound(fmSignal, delay, volume, duration);

            return sound;
        }

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.02 </summary>
        /// <param name="duration"> The duration of the sound in seconds (default is 2.5). </param>
        /// <inheritdoc cref="docs._default" />
        private Outlet RippleSound_FantasyEffect(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[A5];

            var fmSignal = FMAroundFreq(freq, _[10], _[0.02]);
            var sound = ShapeRippleSound(fmSignal, delay, volume, duration);

            return sound;
        }

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.05 </summary>
        /// <inheritdoc cref="ShapeRippleSound" />
        private Outlet RippleSound_CoolDouble(Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null)
        {
            freq = freq ?? _[A5];

            var fmSignal = FMAroundFreq(freq, _[10], _[0.05]);
            var sound = ShapeRippleSound(fmSignal, delay, volume, duration);

            return sound;
        }

        /// <summary> Shapes a ripple effect sound giving it a volume envelope and a delay, volume and duration. </summary>
        /// <param name="duration"> The duration of the sound in seconds (default is 2.5). </param>
        /// <param name="fmSignal"> A ripple sound to be shaped </param>
        /// <inheritdoc cref="docs._default" />
        private Outlet ShapeRippleSound(Outlet fmSignal, Outlet delay, Outlet volume, Outlet duration)
        {
            duration = duration ?? _[2.5];
            var envelope = Stretch(RippleCurve, duration);
            var sound = Multiply(fmSignal, envelope);
            var strike = StrikeNote(sound, delay, volume);
            return strike;
        }

        /// <summary>
        /// Beating audible further along the sound.
        /// Mod speed much below sound freq, changes sound freq drastically * [0.5, 1.5]
        /// </summary>
        private Outlet Create_FM_Noise_Beating(Outlet pitch = null)
            => FMAroundFreq(pitch ?? _[A4], _[55], _[0.5]);

        #endregion

        #region Algorithms

        /// <summary> FM sound synthesis modulating with addition. Modulates sound freq to +/- a number of Hz. </summary>
        /// <param name="modDepth"> In Hz </param>
        /// <inheritdoc cref="docs._default" />
        private Outlet FMInHertz(Outlet soundFreq, Outlet modSpeed, Outlet modDepth)
        {
            var modulator = Sine(modDepth, modSpeed);
            var sound = Sine(_[1], Add(soundFreq, modulator));
            return sound;
        }

        /// <summary> FM with (faulty) multiplication around 0. </summary>
        /// <inheritdoc cref="docs._default" />
        private Outlet FMAround0(Outlet soundFreq, Outlet modSpeed, Outlet modDepth)
        {
            var modulator = Sine(modDepth, modSpeed);
            var sound = Sine(_[1], Multiply(soundFreq, modulator));
            return sound;
        }

        /// <summary> FM with multiplication around 1. </summary>
        /// <inheritdoc cref="docs._default" />
        private Outlet FMAroundFreq(Outlet soundFreq, Outlet modSpeed, Outlet modDepth)
        {
            var modulator = Add(_[1], Sine(modDepth, modSpeed));
            var sound = Sine(_[1], Multiply(soundFreq, modulator));
            return sound;
        }

        private const double MILD_ECHO_TIME = 0.33 * 5;

        private Outlet MildEcho(Outlet outlet)
            => EntityFactory.CreateEcho(this, outlet, count: 6, denominator: 4, delay: 0.33);

        private const double DEEP_ECHO_TIME = 0.5 * 5;

        /// <summary> Applies a deep echo effect to the specified sound. </summary>
        /// <param name="melody"> The original sound to which the echo effect will be applied. </param>
        /// <returns> An <see cref="Outlet" /> representing the sound with the deep echo effect applied. </returns>
        private Outlet DeepEcho(Outlet melody)
            => EntityFactory.CreateEcho(this, melody, count: 6, denominator: 2, delay: 0.5);

        #endregion

        #region Curves

        Outlet FluteCurve => CurveIn
        (
            (time: 0.00, value: 0.0),
            (time: 0.05, value: 0.8),
            (time: 0.10, value: 1.0),
            (time: 0.90, value: 0.7),
            (time: 1.00, value: 0.0)
        );

        Outlet BrassCurve => CurveIn
        (
            (time: 0.00, value: 1),
            (time: 0.93, value: 1),
            (time: 1.00, value: 0)
        );

        Outlet RippleCurve => CurveIn
        (
            (time: 0.00, value: 0.00),
            (time: 0.01, value: 0.75),
            (time: 0.05, value: 0.50),
            (time: 0.25, value: 1.00),
            (time: 1.00, value: 0.00)
        );

        Outlet DampedBlockCurve => CurveIn
        (
            (time: 0.00, value: 0),
            (time: 0.01, value: 1),
            (time: 0.99, value: 1),
            (time: 1.00, value: 0)
        );

        Outlet LineDownCurve => CurveIn
        (
            (time: 0, value: 1),
            (time: 1, value: 0)
        );

        /// <summary>
        /// A curve that can be applied to the modulator depth to tame the modulation.
        /// In this version of FM synthesis, the modulation depth accumulates over time without such taming.
        /// This is because of a lack of time tracking in the oscillators in this version.
        /// </summary>
        Outlet ModTamingCurve => CurveIn
        (
            0.3, 1.0, 0.3, 0.0
        );

        /// <inheritdoc cref="ModTamingCurve" />
        Outlet ModTamingCurve2 => CurveIn
        (
            1.0, 0.5, 0.2, 0.0
        );

        /// <inheritdoc cref="ModTamingCurve" />
        Outlet ModTamingCurve8Times => CurveIn
        (
            0.3, 1.0, 0.3, 0.0,
            0.3, 1.0, 0.3, 0.0,
            0.3, 1.0, 0.3, 0.0,
            0.3, 1.0, 0.3, 0.0,
            0.3, 1.0, 0.3, 0.0,
            0.3, 1.0, 0.3, 0.0,
            0.3, 1.0, 0.3, 0.0,
            0.3, 1.0, 0.3, 0.0
        );

        /// <summary> When harmonics thicken near the center, this curve can even out the volume over time. </summary>
        Outlet EvenOutCurve => CurveIn
        (
            (time: 0.00, value: 1.0),
            (time: 0.33, value: 0.6),
            (time: 0.50, value: 0.6),
            (time: 0.75, value: 0.8),
            (time: 1.00, value: 1.0)
        );

        Outlet ChordVolumeCurve => CurveIn
        (
            (0.0, 0.0), (0.05, 0.0), (0.98, 0.5),
            (1.0, 0.0), (1.05, 0.6), (1.98, 0.6),
            (2.0, 0.0), (2.05, 0.8), (2.98, 0.8),
            (3.0, 0.0), (3.05, 0.6), (3.80, 0.6),
            (4.0, 0.0), (4.05, 0.9), (4.98, 0.9),
            (5.0, 0.0), (5.05, 0.8), (5.92, 0.8),
            (6.0, 0.0), (6.05, 1.0), (6.98, 1.0),
            (7.0, 0.0), (7.05, 0.6), (7.78, 0.2),
            (8.0, 0.0)
        );

        private static readonly (double time, double frequency1, double frequency2, double frequency3)[]
            _chordFrequencies =
            {
                (0.0, E4, A4, C5),
                (1.0, F4, A4, C5),
                (2.0, E4, G4, C5),
                (3.0, D4, G4, B4),
                (4.0, D4, F4, A4),
                (5.0, F4, A4, D5),
                (6.0, E4, A4, C5),
                (7.0, E4, A5, E5),
                (8.0, E4, A5, E5)
            };

        Outlet ChordPitchCurve1 => CurveIn(
            _chordFrequencies.Select(x => new NodeInfo(x.time,
                                                       x.frequency1,
                                                       NodeTypeEnum.Block)).ToArray());

        Outlet ChordPitchCurve2 => CurveIn(
            _chordFrequencies.Select(x => new NodeInfo(x.time,
                                                       x.frequency2,
                                                       NodeTypeEnum.Block)).ToArray());

        Outlet ChordPitchCurve3 => CurveIn(
            _chordFrequencies.Select(x => new NodeInfo(x.time,
                                                       x.frequency3,
                                                       NodeTypeEnum.Block)).ToArray());
        #endregion
    }
}