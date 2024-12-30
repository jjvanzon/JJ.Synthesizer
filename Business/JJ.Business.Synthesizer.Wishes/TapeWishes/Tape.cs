using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Persistence.Synthesizer;
using static System.IO.File;
using static JJ.Business.Synthesizer.Wishes.Helpers.DebuggerDisplayFormatter;
using static JJ.Business.Synthesizer.Wishes.JJ_Framework_Common_Wishes.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.NameWishes;
// ReSharper disable ArrangeAccessorOwnerBody

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class Tape
    {
        string DebuggerDisplay => GetDebuggerDisplay(this);
        
        public Tape()
        {
            Actions = new TapeActions(this);
        }
        
        internal SynthWishes SynthWishes { get; set; }

        #region Buff
        [Obsolete("Prefer Tape properties instead")]
        public Buff Buff { get; } = new Buff();
        
        public bool IsBuff => Has(Bytes) || Exists(FilePathResolved);
        
        public byte[] Bytes 
        { 
            get => Buff.Bytes; 
            set => Buff.Bytes = value; 
        }
        
        /// <summary>
        /// Tape.FilePathResolved = Buff.FilePath
        /// </summary>
        public string FilePathResolved
        {
            get => Buff.FilePath;
            set => Buff.FilePath = value;
        }
        
        public AudioFileOutput UnderlyingAudioFileOutput
        { 
            get => Buff.UnderlyingAudioFileOutput;
            internal set => Buff.UnderlyingAudioFileOutput = value; 
        }
        
        public FlowNode Sample { get; set; }
        
        public Sample UnderlyingSample
        {
            get { return Sample?.UnderlyingSample(); }
            set => Sample.UnderlyingSample(value);
        }
        
        public void ClearBuff()
        {
            Bytes = default;
            FilePathResolved = default;
            UnderlyingAudioFileOutput = default;
            Sample = default;
        }
        
        #endregion

        #region Identity
        public IList<int> IDs => Outlets.Select(x => x?.Operator?.ID)
                                        .Where(FilledIn)
                                        .Select(x => x.Value).ToArray();

        /// <inheritdoc cref="docs._tapename" />
        public string GetName(string name = null, [CallerMemberName] string callerMemberName = null)
            => ResolveName(name, Outlets, FallBackName, FilePathSuggested, callerMemberName);
        
        public string GetFilePath(string filePath = null, [CallerMemberName] string callerMemberName = null)
            => ResolveFilePath(
                Config.FileExtension(), Config.AudioFormat(), 
                filePath, FilePathResolved, 
                Actions.SaveChannels.FilePathSuggested, Actions.Save.FilePathSuggested, FilePathSuggested,
                ResolveName(IDs, Outlets, FallBackName, callerMemberName));

        public string FallBackName { get; set; }
        public string FilePathSuggested { get; set; }
        #endregion

        #region Signals
        internal Outlet Outlet 
        {
            get
            {
                if (Outlets.Count == 1) return Outlets[0];
                return null;
            }
            set
            {
                if (value == null) Outlets = default;
                Outlets = new [] { value };
            }
        }
        
        /// <summary> Not null. Auto(re)created. </summary>
        private IList<Outlet> _outlets = new List<Outlet>();
        
        /// <summary> Not null. Auto(re)created. </summary>
        internal IList<Outlet> Outlets
        { 
            get => _outlets;
            set => _outlets = value ?? new List<Outlet>();
        }
        
        internal IList<FlowNode> GetSignals()
        {
            if (SynthWishes == null)
            {
                throw new Exception("SynthWishes is null, but it is required to convert back-end Outlets into API FlowNodes." +
                                    "It is usually filled in, but may be missing in certain legacy scenarios." +
                                    "Please Set the SynthWishes property or use the Outlets property instead.");
            }

            return _outlets.Select(x => SynthWishes[x]).ToList();
        }
        
        internal void SetSignals(IList<FlowNode> signals)
        {
            Outlets = signals?.Select(x => x.UnderlyingOutlet).ToList();
        }
        
        internal void ClearSignals() => Outlets = default;
        #endregion

        #region Durations
        public double Duration { get; set; }
        /// <inheritdoc cref="docs._padding"/>
        public double LeadingSilence { get; set; }
        /// <inheritdoc cref="docs._padding"/>
        public double TrailingSilence { get; set; }
        #endregion
        
        #region Config
        public TapeConfig Config { get; } = new TapeConfig();
        
        /// <summary> Shorthand for Config.Channel.Value </summary>
        public int i => Config.Channel.Value;

        #endregion

        #region Actions
        public TapeActions Actions { get; }
        public bool IsPadded { get; internal set; }
        /// <inheritdoc cref="docs._istape" />
        public bool IsTape { get; internal set; }
                
        public void ClearChannelActions() => Actions.ClearChannelActions();

        #endregion

        #region Hierarchy
        internal HashSet<Tape> ParentTapes { get; } = new HashSet<Tape>();
        internal HashSet<Tape> ChildTapes { get; } = new HashSet<Tape>();
        internal int NestingLevel { get; set; }
        
        internal bool IsRoot => ParentTapes.Count == 0;
        
        internal void ClearHierarchy()
        {
            foreach (var parent in ParentTapes.ToArray())
            {
                ParentTapes.Remove(parent);
                parent.ChildTapes.Remove(this);
            }

            foreach (var child in ChildTapes.ToArray())
            {
                ChildTapes.Remove(child);
                child.ParentTapes.Remove(this);
            }
        }
        #endregion
    }
        
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class TapeConfig
    {
        string DebuggerDisplay => GetDebuggerDisplay(this);
        
        internal TapeConfig() { }
        
        public int SamplingRate { get; set; }
        public int Bits { get; set; }
        public int? Channels { get; set; }
        public int? Channel { get; set; }
        public bool IsMono => Channels == 1;
        public bool IsStereo => Channels == 2;
        public bool IsLeft => Channel != null && Channel == 0;
        public bool IsRight => Channel != null && Channel == 1;
        public bool IsCenter => Channel == null;
        
        public AudioFileFormatEnum AudioFormat { get; set; }
        /// <summary> Not so much used for taping, as much as when reusing a tape as a Sample. </summary>
        public InterpolationTypeEnum Interpolation { get; set; }
        public int CourtesyFrames { get; set; }
    }

    /// <inheritdoc cref="docs._tapeaction" />
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class TapeAction
    {
        string DebuggerDisplay => GetDebuggerDisplay(this);
        
        internal TapeAction(Tape tape, string name)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            if (!Has(name)) throw new Exception($"{nameof(name)} not provided.");
            Tape = tape;
            Name = name;
        }

        /// <summary> Always filled in. </summary>
        public Tape Tape { get; }
        /// <summary> Always filled in. </summary>
        public string Name { get; }
        public bool On { get; set; }
        public bool Done { get; set; }
        /// <summary> Not always there </summary>
        internal Action<Tape> Callback { get; set; }

        private string _filePathSuggested;

        /// <summary>
        /// You can assign it, but it only returns it when the action is On and not Done.
        /// </summary>
        public string FilePathSuggested
        { 
            get => On && !Done ? _filePathSuggested : default;
            set => _filePathSuggested = value; 
        }
        
        public string GetFilePath(string filePath, [CallerMemberName] string callerMemberName = null)
            => Tape.GetFilePath(Has(filePath) ? filePath : FilePathSuggested, callerMemberName);

        public void Clear()
        {
            On = default;
            Done = default;
            Callback = default;
            FilePathSuggested = default;
        }
    }

    /// <inheritdoc cref="docs._tapeaction" />
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class TapeActions
    {
        string DebuggerDisplay => GetDebuggerDisplay(this);

        internal TapeActions(Tape tape)
        {
            Tape = tape ?? throw new ArgumentNullException(nameof(tape));
            Play = new TapeAction(tape, nameof(Play));
            Save = new TapeAction(tape, nameof(Save));
            BeforeRecord = new TapeAction(tape, nameof(BeforeRecord));
            AfterRecord = new TapeAction(tape, nameof(AfterRecord));
            PlayChannels = new TapeAction(tape, nameof(PlayChannels));
            SaveChannels = new TapeAction(tape, nameof(SaveChannels));
            BeforeRecordChannel = new TapeAction(tape, nameof(BeforeRecordChannel));
            AfterRecordChannel = new TapeAction(tape, nameof(AfterRecordChannel));
            PlayAllTapes = new TapeAction(tape, nameof(PlayAllTapes));
            DiskCache = new TapeAction(tape, nameof(DiskCache));
        }
                
        /// <summary> Parent. Always filled in. </summary>
        public Tape Tape { get; }
        /// <inheritdoc cref="docs._tapeaction" />
        public TapeAction Play { get; }
        /// <inheritdoc cref="docs._tapeaction" />
        public TapeAction Save { get; }
        /// <inheritdoc cref="docs._tapeaction" />
        public TapeAction BeforeRecord { get; }
        /// <inheritdoc cref="docs._tapeaction" />
        public TapeAction AfterRecord { get; }
        /// <inheritdoc cref="docs._tapeaction" />
        public TapeAction PlayChannels { get; }
        /// <inheritdoc cref="docs._tapeaction" />
        public TapeAction SaveChannels { get; }
        /// <inheritdoc cref="docs._tapeaction" />
        public TapeAction BeforeRecordChannel { get; }
        /// <inheritdoc cref="docs._tapeaction" />
        public TapeAction AfterRecordChannel { get; }
        /// <inheritdoc cref="docs._tapeaction" />
        public TapeAction PlayAllTapes { get; }
        /// <inheritdoc cref="docs._tapeaction" />
        public TapeAction DiskCache { get; }
        
        public void ClearChannelActions()
        {
            SaveChannels.Clear();
            PlayChannels.Clear();
            BeforeRecordChannel.Clear();
            AfterRecordChannel.Clear();
        }
    }
}