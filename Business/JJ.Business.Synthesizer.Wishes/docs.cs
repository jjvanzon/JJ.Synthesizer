using JJ.Business.CanonicalModel;
using JJ.Persistence.Synthesizer;

// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

#pragma warning disable CS0649
#pragma warning disable CS0169 // Field is never used

namespace JJ.Business.Synthesizer.Wishes
{
    internal struct docs
    {
        /// <summary>
        /// Adds two <see cref="Outlet"/> operands, optimizing for constant values if possible.
        /// </summary>
        public static object _add;

        /// <summary>
        /// Additional entity entry-points for enum-related extension.
        /// </summary>
        public static object _alternativeentrypointenumextensionwishes;
            
        /// <summary>
        /// Tries getting a constant value from an operator or outlet.
        /// If it is dynamic, so no constant value, null is returned.
        /// </summary>
        public static object _asconst;

        /// <summary>
        /// Replacement Wish version of AudioFileInfo with more intuitive member names.
        /// </summary>
        public static object _audioinfowish;
        
        /// <summary> Using a lower abstraction layer, to circumvent error-prone syncing code in back-end. </summary>
        public static object _avoidSpeakerSetupsBackEnd;
        
        /// <summary>
        /// Returns the time in seconds of the start of a bar.
        /// </summary>
        public static object _barindexer;

        /// <summary>
        /// Returns duration of a number of bars in seconds.
        /// </summary>
        public static object _barsindexer;

        /// <summary>
        /// Returns the start time of a beat in seconds.
        /// </summary>
        public static object _beatindexer;
        
        /// <summary>
        /// Returns duration of a number of beats in seconds.
        /// </summary>
        public static object _beatsindexer;
            
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
        /// <see cref="FlowNode"/> also usable as an <see cref="Outlet"/>.
        /// or as a fluent notation starter <see cref="FlowNode"/>.
        /// </returns>
        public static object _captureindexer;

        /// <summary>
        /// This ConfigurationHelper internally handles null-tolerance for the data missing from the app.config file.
        /// It returns defaults if config items are missing, to make it easier to use SynthWishes.
        /// </summary>
        public static object _confighelper;

        /// <param name="curveFactory">
        /// The factory used to create the <see cref="Curve" />
        /// </param>
        /// <param name="nodeTuples">
        /// A list of tuples representing the nodes,
        /// where each tuple contains the x and y coordinates of a node.
        /// </param>
        /// <param name="values">
        /// When a value is null, a node will not be created at that point in time.
        /// </param>
        /// <returns> A curve populated with the specified data. </returns>
        public static object _createcurve;

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
        public static object _createcurvefromstring;

        /// <summary>
        /// Create a curve from a list of tuples like (0, 0), (0.1, 0.2), (0.2, 1.0).<br />
        /// NOTE: If you get the wrong curve back, see <paramref name="key" /> parameter for info about caching.
        /// </summary>
        /// <inheritdoc cref="_createcurve" />
        public static object _createcurvewithtuples;

        /// <summary>
        /// Creates a new repository, of the given interface type TInterface.
        /// If the context isn't provided, a brand new one is created, based on the settings from the config file.
        /// Depending on the use-case, creating a new context like that each time can be problematic.
        /// </summary>
        public static object _createrepository;
        
        /// <summary>
        /// Paired with an operator, this method creates a curve serving as a volume envelope,
        /// which makes the operator gets multiplied by the curve for it to serve as the volume.
        /// </summary>
        public static object _curvewithoperator;
        
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
        public static object _default;
        
        /// <inheritdoc cref="_tapesanddiskcache" />
        public static object _diskcacheon;

        /// <summary>
        /// Applies an echo effect using a feedback loop.
        /// The goal is to make it more efficient than an additive approach by reusing double echoes 
        /// to generate quadruple echoes, so 4 echoes take 2 iterations, and 8 echoes take 3 iterations.
        /// However, since values from the same formula are not yet reused within the final calculation,
        /// this optimization is currently ineffective. Future versions may improve on this.
        /// Keeping it in here just to have an optimization option for later.
        /// </summary>
        public static object _echofeedback;

