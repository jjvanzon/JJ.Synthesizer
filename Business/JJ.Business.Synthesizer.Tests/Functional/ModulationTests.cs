using System;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable UnusedMember.Local

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
            : base(beat: 0.55, bar: 2.2)
        {
            Stereo();
        }

        #region Tests

        // Long Running
        /// <inheritdoc cref="docs._detunica" />
        internal void Detunica_Jingle_RunTest() 
            => WithAudioLength(bars[7]).Play(() => DeepEcho(DetunicaJingle, volume: 0.44));

        // Long Running
        /// <inheritdoc cref="docs._detunica" />
        internal void Detunica_Jingle_RunTest_Mono() 
            => Mono().WithAudioLength(bars[7]).Play(() => DeepEcho(DetunicaJingle, volume: 0.15));

        // Long Running
        /// <inheritdoc cref="docs._detunica" />
        internal void DetunicaBass_RunTest()
        {
            var duration = _[3];
            WithAudioLength(duration).Play(() => DeepEcho(DetunicaBass(duration: duration), volume: 0.9));
        }

        /// <inheritdoc cref="docs._detunica" />
        [TestMethod]
        public void Test_Detunica1() => new ModulationTests().Detunica1_RunTest();

        /// <inheritdoc cref="docs._detunica" />
        void Detunica1_RunTest()
        {
            var duration = _[3];
            WithAudioLength(duration).Play(() => DeepEcho(Detunica1(freq: E2, duration: duration), volume: 0.15));
        }

        /// <inheritdoc cref="docs._detunica" />
        [TestMethod]
        public void Test_Detunica2() => new ModulationTests().Detunica2_RunTest();

        /// <inheritdoc cref="docs._detunica" />
        void Detunica2_RunTest()
        {
            var duration = _[3];
            WithAudioLength(duration).Play(() => DeepEcho(Detunica2(freq: B4, duration: duration), volume: 1));
        }

        /// <inheritdoc cref="docs._detunica" />
        [TestMethod]
        public void Test_Detunica3() => new ModulationTests().Detunica3_RunTest();

        /// <inheritdoc cref="docs._detunica" />
        void Detunica3_RunTest()
        {
            var duration = _[3];
            WithAudioLength(duration).Play(() => DeepEcho(Detunica3(freq: C5, duration: duration), volume: 1));
        }

        /// <inheritdoc cref="docs._detunica" />
        [TestMethod]
        public void Test_Detunica4() => new ModulationTests().Detunica4_RunTest();

        /// <inheritdoc cref="docs._detunica" />
        void Detunica4_RunTest()
        {
            var duration = _[3];
            WithAudioLength(duration).Play(() => DeepEcho(Detunica4(freq: D5, duration: duration), volume: 0.25));
        }

        /// <inheritdoc cref="docs._detunica" />
        [TestMethod]
        public void Test_Detunica5() => new ModulationTests().Detunica5_RunTest();

        /// <inheritdoc cref="docs._detunica" />
        void Detunica5_RunTest()
        {
            var duration = _[3];
            WithAudioLength(duration).Play(() => DeepEcho(Detunica5(freq: E5, duration: duration), volume: 0.3));
        }

        /// <inheritdoc cref="_vibraphasedocs" />
        [TestMethod]
        public void Test_Vibraphase_Chord() => new ModulationTests().Vibraphase_Chord_RunTest();

        /// <inheritdoc cref="_vibraphasedocs" />
        void Vibraphase_Chord_RunTest()
            => Mono().Play(() => MildEcho(VibraphaseChord, volume: 0.22));

        /// <inheritdoc cref="_vibraphasedocs" />
        [TestMethod]
        public void Test_Vibraphase() => new ModulationTests().Vibraphase_RunTest();

        /// <inheritdoc cref="_vibraphasedocs" />
        void Vibraphase_RunTest()
            => Mono().Play(() => MildEcho(Vibraphase(freq: E5), volume: 0.5));

        #endregion

        #region Jingles

        /// <inheritdoc cref="_vibraphasedocs" />
        FluentOutlet VibraphaseChord => Add
        (
            Vibraphase(freq: A4, volume: _[0.80]),
            Vibraphase(freq: B4, volume: _[0.70]),
            Vibraphase(freq: C5, volume: _[0.85]),
            Vibraphase(freq: D5, volume: _[0.75]),
            Vibraphase(freq: E5, volume: _[0.90])
        );

        /// <inheritdoc cref="docs._detunica" />
        FluentOutlet DetunicaJingle => Add
        (
            DetunicaBass(bar[1],              bars[5.25]),
            Detunica2   (bar[2], B4, _[0.70], bars[1.50]),
            Detunica3   (bar[3], C5, _[0.75], bars[1.60]),
            Detunica4   (bar[4], D5, _[0.90], bars[1.50]),
            Detunica5   (bar[5], E5, _[1.00], bars[3.00])
        );

        #endregion

        #region Notes

        FluentOutlet DetunicaBass(FluentOutlet delay = null, FluentOutlet duration = null) =>
            Panbrello(
                panbrello: (speed: 2, depth: 0.20),
                sound: Add
                (
                    Detunica1(delay, E0, _[0.600], duration, detuneDepth: _[0.6], chorusRate: _[0.040]),
                    Detunica2(delay, E1, _[0.800], duration),
                    Detunica3(delay, E2, _[1.000], duration),
                    Detunica4(delay, E3, _[0.015], duration),
                    Detunica5(delay, E4, _[0.001], duration))
                );

        /// <inheritdoc cref="docs._detunica" />
        FluentOutlet Detunica1(
            FluentOutlet delay = null, FluentOutlet freq = null, FluentOutlet volume = null, FluentOutlet duration = null,
            FluentOutlet detuneDepth = null, FluentOutlet chorusRate = null)
            => Detunica(
                delay, freq, volume, duration,
                vibrato: (_[3], _[0.00010]),
                tremolo: (_[1], _[0.03]),
                detuneDepth: detuneDepth ?? _[0.8],
                chorusRate: Multiply(chorusRate ?? _[0.03], DetuneRateCurve1),
                envelopeVariation: 2);

        /// <inheritdoc cref="docs._detunica" />
        FluentOutlet Detunica2(FluentOutlet delay = null, FluentOutlet freq = null, FluentOutlet volume = null, FluentOutlet duration = null)
            => MildEcho(
                Detunica(
                    delay, freq, volume, duration,
                    vibrato: (_[10], _[0.00020]),
                    tremolo: (_[12], _[0.10]),
                    detuneDepth: _[1.0],
                    churnRate: Multiply(0.10, DetuneRateCurve2),
                    panning: _[0.4],
                    panbrello: (_[2.6], _[0.09])), volume: 0.7) / 0.7;

        /// <inheritdoc cref="docs._detunica" />
        FluentOutlet Detunica3(FluentOutlet delay = null, FluentOutlet freq = null, FluentOutlet volume = null, FluentOutlet duration = null)
            => Detunica(
                delay, freq, volume, duration,
                vibrato: (_[05.5], _[0.0005]),
                tremolo: (_[15.0], _[0.06]),
                detuneDepth: _[0.5],
                interferenceRate: Multiply(0.002, DetuneRateCurve1),
                chorusRate: Multiply(0.002,       DetuneRateCurve1),
                panning: Stretch(Curve(0.7, 0.3), duration),
                panbrello: (_[4.8], _[0.05]),
                envelopeVariation: 2);

        /// <inheritdoc cref="docs._detunica" />
        FluentOutlet Detunica4(FluentOutlet delay = null, FluentOutlet freq = null, FluentOutlet volume = null, FluentOutlet duration = null)
            => Detunica(
                delay, freq, volume, duration,
                vibrato: (_[7], _[0.0003]),
                tremolo: (_[10], _[0.08]),
                detuneDepth: _[0.5],
                interferenceRate: Multiply(0.003, DetuneRateCurve3),
                panning: Stretch(Curve(0.2, 0.8), duration),
                panbrello: (_[3.4], _[0.07]));

        /// <inheritdoc cref="docs._detunica" />
        FluentOutlet Detunica5(FluentOutlet delay = null, FluentOutlet freq = null, FluentOutlet volume = null, FluentOutlet duration = null)
            => Detunica(
                delay, freq, volume, duration,
                vibrato: (_[5.5], _[0.00005]),
                tremolo: (_[3.0], _[0.25]),
                detuneDepth: _[0.8],
                churnRate: Multiply(0.001, DetuneRateCurve1),
                chorusRate: _[0.001],
                panning: _[0.48]);

        #endregion

        #region Instruments

        /// <inheritdoc cref="docs._detunica" />
        internal FluentOutlet Detunica(
            FluentOutlet delay = default, FluentOutlet freq = default, 
            FluentOutlet volume = default, FluentOutlet duration = default,
            (FluentOutlet speed, FluentOutlet depth) vibrato = default, 
            (FluentOutlet speed, FluentOutlet depth) tremolo = default,
            FluentOutlet detuneDepth = null, FluentOutlet churnRate = null, 
            FluentOutlet interferenceRate = null, FluentOutlet chorusRate = null,
            FluentOutlet panning = default, (FluentOutlet speed, FluentOutlet depth) panbrello = default,
            int envelopeVariation = 1)
        {
            duration = duration ?? _[1];

            if (vibrato != default) freq    = VibratoOverPitch(freq, vibrato);
            var baseHarmonics               = BaseHarmonics(freq);
            var detunedHarmonics            = DetunedHarmonics(freq, duration, churnRate, interferenceRate, chorusRate);
            var sound                       = Add(baseHarmonics, Multiply(detunedHarmonics, detuneDepth));
            if (tremolo != default) sound   = Tremolo(sound, tremolo);
            if (panning != null) sound      = Panning(sound, panning);
            if (panbrello != default) sound = Panbrello(sound, panbrello);
            
            // Apply volume curve
            switch (envelopeVariation)
            {
                case 1:
                    sound = Multiply(sound, PatchyEnvelope.Stretch(duration));
                    break;

                case 2:
                    sound = Multiply(sound, EvenEnvelope.Stretch(duration));
                    break;

                default:
                    throw new Exception($"{nameof(envelopeVariation)} value '{envelopeVariation}' not supported.");
            }

            // Apply velocity and delay
            var note = StrikeNote(sound, delay, volume);

            return note;
        }

        /// <inheritdoc cref="_vibraphasedocs" />
        FluentOutlet Vibraphase(
            FluentOutlet delay = null, FluentOutlet freq = null, 
            FluentOutlet volume = null, FluentOutlet duration = null,
            FluentOutlet depthAdjust1 = null, FluentOutlet depthAdjust2 = null)
        {
            var saw       = SemiSaw(freq);
            var jittered  = Jitter(saw, depthAdjust1, depthAdjust2);
            var enveloped = jittered.Multiply(VibraphaseVolumeCurve.Stretch(duration));
            var note      = enveloped.StrikeNote(delay, volume);
            return note;
        }

        #endregion

        #region WaveForms

        /// <inheritdoc cref="_semisawdocs" />
        FluentOutlet SemiSaw(FluentOutlet freq)
        {
            freq = freq ?? A4;

            return Add
            (
                freq.Times(1).Sine * 1.0,
                freq.Times(2).Sine * 0.5,
                freq.Times(3).Sine * 0.3, 
                freq.Times(4).Sine * 0.2
            );
        }

        FluentOutlet BaseHarmonics(FluentOutlet freq)
        {
            freq = freq ?? A4;

            return Add
            (
                freq.Times(1).Sine * 1.00,
                freq.Times(2).Sine * 0.30,
                freq.Times(5).Sine * 0.15,
                freq.Times(7).Sine * 0.08,
                freq.Times(9).Sine * 0.10
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

        /// <inheritdoc cref="_echodocs" />
        //FluentOutlet MildEcho(FluentOutlet sound) => Echo(sound, MildEchoCount, magnitude: _[0.25], MildEchoDelay);
        FluentOutlet MildEcho(FluentOutlet sound, double volume) => EchoParallel(sound, volume: volume, MildEchoCount, magnitude: _[0.25], MildEchoDelay);

        /// <inheritdoc cref="_echodocs" />
        internal FluentOutlet DeepEcho(FluentOutlet sound, double volume)
        {
            switch (Channel)
            {
                case ChannelEnum.Single: 
                    return sound.EchoParallel(volume, DeepEchoCount, magnitude: _[1 / 2.0], DeepEchoDelayL);
                    //return sound.EchoAdditive(DeepEchoCount, magnitude: _[1 / 2.0], DeepEchoDelayL) * volume;
                
                case ChannelEnum.Left:
                    return sound.EchoParallel(volume, DeepEchoCount, magnitude: _[1 / 2.1], DeepEchoDelayL);
                    //return sound.EchoAdditive(DeepEchoCount, magnitude: _[1 / 2.1], DeepEchoDelayL) * volume;
                
                case ChannelEnum.Right: 
                    return sound.EchoParallel(volume, DeepEchoCount, magnitude: _[1 / 2.0], DeepEchoDelayR);
                    //return sound.EchoAdditive(DeepEchoCount, magnitude: _[1 / 2.0], DeepEchoDelayR) * volume;
                
                default: 
                    throw new ValueNotSupportedException(Channel);
            }
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

        FluentOutlet DetuneRateCurve1 => WithName().Curve(@"
                    o          
                                
                                
        o                   o");

        FluentOutlet DetuneRateCurve2 => WithName().Curve(@"
             o                 
                                
                                
        o                   o ");

        FluentOutlet DetuneRateCurve3 => WithName().Curve(@"
                  o            
                                
                                
        o                   o ");

        FluentOutlet VibraphaseVolumeCurve => WithName().Curve(@"
           o                   
         o   o                 
                                
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