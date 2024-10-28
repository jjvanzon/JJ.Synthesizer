using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Wishes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ExplicitCallerInfoArgument

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
        private  int          MildEchoCount   => 7;
        private  FluentOutlet MildEchoDelay   => _[0.33];
        //internal FluentOutlet MildEchoTime    => MildEchoDelay * (MildEchoCount - 1);
        private  int          DeepEchoCount   => 7;
        private  FluentOutlet DeepEchoDelay   => _[0.5];
        //private  FluentOutlet DeepEchoTime    => DeepEchoDelay * (DeepEchoCount - 1);
        private  FluentOutlet DefaultDuration => _[1];
        private  double       DefaultVolume   => 0.5;
        private  FluentOutlet JingleVolume    => _[0.18];

        public FMTests() : base(beat: 0.45, bar: 4 * 0.45)
        {
            Mono();
            WithAudioLength(DefaultDuration);
            _chordFreqs = CreateChordFreqs();
        }

        #region Tests

        [TestMethod]
        public void FM_Jingle() => new FMTests().FM_Jingle_RunTest();

        internal void FM_Jingle_RunTest()
        {
            WithAudioLength(bars[8] + 1); // HACK: Without the + 1 the last note is missing.
            
            Play(() => DeepEcho(Jingle(), 1));
        }

        [TestMethod]
        public void FM_Flute_Melody1() => new FMTests().FM_Flute_Melody1_RunTest();

        void FM_Flute_Melody1_RunTest()
        {
            Play(() => MildEcho(FluteMelody1, volume: 0.6));
        }

        [TestMethod]
        public void FM_Flute_Melody2() => new FMTests().FM_Flute_Melody2_RunTest();

        void FM_Flute_Melody2_RunTest()
        {
            Play(() => MildEcho(FluteMelody2, 0.3));
        }

        [TestMethod]
        public void FM_Flute1() => new FMTests().FM_Flute1_RunTest();

        void FM_Flute1_RunTest() 
            => Play(() => MildEcho(Flute1(E4)));

        [TestMethod]
        public void FM_Flute2() => new FMTests().FM_Flute2_RunTest();

        void FM_Flute2_RunTest()
            => Play(() => MildEcho(Flute2(F4)));

        [TestMethod]
        public void FM_Flute3() => new FMTests().FM_Flute3_RunTest();

        void FM_Flute3_RunTest()
        {
            Play(() => MildEcho(Flute3(G4)));
        }

        [TestMethod]
        public void FM_Flute4() => new FMTests().FM_Flute4_RunTest();

        void FM_Flute4_RunTest()
        {
            Play(() => MildEcho(Flute4(A4)));
        }

        [TestMethod]
        public void FM_Organ() => new FMTests().FM_Organ_RunTest();

        void FM_Organ_RunTest()
        {
            // TODO: Weird notation                        
            var duration = bars[3];
            WithAudioLength(duration);
            Play(() => MildEcho(Organ(duration: duration)));
        }

        [TestMethod]
        public void FM_Organ_Chords() => new FMTests().FM_Organ_Chords_RunTest();

        void FM_Organ_Chords_RunTest()
        {
            Play(() => MildEcho(OrganChords, volume: 0.22));
        }
        
        [TestMethod]
        public void FM_Pad() => new FMTests().FM_Pad_RunTest();

        void FM_Pad_RunTest()
        {
            // TODO: Weird notation
            var duration = bars[3];
            WithAudioLength(duration);
            Play(() => MildEcho(Pad(duration: duration), volume: 0.2));
        }

        [TestMethod]
        public void FM_Pad_Chords() => new FMTests().FM_Pad_Chords_RunTest();

        void FM_Pad_Chords_RunTest()
        {
            Play(() => MildEcho(PadChords(volume: _[0.15]), volume: 1));
        }

        [TestMethod]
        public void FM_Pad_Chords_Distortion() => new FMTests().FM_Pad_Chords_Distortion_RunTest();

        void FM_Pad_Chords_Distortion_RunTest()
        {
            Play(() => MildEcho(PadChords(volume: _[0.92]), volume: 0.15));
        }

        [TestMethod]
        public void FM_Horn() => new FMTests().FM_Horn_RunTest();

        void FM_Horn_RunTest()
        {
            Play(() => MildEcho(Horn()));
        }

        [TestMethod]
        public void FM_Horn_Melody1() => new FMTests().FM_Horn_Melody1_RunTest();

        void FM_Horn_Melody1_RunTest()
        {
            Play(() => MildEcho(HornMelody1, volume: 0.6));
        }

        [TestMethod]
        public void FM_Horn_Melody2() => new FMTests().FM_Horn_Melody2_RunTest();

        void FM_Horn_Melody2_RunTest()
        {
            Play(() => MildEcho(HornMelody2, volume: 0.6));
        }
        
        [TestMethod]
        public void FM_Trombone() => new FMTests().FM_Trombone_RunTest();

        void FM_Trombone_RunTest()
        {
            WithAudioLength(2).Play(() => MildEcho(Trombone(E2)));
        }
        
        [TestMethod]
        public void FM_Trombone_Melody1() => new FMTests().FM_Trombone_Melody1_RunTest();

        void FM_Trombone_Melody1_RunTest()
        {
            Play(() => MildEcho(TromboneMelody1, volume: 0.45));
        }

        [TestMethod]
        public void FM_Trombone_Melody2() => new FMTests().FM_Trombone_Melody2_RunTest();

        void FM_Trombone_Melody2_RunTest()
        {
            Play(() => MildEcho(TromboneMelody2, volume: 0.75));
        }

        [TestMethod]
        public void FM_ElectricNote() => new FMTests().FM_ElectricNote_RunTest();

        void FM_ElectricNote_RunTest()
        {
            var duration = _[1.5];
            WithAudioLength(duration);
            
            Play(() => MildEcho(ElectricNote(duration: duration), volume: 0.2));
        }

        [TestMethod]
        public void FM_RippleBass() => new FMTests().FM_RippleBass_RunTest();

        void FM_RippleBass_RunTest()
        {
            var duration = _[3];
            WithAudioLength(duration);
            Play(() => DeepEcho(RippleBass(duration: duration)));
        }

        [TestMethod]
        public void FM_RippleBass_Melody2() => new FMTests().FM_RippleBass_Melody2_RunTest();

        void FM_RippleBass_Melody2_RunTest()
        {
            WithAudioLength(bars[4]).Play(() => DeepEcho(RippleBassMelody2, 0.3));
        }

        [TestMethod]
        public void FM_RippleNote_SharpMetallic() => new FMTests().FM_RippleNote_SharpMetallic_RunTest();

        void FM_RippleNote_SharpMetallic_RunTest()
        {
            var duration = _[2.2];
            WithAudioLength(duration);
            Play(() => DeepEcho(RippleNote_SharpMetallic(duration: duration), volume: 0.3));
        }

        [TestMethod]
        public void FM_RippleSound_Clean() => new FMTests().FM_RippleSound_Clean_RunTest();

        void FM_RippleSound_Clean_RunTest()
        {
            var duration = _[4];
            WithAudioLength(duration).Play(() => DeepEcho(RippleSound_Clean(duration: duration)));
        }

        [TestMethod]
        public void FM_RippleSound_FantasyEffect() => new FMTests().FM_RippleSound_FantasyEffect_RunTest();

        void FM_RippleSound_FantasyEffect_RunTest()
        {
            var duration = _[4];
            WithAudioLength(duration).Play(() => DeepEcho(RippleSound_FantasyEffect(duration: duration), volume: 0.33));
        }

        [TestMethod]
        public void FM_RippleSound_CoolDouble() => new FMTests().FM_RippleSound_CoolDouble_RunTest();

        void FM_RippleSound_CoolDouble_RunTest()
        {
            var duration = _[3];
            WithAudioLength(duration).Play(() => DeepEcho(RippleSound_CoolDouble(duration:duration), volume: 0.3));
        }

        [TestMethod]
        public void FM_Noise_Beating() => new FMTests().FM_Noise_Beating_RunTest();

        void FM_Noise_Beating_RunTest()
        {
            var duration = _[5];
            WithAudioLength(duration).Play(() => MildEcho(Create_FM_Noise_Beating(A4, duration), volume: 0.25));
        }

        #endregion

        #region Jingle

        FluentOutlet Jingle()
        {
            var originalAudioLength = AudioLength;
            try
            {
                var fluteVolume      = _[1.2];
                var chordsVolume     = _[0.5];
                var tromboneVolume   = _[0.7];
                var hornVolume       = _[0.6];
                var rippleBassVolume = _[0.7];

                WithAudioLength(bars[4]);

                var pattern1 = WithName("Jingle Pattern 1").ParallelAdd
                (
                    JingleVolume.Value,
                    () => Multiply(fluteVolume, FluteMelody1),
                    () => Multiply(hornVolume,  HornMelody1)
                );

                //Play(() => MildEcho(pattern1), volume: 1);
                
                WithAudioLength(bars[4]);

                var pattern2 = WithName("Jingle Pattern 2").ParallelAdd
                (
                    JingleVolume.Value,
                    () => Multiply(fluteVolume,      FluteMelody2),
                    () => Multiply(tromboneVolume,   TromboneMelody2),
                    () => Multiply(hornVolume,       HornMelody2),
                    () => Multiply(rippleBassVolume, RippleBassMelody2)
                );

                //Play(() => MildEcho(pattern2), volume: 1);

                WithAudioLength(bars[8]);

                var composition = WithName("Composition").ParallelAdd
                (
                    () => pattern1,
                    () => Delay(pattern2, bars[4]),
                    //() => PadChords(chordsVolume)/*.PlayMono(0.3)*/ * JingleVolume
                    // HACK: Not sure why the chords are off, but delaying them for now...
                    () => PadChords(chordsVolume).Delay(beats[4]) * JingleVolume 
                );

                return composition;
            }
            finally
            {
                AudioLength = originalAudioLength;
            }
        }

        #endregion

        #region Melodies

        FluentOutlet FluteMelody1 => WithName().WithAudioLength(bars[4]).ParallelAdd
        (
            () => Flute1(E4, t[bar: 1, beat: 1.0], volume: _[0.80], beats[2.00]),
            () => Flute1(F4, t[bar: 1, beat: 2.5], volume: _[0.70], beats[2.17]),
            () => Flute1(G4, t[bar: 1, beat: 4.0], volume: _[0.60], beats[1.00]),
            () => Flute1(A4, t[bar: 2, beat: 1.0], volume: _[0.80], beats[2.33]),
            () => Flute2(B4, t[bar: 2, beat: 2.5], volume: _[0.50], beats[1.00]),
            () => Flute2(A3, t[bar: 2, beat: 4.0], volume: _[0.50], beats[1.67]),
            () => Flute3(G3, t[bar: 3, beat: 1.0], volume: _[0.85], beats[2.00]),
            () => Flute1(G4, t[bar: 3, beat: 2.5], volume: _[0.80], beats[2.50])
        );

        FluentOutlet FluteMelody2 => WithName().WithAudioLength(bars[4]).ParallelAdd
        (
            () => Flute1(E4, t[bar: 1, beat: 1.0], volume: _[0.59], beats[1.8]),
            () => Flute2(F4, t[bar: 1, beat: 2.5], volume: _[0.68], beats[1.0]),
            () => Flute1(G4, t[bar: 1, beat: 4.0], volume: _[0.74], beats[0.6]),
            () => Flute2(A4, t[bar: 2, beat: 1.0], volume: _[0.82], beats[2.0]),
            () => Flute3(B4, t[bar: 2, beat: 2.5], volume: _[0.74], beats[1.0]),
            () => Flute2(G4, t[bar: 2, beat: 4.0], volume: _[0.90], beats[0.4]),
            () => Flute4(A4, t[bar: 3, beat: 1.0], volume: _[1.00], _[1.66])
        );

        FluentOutlet OrganChords => 
            Multiply
            (
                Stretch(ChordVolumeCurve, bars[1]),
                WithAudioLength(bars[8]).WithName().ParallelAdd
                (
                    () => Organ(bar[0], ChordPitchCurve1.Stretch(bars[1]), duration: bars[8]),
                    () => Organ(bar[0], ChordPitchCurve2.Stretch(bars[1]), duration: bars[8]),
                    () => Organ(bar[0], ChordPitchCurve3.Stretch(bars[1]), duration: bars[8])
                )
            );

        FluentOutlet PadChords(FluentOutlet volume)
            => Multiply
            (
                Stretch(ChordVolumeCurve, bars[1]),
                WithName().WithAudioLength(bars[8]).ParallelAdd
                (
                    volume.Value,
                    () => Pad(bar[0], ChordPitchCurve1.Stretch(bars[1]), duration: bars[8]),
                    () => Pad(bar[0], ChordPitchCurve2.Stretch(bars[1]), duration: bars[8]),
                    () => Pad(bar[0], ChordPitchCurve3.Stretch(bars[1]), duration: bars[8])
                )
            );

        FluentOutlet HornMelody1 => WithAudioLength(beat[13 + 4]).WithName().ParallelAdd
        (
            () => _[ beat[09], Horn(C2, length[3]), 0.7 ],
            () => _[ beat[13], Horn(G1, length[4]), 0.5 ]
        );

        FluentOutlet HornMelody2 => WithAudioLength(beat[9 + 4]).WithName().ParallelAdd
        (
            () => _[ b[1], A2, Horn, 0.75, l[2] ],
            () => _[ b[5], F2, Horn, 0.85, l[2] ],
            () => _[ b[9], A1, Horn, 1.00, l[4] ]
        );
        
        FluentOutlet TromboneMelody1 => WithName().WithAudioLength(beats[6]).ParallelAdd
        (
            () => Trombone(A1 , beat[1]),
            () => Trombone(E2 , beat[3]),
            () => Trombone(Fs1, beat[5], volume: _[0.7])
        );

        internal FluentOutlet TromboneMelody2 => WithName().WithAudioLength(beats[8]).ParallelAdd
        (
            () => Trombone(E4, beat[3], durationFactor: _[1.4]),
            () => Trombone(C4, beat[7], durationFactor: _[1.4])
        );

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
        FluentOutlet Organ(FluentOutlet delay = null, FluentOutlet freq = null, FluentOutlet volume = null, FluentOutlet duration = null)
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
        FluentOutlet Pad(FluentOutlet delay = null, FluentOutlet freq = null, FluentOutlet volume = null, FluentOutlet duration = null)
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
        FluentOutlet Horn(FluentOutlet freq = null, FluentOutlet duration = null)
        {
            freq = freq ?? A2;

            var tamedMod = Multiply(5, Stretch(ModTamingCurve2, duration));

            var fmSignal = FMInHertz(Multiply(freq, 2), freq, tamedMod);
            var envelope = Stretch(BrassCurve, duration);
            var sound    = Multiply(fmSignal, envelope);

            return sound;
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
        /// <param name="duration"> The audioLength of the sound in seconds (default is 2.5). </param>
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

        /// <summary> Shapes a ripple effect sound giving it a volume envelope and a delay, volume and audioLength. </summary>
        /// <param name="duration"> The audioLength of the sound in seconds (default is 2.5). </param>
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
        FluentOutlet Create_FM_Noise_Beating(FluentOutlet pitch = null, FluentOutlet duration = null)
        {
            var signal = FMAroundFreq(pitch ?? A4, _[55], _[0.5]);
            
            var curve = Curve(0.0, 1.00, 0.2, 1.10, 
                              0.4, 0.90, 0.4, 0.90, 
                              0.5, 0.85, 0.6, 0.80, 
                              0.5, 0.80, 0.4, 0.85, 
                              0.2, 0.00);
            
            return signal * curve.Skip(duration / 16).Stretch(duration);
        }

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

        internal FluentOutlet MildEcho(FluentOutlet sound, double? volume = null)
            => EchoParallel(sound, volume ?? DefaultVolume, MildEchoCount, magnitude: _[0.25], MildEchoDelay);

        FluentOutlet DeepEcho(FluentOutlet sound, double? volume = null) 
            => EchoParallel(sound, volume ?? DefaultVolume, DeepEchoCount, magnitude: _[0.5], DeepEchoDelay);
        
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

        (double time, FluentOutlet freq1, FluentOutlet freq2, FluentOutlet freq3)[] _chordFreqs;

        (double time, FluentOutlet freq1, FluentOutlet freq2, FluentOutlet freq3)[] CreateChordFreqs() => new[]
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

        FluentOutlet ChordPitchCurve1 => WithName().Curve(
            _chordFreqs.Select(x => new NodeInfo(x.time,
                                                       x.freq1.Value,
                                                       NodeTypeEnum.Block)).ToArray());

        FluentOutlet ChordPitchCurve2 => WithName().Curve(
            _chordFreqs.Select(x => new NodeInfo(x.time, 
                                                       x.freq2.Value,
                                                       NodeTypeEnum.Block)).ToArray());

        FluentOutlet ChordPitchCurve3 => WithName().Curve(
            _chordFreqs.Select(x => new NodeInfo(x.time, 
                                                       x.freq3.Value, 
                                                       NodeTypeEnum.Block)).ToArray());

        #endregion
    }
}