using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Business.Synthesizer.Wishes.Helpers;
using static JJ.Business.Synthesizer.Wishes.Helpers.NameHelper;

namespace JJ.Business.Synthesizer.Tests.Functional
{
    [TestClass]
    [TestCategory("Functional")]
    public class ModulationTests : SynthWishes
    {
        int          MildEchoCount  => 4;
        FluentOutlet MildEchoDelay  => _[0.33];
        int          DeepEchoCount  => 4;
        FluentOutlet DeepEchoDelayL => _[0.5];
        FluentOutlet DeepEchoDelayR => _[0.53];

        public ModulationTests()
            : base(beat: 2.2, bar: 2.2)
        {
            Stereo();
        }

        #region Tests

        // Long Running
        /// <inheritdoc cref="docs._detunica" />
        internal void Detunica_Jingle_RunTest() 
            => WithAudioLength(bars[7]).Play(() => DeepEcho(DetunicaJingle) * 0.6);

        /// <inheritdoc cref="docs._detunica" />
        [TestMethod]
        public void Test_Detunica_Jingle_Mono() => new ModulationTests().Detunica_Jingle_RunTest_Mono();

        /// <inheritdoc cref="docs._detunica" />
        internal void Detunica_Jingle_RunTest_Mono()
        {
            Mono().WithAudioLength(bars[7]).Play(() => DeepEcho(DetunicaJingle) * 0.15);
        }

        /// <inheritdoc cref="docs._detunica" />
        [TestMethod]
        public void Test_DetunicaBass() => new ModulationTests().DetunicaBass_RunTest();

        /// <inheritdoc cref="docs._detunica" />
        internal void DetunicaBass_RunTest()
        {
            WithAudioLength(3).Play(() => DeepEcho(DetunicaBass(E0, _[3])) * 0.9);
        }

        /// <inheritdoc cref="docs._detunica" />
        [TestMethod]
        public void Test_Detunica1() => new ModulationTests().Detunica1_RunTest();

        /// <inheritdoc cref="docs._detunica" />
        void Detunica1_RunTest()
        {
            var duration = _[3];
            WithAudioLength(duration).Play(() => DeepEcho(Detunica1(E2, duration)) * 0.15);
        }

        /// <inheritdoc cref="docs._detunica" />
        [TestMethod]
        public void Test_Detunica2() => new ModulationTests().Detunica2_RunTest();

        /// <inheritdoc cref="docs._detunica" />
        void Detunica2_RunTest()
        {
            var duration = _[3];
            WithAudioLength(duration).Play(() => DeepEcho(Detunica2(freq: B4, duration: duration)) * 0.9);
        }

        /// <inheritdoc cref="docs._detunica" />
        [TestMethod]
        public void Test_Detunica3() => new ModulationTests().Detunica3_RunTest();

        /// <inheritdoc cref="docs._detunica" />
        void Detunica3_RunTest()
        {
            var duration = _[3];
            WithAudioLength(duration).Play(() => DeepEcho(Detunica3(freq: C5, duration: duration)));
        }

        /// <inheritdoc cref="docs._detunica" />
        [TestMethod]
        public void Test_Detunica4() => new ModulationTests().Detunica4_RunTest();

        /// <inheritdoc cref="docs._detunica" />
        void Detunica4_RunTest()
        {
            var duration = _[3];
            WithAudioLength(duration).Play(() => DeepEcho(Detunica4(freq: D5, duration: duration)) * 0.25);
        }

        /// <inheritdoc cref="docs._detunica" />
        [TestMethod]
        public void Test_Detunica5() => new ModulationTests().Detunica5_RunTest();

        /// <inheritdoc cref="docs._detunica" />
        void Detunica5_RunTest()
        {
            var duration = _[3];
            WithAudioLength(duration).Play(() => DeepEcho(Detunica5(freq: E5, duration: duration)) * 0.3);
        }

        /// <inheritdoc cref="_vibraphasedocs" />
        [TestMethod]
        public void Test_Vibraphase_Chord() => new ModulationTests().Vibraphase_Chord_RunTest();

        /// <inheritdoc cref="_vibraphasedocs" />
        void Vibraphase_Chord_RunTest()
            => Mono().Play(() => MildEcho(VibraphaseChord) * 0.28);

        /// <inheritdoc cref="_vibraphasedocs" />
        [TestMethod]
        public void Test_Vibraphase() => new ModulationTests().Vibraphase_RunTest();

        /// <inheritdoc cref="_vibraphasedocs" />
        void Vibraphase_RunTest()
            => Mono().Play(() => MildEcho(Vibraphase(freq: E5)) * 0.5);

        #endregion

