using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Wishes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ExplicitCallerInfoArgument

namespace JJ.Business.Synthesizer.Tests.Functional
{
    /// <inheritdoc cref="docs._fmtests"/>
    [TestClass]
    [TestCategory("Functional")]
    public class FMTests : SynthWishes
    {
        FlowNode MildEcho(FlowNode sound)
            => Echo(sound, count: 6, magnitude: _[0.25], delay: _[0.33]).AddAudioLength(MildEchoDuration);

        FlowNode MildEchoDuration 
            => EchoDuration(count: 6, delay: _[0.33]);

        FlowNode DeepEcho(FlowNode sound) 
            => Echo(sound, count: 7, magnitude: _[0.50], delay: _[0.50]).AddAudioLength(DeepEchoDuration);

        FlowNode DeepEchoDuration 
            => EchoDuration(count: 7, delay: _[0.50]);
        
        /// <inheritdoc cref="docs._fmtests"/>
        public FMTests() : base(beat: 0.45, bar: 4 * 0.45) 
        {
            WithMono();
            _chordFreqs = CreateChordFreqs();
        }

        // Tests

        [TestMethod]
        public void FM_Jingle() => new FMTests().FM_Jingle_RunTest();

        void FM_Jingle_RunTest()
        {
            Save(() => DeepEcho(Jingle()) * 0.2).Play();
        }
        
        [TestMethod]
        public void FM_Flute_Melody1() => new FMTests().FM_Flute_Melody1_RunTest();

        void FM_Flute_Melody1_RunTest()
        {
            WithPlayBack();
            var result = Save(() => MildEcho(FluteMelody1) * 0.5);
            this.Play(result);
        }

        [TestMethod]
        public void FM_Flute_Melody2() => new FMTests().FM_Flute_Melody2_RunTest();

        void FM_Flute_Melody2_RunTest()
        {
            Save(() => MildEcho(FluteMelody2) * 0.5).Play();
        }

        [TestMethod]
        public void FM_Flute1() => new FMTests().FM_Flute1_RunTest();

        void FM_Flute1_RunTest()
        {
            Save(() => MildEcho(Flute1(E4)) * 0.5).Play();
        }

        [TestMethod]
        public void FM_Flute2() => new FMTests().FM_Flute2_RunTest();

        void FM_Flute2_RunTest()
        {
            Save(() => MildEcho(Flute2(F4)) * 0.5).Play();
        }

        [TestMethod]
        public void FM_Flute3() => new FMTests().FM_Flute3_RunTest();

        void FM_Flute3_RunTest()
        {
            Save(() => MildEcho(Flute3(G4)) * 0.5).Play();
        }

        [TestMethod]
        public void FM_Flute4() => new FMTests().FM_Flute4_RunTest();

        void FM_Flute4_RunTest()
        {
            Save(() => MildEcho(Flute4(A4)) * 0.5).Play();
        }

        [TestMethod]
        public void FM_Organ() => new FMTests().FM_Organ_RunTest();

        void FM_Organ_RunTest()
        {
            var duration = bars[3];
            WithAudioLength(duration + MildEchoDuration).Save(
                () => MildEcho(Organ(duration: duration) * RecorderEnvelope.Stretch(duration) * 0.5)).Play();
        }
        
        [TestMethod]
        public void FM_Organ_Chords() => new FMTests().FM_Organ_Chords_RunTest();
        
        void FM_Organ_Chords_RunTest()
        {
            WithAudioLength(bars[8] + MildEchoDuration).Save(() => MildEcho(OrganChords) * 0.2).Play();
        }
        
        [TestMethod]
        public void FM_Organ_Chords2() => new FMTests().FM_Organ_Chords2_RunTest();
        
        void FM_Organ_Chords2_RunTest()
        {
            Save(() => MildEcho(OrganChords2) * 0.2).Play();
        }
        
        [TestMethod]
        public void FM_Pad() => new FMTests().FM_Pad_RunTest();
        
        void FM_Pad_RunTest()
        {
            var duration = bars[3];
            WithAudioLength(duration + MildEchoDuration).Save(
                () => MildEcho(Pad(duration: duration) * RecorderEnvelope.Stretch(duration) * 0.5)).Play();
        }

