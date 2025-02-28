using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Business.Synthesizer.Tests.docs;
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ExplicitCallerInfoArgument

namespace JJ.Business.Synthesizer.Tests.Functional
{
    internal static class EchoExtensions
    {
        public static FlowNode MildEcho(this FlowNode sound)
            => sound.Echo(count: 6, magnitude: 0.25, delay: 0.33)
                    .AddEchoDuration(count: 6, delay: 0.33);

        public static FlowNode DeepEcho(this FlowNode sound) 
            => sound.Echo(count: 7, magnitude: 0.50, delay: 0.50)
                    .AddEchoDuration(count: 7, delay: 0.50);
    }
    
    /// <inheritdoc cref="_fmtests"/>
    [TestClass]
    [TestCategory("Functional")]
    public class FMTests : MySynthWishes
    {
        
        /// <inheritdoc cref="_fmtests"/>
        public FMTests()
        {
            WithMono();
            WithBeatLength(0.45);
            WithBarLength(4 * 0.45);
            _chordFreqs = CreateChordFreqs();
        }

        // Tests

        [TestMethod, TestCategory("Long")] public void FM_Jingle_Test() => Run(FM_Jingle);
        void FM_Jingle() => _[Jingle].DeepEcho().Volume(0.2).Save().Play();
        
        [TestMethod] public void FM_Flute_Melody1_Test() => Run(FM_Flute_Melody1);
        void FM_Flute_Melody1() => (FluteMelody1.MildEcho() * 0.5).Save().Play();
        
        [TestMethod] public void FM_Flute_Melody2_Test() => Run(FM_Flute_Melody2);
        void FM_Flute_Melody2() => (FluteMelody2.MildEcho() * 0.5).Save().Play();
        
        [TestMethod] public void FM_Flute1_Test() => Run(FM_Flute1);
        void FM_Flute1() => (Flute1(E4).MildEcho() * 0.5).Save().Play();
        
        [TestMethod] public void FM_Flute2_Test() => Run(FM_Flute2);
        void FM_Flute2() => (Flute2(F4).MildEcho() * 0.5).Save().Play();
        
        [TestMethod] public void FM_Flute3_Test() => Run(FM_Flute3);
        void FM_Flute3() => (Flute3(G4).MildEcho() * 0.5).Save().Play();
        
        [TestMethod] public void FM_Flute4_Test() => Run(FM_Flute4);
        void FM_Flute4() => (Flute4(A4).MildEcho() * 0.5).Save().Play();
        
        [TestMethod] public void FM_Organ_Test() => Run(FM_Organ);
        void FM_Organ()
        {
            var duration = bars[3];
            WithAudioLength(duration);
            Organ().Curve(RecorderCurve.Stretch(duration)).Volume(0.5).MildEcho().Save().Play();
        }
        
        [TestMethod] public void FM_Organ_Chords_Test() => Run(FM_Organ_Chords);
        void FM_Organ_Chords() => Save(OrganChords.MildEcho() * 0.2).Play();
        
        [TestMethod] public void FM_Organ_Chords2_Test() => Run(FM_Organ_Chords2);
        void FM_Organ_Chords2() => Save(OrganChords2.MildEcho() * 0.2).Play();
        
        [TestMethod] public void FM_Pad_Test() => Run(FM_Pad);
        void FM_Pad()
        {
            var duration = bars[3];
            WithAudioLength(duration).Fluent(Pad() * RecorderCurve.Stretch(duration) * 0.5).MildEcho().Save().Play();
        }

        [TestMethod] public void FM_Pad_Chords_Test() => Run(FM_Pad_Chords);
        void FM_Pad_Chords() => Fluent(PadChords()).MildEcho().Volume(0.14).Save().Play();
        
        [TestMethod] public void FM_Pad_Chords2_Test() => Run(FM_Pad_Chords2);
        void FM_Pad_Chords2() => Save(PadChords2().MildEcho() * 0.14).Play();
        
