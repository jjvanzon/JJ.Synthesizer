using JJ.Persistence.Synthesizer;

// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

#pragma warning disable CS0649
#pragma warning disable CS0169 // Field is never used
#pragma warning disable IDE1006 // Naming Styles
// ReSharper disable UnusedType.Global

namespace JJ.Business.Synthesizer.Wishes
{
    public struct docs
    {
        /// <summary> 
        /// Adds two <see cref="Outlet"/> operands, optimizing for constant values if possible.
        /// </summary> 
        public struct _add { }
            
        /// <summary> 
        /// Additional entity entry-points for enum-related extension.
        /// </summary> 
        public struct _alternativeentrypointenumextensionwishes { }

        /// <summary> 
        /// Tries getting a constant value from an operator or outlet.
        /// If it is dynamic, so no constant value, null is returned.
        /// </summary> 
        public struct _asconst { }

        /// <summary>
        /// A simplified "Wish" replacement for AudioFileInfo, featuring more intuitive member names.
        /// Designed as a lightweight object to mediate between different object types, 
        /// primarily for constructing WAV headers.
        /// </summary>
        public struct _audioinfowish { }

        /// <summary> 
        /// Configures the audio length for rendering tapes and other audio output, 
        /// influencing the total duration of audio processing. <br/> 
        /// This setting can be defined in terms of beats, bars, or seconds, 
        /// allowing for flexible and expressive timing configurations. <br/> 
        /// If not explicitly configured, the value defaults to 1 second, 
        /// though this can be overridden in the configuration file.
        /// 
        /// <para> 
        /// The following members work together to manage the <c> AudioLength </c> : <br/> <br/> 
        /// - <c> WithAudioLength </c> : Sets the audio length explicitly. <br/> 
        /// - <c> GetAudioLength </c> : Retrieves the currently configured audio length. <br/> 
        /// - <c> AddAudioLength </c> : Extends the existing audio length by a specified amount. <br/> 
        /// - <c> ResetAudioLength </c> : Resets the audio length configuration to its default state.
        /// </para> 
        ///
        /// <para> 
        /// Use sparingly for high-level scope definitions, as more granular 
        /// control can be achieved with <c> Tape(duration) </c> or note indexers like <br/> 
        /// <c> _[ t[1,1], A4, Flute, 0.6, len[1] ] </c> ,
        /// where <c> len[1] </c> sets the internal audio buffer length for the note.
        /// </para> 
        /// 
        /// <para> 
        /// That said, you can use <c> WithAudioLength() </c> for more granular control 
        /// if needed, but be aware that your code might "fight" over it. 
        /// You may need to remind the system what <c> AudioLength </c> we're working 
        /// with from time to time.
        /// </para> 
        /// 
        /// Example:
        /// <code> 
        /// WithAudioLength(bars[4]); // Sets audio length to 4 bars.
        /// WithAudioLength(_[10]);  // Sets audio length to 10 seconds.
        /// </code> 
        /// </summary> 
        public struct _audiolength { }

        /// <summary> 
        /// Setting might not work in all contexts 
        /// where the system is unaware of the SynthWishes object.
        /// This is because of a design decision in the software, that might be corrected later.
        ///
        /// <para> For instance with an extension method on Buff, e.g. buff.Play(), there is no
        /// SynthWishes or FlowNode involved, that can provide the custom set value.
        /// Things which would then default back to the config setting or hard-coded default. </para> 
        ///
        /// <para> The system correction that might solve it could be a change called
        /// Func-Free Stereo Tapes, which would make it rare you would operate
        /// directly on Buff or AudioOutput. </para> 
        /// 
        /// <para> Currently, just chaining .Play onto some previous
        /// command could make you lose the SynthWishes context.
        /// Omitting a this. qualifier could do that too. </para> 
        /// </summary> 
        public struct _audioplayback { }

        /// <summary>
        /// Standardizes audio properties among objects in the form of extension methods.
        /// In some cases they were missing in the back-end objects.
        /// In other cases they are for API objects, even if they were already otherwise available
        /// as properties or through sub-objects, or Get methods.
        /// They are repeated here just to standardize everything for fluency.
        /// </summary>
        public struct _configextensionwishes { }
        