        [TestMethod]
        public void FM_Pad_Chords() => new FMTests().FM_Pad_Chords_RunTest();

        void FM_Pad_Chords_RunTest()
        {
            WithAudioLength(bars[8] + MildEchoDuration).Save(() => MildEcho(PadChords()) * 0.14).Play();
        }
        
        [TestMethod]
        public void FM_Distortion_Chords() => new FMTests().FM_Distortion_Chords_RunTest();
        
        void FM_Distortion_Chords_RunTest()
        {
            With16Bit().Save(() => MildEcho(DistortionChords(volume: _[0.92]) * 0.15)).Play();
        }

        /// <inheritdoc cref="docs._horn" />
        [TestMethod]
        public void FM_Horn() => new FMTests().FM_Horn_RunTest();

        /// <inheritdoc cref="docs._horn" />
        void FM_Horn_RunTest()
        {
            Save(() => MildEcho(Horn()) * 0.5).Play();
        }

        /// <inheritdoc cref="docs._horn" />
        [TestMethod]
        public void FM_Horn_Melody1() => new FMTests().FM_Horn_Melody1_RunTest();

        /// <inheritdoc cref="docs._horn" />
        void FM_Horn_Melody1_RunTest()
        {
            Save(() => MildEcho(HornMelody1) * 0.5).Play();
        }
        
        /// <inheritdoc cref="docs._horn" />
        [TestMethod]
        public void FM_Horn_Melody2() => new FMTests().FM_Horn_Melody2_RunTest();

        /// <inheritdoc cref="docs._horn" />
        void FM_Horn_Melody2_RunTest()
        {
            Save(() => MildEcho(HornMelody2).AddAudioLength(MildEchoDuration) * 0.5).Play();
        }
        
        /// <inheritdoc cref="docs._trombone" />
        [TestMethod]
        public void FM_Trombone() => new FMTests().FM_Trombone_RunTest();

        /// <inheritdoc cref="docs._trombone" />
        void FM_Trombone_RunTest()
        {
            // TODO: Output is > 3 sec. Why not 1 + MildEchoDuration?
            WithAudioLength(1).Save(() => MildEcho(Trombone(E2)) * 0.5).Play();
        }
        
        /// <inheritdoc cref="docs._trombone" />
        [TestMethod]
        public void FM_Trombone_Melody1() => new FMTests().FM_Trombone_Melody1_RunTest();

        /// <inheritdoc cref="docs._trombone" />
        void FM_Trombone_Melody1_RunTest()
        {
            Save(() => MildEcho(TromboneMelody1) * 0.5).Play();
        }

        /// <inheritdoc cref="docs._trombone" />
        [TestMethod]
        public void FM_Trombone_Melody2() => new FMTests().FM_Trombone_Melody2_RunTest();

        /// <inheritdoc cref="docs._trombone" />
        void FM_Trombone_Melody2_RunTest()
        {
            Save(() => MildEcho(TromboneMelody2) * 0.75).Play();
        }

        [TestMethod]
        public void FM_ElectricNote() => new FMTests().FM_ElectricNote_RunTest();

        void FM_ElectricNote_RunTest()
        {
            WithAudioLength(1.5).Save(() => MildEcho(ElectricNote()) * 0.2).Play();
        }

        [TestMethod]
        public void FM_RippleBass() => new FMTests().FM_RippleBass_RunTest();

        void FM_RippleBass_RunTest()
        {
            WithAudioLength(3).Save(() => DeepEcho(RippleBass()) * 0.5).Play();
        }

        [TestMethod]
        public void FM_RippleBass_Melody2() => new FMTests().FM_RippleBass_Melody2_RunTest();

        void FM_RippleBass_Melody2_RunTest()
        {
            WithAudioLength(bars[4]).Save(() => DeepEcho(RippleBassMelody2) * 0.33).Play();
        }

        [TestMethod]
        public void FM_RippleNote_SharpMetallic() => new FMTests().FM_RippleNote_SharpMetallic_RunTest();

        void FM_RippleNote_SharpMetallic_RunTest()
        {
            WithAudioLength(2.2).Save(() => DeepEcho(RippleNote_SharpMetallic()) * 0.33).Play();
        }

        [TestMethod]
        public void FM_RippleSound_Clean() => new FMTests().FM_RippleSound_Clean_RunTest();