        [TestMethod] public void FM_Distortion_Chords_Test() => Run(FM_Distortion_Chords);
        void FM_Distortion_Chords() => With16Bit()[DistortionChords, 0.92].Times(0.15).MildEcho().Save().Play();
        
        /// <inheritdoc cref="_horn" />
        [TestMethod] public void FM_Horn_Test() => Run(FM_Horn);
        /// <inheritdoc cref="_horn" />
        void FM_Horn() => Horn().MildEcho().Volume(0.5).Save().Play();
        
        /// <inheritdoc cref="_horn" />
        [TestMethod] public void FM_Horn_Melody1_Test() => Run(FM_Horn_Melody1);
        /// <inheritdoc cref="_horn" />
        void FM_Horn_Melody1() => (HornMelody1.MildEcho() * 0.5).Save().Play();
        
        /// <inheritdoc cref="_horn" />
        [TestMethod] public void FM_Horn_Melody2_Test() => Run(FM_Horn_Melody2);
        /// <inheritdoc cref="_horn" />
        void FM_Horn_Melody2() => HornMelody2.MildEcho().Volume(0.5).Save().Play();
        
        /// <inheritdoc cref="_trombone" />
        [TestMethod] public void FM_Trombone_Test() => Run(FM_Trombone);
        /// <inheritdoc cref="_trombone" />
        void FM_Trombone() => Save(Trombone(E2).MildEcho() * 0.5).Play();
        
        /// <inheritdoc cref="_trombone" />
        [TestMethod] public void FM_Trombone_Melody1_Test() => Run(FM_Trombone_Melody1);
        /// <inheritdoc cref="_trombone" />
        void FM_Trombone_Melody1() => (TromboneMelody1.MildEcho() * 0.5).Save().Play();
        
        /// <inheritdoc cref="_trombone" />
        [TestMethod] public void FM_Trombone_Melody2_Test() => Run(FM_Trombone_Melody2);
        /// <inheritdoc cref="_trombone" />
        void FM_Trombone_Melody2() => (TromboneMelody2.MildEcho() * 0.75).Save().Play();
        
        [TestMethod] public void FM_ElectricNote_Test() => Run(FM_ElectricNote);
        void FM_ElectricNote() => _[ElectricNote, _, 1.5].MildEcho().Volume(0.2).Save().Play();
        
        [TestMethod] public void FM_RippleBass_Test() => Run(FM_RippleBass);
        void FM_RippleBass() => WithNoteLength(3)[RippleBass].DeepEcho().Volume(0.5).Save().Play();
        
        [TestMethod] public void FM_RippleBass_Melody2_Test() => Run(FM_RippleBass_Melody2);
        void FM_RippleBass_Melody2() => _[RippleBassMelody2].DeepEcho().Volume(0.33).Save().Play();
        
        [TestMethod] public void FM_RippleNote_SharpMetallic_Test() => Run(FM_RippleNote_SharpMetallic);
        void FM_RippleNote_SharpMetallic() => WithAudioPlayback(false).A3[RippleNote_SharpMetallic, _[2.2]].DeepEcho().Volume(0.33).Save().Play();
        
        [TestMethod] public void FM_RippleSound_Clean_Test() => Run(FM_RippleSound_Clean);
        void FM_RippleSound_Clean() => Fluent(RippleSound_Clean(_, _[4])).DeepEcho().Volume(0.5).Save().Play();
        
        [TestMethod] public void FM_RippleSound_FantasyEffect_Test() => Run(FM_RippleSound_FantasyEffect);
        void FM_RippleSound_FantasyEffect() => _[RippleSound_FantasyEffect(A5, _[4])].DeepEcho().Volume(0.33).Save().Play();
        
        [TestMethod] public void FM_RippleSound_CoolDouble_Test() => Run(FM_RippleSound_CoolDouble);
        void FM_RippleSound_CoolDouble() => _[RippleSound_CoolDouble, A5, 3].DeepEcho().Volume(0.33).Save().Play();
        
