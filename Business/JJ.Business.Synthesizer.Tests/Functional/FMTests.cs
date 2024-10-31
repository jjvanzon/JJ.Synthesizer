using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Wishes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Tests.docs;
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ExplicitCallerInfoArgument

namespace JJ.Business.Synthesizer.Tests.Functional
{
    /// <inheritdoc cref="_fmtests"/>
    [TestClass]
    [TestCategory("Functional")]
    public class FMTests : SynthWishes
    {
        FluentOutlet JingleVolume => _[0.18];

        FluentOutlet MildEcho(FluentOutlet sound)
            => EchoParallel(sound * 0.3, count: 7, magnitude: _[0.25], delay: _[0.33]) / 0.3;

        FluentOutlet DeepEcho(FluentOutlet sound) 
            => EchoParallel(sound * 0.4, count: 7, magnitude: _[0.50], delay: _[0.50]) / 0.4;

        /// <inheritdoc cref="_fmtests"/>
        public FMTests() : base(beat: 0.45, bar: 4 * 0.45)
        {
            Mono();
            _chordFreqs = CreateChordFreqs();
        }

        // Tests

        [TestMethod]
        public void FM_Jingle() => new FMTests().FM_Jingle_RunTest();

        internal void FM_Jingle_RunTest()
        {
            WithAudioLength(bars[8] + 1); // HACK: Without the + something, sometimes last note is missing.
            
            Play(() => DeepEcho(Jingle()) * 1);
        }

        [TestMethod]
        public void FM_Flute_Melody1() => new FMTests().FM_Flute_Melody1_RunTest();

        void FM_Flute_Melody1_RunTest()
        {
            Play(() => MildEcho(FluteMelody1) * 0.6);
        }

        [TestMethod]
        public void FM_Flute_Melody2() => new FMTests().FM_Flute_Melody2_RunTest();

        void FM_Flute_Melody2_RunTest()
        {
            Play(() => MildEcho(FluteMelody2) * 0.4);
        }

        [TestMethod]
        public void FM_Flute1() => new FMTests().FM_Flute1_RunTest();

        void FM_Flute1_RunTest() 
            => Play(() => MildEcho(Flute1(E4)) * 0.5);

        [TestMethod]
        public void FM_Flute2() => new FMTests().FM_Flute2_RunTest();

        void FM_Flute2_RunTest()
            => Play(() => MildEcho(Flute2(F4))* 0.5);

        [TestMethod]
        public void FM_Flute3() => new FMTests().FM_Flute3_RunTest();

        void FM_Flute3_RunTest()
        {
            Play(() => MildEcho(Flute3(G4))* 0.5);
        }

        [TestMethod]
        public void FM_Flute4() => new FMTests().FM_Flute4_RunTest();

        void FM_Flute4_RunTest()
        {
            Play(() => MildEcho(Flute4(A4))* 0.5);
        }

        [TestMethod]
        public void FM_Organ() => new FMTests().FM_Organ_RunTest();

        void FM_Organ_RunTest()
        {
            WithAudioLength(bars[3]).Play(() => MildEcho(Organ()) * 0.5);
        }

        [TestMethod]
        public void FM_Organ_Chords() => new FMTests().FM_Organ_Chords_RunTest();

        void FM_Organ_Chords_RunTest()
        {
            Play(() => MildEcho(OrganChords) * 0.2);
        }
        
        [TestMethod]
        public void FM_Pad() => new FMTests().FM_Pad_RunTest();

        void FM_Pad_RunTest()
        {
            WithAudioLength(bars[3]).Play(() => MildEcho(Pad()) * 0.4);
        }

        [TestMethod]
        public void FM_Pad_Chords() => new FMTests().FM_Pad_Chords_RunTest();

        void FM_Pad_Chords_RunTest()
        {
            Play(() => MildEcho(PadChords(volume: _[0.15])) * 0.6);
        }

        [TestMethod]
        public void FM_Pad_Chords_Distortion() => new FMTests().FM_Pad_Chords_Distortion_RunTest();

        void FM_Pad_Chords_Distortion_RunTest()
        {
            Play(() => MildEcho(PadChords(volume: _[0.92]) * 0.15));
        }

        /// <inheritdoc cref="_horn" />
        [TestMethod]
        public void FM_Horn() => new FMTests().FM_Horn_RunTest();

        /// <inheritdoc cref="_horn" />
        void FM_Horn_RunTest()
        {
            Play(() => MildEcho(Horn())* 0.5);
        }