        void FM_RippleSound_Clean_RunTest()
        {
            WithAudioLength(4).Save(() => DeepEcho(RippleSound_Clean()) * 0.5).Play();
        }

        [TestMethod]
        public void FM_RippleSound_FantasyEffect() => new FMTests().FM_RippleSound_FantasyEffect_RunTest();

        void FM_RippleSound_FantasyEffect_RunTest()
        {
            WithAudioLength(4).Save(() => DeepEcho(RippleSound_FantasyEffect()) * 0.33).Play();
        }

        [TestMethod]
        public void FM_RippleSound_CoolDouble() => new FMTests().FM_RippleSound_CoolDouble_RunTest();

        void FM_RippleSound_CoolDouble_RunTest()
        {
            WithAudioLength(3).Save(() => DeepEcho(RippleSound_CoolDouble()) * 0.33).Play();
        }

        [TestMethod]
        public void FM_Noise_Beating() => new FMTests().FM_Noise_Beating_RunTest();

        void FM_Noise_Beating_RunTest()
        {
            WithAudioLength(5).Save(() => MildEcho(Create_FM_Noise_Beating(A4)) * 0.25).Play();
        }

        // Jingle

        FlowNode Jingle()
        {
            var fluteVolume      = _[1.2];
            var chordsVolume     = _[0.55];
            var tromboneVolume   = _[0.7];
            var hornVolume       = _[0.6];
            var rippleBassVolume = _[0.7];

            WithAudioLength(bars[4]);

            var pattern1 = Add
            (
                Multiply(fluteVolume, FluteMelody1),
                Multiply(hornVolume,  HornMelody1 )
            ).SetName("Jingle Pattern 1");

            WithAudioLength(bars[4]);
            
            var pattern2 = Add
            (
                Multiply(fluteVolume,      FluteMelody2     ),
                Multiply(tromboneVolume,   TromboneMelody2  ),
                Multiply(hornVolume,       HornMelody2      ),
                Multiply(rippleBassVolume, RippleBassMelody2)
            ).SetName("Jingle Pattern 2");

            WithAudioLength(bars[8]);

            var jingle = Add
            (
                pattern1.Tape(),
                Delay(pattern2, bars[4]).Tape(),
                PadChords() * chordsVolume
            ).SetName("Jingle");

            return jingle;
        }

        // Melodies
        
        FlowNode FluteMelody1 => WithAudioLength(bars[4]).Add
        (
            _[ t[1, 1.0], E4, Flute1, 0.80, l[2.00] ],
            _[ t[1, 2.5], F4, Flute1, 0.70, l[2.17] ],
            _[ t[1, 4.0], G4, Flute1, 0.60, l[1.00] ],
            _[ t[2, 1.0], A4, Flute1, 0.80, l[2.33] ],
            _[ t[2, 2.5], B4, Flute2, 0.50, l[1.00] ],
            _[ t[2, 4.0], A3, Flute2, 0.50, l[1.67] ],
            _[ t[3, 1.0], G3, Flute3, 0.85, l[2.00] ],
            _[ t[3, 2.5], G4, Flute1, 0.80, l[2.50] ]
        ).SetName().Tape();

        FlowNode FluteMelody2 => WithAudioLength(bars[4]).Add
        (
            _[ t[1,1.0], E4, Flute1, 0.59, l[1.8]  ],
            _[ t[1,2.5], F4, Flute2, 0.68, l[1.0]  ],
            _[ t[1,4.0], G4, Flute1, 0.74, l[0.6]  ],
            _[ t[2,1.0], A4, Flute2, 0.82, l[2.0]  ],
            _[ t[2,2.5], B4, Flute3, 0.74, l[1.0]  ],
            _[ t[2,4.0], G4, Flute2, 0.90, l[0.4]  ],
            _[ t[3,1.0], A4, Flute4, 1.00, _[1.66] ]
        ).SetName().Tape();
        
        FlowNode OrganChords => Add
        (
            _[t: _[0], ChordPitchCurve1.Stretch(bars[1]), Organ, ChordVolumeCurve.Stretch(bars[1]), bars[8]],
            _[t: _[0], ChordPitchCurve2.Stretch(bars[1]), Organ, ChordVolumeCurve.Stretch(bars[1]), bars[8]],
            _[t: _[0], ChordPitchCurve3.Stretch(bars[1]), Organ, ChordVolumeCurve.Stretch(bars[1]), bars[8]]
        ).SetName();