        [TestMethod] public void FM_Noise_Beating_Test() => Run(FM_Noise_Beating);
        void FM_Noise_Beating() => WithAudioPlayback(false) [A4, Create_FM_Noise_Beating, vol: 1, len: _[5]].MildEcho().Volume(0.25).Save().Play();
        
        // Jingle

        FlowNode Jingle()
        {
            var fluteVolume      = 1.2;
            var chordsVolume     = 0.55;
            var tromboneVolume   = 0.7;
            var hornVolume       = 0.6;
            var rippleBassVolume = 0.7;

            FlowNode pattern1() => Add
            (
                FluteMelody1 * fluteVolume,
                HornMelody1  * hornVolume
            ).SetName("Jingle Pattern 1");

            FlowNode pattern2() => Add
            (
                FluteMelody2      * fluteVolume,
                HornMelody2       * hornVolume,
                TromboneMelody2   * tromboneVolume,
                RippleBassMelody2 * rippleBassVolume
            ).SetName("Jingle Pattern 2");
            
            var jingle = Add
            (
                _[ bar[1], PadChords, chordsVolume, len: bars[8] ],
                _[ bar[1], pattern1,                len: bars[4] ],
                _[ bar[5], pattern2,                len: bars[4] ]
            ).SetName("Jingle");
            
            EnsureAudioLength(bars[8]);
            
            return jingle;
        }

        // Melodies
        
        FlowNode FluteMelody1 => _
        [ t[1,1.0], E4, Flute1, 0.80, l[2.00] ]
        [ t[1,2.5], F4, Flute1, 0.70, l[2.17] ]
        [ t[1,4.0], G4, Flute1, 0.60, l[1.00] ]
        [ t[2,1.0], A4, Flute1, 0.80, l[2.33] ]
        [ t[2,2.5], B4, Flute2, 0.50, l[1.00] ]
        [ t[2,4.0], A3, Flute2, 0.50, l[1.67] ]
        [ t[3,1.0], G3, Flute3, 0.85, l[2.00] ]
        [ t[3,2.5], G4, Flute1, 0.80, l[2.50] ];

        FlowNode FluteMelody2 => _
        [ t[1,1.0], E4, Flute1, 0.59, l[1.8]  ]
        [ t[1,2.5], F4, Flute2, 0.68, l[1.0]  ]
        [ t[1,4.0], G4, Flute1, 0.74, l[0.6]  ]
        [ t[2,1.0], A4, Flute2, 0.82, l[2.0]  ]
        [ t[2,2.5], B4, Flute3, 0.74, l[1.0]  ]
        [ t[2,4.0], G4, Flute2, 0.90, l[0.4]  ]
        [ t[3,1.0], A4, Flute4, 1.00, _[1.66] ];
        
        FlowNode OrganChords => _
        [ ChordPitchCurve1.Stretch(bars[1]), Organ, ChordVolumeCurve, bars[8] ]
        [ ChordPitchCurve2.Stretch(bars[1]), Organ, ChordVolumeCurve, bars[8] ]
        [ ChordPitchCurve3.Stretch(bars[1]), Organ, ChordVolumeCurve, bars[8] ];

        FlowNode OrganChords2 =>
            EnsureAudioLength(bars[8]).Multiply
            (
                Stretch(ChordVolumeCurve, bars[1]),
                Add
                (
                    Organ2(_[0], ChordPitchCurve1.Stretch(bars[1]), duration: bars[8]),
                    Organ2(_[0], ChordPitchCurve2.Stretch(bars[1]), duration: bars[8]),
                    Organ2(_[0], ChordPitchCurve3.Stretch(bars[1]), duration: bars[8])
                )
            ).SetName();
        