        /// <summary> 
        /// Using a lower abstraction layer, to circumvent error-prone syncing code in back-end.
        /// </summary> 
        public struct _avoidspeakersetupsbackend { }

        /// <summary> 
        /// Returns the time in seconds of the start of a bar.
        /// The bars start counting at 1.
        /// Bar one's start time is at 0 seconds.
        /// </summary> 
        public struct _barindexer { }

        /// <summary> 
        /// Returns duration of a number of bars in seconds.
        /// 0 bars = a duration of 0 seconds.
        /// </summary> 
        public struct _barsindexer { }

        /// <summary> 
        /// Returns the start time of a beat in seconds.
        /// The beats start counting at 1.
        /// Beat one's start time is at 0 seconds.
        /// </summary> 
        public struct _beatindexer { }

        /// <summary> 
        /// Returns duration of a number of beats in seconds.
        /// 0 beats = a duration of 0 seconds.
        /// </summary> 
        public struct _beatsindexer { }

        /// <summary> Nullable. Not supplied when DiskCache. </summary> 
        /// <param name="bytes"> Nullable. Not supplied when DiskCache. </param> 
        public struct _buffbytes { }

        /// <summary> Formula based on primary audio properties. </summary>
        public struct _bytecountfromprimaries { }
            
        /// <inheritdoc
        ///    cref="_tapesanddiskcache" /> 
        public struct _diskcache { }

        /// <summary> 
        /// The "capture operator" or "capture indexer"
        /// captures something not originally fluent into the fluent notation.
        /// Allows capturing double values into the synthesizer,
        /// and can be used to start fluent notation / method chaining.
        /// Double values can be captured as follows: <c> _[440] </c> or <c> [440] </c> 
        /// It wraps a literal number inside a Value <see cref="Operator"/> , 
        /// so that can always be substituted by a whole formula /
        /// graph / calculation / curve that varies over time for added flexibility.
        /// Here is an example of using it to start fluent notation / method chaining:
        /// <c> _[freq].Sine.Multiply(2) </c> 
        /// Sometimes capturing goes automatically, so only use it, if it otherwise won't take it.
        /// </summary> 
        /// <returns> 
        /// <see cref="FlowNode"/> also usable as an <see cref="Outlet"/>.
        /// or as a fluent notation starter <see cref="FlowNode"/>.
        /// </returns> 
        public struct _captureindexer { }

        /// <summary>
        /// When AudioFileOutput has 1 channel, it will set the channel index of that channel to the supplied value.
        /// This way AudioFileOutput stand in to function as left-only or right-only.
        /// If the AudioFileOutput has 2 channels, the channel value has to be null,
        /// and it will reset the channel indexes to the order in which the channels occur in the list.
        /// That way you can revert an AudioFileOutput back to normal Mono or Stereo mode.
        /// </summary>
        public struct _channeltoaudiofileoutput { }

        /// <summary>
        /// <strong>Command Indexers</strong><br/>
        /// Allows notation such as <c>[ Panbrello ]</c> to apply the specified command
        /// to the current <see cref="FlowNode">FlowNode</see>.
        /// Enables chaining and shorthand notation for effects and transformations.
        /// <code>
        /// [ A4, Flute, 0.80 ] [ Panbrello ]
        /// </code>
        /// What you put before the command will become the first parameter passed to the command.
        /// </summary>
        /// <param name="command">
        /// The command to apply to the <see cref="FlowNode">FlowNode</see>.
        /// This can be any method that takes and returns FlowNode up to 10 parameters.
        /// </param>
        /// <returns>A new <see cref="FlowNode"/> with the command applied.</returns>
        public struct _commandindexer { }
        
        ///// <summary> 
        ///// This ConfigurationHelper internally handles null-tolerance for the data missing from the app.config file.
        ///// It returns defaults if config items are missing, to make it easier to use SynthWishes.
        ///// </summary> 
        //public struct _confighelper { }

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
        public struct _createcurve { }

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
        public struct _createcurvefromstring { }

        /// <summary> 
        /// Create a curve from a list of tuples like (0, 0), (0.1, 0.2), (0.2, 1.0). <br /> 
        /// NOTE: If you get the wrong curve back, see <paramref name="key" /> parameter for info about caching.
        /// </summary> 
        /// <inheritdoc cref="_createcurve" /> 
        public struct _createcurvewithtuples { }