        #region Jingles

        /// <inheritdoc cref="_vibraphasedocs" />
        FluentOutlet VibraphaseChord => Add // Parallel gives different sound at the moment.
        (
            Vibraphase(A4) * 0.80,
            Vibraphase(B4) * 0.70,
            Vibraphase(C5) * 0.85,
            Vibraphase(D5) * 0.75,
            Vibraphase(E5) * 0.90
        );

        /// <inheritdoc cref="docs._detunica" />
        FluentOutlet DetunicaJingle => WithName().ParallelAdd
        (
            volume: 0.26,
            () => _[ beat[1], E0, DetunicaBass, 1.00, l[5.25] ],
            () => _[ beat[2], B4, Detunica2   , 0.70, l[1.50] ],
            () => _[ beat[3], C5, Detunica3   , 0.75, l[1.60] ],
            () => _[ beat[4], D5, Detunica4   , 0.90, l[1.50] ],
            () => _[ beat[5], E5, Detunica5   , 1.00, l[3.00] ]
        ) / 0.26;

        #endregion

        #region Notes

        FluentOutlet DetunicaBass(FluentOutlet freq, FluentOutlet duration = null)
        {
            duration = duration ?? AudioLength;

            return Panbrello(
                panbrello: (speed: 2, depth: 0.20),
                sound: WithName().ParallelAdd
                (
                    volume: 0.5,
                    () => 0.600 * Detunica1(freq *  1, duration, detuneDepth: _[0.6], chorusRate: _[0.040]),
                    () => 0.800 * Detunica2(freq *  2, duration),
                    () => 1.000 * Detunica3(freq *  4, duration),
                    () => 0.015 * Detunica4(freq *  8, duration),
                    () => 0.001 * Detunica5(freq * 16, duration)
                ) / 0.5
            );
        }

        /// <inheritdoc cref="docs._detunica" />
        FluentOutlet Detunica1(
            FluentOutlet freq, FluentOutlet duration = null,
            FluentOutlet detuneDepth = null, FluentOutlet chorusRate = null)
            => Detunica
                (
                    freq.VibratoOverPitch(3, 0.00010), duration,
                    detuneDepth: detuneDepth ?? _[0.8],
                    chorusRate: (chorusRate ?? _[0.03]) * RateCurve1,
                    patchyEnvelope: false
                )
                .Tremolo(1, 0.03);

        /// <inheritdoc cref="docs._detunica" />
        FluentOutlet Detunica2(FluentOutlet freq, FluentOutlet duration = null)
            => MildEcho
            (
                Detunica(
                        freq.VibratoOverPitch(10, 0.00020), duration,
                        detuneDepth: _[1.0],
                        churnRate: 0.1 * RateCurve2)
                    .Tremolo(12, 0.1)
                    .Panning(0.4)
                    .Panbrello(2.6, 0.09)
            );

        /// <inheritdoc cref="docs._detunica" />
        FluentOutlet Detunica3(FluentOutlet freq, FluentOutlet duration = null)
            => Detunica
               (
                   freq.VibratoOverPitch(5.5, 0.0005),
                   duration,
                   detuneDepth: _[0.5],
                   interferenceRate: Multiply(0.002, RateCurve1),
                   chorusRate: Multiply(0.002,       RateCurve1),
                   patchyEnvelope: false
               )
               .Tremolo(15, 0.06)
               .Panning(Stretch(Curve(0.7, 0.3), duration))
               .Panbrello(4.8, 0.05);

        /// <inheritdoc cref="docs._detunica" />
        FluentOutlet Detunica4(FluentOutlet freq, FluentOutlet duration = null)
            => Detunica
               (
                   freq.VibratoOverPitch(7, 0.0003),
                   duration,
                   detuneDepth: _[0.5],
                   interferenceRate: 0.003 * RateCurve3
               )
               .Tremolo(10, 0.08)
               .Panning(Curve(0.2, 0.8).Stretch(duration))
               .Panbrello(3.4, 0.07);

        /// <inheritdoc cref="docs._detunica" />
        FluentOutlet Detunica5(FluentOutlet freq, FluentOutlet duration = null)
            => Detunica
               (
                   freq.VibratoOverPitch(5.5, 0.00005), duration,
                   detuneDepth: _[0.8],
                   churnRate: 0.001 * RateCurve1,
                   chorusRate: _[0.001]
               )
               .Tremolo(3, 0.25)
               .Panning(0.48);

        #endregion

        #region Instruments

