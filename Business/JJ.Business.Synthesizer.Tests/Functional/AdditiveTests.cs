using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Math;
using static JJ.Business.Synthesizer.Tests.Helpers.TestHelper;

// ReSharper disable LocalizableElement

namespace JJ.Business.Synthesizer.Tests.Functional
{
    /// <summary>
    /// Additional tests written upon retro-actively isolating older synthesizer versions.
    /// </summary>
    [TestClass]
    [TestCategory("Functional")]
    public class AdditiveTests : SynthWishes
    {
        const double DEFAULT_NOTE_DURATION = 2.5;

        public AdditiveTests()
            : base(beat: 0.4, bar: 1.6)
        { }

        [TestMethod]
        public void Sines_Samples_Metallophone_Jingle() => new AdditiveTests().Sines_Samples_Metallophone_Jingle_RunTest();

        /// <summary>
        /// Arpeggio sound with harmonics, a high-pitch sample for attack,
        /// separate curves for each partial, triggers a wav header auto-detect.
        /// </summary>
        void Sines_Samples_Metallophone_Jingle_RunTest()
            => PlayMono(
                () => Echo(MetallophoneJingle),
                duration: 1.2 + DEFAULT_NOTE_DURATION + ECHO_TIME,
                volume: 0.3);

        [TestMethod]
        public void Sines_Samples_Metallophone_Note() => new AdditiveTests().Sines_Samples_Metallophone_Note_RunTest();

        void Sines_Samples_Metallophone_Note_RunTest()
            => PlayMono(
                () => Echo(Metallophone(F4_Sharp)),
                duration: DEFAULT_NOTE_DURATION + ECHO_TIME,
                volume: 0.5);

        Outlet MetallophoneJingle => Add
        (
            Metallophone(A4,       delay: t[bar:1, beat:1.0], volume: _[0.9]),
            Metallophone(E5,       delay: t[bar:1, beat:1.5], volume: _[1.0]),
            Metallophone(B4,       delay: t[bar:1, beat:2.0], volume: _[0.5]),
            Metallophone(C5_Sharp, delay: t[bar:1, beat:2.5], volume: _[0.7]),
            Metallophone(F4_Sharp, delay: t[bar:1, beat:4.0], volume: _[0.4])
        );

        /// <param name="duration"> The duration of the sound in seconds (default is 2.5). </param>
        /// <inheritdoc cref="Wishes.Helpers.docs._default" />
        Outlet Metallophone(Outlet frequency = null, Outlet volume = null, Outlet delay = null, Outlet duration = null)
        {
            frequency = frequency ?? A4;
            duration = duration ?? _[DEFAULT_NOTE_DURATION];

            var sound = Add
            (
                SinePartial(           frequency,        volume: _[1.0], Stretch(Sine1Envelope,  duration)),
                SinePartial(  Multiply(frequency, _[2]), volume: _[0.7], Stretch(Sine2Envelope,  duration)),
                SinePartial(  Multiply(frequency, _[5]), volume: _[0.4], Stretch(Sine3Envelope,  duration)),
                SamplePartial(Multiply(frequency, _[2]), volume: _[3.0], Stretch(SampleEnvelope, duration)),
                SamplePartial(Multiply(frequency, _[7]), volume: _[5.0], Stretch(SampleEnvelope, duration))
            );

            return StrikeNote(sound, delay, volume);
        }

        Outlet SinePartial(Outlet frequency, Outlet volume, Outlet envelope)
            =>  Multiply(Sine(frequency), Multiply(volume, envelope));

        Outlet SamplePartial(Outlet frequency, Outlet volume, Outlet envelope)
            => Squash
            (
                Multiply(Multiply(Sample(), envelope), volume),
                Divide(frequency, A4)
            );

        const double ECHO_TIME = 0.66 * 3;

        /// <inheritdoc cref="Wishes.Helpers.docs._default" />
        Outlet Echo(Outlet sound) => Echo(sound, count: 4, magnitude: _[0.33], delay: _[0.66]);


        SampleOperatorWrapper _sample;

        /// <summary>
        /// Load a sample, skip some old header's bytes, maximize volume and tune to 440Hz.
        /// </summary>
        SampleOperatorWrapper Sample()
        {
            if (_sample != null) return _sample;

            // Skip over Header (from some other file format, that slipped into the audio data).
            int bytesToSkip = 62;

            // Skip for Sharper Attack
            bytesToSkip += 1000;

            // Maximize and Normalize sample values (from 16-bit numbers to [-1, 1]).
            double amplifier = 1.467;

            // Tune to A 440Hz
            double octaveFactor   = 0.5;
            double intervalFactor = 4.0 / 5.0;
            double fineTuneFactor = 0.94;
            double speedFactor    = octaveFactor * intervalFactor * fineTuneFactor;

            _sample = Sample(GetViolin16BitMono44100WavStream(), amplifier, speedFactor, bytesToSkip);

            return _sample;
        }

        // Curves

        /// <summary>
        /// Creates a curve representing the volume modulation for the first sine partial.
        /// Starts quietly, peaks at a strong volume, and then fades gradually.
        /// </summary>
        Outlet Sine1Envelope => CurveIn
        (
            0.00, 0.80, 1.00, null, null, null, null, null,
            0.25, null, null, null, null, null, null, null,
            0.10, null, null, 0.02, null, null, null, 0.00
        );

        /// <summary>
        /// Creates a curve for volume modulation of the second sine partial.
        /// Begins with a quick rise, reaches a high peak, and then slightly drops before fading.
        /// </summary>
        Outlet Sine2Envelope => CurveIn
        (
            0.00, 1.00, 0.80, null, null, null, null, null,
            0.10, null, null, null, null, null, null, null,
            0.05, null, null, 0.01, null, null, null, 0.00
        );

        /// <summary>
        /// Constructs a volume curve for the third sine partial.
        /// Starts at a moderate volume, dips to a very low level,
        /// and then has a slight resurgence before fading out.
        /// </summary>
        Outlet Sine3Envelope => CurveIn
        (
            0.30, 1.00, 0.30, null, null, null, null, null,
            0.10, null, null, null, null, null, null, null,
            0.15, null, null, 0.05, null, null, null, 0.00
        );

        /// <summary>
        /// Generates a volume curve for the sample, starting at full volume
        /// and quickly diminishing to a lower level.
        /// </summary>
        Outlet SampleEnvelope => CurveIn
        (
            1.00, 0.50, 0.20, null, null, null, null, 0.00,
            null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null
        );
    }
}