        /// <summary> 
        /// Creates a new repository, of the given interface type TInterface.
        /// If the context isn't provided, a brand new one is created, based on the settings from the config file.
        /// Depending on the use-case, creating a new context like that each time can be problematic.
        /// </summary> 
        public struct _createrepository { }

        /// <summary> 
        /// Paired with an operator, this method creates a curve serving as a volume envelope,
        /// which makes the operator gets multiplied by the curve for it to serve as the volume.
        /// </summary> 
        public struct _curvewithoperator { }

        /// <param name="freq"> The base frequency of the sound in Hz (default is A4/440Hz). </param> 
        /// <param name="frequency"> The base frequency of the sound in Hz (default is A4/440Hz). </param> 
        /// <param name="delay"> The time delay in seconds before the sound starts (default is 0). </param> 
        /// <param name="vol"> 
        /// The volume of the sound (default is 1).
        /// For note commands, if the volume is a curve,
        /// it will be stretched to match the note length,
        /// assuming the curve has a timespan of 1.
        /// </param> 
        /// <param name="volume"> 
        /// The volume of the sound (default is 1).
        /// For note commands, if the volume is a curve,
        /// it will be stretched to match the note length,
        /// assuming the curve has a timespan of 1.
        /// </param> 
        /// <param name="duration"> The duration of the sound in seconds (default is 1). </param> 
        /// <param name="soundFreq"> The base frequency in Hz for the carrier signal for the FM synthesis. </param> 
        /// <param name="modSpeed"> The speed of the modulator in Hz. Determines much of the timbre. </param> 
        /// <param name="modDepth"> The depth of the modulator. The higher the value, the more harmonic complexity. </param> 
        /// <param name="sound"> The sound to be shaped. </param> 
        /// <param name="tremolo"> 
        /// tremolo.speed: <br /> 
        /// The speed in Hz at which the volume goes up and down.
        /// A typical value can be 8 Hz.
        /// <br /> <br /> 
        /// tremolo.depth: <br /> 
        /// The factor by which the volume decreases and increases periodically.
        /// A good default might be a factor of 0.33 of the volume.
        /// </param> 
        /// <param name="vibrato"> 
        /// vibrato.speed: The speed of the vibrato modulation. A typical value might be 5.5. <br /> 
        /// vibrato.depth: The depth of the vibrato modulation. Typical values might include 0.0005.
        /// </param> 
        /// <param name="panning"> 
        /// An <see cref="Outlet" /> or <see cref="System.Double" /> 
        /// representing the panning value,
        /// where 0 is fully left, 1 is fully right, and 0.5 is centered.
        /// </param> 
        /// <param name="panbrello"> 
        /// A tuple containing the speed and depth of the panbrello effect. <br/> 
        /// Default speed is 3 times a second. <br/> 
        /// Default depth is 1 = full volume going up and down.
        /// </param> 
        /// <returns> A FlowNode representing the output sound. </returns> 
        public struct _default { }

        /// <summary> 
        /// Applies an echo effect using a feedback loop.
        /// The goal is to make it more efficient than an additive approach by reusing double echoes 
        /// to generate quadruple echoes, so 4 echoes take 2 iterations, and 8 echoes take 3 iterations.
        /// However, since values from the same formula are not yet reused within the final calculation,
        /// this optimization is currently ineffective. Future versions may improve on this.
        /// Keeping it in here just to have an optimization option for later.
        /// </summary> 
        public struct _echofeedback { }

        /// <summary> 
        /// Gets the name chose by the user with the SetName method and then resets it to null
        /// after it retrieves it. If nothing was in it, it uses the fallback name supplied.
        /// Also, if an explicitName is passed, it will override all the other options.
        /// </summary> 
        public struct _resolvename { }

        /// <summary> 
        /// Retrieves the file extension associated with the specified audio file.
        /// </summary> 
        /// <param name="enumValue"> The audio file format enumeration value. </param> 
        /// <returns> 
        /// The file extension corresponding to the provided audio file format.
        /// A period (.) is included.
        /// </returns> 
        public struct _fileextension { }

        /// <summary>
        /// Extensions to the FrameworkWishes' FilledIn methods,
        /// specific to Synth objects.
        /// </summary>
        public struct _filledinhelper { }
            
