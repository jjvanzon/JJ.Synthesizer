using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Business.Synthesizer.Tests.docs;
using JJ.Business.Synthesizer.Wishes;
using static JJ.Business.Synthesizer.Tests.Helpers.TestHelper;
// ReSharper disable InvokeAsExtensionMethod

namespace JJ.Business.Synthesizer.Tests.Functional
{
    internal static class AdditiveEchoExtensions
    {
        const int    count     = 4;
        const double magnitude = 0.33;
        const double delay     = 0.66;
        
        public static FlowNode Echo(this FlowNode x) 
            => x.Echo(count, x[magnitude], x[delay])
                .AddEchoDuration(count, x[delay])
                .SetName();
    }

    /// <inheritdoc cref="_metallophone"/>
    [TestClass]
    [TestCategory("Functional")]
    public class AdditiveTests : SynthWishes
    {
        FlowNode NoteDuration => _[2.5];
        
        /// <inheritdoc cref="_metallophone" />
        public AdditiveTests()
        {
            WithMono();
            WithBeatLength(0.4);
            WithBarLength(1.6);
        }
        
        /// <inheritdoc cref="_metallophone"/>
        [TestMethod, TestCategory("Long")] public void Metallophone_Jingle_Test() => Run(Metallophone_Jingle);
        /// <inheritdoc cref="_metallophone"/>
        public void Metallophone_Jingle()
        {
            MetallophoneJingle().Volume(0.33).Echo().Save().Play();
        }
        
        /// <inheritdoc cref="_metallophone"/>
        [TestMethod] public void Metallophone_Chord_Test() => Run(Metallophone_Chord);
        /// <inheritdoc cref="_metallophone"/>
        public void Metallophone_Chord()
        {
            Fluent(MetallophoneChord).Volume(0.33).Echo().Save().Play();
        }

        /// <inheritdoc cref="_metallophone"/>
        [TestMethod] public void Metallophone_Note_Test() => Run(Metallophone_Note);
        /// <inheritdoc cref="_metallophone"/>
        public void Metallophone_Note()
        {
            Metallophone(Fs4).Echo().Volume(0.5).Save().Play();
        }

        /// <inheritdoc cref="_metallophone"/>
        FlowNode MetallophoneJingle() => _
        [ t[1, 1.0], A4 , Metallophone, 0.9, NoteDuration ]
        [ t[1, 1.5], E5 , Metallophone, 1.0, NoteDuration ]
        [ t[1, 2.0], B4 , Metallophone, 0.5, NoteDuration ]
        [ t[1, 2.5], Cs5, Metallophone, 0.7, NoteDuration ]
        [ t[1, 4.0], Fs4, Metallophone, 0.4, NoteDuration ];

        /// <inheritdoc cref="_metallophone"/>
        FlowNode MetallophoneChord => _
        [ A4 , Metallophone, 0.9, NoteDuration ]
        [ E5 , Metallophone, 1.0, NoteDuration ]
        [ B4 , Metallophone, 0.5, NoteDuration ]
        [ Cs5, Metallophone, 0.7, NoteDuration ]
        [ Fs4, Metallophone, 0.4, NoteDuration ];

        /// <inheritdoc cref="_metallophone"/>
        FlowNode Metallophone(FlowNode freq, FlowNode duration = null)
        {
            freq = freq ?? A4;
            duration = duration ?? NoteDuration;
            
            EnsureAudioLength(duration);

            return Add
            (
                1.0 * Sine(1 * freq) * Stretch(SineEnvelope1, duration),
                0.7 * Sine(2 * freq) * Stretch(SineEnvelope2, duration),
                0.4 * Sine(5 * freq) * Stretch(SineEnvelope3, duration),
                3.0 * SamplePartial(2 * freq, duration),
                5.0 * SamplePartial(7 * freq, duration)
            ).SetName();
        }

        FlowNode SamplePartial(FlowNode frequency, FlowNode duration)
        {
            var sound = MySample * Stretch(SampleEnvelope, duration);
            var faster = SpeedUp(sound, factor: frequency / A4);
            return faster.SetName();
        }

        /// <inheritdoc cref="_mysample"/>
        FlowNode _mySample;

        /// <inheritdoc cref="_mysample"/>
        FlowNode MySample
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

        FlowNode SineEnvelope1 => Curve
        (
            0.00, 0.80, 1.00, null, null, null, null, null,
            0.25, null, null, null, null, null, null, null,
            0.10, null, null, 0.02, null, null, null, 0.00
        ).SetName();

        FlowNode SineEnvelope2 => Curve
        (
            0.00, 1.00, 0.80, null, null, null, null, null,
            0.10, null, null, null, null, null, null, null,
            0.05, null, null, 0.01, null, null, null, 0.00
        ).SetName();

        FlowNode SineEnvelope3 => Curve
        (
            0.30, 1.00, 0.30, null, null, null, null, null,
            0.10, null, null, null, null, null, null, null,
            0.15, null, null, 0.05, null, null, null, 0.00
        ).SetName();

        FlowNode SampleEnvelope => Curve(
            1.00, 0.50, 0.20, null, null, null, null, 0.00,
            null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null
        ).SetName();
    }
}