        FlowNode OrganChords2 =>
            Multiply
            (
                Stretch(ChordVolumeCurve, bars[1]),
                WithAudioLength(bars[8]).Add
                (
                    Organ2(_[0], ChordPitchCurve1.Stretch(bars[1]), duration: bars[8]),
                    Organ2(_[0], ChordPitchCurve2.Stretch(bars[1]), duration: bars[8]),
                    Organ2(_[0], ChordPitchCurve3.Stretch(bars[1]), duration: bars[8])
                )
            ).SetName();
        
        FlowNode PadChords()
        {
            return Add
            (
                _[t:_[0], ChordPitchCurve1.Stretch(bars[1]), Pad, Stretch(ChordVolumeCurve, bars[1]).Delay(bars[1]), bars[8]],
                _[t:_[0], ChordPitchCurve2.Stretch(bars[1]), Pad, Stretch(ChordVolumeCurve, bars[1]).Delay(bars[1]), bars[8]],
                _[t:_[0], ChordPitchCurve3.Stretch(bars[1]), Pad, Stretch(ChordVolumeCurve, bars[1]).Delay(bars[1]), bars[8]]
            ).SetName();
        }
        
        /// <param name="volume">Used to promote clipping for distortion (only works for 16-bit, not 32-bit).</param>
        FlowNode DistortionChords(FlowNode volume = null)
        {
            return Multiply
            (
                Stretch(ChordVolumeCurve, bars[1]),
                WithAudioLength(bars[8]).Add
                (
                    DistortedNote(bar[0], ChordPitchCurve1.Stretch(bars[1]), duration: bars[8]).Volume(volume),
                    DistortedNote(bar[0], ChordPitchCurve2.Stretch(bars[1]), duration: bars[8]).Volume(volume),
                    DistortedNote(bar[0], ChordPitchCurve3.Stretch(bars[1]), duration: bars[8]).Volume(volume)
                ).SetName()
            );
        }
        
        /// <inheritdoc cref="docs._horn" />
        FlowNode HornMelody1 => WithAudioLength(beat[13 + 4]).Add
        (
            _[ beat[09], C2, Horn, 0.7, length[3] ],
            _[ beat[13], G1, Horn, 0.5, length[4] ]
        ).SetName().Tape();

        /// <inheritdoc cref="docs._horn" />
        FlowNode HornMelody2 => WithAudioLength(beat[9 + 4]).Add
        (
            _[ b[1], A2, Horn, 0.75, l[2] ],
            _[ b[5], F2, Horn, 0.85, l[2] ],
            _[ b[9], A1, Horn, 1.00, l[4] ]
        ).SetName().Tape();
        
        /// <inheritdoc cref="docs._trombone" />
        FlowNode TromboneMelody1 => WithAudioLength(beats[6]).Add
        (
            _[ b[1], A1 , Trombone      ],
            _[ b[3], E2 , Trombone      ],
            _[ b[5], Fs1, Trombone, 0.7 ]
        ).SetName().Tape();

        /// <inheritdoc cref="docs._trombone" />
        FlowNode TromboneMelody2 => WithAudioLength(beats[8]).Add
        (
            _[ beat[3], E4, Trombone, 1, _[1.4] ],
            _[ beat[7], C4, Trombone, 1, _[1.4] ]
        ).SetName().Tape();

        FlowNode RippleBassMelody2 =>
            _[ bar[3.5], A1, RippleBass, 1, bars[0.8] ].SetName();
        
        // Instruments

        /// <inheritdoc cref="docs._flute1" />
        FlowNode Flute1(FlowNode freq = null, FlowNode duration = null)
        {
            freq = freq ?? A4;

            var fmSignal = FMAround0(Divide(freq, 2), freq, _[0.005]);
            var envelope = Stretch(FluteCurve, duration);
            var note     = Multiply(fmSignal, envelope);
            
            return note.SetName();
        }