        /// <summary> 
        /// Alternative entry point (Operator) Outlet (used in tests).
        /// </summary> 
        public struct _flattenfactorswithmultiplyoutlet { }

        /// <summary> 
        /// Wraps any <see cref="FlowNode"/> or outlet into a chaining-compatible syntax,
        /// enabling fluent-style chaining even for commands that are not inherently chaining methods.
        /// Example usage:
        /// <code> 
        /// WithStereo().WithAudioLength(2).Fluent(MyInstrument(A4)).Play();
        /// </code> 
        /// This allows you to seamlessly integrate <c> MyInstrument </c> into a chain where it would otherwise require multiple statements:
        /// <code> 
        /// WithStereo().WithAudioLength(2);
        /// MyInstrument(A4).Play();
        /// </code> 
        /// Note: This utility is still experimental and may not support all chaining scenarios. 
        /// It is designed to simplify the inclusion of non-chaining methods in fluent interfaces.
        /// </summary> 
        public struct _fluent { }
        
        /// <summary> 
        /// Alternative entry point (Operator) Outlet (used in tests).
        /// </summary> 
        public struct _flattentermswithsumoradd { }

        /// <summary> 
        /// A.k.a. SampleCount
        /// </summary> 
        public struct _framecount { }

        /// <summary> 
        /// Works similar to Sum, but instead of adding up all the numbers, all the numbers are multiplied.
        /// If the collection is empty, returns 1 (the null-operation for a multiplication).
        /// </summary> 
        public struct _frameworkwishproduct { }

        /// <summary> 
        /// If this is a sample operator, this will return a curve operator wrapper,
        /// which serves as a helper for retrieving specifics from the underlying Operator
        /// and Curve entities.
        /// </summary> 
        public struct _getcurvewrapper { }

        /// <summary> 
        /// If this is a sample operator, this will return a sample operator wrapper,
        /// which serves as a helper for retrieving specifics from the underlying Operator
        /// and Sample entities.
        /// </summary> 
        public struct _getsamplewrapper { }

        /// <summary> 
        /// Overrides the default sampling rate from the config file (for testing purposes).
        /// If you set it back to default it will use the config again, e.g. WithSamplingRate(default).
        /// </summary> 
        public struct _getsamplingrate { }

        /// <returns> 
        /// Length of a file header in bytes.
        /// </returns> 
        public struct _headerlength { }

        /// <returns> 
        /// Length of a file header in bytes.
        /// </returns>
        /// <inheritdoc cref="_quasisetter" />
        public struct _headerlengthquasisetter { }
        
        /// <summary> 
        /// Indicates that the tape was a result of a direct Tape() call.
        /// </summary> 
        public struct _istape { }
        
        /// <summary> 
        /// Outputs audio in an audio file format and plays it if needed. <br /> 
        /// A single <see cref="Outlet"> Outlet </see> will result in Mono audio. <br /> 
        /// Use a func returning an <see cref="Outlet"> Outlet </see> e.g. <c> Save(() => MySound()); </c> <br /> 
        /// For Stereo it must return a new outlet each time. <br /> 
        /// <strong> So call your <see cref="Outlet"> Outlet </see> -creation method in the Func! </strong> <br /> 
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
        /// <param name="channelSignals"> 
        /// A list of outlets, one for each channel,
        /// e.g. a single one for Mono and 2 outlets for stereo.
        /// </param> 
        /// <param name="duration"> 
        /// The duration of the audio in seconds.
        /// Nullable. Falls back to AudioLength or else to a 1-second time span.
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
        /// Can be null. <br/> 
        /// Inserts additional text in the result messages
        /// in an appropriate spot for overview.
        /// </param> 
        /// <returns> 
        /// A <see cref="Buff"> Buff </see> object with the <see cref="AudioFileOutput"/> entity in it,
        /// and resultant data, like the file path or the bytes array.
        /// </returns> 
        public struct _makebuff { }

        /// <summary> 
        /// Returns the current method name or current property name.
        /// </summary> 
        public struct _membername { }

        /// <summary> 
        /// Multiplies two <see cref="Outlet"/> operands, optimizing for constant values if possible.
        /// </summary> 
        public struct _multiply { }

