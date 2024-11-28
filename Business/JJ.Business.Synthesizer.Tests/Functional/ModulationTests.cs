using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.NameHelper;
// ReSharper disable RedundantAssignment
// ReSharper disable NotAccessedVariable

namespace JJ.Business.Synthesizer.Tests.Functional
{
    [TestClass]
    [TestCategory("Functional")]
    public class ModulationTests : SynthWishes
    {
        int      MildEchoCount  => 4;
        FlowNode MildEchoDelay  => _[0.33];
        int      DeepEchoCount  => 4;
        FlowNode DeepEchoDelayL => _[0.5];
        FlowNode DeepEchoDelayR => _[0.53];

        public ModulationTests()
        {
            WithStereo();
            WithBeatLength(_[2.2]);
            WithBarLength(_[2.2]);
        }

        // Tests

        // Long Running
        /// <inheritdoc cref="docs._detunica" />
        internal void Detunica_Jingle_RunTest()
        {
            WithAudioLength(bars[7] + DeepEchoDuration).Save(() => DeepEcho(DetunicaJingle) * 0.8).Play();
        }

        /// <inheritdoc cref="docs._detunica" />
        [TestMethod]
        public void Test_DetunicaBass() => new ModulationTests().DetunicaBass_RunTest();
        /// <inheritdoc cref="docs._detunica" />
        internal void DetunicaBass_RunTest()
        {
            WithAudioLength(3 + DeepEchoDuration).Save(() => DeepEcho(DetunicaBass(E0, _[3])) * 0.9).Play();
        }

        /// <inheritdoc cref="docs._detunica" />
        [TestMethod]
        public void Test_Detunica1() => new ModulationTests().Detunica1_RunTest();
        /// <inheritdoc cref="docs._detunica" />
        void Detunica1_RunTest()
        {
            var duration = _[3];
            WithAudioLength(duration + DeepEchoDuration).Save(() => DeepEcho(Detunica1(E2, duration)) * 0.15).Play();
        }

        /// <inheritdoc cref="docs._detunica" />
        [TestMethod]
        public void Test_Detunica2() => new ModulationTests().Detunica2_RunTest();
        /// <inheritdoc cref="docs._detunica" />
        void Detunica2_RunTest()
        {
            var duration = _[3];
            WithAudioLength(duration + DeepEchoDuration).Save(() => DeepEcho(Detunica2(B4, duration)) * 0.9).Play();
        }

        /// <inheritdoc cref="docs._detunica" />
        [TestMethod]
        public void Test_Detunica3() => new ModulationTests().Detunica3_RunTest();
        /// <inheritdoc cref="docs._detunica" />
        void Detunica3_RunTest()
        {
            var duration = _[3];
            WithAudioLength(duration + DeepEchoDuration).Save(() => DeepEcho(Detunica3(C5, duration))).Play();
        }

        /// <inheritdoc cref="docs._detunica" />
        [TestMethod]
        public void Test_Detunica4() => new ModulationTests().Detunica4_RunTest();
        /// <inheritdoc cref="docs._detunica" />
        void Detunica4_RunTest()
        {
            var duration = _[3];
            WithAudioLength(duration + DeepEchoDuration).Save(() => DeepEcho(Detunica4(D5, duration)) * 0.25).Play();
        }

        /// <inheritdoc cref="docs._detunica" />
        [TestMethod]
        public void Test_Detunica5() => new ModulationTests().Detunica5_RunTest();
        /// <inheritdoc cref="docs._detunica" />
        void Detunica5_RunTest()
        {
            var duration = _[3];
            WithAudioLength(duration + DeepEchoDuration).Save(() => DeepEcho(Detunica5(E5, duration)) * 0.3).Play();
        }

