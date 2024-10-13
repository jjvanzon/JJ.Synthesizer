using JetBrains.Annotations;
using JJ.Persistence.Synthesizer;

#pragma warning disable CS0649
#pragma warning disable CS0169 // Field is never used

// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal struct docs
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
        /// <param name="tremolo">
        /// tremolo.speed:<br />
        /// The speed in Hz at which the volume goes up and down.
        /// A typical value can be 8 Hz.
        /// <br /><br />
        /// tremolo.depth:<br />
        /// The factor by which the volume decreases and increases periodically.
        /// A good default might be a factor of 0.33 of the volume.
        /// </param>
        /// <param name="vibrato">
        /// vibrato.speed: The speed of the vibrato modulation. A typical value might be 5.5.<br />
        /// vibrato.depth: The depth of the vibrato modulation. Typical values might include 0.0005.
        /// </param>
        /// <returns> An Outlet representing the output sound. </returns>
        public object _default;

        /// <summary>
        /// Applies vibrato modulation to a given frequency by modulating it with a sine wave.<br />
        /// NOTE: Due to the lack of phase tracking, the vibrato depth tends to accumulate over time.
        /// </summary>
        /// <param name="freq"> The base frequency to which vibrato will be applied. </param>
        /// <returns> An <see cref="Outlet" /> object representing the frequency modulated with vibrato. </returns>
        /// <inheritdoc cref="_default" />
        public object _vibrato;

        /// <summary> Apply _tremolo by modulating amplitude over time using an oscillator. </summary>
        /// <inheritdoc cref="_default" />
        public object _tremolo;

        /// <summary>
        /// Create a Curve from a list of strings, that 'ASCII-encode' the curve. Putting the strings under each other, they create
        /// a block of space with time on the horizontal axis and values on the vertical axis. The background character is usually
        /// just a space character, but any other background character is possible and automatically recognized. Any character
        /// other than the background character is seen as a data point. That way you can creatively choose your own characters.
        /// White space is trimmed off of the top, bottom, left and right, leaving only the block of characters that contains data.
        /// <br />
        /// NOTE: If you get the wrong curve back, see <paramref name="key" /> parameter for info about caching.
        /// </summary>
        /// <inheritdoc cref="_createcurve" />
        public object _createcurvefromstrings;

        /// <summary>
        /// Create a curve from a list of tuples like (0, 0), (0.1, 0.2), (0.2, 1.0).<br />
        /// NOTE: If you get the wrong curve back, see <paramref name="key" /> parameter for info about caching.
        /// </summary>
        /// <inheritdoc cref="_createcurve" />
        public object _createcurvewithtuples;

        /// <summary>
        /// NOTE: If you get the wrong curve back, see <paramref name="key" /> parameter for info about caching.
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
        public object _createcurve;

        /// <summary>
        /// Tries getting a constant value from an operator or outlet.
        /// If it is dynamic, so no constant value, null is returned.
        /// </summary>
        /// <returns> </returns>
        public object _asconst;
        
        /// <summary>
        /// Generates a sine wave signal with the specified pitch.<br />
        /// Simpler variation on the one in the original OperatorFactory
        /// with pitch as the first and only parameter.
        /// </summary>
        /// <param name="pitch"> The frequency in Hz of the sine wave. </param>
        /// <returns> An <see cref="Outlet" /> representing the sine wave signal. </returns>
        public object _sine;
        
        /// <summary>
        /// Applies panning to a stereo signal by adjusting the left and right
        /// channel volumes based on the specified panning value.
        /// TODO: A variable panning might go into the negative. Should be clamped to 0-1.
        /// </summary>
        /// <param name="panning">
        /// An <see cref="Outlet" /> or <see cref="System.Double" />
        /// representing the panning value,
        /// where 0 is fully left, 1 is fully right, and 0.5 is centered.
        /// </param>
        /// <param name="channels">
        /// A tuple containing the left and right channels of the stereo signal.
        /// </param>
        /// <returns>
        /// A tuple containing the adjusted left and right channels
        /// after applying the panning.
        /// </returns>
        public object _panning;

        /// <summary>
        /// Applies a panbrello effect to a stereo signal by modulating the panning
        /// with a sine wave based on the specified speed and depth.
        /// </summary>
        /// <param name="channels">
        /// A tuple containing the left and right channels of the stereo signal.
        /// </param>
        /// <param name="panbrello">
        /// A tuple containing the speed and depth of the panbrello effect.
        /// If not provided, they will default to 1.
        /// </param>
        /// <returns>
        /// A tuple containing the adjusted left and right channels
        /// after applying the panbrello effect.
        /// </returns>
        public object _panbrello;
        
        /// <summary>
        /// Returns a panning based on the pitch,
        /// to spread different notes across a stereo field.
        /// (In other words: If the frequency is the referenceFrequency,
        /// then the panning is the referencePanning.
        /// Calculates the new panning for the supplied frequency by extrapolating.)
        /// </summary>
        /// <param name="actualFrequency">
        /// The frequency for which to calculate a panning value.
        /// </param>
        /// <param name="centerFrequency">
        /// The center frequency used as a reference point.
        /// Defaults to A4 if not provided.
        /// </param>
        /// <param name="referenceFrequency">
        /// The reference frequency to assign a specific panning value to.
        /// Defaults to E4 if not provided.
        /// </param>
        /// <param name="referencePanning">
        /// Panning value that the reference pitch would get.
        /// Defaults to 0.6 if not provided.
        /// </param>
        /// <returns> The adjusted panning value based on the pitch. </returns>
        public object _pitchpan;
 
        /// <summary>
        /// Shorthand for OperatorFactor.Value(123), x.Value(123) or Value(123). Allows using _[123] instead.
        /// Literal numbers need to be wrapped inside a Value Operator so they can always be substituted by
        /// a whole formula / graph / calculation / curve over time.
        /// </summary>
        /// <returns>
        /// ValueOperatorWrapper also usable as Outlet or double.
        /// </returns>
        public object _valueindexer;
    }
}