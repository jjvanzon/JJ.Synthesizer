using System;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Validation.Entities;
using JJ.Framework.Common;
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
        private const double TOTAL_TIME = 6.15; //3.1;

        private Sample _sample;
        private Curve _sine1Curve;
        private Curve _sine2Curve;
        private Curve _sine3Curve;
        private Curve _sampleCurve;

        public SynthesizerTests_Additive(IContext context)
            : base(context)
        { }

        // Tests

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
                new NodeInfo(time: 1.20, value: 0.20),
                new NodeInfo(time: 1.40, value: 0.08),
                new NodeInfo(time: 1.60, value: 0.30),
                new NodeInfo(time: 4.00, value: 0.00)
            );
            new CurveValidator(curve).Verify();

            Outlet outlet = Sine(CurveIn(curve), _[880]);

            WriteToAudioFile(outlet, duration: 4);
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
            // Create entities
            _sample = ConfigureSample();
            _sine1Curve = CreateSine1Envelope();
            _sine2Curve = CreateSine2VolumeCurve();
            _sine3Curve = CreateSine3VolumeCurve();
            _sampleCurve = CreateSampleVolumeCurve();
            
            Outlet melody = Melody;
            melody = AddEcho(melody);

            // Assert entities
            AssertEntities(_sample, _sine1Curve, _sine2Curve, _sine3Curve, _sampleCurve);

            // Calculate
            WriteToAudioFile(melody, duration: TOTAL_TIME, volume: 1 / 3.5);
        }

        /// <summary> Asserts the entities that WriteToAudioFile won't. </summary>
        private void AssertEntities(Sample sample, params Curve[] curves)
        {
            SampleManager.ValidateSample(sample).Verify();
            curves.ForEach(x => new CurveValidator(x).Verify());
        }

        // Patches

        private Outlet Melody => Adder
        (
            Xylophone(_[Notes.A4], volume: _[0.9]),
            Xylophone(_[Notes.E5], volume: _[1.0], delay: _[0.2]),
            Xylophone(_[Notes.B4], volume: _[0.5], delay: _[0.4]),
            Xylophone(_[Notes.C5_Sharp], volume: _[0.7], delay: _[0.6]),
            Xylophone(_[Notes.F4_Sharp], volume: _[0.4], delay: _[1.2])
        );

        /// <param name="duration">The duration of the sound in seconds (default is 2.5). </param>
        /// <returns></returns>
        /// <inheritdoc cref="DocComments.Default"/>
        private Outlet Xylophone(Outlet freq, Outlet volume, Outlet delay = null, Outlet duration = null)
        {
            freq = freq ?? _[Notes.A4];
            delay = delay ?? _[0];
            duration = duration ?? _[2.5];

            Outlet outlet = Adder
            (
                CreateSine(                 freq,        volume: _[1.0], StretchCurve(_sine1Curve,  duration)),
                CreateSine(        Multiply(freq, _[2]), volume: _[0.7], StretchCurve(_sine2Curve,  duration)),
                CreateSine(        Multiply(freq, _[5]), volume: _[0.4], StretchCurve(_sine3Curve,  duration)),
                CreateSampleOutlet(Multiply(freq, _[2]), volume: _[3.0], StretchCurve(_sampleCurve, duration)),
                CreateSampleOutlet(Multiply(freq, _[7]), volume: _[5.0], StretchCurve(_sampleCurve, duration))
            );

            StrikeNote(delay, volume);
            
            return outlet;
        }

        private Sine CreateSine(Outlet frequency, Outlet volume, Outlet curve) =>
            Sine(Multiply(volume, curve), frequency);

        private Outlet CreateSampleOutlet(Outlet frequency, Outlet volume, Outlet curve) =>
            TimeDivide
            (
                Multiply(Multiply(Sample(_sample), curve), volume),
                Divide(frequency, _[440])
            );

        private const double ECHO_TIME = 0.66 * 4;
        
        /// <inheritdoc cref="DocComments.Default"/>
        private Outlet AddEcho(Outlet sound)
            => EntityFactory.CreateEcho(this, sound, count: 5, denominator: 3, delay: 0.66);

        // Samples
        
        /// <summary>
        /// Load a sample, skip some old header's bytes, maximize volume and tune to 440Hz.
        /// </summary>
        private Sample ConfigureSample()
        {
            Sample sample = SampleManager.CreateSample(TestHelper.GetViolin16BitMono44100WavStream());

            // Skip over Header (from some other file format, that slipped into the audio data).
            sample.BytesToSkip = 62;

            // Skip for Sharper Attack
            sample.BytesToSkip += 1000;

            // Maximize and Normalize sample values (from 16-bit numbers to [-1, 1]).
            sample.Amplifier = 1.467 / short.MaxValue;

            // Tune to A 440Hz
            double octaveFactor = Math.Pow(2, -1);
            double intervalFactor = 4.0 / 5.0;
            double fineTuneFactor = 0.94;
            sample.TimeMultiplier = 1.0 / (octaveFactor * intervalFactor * fineTuneFactor);

            return sample;
        }
        
        // Curves
        
        /// <summary>
        /// Creates a curve representing the volume modulation for the first sine partial.
        /// Starts quietly, peaks at a strong volume, and then fades gradually.
        /// </summary>
        private Curve CreateSine1Envelope() => CurveFactory.CreateCurve(
            timeSpan: 1,
            0.00, 0.80, 1.00, null, null, null, null, null,
            0.25, null, null, null, null, null, null, null,
            0.10, null, null, 0.02, null, null, null, 0.00);

        /// <summary>
        /// Creates a curve for volume modulation of the second sine partial.
        /// Begins with a quick rise, reaches a high peak, and then slightly drops before fading.
        /// </summary>
        private Curve CreateSine2VolumeCurve() => CurveFactory.CreateCurve(
            timeSpan: 1,
            0.00, 1.00, 0.80, null, null, null, null, null,
            0.10, null, null, null, null, null, null, null,
            0.05, null, null, 0.01, null, null, null, 0.00);

        /// <summary>
        /// Constructs a volume curve for the third sine partial.
        /// Starts at a moderate volume, dips to a very low level,
        /// and then has a slight resurgence before fading out.
        /// </summary>
        private Curve CreateSine3VolumeCurve() => CurveFactory.CreateCurve(
            timeSpan: 1,
            0.30, 1.00, 0.30, null, null, null, null, null,
            0.10, null, null, null, null, null, null, null,
            0.15, null, null, 0.05, null, null, null, 0.00);

        /// <summary>
        /// Generates a volume curve for the sample, starting at full volume
        /// and quickly diminishing to a lower level.
        /// </summary>
        private Curve CreateSampleVolumeCurve() => CurveFactory.CreateCurve(
            timeSpan: 1,
            1.00, 0.50, 0.20, null, null, null, null, 0.00,
            null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null);
    }
}