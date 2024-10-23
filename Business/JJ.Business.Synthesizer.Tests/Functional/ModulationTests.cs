using System;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Business.Synthesizer.Tests.Helpers;

#pragma warning disable CS8123 // The tuple element name is ignored because a different name or no name is specified by the assignment target.

namespace JJ.Business.Synthesizer.Tests.Functional
{
    [TestClass]
    [TestCategory("Functional")]
    public class ModulationTests : SynthWishes
    {
        public ModulationTests()
            : base(beat: 0.55, bar: 2.2)
        { }

        #region Tests

        // Long Running
        /// <inheritdoc cref="docs._detunica" />
        internal void Detunica_Jingle_RunTest() 
            => Play(() => DeepEcho(DetunicaJingle), volume: 0.45, duration: bars[7] + DEEP_ECHO_TIME);

        // Long Running
        /// <inheritdoc cref="docs._detunica" />
        internal void Detunica_Jingle_RunTest_Mono() 
            => PlayMono(() => DeepEcho(DetunicaJingle), volume: 0.15, duration: bars[7] + DEEP_ECHO_TIME);

        // Long Running
        /// <inheritdoc cref="docs._detunica" />
        internal void DetunicaBass_RunTest()
            => Play(() => DeepEcho(DetunicaBass(duration: _[3])), duration: 3 + DEEP_ECHO_TIME, volume: 0.9);

        /// <inheritdoc cref="docs._detunica" />
        [TestMethod]
        public void Test_Detunica1() => new ModulationTests().Detunica1_RunTest();

        /// <inheritdoc cref="docs._detunica" />
        void Detunica1_RunTest()
            => Play(() => DeepEcho(Detunica1(freq: E2, duration: _[3])), 3 + DEEP_ECHO_TIME, volume: 0.3);

        /// <inheritdoc cref="docs._detunica" />
        [TestMethod]
        public void Test_Detunica2() => new ModulationTests().Detunica2_RunTest();

        /// <inheritdoc cref="docs._detunica" />
        void Detunica2_RunTest()
            => Play(() => DeepEcho(Detunica2(freq: B4, duration: _[3])), 3 + DEEP_ECHO_TIME, volume: 0.2);

        /// <inheritdoc cref="docs._detunica" />
        [TestMethod]
        public void Test_Detunica3() => new ModulationTests().Detunica3_RunTest();

        /// <inheritdoc cref="docs._detunica" />
        void Detunica3_RunTest()
            => Play(() => DeepEcho(Detunica3(freq: C5, duration: _[3])), 3 + DEEP_ECHO_TIME, volume: 0.4);

        /// <inheritdoc cref="docs._detunica" />
        [TestMethod]
        public void Test_Detunica4() => new ModulationTests().Detunica4_RunTest();

        /// <inheritdoc cref="docs._detunica" />
        void Detunica4_RunTest()
            => Play(() => DeepEcho(Detunica4(freq: D5, duration: _[3])), 3 + DEEP_ECHO_TIME, volume: 0.25);

        /// <inheritdoc cref="docs._detunica" />
        [TestMethod]
        public void Test_Detunica5() => new ModulationTests().Detunica5_RunTest();

        /// <inheritdoc cref="docs._detunica" />
        void Detunica5_RunTest()
            => Play(() => DeepEcho(Detunica5(freq: E5, duration: _[3])), 3 + DEEP_ECHO_TIME, volume: 0.3);

        /// <inheritdoc cref="_vibraphasedocs" />
        [TestMethod]
        public void Test_Vibraphase_Chord() => new ModulationTests().Vibraphase_Chord_RunTest();

        /// <inheritdoc cref="_vibraphasedocs" />
        void Vibraphase_Chord_RunTest()
            => PlayMono(() => MildEcho(VibraphaseChord), volume: 0.25, duration: 1 + MILD_ECHO_TIME);

        /// <inheritdoc cref="_vibraphasedocs" />
        [TestMethod]
        public void Test_Vibraphase() => new ModulationTests().Vibraphase_RunTest();

        /// <inheritdoc cref="_vibraphasedocs" />
        void Vibraphase_RunTest()
            => PlayMono(() => MildEcho(Vibraphase(freq: E5)), duration: 1 + MILD_ECHO_TIME, volume: 0.5);

