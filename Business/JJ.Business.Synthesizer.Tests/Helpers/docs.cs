using wishes = JJ.Business.Synthesizer.Wishes.Helpers;

#pragma warning disable CS0649
#pragma warning disable CS0169 // Field is never used

// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal struct docs
    {
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
        /// <inheritdoc cref="docs._default" />
        public static object _detunica;

        /// <inheritdoc cref="wishes.docs._default" />
        public static object _default;

        /// <summary>
        /// An airy sound with harmonics, a high-pitch sample for attack,
        /// separate curves for each partial, triggers a wav header auto-detect.
        /// </summary>
        /// <inheritdoc cref="docs._default" />
        public static object _metallophone;
    }
}
