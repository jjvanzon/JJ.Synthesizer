using System;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Validation.Entities;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;

// ReSharper disable LocalizableElement

namespace JJ.Business.Synthesizer.Tests
{
    internal class SynthesizerTester_AdditiveSinesAndSamples : SynthesizerSugarBase
    {
        private const double NOTE_TIME_WITH_FADE = 2.5;
        private const double TOTAL_TIME = 6.15; //3.1;

        private Sample _sample;
        private Curve _sine1Envelope;
        private Curve _sine2Envelope;
        private Curve _sine3Envelope;
        private Curve _sampleEnvelope;

        public SynthesizerTester_AdditiveSinesAndSamples(IContext context)
            : base(context)
        { }

        // Steps

        /// <summary>
        /// Arpeggio sound with harmonics, a high-pitch sample for attack,
        /// separate curves for each partial, triggers a wav header auto-detect.
        /// </summary>
        public void Test_Synthesizer_Additive_Sines_And_Samples()
        {
            // Create entities
            _sample = ConfigureSample();
            _sine1Envelope = CreateSine1Envelope();
            _sine2Envelope = CreateSine2VolumeCurve();
            _sine3Envelope = CreateSine3VolumeCurve();
            _sampleEnvelope = CreateSampleVolumeCurve();
            
            Outlet melody = CreateMelody();
            melody = EntityFactory.CreateEcho(this, melody, count: 5, denominator: 3, delay: 0.66);

            // Assert entities
            AssertEntities(_sample, _sine1Envelope, _sine2Envelope, _sine3Envelope, _sampleEnvelope);

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
        
        private Outlet CreateMelody() => Adder
        (
            CreateNote(_[Notes.A4],       volume: _[0.9]),
            CreateNote(_[Notes.E5],       volume: _[1.0], delay: _[0.2]),
            CreateNote(_[Notes.B4],       volume: _[0.5], delay: _[0.4]),
            CreateNote(_[Notes.C5_Sharp], volume: _[0.7], delay: _[0.6]),
            CreateNote(_[Notes.F4_Sharp], volume: _[0.4], delay: _[1.2])
        );

        private Outlet CreateNote(Outlet frequency, Outlet volume, Outlet delay = null)
        {
            delay = delay ?? _[0];
            
            var sine1Volume   = _[1.0];
            var sine2Volume   = _[0.7];
            var sine3Volume   = _[0.4];
            var sample1Volume = _[3.0];
            var sample2Volume = _[5.0];

            Outlet outlet = Adder
            (
                CreateSine(                 frequency,        sine1Volume,   _sine1Envelope),
                CreateSine(        Multiply(frequency, _[2]), sine2Volume,   _sine2Envelope),
                CreateSine(        Multiply(frequency, _[5]), sine3Volume,   _sine3Envelope),
                CreateSampleOutlet(Multiply(frequency, _[2]), sample1Volume, _sampleEnvelope),
                CreateSampleOutlet(Multiply(frequency, _[7]), sample2Volume, _sampleEnvelope)
            );

            outlet = Multiply(outlet, volume);
            outlet = TimeAdd(outlet, delay);

            return outlet;
        }

        private Sine CreateSine(Outlet frequency, Outlet volume, Curve curve) =>
            Sine(Multiply(volume, CurveIn(curve)), frequency);

        private Outlet CreateSampleOutlet(Outlet frequency, Outlet volume, Curve curve) =>
            TimeDivide
            (
                Multiply(Multiply(Sample(_sample), CurveIn(curve)), volume),
                Divide(frequency, _[440])
            );

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
            NOTE_TIME_WITH_FADE,
            0.00, 0.80, 1.00, null, null, null, null, null,
            0.25, null, null, null, null, null, null, null,
            0.10, null, null, 0.02, null, null, null, 0.00);

        /// <summary>
        /// Creates a curve for volume modulation of the second sine partial.
        /// Begins with a quick rise, reaches a high peak, and then slightly drops before fading.
        /// </summary>
        private Curve CreateSine2VolumeCurve() => CurveFactory.CreateCurve(
            NOTE_TIME_WITH_FADE,
            0.00, 1.00, 0.80, null, null, null, null, null,
            0.10, null, null, null, null, null, null, null,
            0.05, null, null, 0.01, null, null, null, 0.00);

        /// <summary>
        /// Constructs a volume curve for the third sine partial.
        /// Starts at a moderate volume, dips to a very low level,
        /// and then has a slight resurgence before fading out.
        /// </summary>
        private Curve CreateSine3VolumeCurve() => CurveFactory.CreateCurve(
            NOTE_TIME_WITH_FADE,
            0.30, 1.00, 0.30, null, null, null, null, null,
            0.10, null, null, null, null, null, null, null,
            0.15, null, null, 0.05, null, null, null, 0.00);

        /// <summary>
        /// Generates a volume curve for the sample, starting at full volume
        /// and quickly diminishing to a lower level.
        /// </summary>
        private Curve CreateSampleVolumeCurve() => CurveFactory.CreateCurve(
            NOTE_TIME_WITH_FADE,
            1.00, 0.50, 0.20, null, null, null, null, 0.00,
            null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null);
    }
}