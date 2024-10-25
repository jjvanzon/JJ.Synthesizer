using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Wishes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace JJ.Business.Synthesizer.Tests.Functional
{
    /// <summary>
    /// NOTE: Version 0.0.250 does not have time tracking in its oscillator,
    /// making the FM synthesis behave differently.
    /// </summary>
    [TestClass]
    [TestCategory("Functional")]
    public class FMTests : SynthWishes
    {
        FluentOutlet DefaultVolume => _[0.5];
        FluentOutlet JingleVolume => _[0.19];

        int          MildEchoCount => 4;
        FluentOutlet MildEchoDelay => _[0.33];
        FluentOutlet MildEchoTime  => MildEchoDelay * (MildEchoCount - 1);
        int          DeepEchoCount => 4;
        FluentOutlet DeepEchoDelay => _[0.5];
        FluentOutlet DeepEchoTime  => DeepEchoDelay * (DeepEchoCount - 1);

        public FMTests()
            : base(beat: 0.45, bar: 4 * 0.45)
        {
            _chordFrequencies = CreateChordFrequencies();
        }

        #region Tests

        // Long Running
        internal void FM_Jingle_RunTest()
        {
            PlayMono(() => DeepEcho(Jingle()), t[bar: 9, beat: 2] + DeepEchoTime);
        }

        [TestMethod]
        public void FM_Flute_Melody1() => new FMTests().FM_Flute_Melody1_RunTest();

        void FM_Flute_Melody1_RunTest()
            => PlayMono(() => MildEcho(FluteMelody1), volume: 0.6, duration: bars[4] + MildEchoTime);

        [TestMethod]
        public void FM_Flute_Melody2() => new FMTests().FM_Flute_Melody2_RunTest();

        void FM_Flute_Melody2_RunTest()
            => PlayMono(() => MildEcho(FluteMelody2), volume: 0.3, duration: bars[2.5] + MildEchoTime);

        [TestMethod]
        public void FM_Flute1() => new FMTests().FM_Flute1_RunTest();

        void FM_Flute1_RunTest()
            => PlayMono(() => MildEcho(Flute1()), duration: 1 + MildEchoTime, DefaultVolume);

        [TestMethod]
        public void FM_Flute2() => new FMTests().FM_Flute2_RunTest();

        void FM_Flute2_RunTest()
            => PlayMono(() => MildEcho(Flute2()), duration: 1 + MildEchoTime, DefaultVolume);

        [TestMethod]
        public void FM_Flute3() => new FMTests().FM_Flute3_RunTest();

        void FM_Flute3_RunTest()
            => PlayMono(() => MildEcho(Flute3()), duration: 1 + MildEchoTime, DefaultVolume);

        [TestMethod]
        public void FM_Flute4() => new FMTests().FM_Flute4_RunTest();

        void FM_Flute4_RunTest()
            => PlayMono(() => MildEcho(Flute4()), duration: 1 + MildEchoTime, DefaultVolume);

        [TestMethod]
        public void FM_Organ() => new FMTests().FM_Organ_RunTest();

        void FM_Organ_RunTest()
            => SaveAudioMono(() => MildEcho(Organ(duration: bars[8])), duration: bars[8] + MildEchoTime, DefaultVolume);

        [TestMethod]
        public void FM_Organ_Chords() => new FMTests().FM_Organ_Chords_RunTest();

        void FM_Organ_Chords_RunTest()
            => PlayMono(() => MildEcho(OrganChords), volume: 0.22, duration: bars[8] + MildEchoTime);

        [TestMethod]
        public void FM_Pad() => new FMTests().FM_Pad_RunTest();

        void FM_Pad_RunTest()
            => PlayMono(() => MildEcho(Pad()), duration: bars[8] + MildEchoTime, volume: 0.2);

        [TestMethod]
        public void FM_Pad_Chords() => new FMTests().FM_Pad_Chords_RunTest();

        void FM_Pad_Chords_RunTest()
            => PlayMono(() => MildEcho(PadChords), volume: 0.15, duration: bars[8] + MildEchoTime);

        [TestMethod]
        public void FM_Trombone() => new FMTests().FM_Trombone_RunTest();

        void FM_Trombone_RunTest()
            => PlayMono(() => MildEcho(Trombone(E2)), duration: 2, DefaultVolume);

        [TestMethod]
        public void FM_Horn() => new FMTests().FM_Horn_RunTest();

        void FM_Horn_RunTest()
            => PlayMono(() => MildEcho(Horn(duration: _[1])), duration: 1 + MildEchoTime, DefaultVolume);

        [TestMethod]
        public void FM_Trombone_Melody1() => new FMTests().FM_Trombone_Melody1_RunTest();

        void FM_Trombone_Melody1_RunTest()
            => PlayMono(() => MildEcho(TromboneMelody1), volume: 0.45, duration: bars[4] + MildEchoTime);

        [TestMethod]
        public void FM_Trombone_Melody2() => new FMTests().FM_Trombone_Melody2_RunTest();

        void FM_Trombone_Melody2_RunTest()
            => PlayMono(() => MildEcho(TromboneMelody2), volume: 0.75, duration: bars[3.5] + MildEchoTime);

        [TestMethod]
        public void FM_Trombone_Melody3() => new FMTests().FM_Trombone_Melody3_RunTest();

        void FM_Trombone_Melody3_RunTest()
            => PlayMono(() => MildEcho(TromboneMelody3), duration: bars[1.5] + MildEchoTime, DefaultVolume);

        [TestMethod]
        public void FM_Horn_Melody1() => new FMTests().FM_Horn_Melody1_RunTest();

        void FM_Horn_Melody1_RunTest()
            => PlayMono(() => MildEcho(HornMelody1), volume: 0.6, duration: bars[4] + MildEchoTime);

        [TestMethod]
        public void FM_Horn_Melody2() => new FMTests().FM_Horn_Melody2_RunTest();

        void FM_Horn_Melody2_RunTest()
            => PlayMono(() => MildEcho(HornMelody2), volume: 0.6, duration: bars[3.5] + MildEchoTime);

        [TestMethod]
        public void FM_ElectricNote() => new FMTests().FM_ElectricNote_RunTest();

        void FM_ElectricNote_RunTest()
            => PlayMono(() => MildEcho(ElectricNote(duration: _[1.5])), 1.5 + MildEchoTime, 0.20);

        [TestMethod]
        public void FM_RippleBass() => new FMTests().FM_RippleBass_RunTest();

        void FM_RippleBass_RunTest()
            => PlayMono(() => DeepEcho(RippleBass(duration: _[3])), duration: 3 + DeepEchoTime, DefaultVolume);

        [TestMethod]
        public void FM_RippleBass_Melody1() => new FMTests().FM_RippleBass_Melody1_RunTest();

        void FM_RippleBass_Melody1_RunTest()
            => SaveAudioMono(() => DeepEcho(RippleBassMelody1), volume: 0.3, duration: bars[5] + DeepEchoTime);

        [TestMethod]
        public void FM_RippleBass_Melody2() => new FMTests().FM_RippleBass_Melody2_RunTest();

        void FM_RippleBass_Melody2_RunTest()
            => PlayMono(() => DeepEcho(RippleBassMelody2), volume: 0.3, duration: bars[4] + DeepEchoTime);

        [TestMethod]
        public void FM_RippleNote_SharpMetallic() => new FMTests().FM_RippleNote_SharpMetallic_RunTest();

        void FM_RippleNote_SharpMetallic_RunTest()
            => PlayMono(() => DeepEcho(RippleNote_SharpMetallic(duration: _[2.2])), duration: 2.2 + DeepEchoTime, DefaultVolume);

        [TestMethod]
        public void FM_RippleSound_Clean() => new FMTests().FM_RippleSound_Clean_RunTest();

        void FM_RippleSound_Clean_RunTest()
            => PlayMono(() => DeepEcho(RippleSound_Clean(duration: _[4])), duration: 4 + DeepEchoTime, DefaultVolume);

        [TestMethod]
        public void FM_RippleSound_FantasyEffect() => new FMTests().FM_RippleSound_FantasyEffect_RunTest();

        void FM_RippleSound_FantasyEffect_RunTest()
            => PlayMono(() => DeepEcho(RippleSound_FantasyEffect(duration: _[4])), duration: 4 + DeepEchoTime, volume: 0.10);

        [TestMethod]
        public void FM_RippleSound_CoolDouble() => new FMTests().FM_RippleSound_CoolDouble_RunTest();

        void FM_RippleSound_CoolDouble_RunTest()
            => PlayMono(() => DeepEcho(RippleSound_CoolDouble(duration: _[3])), duration: 3 + DeepEchoTime, volume: 0.10);

        [TestMethod]
        public void FM_Noise_Beating() => new FMTests().FM_Noise_Beating_RunTest();

        void FM_Noise_Beating_RunTest()
            => SaveAudioMono(() => MildEcho(Create_FM_Noise_Beating(A4)), duration: 5, DefaultVolume);

        #endregion

        #region Jingle

        FluentOutlet Jingle()
        {
            double fluteVolume      = 1.2;
            double chordsVolume     = 0.5;
            double tromboneVolume   = 0.7;
            double hornVolume       = 0.6;
            double rippleBassVolume = 0.7;

            var pattern1 = ParallelAdd(
                bars[4], JingleVolume,
                () => Multiply(fluteVolume,      FluteMelody1),
                () => Multiply(tromboneVolume,   TromboneMelody1),
                () => Multiply(hornVolume,       HornMelody1),
                () => Multiply(rippleBassVolume, RippleBassMelody1));

            var pattern2 = ParallelAdd
            (
                bars[4], JingleVolume,
                () => Multiply(fluteVolume,      FluteMelody2),
                () => Multiply(tromboneVolume,   TromboneMelody2),
                () => Multiply(hornVolume,       HornMelody2),
                () => Multiply(rippleBassVolume, RippleBassMelody2)
            );

            var composition = ParallelAdd
            (
                bars[8],
                () => Multiply(chordsVolume,     PadChords) * JingleVolume,
                () => pattern1,
                () => Delay(pattern2, bars[4])
            );

            return composition;
        }

        #endregion

        #region Melodies

        FluentOutlet FluteMelody1 => Add
        (
            Flute1(E4, t[bar: 1, beat: 1.0], volume: _[0.80], beats[2.00]),
            Flute1(F4, t[bar: 1, beat: 2.5], volume: _[0.70], beats[2.17]),
            Flute1(G4, t[bar: 1, beat: 4.0], volume: _[0.60], beats[1.00]),
            Flute1(A4, t[bar: 2, beat: 1.0], volume: _[0.80], beats[2.33]),
            Flute2(B4, t[bar: 2, beat: 2.5], volume: _[0.50], beats[1.00]),
            Flute2(A3, t[bar: 2, beat: 4.0], volume: _[0.50], beats[1.67]), // Flute1 would appear delayed because of destructive interference?
            Flute3(G3, t[bar: 3, beat: 1.0], volume: _[0.85], beats[2.00]),
            Flute1(G4, t[bar: 3, beat: 2.5], volume: _[0.80], beats[2.50])
        );

        FluentOutlet FluteMelody2 => Add
        (
            Flute1(E4, t[bar: 1, beat: 1.0], volume: _[0.59], beats[1.8]),
            Flute2(F4, t[bar: 1, beat: 2.5], volume: _[0.68], beats[1.0]),
            Flute1(G4, t[bar: 1, beat: 4.0], volume: _[0.74], beats[0.6]),
            Flute2(A4, t[bar: 2, beat: 1.0], volume: _[0.82], beats[2.0]),
            Flute3(B4, t[bar: 2, beat: 2.5], volume: _[0.74], beats[1.0]),
            Flute2(G4, t[bar: 2, beat: 4.0], volume: _[0.90], beats[0.4]),
            Flute4(A4, t[bar: 3, beat: 1.0], volume: _[1.00], _[1.66])
        );

        FluentOutlet OrganChords =>
            Multiply
            (
                Stretch(ChordVolumeCurve, bars[1]),
                Add
                (
                    Organ(Stretch(ChordPitchCurve1, bars[1]), duration: bars[8]),
                    Organ(Stretch(ChordPitchCurve2, bars[1]), duration: bars[8]),
                    Organ(Stretch(ChordPitchCurve3, bars[1]), duration: bars[8])
                )
            );

        FluentOutlet PadChords => Multiply
        (
            Stretch(ChordVolumeCurve, bars[1]),
            Add
            (
                Pad(Stretch(ChordPitchCurve1, bars[1]), duration: bars[8]),
                Pad(Stretch(ChordPitchCurve2, bars[1]), duration: bars[8]),
                Pad(Stretch(ChordPitchCurve3, bars[1]), duration: bars[8])
            )
        );

        FluentOutlet HornMelody1 => Add
        (
            //Horn(A2, Beat[01], duration: Beats[2]),
            //Horn(E3, Beat[02]),
            //Horn(F2, Beat[05], duration: Beats[3]),
            //Horn(C3, Beat[07]),
            Horn(C2, beat[09], duration: beats[3], volume: _[0.7]),
            //Horn(G2, Beat[11]),
            Horn(G1, beat[13], duration: beats[4], volume: _[0.5]) //,
            //Horn(D3, Beat[15])
        );

        FluentOutlet HornMelody2 => Add
        (
            Horn(A2, beat[1], duration: beat[3], volume: _[0.75]),
            //Horn(E3, Beat[3]),
            Horn(F2, beat[5], duration: beat[3], volume: _[0.85]),
            //Horn(C3, Beat[7]),
            Horn(A1, beat[9], duration: beat[5], volume: _[1.0])
        );

        FluentOutlet TromboneMelody1 => Add
        (
            //Trombone(A3, Beat[00]),
            //Trombone(E4, Beat[02]),
            //Trombone(F3, Beat[04]),
            //Trombone(C4, Beat[06]),
            //Trombone(C3, Beat[08]),
            //Trombone(G3, Beat[10]),
            //Trombone(G2, Beat[12]),
            //Trombone(B3, Beat[14])
        );

        FluentOutlet TromboneMelody2 => Add
        (
            //Trombone(A2, Beat[1]),
            Trombone(E4, beat[3], durationFactor: _[1.4]),
            //Trombone(F2, Beat[5]),
            Trombone(C4, beat[7], durationFactor: _[1.4]) //,
            //Trombone(A3, Beat[9])
        );

        FluentOutlet TromboneMelody3 => Add
        (
            Trombone(A1,       beat[1]),
            Trombone(E2,       beat[3]),
            Trombone(F1_Sharp, beat[5], volume: _[0.7])
        );

        FluentOutlet RippleBassMelody1 => _[0];
        //RippleBass(A2, delay: Bar[1], duration: Bars[2]);

        FluentOutlet RippleBassMelody2 =>
            RippleBass(A1, delay: bar[3.5], duration: bars[0.8]);

        #endregion

        #region Instruments

        /// <summary> High hard flute: mod speed above sound freq, changes sound freq * [-0.005, 0.005] (erroneously) </summary>
        /// <inheritdoc cref="Wishes.Helpers.docs._default" />
        FluentOutlet Flute1(FluentOutlet freq = null, FluentOutlet delay = null, FluentOutlet volume = null, FluentOutlet duration = null)
        {
            freq = freq ?? A4;

            var fmSignal       = FMAround0(Divide(freq, 2), freq, _[0.005]);
            var envelope       = Stretch(FluteCurve, duration);
            var modulatedSound = Multiply(fmSignal, envelope);
            var note           = StrikeNote(modulatedSound, delay, volume);

            return note;
        }

        /// <summary> Yet another flute: mod speed above sound freq, changes sound freq * 1 +/- 0.005 </summary>
        /// <inheritdoc cref="Wishes.Helpers.docs._default" />
        FluentOutlet Flute2(FluentOutlet freq = null, FluentOutlet delay = null, FluentOutlet volume = null, FluentOutlet duration = null)
        {
            freq   = freq ?? A4;
            volume = volume ?? _[1];

            var fmSignal       = FMAroundFreq(freq, Multiply(freq, 2), _[0.005]);
            var envelope       = Stretch(FluteCurve, duration);
            var modulatedSound = Multiply(fmSignal, envelope);
            var adjustedVolume = Multiply(volume,   0.85);
            var note           = StrikeNote(modulatedSound, delay, adjustedVolume);

            return note;
        }

        /// <summary> Yet another flute: mod speed above sound freq, changes sound freq * 1 +/- 0.005 </summary>
        /// <inheritdoc cref="Wishes.Helpers.docs._default" />
        FluentOutlet Flute3(FluentOutlet freq = null, FluentOutlet delay = null, FluentOutlet volume = null, FluentOutlet duration = null)
        {
            freq   = freq ?? A4;
            volume = volume ?? _[1];

            var fmSignal       = FMAroundFreq(freq, Multiply(freq, 4), _[0.005]);
            var envelope       = Stretch(FluteCurve, duration);
            var sound          = Multiply(fmSignal, envelope);
            var adjustedVolume = Multiply(volume, 0.8);
            var note           = StrikeNote(sound, delay, adjustedVolume);

            return note;
        }

        /// <summary> Modulated hard flute: mod speed below sound freq, changes sound freq * [-0.005, 0.005] (erroneously) </summary>
        /// <inheritdoc cref="Wishes.Helpers.docs._default" />
        FluentOutlet Flute4(FluentOutlet freq = null, FluentOutlet delay = null, FluentOutlet volume = null, FluentOutlet duration = null)
        {
            freq   = freq ?? A4;
            volume = volume ?? _[1];

            var fmSignal       = FMAround0(Multiply(freq, 2), freq, _[0.005]);
            var envelope       = Stretch(FluteCurve, duration);
            var sound          = Multiply(fmSignal, envelope);
            var adjustedVolume = Multiply(volume, 0.70);
            var note           = StrikeNote(sound, delay, adjustedVolume);

            return note;
        }

        /// <inheritdoc cref="Wishes.Helpers.docs._default" />
        FluentOutlet Organ(FluentOutlet freq = null, FluentOutlet delay = null, FluentOutlet volume = null, FluentOutlet duration = null)
        {
            freq     = freq ?? A4;
            duration = duration ?? _[1];

            var modCurve = Stretch(ModTamingCurve, duration);
            var modDepth = Multiply(0.0001, modCurve);
            var fmSignal = FMAroundFreq(freq, Multiply(freq, 2), modDepth);

            var volumeEvenOutCurve  = Stretch(EvenOutCurve, duration);
            var soundWithEvenVolume = Multiply(fmSignal, volumeEvenOutCurve);

            var note = StrikeNote(soundWithEvenVolume, delay, volume);

            return note;
        }

        /// <inheritdoc cref="Wishes.Helpers.docs._default" />
        FluentOutlet Pad(FluentOutlet freq = null, FluentOutlet delay = null, FluentOutlet volume = null, FluentOutlet duration = null)
        {
            freq     = freq ?? A4;
            duration = duration ?? beats[1];

            // Tame modulation
            var modCurve = Stretch(ModTamingCurve8Times, duration);
            modCurve = Multiply(modCurve, Stretch(ModTamingCurve, duration));
            modCurve = Multiply(modCurve, Stretch(LineDownCurve,  duration));

            var fmSignal = Add
            (
                FMAroundFreq(freq, Multiply(freq, 2), Multiply(0.00020, modCurve)),
                FMAroundFreq(freq, Multiply(freq, 3), Multiply(0.00015, modCurve))
            );

            var volumeEvenOutCurve  = Stretch(EvenOutCurve, duration);
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
        /// <inheritdoc cref="Wishes.Helpers.docs._default" />
        FluentOutlet Trombone(FluentOutlet freq = null, FluentOutlet delay = null, FluentOutlet volume = null, FluentOutlet durationFactor = null)
        {
            freq           = freq ?? A1;
            durationFactor = durationFactor ?? _[1];

            var fmSignal = FMInHertz(Multiply(freq, 2), freq, _[5]);

            // Exaggerate Duration when Lower
            var baseNote            = A1;
            var baseNoteDuration    = Multiply(0.8, durationFactor);
            var ratio               = Divide(baseNote, freq);
            var transformedDuration = Multiply(baseNoteDuration, Power(ratio, 1.5));

            var envelope = Stretch(BrassCurve, transformedDuration);
            var sound    = Multiply(fmSignal, envelope);
            var note     = StrikeNote(sound, delay, volume);

            return note;
        }

        /// <summary>
        /// Sounds like Horn.
        /// FM with mod speed below sound freq, changes sound freq to +/- 5Hz.
        /// Volume curve is applied.
        /// FM modulator is attempted to be tamed with curves.
        /// </summary>
        /// <param name="freq"> The base frequency of the sound in Hz (default A2/110Hz). </param>
        /// <inheritdoc cref="Wishes.Helpers.docs._default" />
        FluentOutlet Horn(FluentOutlet freq = null, FluentOutlet delay = null, FluentOutlet volume = null, FluentOutlet duration = null)
        {
            freq = freq ?? A2;

            var tamedMod = Multiply(5, Stretch(ModTamingCurve2, duration));

            var fmSignal = FMInHertz(Multiply(freq, 2), freq, tamedMod);
            var envelope = Stretch(BrassCurve, duration);
            var sound    = Multiply(fmSignal, envelope);
            var note     = StrikeNote(sound, delay, volume);

            return note;
        }

        /// <inheritdoc cref="Wishes.Helpers.docs._default" />
        FluentOutlet ElectricNote(FluentOutlet freq = null, FluentOutlet delay = null, FluentOutlet volume = null, FluentOutlet duration = null)
        {
            freq   = freq ?? A4;
            volume = volume ?? _[1];

            var modDepth = 0.02 * Stretch(LineDownCurve, duration);
            var fmSignal = Add
            (
                FMAroundFreq(freq, freq * 1.5, modDepth),
                FMAroundFreq(freq, freq * 2.0, modDepth)
            );

            var modulatedSound = fmSignal.Curve(DampedBlockCurve.Stretch(duration));
            var adjustedVolume = volume * 0.6;
            var note           = StrikeNote(modulatedSound, delay, adjustedVolume);
            
            return note;
        }

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.005 </summary>
        /// <param name="freq"> The base frequency of the sound in Hz (default A1/55Hz). </param>
        /// <inheritdoc cref="Wishes.Helpers.docs._default" />
        FluentOutlet RippleBass(FluentOutlet freq = null, FluentOutlet delay = null, FluentOutlet volume = null, FluentOutlet duration = null)
        {
            freq = freq ?? A1;
            var fmSignal = FMAroundFreq(freq * 8, freq / 2, _[0.005]);
            var note     = ShapeRippleSound(fmSignal, delay, volume, duration);
            return note;
        }

        /// <summary> Mod speed below sound freq, changes sound freq ±10Hz </summary>
        /// <param name="freq"> The base frequency of the sound in Hz (default A3/220Hz). </param>
        /// <inheritdoc cref="Wishes.Helpers.docs._default" />
        FluentOutlet RippleNote_SharpMetallic(FluentOutlet freq = null, FluentOutlet delay = null, FluentOutlet volume = null, FluentOutlet duration = null)
        {
            freq = freq ?? A3;
            var fmSignal = FMInHertz(freq, freq / 2, _[10]);
            var sound    = ShapeRippleSound(fmSignal, delay, volume, duration);
            return sound;
        }

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.005 </summary>
        FluentOutlet RippleSound_Clean(FluentOutlet freq = null, FluentOutlet delay = null, FluentOutlet volume = null, FluentOutlet duration = null)
        {
            freq = freq ?? A4;
            var fmSignal = FMAroundFreq(freq, _[20], _[0.005]);
            var sound    = ShapeRippleSound(fmSignal, delay, volume, duration);
            return sound;
        }

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.02 </summary>
        /// <param name="duration"> The duration of the sound in seconds (default is 2.5). </param>
        /// <inheritdoc cref="Wishes.Helpers.docs._default" />
        FluentOutlet RippleSound_FantasyEffect(FluentOutlet freq = null, FluentOutlet delay = null, FluentOutlet volume = null, FluentOutlet duration = null)
        {
            freq = freq ?? A5;
            var fmSignal = FMAroundFreq(freq, _[10], _[0.02]);
            var sound    = ShapeRippleSound(fmSignal, delay, volume, duration);
            return sound;
        }

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.05 </summary>
        /// <inheritdoc cref="ShapeRippleSound" />
        FluentOutlet RippleSound_CoolDouble(FluentOutlet freq = null, FluentOutlet delay = null, FluentOutlet volume = null, FluentOutlet duration = null)
        {
            freq = freq ?? A5;
            var fmSignal = FMAroundFreq(freq, _[10], _[0.05]);
            var sound    = ShapeRippleSound(fmSignal, delay, volume, duration);
            return sound;
        }

        /// <summary> Shapes a ripple effect sound giving it a volume envelope and a delay, volume and duration. </summary>
        /// <param name="duration"> The duration of the sound in seconds (default is 2.5). </param>
        /// <param name="fmSignal"> A ripple sound to be shaped </param>
        /// <inheritdoc cref="Wishes.Helpers.docs._default" />
        FluentOutlet ShapeRippleSound(FluentOutlet input, FluentOutlet delay, FluentOutlet volume, FluentOutlet duration)
        {
            duration = duration ?? _[2.5];
            var envelope = Stretch(RippleCurve, duration);
            var sound    = input * envelope;
            var strike   = StrikeNote(sound, delay, volume);
            return strike;
        }

        /// <summary>
        /// Beating audible further along the sound.
        /// Mod speed much below sound freq, changes sound freq drastically * [0.5, 1.5]
        /// </summary>
        FluentOutlet Create_FM_Noise_Beating(FluentOutlet pitch = null)
            => FMAroundFreq(pitch ?? A4, _[55], _[0.5]);

        #endregion

        #region Algorithms

        /// <summary> FM sound synthesis modulating with addition. Modulates sound freq to +/- a number of Hz. </summary>
        /// <param name="modDepth"> In Hz </param>
        /// <inheritdoc cref="Wishes.Helpers.docs._default" />
        FluentOutlet FMInHertz(FluentOutlet soundFreq, FluentOutlet modSpeed, FluentOutlet modDepth)
        {
            var modulator = Sine(modSpeed) * modDepth;
            var sound     = Sine(soundFreq + modulator);
            return sound;
        }

        /// <summary> FM with (faulty) multiplication around 0. </summary>
        /// <inheritdoc cref="Wishes.Helpers.docs._default" />
        FluentOutlet FMAround0(FluentOutlet soundFreq, FluentOutlet modSpeed, FluentOutlet modDepth)
        {
            var modulator = Sine(modSpeed) * modDepth;
            var sound     = Sine(soundFreq * modulator);
            return sound;
        }

        /// <summary> FM with multiplication around 1. </summary>
        /// <inheritdoc cref="Wishes.Helpers.docs._default" />
        FluentOutlet FMAroundFreq(FluentOutlet soundFreq, FluentOutlet modSpeed, FluentOutlet modDepth)
        {
            var modulator = 1 + Sine(modSpeed) * modDepth;
            var sound     = Sine(soundFreq * modulator);
            return sound;
        }

        FluentOutlet MildEcho(FluentOutlet outlet) => Echo(outlet, MildEchoCount, 0.25, MildEchoDelay);

        FluentOutlet DeepEcho(FluentOutlet melody) => Echo(melody, DeepEchoCount, 0.5, DeepEchoDelay);

        #endregion

        #region Curves

        FluentOutlet FluteCurve => Curve
        (
            (time: 0.00, value: 0.0),
            (time: 0.05, value: 0.8),
            (time: 0.10, value: 1.0),
            (time: 0.90, value: 0.7),
            (time: 1.00, value: 0.0)
        );

        FluentOutlet BrassCurve => Curve
        (
            (time: 0.00, value: 0),
            (time: 0.07, value: 1),
            (time: 0.93, value: 1),
            (time: 1.00, value: 0)
        );

        FluentOutlet RippleCurve => Curve
        (
            (time: 0.00, value: 0.00),
            (time: 0.01, value: 0.75),
            (time: 0.05, value: 0.50),
            (time: 0.25, value: 1.00),
            (time: 1.00, value: 0.00)
        );

        FluentOutlet DampedBlockCurve => Curve
        (
            (time: 0.00, value: 0),
            (time: 0.01, value: 1),
            (time: 0.99, value: 1),
            (time: 1.00, value: 0)
        );

        FluentOutlet LineDownCurve => Curve
        (
            (time: 0, value: 1),
            (time: 1, value: 0)
        );

        /// <summary>
        /// A curve that can be applied to the modulator depth to tame the modulation.
        /// In this version of FM synthesis, the modulation depth accumulates over time without such taming.
        /// This is because of a lack of time tracking in the oscillators in this version.
        /// </summary>
        FluentOutlet ModTamingCurve => Curve(0.3, 1.0, 0.3, 0.0);

        /// <inheritdoc cref="ModTamingCurve" />
        FluentOutlet ModTamingCurve2 => Curve(1.0, 0.5, 0.2, 0.0);

        /// <inheritdoc cref="ModTamingCurve" />
        FluentOutlet ModTamingCurve8Times => Curve
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
        FluentOutlet EvenOutCurve => Curve
        (
            (time: 0.00, value: 1.0),
            (time: 0.33, value: 0.6),
            (time: 0.50, value: 0.6),
            (time: 0.75, value: 0.8),
            (time: 1.00, value: 1.0)
        );

        FluentOutlet ChordVolumeCurve => Curve
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

        (double time, FluentOutlet freq1, FluentOutlet freq2, FluentOutlet freq3)[] _chordFrequencies;

        (double time, FluentOutlet freq1, FluentOutlet freq2, FluentOutlet freq3)[] CreateChordFrequencies() => new[]
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

        FluentOutlet ChordPitchCurve1 => Curve(
            _chordFrequencies.Select(x => new NodeInfo(x.time,
                                                       x.freq1.Value,
                                                       NodeTypeEnum.Block)).ToArray());

        FluentOutlet ChordPitchCurve2 => Curve(
            _chordFrequencies.Select(x => new NodeInfo(x.time,
                                                       x.freq2.Value,
                                                       NodeTypeEnum.Block)).ToArray());

        FluentOutlet ChordPitchCurve3 => Curve(
            _chordFrequencies.Select(x => new NodeInfo(x.time,
                                                       x.freq3.Value,
                                                       NodeTypeEnum.Block)).ToArray());

        #endregion
    }
}