        /// <summary> 
        /// <c> SetName() </c> is applied to the object before it. <br/> 
        /// You can pass the desired name as a parameter, but if you omit it, the name of the current member
        /// you are in is used. So the place where you code, that method's name, becomes
        /// the name for the object you are calling upon.
        /// Some methods work without SetName,
        /// in that they'll automatically pick up the name of the member called from.
        /// This only does not work with params / variable amount of arguments
        /// where the parameters separate with commas describe a list of items.
        /// Calling SetName explicitly there helps.
        /// The names are mostly optional, but they can be useful as a diagnostic tool.
        /// </summary> 
        public struct _names { }

        /// <inheritdoc
        ///    cref="docs._default" /> 
        public struct _noteindexer { }
        
        /// <summary> 
        /// Manages note length configuration and fallback logic in audio synthesis. <br/>
        /// Provides methods to set, reset, retrieve, and snapshot note lengths using defaults from <see cref="SynthWishes">SynthWishes</see>.
        /// 
        /// <para> <strong> Fallback Logic </strong> <br/> 
        /// If <paramref name="noteLength"/> is null, the system uses these fallbacks: <br/> 
        /// 1. Explicit note length from <see cref="SynthWishes.WithNoteLength">WithNoteLength</see>. <br/> 
        /// 2. The <c> BeatLength </c> if set. <br/> 
        /// 3. The <c> noteLength </c> from the config file. <br/> 
        /// 4. Default of <c> 0.5 </c> seconds. </para> 
        ///
        /// <para> <strong> Members </strong> <br/> 
        /// - <see cref="SynthWishes.GetNoteLength"> GetNoteLength </see> :
        ///   Resolves note length using the fallback logic. <br/> 
        /// - <see cref="SynthWishes.WithNoteLength"> WithNoteLength </see> :
        ///   Sets note length as a <see cref="FlowNode"> FlowNode </see> or <c> double</c>. <br/> 
        /// - <see cref="SynthWishes.ResetNoteLength"> ResetNoteLength </see> :
        ///   Clears the note length, reverting to fallbacks. <br/> 
        /// - <see cref="SynthWishes.GetNoteLengthSnapShot"> GetNoteLengthSnapShot </see>
        ///   resolves dynamic note lengths for specific <br/>
        ///   <paramref name="time"/> and <paramref name="channel"/> values.
        ///   Ensures fixed lengths for buffer sizing and curve stretching. </para> 
        /// </summary> 
        /// <param name="noteLength"> 
        /// Optional <see cref="FlowNode"> FlowNode</see>. Defaults are used if null.
        /// </param> 
        /// <param name="time"> 
        /// Time for resolving a dynamic note length.
        /// </param> 
        /// <param name="channel"> 
        /// Audio channel index for channel-specific note lengths.
        /// </param> 
        /// <returns> 
        /// A <see cref="FlowNode"> FlowNode </see> with the resolved note length.
        /// </returns> 
        public struct _notelength { }
        
        /// <summary> 
        /// Allow specifying 1 value: make it the start and end node values.
        /// </summary> 
        public struct _onebecomestwo { }

        /// <summary> 
        /// Returns what's input into an operand of the operator.
        /// </summary> 
        public struct _operand { }

        /// <summary>
        /// Returns the operator name, except when it is the same as the operator type name.
        /// Then it returns a default string.
        /// </summary>
        public struct _operatorgetname { }
        
        /// <summary>
        /// Audio padding is only relevant in case of Save and Play actions. <br/>
        /// Padding before and after the audio can be set separately with the LeadingSilence and TrailingSilence settings. <br/>
        /// The Padding settings is shorthand for setting both LeadingSilence and TrailingSilence at the same time. <br/>
        /// Padding setting returns null of LeadingSilence and TrailingSilence are not the same.
        /// </summary> 
        public struct _padding { }
        
        /// <summary> 
        /// Applies a panbrello effect to a stereo signal by modulating the panning
        /// with a sine wave based on the specified speed and depth. <br/> 
        /// Default speed is 3 times a second. <br/> 
        /// Default depth is 1 = full volume going up and down.
        /// </summary> 
        /// <inheritdoc cref="_default" /> 
        public struct _panbrello { }

