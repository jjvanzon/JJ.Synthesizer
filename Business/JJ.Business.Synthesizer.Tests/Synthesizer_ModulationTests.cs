#pragma warning disable CS0169 // Field is never used
// ReSharper disable InconsistentNaming

using System;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Tests.Wishes;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class Synthesizer_ModulationTests : SynthesizerSugarBase
    {
        [UsedImplicitly]
        public Synthesizer_ModulationTests()
        { }

        Synthesizer_ModulationTests(IContext context)
            : base(context, beat: 0.5, bar: 2.0)
        {
            CreateCurves();
        }

        #region Tests

        /// <inheritdoc cref="_detunicadocs" />
        [TestMethod]
        public void Test_Synthesizer_Modulation_Detunica_Jingle()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new Synthesizer_ModulationTests(context).Test_Modulation_Detunica_Jingle();
        }

        /// <inheritdoc cref="_detunicadocs" />
        void Test_Modulation_Detunica_Jingle()
            => SaveWav(DeepEcho(DetunicaJingle), volume: 0.08, duration: bars[4] + bars[2.5] + DEEP_ECHO_TIME);

        /// <inheritdoc cref="_detunicadocs" />
        [TestMethod]
        public void Test_Synthesizer_Modulation_Detunica1()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new Synthesizer_ModulationTests(context).Test_Modulation_Detunica1();
        }

        /// <inheritdoc cref="_detunicadocs" />
        void Test_Modulation_Detunica1()
            => SaveWav(DeepEcho(Detunica1(freq: _[Notes.A3], duration: _[3])), 3 + DEEP_ECHO_TIME, volume: 0.1);

        /// <inheritdoc cref="_detunicadocs" />
        [TestMethod]
        public void Test_Synthesizer_Modulation_Detunica2()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new Synthesizer_ModulationTests(context).Test_Modulation_Detunica2();
        }

        /// <inheritdoc cref="_detunicadocs" />
        void Test_Modulation_Detunica2()
            => SaveWav(DeepEcho(Detunica2(freq: _[Notes.B4], duration: _[3])), 3 + DEEP_ECHO_TIME, volume: 0.1);

        /// <inheritdoc cref="_detunicadocs" />
        [TestMethod]
        public void Test_Synthesizer_Modulation_Detunica3()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new Synthesizer_ModulationTests(context).Test_Modulation_Detunica3();
        }

        /// <inheritdoc cref="_detunicadocs" />
        void Test_Modulation_Detunica3()
            => SaveWav(DeepEcho(Detunica3(freq: _[Notes.C5], duration: _[3])), 3 + DEEP_ECHO_TIME, volume: 0.1);

        /// <inheritdoc cref="_detunicadocs" />
        [TestMethod]
        public void Test_Synthesizer_Modulation_Detunica4()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new Synthesizer_ModulationTests(context).Test_Modulation_Detunica4();
        }

        /// <inheritdoc cref="_detunicadocs" />
        void Test_Modulation_Detunica4()
            => SaveWav(DeepEcho(Detunica4(freq: _[Notes.D5], duration: _[3])), 3 + DEEP_ECHO_TIME, volume: 0.1);

        /// <inheritdoc cref="_detunicadocs" />
        [TestMethod]
        public void Test_Synthesizer_Modulation_Detunica5()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new Synthesizer_ModulationTests(context).Test_Modulation_Detunica5();
        }

        /// <inheritdoc cref="_detunicadocs" />
        void Test_Modulation_Detunica5()
            => SaveWav(DeepEcho(Detunica5(freq: _[Notes.E5], duration: _[3])), 3 + DEEP_ECHO_TIME, volume: 0.1);


        /// <inheritdoc cref="_vibraphasedocs" />
        [TestMethod]
        public void Test_Synthesizer_Modulation_Vibraphase_Chord()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new Synthesizer_ModulationTests(context).Test_Modulation_Vibraphase_Chord();
        }

        /// <inheritdoc cref="_vibraphasedocs" />
        void Test_Modulation_Vibraphase_Chord()
            => SaveWav(MildEcho(VibraphaseChord), volume: 0.30, duration: 1 + MILD_ECHO_TIME);

        /// <inheritdoc cref="_vibraphasedocs" />
        [TestMethod]
        public void Test_Synthesizer_Modulation_Vibraphase()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new Synthesizer_ModulationTests(context).Test_Modulation_Vibraphase();
        }

        /// <inheritdoc cref="_vibraphasedocs" />
        void Test_Modulation_Vibraphase()
            => SaveWav(MildEcho(Vibraphase(freq: _[Notes.E5])), duration: 1 + MILD_ECHO_TIME);

        /// <inheritdoc cref="_vibratodocs" />
        [TestMethod]
        public void Test_Synthesizer_Modulation_Vibrato()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new Synthesizer_ModulationTests(context).Test_Modulation_Vibrato();
        }

        /// <inheritdoc cref="_vibratodocs" />
        void Test_Modulation_Vibrato()
            => SaveWav(
                Sine(volume: _[1], VibratoOverPitch(_[Notes.A4])),
                volume: 0.9, duration: 3);

        /// <inheritdoc cref="_tremolodocs" />
        [TestMethod]
        public void Test_Synthesizer_Modulation_Tremolo()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new Synthesizer_ModulationTests(context).Test_Modulation_Tremolo();
        }

        /// <inheritdoc cref="_tremolodocs" />
        void Test_Modulation_Tremolo()
            => SaveWav(
                Tremolo(Sine(volume: _[1], _[Notes.A4]), tremoloSpeed: _[4], tremoloDepth: _[0.5]),
                volume: 0.30, duration: 3);

        #endregion

        #region Jingles

        /// <inheritdoc cref="_detunicadocs" />
        Outlet DetunicaJingle => Adder
        (
            Detunica1(bar[1], _[Notes.A3], _[1.00], duration: bars[2.75]),
            Detunica2(bar[2], _[Notes.B4], _[0.70], duration: bars[1.25]),
            Detunica3(bar[3], _[Notes.C5], _[0.85], duration: bars[1.75]),
            Detunica4(bar[4], _[Notes.D5], _[0.75], duration: bars[2.00]),
            Detunica5(bar[5], _[Notes.E5], _[0.90], duration: bars[2.50])
        );

        /// <inheritdoc cref="_detunicadocs" />
        Outlet Detunica1(Outlet delay = null, Outlet freq = null, Outlet volume = null, Outlet duration = null) 
            => Detunica(
                delay, freq, volume, duration, 
                tremoloSpeed: _[4.0], tremoloDepth: _[0.05],
                vibratoSpeed: _[5.5], vibratoDepth: _[0.0001],
                detuneDepth: Multiply(CurveIn(DetuneCurve1), _[0.03]));

        /// <inheritdoc cref="_detunicadocs" />
        Outlet Detunica2(Outlet delay = null, Outlet freq = null, Outlet volume = null, Outlet duration = null) 
            => Detunica(
                delay, freq, volume, duration, 
                tremoloSpeed: _[12.0], tremoloDepth: _[0.10],
                vibratoSpeed: _[06.5], vibratoDepth: _[0.0002],
                detuneDepth: Multiply(CurveIn(DetuneCurve2), _[0.10]));

        /// <inheritdoc cref="_detunicadocs" />
        Outlet Detunica3(Outlet delay = null, Outlet freq = null, Outlet volume = null, Outlet duration = null) 
            => Detunica(
                delay, freq, volume, duration,
                tremoloSpeed: _[12], tremoloDepth: _[0.20],
                vibratoSpeed: _[08], vibratoDepth: _[0.0003],
                detuneDepth: Multiply(CurveIn(DetuneCurve2), _[0.05]));

        /// <inheritdoc cref="_detunicadocs" />
        Outlet Detunica4(Outlet delay = null, Outlet freq = null, Outlet volume = null, Outlet duration = null) 
            => Detunica(
                delay, freq, volume, duration,
                tremoloSpeed: _[30.0], tremoloDepth: _[0.10],
                vibratoSpeed: _[05.5], vibratoDepth: _[0.0005],
                detuneDepth: Multiply(CurveIn(DetuneCurve3), _[0.02]));

        /// <inheritdoc cref="_detunicadocs" />
        Outlet Detunica5(Outlet delay = null, Outlet freq = null, Outlet volume = null, Outlet duration = null) 
            => Detunica(
                delay, freq, volume, duration, 
                tremoloSpeed: _[3.0], tremoloDepth: _[0.25],
                vibratoSpeed: _[5.5], vibratoDepth: _[0.00005],
                detuneDepth: Multiply(CurveIn(DetuneCurve1), _[0.001]));

        /// <inheritdoc cref="_vibraphasedocs" />
        Outlet VibraphaseChord => Adder
        (
            Vibraphase(freq: _[Notes.A4], volume: _[0.80]),
            Vibraphase(freq: _[Notes.B4], volume: _[0.70]),
            Vibraphase(freq: _[Notes.C5], volume: _[0.85]),
            Vibraphase(freq: _[Notes.D5], volume: _[0.75]),
            Vibraphase(freq: _[Notes.E5], volume: _[0.90])
        );

        #endregion

        #region Instruments

        /// <inheritdoc cref="_detunicadocs" />
        Outlet Detunica(
            Outlet delay = null, Outlet freq = null, Outlet volume = null, Outlet duration = null,
            Outlet vibratoSpeed = null, Outlet vibratoDepth = null, Outlet tremoloSpeed = null, Outlet tremoloDepth = null, 
            Outlet detuneDepth = null)
        {
            duration = duration ?? _[1];

            var vibratoPitch = VibratoOverPitch(freq, vibratoSpeed, vibratoDepth);

            // Base additive synthesis waveform
            var semiSaw = SemiSaw(vibratoPitch);

            // Apply detune by modulating harmonic frequencies slightly
            var stretchedDetune = TimeMultiply(detuneDepth, duration);
            var detunedHarmonics = DetunedHarmonics(vibratoPitch, stretchedDetune);

            // Mix them together
            Outlet sound = Add(semiSaw, detunedHarmonics);

            sound = Tremolo(sound, tremoloSpeed, tremoloDepth);

            // Apply volume curve
            sound = Multiply(sound, StretchCurve(DetunicaVolumeCurve, duration));

            // Apply velocity and delay
            var note = StrikeNote(sound, delay, volume);

            return note;
        }

        /// <inheritdoc cref="_vibraphasedocs" />
        Outlet Vibraphase(
            Outlet delay = null, Outlet freq = null, Outlet volume = null, Outlet duration = null,
            Outlet depthAdjust1 = null, Outlet depthAdjust2 = null)
        {
            var saw = SemiSaw(freq);
            var jittered = Jitter(saw, depthAdjust1, depthAdjust2);
            var enveloped = Multiply(jittered, StretchCurve(VibraphaseVolumeCurve, duration));
            var note = StrikeNote(enveloped, delay, volume);
            return note;
        }

        #endregion

        #region WaveForms

        /// <inheritdoc cref="_semisawdocs" />
        Outlet SemiSaw(Outlet freq)
        {
            freq = freq ?? _[440];

            return Adder
            (
                Sine(_[1.0], freq),
                Sine(_[0.5], Multiply(freq, _[2])),
                Sine(_[0.3], Multiply(freq, _[3])),
                Sine(_[0.2], Multiply(freq, _[4]))
            );
        }

        /// <inheritdoc cref="_detunedharmonicsdocs" />
        Outlet DetunedHarmonics(Outlet freq, Outlet detuneDepth = null)
        {
            freq = freq ?? _[440];
            detuneDepth = detuneDepth ?? _[0.02];

            return Adder
            (
                Sine(_[1], Multiply(freq, Add(_[1], detuneDepth))),
                Sine(_[0.8], Multiply(freq, Add(_[2], detuneDepth))),
                Sine(_[0.6], Multiply(freq, Add(_[3], detuneDepth))),
                Sine(_[0.4], Multiply(freq, Add(_[4], detuneDepth)))
            );
        }

        #endregion

        #region Effects

        /// <inheritdoc cref="_vibratodocs" />
        Outlet VibratoOverPitch(Outlet freq, Outlet vibratoSpeed = null, Outlet vibratoDepth = null)
        {
            vibratoSpeed = vibratoSpeed ?? _[5.5];
            vibratoDepth = vibratoDepth ?? _[0.0005];

            var vibratoPitch = Multiply(freq, Add(_[1], Sine(vibratoDepth, vibratoSpeed)));

            return vibratoPitch;
        }

        /// <inheritdoc cref="_tremolodocs" />
        Outlet Tremolo(Outlet sound, Outlet tremoloSpeed, Outlet tremoloDepth)
        {
            tremoloSpeed = tremoloSpeed ?? _[8];
            tremoloDepth = tremoloDepth ?? _[0.33];

            var wave = Add(Sine(tremoloDepth, tremoloSpeed), _[1]);
            sound = Multiply(sound, wave);
            return sound;
        }

        /// <inheritdoc cref="_vibraphasedocs" />
        Outlet Jitter(Outlet sound, Outlet depthAdjust1 = null, Outlet depthAdjust2 = null)
        {
            depthAdjust1 = depthAdjust1 ?? _[0.005];
            depthAdjust2 = depthAdjust2 ?? _[0.250];

            var tremoloWave1 = Sine(Add(_[1], depthAdjust1), _[5.5]); // 5.5 Hz tremolo
            sound = Multiply(sound, tremoloWave1);

            var tremoloWave2 = Sine(Add(_[1], depthAdjust2), _[4]); // 4 Hz tremolo
            sound = Multiply(sound, tremoloWave2);

            return sound;
        }

        const double MILD_ECHO_TIME = 0.33 * 5;

        /// <inheritdoc cref="_echodocs" />
        Outlet MildEcho(Outlet sound)
            => EntityFactory.CreateEcho(this, sound, count: 6, denominator: 4, delay: 0.33);

        const double DEEP_ECHO_TIME = 0.5 * 5;

        /// <inheritdoc cref="_echodocs" />
        Outlet DeepEcho(Outlet melody)
            => EntityFactory.CreateEcho(this, melody, count: 6, denominator: 2, delay: 0.5);

        #endregion

        #region Curves

        Curve DetunicaVolumeCurve;
        Curve DetuneCurve1;
        Curve DetuneCurve2;
        Curve DetuneCurve3;
        Curve VibraphaseVolumeCurve;

        void CreateCurves()
        {
            DetunicaVolumeCurve = Curves.Create(@"
                         o                             
              o      o       o                         
                                                       
                  o                o                   
            o                                       o ");

            DetuneCurve1 = Curves.Create(@"
                        o          
                                    
                                    
            o                   o ");

            DetuneCurve2 = Curves.Create(@"
                 o                 
                                    
                                    
            o                   o ");

            DetuneCurve3 = Curves.Create(@"
                      o            
                                    
                                    
            o                   o ");

            VibraphaseVolumeCurve = Curves.Create(@"
               o                   
             o   o                 
                                    
                       o           
            o                   o ");
        }

        #endregion

        #region Docs

        /// <summary>
        /// A detuned note characterized by a rich and slightly eerie sound due to the detuned harmonics.
        /// It produces a haunting and spacious tone with subtle shifts in pitch.
        /// </summary>
        /// <param name="vibratoDepth"> Reserved for vibrato effect (frequency modulation) but not yet implemented. </param>
        /// <param name="detuneDepth">
        /// The detune depth, adjusting the harmonic frequencies relative to the base frequency,
        /// creating a subtle dissonance and eerie quality.
        /// </param>
        /// <inheritdoc cref="DocComments.Default" />
        object _detunicadocs;

        /// <summary>
        /// Applies a jitter effect to notes, with adjustable depths.
        /// Basically with an extreme double tremolo effect, that goes into the negative.
        /// That can also cause a phasing effect due to constructive and destructive interference
        /// when playing multiple notes at the same time.
        /// </summary>
        /// <param name="sound"> The sound to apply the jitter effect to. </param>
        /// <param name="depthAdjust1"> The first depth adjustment for the jitter effect. Defaults to 0.005 if not provided. </param>
        /// <param name="depthAdjust2"> The second depth adjustment for the jitter effect. Defaults to 0.250 if not provided. </param>
        /// <inheritdoc cref="DocComments.Default" />
        object _vibraphasedocs;

        /// <summary>
        /// Generates a mild sawtooth-like waveform by combining multiple sine waves with different frequencies.
        /// </summary>
        /// <returns> An <see cref="Outlet" /> representing the semi-sawtooth waveform. </returns>
        /// <inheritdoc cref="DocComments.Default" />
        object _semisawdocs;

        /// <summary> Generates a detuned harmonic sound by altering the frequencies slightly. </summary>
        /// <param name="detuneDepth">
        /// The depth of the detuning applied to the harmonics.
        /// If not provided, a default value is used.
        /// </param>
        /// <inheritdoc cref="DocComments.Default" />
        object _detunedharmonicsdocs;

        /// <summary>
        /// Applies vibrato modulation to a given frequency by modulating it with a sine wave.<br/>
        /// NOTE: Due to the lack of phase tracking, the vibrato depth tends to accumulate over time.
        /// </summary>
        /// <param name="freq">The base frequency to which vibrato will be applied.</param>
        /// <returns>An <see cref="Outlet"/> object representing the frequency modulated with vibrato.</returns>
        /// <inheritdoc cref="DocComments.Default"/>
        object _vibratodocs;

        /// <summary> Apply tremolo by modulating amplitude over time using an oscillator. </summary>
        /// <inheritdoc cref="DocComments.Default" />
        object _tremolodocs;

        /// <summary>
        /// Applies an echo effect to the given sound.
        /// </summary>
        /// <param name="sound"> The original sound to which the echo effect will be applied. </param>
        /// <returns> An <see cref="Outlet" /> representing the sound with the applied echo effect. </returns>
        object _echodocs;

        #endregion
    }
}