        FlowNode PadChords() => _
        [ ChordPitchCurve1.Stretch(bars[1]), Pad, SpeedUp(ChordVolumeCurve, 8).Delay(1/8d), len: bars[8] ]
        [ ChordPitchCurve2.Stretch(bars[1]), Pad, SpeedUp(ChordVolumeCurve, 8).Delay(1/8d), len: bars[8] ]
        [ ChordPitchCurve3.Stretch(bars[1]), Pad, SpeedUp(ChordVolumeCurve, 8).Delay(1/8d), len: bars[8] ];
        
        FlowNode PadChords2() => _
        [ ChordPitchCurve1.Stretch(bars[1]), Pad, SpeedUp(ChordVolumeCurve.Delay(1), 8), len: bars[8] ]
        [ ChordPitchCurve2.Stretch(bars[1]), Pad, SpeedUp(ChordVolumeCurve.Delay(1), 8), len: bars[8] ]
        [ ChordPitchCurve3.Stretch(bars[1]), Pad, SpeedUp(ChordVolumeCurve.Delay(1), 8), len: bars[8] ];
        
        /// <param name="volume">Used to promote clipping for distortion (only works for 16-bit, not 32-bit).</param>
        FlowNode DistortionChords(FlowNode volume = null)
        {
            return Multiply
            (
                Stretch(ChordVolumeCurve, bars[1]),
                Add
                (
                    DistortedNote(bar[0], ChordPitchCurve1.Stretch(bars[1]), duration: bars[8]).Volume(volume),
                    DistortedNote(bar[0], ChordPitchCurve2.Stretch(bars[1]), duration: bars[8]).Volume(volume),
                    DistortedNote(bar[0], ChordPitchCurve3.Stretch(bars[1]), duration: bars[8]).Volume(volume)
                ).EnsureAudioLength(bars[8]).SetName()
            );
        }
        
        /// <inheritdoc cref="_horn" />
        FlowNode HornMelody1 => _
        [ beat[09], C2, Horn, 0.7, length[3] ]
        [ beat[13], G1, Horn, 0.5, length[4] ];

        /// <inheritdoc cref="_horn" />
        FlowNode HornMelody2 => _
        [ b[1], A2, Horn, 0.75, l[2] ]
        [ b[5], F2, Horn, 0.85, l[2] ]
        [ b[9], A1, Horn, 1.00, l[4] ];
        
        /// <inheritdoc cref="_trombone" />
        FlowNode TromboneMelody1 => Add(
        Trombone(A1 ).Volume(1.0).Tape(3).Delay(b[1]),
        Trombone(E2 ).Volume(1.0).Tape(3).Delay(b[3]),
        Trombone(Fs1).Volume(0.7).Tape(3).Delay(b[5])
        ).EnsureAudioLength(b[5] + 3);

        /// <inheritdoc cref="_trombone" />
        FlowNode TromboneMelody2 => _
        [ beat[3], E4, Trombone, 1, _[1.4] ]
        [ beat[7], C4, Trombone, 1, _[1.4] ];

        FlowNode RippleBassMelody2 => _[ bar[3.5], A1, RippleBass, 1, bars[0.8] ];
        
        // Instruments

        /// <inheritdoc cref="_flute1" />
        FlowNode Flute1(FlowNode freq = null, FlowNode duration = null)
        {
            freq = freq ?? A4;

            var fmSignal = FMAround0(Divide(freq, 2), freq, _[0.005]);
            var envelope = Stretch(FluteCurve, duration);
            var note     = Multiply(fmSignal, envelope);
            
            return note.SetName();
        }

        /// <inheritdoc cref="_flute2" />
        FlowNode Flute2(FlowNode freq = null, FlowNode duration = null)
        {
            freq = freq ?? A4;

            var fmSignal       = FMAroundFreq(freq, Multiply(freq, 2), _[0.005]);
            var envelope       = Stretch(FluteCurve, duration);
            var modulatedSound = Multiply(fmSignal, envelope);
            var adjustedVolume = Multiply(modulatedSound, 0.85);

            return adjustedVolume.SetName();
        }