        #endregion

        #region Jingles

        /// <inheritdoc cref="_vibraphasedocs" />
        Outlet VibraphaseChord => Add
        (
            Vibraphase(freq: A4, volume: _[0.80]),
            Vibraphase(freq: B4, volume: _[0.70]),
            Vibraphase(freq: C5, volume: _[0.85]),
            Vibraphase(freq: D5, volume: _[0.75]),
            Vibraphase(freq: E5, volume: _[0.90])
        );

        /// <inheritdoc cref="docs._detunica" />
        Outlet DetunicaJingle => Add
        (
            DetunicaBass(bar[1],              bars[5.25]),
            Detunica2   (bar[2], B4, _[0.70], bars[1.50]),
            Detunica3   (bar[3], C5, _[0.75], bars[1.60]),
            Detunica4   (bar[4], D5, _[0.90], bars[1.50]),
            Detunica5   (bar[5], E5, _[1.00], bars[3.00])
        );

        #endregion

        #region Notes

        Outlet DetunicaBass(Outlet delay = null, Outlet duration = null) =>
            Panbrello(
                panbrello: (speed: _[2], depth: _[0.20]),
                sound: Add(
                    Detunica1(delay, E0, _[0.600], duration, detuneDepth: _[0.6], chorusRate: _[0.040]),
                    Detunica2(delay, E1, _[0.800], duration), // TODO: Maybe don't use this churning sound.
                    Detunica3(delay, E2, _[1.000], duration),
                    Detunica4(delay, E3, _[0.015], duration),
                    Detunica5(delay, E4, _[0.001], duration)));

        /// <inheritdoc cref="docs._detunica" />
        Outlet Detunica1(
            Outlet delay = null, Outlet freq = null, Outlet volume = null, Outlet duration = null,
            Outlet detuneDepth = null, Outlet chorusRate = null)
            => Detunica(
                delay, freq, volume, duration,
                vibrato: (_[3], _[0.00010]),
                tremolo: (_[1], _[0.03]),
                detuneDepth: detuneDepth ?? _[0.8],
                chorusRate: Multiply(chorusRate ?? _[0.03], DetuneRateCurve1),
                envelopeVariation: 2);

        /// <inheritdoc cref="docs._detunica" />
        Outlet Detunica2(Outlet delay = null, Outlet freq = null, Outlet volume = null, Outlet duration = null)
            => MildEcho(
                Detunica(
                    delay, freq, volume, duration,
                    vibrato: (_[10], _[0.00020]),
                    tremolo: (_[12], _[0.10]),
                    detuneDepth: _[1.0],
                    churnRate: Multiply(_[0.10], DetuneRateCurve2),
                    panning: _[0.4], 
                    panbrello: (_[2.6], _[0.09])));

        /// <inheritdoc cref="docs._detunica" />
        Outlet Detunica3(Outlet delay = null, Outlet freq = null, Outlet volume = null, Outlet duration = null)
            => Detunica(
                delay, freq, volume, duration,
                vibrato: (_[05.5], _[0.0005]),
                tremolo: (_[15.0], _[0.06]),
                detuneDepth     : _[0.5],
                interferenceRate: Multiply(_[0.002], DetuneRateCurve1),
                chorusRate      : Multiply(_[0.002], DetuneRateCurve1),
                panning: Stretch(CurveIn(0.7, 0.3), duration), 
                panbrello: (_[4.8], _[0.05]),
                envelopeVariation: 2);

        /// <inheritdoc cref="docs._detunica" />
        Outlet Detunica4(Outlet delay = null, Outlet freq = null, Outlet volume = null, Outlet duration = null)
            => Detunica(
                delay, freq, volume, duration,
                vibrato: (_[7], _[0.0003]),
                tremolo: (_[10], _[0.08]),
                detuneDepth: _[0.5],
                interferenceRate: Multiply(_[0.003], DetuneRateCurve3),
                panning: Stretch(CurveIn(0.2, 0.8), duration),
                panbrello: (_[3.4], _[0.07]));

