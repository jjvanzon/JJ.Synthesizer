using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Tests.Helpers.TestHelper;
using static JJ.Business.Synthesizer.Tests.Helpers.docs;

// ReSharper disable LocalizableElement

namespace JJ.Business.Synthesizer.Tests.Functional
{
    /// <inheritdoc cref="_metallophone" />
    [TestClass]
    [TestCategory("Functional")]
    public class AdditiveTests : SynthWishes
    {
        int EchoCount => 4;
        FluentOutlet NoteDuration => _[2.5];
        FluentOutlet EchoDelay => _[0.66];
        FluentOutlet EchoTime => EchoDelay * (EchoCount - 1);
         
    /// <inheritdoc cref="_metallophone" />
        public AdditiveTests()
            : base(beat: 0.4, bar: 1.6)
        {
            Mono();
        }

        /// <inheritdoc cref="_metallophone"/>
        [TestMethod]
        public void Additive_Metallophone_Jingle() => new AdditiveTests().Additive_Metallophone_Jingle_RunTest();

        /// <inheritdoc cref="_metallophone"/>
        public void Additive_Metallophone_Jingle_RunTest()
        {
            var duration = beat[4] + NoteDuration + EchoTime;
            WithAudioLength(duration).Play(() => Echo(MetallophoneJingle), volume: 0.3);
        }

        /// <inheritdoc cref="_metallophone"/>
        [TestMethod]
        public void Additive_Metallophone_Note() => new AdditiveTests().Additive_Metallophone_Note_RunTest();

        /// <inheritdoc cref="_metallophone"/>
        public void Additive_Metallophone_Note_RunTest()
            => WithAudioLength(NoteDuration + EchoTime).Play(() => Echo(Metallophone(F4_Sharp)));

        /// <inheritdoc cref="_metallophone"/>
        FluentOutlet MetallophoneJingle => Add
        (
            //duration: bars[1], volume: _[0.5],
            /*() => */Metallophone(t[bar: 1, beat: 1.0], A4      , _[0.9]),
            /*() => */Metallophone(t[bar: 1, beat: 1.5], E5      , _[1.0]),
            /*() => */Metallophone(t[bar: 1, beat: 2.0], B4      , _[0.5]),
            /*() => */Metallophone(t[bar: 1, beat: 2.5], C5_Sharp, _[0.7]),
            /*() => */Metallophone(t[bar: 1, beat: 4.0], F4_Sharp, _[0.4])
        );

        bool PreviewPartials { get; set; }

        public AdditiveTests WithPreviewPartials()
        {
            PreviewPartials = true;
            return this;
        }

        /// <inheritdoc cref="_default" />
        FluentOutlet Metallophone(
            FluentOutlet delay = default,
            FluentOutlet frequency = default,
            FluentOutlet volume = default,
            FluentOutlet duration = default)
        {
            frequency = frequency ?? A4;
            volume = volume ?? _[1];
            
            if (PreviewPartials) WithPreviewParallels();

            WithAudioLength(duration ?? NoteDuration);

            var sound = ParallelAdd
            (
                volume: (volume * 0.2).Value,
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

        /// <inheritdoc cref="_default" />
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
                double volume = 1.467;

                // Tune to A 440Hz
                double octaveFactor   = 0.5;
                double intervalFactor = 4.0 / 5.0;
                double fineTuneFactor = 0.94;
                double speedFactor    = octaveFactor * intervalFactor * fineTuneFactor;

                _mySample = Sample(GetViolin16BitMono44100WavStream(), volume, speedFactor, bytesToSkip);

                return _mySample;
            }
        }
        
        // Curves

        /// <summary>
        /// Creates a curve representing the volume modulation for the first sine partial.
        /// Starts quietly, peaks at a strong volume, and then fades gradually.
        /// </summary>
        FluentOutlet Sine1Envelope => WithName().Curve
        (
            0.00, 0.80, 1.00, null, null, null, null, null,
            0.25, null, null, null, null, null, null, null,
            0.10, null, null, 0.02, null, null, null, 0.00
        );

        /// <summary>
        /// Creates a curve for volume modulation of the second sine partial.
        /// Begins with a quick rise, reaches a high peak, and then slightly drops before fading.
        /// </summary>
        FluentOutlet Sine2Envelope => WithName().Curve
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
        FluentOutlet Sine3Envelope => WithName().Curve
        (
            0.30, 1.00, 0.30, null, null, null, null, null,
            0.10, null, null, null, null, null, null, null,
            0.15, null, null, 0.05, null, null, null, 0.00
        );

        /// <summary>
        /// Generates a volume curve for the sample, starting at full volume
        /// and quickly diminishing to a lower level.
        /// </summary>
        FluentOutlet SampleEnvelope => WithName().Curve(
            1.00, 0.50, 0.20, null, null, null, null, 0.00,
            null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null
        );
    }
}