        /// <inheritdoc cref="_flute3" />
        FlowNode Flute3(FlowNode freq = null, FlowNode duration = null)
        {
            freq   = freq ?? A4;

            var fmSignal       = FMAroundFreq(freq, Multiply(freq, 4), _[0.005]);
            var envelope       = Stretch(FluteCurve, duration);
            var sound          = Multiply(fmSignal, envelope);
            var adjustedVolume = Multiply(sound, 0.8);

            return adjustedVolume.SetName();
        }

        /// <inheritdoc cref="_flute4" />
        FlowNode Flute4(FlowNode freq = null, FlowNode duration = null)
        {
            freq   = freq ?? A4;

            var fmSignal       = FMAround0(Multiply(freq, 2), freq, _[0.005]);
            var envelope       = Stretch(FluteCurve, duration);
            var sound          = Multiply(fmSignal, envelope);
            var adjustedVolume = Multiply(sound, 0.70);

            return adjustedVolume.SetName();
        }

        /// <inheritdoc cref="_default" />
        FlowNode Organ(FlowNode freq = null, FlowNode duration = null)
        {
            freq     = freq ?? A4;
            duration = duration ?? GetAudioLength;

            var modCurve = Stretch(ModTamingCurve, duration);
            var modDepth = Multiply(0.0001, modCurve);
            var fmSignal = FMAroundFreq(freq, Multiply(freq, 2), modDepth);

            var volumeEvenOutCurve  = Stretch(EvenOutCurve, duration);
            var soundWithEvenVolume = Multiply(fmSignal, volumeEvenOutCurve);

            return soundWithEvenVolume.SetName();
        }
        
        /// <inheritdoc cref="_default" />
        FlowNode Organ2(FlowNode delay = null, FlowNode freq = null, FlowNode volume = null, FlowNode duration = null)
        {
            freq     = freq ?? A4;
            duration = duration ?? GetAudioLength;
            
            var modCurve = Stretch(ModTamingCurve, duration);
            var modDepth = Multiply(0.0001, modCurve);
            var fmSignal = FMAroundFreq(freq, Multiply(freq, 2), modDepth);
            
            var volumeEvenOutCurve  = Stretch(EvenOutCurve, duration);
            var soundWithEvenVolume = Multiply(fmSignal, volumeEvenOutCurve);
            
            var note = Note(soundWithEvenVolume, delay, volume, duration);
            
            return note.SetName();
        }

        /// <inheritdoc cref="_default" />
        FlowNode Pad(FlowNode freq = null, FlowNode duration = null)
        {
            freq     = freq ?? A4;
            duration = duration ?? GetAudioLength;

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

            return soundWithEvenVolume.SetName();
        }
        
        /// <inheritdoc cref="_default" />
        FlowNode DistortedNote(FlowNode delay = null, FlowNode freq = null, FlowNode volume = null, FlowNode duration = null)
        {
            freq     = freq ?? A4;
            duration = duration ?? GetAudioLength;
            
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
            
            var note = Note(soundWithEvenVolume, delay, volume, duration);
            
            return note.SetName();
        }

        /// <inheritdoc cref="_horn" />
        FlowNode Horn(FlowNode freq = null, FlowNode duration = null)
        {
            freq = freq ?? A2;

            var tamedMod = Multiply(5, Stretch(ModTamingCurve2, duration));

            var fmSignal = FMInHertz(Multiply(freq, 2), freq, tamedMod);
            var envelope = Stretch(BrassCurve, duration);
            var sound    = Multiply(fmSignal, envelope);

            return sound.SetName();
        }

        /// <inheritdoc cref="_trombone" />
        FlowNode Trombone(FlowNode freq = null, FlowNode durationFactor = null)
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

            return sound.SetName();
        }