        /// <inheritdoc cref="docs._flute2" />
        FlowNode Flute2(FlowNode freq = null, FlowNode duration = null)
        {
            freq = freq ?? A4;

            var fmSignal       = FMAroundFreq(freq, Multiply(freq, 2), _[0.005]);
            var envelope       = Stretch(FluteCurve, duration);
            var modulatedSound = Multiply(fmSignal, envelope);
            var adjustedVolume = Multiply(modulatedSound, 0.85);

            return adjustedVolume.SetName();
        }

        /// <inheritdoc cref="docs._flute3" />
        FlowNode Flute3(FlowNode freq = null, FlowNode duration = null)
        {
            freq   = freq ?? A4;

            var fmSignal       = FMAroundFreq(freq, Multiply(freq, 4), _[0.005]);
            var envelope       = Stretch(FluteCurve, duration);
            var sound          = Multiply(fmSignal, envelope);
            var adjustedVolume = Multiply(sound, 0.8);

            return adjustedVolume.SetName();
        }

        /// <inheritdoc cref="docs._flute4" />
        FlowNode Flute4(FlowNode freq = null, FlowNode duration = null)
        {
            freq   = freq ?? A4;

            var fmSignal       = FMAround0(Multiply(freq, 2), freq, _[0.005]);
            var envelope       = Stretch(FluteCurve, duration);
            var sound          = Multiply(fmSignal, envelope);
            var adjustedVolume = Multiply(sound, 0.70);

            return adjustedVolume.SetName();
        }

        /// <inheritdoc cref="docs._default" />
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
        
        /// <inheritdoc cref="docs._default" />
        FlowNode Organ2(FlowNode delay = null, FlowNode freq = null, FlowNode volume = null, FlowNode duration = null)
        {
            freq     = freq ?? A4;
            duration = duration ?? GetAudioLength;
            
            var modCurve = Stretch(ModTamingCurve, duration);
            var modDepth = Multiply(0.0001, modCurve);
            var fmSignal = FMAroundFreq(freq, Multiply(freq, 2), modDepth);
            
            var volumeEvenOutCurve  = Stretch(EvenOutCurve, duration);
            var soundWithEvenVolume = Multiply(fmSignal, volumeEvenOutCurve);
            
            var note = StrikeNote(soundWithEvenVolume, delay, volume, duration);
            
            return note.SetName();
        }

        /// <inheritdoc cref="docs._default" />
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
        
        /// <inheritdoc cref="docs._default" />
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
            
            var note = StrikeNote(soundWithEvenVolume, delay, volume, duration);
            
            return note.SetName().Tape(duration);
        }


        /// <inheritdoc cref="docs._horn" />
        FlowNode Horn(FlowNode freq = null, FlowNode duration = null)
        {
            freq = freq ?? A2;

            var tamedMod = Multiply(5, Stretch(ModTamingCurve2, duration));

            var fmSignal = FMInHertz(Multiply(freq, 2), freq, tamedMod);
            var envelope = Stretch(BrassCurve, duration);
            var sound    = Multiply(fmSignal, envelope);

            return sound.SetName();
        }

        /// <inheritdoc cref="docs._trombone" />
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

        /// <inheritdoc cref="docs._default" />
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
            