        /// <inheritdoc cref="_horn" />
        [TestMethod]
        public void FM_Horn_Melody1() => new FMTests().FM_Horn_Melody1_RunTest();

        /// <inheritdoc cref="_horn" />
        void FM_Horn_Melody1_RunTest()
        {
            Play(() => MildEcho(HornMelody1) * 0.6);
        }

        /// <inheritdoc cref="_horn" />
        [TestMethod]
        public void FM_Horn_Melody2() => new FMTests().FM_Horn_Melody2_RunTest();

        /// <inheritdoc cref="_horn" />
        void FM_Horn_Melody2_RunTest()
        {
            Play(() => MildEcho(HornMelody2) * 0.6);
        }
        
        /// <inheritdoc cref="_trombone" />
        [TestMethod]
        public void FM_Trombone() => new FMTests().FM_Trombone_RunTest();

        /// <inheritdoc cref="_trombone" />
        void FM_Trombone_RunTest()
        {
            WithAudioLength(2).Play(() => MildEcho(Trombone(E2))* 0.5);
        }
        
        /// <inheritdoc cref="_trombone" />
        [TestMethod]
        public void FM_Trombone_Melody1() => new FMTests().FM_Trombone_Melody1_RunTest();

        /// <inheritdoc cref="_trombone" />
        void FM_Trombone_Melody1_RunTest()
        {
            Play(() => MildEcho(TromboneMelody1) * 0.45);
        }

        /// <inheritdoc cref="_trombone" />
        [TestMethod]
        public void FM_Trombone_Melody2() => new FMTests().FM_Trombone_Melody2_RunTest();

        /// <inheritdoc cref="_trombone" />
        void FM_Trombone_Melody2_RunTest()
        {
            Play(() => MildEcho(TromboneMelody2) * 0.75);
        }

        [TestMethod]
        public void FM_ElectricNote() => new FMTests().FM_ElectricNote_RunTest();

        void FM_ElectricNote_RunTest()
        {
            WithAudioLength(_[1.5]).Play(() => MildEcho(ElectricNote()) * 0.2);
        }

        [TestMethod]
        public void FM_RippleBass() => new FMTests().FM_RippleBass_RunTest();

        void FM_RippleBass_RunTest()
        {
            WithAudioLength(3).Play(() => DeepEcho(RippleBass())* 0.5);
        }

        [TestMethod]
        public void FM_RippleBass_Melody2() => new FMTests().FM_RippleBass_Melody2_RunTest();

        void FM_RippleBass_Melody2_RunTest()
        {
            WithAudioLength(bars[4]).Play(() => DeepEcho(RippleBassMelody2) * 0.3);
        }

        [TestMethod]
        public void FM_RippleNote_SharpMetallic() => new FMTests().FM_RippleNote_SharpMetallic_RunTest();

        void FM_RippleNote_SharpMetallic_RunTest()
        {
            WithAudioLength(2.2).Play(() => DeepEcho(RippleNote_SharpMetallic()) * 0.3);
        }

        [TestMethod]
        public void FM_RippleSound_Clean() => new FMTests().FM_RippleSound_Clean_RunTest();

        void FM_RippleSound_Clean_RunTest()
        {
            WithAudioLength(4).Play(() => DeepEcho(RippleSound_Clean())* 0.5);
        }

        [TestMethod]
        public void FM_RippleSound_FantasyEffect() => new FMTests().FM_RippleSound_FantasyEffect_RunTest();

        void FM_RippleSound_FantasyEffect_RunTest()
        {
            WithAudioLength(4).Play(() => DeepEcho(RippleSound_FantasyEffect()) * 0.33);
        }

        [TestMethod]
        public void FM_RippleSound_CoolDouble() => new FMTests().FM_RippleSound_CoolDouble_RunTest();

        void FM_RippleSound_CoolDouble_RunTest()
        {
            WithAudioLength(3).Play(() => DeepEcho(RippleSound_CoolDouble()) * 0.3);
        }

        [TestMethod]
        public void FM_Noise_Beating() => new FMTests().FM_Noise_Beating_RunTest();

        void FM_Noise_Beating_RunTest()
        {
            WithAudioLength(5).Play(() => MildEcho(Create_FM_Noise_Beating(A4)) * 0.25);
        }

        // Jingle

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
                
                WithAudioLength(bars[4]);