        /// <summary> 
        /// Applies panning to a stereo signal by adjusting
        /// the left and right channel volumes based on the specified panning value.
        /// NOTE: For mono signals, panning would cut the volume in half, ensuring consistency with center-panned stereo signals.
        /// TODO: A variable panning might go into the negative. Should be clamped to 0-1.
        /// </summary> 
        /// <param name="channel"> 
        /// The channel for which to calculate the panning.
        /// If omitted, the <see cref="SynthWishes.GetChannel"/> property is used,
        /// which indicates the current channel.
        /// </param> 
        /// <inheritdoc cref="_default" /> 
        public struct _panning { }

        /// <summary> 
        /// Performs an addition in parallel to improve performance. <br/> 
        /// It uses a trick to make use of existing functions. <br/> 
        /// It saves each term to an audio file. <br/> 
        /// Then it reloads each using a
        /// <see cref="Sample"> Sample </see> <see cref="Operator"> Operator</see>. <br/> 
        /// Then <see cref="ParallelAdd"> ParallelAdd </see> returns
        /// a normal non-parallel <see cref="Add"> Add </see> operator,
        /// that will add up all the <see cref="Sample"> Samples</see>. <br/> 
        /// Please set the volume to something that doesn't make the partials go over the max. <br/> 
        /// You also need to call the fluent <see cref="SynthWishes.WithAudioLength"> WithAudioLength </see> 
        /// or <see cref="SynthWishes.AddAudioLength"> AddAudioLength </see> methods to set the buffer size.
        /// </summary> 
        /// <param name="funcs"> Lambdas <c> () =>... </c> each returning a term for the addition. </param> 
        /// <returns> 
        /// A normal <see cref="Add"> Add </see> <see cref="Operator"> Operator's </see> 
        /// <see cref="Outlet"> Outlet </see> (as <see cref="FlowNode"> FlowNode </see> ).
        /// </returns> 
        public struct _paralleladd { }

        /// <inheritdoc 
        ///    cref="_tapesanddiskcache" /> 
        public struct _parallelprocessing { }

        /// <summary> 
        /// The parallel processing, processes a tree of tasks, "leaf" tasks first.
        /// But as soon as they get processed, more "leaf" tasks emerge up for processing.
        /// There's a time interval between checking for new leaves to process.
        /// <c> LeafCheckTimeOut </c> defines this (short) wait time.
        /// Its is in seconds, but can use millisecond precision.
        /// Currently, when a leaf is processed, it signals another leaf check.
        /// The LeafCheckTimeOut can make that check happen sooner.
        /// It can also be used as a safeguard against system hang-ups
        /// if there is a problem with the processing.
        /// A value of -1 disables any time-out solely relying on the
        /// end of a leaf's processing signaling the check.
        /// </summary> 
        public struct _leafchecktimeout { }

        /// <summary> 
        /// Can get persistence configuration from config, or otherwise falls back
        /// to default in-memory persistence.
        /// </summary> 
        public struct _persistencehelper { }

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
        public struct _pitchpan { }

        /// <inheritdoc
        ///    cref="_tapesanddiskcache" /> 
        public struct _playalltapes { }

        /// <summary> 
        /// Extensions that are wishes for the back-end related that retrieve related objects like the Operator, Curve or Sample entities.
        /// </summary> 
        public struct _underlyingextensions { }

        /// <summary> 
        /// Creates a Sample by reading the file at the given <paramref name="filePath" /> or Stream or Byte array.
        /// </summary> 
        /// <param name="filePath"> The file path of the audio sample to load. </param> 
        /// <returns> <see cref="SampleOperatorWrapper" />  that can be used as an <see cref="Outlet" /> too. </returns> 
        public struct _sample { }

        /// <summary>
        /// Adjusts sampling rate to match the new audio length.
        /// </summary>
        public struct _sampleaudiolength { }
            
        /// <summary> 
        /// Back-end will need bytes wrapped in a Stream and will read it back into a byte[] again.
        /// This code would prevent that, but won't kick off the wav header parsing,
        /// which is important as a test.
        /// The WavWishes to solve both are currently lacking.
        /// Revisit later.
        /// </summary> 
        public struct _samplefromfluentconfig { }

        /// <summary> 
        /// With optional Context.
        /// </summary> 
        public struct _setenumwishes { }