            return adjustedVolume.SetName();
        }

        /// <inheritdoc cref="docs._ripplebass" />
        FlowNode RippleBass(FlowNode freq = null, FlowNode duration = null)
        {
            freq = freq ?? A1;
            var fmSignal = FMAroundFreq(freq * 8, freq / 2, _[0.005]);
            var note     = ShapeRippleSound(fmSignal, duration);
            return note.SetName();
        }

        /// <inheritdoc cref="docs._ripplenotesharpmetallic" />
        FlowNode RippleNote_SharpMetallic(FlowNode freq = null, FlowNode duration = null)
        {
            freq = freq ?? A3;
            var fmSignal = FMInHertz(freq, freq / 2, _[10]);
            var sound    = ShapeRippleSound(fmSignal, duration);
            return sound.SetName();
        }

        /// <inheritdoc cref="docs._ripplesoundclean" />
        FlowNode RippleSound_Clean(FlowNode freq = null, FlowNode duration = null)
        {
            freq = freq ?? A4;
            var fmSignal = FMAroundFreq(freq, _[20], _[0.005]);
            var sound    = ShapeRippleSound(fmSignal, duration);
            return sound.SetName();
        }

        /// <inheritdoc cref="docs._ripplesoundfantasyeffect" />
        FlowNode RippleSound_FantasyEffect(FlowNode freq = null, FlowNode duration = null)
        {
            freq = freq ?? A5;
            var fmSignal = FMAroundFreq(freq, _[10], _[0.02]);
            var sound    = ShapeRippleSound(fmSignal, duration);
            return sound.SetName();
        }

        /// <inheritdoc cref="docs._ripplesoundcooldouble" />
        FlowNode RippleSound_CoolDouble(FlowNode freq = null, FlowNode duration = null)
        {
            freq = freq ?? A5;
            var fmSignal = FMAroundFreq(freq, _[10], _[0.05]);
            var sound    = ShapeRippleSound(fmSignal, duration);
            return sound.SetName();
        }

        /// <inheritdoc cref="docs._shaperipplesound" />
        FlowNode ShapeRippleSound(FlowNode input, FlowNode duration)
        {
            duration = duration ?? GetAudioLength;
            var envelope = Stretch(RippleCurve, duration);
            var sound    = input * envelope;
            return sound.SetName();
        }

        /// <inheritdoc cref="docs._createfmnoisebeating" />
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
        
        /// <inheritdoc cref="docs._fminhertz" />
        FlowNode FMInHertz(FlowNode soundFreq, FlowNode modSpeed, FlowNode modDepth)
        {
            var modulator = Sine(modSpeed) * modDepth;
            var sound     = Sine(soundFreq + modulator);
            return sound.SetName();
        }

        /// <inheritdoc cref="docs._fmaround0" />
        FlowNode FMAround0(FlowNode soundFreq, FlowNode modSpeed, FlowNode modDepth)
        {
            var modulator = Sine(modSpeed) * modDepth;
            var sound     = Sine(soundFreq * modulator);
            return sound.SetName();
        }

        /// <inheritdoc cref="docs._fmaroundfreq" />
        FlowNode FMAroundFreq(FlowNode soundFreq, FlowNode modSpeed, FlowNode modDepth)
        {
            var modulator = 1 + Sine(modSpeed) * modDepth;
            var sound     = Sine(soundFreq * modulator);
            return sound.SetName();
        }
        
        // Curves

        FlowNode FluteCurve => WithName().Curve
        (
            (time: 0.00, value: 0.0),
            (time: 0.05, value: 0.8),
            (time: 0.10, value: 1.0),
            (time: 0.90, value: 0.7),
            (time: 1.00, value: 0.0)
        );

        FlowNode BrassCurve => WithName().Curve
        (
            (time: 0.00, value: 0),
            (time: 0.07, value: 1),
            (time: 0.93, value: 1),
            (time: 1.00, value: 0)
        );

        FlowNode RippleCurve => WithName().Curve
        (
            (time: 0.00, value: 0.00),
            (time: 0.01, value: 0.75),
            (time: 0.05, value: 0.50),
            (time: 0.25, value: 1.00),
            (time: 1.00, value: 0.00)
        );

        FlowNode DampedBlockCurve => WithName().Curve
        (
            (time: 0.00, value: 0),
            (time: 0.01, value: 1),
            (time: 0.99, value: 1),
            (time: 1.00, value: 0)
        );

        FlowNode LineDownCurve => WithName().Curve
        (
            (time: 0, value: 1),
            (time: 1, value: 0)
        );

        /// <inheritdoc cref="docs._modtamingcurve"/>
        FlowNode ModTamingCurve => WithName().Curve(0.3, 1.0, 0.3, 0.0);

        /// <inheritdoc cref="docs._modtamingcurve"/>
        FlowNode ModTamingCurve2 => WithName().Curve(1.0, 0.5, 0.2, 0.0);

        /// <inheritdoc cref="docs._modtamingcurve"/>
        FlowNode ModTamingCurve8Times => WithName().Curve
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
            
        /// <inheritdoc cref="docs._evenoutcurve"/>
        FlowNode EvenOutCurve => WithName().Curve
        (
            (time: 0.00, value: 1.0),
            (time: 0.33, value: 0.6),
            (time: 0.50, value: 0.6),
            (time: 0.75, value: 0.8),
            (time: 1.00, value: 1.0)
        );

        FlowNode ChordVolumeCurve => WithName().Curve
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