        /// <inheritdoc cref="docs._detunica" />
        Outlet Detunica5(Outlet delay = null, Outlet freq = null, Outlet volume = null, Outlet duration = null)
            => Detunica(
                delay, freq, volume, duration,
                vibrato: (_[5.5], _[0.00005]),
                tremolo: (_[3.0], _[0.25]),
                detuneDepth: _[0.8],
                churnRate: Multiply(_[0.001], DetuneRateCurve1),
                chorusRate: _[0.001],
                panning: _[0.48]);

        #endregion

        #region Instruments

        /// <inheritdoc cref="docs._detunica" />
        internal Outlet Detunica(
            Outlet delay = default, Outlet freq = default, Outlet volume = default, Outlet duration = default,
            (Outlet speed, Outlet depth) vibrato = default, 
            (Outlet speed, Outlet depth) tremolo = default,
            Outlet detuneDepth = null, Outlet churnRate = null, Outlet interferenceRate = null, Outlet chorusRate = null,
            Outlet panning = default, (Outlet speed, Outlet depth) panbrello = default,
            int envelopeVariation = 1)
        {
            duration = duration ?? _[1];

            if (vibrato != default) freq    = VibratoOverPitch(freq, vibrato);
            var    baseHarmonics            = BaseHarmonics(freq);
            var    detunedHarmonics         = DetunedHarmonics(freq, duration, churnRate, interferenceRate, chorusRate);
            Outlet sound                    = Add(baseHarmonics, Multiply(detunedHarmonics, detuneDepth));
            if (tremolo != default) sound   = Tremolo(sound, tremolo);
            if (panning != null) sound      = Panning(sound, panning);
            if (panbrello != default) sound = Panbrello(sound, panbrello);
            
            // Apply volume curve
            switch (envelopeVariation)
            {
                case 1:
                    sound = Multiply(sound, Stretch(DetunicaPatchyVolumeCurve, duration));
                    break;

                case 2:
                    sound = Multiply(sound, Stretch(DetunicaEvenVolumeCurve, duration));
                    break;

                default:
                    throw new Exception($"{nameof(envelopeVariation)} value '{envelopeVariation}' not supported.");
            }

            // Apply velocity and delay
            var note = StrikeNote(sound, delay, volume);

            return note;
        }

        /// <inheritdoc cref="_vibraphasedocs" />
        Outlet Vibraphase(
            Outlet delay = null, Outlet freq = null, Outlet volume = null, Outlet duration = null,
            Outlet depthAdjust1 = null, Outlet depthAdjust2 = null)
        {
            var saw       = SemiSaw(freq);
            var jittered  = Jitter(saw, depthAdjust1, depthAdjust2);
            var enveloped = Multiply(jittered, Stretch(VibraphaseVolumeCurve, duration));
            var note      = StrikeNote(enveloped, delay, volume);
            return note;
        }

        #endregion

        #region WaveForms

        /// <inheritdoc cref="_semisawdocs" />
        Outlet SemiSaw(Outlet freq)
        {
            freq = freq ?? A4;

            return Add
            (
                Multiply(Sine(Multiply(freq, _[1])), _[1.0]),
                Multiply(Sine(Multiply(freq, _[2])), _[0.5]),
                Multiply(Sine(Multiply(freq, _[3])), _[0.3]), 
                Multiply(Sine(Multiply(freq, _[4])), _[0.2])
            );
        }

        /// <inheritdoc cref="_semisawdocs" />
        Outlet BaseHarmonics(Outlet freq)
        {
            freq = freq ?? A4;

            return Add
            (
                Multiply(Sine(Multiply(freq, _[1])), _[1.00]),
                Multiply(Sine(Multiply(freq, _[2])), _[0.30]),
                Multiply(Sine(Multiply(freq, _[5])), _[0.15]),
                Multiply(Sine(Multiply(freq, _[7])), _[0.08]),
                Multiply(Sine(Multiply(freq, _[9])), _[0.10])
            );
        }

        /// <inheritdoc cref="_detunedocs" />
        Outlet DetunedHarmonics(
            Outlet freq, Outlet duration,
            Outlet churnRate = null, Outlet interferenceRate = null, Outlet chorusRate = null)
        {
            freq = freq ?? A4;

            return Add
            (
                Multiply(_[1.00], Sine(DetuneFreq(freq, _[1], duration, churnRate, interferenceRate, chorusRate))),
                Multiply(_[0.30], Sine(DetuneFreq(freq, _[2], duration, churnRate, interferenceRate, chorusRate))),
                Multiply(_[0.15], Sine(DetuneFreq(freq, _[5], duration, churnRate, interferenceRate, chorusRate))),
                Multiply(_[0.08], Sine(DetuneFreq(freq, _[7], duration, churnRate, interferenceRate, chorusRate))),
                Multiply(_[0.10], Sine(DetuneFreq(freq, _[9], duration, churnRate, interferenceRate, chorusRate)))
            );
        }

