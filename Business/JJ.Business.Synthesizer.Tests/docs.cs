using wishdocs = JJ.Business.Synthesizer.Wishes.docs;
// ReSharper disable UnusedType.Global

#pragma warning disable CS0649
#pragma warning disable CS0169 // Field is never used

// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace JJ.Business.Synthesizer.Tests
{
    public struct docs
    {
        /// <summary>
        /// Can function as a single collection, but also as a collection of collections, <br/>
        /// and a helper for using structured test Cases. <br/>
        /// Provides integration with MSTest using DynamicData for parameterized data-driven testing. <br/><br/>
        ///
        /// Helps define a store the test cases into memory,
        /// retrieving them by key, conversion to DynamicData
        /// and Case templating.
        /// </summary>
        public struct _casecollection { }
        
        /// <summary>
        /// Creates new cases based on the specified template, applying its properties to the provided cases.
        /// </summary>
        /// <param name="template">The template case to apply.</param>
        /// <param name="cases">The cases to which the template is applied.</param>
        /// <param name="destCases">The cases to which the template is applied.</param>
        /// <returns>A collection of cases derived from the template.</returns>
        public struct _casetemplate { }
        
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
        /// <inheritdoc cref="docs._default" />
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
        /// <inheritdoc cref="docs._docs._default" />
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
            
        /// <summary>
        /// High hard flute: mod speed above sound freq, changes sound freq * [-0.005, 0.005] (erroneously)
        /// </summary>
        /// <inheritdoc cref="docs._default" />
        public struct _flute1 { }

        /// <summary> Yet another flute: mod speed above sound freq, changes sound freq * 1 +/- 0.005 </summary>
        /// <inheritdoc cref="docs._default" />
        public struct _flute2 { }

        /// <summary> Yet another flute: mod speed above sound freq, changes sound freq * 1 +/- 0.005 </summary>
        /// <inheritdoc cref="docs._default" />
        public struct _flute3 { }

        /// <summary> Modulated hard flute: mod speed below sound freq, changes sound freq * [-0.005, 0.005] (erroneously) </summary>
        /// <inheritdoc cref="docs._default" />
        public struct _flute4 { }

        /// <summary> FM with (faulty) multiplication around 0. </summary>
        /// <inheritdoc cref="docs._default" />
        public struct _fmaround0 { }

        /// <summary> FM with multiplication around 1. </summary>
        /// <inheritdoc cref="docs._default" />
        public struct _fmaroundfreq { }
            
        /// <summary> FM sound synthesis modulating with addition. Modulates sound freq to +/- a number of Hz. </summary>
        /// <param name="modDepth"> In Hz </param>
        /// <inheritdoc cref="docs._default" />
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
       
        /// <summary>
        /// <c>From</c>, <c>Init</c> and <c>Source</c> are synonyms.
        /// </summary>
        public struct _from { }
        
        /// <inheritdoc cref="wishdocs._getsamplingrate" />
        public struct _getsamplingrate { }
        
        /// <summary>
        /// Sounds like Horn.
        /// FM with mod speed below sound freq, changes sound freq to +/- 5Hz.
        /// Volume curve is applied.
        /// FM modulator is attempted to be tamed with curves.
        /// </summary>
        /// <param name="freq"> The base frequency of the sound in Hz (default A2/110Hz). </param>
        /// <inheritdoc cref="docs._default" />
        public struct _horn { }

        /// <summary>
        /// An airy sound with harmonics, a high-pitch sample for attack,
        /// separate curves for each partial, triggers a wav header auto-detect.
        /// </summary>
        /// <inheritdoc cref="docs._docs._default" />
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

        /// <summary>
        /// <strong> "NullyLen Lullaby" </strong> <br/>
        /// Tests various note length fallbacks, including explicit values,
        /// defaults in config files or based on the beat length
        /// or explicit specification in the note commands.
        /// Ends with a soothing lullaby to help this big baby calm down after all the bleeps.
        /// </summary>
        public struct _notelengthfallbacktests { }
        
        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.005 </summary>
        /// <param name="freq"> The base frequency of the sound in Hz (default A1/55Hz). </param>
        /// <inheritdoc cref="docs._default" />
        public struct _ripplebass { }

        /// <summary> Mod speed below sound freq, changes sound freq ±10Hz </summary>
        /// <param name="freq"> The base frequency of the sound in Hz (default A3/220Hz). </param>
        /// <inheritdoc cref="docs._default" />
        public struct _ripplenotesharpmetallic { }

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.005 </summary>
        /// <inheritdoc cref="docs._default" />
        public struct _ripplesoundclean { }

        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.05 </summary>
        /// <inheritdoc cref="docs._default" />
        public struct _ripplesoundcooldouble { }
            
        /// <summary> Mod speed way below sound freq, changes sound freq * 1 ± 0.02 </summary>
        /// <param name="duration"> The audioLength of the sound in seconds (default is 2.5). </param>
        /// <inheritdoc cref="docs._default" />
        public struct _ripplesoundfantasyeffect { }
        
        /// <summary>
        /// Generates a mild sawtooth-like waveform by combining multiple sine waves with different frequencies.
        /// </summary>
        /// <returns> A FlowNode representing the semi-sawtooth waveform. </returns>
        /// <inheritdoc cref="docs._default" />
        public struct _semisaw { }

        /// <summary> Shapes a ripple effect sound giving it a volume envelope and a delay, volume and audioLength. </summary>
        /// <param name="duration"> The audioLength of the sound in seconds (default is 2.5). </param>
        /// <param name="fmSignal"> A ripple sound to be shaped </param>
        /// <inheritdoc cref="docs._default" />
        public struct _shaperipplesound { }

        /// <summary>
        /// Specifies whether strict validation is applied to ensure consistency between 
        /// <see cref="FrameCount">FrameCount</see>, <see cref="AudioLength">AudioLength</see>, 
        /// <see cref="SamplingRate">SamplingRate</see>, and <see cref="CourtesyFrames">CourtesyFrames</see>. <br/><br/>
        /// 
        /// - <c>true</c>: Validation ensures that <see cref="FrameCount">FrameCount</see>
        ///   matches the calculated  value based on <see cref="AudioLength">AudioLength</see>,
        ///   <see cref="SamplingRate">SamplingRate</see>, nd <see cref="CourtesyFrames">CourtesyFrames</see>. 
        ///   Inconsistencies result in exceptions, such as: <br/>
        ///   "Attempt to initialize FrameCount to 11 is inconsistent with FrameCount 4803 
        ///    based on initial values for AudioLength (0.1), SamplingRate (default 48000), and CourtesyFrames (3)." <br/>
        /// - <c>false</c>: Validation is relaxed, and mismatched values are allowed for scenarios 
        ///   where not all properties might be relevant to the test. <br/><br/>
        /// 
        /// Use this flag to test cases with or without strict mathematical relationships between these properties.
        /// </summary>
        public struct _strict { }
        
        /// <summary>
        /// Tests if members can be called without a <c>this.</c> qualifier.
        /// </summary>
        public struct _synthwishesderived { }
        
        /// <summary>
        /// Testing extension methods in <see cref="docs._AudioFileExtensionWishes" />
        /// that didn't get any coverage elsewhere.
        /// </summary>
        public struct _testattributewishesold { }

        /// <summary>
        /// Aims to test rare exception for code coverage.
        /// </summary>
        public struct _throwtests { }
        
        /// <summary>
        /// <c>To</c>, <c>Value</c>, <c>Val</c> and <c>Dest</c> are synonyms.
        /// </summary>
        public struct _to { }
        
        /// <summary>
        /// Sounds like Trombone at beginning.
        /// FM with mod speed below sound freq, changes sound freq to +/- 5Hz.
        /// Volume curve is applied.
        /// Higher notes are shorter, lower notes are much longer.
        /// </summary>
        /// <param name="freq"> The base frequency of the sound in Hz (default A1/55Hz). </param>
        /// <param name="durationFactor"> Duration varies with pitch, but can be multiplied by this factor (default is 1). </param>
        /// <inheritdoc cref="docs._default" />
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
        /// <inheritdoc cref="docs._default" />
        public struct _vibraphase { }

        // Aliases for ones in SynthWishes

        /// <inheritdoc
        ///     cref="wishdocs._default" />
        public struct _default { }

        /// <inheritdoc
        ///     cref="wishdocs._samplingrate" />
        public struct _samplingrate { }

        /// <inheritdoc
        ///     cref="wishdocs._tremolo" />
        public struct _tremolo { }

        /// <inheritdoc
        ///     cref="wishdocs._vibrato" />
        public struct _vibrato { }
    }
}