        /// <summary>
        /// Gets the name chose by the user with the WithName method and then resets it to null
        /// after it retrieves it. If nothing was in it, it uses the fallback name supplied.
        /// Also, if an explicitName is passed, it will override all the other options.
        /// </summary>
        public static object _fetchname;
            
        /// <summary>
        /// Alternative entry point (Operator) Outlet (used in tests).
        /// </summary>
        public static object _flattenfactorswithmultiplyoutlet;

        /// <summary>
        /// Alternative entry point (Operator) Outlet (used in tests).
        /// </summary>
        public static object _flattentermswithsumoradd;

        /// <summary>
        /// Works similar to Sum, but instead of adding up all the numbers, all the numbers are multiplied.
        /// If the collection is empty, returns 1 (the null-operation for a multiplication).
        /// </summary>
        public static object _frameworkwishproduct;
        
        /// <summary>
        /// If this is a curve operator, this will return the underlying Curve entity,
        /// that contains specifics about the nodes and how they are connected.
        /// If it's called on something that isn't a Curve, an exception will be thrown.
        /// </summary>
        public static object _underlyingcurve;

        /// <summary>
        /// If this is a sample operator, this will return a curve operator wrapper,
        /// which serves as a helper for retrieving specifics from the underlying Operator
        /// and Curve entities.
        /// </summary>
        public static object _getcurvewrapper;
        
        /// <summary>
        /// If this is a sample operator, this will return the underlying Sample entity,
        /// with configuration, byte array, etc. If it's not a sample operator,
        /// it will throw an exception.
        /// </summary>
        public static object _underlyingsample;

        /// <summary>
        /// If this is a sample operator, this will return a sample operator wrapper,
        /// which serves as a helper for retrieving specifics from the underlying Operator
        /// and Sample entities.
        /// </summary>
        public static object _getsamplewrapper;
            
        /// <summary>
        /// Retrieves the file extension associated with the specified audio file.
        /// </summary>
        /// <param name="enumValue">The audio file format enumeration value.</param>
        /// <returns>
        /// The file extension corresponding to the provided audio file format.
        /// A period (.) is included.
        /// </returns>
        public static object _fileextension;

        /// <summary>
        /// A.k.a. SampleCount
        /// </summary>
        public static object _framecount;
        
        /// <returns>
        /// Length of a file header in bytes.
        /// </returns>
        public static object _headerLength;

        /// <summary>
        /// Returns the current method name or current property name.
        /// </summary>
        public static object _membername;

        /// <summary>
        /// Multiplies two <see cref="Outlet"/> operands, optimizing for constant values if possible.
        /// </summary>
        public static object _multiply;

        /// <summary>
        /// <c>WithName()</c> is prefix: it is applied to the next object.<br/>
        /// <c>SetName()</c> is suffix: it is applied to the object before it.<br/>
        /// <c>Name</c> returns the last name assigned using WithName(). <br/>
        /// You can pass the desired name as a parameter, but if you omit it, the name of the current member
        /// you are in is used. So the place where you code, that method's name, becomes
        /// the name for the object you're about to create or are calling upon.
        /// Some methods work without WithName,
        /// in that they'll automatically pick up the name of the member called from.
        /// This only does not work with params / variable amount of arguments
        /// where the parameters separate with commas describe a list of items.
        /// Calling WithName or SetName explicitly there helps.
        /// The names are mostly optional, but they can be useful as a diagnostic tool.
        /// </summary>
        public static object _names;

        /// <summary>
        /// Returns what's input into an operand of the operator.
        /// </summary>
        public static object _operand;

        /// <summary>
        /// The parallel processing, processes a tree of tasks, "leaf" tasks first.
        /// But as soon as they get processed, more "leaf" tasks emerge up for processing.
        /// There's a time interval between checking for new leaves to process.
        /// <c>ParallelTaskCheckDelay</c> defines this (short) wait time.
        /// Its unit is in seconds, but it only has a millisecond precision.
        /// 1 millisecond is usually good enough (0.001).
        /// But since this may be infrastructure-dependent, it can be configured.
        /// Anything under the minimum of 1 milliseconds (0.001) rounds down to 0,
        /// meaning it'll check a lot for new tasks to process.
        /// This can mean millions of checks instead of hundreds,
        /// that's why this small delay exists.
        /// </summary>
        public static object _paralleltaskcheckdelay;
        