        /// <inheritdoc cref="_default" />
        FlowNode ElectricNote(FlowNode freq = null, FlowNode duration = null)
        {
            freq   = freq ?? A4;
            duration = duration ?? GetAudioLength;

            var modDepth = 0.02 * Stretch(LineDownCurve, duration);
            var fmSignal = Add
            (
                FMAroundFreq(freq, freq * 1.5, modDepth),
                FMAroundFreq(freq, freq * 2.0, modDepth)
            );

            var modulatedSound = fmSignal.Curve(DampedBlockCurve.Stretch(duration));
            var adjustedVolume = modulatedSound * 0.6;
            
            EnsureAudioLength(duration);
            
            return adjustedVolume.SetName();
        }

        /// <inheritdoc cref="_ripplebass" />
        FlowNode RippleBass(FlowNode freq = null, FlowNode duration = null)
        {
            freq = freq ?? A1;
            var fmSignal = FMAroundFreq(freq * 8, freq / 2, _[0.005]);
            var note     = ShapeRippleSound(fmSignal, duration);
            return note.SetName();
        }

        /// <inheritdoc cref="_ripplenotesharpmetallic" />
        FlowNode RippleNote_SharpMetallic(FlowNode freq = null, FlowNode duration = null)
        {
            freq = freq ?? A3;
            var fmSignal = FMInHertz(freq, freq / 2, _[10]);
            var sound    = ShapeRippleSound(fmSignal, duration);
            return sound.SetName();
        }

        /// <inheritdoc cref="_ripplesoundclean" />
        FlowNode RippleSound_Clean(FlowNode freq = null, FlowNode duration = null)
        {
            freq = freq ?? A4;
            var fmSignal = FMAroundFreq(freq, _[20], _[0.005]);
            var sound    = ShapeRippleSound(fmSignal, duration);
            return sound.SetName();
        }

        /// <inheritdoc cref="_ripplesoundfantasyeffect" />
        FlowNode RippleSound_FantasyEffect(FlowNode freq = null, FlowNode duration = null)
        {
            freq = freq ?? A5;
            var fmSignal = FMAroundFreq(freq, _[10], _[0.02]);
            var sound    = ShapeRippleSound(fmSignal, duration);
            return sound.SetName();
        }

        /// <inheritdoc cref="_ripplesoundcooldouble" />
        FlowNode RippleSound_CoolDouble(FlowNode freq = null, FlowNode duration = null)
        {
            freq = freq ?? A5;
            var fmSignal = FMAroundFreq(freq, _[10], _[0.05]);
            var sound    = ShapeRippleSound(fmSignal, duration);
            return sound.SetName();
        }

        /// <inheritdoc cref="_shaperipplesound" />
        FlowNode ShapeRippleSound(FlowNode input, FlowNode duration)
        {
            duration = GetNoteLength(duration);
            var envelope = Stretch(RippleCurve, duration);
            var sound    = input * envelope;
            
            return sound.EnsureAudioLength(duration).SetName();
        }

        /// <inheritdoc cref="_createfmnoisebeating" />
        FlowNode Create_FM_Noise_Beating(FlowNode pitch = null, FlowNode duration = null)
        {
            duration = duration ?? GetAudioLength;
            
            var signal = FMAroundFreq(pitch ?? A4, _[55], _[0.5]);
            
            var curve = Curve(0.0, 1.00, 0.2, 1.10, 
                              0.4, 0.90, 0.4, 0.90, 
                              0.5, 0.85, 0.6, 0.80, 
                              0.5, 0.80, 0.4, 0.85, 
                              0.2, 0.00);

            var shaped = signal * curve.Skip(duration / 16).Stretch(duration);
            
            return shaped.SetName();
        }

        // Algorithms
        
        /// <inheritdoc cref="_fminhertz" />
        FlowNode FMInHertz(FlowNode soundFreq, FlowNode modSpeed, FlowNode modDepth)
        {
            var modulator = Sine(modSpeed) * modDepth;
            var sound     = Sine(soundFreq + modulator);
            return sound.SetName();
        }

