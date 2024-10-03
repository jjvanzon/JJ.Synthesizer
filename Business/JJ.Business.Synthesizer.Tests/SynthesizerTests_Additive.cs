using System;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Tests.Helpers;
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
    public class SynthesizerTests_Additive : SynthesizerSugarBase
    {
        const double DEFAULT_NOTE_DURATION = 2.5;

        /// <summary> Constructor for test runner. </summary>
        [UsedImplicitly]
        public SynthesizerTests_Additive()
        { }

        /// <summary> Constructor allowing each test to run in its own instance. </summary>
        public SynthesizerTests_Additive(IContext context)
            : base(context, beat: 0.4, bar: 1.6)
        { }

        #region Tests
                
        [TestMethod]
        public void Test_Synthesizer_Additive_Sine_And_Curve()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_Additive(context).Test_Additive_Sine_And_Curve();
        }
        
        /// <summary>
        /// Generates a Sine wave with a Volume Curve, testing both Block and Line Interpolation.
        /// Verifies data using (Warning)Validators and writes the output audio to a file.
        /// </summary>
        public void Test_Additive_Sine_And_Curve()
        {
            Curve curve = CurveFactory.CreateCurve
            (
                new NodeInfo(time: 0.00, value: 0.00),
                new NodeInfo(time: 0.05, value: 0.95),
                new NodeInfo(time: 0.10, value: 1.00),
                new NodeInfo(time: 0.20, value: 0.60),
                new NodeInfo(time: 0.80, value: 0.20),
                new NodeInfo(time: 1.00, value: 0.00),
                new NodeInfo(time: 1.20, value: 0.50),
                new NodeInfo(time: 1.40, value: 0.08),
                new NodeInfo(time: 1.60, value: 0.70),
                new NodeInfo(time: 4.00, value: 0.00)
            );
            new CurveValidator(curve).Verify();

            Outlet outlet = Sine(CurveIn(curve), _[Notes.G5]);

            WriteToAudioFile(outlet, volume: 1, duration: 4);
        }

        [TestMethod]
        public void Test_Synthesizer_Additive_Sines_And_Samples()
        {
            using (IContext context = PersistenceHelper.CreateContext()) 
                new SynthesizerTests_Additive(context).Test_Additive_Sines_And_Samples();
        }

        /// <summary>
        /// Arpeggio sound with harmonics, a high-pitch sample for attack,
        /// separate curves for each partial, triggers a wav header auto-detect.
        /// </summary>
        public void Test_Additive_Sines_And_Samples()
        {
            /// Assert the entities that WriteToAudioFile won't.
            SampleManager.ValidateSample(GetSample()).Verify();
            new CurveValidator(SinePartialCurve1).Verify();
            new CurveValidator(SinePartialCurve2).Verify();
            new CurveValidator(SinePartialCurve3).Verify();
            new CurveValidator(SamplePartialCurve).Verify();
            
            WriteToAudioFile(
                AddEcho(Melody),
                volume: 0.3,
                duration: 1.2 + DEFAULT_NOTE_DURATION + ECHO_TIME);
        }

        #endregion
        
        #region Patches

        private Outlet Melody => Adder
        (
            Xylophone(_[Notes.A4],       delay: t[bar:0, beat:0.0], volume: _[0.9]),
            Xylophone(_[Notes.E5],       delay: t[bar:0, beat:0.5], volume: _[1.0]),
            Xylophone(_[Notes.B4],       delay: t[bar:0, beat:1.0], volume: _[0.5]),
            Xylophone(_[Notes.C5_Sharp], delay: t[bar:0, beat:1.5], volume: _[0.7]),
            Xylophone(_[Notes.F4_Sharp], delay: t[bar:0, beat:3.0], volume: _[0.4])
        );

        /// <param name="duration">The duration of the sound in seconds (default is 2.5). </param>
        /// <inheritdoc cref="DocComments.Default"/>
        private Outlet Xylophone(Outlet frequency = null, Outlet volume = null, Outlet delay = null, Outlet duration = null)
        {
            frequency = frequency ?? _[Notes.A4];
            duration = duration ?? _[DEFAULT_NOTE_DURATION];

            var sound = Adder
            (
                SinePartial(           frequency,        volume: _[1.0], StretchCurve(SinePartialCurve1,  duration)),
                SinePartial(  Multiply(frequency, _[2]), volume: _[0.7], StretchCurve(SinePartialCurve2,  duration)),
                SinePartial(  Multiply(frequency, _[5]), volume: _[0.4], StretchCurve(SinePartialCurve3,  duration)),
                SamplePartial(Multiply(frequency, _[2]), volume: _[3.0], StretchCurve(SamplePartialCurve, duration)),
                SamplePartial(Multiply(frequency, _[7]), volume: _[5.0], StretchCurve(SamplePartialCurve, duration))
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
        
        /// <inheritdoc cref="DocComments.Default"/>
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
            
            _sample = SampleManager.CreateSample(TestHelper.GetViolin16BitMono44100WavStream());

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
        private Curve SinePartialCurve1 => CurveFactory.CreateCurve
        (
            timeSpan: 1,
            0.00, 0.80, 1.00, null, null, null, null, null,
            0.25, null, null, null, null, null, null, null,
            0.10, null, null, 0.02, null, null, null, 0.00
        );

        /// <summary>
        /// Creates a curve for volume modulation of the second sine partial.
        /// Begins with a quick rise, reaches a high peak, and then slightly drops before fading.
        /// </summary>
        private Curve SinePartialCurve2 => CurveFactory.CreateCurve
        (
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
        private Curve SinePartialCurve3 => CurveFactory.CreateCurve
        (
            timeSpan: 1,
            0.30, 1.00, 0.30, null, null, null, null, null,
            0.10, null, null, null, null, null, null, null,
            0.15, null, null, 0.05, null, null, null, 0.00
        );

        /// <summary>
        /// Generates a volume curve for the sample, starting at full volume
        /// and quickly diminishing to a lower level.
        /// </summary>
        private Curve SamplePartialCurve => CurveFactory.CreateCurve
        (
            timeSpan: 1,
            1.00, 0.50, 0.20, null, null, null, null, 0.00,
            null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null
        );
        
        #endregion
    }
}