using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        int EchoCount => 4;
        FluentOutlet NoteDuration => _[2.5];
        FluentOutlet EchoDelay => _[0.66];
        FluentOutlet EchoTime => EchoDelay * (EchoCount - 1);
         
        public AdditiveTests()
            : base(beat: 0.4, bar: 1.6)
        { }

        [TestMethod]
        public void Additive_Metallophone_Jingle() => new AdditiveTests().Additive_Metallophone_Jingle_RunTest();

        /// <summary>
        /// Arpeggio sound with harmonics, a high-pitch sample for attack,
        /// separate curves for each partial, triggers a wav header auto-detect.
        /// </summary>
        void Additive_Metallophone_Jingle_RunTest()
            => PlayMono(
                () => Echo(MetallophoneJingle),
                duration: 1.2 + NoteDuration + EchoTime,
                volume: 0.3);

        [TestMethod]
        public void Additive_Metallophone_Note() => new AdditiveTests().Additive_Metallophone_Note_RunTest();

        void Additive_Metallophone_Note_RunTest()
            => PlayMono(
                () => Echo(Metallophone(frequency: F4_Sharp)),
                duration: NoteDuration + EchoTime);

        FluentOutlet MetallophoneJingle => Add
        (
            //duration: bars[1], volume: _[0.5],
            /*() => */Metallophone(t[bar: 1, beat: 1.0], A4      , _[0.9]),
            /*() => */Metallophone(t[bar: 1, beat: 1.5], E5      , _[1.0]),
            /*() => */Metallophone(t[bar: 1, beat: 2.0], B4      , _[0.5]),
            /*() => */Metallophone(t[bar: 1, beat: 2.5], C5_Sharp, _[0.7]),
            /*() => */Metallophone(t[bar: 1, beat: 4.0], F4_Sharp, _[0.4])
        );

        /// <param name="delay"> </param>
        /// <param name="frequency"> </param>
        /// <param name="volume"> </param>
        /// <param name="duration"> The duration of the sound in seconds (default is 2.5). </param>
        /// <inheritdoc cref="Wishes.Helpers.docs._default" />
        FluentOutlet Metallophone(
            FluentOutlet delay = default,
            FluentOutlet frequency = default,
            FluentOutlet volume = default,
            FluentOutlet duration = default)
        {
            frequency = frequency ?? A4;
            duration  = duration ?? NoteDuration;

            var sound = ParallelPlay
            (   duration, volume * 0.2,
                () => 1.0 * Sine(1 * frequency) * Stretch(Sine1Envelope, duration),
                () => 0.7 * Sine(2 * frequency) * Stretch(Sine2Envelope, duration),
                () => 0.4 * Sine(5 * frequency) * Stretch(Sine3Envelope, duration),
                () => 3.0 * SamplePartial(2 * frequency, duration),
                () => 5.0 * SamplePartial(7 * frequency, duration)
            );

            return StrikeNote(sound, delay, volume);
        }

        FluentOutlet SamplePartial(FluentOutlet frequency, FluentOutlet duration)
        {
            var sound = MySample * Stretch(SampleEnvelope, duration);
            var faster = SpeedUp(sound, factor: frequency / A4);
            return faster;
        }

        /// <inheritdoc cref="Wishes.Helpers.docs._default" />
        FluentOutlet Echo(FluentOutlet sound) => Echo(sound, EchoCount, 0.33, EchoDelay);

        FluentOutlet _mySample;

        /// <summary>
        /// Load a sample, skip some old header's bytes, maximize volume and tune to 440Hz.
        /// Returns the initialized Sample if already loaded.
        /// </summary>
        FluentOutlet MySample
        {
            get
            {
                if (_mySample != null) return _mySample;

                // Skip over Header (from some other file format, that slipped into the audio data).
                int bytesToSkip = 62;

                // Skip for Sharper Attack
                bytesToSkip += 1000;

                // Maximize Volume
                double amplifier = 1.467;

                // Tune to A 440Hz
                double octaveFactor   = 0.5;
                double intervalFactor = 4.0 / 5.0;
                double fineTuneFactor = 0.94;
                double speedFactor    = octaveFactor * intervalFactor * fineTuneFactor;

                _mySample = Sample(GetViolin16BitMono44100WavStream(), default, amplifier, speedFactor, bytesToSkip);

                return _mySample;
            }
        }
        
        // Curves

        /// <summary>
        /// Creates a curve representing the volume modulation for the first sine partial.
        /// Starts quietly, peaks at a strong volume, and then fades gradually.
        /// </summary>
        FluentOutlet Sine1Envelope => Curve
        (
            NameHelper.Name(),
            0.00, 0.80, 1.00, null, null, null, null, null,
            0.25, null, null, null, null, null, null, null,
            0.10, null, null, 0.02, null, null, null, 0.00
        );

        /// <summary>
        /// Creates a curve for volume modulation of the second sine partial.
        /// Begins with a quick rise, reaches a high peak, and then slightly drops before fading.
        /// </summary>
        FluentOutlet Sine2Envelope => Curve
        (
            NameHelper.Name(),
            0.00, 1.00, 0.80, null, null, null, null, null,
            0.10, null, null, null, null, null, null, null,
            0.05, null, null, 0.01, null, null, null, 0.00
        );

        /// <summary>
        /// Constructs a volume curve for the third sine partial.
        /// Starts at a moderate volume, dips to a very low level,
        /// and then has a slight resurgence before fading out.
        /// </summary>
        FluentOutlet Sine3Envelope => Curve
        (
            NameHelper.Name(),
            0.30, 1.00, 0.30, null, null, null, null, null,
            0.10, null, null, null, null, null, null, null,
            0.15, null, null, 0.05, null, null, null, 0.00
        );

        /// <summary>
        /// Generates a volume curve for the sample, starting at full volume
        /// and quickly diminishing to a lower level.
        /// </summary>
        FluentOutlet SampleEnvelope => Curve(
            NameHelper.Name(),
            1.00, 0.50, 0.20, null, null, null, null, 0.00,
            null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null
        );
    }
}