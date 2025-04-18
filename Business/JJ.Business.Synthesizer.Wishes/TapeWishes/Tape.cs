﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Business.Synthesizer.Wishes.Logging;
using JJ.Business.Synthesizer.Wishes.docs;
using static System.IO.File;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.DebuggerDisplayFormatter;
using static JJ.Framework.Existence.Core.FilledInHelper;
using static JJ.Business.Synthesizer.Wishes.NameWishes;
// ReSharper disable ArrangeAccessorOwnerBody

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    public class Tape
    {
        public override string ToString() => GetDebuggerDisplay(this);

        public Tape(SynthWishes synthWishes) : this()
        {
            SynthWishes = synthWishes;
        }

        public Tape()
        {
            Config = new TapeConfig(this);
            Actions = new TapeActions(this);
            Buff = new Buff { Tape = this };
        }
        
        private SynthWishes _synthWishes;
        
        /// <summary> Must be filled in. In edge-cases it can throw an error if not initialized. </summary>
        internal SynthWishes SynthWishes
        {
            get => _synthWishes         ?? throw new NullException(() => SynthWishes);
            set => _synthWishes = value ?? throw new NullException(() => SynthWishes);
        }
        
        
        #region Buff
        
        [Obsolete("Prefer Tape properties instead")]
        public Buff Buff { get; }
        
        public bool IsBuff => Has(Bytes) || Exists(FilePathResolved);
        
        public byte[] Bytes 
        { 
            get => Buff.Bytes; 
            set => Buff.Bytes = value; 
        }
        
        /// <summary> Tape.FilePathResolved = Buff.FilePath </summary>
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

        /// <inheritdoc cref="_tapename" />
        public string GetName(string name = null, [CallerMemberName] string callerMemberName = null)
            => ResolveName(name, Outlets, FallbackName, FilePathSuggested, callerMemberName);
        
        public string GetFilePath(string filePath = null, [CallerMemberName] string callerMemberName = null)
            => ResolveFilePath(
                Config.FileExtension(), Config.AudioFormat(), 
                filePath, FilePathResolved, 
                Actions.SaveChannels.FilePathSuggested, Actions.Save.FilePathSuggested, 
                FilePathSuggested, Actions.DiskCache.FilePathSuggested,
                ResolveName(IDs, Outlets, FallbackName, callerMemberName));

        public string FallbackName { get; set; }
        public string FilePathSuggested { get; set; }
        
        public string Descriptor => this.Descriptor();
        
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
            => Outlets = signals?.Select(x => x.UnderlyingOutlet).ToList();
        
        internal void ClearSignals() => Outlets = default;
        
        #endregion

        #region Durations
        
        private double _duration;
        public double Duration
        {
            get => _duration;
            set => _duration = AssertAudioLength(value);
        }
      
        /// <inheritdoc cref="_padding"/>
        private double _leadingSilence;
        /// <inheritdoc cref="_padding"/>
        public double LeadingSilence
        {
            get => _leadingSilence;
            set => _leadingSilence = AssertAudioLength(value);
        }
        
        /// <inheritdoc cref="_padding"/>
        private double _trailingSilence;
        /// <inheritdoc cref="_padding"/>
        public double TrailingSilence
        {
            get => _trailingSilence;
            set => _trailingSilence = AssertAudioLength(value);
        }
        
        #endregion
        
        #region Config
        
        public TapeConfig Config { get; }

        public bool IsChannel => Config.IsChannel();
        /// <summary> Shorthand for Config.Channel.Value </summary>
        public int i => Config.Channel.Value;

        #endregion

        #region Actions
        
        public TapeActions Actions { get; }
        public bool IsPadded { get; internal set; }
        /// <inheritdoc cref="_istape" />
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
        
    public class TapeConfig
    {
        public override string ToString() => GetDebuggerDisplay(this);
        
        public SynthWishes SynthWishes => Tape.SynthWishes;
        
        /// <summary> Always filled in. </summary>
        public Tape Tape { get; }

        internal TapeConfig(Tape tape)
        {
            Tape = tape ?? throw new ArgumentNullException(nameof(tape));
        }
        
        private int _samplingRate;
        public int SamplingRate
        {
            get => AssertSamplingRate(_samplingRate);
            set => _samplingRate = AssertSamplingRate(value);
        }
        
        private int _bits;
        public int Bits
        {
            get => AssertBits(_bits);
            set => _bits = AssertBits(value);
        }

        private int _channels;
        public int Channels 
        {
            get => AssertChannels(_channels); 
            set => _channels = AssertChannels(value); 
        }
        
        private int? _channel;
        public int? Channel
        {
            get => AssertChannel(_channel);
            set => _channel = AssertChannel(value);
        }
        
        private AudioFileFormatEnum _audioFormat;
        public AudioFileFormatEnum AudioFormat
        {
            get => AssertAudioFormat(_audioFormat);
            set => _audioFormat = AssertAudioFormat(value);
        }
        
        /// <inheritdoc cref="_tapeinterpolation" />
        private InterpolationTypeEnum _interpolation;
        /// <inheritdoc cref="_tapeinterpolation" />
        public InterpolationTypeEnum Interpolation
        {
            get => AssertInterpolation(_interpolation);
            set => _interpolation = AssertInterpolation(value);
        }
        
        private int _courtesyFrames;
        public int CourtesyFrames
        {
            get => AssertCourtesyFrames(_courtesyFrames);
            set => _courtesyFrames = AssertCourtesyFrames(value);
        }
        
        // Helpers
        
        public bool IsMono => Channels == 1;
        public bool IsStereo => Channels == 2;
        public bool IsChannel() => Channel != null;
        public bool IsChannel(int? channel) => Channel != null && Channel == channel;
        public bool IsLeft         => IsChannel(0)    && IsStereo;
        public bool IsRight        => IsChannel(1)    && IsStereo;
        public bool IsCenter       => IsChannel(0)    && IsMono;
        public bool IsNoChannel    => Channel == null && IsStereo;
        public bool IsAnyChannel   => IsNoChannel;
        public bool IsEveryChannel => IsNoChannel;
    }
        
    public enum ActionEnum
    {
        Undefined,
        Tape,
        Pad,
        Play,
        Save,
        BeforeRecord,
        AfterRecord,
        PlayChannels,
        SaveChannels,
        BeforeRecordChannel,
        AfterRecordChannel,
        PlayAllTapes,
        DiskCache
    }

    /// <inheritdoc cref="_tapeaction" />
    public class TapeAction
    {
        public override string ToString() => GetDebuggerDisplay(this);
        
        internal TapeAction(Tape tape, ActionEnum actionType)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            if (!Has(actionType)) throw new Exception($"{nameof(actionType)} not provided.");
            Tape = tape;
            Type = actionType;
        }

        public SynthWishes SynthWishes => Tape.SynthWishes;

        /// <summary> Always filled in. </summary>
        public Tape Tape { get; }
        /// <summary> Always filled in. </summary>
        public ActionEnum Type { get; }
        public bool On { get; set; }
        public bool Done { get; set; }

        /// <inheritdoc cref="_tapeactionactive" />"/>
        public bool Active
        {
            get
            {
                if (!On) return false;
                if (Done) return false;
                
                if (IsIntercept && Callback == null) 
                {
                    throw new Exception("Intercept action is missing its callback.");
                }
                
                if (Type == ActionEnum.DiskCache)
                {
                    return true;
                }
                
                if (Tape.Config.IsStereo) 
                {
                    return IsForChannel == IsChannel;
                }
                
                return true;
            }
        }
        
        /// <summary> Not always there </summary>
        internal Action<Tape> Callback { get; set; }

        private string _filePathSuggested;

        /// <inheritdoc cref="_tapeactionfilepathsuggested"/>
        public string FilePathSuggested
        { 
            get => Active ? _filePathSuggested : default;
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
        
        public bool IsChannel => Tape.Config.IsChannel();
        
        public bool IsForChannel 
            => Type == ActionEnum.PlayChannels || 
               Type == ActionEnum.SaveChannels || 
               Type == ActionEnum.BeforeRecordChannel || 
               Type == ActionEnum.AfterRecordChannel;
        
        public bool IsIntercept
            => Type == ActionEnum.BeforeRecord ||
               Type == ActionEnum.AfterRecord ||
               Type == ActionEnum.BeforeRecordChannel ||
               Type == ActionEnum.AfterRecordChannel;
    }

    /// <inheritdoc cref="_tapeaction" />
    public class TapeActions
    {
        public override string ToString() => GetDebuggerDisplay(this);

        internal TapeActions(Tape tape)
        {
            Tape = tape ?? throw new ArgumentNullException(nameof(tape));
            Play = new TapeAction(tape, ActionEnum.Play);
            Save = new TapeAction(tape, ActionEnum.Save);
            BeforeRecord = new TapeAction(tape, ActionEnum.BeforeRecord);
            AfterRecord = new TapeAction(tape, ActionEnum.AfterRecord);
            PlayChannels = new TapeAction(tape, ActionEnum.PlayChannels);
            SaveChannels = new TapeAction(tape, ActionEnum.SaveChannels);
            BeforeRecordChannel = new TapeAction(tape, ActionEnum.BeforeRecordChannel);
            AfterRecordChannel = new TapeAction(tape, ActionEnum.AfterRecordChannel);
            PlayAllTapes = new TapeAction(tape, ActionEnum.PlayAllTapes);
            DiskCache = new TapeAction(tape, ActionEnum.DiskCache);
        }
        
        public SynthWishes SynthWishes => Tape.SynthWishes;
        
        /// <summary> Parent. Always filled in. </summary>
        public Tape Tape { get; }
        /// <inheritdoc cref="_tapeaction" />
        public TapeAction Play { get; }
        /// <inheritdoc cref="_tapeaction" />
        public TapeAction Save { get; }
        /// <inheritdoc cref="_tapeaction" />
        public TapeAction BeforeRecord { get; }
        /// <inheritdoc cref="_tapeaction" />
        public TapeAction AfterRecord { get; }
        /// <inheritdoc cref="_tapeaction" />
        public TapeAction PlayChannels { get; }
        /// <inheritdoc cref="_tapeaction" />
        public TapeAction SaveChannels { get; }
        /// <inheritdoc cref="_tapeaction" />
        public TapeAction BeforeRecordChannel { get; }
        /// <inheritdoc cref="_tapeaction" />
        public TapeAction AfterRecordChannel { get; }
        /// <inheritdoc cref="_tapeaction" />
        public TapeAction PlayAllTapes { get; }
        /// <inheritdoc cref="_tapeaction" />
        public TapeAction DiskCache { get; }
        
        public void ClearChannelActions()
        {
            SaveChannels.Clear();
            PlayChannels.Clear();
            BeforeRecordChannel.Clear();
            AfterRecordChannel.Clear();
        }

        public TapeAction Get(ActionEnum type)
        {
            TapeAction action = TryGet(type);
            if (action == null)
            {
                throw new Exception($"{nameof(ActionEnum)} {type} does not have an associated {nameof(TapeAction)} object.");
            }
            return action;
        }

        public TapeAction TryGet(ActionEnum type)
        {
            switch (type)
            {
                case ActionEnum.Play: return Play;
                case ActionEnum.Save: return Save;
                case ActionEnum.BeforeRecord: return BeforeRecord;
                case ActionEnum.AfterRecord: return AfterRecord;
                case ActionEnum.PlayChannels: return PlayChannels;
                case ActionEnum.SaveChannels: return SaveChannels;
                case ActionEnum.BeforeRecordChannel: return BeforeRecordChannel;
                case ActionEnum.AfterRecordChannel: return AfterRecordChannel;
                case ActionEnum.PlayAllTapes: return PlayAllTapes;
                case ActionEnum.DiskCache: return DiskCache;
                default: return null;
            }
        }
    }
}