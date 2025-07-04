﻿using wishdocs = JJ.Business.Synthesizer.Wishes.docs;
// ReSharper disable UnusedType.Global

#pragma warning disable CS0649
#pragma warning disable CS0169 // Field is never used

// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace JJ.Business.Synthesizer.Tests
{
    namespace docs
    {
        /// <inheritdoc cref="wishdocs._audiolength" />
        public struct _audiolength { }
        
        /// <inheritdoc cref="wishdocs._audioplayback" />
        public struct _audioplayback { }

        /// <summary>
        /// Allows implicit conversion from CaseProp to int:
        /// </summary>
        public struct _areequalint { }
        
        /// <inheritdoc cref="wishdocs._channeltoaudiofileoutput "/>
        public struct _channeltoaudiofileoutput { }
            
        /// <summary>
        /// Copied from newer JJ.Framework version.
        /// </summary>
        public struct _copiedfromframework { }
            
        /// <summary>
        /// Beating audible further along the sound.
        /// Mod speed much below sound freq, changes sound freq drastically * [0.5, 1.5]
        /// </summary>
        public struct _createfmnoisebeating { }
        
        /// <summary> Generates a detuned harmonic sound by altering the frequencies slightly. </summary>
        /// <param name="detuneRate">
        /// The depth of the detuning applied to the harmonics.
        /// If not provided, a default value is used.
        /// </param>
        /// <inheritdoc cref="_default" />
        public struct _detune { }

        /// <summary>
        /// A detuned note characterized by a rich and slightly eerie sound due to the detuned harmonics.
        /// It produces a haunting tone with subtle shifts in pitch.
        /// </summary>
        /// <param name="detuneDepth">
        /// The detune depth, adjusting the harmonic frequencies relative to the base frequency,
        /// creating a subtle dissonance and eerie quality.<br /><br />
        /// If the detune depth is low, this may cause a slow _tremolo-like effect
        /// due to periodic constructive/destructive interference <br /><br />
        /// This effect of which can be quite drastic. Possible mitigations:<br /><br />
        /// 1) Increase the detune depth
        /// 2) Lower amplitude for the detuned partials
        /// 3) Different volume envelope
        /// 4) A different detune function
        /// </param>
        /// <param name="envelopeVariation">
        /// 1 is the default and a more patchy volume envelope.<br />
        /// 2 gives the newer with a move even fade in and out.
        /// </param>
        /// <inheritdoc cref="_docs._default" />
        public struct _detunica { }

        /// <summary>
        /// Applies an echo effect to the given sound.
        /// </summary>
        /// <param name="sound"> The original sound to which the echo effect will be applied. </param>
        /// <returns> A FlowNode representing the sound with the applied echo effect. </returns>
        public struct _echo { }

        /// <summary>
        /// When harmonics thicken near the center, this curve can even out the volume over time.
        /// </summary>
        public struct _evenoutcurve { }
            
        /// <inheritdoc cref="wishdocs._fileextension" />
        public struct _fileextension { }
        
        /// <summary>
        /// High hard flute: mod speed above sound freq, changes sound freq * [-0.005, 0.005] (erroneously)
        /// </summary>
        /// <inheritdoc cref="_default" />
        public struct _flute1 { }

        /// <summary> Yet another flute: mod speed above sound freq, changes sound freq * 1 +/- 0.005 </summary>
        /// <inheritdoc cref="_default" />
        public struct _flute2 { }

        /// <summary> Yet another flute: mod speed above sound freq, changes sound freq * 1 +/- 0.005 </summary>
        /// <inheritdoc cref="_default" />
        public struct _flute3 { }

        /// <summary> Modulated hard flute: mod speed below sound freq, changes sound freq * [-0.005, 0.005] (erroneously) </summary>
        /// <inheritdoc cref="_default" />
        public struct _flute4 { }

        /// <summary> FM with (faulty) multiplication around 0. </summary>
        /// <inheritdoc cref="_default" />
        public struct _fmaround0 { }

        /// <summary> FM with multiplication around 1. </summary>
        /// <inheritdoc cref="_default" />
        public struct _fmaroundfreq { }
            
        /// <summary> FM sound synthesis modulating with addition. Modulates sound freq to +/- a number of Hz. </summary>
        /// <param name="modDepth"> In Hz </param>
        /// <inheritdoc cref="_default" />
        public struct _fminhertz { }

        /// <summary>
        /// Tests various algorithms for FM sound synthesis, culminating in an atmospheric FM jingle.
        /// While the code might appear a bit messy, it serves as a test bed for FM synthesis,
        /// exploring multiple scenarios and notations.
        /// <para>
        /// NOTE: Version 0.0.250 lacks time tracking in its oscillator, resulting in FM synthesis 
        /// with more dynamic timbres that can be harder to control.
        /// </para>
        /// </summary>
        public struct _fmtests { }
        
        /// <inheritdoc cref="wishdocs._getsamplingrate" />
        public struct _getsamplingrate { }
        
        /// <inheritdoc cref="wishdocs._headerlength" />
        public struct _headerlength { }
        
        /// <summary>
        /// Sounds like Horn.
        /// FM with mod speed below sound freq, changes sound freq to +/- 5Hz.
        /// Volume curve is applied.
        /// FM modulator is attempted to be tamed with curves.
        /// </summary>
        /// <param name="freq"> The base frequency of the sound in Hz (default A2/110Hz). </param>
        /// <inheritdoc cref="_default" />
        public struct _horn { }

        /// <inheritdoc cref="wishdocs._leafchecktimeout" />
        public struct _leafchecktimeout { }
        
        /// <summary>
        /// An airy sound with harmonics, a high-pitch sample for attack,
        /// separate curves for each partial, triggers a wav header auto-detect.
        /// </summary>
        /// <inheritdoc cref="wishdocs._default" />
        public struct _metallophone { }

        /// <summary>
        /// A curve that can be applied to the modulator depth to tame the modulation.
        /// In this version of FM synthesis, the modulation depth accumulates over time without such taming.
        /// This is because of a lack of time tracking in the oscillators in this version.
        /// </summary>
        public struct _modtamingcurve { }
        
        /// <summary>
        /// Loads a sample, skip some old header's bytes, maximize volume and tune to 440Hz.
        /// Returns the initialized Sample if already loaded.
        /// </summary>
        public struct _mysample { }

        /// <inheritdoc cref="wishdocs._notelength" />
        public struct _notelength { }
        /// <summary>
        /// <strong> "NullyLen Lullaby" </strong> <br/>
        /// Tests various note length fallbacks, including explicit values,
        /// defaults in config files or based on the beat length
        /// or explicit specification in the note commands.
        /// Ends with a soothing lullaby to help this big baby calm down after all the bleeps.
        /// </summary>
        public struct _notelengthfallbacktests { }
        
        /// <inheritdoc cref="wishdocs._padding" />
        public struct _padding { }
        
        /// <inheritdoc cref="wishdocs._parallelprocessing" />
        public struct _parallelprocessing { }
        
        /// <inheritdoc cref="wishdocs._playalltapes" />
        public struct _playalltapes { }
        
        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.005 </summary>
        /// <param name="freq"> The base frequency of the sound in Hz (default A1/55Hz). </param>
        /// <inheritdoc cref="_default" />
        public struct _ripplebass { }

        /// <summary> Mod speed below sound freq, changes sound freq ±10Hz </summary>
        /// <param name="freq"> The base frequency of the sound in Hz (default A3/220Hz). </param>
        /// <inheritdoc cref="_default" />
        public struct _ripplenotesharpmetallic { }

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.005 </summary>
        /// <inheritdoc cref="_default" />
        public struct _ripplesoundclean { }

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.05 </summary>
        /// <inheritdoc cref="_default" />
        public struct _ripplesoundcooldouble { }
            
        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.02 </summary>
        /// <param name="duration"> The audioLength of the sound in seconds (default is 2.5). </param>
        /// <inheritdoc cref="_default" />
        public struct _ripplesoundfantasyeffect { }
        
        /// <summary>
        /// Generates a mild sawtooth-like waveform by combining multiple sine waves with different frequencies.
        /// </summary>
        /// <returns> A FlowNode representing the semi-sawtooth waveform. </returns>
        /// <inheritdoc cref="_default" />
        public struct _semisaw { }

        /// <summary>
        /// Changing a property like Channels without re-recording desynchronizes it from
        /// TestEntities.ChannelTapes, making direct assertions unreliable. <br/><br/>
        ///
        /// In such cases, use this <c>Simple</c> variation instead of the <c>Complete</c> version
        /// to validate a single tape without channel tapes.
        /// </summary>
        public struct _singletapeassertion { }
            
        /// <summary> Shapes a ripple effect sound giving it a volume envelope and a delay, volume and audioLength. </summary>
        /// <param name="duration"> The audioLength of the sound in seconds (default is 2.5). </param>
        /// <param name="fmSignal"> A ripple sound to be shaped </param>
        /// <inheritdoc cref="_default" />
        public struct _shaperipplesound { }

        
        /// <summary>
        /// Tests if members can be called without a <c>this.</c> qualifier.
        /// </summary>
        public struct _synthwishesderived { }
        
        /// <summary>
        /// Testing extension methods in <see cref="_AudioFileExtensionWishes" />
        /// that didn't get any coverage elsewhere.
        /// </summary>
        public struct _testattributewishesold { }

        /// <summary>
        /// Aims to test rare exception for code coverage.
        /// </summary>
        public struct _throwtests { }
        
        /// <summary>
        /// Sounds like Trombone at beginning.
        /// FM with mod speed below sound freq, changes sound freq to +/- 5Hz.
        /// Volume curve is applied.
        /// Higher notes are shorter, lower notes are much longer.
        /// </summary>
        /// <param name="freq"> The base frequency of the sound in Hz (default A1/55Hz). </param>
        /// <param name="durationFactor"> Duration varies with pitch, but can be multiplied by this factor (default is 1). </param>
        /// <inheritdoc cref="_default" />
        public struct _trombone { }

        /// <summary>
        /// Applies a jitter effect to notes, with adjustable depths.
        /// Basically with an extreme double _tremolo effect, that goes into the negative.
        /// That can also cause a phasing effect due to constructive and destructive interference
        /// when playing multiple notes at the same time.
        /// </summary>
        /// <param name="sound"> The sound to apply the jitter effect to. </param>
        /// <param name="depthAdjust1"> The first depth adjustment for the jitter effect. Defaults to 0.005 if not provided. </param>
        /// <param name="depthAdjust2"> The second depth adjustment for the jitter effect. Defaults to 0.250 if not provided. </param>
        /// <inheritdoc cref="_default" />
        public struct _vibraphase { }

        // Aliases for ones in SynthWishes

        /// <inheritdoc
        ///     cref="wishdocs._default" />
        public struct _default { }

        /// <inheritdoc
        ///     cref="_samplingrate" />
        public struct _samplingrate { }

        /// <inheritdoc
        ///     cref="wishdocs._tremolo" />
        public struct _tremolo { }

        /// <inheritdoc
        ///     cref="wishdocs._vibrato" />
        public struct _vibrato { }
    }
}
