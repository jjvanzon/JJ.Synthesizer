using JJ.Business.CanonicalModel;
using JJ.Persistence.Synthesizer;
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

#pragma warning disable CS0649
#pragma warning disable CS0169 // Field is never used

namespace JJ.Business.Synthesizer.Wishes.Helpers
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
        /// <param name="panning">
        /// An <see cref="Outlet" /> or <see cref="System.Double" />
        /// representing the panning value,
        /// where 0 is fully left, 1 is fully right, and 0.5 is centered.
        /// </param>
        /// <param name="panbrello">
        /// A tuple containing the speed and depth of the panbrello effect.
        /// If not provided, they will default to 1.
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
        public object _createcurvefromstring;

        /// <summary>
        /// Create a curve from a list of tuples like (0, 0), (0.1, 0.2), (0.2, 1.0).<br />
        /// NOTE: If you get the wrong curve back, see <paramref name="key" /> parameter for info about caching.
        /// </summary>
        /// <inheritdoc cref="_createcurve" />
        public object _createcurvewithtuples;

        /// <summary>
        /// NOTE: If you get the wrong curve back, see <paramref name="key" /> parameter about caching.
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
        public object _asconst;
        
        /// <summary>
        /// Generates a sine wave signal with the specified pitch.<br />
        /// Simpler variation on the one in the original OperatorFactory
        /// with pitch as the first and only parameter.
        /// </summary>
        /// <param name="pitch"> The frequency in Hz of the sine wave. Defaults to 1 Hz.</param>
        /// <returns> An <see cref="Outlet" /> representing the sine wave signal. </returns>
        /// <inheritdoc cref="_default" />
        public object _sine;
        
        /// <summary>
        /// Applies panning to a stereo signal by adjusting the left and right
        /// channel volumes based on the specified panning value.
        /// TODO: A variable panning might go into the negative. Should be clamped to 0-1.
        /// </summary>
        /// <param name="channel">
        /// The channel for which to calculate the panning.
        /// If omitted, the <see cref="SynthWishes.Channel"/> property is used,
        /// which indicates the current channel.
        /// </param>
        /// <inheritdoc cref="_default" />
        public object _panning;

        /// <summary>
        /// Applies a panbrello effect to a stereo signal by modulating the panning
        /// with a sine wave based on the specified speed and depth.
        /// </summary>
        /// <inheritdoc cref="_default" />
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
        /// <inheritdoc cref="_default" />
        public object _pitchpan;

        /// <summary>
        /// Colloquially called the "capture" operator.
        /// Allows capturing double values into the synthesizer,
        /// and can be used to start fluent notation / method chaining.
        /// Double values can be captured as follows: <c>_[440]</c>
        /// It wraps a literal number inside a Value <see cref="Operator"/>, 
        /// so that can always be substituted by a whole formula /
        /// graph / calculation / curve that varies over time for added flexibility.
        /// Here is an example of using it to start fluent notation / method chaining:
        /// <c>_[freq].Sine.Multiply(2)</c>
        /// Sometimes capturing goes automatically, so only use it, if it otherwise won't take it.
        /// </summary>
        /// <returns>
        /// <see cref="FluentOutlet"/> also usable as an <see cref="Outlet"/>.
        /// or as a fluent notation starter <see cref="FluentOutlet"/>.
        /// </returns>
        public object _captureindexer;

        /// <summary>
        /// Outputs audio to a WAV file and plays it if needed.<br />
        /// A single <see cref="Outlet" /> will result in Mono audio.<br />
        /// Use a func returning an <see cref="Outlet" /> e.g. <c> SaveAudio(() => MySound()); </c> <br />
        /// For Stereo it must return a new outlet each time.<br />
        /// <strong> So call your <see cref="Outlet" />-creation method in the Func! </strong> <br />
        /// If parameters are not provided, defaults will be employed.
        /// Some of these defaults you can set in the configuration file.
        /// Also, the entity data tied to the outlet will be verified.
        /// </summary>
        /// <param name="func">
        /// A function that provides a signal.
        /// Can be used for both Mono and Stereo sound.
        /// </param>
        /// <param name="monoChannel">
        /// An Outlet that provides the Mono signal.
        /// Use () => myOutlet for stereo instead.
        /// </param>
        /// <param name="stereoChannels">
        /// A tuple of two outlets, one for the Left channel, one for the Right channel.
        /// </param>
        /// <param name="channels">
        /// A list of outlets, one for each channel,
        /// e.g. a single one for Mono and 2 outlets for stereo.
        /// </param>
        /// <param name="duration">
        /// The duration of the audio in seconds. When 0, the default duration of 1 second is used.
        /// </param>
        /// <param name="volume">
        /// The volume level of the audio. If null, the default volume is 1 (full volume).
        /// </param>
        /// <param name="speakerSetupEnum">
        /// The speaker setup configuration (e.g., Mono, Stereo).
        /// </param>
        /// <param name="fileName">
        /// The name of the file to save the audio to.
        /// If null, a default file name is used, based on the caller's name.
        /// If no file extension is provided, ".wav" is assumed.
        /// </param>
        /// <param name="samplingRateOverride">
        /// Overrides the sampling rate that was otherwise taken from the config file.
        /// If you want to test for specific values of specific sample frames, you can use this.
        /// <br/>
        /// NOTE: This also overrides optimizations for tooling such as NCrunch code coverage and
        /// Azure Pipelines automated build. So use with caution.
        /// </param>
        /// <param name="callerMemberName">
        /// The name of the calling method. This is automatically set by the compiler.
        /// </param>
        /// <returns>
        /// A <see cref="Result"/> with the <see cref="AudioFileOutput"/> entity in it,
        /// containing resultant data, like the file path and validation messages (warnings).
        /// </returns>
        public object _saveorplay;
    
        /// <summary>
        /// Retrieves the file extension associated with the specified audio file.
        /// </summary>
        /// <param name="enumValue">The audio file format enumeration value.</param>
        /// <returns>
        /// The file extension corresponding to the provided audio file format.
        /// A period (.) is included.
        /// </returns>
        public object _fileextension;

        /// <returns> Length of a file header in bytes. </returns>
        public object _headerLength;

        /// <summary> Adds two <see cref="Outlet"/> operands, optimizing for constant values if possible. </summary>
        public object _add;

        /// <summary> Multiplies two <see cref="Outlet"/> operands, optimizing for constant values if possible. </summary>
        public object _multiply;

        /// <summary>
        /// Sets the speaker setup for the specified <see cref="AudioFileOutput"/>.
        /// Executes some side effects to initialize the <see cref="AudioFileOutput"/> channels.
        /// </summary>
        /// <summary>
        /// </summary>
        /// <param name="audioFileOutput">The <see cref="AudioFileOutput"/> to update.</param>
        /// <param name="speakerSetup">The <see cref="SpeakerSetup"/> to apply to the <paramref name="audioFileOutput"/>.</param>
        /// <param name="context">
        /// Optional. The context for persistence operations.
        /// If one isn't provided, a brand new one will be created,
        /// which depending on the situation, could cause problems or not.
        /// </param>
        public object _setspeakersetup_withsideeffects;

        /// <summary>
        /// Creates a Sample by reading the file at the given <paramref name="filePath" /> or Stream or Byte array.
        /// </summary>
        /// <param name="filePath"> The file path of the audio sample to load. </param>
        /// <returns> <see cref="SampleOperatorWrapper" />  that can be used as an <see cref="Outlet" /> too. </returns>
        public object _sample;

        /// <summary>
        /// Turns an <see cref="Operator"/> graph into a string,
        /// in a way that the complexity becomes apparent.
        /// Having each <see cref="Operator"/> be on its own line,
        /// the complexity can be expressed as the number of lines.
        /// Optionally, it can be output in a single line.
        /// </summary>
        public object _stringify;

        /// <returns> <see cref="ValueWrapper"/> which can also be used as an Outlet or a <see langword="double"/>. </returns>
        public object _timeindexer;

        /// <summary>
        /// Performs an addition in parallel to improve performance.<br/>
        /// It uses a trick to make use of existing functions.<br/>
        /// It saves each term to an audio file.<br/>
        /// Then it reloads each using a
        /// <see cref="Sample">Sample</see><see cref="Operator">Operator</see>.<br/>
        /// Then <see cref="ParallelAdd">ParallelAdd</see> returns
        /// a normal non-parallel <see cref="Add">Add</see> operator,
        /// that will add up all the <see cref="Sample">Samples</see>.<br/>
        /// Please set the volume to something that doesn't make the partials go over the max.<br/>
        /// You also need to call the fluent WithAudioLength or AddAudioLength to set the buffer size.
        /// </summary>
        /// <param name="funcs">Lambdas each returning a term for the addition.</param>
        /// <returns>
        /// A normal <see cref="Add">Add</see> <see cref="Operator">Operator's</see>
        /// <see cref="Outlet">Outlet</see> (as <see cref="FluentOutlet">FluentOutlet</see>).
        /// </returns>
        public object _paralleladd;

        /// <summary> Overrides the default sampling rate from the config file, for testing purposes. </summary>
        public object _samplingrateoverride;
    }
}