        /// <inheritdoc cref="_fmaround0" />
        FlowNode FMAround0(FlowNode soundFreq, FlowNode modSpeed, FlowNode modDepth)
        {
            var modulator = Sine(modSpeed) * modDepth;
            var sound     = Sine(soundFreq * modulator);
            return sound.SetName();
        }

        /// <inheritdoc cref="_fmaroundfreq" />
        FlowNode FMAroundFreq(FlowNode soundFreq, FlowNode modSpeed, FlowNode modDepth)
        {
            var modulator = 1 + Sine(modSpeed) * modDepth;
            var sound     = Sine(soundFreq * modulator);
            return sound.SetName();
        }
        
        // Curves

        FlowNode FluteCurve => Curve
        (
            (time: 0.00, value: 0.0),
            (time: 0.05, value: 0.8),
            (time: 0.10, value: 1.0),
            (time: 0.90, value: 0.7),
            (time: 1.00, value: 0.0)
        ).SetName();

        FlowNode BrassCurve => Curve
        (
            (time: 0.00, value: 0),
            (time: 0.07, value: 1),
            (time: 0.93, value: 1),
            (time: 1.00, value: 0)
        ).SetName();

        FlowNode RippleCurve => Curve
        (
            (time: 0.00, value: 0.00),
            (time: 0.01, value: 0.75),
            (time: 0.05, value: 0.50),
            (time: 0.25, value: 1.00),
            (time: 1.00, value: 0.00)
        ).SetName();

        FlowNode DampedBlockCurve => Curve
        (
            (time: 0.00, value: 0),
            (time: 0.01, value: 1),
            (time: 0.99, value: 1),
            (time: 1.00, value: 0)
        ).SetName();

        FlowNode LineDownCurve => Curve
        (
            (time: 0, value: 1),
            (time: 1, value: 0)
        ).SetName();

        /// <inheritdoc cref="_modtamingcurve"/>
        FlowNode ModTamingCurve => Curve(0.3, 1.0, 0.3, 0.0).SetName();

        /// <inheritdoc cref="_modtamingcurve"/>
        FlowNode ModTamingCurve2 => Curve(1.0, 0.5, 0.2, 0.0).SetName();

        /// <inheritdoc cref="_modtamingcurve"/>
        FlowNode ModTamingCurve8Times => Curve
        (
            0.3, 1.0, 0.3, 0.0,
            0.3, 1.0, 0.3, 0.0,
            0.3, 1.0, 0.3, 0.0,
            0.3, 1.0, 0.3, 0.0,
            0.3, 1.0, 0.3, 0.0,
            0.3, 1.0, 0.3, 0.0,
            0.3, 1.0, 0.3, 0.0,
            0.3, 1.0, 0.3, 0.0
        ).SetName();
            
        /// <inheritdoc cref="_evenoutcurve"/>
        FlowNode EvenOutCurve => Curve
        (
            (time: 0.00, value: 1.0),
            (time: 0.33, value: 0.6),
            (time: 0.50, value: 0.6),
            (time: 0.75, value: 0.8),
            (time: 1.00, value: 1.0)
        ).SetName();

        FlowNode ChordVolumeCurve => Curve
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
        ).SetName();

        (double time, FlowNode freq1, FlowNode freq2, FlowNode freq3)[] _chordFreqs;

        (double time, FlowNode freq1, FlowNode freq2, FlowNode freq3)[] CreateChordFreqs() => new[]
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

        FlowNode ChordPitchCurve1 => Curve(
            _chordFreqs.Select(x => new NodeInfo(x.time,
                                                 x.freq1.Value,
                                                 NodeTypeEnum.Block)).ToList());

        FlowNode ChordPitchCurve2 => Curve(
            _chordFreqs.Select(x => new NodeInfo(x.time,
                                                 x.freq2.Value,
                                                 NodeTypeEnum.Block)).ToList());

        FlowNode ChordPitchCurve3 => Curve(
            _chordFreqs.Select(x => new NodeInfo(x.time,
                                                 x.freq3.Value,
                                                 NodeTypeEnum.Block)).ToList());
    }
}