        /// <inheritdoc cref="_tapesanddiskcache" />
        public static object _playalltapes;
        
        /// <summary>
        /// Extensions that are wishes for the back-end related that retrieve related objects like the Operator, Curve or Sample entities.
        /// </summary>
        public static object _relatedobjectextensions;

        /// <summary>
        /// Determines the configured sampling rate.
        /// This can be set in the <c>.config</c> file,
        /// and can be different depending on the environment.<br/>
        /// For instance <strong>NCrunch</strong> (a code coverage tool)
        /// and <strong>Azure Pipelines</strong> can have
        /// an alternative (lower) sampling rate configured,
        /// for those tools to perform better.<br/>
        /// <br/>
        /// The sampling rate can be overridden in C# using the
        /// <c>.WithSamplingRate()</c> fluent configuration method.
        /// This is usually for testing purposes.
        /// Prefer using the <c>.config</c> file instead.<br/>
        /// </summary>
        public static object _withsamplingrate;

        /// <summary>
        /// Back-end will need bytes wrapped in a Stream and will read it back into a byte[] again.
        /// This code would prevent that, but won't kick off the wav header parsing,
        /// which is important as a test.
        /// The WavHeaderWishes to solve both are currently lacking.
        /// Revisit later.
        /// </summary>
        public static object _samplefromfluentconfig;
        
        /// <summary>
        /// Allow specifying 1 value: make it the start and end node values.
        /// </summary>
        public static object _onebecomestwo;
        
        /// <summary>
        /// Applies a panbrello effect to a stereo signal by modulating the panning
        /// with a sine wave based on the specified speed and depth.
        /// </summary>
        /// <inheritdoc cref="_default" />
        public static object _panbrello;

        /// <summary>
        /// Applies panning to a stereo signal by adjusting the left and right
        /// channel volumes based on the specified panning value.
        /// TODO: A variable panning might go into the negative. Should be clamped to 0-1.
        /// </summary>
        /// <param name="channel">
        /// The channel for which to calculate the panning.
        /// If omitted, the <see cref="SynthWishes.GetChannel"/> property is used,
        /// which indicates the current channel.
        /// </param>
        /// <inheritdoc cref="_default" />
        public static object _panning;

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
        /// You also need to call the fluent <see cref="SynthWishes.WithAudioLength">WithAudioLength</see>
        /// or <see cref="SynthWishes.AddAudioLength">AddAudioLength</see> methods to set the buffer size.
        /// </summary>
        /// <param name="funcs">Lambdas <c>() => ...</c> each returning a term for the addition.</param>
        /// <returns>
        /// A normal <see cref="Add">Add</see> <see cref="Operator">Operator's</see>
        /// <see cref="Outlet">Outlet</see> (as <see cref="FlowNode">FlowNode</see>).
        /// </returns>
        public static object _paralleladd;
        
        /// <inheritdoc cref="_tapesanddiskcache" />
        public static object _paralleltaping;

        /// <summary>
        /// When PlayAllTapes is set, Tape and ParallelAdd play the sounds generated in the parallel loop or
        /// other parallel tasks for testing purposes.<br/>
        /// DiskCacheOn controls whether the tapes will be cached to disk instead of memory,
        /// in case of which it also doesn't clean up the files. Mostly for testing purposes.
        /// But could be used in low-memory high-disk-space scenarios.<br/>
        /// With the Parallels setting you can turn off parallel processing completely.
        /// </summary>
        public static object _tapesanddiskcache;

        /// <summary>
        /// Can get persistence configuration from config, or otherwise falls back
        /// to default in-memory persistence.
        /// </summary>
        public static object _persistencehelper;
            
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
        public static object _pitchpan;

        /// <summary>
        /// Creates a Sample by reading the file at the given <paramref name="filePath" /> or Stream or Byte array.
        /// </summary>
        /// <param name="filePath"> The file path of the audio sample to load. </param>
        /// <returns> <see cref="SampleOperatorWrapper" />  that can be used as an <see cref="Outlet" /> too. </returns>
        public static object _sample;

        /// <summary>
        /// Overrides the default sampling rate from the config file (for testing purposes).
        /// If you set it back to default it will use the config again, e.g. WithSamplingRate(default).
        /// </summary>
        public static object _getsamplingrate;

