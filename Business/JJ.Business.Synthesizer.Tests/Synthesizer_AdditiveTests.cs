using System;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Tests.Wishes;
using JJ.Business.Synthesizer.Validation.Entities;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable LocalizableElement

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    /// <summary>
    /// Additional tests written upon retro-actively isolating older synthesizer versions.
    /// </summary>
    public class Synthesizer_AdditiveTests : SynthesizerSugarBase
    {
        const double DEFAULT_NOTE_DURATION = 2.5;

        /// <summary> Constructor for test runner. </summary>
        [UsedImplicitly]
        public Synthesizer_AdditiveTests()
        { }

        /// <summary> Constructor allowing each test to run in its own instance. </summary>
        public Synthesizer_AdditiveTests(IContext context)
            : base(context, beat: 0.4, bar: 1.6)
        { }

        #region Tests

        [TestMethod]
        public void Test_Synthesizer_Additive_Sines_Samples_Metallophone_Melody()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new Synthesizer_AdditiveTests(context).Test_Additive_Sines_Samples_Metallophone_Melody();
        }

        /// <summary>
        /// Arpeggio sound with harmonics, a high-pitch sample for attack,
        /// separate curves for each partial, triggers a wav header auto-detect.
        /// </summary>
        public void Test_Additive_Sines_Samples_Metallophone_Melody()
        {
            AssertEntities();

            SaveWav(
                AddEcho(Melody),
                volume: 0.3,
                duration: 1.2 + DEFAULT_NOTE_DURATION + ECHO_TIME);
        }

        [TestMethod]
        public void Test_Synthesizer_Additive_Sines_Samples_Metallophone_Note()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new Synthesizer_AdditiveTests(context).Test_Additive_Sines_Samples_Metallophone_Note();
        }

        public void Test_Additive_Sines_Samples_Metallophone_Note()
        {
            AssertEntities();

            SaveWav(
                AddEcho(Metallophone(_[Notes.F4_Sharp])),
                duration: DEFAULT_NOTE_DURATION + ECHO_TIME,
                volume: 0.5);
        }

        /// <summary> Assert some entities that WriteToAudioFile won't. </summary>
        private void AssertEntities()
        {
            Samples.ValidateSample(GetSample()).Verify();
        }

        #endregion

        #region Patches

        private Outlet Melody => Adder
        (
            Metallophone(_[Notes.A4],       delay: t[bar:1, beat:1.0], volume: _[0.9]),
            Metallophone(_[Notes.E5],       delay: t[bar:1, beat:1.5], volume: _[1.0]),
            Metallophone(_[Notes.B4],       delay: t[bar:1, beat:2.0], volume: _[0.5]),
            Metallophone(_[Notes.C5_Sharp], delay: t[bar:1, beat:2.5], volume: _[0.7]),
            Metallophone(_[Notes.F4_Sharp], delay: t[bar:1, beat:4.0], volume: _[0.4])
        );

        /// <param name="duration"> The duration of the sound in seconds (default is 2.5). </param>
        /// <inheritdoc cref="docs._default" />
        private Outlet Metallophone(Outlet frequency = null, Outlet volume = null, Outlet delay = null, Outlet duration = null)
        {
            frequency = frequency ?? _[Notes.A4];
            duration = duration ?? _[DEFAULT_NOTE_DURATION];

            var sound = Adder
            (
                SinePartial(           frequency,        volume: _[1.0], Stretch(SinePartialCurve1,  duration)),
                SinePartial(  Multiply(frequency, _[2]), volume: _[0.7], Stretch(SinePartialCurve2,  duration)),
                SinePartial(  Multiply(frequency, _[5]), volume: _[0.4], Stretch(SinePartialCurve3,  duration)),
                SamplePartial(Multiply(frequency, _[2]), volume: _[3.0], Stretch(SamplePartialCurve, duration)),
                SamplePartial(Multiply(frequency, _[7]), volume: _[5.0], Stretch(SamplePartialCurve, duration))
            );

            return StrikeNote(sound, delay, volume);
        }

        private Outlet SinePartial(Outlet frequency, Outlet volume, Outlet curve)
            => Sine(Multiply(volume, curve), frequency);

        private Outlet SamplePartial(Outlet frequency, Outlet volume, Outlet curve)
            => TimeDivide
            (
                Multiply(Multiply(Sample(_sample), curve), volume),
                Divide(frequency, _[440])
            );

        private const double ECHO_TIME = 0.66 * 4;

        /// <inheritdoc cref="docs._default" />
        private Outlet AddEcho(Outlet sound)
            => EntityFactory.CreateEcho(this, sound, count: 5, denominator: 3, delay: 0.66);

        #endregion

        #region Samples

        private Sample _sample;

        /// <summary>
        /// Load a sample, skip some old header's bytes, maximize volume and tune to 440Hz.
        /// </summary>
        private Sample GetSample()
        {
            if (_sample != null) return _sample;

            _sample = Samples.CreateSample(TestHelper.GetViolin16BitMono44100WavStream());

            // Skip over Header (from some other file format, that slipped into the audio data).
            _sample.BytesToSkip = 62;

            // Skip for Sharper Attack
            _sample.BytesToSkip += 1000;

            // Maximize and Normalize sample values (from 16-bit numbers to [-1, 1]).
            _sample.Amplifier = 1.467 / short.MaxValue;

            // Tune to A 440Hz
            double octaveFactor = Math.Pow(2, -1);
            double intervalFactor = 4.0 / 5.0;
            double fineTuneFactor = 0.94;
            _sample.TimeMultiplier = 1.0 / (octaveFactor * intervalFactor * fineTuneFactor);

            return _sample;
        }

        #endregion

        #region Curves

        /// <summary>
        /// Creates a curve representing the volume modulation for the first sine partial.
        /// Starts quietly, peaks at a strong volume, and then fades gradually.
        /// </summary>
        Outlet SinePartialCurve1 => CurveIn
        (
            "SinePartialCurve1",
            timeSpan: 1,
            0.00, 0.80, 1.00, null, null, null, null, null,
            0.25, null, null, null, null, null, null, null,
            0.10, null, null, 0.02, null, null, null, 0.00
        );

        /// <summary>
        /// Creates a curve for volume modulation of the second sine partial.
        /// Begins with a quick rise, reaches a high peak, and then slightly drops before fading.
        /// </summary>
        Outlet SinePartialCurve2 => CurveIn
        (
            "SinePartialCurve2",
            timeSpan: 1,
            0.00, 1.00, 0.80, null, null, null, null, null,
            0.10, null, null, null, null, null, null, null,
            0.05, null, null, 0.01, null, null, null, 0.00
        );

        /// <summary>
        /// Constructs a volume curve for the third sine partial.
        /// Starts at a moderate volume, dips to a very low level,
        /// and then has a slight resurgence before fading out.
        /// </summary>
        Outlet SinePartialCurve3 => CurveIn
        (
            "SinePartialCurve3",
            timeSpan: 1,
            0.30, 1.00, 0.30, null, null, null, null, null,
            0.10, null, null, null, null, null, null, null,
            0.15, null, null, 0.05, null, null, null, 0.00
        );

        /// <summary>
        /// Generates a volume curve for the sample, starting at full volume
        /// and quickly diminishing to a lower level.
        /// </summary>
        Outlet SamplePartialCurve => CurveIn
        (
            "SamplePartialCurve",
            timeSpan: 1,
            1.00, 0.50, 0.20, null, null, null, null, 0.00,
            null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null
        );

        #endregion
    }
}