        #endregion

        #region Effects

        /// <inheritdoc cref="_detunedocs" />
        Outlet DetuneFreq(
            Outlet freq, Outlet harmonic, Outlet duration,
            Outlet churnRate = null, Outlet interfereRate = null, Outlet chorusRate = null)
        {
            Outlet detunedFreq = freq;

            // Add to harmonic number = churn / heavy interference
            if (churnRate != null)
            {
                Outlet detunedHarmonic = Add(harmonic, Stretch(churnRate, duration));
                detunedFreq = Multiply(detunedFreq, detunedHarmonic);
            }

            // Add Hz = light interference
            if (interfereRate != null)
            {
                detunedFreq = Add(detunedFreq, Stretch(interfereRate, duration));
            }

            // Multiply by 1 + depth = chorus
            if (chorusRate != null)
            {
                detunedFreq = Multiply(detunedFreq, Add(_[1], Stretch(chorusRate, duration)));
            }

            return detunedFreq;
        }

        /// <inheritdoc cref="_vibraphasedocs" />
        Outlet Jitter(Outlet sound, Outlet depthAdjust1 = null, Outlet depthAdjust2 = null)
        {
            depthAdjust1 = depthAdjust1 ?? _[0.005];
            depthAdjust2 = depthAdjust2 ?? _[0.250];

            var tremoloWave1 = Multiply(Add(_[1], depthAdjust1), Sine(_[5.5])); // 5.5 Hz _tremolo
            sound = Multiply(sound, tremoloWave1);

            var tremoloWave2 = Multiply(Add(_[1], depthAdjust2), Sine(_[4])); // 4 Hz _tremolo
            sound = Multiply(sound, tremoloWave2);

            return sound;
        }

        const double MILD_ECHO_TIME = 0.33 * 3;

        /// <inheritdoc cref="_echodocs" />
        Outlet MildEcho(Outlet sound)
            => Echo(sound, count: 4, magnitude: _[0.25], delay: _[0.33]);

        const double DEEP_ECHO_TIME = 0.5 * 3;

        /// <inheritdoc cref="_echodocs" />
        internal Outlet DeepEcho(Outlet sound)
        {
            switch (Channel)
            {
                case ChannelEnum.Single:
                    return Echo(sound, count: 4, magnitude: _[1 / 2.0], delay: _[0.50]);
                
                case ChannelEnum.Left:
                    return Echo(sound, count: 4, magnitude: _[1 / 2.1], delay: _[0.50]);
                
                case ChannelEnum.Right:
                    return Echo(sound, count: 4, magnitude: _[1 / 2.0], delay: _[0.53]);
                
                default:
                    throw new ValueNotSupportedException(Channel);
            }
        }

        #endregion

        #region Curves

        Outlet DetunicaPatchyVolumeCurve => CurveIn(@"
                         o                             
                    
                              o                         
            o       o
                o                                     

                                     o                   

        o                                       o ");

        Outlet DetunicaEvenVolumeCurve => CurveIn(@"
                          o                             
                   
                                                        
          o                             o
                                                      
                                    
                                     
           
        o                                       o ");

        Outlet DetuneRateCurve1 => CurveIn(@"
                    o          
                                
                                
        o                   o");

        Outlet DetuneRateCurve2 => CurveIn(@"
             o                 
                                
                                
        o                   o ");

        Outlet DetuneRateCurve3 => CurveIn(@"
                  o            
                                
                                
        o                   o ");

        Outlet VibraphaseVolumeCurve => CurveIn(@"
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
        /// <returns> An <see cref="Outlet" /> representing the semi-sawtooth waveform. </returns>
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
        /// <returns> An <see cref="Outlet" /> representing the sound with the applied echo effect. </returns>
        [UsedImplicitly] object _echodocs;

        #endregion
    }
}