        /// <summary>
        /// Outputs audio in an audio file format and plays it if needed.<br />
        /// A single <see cref="Outlet">Outlet</see> will result in Mono audio.<br />
        /// Use a func returning an <see cref="Outlet">Outlet</see> e.g. <c> Save(() => MySound()); </c> <br />
        /// For Stereo it must return a new outlet each time.<br />
        /// <strong> So call your <see cref="Outlet">Outlet</see>-creation method in the Func! </strong> <br />
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
        /// <param name="additionalMessages">
        /// Can be null.<br/>
        /// Inserts additional text in the result messages
        /// in an appropriate spot for overview.
        /// </param>
        /// <returns>
        /// A <see cref="Result"/> with the <see cref="AudioFileOutput"/> entity in it,
        /// containing resultant data, like the file path and validation messages (warnings).
        /// </returns>
        public static object _saveorplay;

        /// <summary> Nullable. Not supplied when DiskCacheOn. </summary>
        /// <param name="bytes">Nullable. Not supplied when DiskCacheOn.</param>
        public static object _buffbytes;
        
        /// <summary>
        /// With optional Context.
        /// </summary>
        public static object _setenumwishes;

        /// <summary>
        /// Sets the node type (the type of interpolation) for a curve as a whole.
        /// This sets the node type of all the curve nodes at once.
        /// </summary>
        public static object _setnodetype;

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
        public static object _setspeakersetup_withsideeffects;

        /// <summary>
        /// Generates a sine wave signal with the specified pitch.<br />
        /// Simpler variation on the one in the original OperatorFactory
        /// with pitch as the first and only parameter.
        /// </summary>
        /// <param name="pitch"> The frequency in Hz of the sine wave. Defaults to 1 Hz.</param>
        /// <returns> An <see cref="Outlet" /> representing the sine wave signal. </returns>
        /// <inheritdoc cref="_default" />
        public static object _sine;

        /// <summary>
        /// Turns an <see cref="Operator"/> graph into a string,
        /// in a way that the complexity becomes apparent.
        /// Having each <see cref="Operator"/> be on its own line,
        /// the complexity can be expressed as the number of lines.
        /// Optionally, it can be output in a single line.
        /// </summary>
        public static object _stringify;

        /// <summary>
        /// Uses the channel specified by the <see cref="SynthWishes.GetChannel"/> property.
        /// Or you can call e.g. <c>Outlet.Calculate(time, ChannelEnum.Right)</c>
        /// </summary>
        public static object _synthwishescalculate;
            
        /// <summary>
        /// This TimeIndexer provides shorthand for specifying bar and beat in a musical sense.
        /// Access by bar and beat to get time-based value.
        /// Example usage: t[bar: 2, beat: 1.5] will return the number of seconds.
        /// The numbers are 1-based, so the first bar is bar 1, the first beat is beat 1.
        /// </summary>
        public static object _timeindexer;

        /// <summary>
        /// Apply _tremolo by modulating amplitude over time using an oscillator.
        /// </summary>
        /// <inheritdoc cref="_default" />
        public static object _tremolo;

        /// <summary>
        /// Prep Data: Split into unique lines and determine the window where there are characters.
        /// White space is trimmed off of the top, bottom, left and right,
        /// leaving only the block of characters that contains data.
        /// </summary>
        public static object _trimasciicurves;

        /// <summary>
        /// Gets the node type (the type of interpolation) for a curve as a whole.
        /// This only works if all (but the last) node are set to the same node type.
        /// Otherwise, NodeTypeEnum.Undefined is returned.
        /// </summary>
        public static object _trygetnodetype;
        
        /// <summary>
        /// This null-tolerant version is missing in JJ.Framework.Configuration for now.
        /// </summary>
        public static object _trygetsection;

        /// <summary>
        /// Applies vibrato modulation to a given frequency by modulating it with a sine wave.<br />
        /// NOTE: Due to the lack of phase tracking, the vibrato depth tends to accumulate over time.
        /// </summary>
        /// <param name="freq"> The base frequency to which vibrato will be applied. </param>
        /// <returns> An <see cref="Outlet" /> object representing the frequency modulated with vibrato. </returns>
        /// <inheritdoc cref="_default" />
        public static object _vibrato;
    }
}