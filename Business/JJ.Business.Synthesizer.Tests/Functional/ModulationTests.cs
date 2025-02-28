using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Business.Synthesizer.Tests.docs;
using static JJ.Business.Synthesizer.Wishes.NameWishes;
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

        public ModulationTests() => WithStereo().WithBeatLength(2.2).WithBarLength(2.2);
        
        // Tests

        [TestCategory("Long")]
        [TestMethod] public void Detunica_Jingle_Test() => Run(Detunica_Jingle); void Detunica_Jingle() => _
        [DetunicaJingle] [DeepEcho] [Volume, 0.6]
        .AddAudioLength(DeepEchoDuration).Save().Play();

        [TestMethod] public void DetunicaBass_Test() => Run(DetunicaBass); void DetunicaBass() => _
        [ E0, DetunicaBass, 0.9, len:_[4.5] ] [DeepEcho]
        .AddAudioLength(DeepEchoDuration).Save().Play();

        [TestMethod] public void Detunica1_Test() => Run(Detunica1); void Detunica1() => _
        [ E2, Detunica1, 0.3, len:_[3] ] [DeepEcho]
        .AddAudioLength(DeepEchoDuration).Save().Play();
        
        [TestMethod] public void Detunica2_Test() => Run(Detunica2); void Detunica2() =>
        WithAudioLength(3)
        [ B4, Detunica2, 0.9, len: _[3] ] [DeepEcho]
        .AddAudioLength(DeepEchoDuration).Save().Play();

        [TestMethod] public void Detunica3_Test() => Run(Detunica3); void Detunica3() => _
        [ C5, Detunica3, len:_[3] ] [DeepEcho]
        .AddAudioLength(DeepEchoDuration).Save().Play();
        
        [TestMethod] public void Detunica4_Test() => Run(Detunica4); void Detunica4() => _
        [ D5, Detunica4, len:_[3] ] [DeepEcho]
        .AddAudioLength(DeepEchoDuration).Save().Play();

        [TestMethod] public void Detunica5_Test() => Run(Detunica5); void Detunica5() => _
        [ E5, Detunica5, len:_[3] ] [Volume, 0.6] [DeepEcho]
        .AddAudioLength(DeepEchoDuration).Save().Play();
        
        [TestMethod] public void Vibraphase_Chord_Test() => Run(Vibraphase_Chord); void Vibraphase_Chord() =>
        WithStereo().WithNoteLength(1).AddAudioLength(MildEchoDuration)
        [VibraphaseChord] [MildEcho] [Volume, 0.28]
        .Save().Play();

        [TestMethod] public void Vibraphase_Note_Test() => Run(VibraphaseNote); void VibraphaseNote() =>
        WithMono().WithNoteLength(1).AddAudioLength(MildEchoDuration)
        [ E5, Vibraphase, 0.5 ] [MildEcho]
        .Save().Play();

        // Jingles

        /// <inheritdoc cref="_vibraphase" />
        FlowNode VibraphaseChord => _
        [ A4, Vibraphase, 0.80 ] [ Panbrello ]
        [ B4, Vibraphase, 0.70 ]
        [ C5, Vibraphase, 0.85 ]
        [ D5, Vibraphase, 0.75 ]
        [ E5, Vibraphase, 0.90 ];

        /// <inheritdoc cref="_detunica" />
        FlowNode DetunicaJingle => _
        [ beat[1], E0, DetunicaBass, 1.00, l[5.25] ]
        [ beat[2], B4, Detunica2   , 0.70, l[1.50] ]
        [ beat[3], C5, Detunica3   , 0.75, l[1.60] ]
        [ beat[4], D5, Detunica4   , 0.90, l[1.50] ]
        [ beat[5], E5, Detunica5   , 1.00, l[3.00] ];

        // Notes

        /// <inheritdoc cref="_detunica" />
        FlowNode DetunicaBass(FlowNode freq, FlowNode duration = null)
        {
            duration = duration ?? GetAudioLength;

            FlowNode depth;
            FlowNode chorusRate;
            
            return _
            [ freq * 1 , Detunica1, 0.600, duration, depth=_[0.6], chorusRate=_[0.040] ]
            [ freq * 2 , Detunica2, 0.800, duration ]
            [ freq * 4 , Detunica3, 1.000, duration ]
            [ freq * 8 , Detunica4, 0.015, duration ]
            [ freq * 16, Detunica5, 0.001, duration ]
            [ Panbrello, 2, 0.2 ];
        }

        /// <inheritdoc cref="_detunica" />
        FlowNode Detunica1(
            FlowNode freq, FlowNode duration = null,
            FlowNode depth = null, FlowNode chorusRate = null)
            => Detunica
            (
                freq [ VibratoFreq, 3, 0.00010 ], 
                duration,
                depth ?? _[0.8],
                chorusRate: (chorusRate ?? _[0.03]) * RateCurve1,
                patchyEnvelope: false
            )
            [ Tremolo, 1, 0.03 ]
            .SetName();
        
        /// <inheritdoc cref="_detunica" />
        FlowNode Detunica2(FlowNode freq, FlowNode duration = null)
            => Detunica
            (
                freq [ VibratoFreq, 10, 0.00020 ],
                duration,
                depth: _[1.0],
                churnRate: 0.1 * RateCurve2
            )
            [ Tremolo, 12, 0.1 ]
            [ Panning, 0.4 ]
            [ Panbrello, 2.6, 0.09 ].SetName()
            [ MildEcho ];
        
        /// <inheritdoc cref="_detunica" />
        FlowNode Detunica3(FlowNode freq, FlowNode duration = null)
            => Detunica
            (
                freq [ VibratoFreq, 5.5, 0.0005 ],
                duration,
                depth: _[0.5],
                interferenceRate: Multiply(0.002, RateCurve1),
                chorusRate:       Multiply(0.002, RateCurve1),
                patchyEnvelope: false
            )
            [ Tremolo, 15, 0.06 ]
            [ Panbrello, 4.8, 0.05 ]
            [ Panning, Curve(0.7, 0.3).Stretch(duration) ];

        /// <inheritdoc cref="_detunica" />
        FlowNode Detunica4(FlowNode freq, FlowNode duration = null)
            => Detunica
            (
                freq [ VibratoFreq, 7, 0.0003 ],
                duration,
                depth: _[0.5],
                interferenceRate: 0.003 * RateCurve3
            )
            [ Tremolo, 10, 0.08 ]
            [ Panbrello, 3.4, 0.07 ]
            [ Panning, Curve(0.2, 0.8).Stretch(duration) ];

        /// <inheritdoc cref="_detunica" />
        FlowNode Detunica5(FlowNode freq, FlowNode duration = null)
            => Detunica
            (
                freq [ VibratoFreq, 5.5, 0.00005 ], duration,
                depth: _[0.8],
                churnRate: 0.001 * RateCurve1,
                chorusRate: _[0.001]
            )
            [ Tremolo, 3, 0.25 ]
            [ Panning, 0.48 ];

        // Instruments

        /// <inheritdoc cref="_detunica" />
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

        /// <inheritdoc cref="_vibraphase" />
        FlowNode Vibraphase(
            FlowNode freq,
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

        /// <inheritdoc cref="_semisaw" />
        FlowNode SemiSaw(FlowNode freq) =>
        ( freq * 1 ) [ Sine ] [ Volume, 1.0 ] +
        ( freq * 2 ) [ Sine ] [ Volume, 0.5 ] +
        ( freq * 3 ) [ Sine ] [ Volume, 0.3 ] + 
        ( freq * 4 ) [ Sine ] [ Volume, 0.2 ]
        .SetName().Tape();

        FlowNode BaseHarmonics(FlowNode freq, FlowNode duration) => 
        ( freq * 1 ) [ Sine ] [ Volume, 1.00 ] +
        ( freq * 2 ) [ Sine ] [ Volume, 0.30 ] +
        ( freq * 5 ) [ Sine ] [ Volume, 0.15 ] +
        ( freq * 7 ) [ Sine ] [ Volume, 0.08 ] +
        ( freq * 9 ) [ Sine ] [ Volume, 0.10 ] 
        .Tape(duration).SetName();

        /// <inheritdoc cref="_detune" />
        FlowNode DetunedHarmonics
        (FlowNode freq, FlowNode duration, FlowNode churnRate = null, FlowNode interferenceRate = null, FlowNode chorusRate = null) =>
        _[ DetuneFreq, freq, _[1], duration, churnRate, interferenceRate, chorusRate ] [ Sine ] [ Volume, 1.00 ] +
        _[ DetuneFreq, freq, _[2], duration, churnRate, interferenceRate, chorusRate ] [ Sine ] [ Volume, 0.30 ] +
        _[ DetuneFreq, freq, _[5], duration, churnRate, interferenceRate, chorusRate ] [ Sine ] [ Volume, 0.15 ] +
        _[ DetuneFreq, freq, _[7], duration, churnRate, interferenceRate, chorusRate ] [ Sine ] [ Volume, 0.08 ] +
        _[ DetuneFreq, freq, _[9], duration, churnRate, interferenceRate, chorusRate ] [ Sine ] [ Volume, 0.10 ]
        .SetName().Tape(duration);

        // Effects

        /// <inheritdoc cref="_detune" />
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

        /// <inheritdoc cref="_vibraphase" />
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

        /// <inheritdoc cref="_echo" />
        FlowNode MildEcho(FlowNode sound)
            // Test without name (defaults to caller member name 'MildEcho')
            => Echo(sound, MildEchoCount, magnitude: _[0.25], MildEchoDelay);

        FlowNode MildEchoDuration
            => EchoDuration(MildEchoCount, MildEchoDelay);

        /// <inheritdoc cref="_echo" />
        internal FlowNode DeepEcho(FlowNode sound)
        {
            if (IsCenter)
            {
                // Test SetName
                return (sound * 0.18).SetName().Echo(DeepEchoCount, magnitude: _[1 / 2.0], DeepEchoDelayL) / 0.18;
            }   
            if (IsLeft)
            {
                // Test ResolveName
                return (sound * 0.4).Echo(DeepEchoCount, magnitude: _[1 / 2.1], DeepEchoDelayL, ResolveName()) / 0.4;
            }
            if (IsRight)
            {
                // Test MemberName
                return (sound * 0.4).Echo(DeepEchoCount, magnitude: _[1 / 2.0], DeepEchoDelayR, MemberName()) / 0.4;
            }
            
            throw new ValueNotSupportedException(GetChannel);
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