        /// <inheritdoc cref="docs._detunica" />
        internal FluentOutlet Detunica(
            FluentOutlet freq = default, FluentOutlet duration = default,
            FluentOutlet detuneDepth = null, FluentOutlet churnRate = null, 
            FluentOutlet interferenceRate = null, FluentOutlet chorusRate = null,
            bool patchyEnvelope = true)
        {
            duration = duration ?? _[1];

            var baseHarmonics    = BaseHarmonics(freq);
            var detunedHarmonics = DetunedHarmonics(freq, duration, churnRate, interferenceRate, chorusRate);
            var sound            = baseHarmonics + detunedHarmonics * detuneDepth;
            var envelope         = patchyEnvelope ? PatchyEnvelope : EvenEnvelope;
            
            sound *= envelope.Stretch(duration);

            return sound;
        }

        /// <inheritdoc cref="_vibraphasedocs" />
        FluentOutlet Vibraphase(
            FluentOutlet freq = null,
            FluentOutlet duration = null,
            FluentOutlet depthAdjust1 = null, FluentOutlet depthAdjust2 = null)
        {
            var saw      = SemiSaw(freq);
            var jittered = Jitter(saw, depthAdjust1, depthAdjust2);
            var envelope = Curve(@"
               o                   
             o   o                 
                                    
                       o           
            o                   o");

            var sound = jittered * envelope.Stretch(duration);

            return sound;
        }

        #endregion

        #region WaveForms

        /// <inheritdoc cref="_semisawdocs" />
        FluentOutlet SemiSaw(FluentOutlet freq)
        {
            freq = freq ?? A4;

            return WithName().ParallelAdd
            (
                () => freq.Times(1).Sine * 1.0,
                () => freq.Times(2).Sine * 0.5,
                () => freq.Times(3).Sine * 0.3, 
                () => freq.Times(4).Sine * 0.2
            );
        }

        FluentOutlet BaseHarmonics(FluentOutlet freq)
        {
            freq = freq ?? A4;

            return Add
            (
                1.00 * Sine(freq * 1),
                0.30 * Sine(freq * 2),
                0.15 * Sine(freq * 5),
                0.08 * Sine(freq * 7),
                0.10 * Sine(freq * 9)
            );
        }

        /// <inheritdoc cref="_detunedocs" />
        FluentOutlet DetunedHarmonics(
            FluentOutlet freq, FluentOutlet duration,
            FluentOutlet churnRate = null, FluentOutlet interferenceRate = null, FluentOutlet chorusRate = null)
        {
            freq = freq ?? A4;
             
            return Add
            (
                1.00 * Sine(DetuneFreq(freq, _[1], duration, churnRate, interferenceRate, chorusRate)),
                0.30 * Sine(DetuneFreq(freq, _[2], duration, churnRate, interferenceRate, chorusRate)),
                0.15 * Sine(DetuneFreq(freq, _[5], duration, churnRate, interferenceRate, chorusRate)),
                0.08 * Sine(DetuneFreq(freq, _[7], duration, churnRate, interferenceRate, chorusRate)),
                0.10 * Sine(DetuneFreq(freq, _[9], duration, churnRate, interferenceRate, chorusRate))
            );
        }

        #endregion

        #region Effects

        /// <inheritdoc cref="_detunedocs" />
        FluentOutlet DetuneFreq(
            FluentOutlet freq, FluentOutlet harmonic, FluentOutlet duration,
            FluentOutlet churnRate = null, FluentOutlet interfereRate = null, FluentOutlet chorusRate = null)
        {
            var detunedFreq = freq;

            // Add to harmonic number = churn / heavy interference
            if (churnRate != null)
            {
                var detunedHarmonic = harmonic + churnRate.Stretch(duration);
                detunedFreq *= detunedHarmonic;
            }

            // Add Hz = light interference
            if (interfereRate != null)
            {
                detunedFreq += interfereRate.Stretch(duration);
            }

            // Multiply by 1 + rate = chorus
            if (chorusRate != null)
            {
                detunedFreq *= 1 + chorusRate.Stretch(duration);
            }

            return detunedFreq;
        }

        /// <inheritdoc cref="_vibraphasedocs" />
        FluentOutlet Jitter(FluentOutlet sound, FluentOutlet depthAdjust1 = null, FluentOutlet depthAdjust2 = null)
        {
            depthAdjust1 = depthAdjust1 ?? _[0.005];
            depthAdjust2 = depthAdjust2 ?? _[0.250];

            var tremoloWave1 = Sine(5.5) * depthAdjust1.Add(1); // 5.5 Hz _tremolo
            sound *= tremoloWave1;

            var tremoloWave2 = Sine(4.0) * depthAdjust2.Add(1); // 4 Hz _tremolo
            sound *= tremoloWave2;

            return sound;
        }

        bool _mildEchoAudioLengthWasAdded;

        /// <inheritdoc cref="_echodocs" />
        FluentOutlet MildEcho(FluentOutlet sound)
        {
            bool mustAddAudioLength = !_mildEchoAudioLengthWasAdded;
         
            // Test without name (defaults to caller member name 'MildEcho')
            var echoed = EchoParallel(sound * 0.25, MildEchoCount, magnitude: _[0.25], MildEchoDelay, mustAddAudioLength) / 0.25;

            _mildEchoAudioLengthWasAdded = true;

            return echoed;
        }

        bool _deepEchoAudioLengthWasAdded;
            
        /// <inheritdoc cref="_echodocs" />
        internal FluentOutlet DeepEcho(FluentOutlet sound)
        {
            bool mustAddAudioLength = !_deepEchoAudioLengthWasAdded;

            FluentOutlet echoed;

            switch (Channel)
            {
                case ChannelEnum.Single:
                    // Test WithName
                    WithName();
                    echoed = (sound * 0.18).EchoParallel(DeepEchoCount, magnitude: _[1 / 2.0], DeepEchoDelayL, mustAddAudioLength) / 0.18;
                    break;
                
                case ChannelEnum.Left:
                    // Test FetchName
                    echoed = (sound * 0.4).EchoParallel(DeepEchoCount, magnitude: _[1 / 2.1], DeepEchoDelayL, mustAddAudioLength, FetchName()) / 0.4;
                    break;
                
                case ChannelEnum.Right:
                    // Test MemberName
                    echoed = (sound * 0.4).EchoParallel(DeepEchoCount, magnitude: _[1 / 2.0], DeepEchoDelayR, mustAddAudioLength, MemberName()) / 0.4;
                    break;
                
                default: 
                    throw new ValueNotSupportedException(Channel);
            }

            _deepEchoAudioLengthWasAdded = true;
            
            return echoed;

        }

        #endregion

        #region Curves

        FluentOutlet PatchyEnvelope => WithName().Curve(@"
                         o                             
                    
                              o                         
            o       o
                o                                     

                                     o                   

        o                                       o ");

        FluentOutlet EvenEnvelope => WithName().Curve(@"
                          o                             
                   
                                                        
          o                             o
                                                      
                                    
                                     
           
        o                                       o ");

        FluentOutlet RateCurve1 => WithName().Curve(@"
                    o          
                                
                                
        o                   o");

        FluentOutlet RateCurve2 => WithName().Curve(@"
             o                 
                                
                                
        o                   o ");

        FluentOutlet RateCurve3 => WithName().Curve(@"
                  o            
                                
                                
        o                   o ");

        #endregion

        #region Docs

        #pragma warning disable CS0649
        #pragma warning disable IDE0044
        #pragma warning disable CS0169 // Field is never used

        // ReSharper disable InconsistentNaming

        /// <summary>
        /// Applies a jitter effect to notes, with adjustable depths.
        /// Basically with an extreme double _tremolo effect, that goes into the negative.
        /// That can also cause a phasing effect due to constructive and destructive interference
        /// when playing multiple notes at the same time.
        /// </summary>
        /// <param name="sound"> The sound to apply the jitter effect to. </param>
        /// <param name="depthAdjust1"> The first depth adjustment for the jitter effect. Defaults to 0.005 if not provided. </param>
        /// <param name="depthAdjust2"> The second depth adjustment for the jitter effect. Defaults to 0.250 if not provided. </param>
        /// <inheritdoc cref="Wishes.Helpers.docs._default" />
        [UsedImplicitly] object _vibraphasedocs;

        /// <summary>
        /// Generates a mild sawtooth-like waveform by combining multiple sine waves with different frequencies.
        /// </summary>
        /// <returns> An <see cref="FluentOutlet" /> representing the semi-sawtooth waveform. </returns>
        /// <inheritdoc cref="Wishes.Helpers.docs._default" />
        [UsedImplicitly] object _semisawdocs;

        /// <summary> Generates a detuned harmonic sound by altering the frequencies slightly. </summary>
        /// <param name="detuneRate">
        /// The depth of the detuning applied to the harmonics.
        /// If not provided, a default value is used.
        /// </param>
        /// <inheritdoc cref="Wishes.Helpers.docs._default" />
        [UsedImplicitly] object _detunedocs;

        /// <summary>
        /// Applies an echo effect to the given sound.
        /// </summary>
        /// <param name="sound"> The original sound to which the echo effect will be applied. </param>
        /// <returns> An <see cref="FluentOutlet" /> representing the sound with the applied echo effect. </returns>
        [UsedImplicitly] object _echodocs;

        #endregion
    }
}