// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
#pragma warning disable CS0649 // Field never assigned

using JetBrains.Annotations;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.EntityWrappers;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal class docs
    {
        /// <param name="freq"> The base frequency of the sound in Hz (default is A4/440Hz). </param>
        /// <param name="frequency"> The base frequency of the sound in Hz (default is A4/440Hz). </param>
        /// <param name="delay"> The time delay in seconds before the sound starts (default is 0). </param>
        /// <param name="vol"> The volume of the sound (default is 1). </param>
        /// <param name="volume"> The volume of the sound (default is 1). </param>
        /// <param name="duration"> The duration of the sound in seconds (default is 1). </param>
        /// <param name="soundFreq"> The base frequency in Hz for the carrier signal for the FM synthesis. </param>
        /// <param name="modSpeed"> The speed of the modulator in Hz. Determines much of the timbre. </param>
        /// <param name="modDepth"> The depth of the modulator. The higher the value, the more harmonic complexity. </param>
        /// <param name="sound"> The sound to be shaped. </param>
        /// <param name="tremoloSpeed">The speed in Hz at which the volume goes up and down. A typical value can be 8 Hz.</param>
        /// <param name="tremoloDepth">
        /// The factor by which the volume decreases and increases periodically.
        /// A good default might be a factor of 0.33 of the volume.
        /// </param>
        /// <param name="vibratoSpeed">The speed of the vibrato modulation. A typical value might be 5.5.</param>
        /// <param name="vibratoDepth">The depth of the vibrato modulation. Typical values might include 0.0005.</param>
        /// <returns> An Outlet representing the output sound. </returns>
        public static object _default;

        /// <summary>
        /// Create a Curve from a list of strings, that 'ASCII-encode' the curve. Putting the strings under each other, they create
        /// a block of space with time on the horizontal axis and values on the vertical axis. The background character is usually
        /// just a space character, but any other background character is possible and automatically recognized. Any character
        /// other than the background character is seen as a data point. That way you can creatively choose your own characters.
        /// White space is trimmed off of the top, bottom, left and right, leaving only the block of characters that contains data.<br/>
        /// NOTE: If you get the wrong curve back, see <paramref name="key"/> parameter for info about caching.
        /// </summary>
        /// <inheritdoc cref="createcurve" />
        public static object createcurvefromstrings;

        /// <summary>
        /// Create a curve from a list of tuples like (0, 0), (0.1, 0.2), (0.2, 1.0).<br/>
        /// NOTE: If you get the wrong curve back, see <paramref name="key"/> parameter for info about caching.
        /// </summary>
        /// <inheritdoc cref="createcurve" />
        public static object createcurvewithtuples;

        /// <summary>
        /// NOTE: If you get the wrong curve back, see <paramref name="key"/> parameter for info about caching.
        /// </summary>
        /// <param name="curveFactory"> The factory used to create the <see cref="Curve" />. </param>
        /// <param name="nodeTuples">
        /// A list of tuples representing the nodes,
        /// where each tuple contains the x and y coordinates of a node.
        /// </param>
        /// <param name="key">
        /// The cache key for the curve.
        /// Using the same key for two different curves won't work.
        /// If you don't specify a key yourself, the calling member's name may be used.
        /// If no key at all can be resolved, an exception is thrown.
        /// </param>
        /// <returns> A curve populated with the specified data. </returns>
        public static object createcurve;
    }
}