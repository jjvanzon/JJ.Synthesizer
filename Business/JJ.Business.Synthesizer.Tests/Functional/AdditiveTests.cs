using JJ.Business.Synthesizer.Wishes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Tests.Helpers.TestHelper;
using static JJ.Business.Synthesizer.Tests.Functional.AdditiveEchoExtensions;
// ReSharper disable InvokeAsExtensionMethod

namespace JJ.Business.Synthesizer.Tests.Functional
{
    internal static class AdditiveEchoExtensions
    {
        const int    count     = 4;
        const double magnitude = 0.33;
        const double delay     = 0.66;
        
        public static FluentOutlet Echo(this FluentOutlet x) 
            => x.Echo(count, x._[magnitude], x._[delay])
                .AddEchoDuration(count, x._[delay])
                .SetName();
    }

    /// <inheritdoc cref="docs._metallophone"/>
    [TestClass]
    [TestCategory("Functional")]
    public class AdditiveTests : SynthWishes
    {
        FluentOutlet NoteDuration => _[2.5];
        
        /// <inheritdoc cref="docs._metallophone" />
        public AdditiveTests() : base(beat: 0.4, bar: 1.6) => WithMono();

        /// <inheritdoc cref="docs._metallophone"/>
        [TestMethod]
        public void Additive_Metallophone_Jingle() => new AdditiveTests().Additive_Metallophone_Jingle_RunTest();

        /// <inheritdoc cref="docs._metallophone"/>
        public void Additive_Metallophone_Jingle_RunTest()
        {
            WithAudioLength(beat[4] + NoteDuration);
            Save(() => MetallophoneJingle.Echo() * 0.33).Play();
        }

        /// <inheritdoc cref="docs._metallophone"/>
        [TestMethod]
        public void Additive_Metallophone_Note() => new AdditiveTests().Additive_Metallophone_Note_RunTest();

        /// <inheritdoc cref="docs._metallophone"/>
        public void Additive_Metallophone_Note_RunTest()
        {
            WithAudioLength(NoteDuration).Save(() => Metallophone(Fs4).Echo() * 0.5).Play();
        }

        /// <inheritdoc cref="docs._metallophone"/>
        FluentOutlet MetallophoneJingle => Add
        (
            _[ t[1, 1.0], A4 , Metallophone, 0.9 ],
            _[ t[1, 1.5], E5 , Metallophone, 1.0 ],
            _[ t[1, 2.0], B4 , Metallophone, 0.5 ],
            _[ t[1, 2.5], Cs5, Metallophone, 0.7 ],
            _[ t[1, 4.0], Fs4, Metallophone, 0.4 ]
        ).SetName();

        /// <inheritdoc cref="docs._metallophone"/>
        FluentOutlet Metallophone(FluentOutlet freq, FluentOutlet duration = null)
        {
            freq = freq ?? A4;
            duration = duration ?? NoteDuration;

            return Add
            (
                1.0 * Sine(1 * freq) * Stretch(SineEnvelope1, duration),
                0.7 * Sine(2 * freq) * Stretch(SineEnvelope2, duration),
                0.4 * Sine(5 * freq) * Stretch(SineEnvelope3, duration),
                3.0 * SamplePartial(2 * freq, duration),
                5.0 * SamplePartial(7 * freq, duration)
            ).SetName();
        }

        FluentOutlet SamplePartial(FluentOutlet frequency, FluentOutlet duration)
        {
            var sound = MySample * Stretch(SampleEnvelope, duration);
            var faster = SpeedUp(sound, factor: frequency / A4);
            return faster.SetName();
        }

        /// <inheritdoc cref="docs._mysample"/>
        FluentOutlet _mySample;

        /// <inheritdoc cref="docs._mysample"/>
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

                _mySample = Sample(GetViolin16BitMono44100WavStream(), bytesToSkip).SpeedUp(speedFactor) * volume;

                return _mySample;
            }
        }
        
        // Curves

        FluentOutlet SineEnvelope1 => WithName().Curve
        (
            0.00, 0.80, 1.00, null, null, null, null, null,
            0.25, null, null, null, null, null, null, null,
            0.10, null, null, 0.02, null, null, null, 0.00
        );

        FluentOutlet SineEnvelope2 => WithName().Curve
        (
            0.00, 1.00, 0.80, null, null, null, null, null,
            0.10, null, null, null, null, null, null, null,
            0.05, null, null, 0.01, null, null, null, 0.00
        );

        FluentOutlet SineEnvelope3 => WithName().Curve
        (
            0.30, 1.00, 0.30, null, null, null, null, null,
            0.10, null, null, null, null, null, null, null,
            0.15, null, null, 0.05, null, null, null, 0.00
        );

        FluentOutlet SampleEnvelope => WithName().Curve(
            1.00, 0.50, 0.20, null, null, null, null, 0.00,
            null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null
        );
    }
}