        /// <inheritdoc cref="docs._vibraphase" />
        [TestMethod]
        public void Test_Vibraphase_Chord() => new ModulationTests().Vibraphase_Chord_RunTest();
        /// <inheritdoc cref="docs._vibraphase" />
        void Vibraphase_Chord_RunTest()
        {
            WithMono().WithAudioLength(1 + MildEchoDuration).Save(() => MildEcho(VibraphaseChord) * 0.28).Play();
        }

        /// <inheritdoc cref="docs._vibraphase" />
        [TestMethod]
        public void Test_Vibraphase() => new ModulationTests().Vibraphase_RunTest();
        /// <inheritdoc cref="docs._vibraphase" />
        void Vibraphase_RunTest()
        {
            WithMono().WithAudioLength(1 + MildEchoDuration).Save(() => MildEcho(Vibraphase(E5)) * 0.5).Play();
        }

        // Jingles

        /// <inheritdoc cref="docs._vibraphase" />
        FlowNode VibraphaseChord => Add
        (
            _[ A4, Vibraphase, 0.80 ],
            _[ B4, Vibraphase, 0.70 ],
            _[ C5, Vibraphase, 0.85 ],
            _[ D5, Vibraphase, 0.75 ],
            _[ E5, Vibraphase, 0.90 ]
        ).SetName();

        /// <inheritdoc cref="docs._detunica" />
        FlowNode DetunicaJingle => Add
        (
            _[ beat[1], E0, DetunicaBass, 1.00, l[5.25] ],
            _[ beat[2], B4, Detunica2   , 0.70, l[1.50] ],
            _[ beat[3], C5, Detunica3   , 0.75, l[1.60] ],
            _[ beat[4], D5, Detunica4   , 0.90, l[1.50] ],
            _[ beat[5], E5, Detunica5   , 1.00, l[3.00] ]
        ).SetName();

        // Notes

        /// <inheritdoc cref="docs._detunica" />
        FlowNode DetunicaBass(FlowNode freq, FlowNode duration = null)
        {
            duration = duration ?? GetAudioLength;

            FlowNode depth;
            FlowNode chorusRate;
            
            return Add
            (
                _[ freq * 1 , Detunica1, 0.600, duration, depth=_[0.6], chorusRate=_[0.040] ],
                _[ freq * 2 , Detunica2, 0.800, duration ],
                _[ freq * 4 , Detunica3, 1.000, duration ],
                _[ freq * 8 , Detunica4, 0.015, duration ],
                _[ freq * 16, Detunica5, 0.001, duration ]
            ).SetName().Panbrello(2, 0.2);
        }

        /// <inheritdoc cref="docs._detunica" />
        FlowNode Detunica1(
            FlowNode freq, FlowNode duration = null,
            FlowNode depth = null, FlowNode chorusRate = null)
            => Detunica
                (
                    freq.VibratoFreq(3, 0.00010), duration,
                    depth ?? _[0.8],
                    chorusRate: (chorusRate ?? _[0.03]) * RateCurve1,
                    patchyEnvelope: false
                )
                .Tremolo(1, 0.03).SetName();
        
        /// <inheritdoc cref="docs._detunica" />
        FlowNode Detunica2(FlowNode freq, FlowNode duration = null)
            => MildEcho
            (
                Detunica
                (
                    freq.VibratoFreq(10, 0.00020),
                    duration,
                    depth: _[1.0],
                    churnRate: 0.1 * RateCurve2
                )
                .Tremolo(12, 0.1)
                .Panning(0.4)
                .Panbrello(2.6, 0.09).SetName()
            );
        
        /// <inheritdoc cref="docs._detunica" />
        FlowNode Detunica3(FlowNode freq, FlowNode duration = null)
            => Detunica
               (
                   freq.VibratoFreq(5.5, 0.0005),
                   duration,
                   depth: _[0.5],
                   interferenceRate: Multiply(0.002, RateCurve1),
                   chorusRate: Multiply(0.002,       RateCurve1),
                   patchyEnvelope: false
               )
               .Tremolo(15, 0.06)
               .Panning(Stretch(Curve(0.7, 0.3), duration))
               .Panbrello(4.8, 0.05).SetName();