        /// <summary> 
        /// Sets the node type (the type of interpolation) for a curve as a whole.
        /// This sets the node type of all the curve nodes at once.
        /// </summary> 
        public struct _setnodetype { }

        /// <summary> 
        /// Sets the speaker setup for the specified <see cref="AudioFileOutput"/>.
        /// Executes some side effects to initialize the <see cref="AudioFileOutput"/> channels.
        /// </summary> 
        /// <summary> 
        /// </summary> 
        /// <param name="audioFileOutput"> The <see cref="AudioFileOutput"/> to update. </param> 
        /// <param name="speakerSetup"> The <see cref="SpeakerSetup"/> to apply to the <paramref name="audioFileOutput"/>. </param> 
        /// <param name="context"> 
        /// Optional. The context for persistence operations.
        /// If one isn't provided, a brand new one will be created,
        /// which depending on the situation, could cause problems or not.
        /// </param> 
        public struct _setspeakersetup_withsideeffects { }

        /// <summary> 
        /// Generates a sine wave signal with the specified pitch. <br /> 
        /// Simpler variation on the one in the original OperatorFactory
        /// with pitch as the first and only parameter.
        /// </summary> 
        /// <param name="pitch"> The frequency in Hz of the sine wave. Defaults to 1 Hz. </param> 
        /// <returns> An <see cref="Outlet" /> representing the sine wave signal. </returns> 
        /// <inheritdoc cref="_default" /> 
        public struct _sine { }

        /// <inheritdoc
        ///    cref="_default" /> 
        public struct _note { }

        /// <summary>
        /// Unsettling setters for the unsettable.<br/><br/>
        /// 
        /// E.g., <c> sampleDataType.With32Bit() </c> returns a different entity than went in. <br/>
        /// The method left the input object completely untouched, because it is immutable. <br/> <br/>
        /// 
        /// <strong> Programming Technicalities </strong> <br/> <br/>
        ///
        /// These quasi-setters are a strange kind of value setters. <br/>
        /// They are executed upon objects they don't even use. <br/>
        /// The <c> this </c> object before the dot <c> . </c> is only used to discriminate between object types. <br/>
        /// So when you have <c> sampleDataTypeEnum.With32Bit() </c> it returns an <c> enum </c>. <br/>
        /// Another example: <c> With8Bit&lt;float&gt;() </c> returns <c> typeof(byte) </c> even though the call mentioned <c> float </c> not <c>byte</c>. <br/>
        /// What happens is that it returns the 8-bit variation of a data type compatible to what a float data type is to 32-bit audio. <br/>
        /// These kinds of quasi-setters are used for immutable types and structs. Instead of setting a property of the target object, <br/>
        /// they return a different object, that has that different property. It makes these immutable types have setters with fluent syntax, <br/>
        /// just like types that are mutable, allowing for consistent fluent syntax throughout.
        /// </summary>
        public struct _quasisetter { }
        
        /// <summary> 
        /// Turns an <see cref="Operator"/> graph into a string,
        /// in a way that the complexity becomes apparent.
        /// Having each <see cref="Operator"/> be on its own line,
        /// the complexity can be expressed as the number of lines.
        /// Optionally, it can be output in a single line.
        /// </summary> 
        public struct _stringify { }

        /// <summary> 
        /// Uses the channel specified by the <see cref="SynthWishes.GetChannel"/> property.
        /// Or you can call e.g. <c> Outlet.Calculate(time, ChannelEnum.Right) </c> 
        /// </summary> 
        public struct _synthwishescalculate { }

        /// <summary>
        /// A deferred Tape action, like Play or Save that can go off later.
        /// (Object is non-nullable and non-exchangeable.)
        /// </summary>
        public struct _tapeaction { }

        /// <summary>
        /// Determines if the action is effectively active. This isn't just whether it's On.
        /// Checks if the action is On and not Done yet. <br/>
        /// Also checks if it's an Intercept that has its callback filled in. <br/>
        /// And checks if it's a Channel action and actually a Channel tape. <br/>
        /// Or the other way around: if it's a Channel but the action is not for channels. <br/>
        /// (It's a little much, but flags are abundant, e.g.,
        /// non-Channel flags can be registered on Channel tapes for later use with the combined stereo tape.)
        /// </summary>
        public struct _tapeactionactive { }
            