                var pattern2 = WithName("Jingle Pattern 2").ParallelAdd
                (
                    JingleVolume.Value,
                    () => Multiply(fluteVolume,      FluteMelody2),
                    () => Multiply(tromboneVolume,   TromboneMelody2),
                    () => Multiply(hornVolume,       HornMelody2),
                    () => Multiply(rippleBassVolume, RippleBassMelody2)
                );

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

        // Melodies

        FluentOutlet FluteMelody1 => WithAudioLength(bars[4]).WithName().ParallelAdd
        (
            () => _[ t[1,1.0], E4, Flute1, 0.80, l[2.00] ],
            () => _[ t[1,2.5], F4, Flute1, 0.70, l[2.17] ],
            () => _[ t[1,4.0], G4, Flute1, 0.60, l[1.00] ],
            () => _[ t[2,1.0], A4, Flute1, 0.80, l[2.33] ],
            () => _[ t[2,2.5], B4, Flute2, 0.50, l[1.00] ],
            () => _[ t[2,4.0], A3, Flute2, 0.50, l[1.67] ],
            () => _[ t[3,1.0], G3, Flute3, 0.85, l[2.00] ],
            () => _[ t[3,2.5], G4, Flute1, 0.80, l[2.50] ]
        );

        FluentOutlet FluteMelody2 => WithAudioLength(bars[4]).WithName().ParallelAdd
        (
            () => _[ t[1,1.0], E4, Flute1, 0.59, l[1.8]  ],
            () => _[ t[1,2.5], F4, Flute2, 0.68, l[1.0]  ],
            () => _[ t[1,4.0], G4, Flute1, 0.74, l[0.6]  ],
            () => _[ t[2,1.0], A4, Flute2, 0.82, l[2.0]  ],
            () => _[ t[2,2.5], B4, Flute3, 0.74, l[1.0]  ],
            () => _[ t[2,4.0], G4, Flute2, 0.90, l[0.4]  ],
            () => _[ t[3,1.0], A4, Flute4, 1.00, _[1.66] ]
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

        /// <inheritdoc cref="_horn" />
        FluentOutlet HornMelody1 => WithAudioLength(beat[13 + 4]).WithName().ParallelAdd
        (
            () => _[ beat[09], Horn(C2, length[3]), 0.7 ],
            () => _[ beat[13], Horn(G1, length[4]), 0.5 ]
        );

        /// <inheritdoc cref="_horn" />
        FluentOutlet HornMelody2 => WithAudioLength(beat[9 + 4]).WithName().ParallelAdd
        (
            () => _[ b[1], A2, Horn, 0.75, l[2] ],
            () => _[ b[5], F2, Horn, 0.85, l[2] ],
            () => _[ b[9], A1, Horn, 1.00, l[4] ]
        );
        
        /// <inheritdoc cref="_trombone" />
        FluentOutlet TromboneMelody1 => WithAudioLength(beats[6]).WithName().ParallelAdd
        (
            () => _[ b[1], A1 , Trombone      ],
            () => _[ b[3], E2 , Trombone      ],
            () => _[ b[5], Fs1, Trombone, 0.7 ]
        );

        /// <inheritdoc cref="_trombone" />
        FluentOutlet TromboneMelody2 => WithAudioLength(beats[8]).WithName().ParallelAdd
        (
            () => _[ beat[3], E4, Trombone, 1, _[1.4] ],
            () => _[ beat[7], C4, Trombone, 1, _[1.4] ]
        );

        FluentOutlet RippleBassMelody2 =>
            _[ bar[3.5], A1, RippleBass, 1, bars[0.8] ];

        
        // Instruments

        /// <inheritdoc cref="_flute1" />
        FluentOutlet Flute1(FluentOutlet freq = null, FluentOutlet duration = null)
        {
            freq = freq ?? A4;

            var fmSignal = FMAround0(Divide(freq, 2), freq, _[0.005]);
            var envelope = Stretch(FluteCurve, duration);
            var note     = Multiply(fmSignal, envelope);
            
            return note;
        }

        /// <inheritdoc cref="_flute2" />
        FluentOutlet Flute2(FluentOutlet freq = null, FluentOutlet duration = null)
        {
            freq = freq ?? A4;

            var fmSignal       = FMAroundFreq(freq, Multiply(freq, 2), _[0.005]);
            var envelope       = Stretch(FluteCurve, duration);
            var modulatedSound = Multiply(fmSignal, envelope);
            var adjustedVolume = Multiply(modulatedSound, 0.85);

            return adjustedVolume;
        }

        /// <inheritdoc cref="_flute3" />
        FluentOutlet Flute3(FluentOutlet freq = null, FluentOutlet duration = null)
        {
            freq   = freq ?? A4;

            var fmSignal       = FMAroundFreq(freq, Multiply(freq, 4), _[0.005]);
            var envelope       = Stretch(FluteCurve, duration);
            var sound          = Multiply(fmSignal, envelope);
            var adjustedVolume = Multiply(sound, 0.8);

            return adjustedVolume;
        }

        /// <inheritdoc cref="_flute4" />
        FluentOutlet Flute4(FluentOutlet freq = null, FluentOutlet duration = null)
        {
            freq   = freq ?? A4;

            var fmSignal       = FMAround0(Multiply(freq, 2), freq, _[0.005]);
            var envelope       = Stretch(FluteCurve, duration);
            var sound          = Multiply(fmSignal, envelope);
            var adjustedVolume = Multiply(sound, 0.70);

            return adjustedVolume;
        }

        /// <inheritdoc cref="_default" />
        FluentOutlet Organ(FluentOutlet delay = null, FluentOutlet freq = null, FluentOutlet volume = null, FluentOutlet duration = null)
        {
            freq     = freq ?? A4;
            //duration = duration ?? _[1];
            duration = duration ?? AudioLength;

            var modCurve = Stretch(ModTamingCurve, duration);
            var modDepth = Multiply(0.0001, modCurve);
            var fmSignal = FMAroundFreq(freq, Multiply(freq, 2), modDepth);

            var volumeEvenOutCurve  = Stretch(EvenOutCurve, duration);
            var soundWithEvenVolume = Multiply(fmSignal, volumeEvenOutCurve);

            var note = StrikeNote(soundWithEvenVolume, delay, volume);

            return note;
        }

        /// <inheritdoc cref="_default" />
        FluentOutlet Pad(FluentOutlet delay = null, FluentOutlet freq = null, FluentOutlet volume = null, FluentOutlet duration = null)
        {
            freq     = freq ?? A4;
            //duration = duration ?? beats[1];
            duration = duration ?? AudioLength;

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

        /// <inheritdoc cref="_trombone" />
        FluentOutlet Trombone(FluentOutlet freq = null, FluentOutlet durationFactor = null)
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

            return sound;
        }

        /// <inheritdoc cref="_horn" />
        FluentOutlet Horn(FluentOutlet freq = null, FluentOutlet duration = null)
        {
            freq = freq ?? A2;

            var tamedMod = Multiply(5, Stretch(ModTamingCurve2, duration));

            var fmSignal = FMInHertz(Multiply(freq, 2), freq, tamedMod);
            var envelope = Stretch(BrassCurve, duration);
            var sound    = Multiply(fmSignal, envelope);

            return sound;
        }

        /// <inheritdoc cref="_default" />
        FluentOutlet ElectricNote(FluentOutlet freq = null, FluentOutlet duration = null)
        {
            freq   = freq ?? A4;
            duration = duration ?? AudioLength;

            var modDepth = 0.02 * Stretch(LineDownCurve, duration);
            var fmSignal = Add
            (
                FMAroundFreq(freq, freq * 1.5, modDepth),
                FMAroundFreq(freq, freq * 2.0, modDepth)
            );

            var modulatedSound = fmSignal.Curve(DampedBlockCurve.Stretch(duration));
            var adjustedVolume = modulatedSound * 0.6;
            
            return adjustedVolume;
        }

        /// <inheritdoc cref="_ripplebass" />
        FluentOutlet RippleBass(FluentOutlet freq = null, FluentOutlet duration = null)
        {
            freq = freq ?? A1;
            var fmSignal = FMAroundFreq(freq * 8, freq / 2, _[0.005]);
            var note     = ShapeRippleSound(fmSignal, duration);
            return note;
        }

        /// <inheritdoc cref="_ripplenotesharpmetallic" />
        FluentOutlet RippleNote_SharpMetallic(FluentOutlet freq = null, FluentOutlet duration = null)
        {
            freq = freq ?? A3;
            var fmSignal = FMInHertz(freq, freq / 2, _[10]);
            var sound    = ShapeRippleSound(fmSignal, duration);
            return sound;
        }

        /// <inheritdoc cref="_ripplesoundclean" />
        FluentOutlet RippleSound_Clean(FluentOutlet freq = null, FluentOutlet duration = null)
        {
            freq = freq ?? A4;
            var fmSignal = FMAroundFreq(freq, _[20], _[0.005]);
            var sound    = ShapeRippleSound(fmSignal, duration);
            return sound;
        }

        /// <inheritdoc cref="_ripplesoundfantasyeffect" />
        FluentOutlet RippleSound_FantasyEffect(FluentOutlet freq = null, FluentOutlet duration = null)
        {
            freq = freq ?? A5;
            var fmSignal = FMAroundFreq(freq, _[10], _[0.02]);
            var sound    = ShapeRippleSound(fmSignal, duration);
            return sound;
        }

        /// <inheritdoc cref="_ripplesoundcooldouble" />
        FluentOutlet RippleSound_CoolDouble(FluentOutlet freq = null, FluentOutlet duration = null)
        {
            freq = freq ?? A5;
            var fmSignal = FMAroundFreq(freq, _[10], _[0.05]);
            var sound    = ShapeRippleSound(fmSignal, duration);
            return sound;
        }

        /// <inheritdoc cref="_shaperipplesound" />
        FluentOutlet ShapeRippleSound(FluentOutlet input, FluentOutlet duration)
        {
            duration = duration ?? AudioLength; // _[2.5];
            var envelope = Stretch(RippleCurve, duration);
            var sound    = input * envelope;
            return sound;
        }

        /// <inheritdoc cref="_createfmnoisebeating" />
        FluentOutlet Create_FM_Noise_Beating(FluentOutlet pitch = null, FluentOutlet duration = null)
        {
            duration = duration ?? AudioLength;
            
            var signal = FMAroundFreq(pitch ?? A4, _[55], _[0.5]);
            
            var curve = Curve(0.0, 1.00, 0.2, 1.10, 
                              0.4, 0.90, 0.4, 0.90, 
                              0.5, 0.85, 0.6, 0.80, 
                              0.5, 0.80, 0.4, 0.85, 
                              0.2, 0.00);
            
            return signal * curve.Skip(duration / 16).Stretch(duration);
        }

        // Algorithms
        
        /// <inheritdoc cref="_fminhertz" />
        FluentOutlet FMInHertz(FluentOutlet soundFreq, FluentOutlet modSpeed, FluentOutlet modDepth)
        {
            var modulator = Sine(modSpeed) * modDepth;
            var sound     = Sine(soundFreq + modulator);
            return sound;
        }

        /// <inheritdoc cref="_fmaround0" />
        FluentOutlet FMAround0(FluentOutlet soundFreq, FluentOutlet modSpeed, FluentOutlet modDepth)
        {
            var modulator = Sine(modSpeed) * modDepth;
            var sound     = Sine(soundFreq * modulator);
            return sound;
        }

        /// <inheritdoc cref="_fmaroundfreq" />
        FluentOutlet FMAroundFreq(FluentOutlet soundFreq, FluentOutlet modSpeed, FluentOutlet modDepth)
        {
            var modulator = 1 + Sine(modSpeed) * modDepth;
            var sound     = Sine(soundFreq * modulator);
            return sound;
        }
        
        // Curves
        
        // TODO: Name them? Or does GetCallerNameFromStack do it?

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

        /// <inheritdoc cref="_modtamingcurve"/>
        FluentOutlet ModTamingCurve => Curve(0.3, 1.0, 0.3, 0.0);

        /// <inheritdoc cref="_modtamingcurve"/>
        FluentOutlet ModTamingCurve2 => Curve(1.0, 0.5, 0.2, 0.0);

        /// <inheritdoc cref="_modtamingcurve"/>
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
            
        /// <inheritdoc cref="_evenoutcurve"/>
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

        FluentOutlet ChordPitchCurve1 => Curve(
            _chordFreqs.Select(x => new NodeInfo(x.time,
                                                 x.freq1.Value,
                                                 NodeTypeEnum.Block)).ToList());

        FluentOutlet ChordPitchCurve2 => Curve(
            _chordFreqs.Select(x => new NodeInfo(x.time,
                                                 x.freq2.Value,
                                                 NodeTypeEnum.Block)).ToList());

        FluentOutlet ChordPitchCurve3 => Curve(
            _chordFreqs.Select(x => new NodeInfo(x.time,
                                                 x.freq3.Value,
                                                 NodeTypeEnum.Block)).ToList());
    }
}