        /// <inheritdoc cref="docs._detunica" />
        FlowNode Detunica4(FlowNode freq, FlowNode duration = null)
            => Detunica
               (
                   freq.VibratoFreq(7, 0.0003),
                   duration,
                   depth: _[0.5],
                   interferenceRate: 0.003 * RateCurve3
               )
               .Tremolo(10, 0.08)
               .Panning(Curve(0.2, 0.8).Stretch(duration))
               .Panbrello(3.4, 0.07).SetName();

        /// <inheritdoc cref="docs._detunica" />
        FlowNode Detunica5(FlowNode freq, FlowNode duration = null)
            => Detunica
               (
                   freq.VibratoFreq(5.5, 0.00005), duration,
                   depth: _[0.8],
                   churnRate: 0.001 * RateCurve1,
                   chorusRate: _[0.001]
               )
               .Tremolo(3, 0.25)
               .Panning(0.48).SetName();

        // Instruments

        /// <inheritdoc cref="docs._detunica" />
        internal FlowNode Detunica(
            FlowNode freq = default, FlowNode duration = default,
            FlowNode depth = null, FlowNode churnRate = null, 
            FlowNode interferenceRate = null, FlowNode chorusRate = null,
            bool patchyEnvelope = true)
        {
            duration = duration ?? _[1];

            var baseHarmonics    = BaseHarmonics(freq, duration);
            var detunedHarmonics = DetunedHarmonics(freq, duration, churnRate, interferenceRate, chorusRate);
            var sound            = baseHarmonics + detunedHarmonics * depth;
            var envelope         = patchyEnvelope ? PatchyEnvelope : EvenEnvelope;
            
            sound *= envelope.Stretch(duration);

            return sound.SetName();
        }