        /// <summary>
        /// You can assign it, but it only returns it when the action is On and not Done.
        /// </summary>
        public struct _tapeactionfilepathsuggested { }
        
        /// <summary> 
        /// Not so much used for taping, as much as when reusing a tape as a Sample.
        /// </summary>
        public struct _tapeinterpolation { }
        
        /// <summary> 
        /// Returns the <c> Tape </c> 's <c> Signal.Name </c> , <c> FallBackName </c> 
        /// or else <c> FilePath </c> in prettified form.
        /// </summary> 
        public struct _tapename { }
       
        /// <summary> 
        /// Adds padding to Play and Save tapes,
        /// without affecting the original tape.
        /// </summary>
        /// <returns>Returns only new Tapes that were padded.</returns>
        public struct _tapepadder { }

        /// <summary> 
        /// When PlayAllTapes is set, Tape and ParallelAdd play the sounds generated in the parallel loop or
        /// other parallel tasks for testing purposes. <br/> 
        /// DiskCache controls whether the tapes will be cached to disk instead of memory,
        /// in case of which it also doesn't clean up the files. Mostly for testing purposes.
        /// But could be used in low-memory high-disk-space scenarios. <br/> 
        /// With the Parallels setting you can turn off parallel processing completely.
        /// </summary> 
        public struct _tapesanddiskcache { }

        /// <summary> 
        /// This TimeIndexer provides shorthand for specifying bar and beat in a musical sense.
        /// Access by bar and beat to get time-based value.
        /// Example usage: t[bar: 2, beat: 1.5] will return the number of seconds.
        /// The numbers are 1-based, so the first bar is bar 1, the first beat is beat 1.
        /// </summary> 
        public struct _timeindexer { }

        /// <summary> 
        /// Apply _tremolo by modulating amplitude over time using an oscillator.
        /// </summary> 
        /// <inheritdoc cref="_default" /> 
        public struct _tremolo { }

        /// <summary> 
        /// Prep Data: Split into unique lines and determine the window where there are characters.
        /// White space is trimmed off of the top, bottom, left and right,
        /// leaving only the block of characters that contains data.
        /// </summary> 
        public struct _trimasciicurves { }

        /// <summary> 
        /// Gets the node type (the type of interpolation) for a curve as a whole.
        /// This only works if all (but the last) node are set to the same node type.
        /// Otherwise, NodeTypeEnum.Undefined is returned.
        /// </summary> 
        public struct _trygetnodetype { }

        /// <summary> 
        /// If this is a curve operator, this will return the underlying Curve entity,
        /// that contains specifics about the nodes and how they are connected.
        /// If it's called on something that isn't a Curve, an exception will be thrown.
        /// </summary> 
        public struct _underlyingcurve { }

        /// <summary> 
        /// If this is a sample operator, this will return the underlying Sample entity,
        /// with configuration, byte array, etc. If it's not a sample operator,
        /// it will throw an exception.
        /// </summary> 
        public struct _underlyingsample { }

        /// <summary> 
        /// Applies vibrato modulation to a given frequency by modulating it with a sine wave. <br /> 
        /// NOTE: Due to the lack of phase tracking, the vibrato depth tends to accumulate over time.
        /// </summary> 
        /// <param name="freq"> The base frequency to which vibrato will be applied. </param> 
        /// <returns> An <see cref="Outlet" /> object representing the frequency modulated with vibrato. </returns> 
        /// <inheritdoc cref="_default" /> 
        public struct _vibrato { }

        /// <summary> 
        /// Determines the configured sampling rate.
        /// This can be set in the <c>.config </c> file,
        /// and can be different depending on the environment. <br/> 
        /// For instance <strong> NCrunch </strong> (a code coverage tool)
        /// and <strong> Azure Pipelines </strong> can have
        /// an alternative (lower) sampling rate configured,
        /// for those tools to perform better. <br/> 
        /// <br/> 
        /// The sampling rate can be overridden in C# using the
        /// <c>.WithSamplingRate() </c> fluent configuration method.
        /// This is usually for testing purposes.
        /// Prefer using the <c>.config </c> file instead. <br/> 
        /// </summary> 
        public struct _withsamplingrate { }
    }
}