        /// <inheritdoc cref="docs._vibraphase" />
        FlowNode Vibraphase(
            FlowNode freq = null,
            FlowNode duration = null,
            FlowNode depthAdjust1 = null, FlowNode depthAdjust2 = null)
        {
            var saw      = SemiSaw(freq);
            var jittered = Jitter(saw, depthAdjust1, depthAdjust2);
            var envelope = Curve(@"
               o                   
             o   o                 
                                    
                       o           
            o                   o");

            var sound = jittered * envelope.Stretch(duration);

            return sound.SetName();
        }

        // WaveForms

        /// <inheritdoc cref="docs._semisaw" />
        FlowNode SemiSaw(FlowNode freq)
        {
            freq = freq ?? A4;

            return Add
            (
                freq.Times(1).Sine().Volume(1.0).Tape(),
                freq.Times(2).Sine().Volume(0.5).Tape(),
                freq.Times(3).Sine().Volume(0.3).Tape(), 
                freq.Times(4).Sine().Volume(0.2).Tape()
            ).SetName().Tape();
        }

        FlowNode BaseHarmonics(FlowNode freq, FlowNode duration)
        {
            freq = freq ?? A4;

            return Add
            (
                1.00 * Sine(freq * 1),
                0.30 * Sine(freq * 2),
                0.15 * Sine(freq * 5),
                0.08 * Sine(freq * 7),
                0.10 * Sine(freq * 9)
            ).SetName().Tape(duration);
        }

        /// <inheritdoc cref="docs._detune" />
        FlowNode DetunedHarmonics(
            FlowNode freq, FlowNode duration,
            FlowNode churnRate = null, FlowNode interferenceRate = null, FlowNode chorusRate = null)
        {
            freq = freq ?? A4;
             
            return Add
            (
                1.00 * Sine(DetuneFreq(freq, _[1], duration, churnRate, interferenceRate, chorusRate)),
                0.30 * Sine(DetuneFreq(freq, _[2], duration, churnRate, interferenceRate, chorusRate)),
                0.15 * Sine(DetuneFreq(freq, _[5], duration, churnRate, interferenceRate, chorusRate)),
                0.08 * Sine(DetuneFreq(freq, _[7], duration, churnRate, interferenceRate, chorusRate)),
                0.10 * Sine(DetuneFreq(freq, _[9], duration, churnRate, interferenceRate, chorusRate))
            ).SetName().Tape(duration);
        }

        // Effects

        /// <inheritdoc cref="docs._detune" />
        FlowNode DetuneFreq(
            FlowNode freq, FlowNode harmonic, FlowNode duration,
            FlowNode churnRate = null, FlowNode interfereRate = null, FlowNode chorusRate = null)
        {
            var detunedFreq = freq;

            // Add to harmonic number = churn / heavy interference
            if (churnRate != null)
            {
                var detunedHarmonic = harmonic + churnRate.Stretch(duration);
                detunedFreq *= detunedHarmonic;
            }

            // Add Hz = light interference
            if (interfereRate != null)
            {
                detunedFreq += interfereRate.Stretch(duration);
            }

            // Multiply by 1 + rate = chorus
            if (chorusRate != null)
            {
                detunedFreq *= 1 + chorusRate.Stretch(duration);
            }

            return detunedFreq.SetName();
        }

        /// <inheritdoc cref="docs._vibraphase" />
        FlowNode Jitter(FlowNode sound, FlowNode depthAdjust1 = null, FlowNode depthAdjust2 = null)
        {
            depthAdjust1 = depthAdjust1 ?? _[0.005];
            depthAdjust2 = depthAdjust2 ?? _[0.250];

            var tremoloWave1 = Sine(5.5) * depthAdjust1.Add(1); // 5.5 Hz _tremolo
            sound *= tremoloWave1;

            var tremoloWave2 = Sine(4.0) * depthAdjust2.Add(1); // 4 Hz _tremolo
            sound *= tremoloWave2;

            return sound.SetName();
        }


        /// <inheritdoc cref="docs._echo" />
        FlowNode MildEcho(FlowNode sound)
            // Test without name (defaults to caller member name 'MildEcho')
            => Echo(sound, MildEchoCount, magnitude: _[0.25], MildEchoDelay);

        FlowNode MildEchoDuration
            => EchoDuration(MildEchoCount, MildEchoDelay);

        /// <inheritdoc cref="docs._echo" />
        internal FlowNode DeepEcho(FlowNode sound)
        {
            switch (GetChannel)
            {
                case ChannelEnum.Single:
                    // Test WithName
                    WithName();
                    return (sound * 0.18).Echo(DeepEchoCount, magnitude: _[1 / 2.0], DeepEchoDelayL) / 0.18;
                
                case ChannelEnum.Left:
                    // Test FetchName
                    return (sound * 0.4).Echo(DeepEchoCount, magnitude: _[1 / 2.1], DeepEchoDelayL, FetchName()) / 0.4;
                
                case ChannelEnum.Right:
                    // Test MemberName
                    return (sound * 0.4).Echo(DeepEchoCount, magnitude: _[1 / 2.0], DeepEchoDelayR, MemberName()) / 0.4;
                
                default: 
                    throw new ValueNotSupportedException(GetChannel);
            }
        }

        FlowNode DeepEchoDuration => EchoDuration(DeepEchoCount, DeepEchoDelayR);

        // Curves

        FlowNode PatchyEnvelope => Curve(@"
                         o                             
                    
                              o                         
            o       o
                o                                     

                                     o                   

        o                                       o ");

        FlowNode EvenEnvelope => Curve(@"
                          o                             
                   
                                                        
          o                             o
                                                      
                                    
                                     
           
        o                                       o ");

        FlowNode RateCurve1 => Curve(@"
                    o          
                                
                                
        o                   o");

        FlowNode RateCurve2 => Curve(@"
             o                 
                                
                                
        o                   o ");

        FlowNode RateCurve3 => Curve(@"
                  o            
                                
                                
        o                   o ");

    }
}