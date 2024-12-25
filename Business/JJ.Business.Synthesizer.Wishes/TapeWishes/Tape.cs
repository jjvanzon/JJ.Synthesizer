using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Persistence.Synthesizer;
using static System.IO.File;
using static JJ.Business.Synthesizer.Wishes.Helpers.DebuggerDisplayFormatter;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Common_Wishes.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

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
        
        // Buff

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
        
        [Obsolete("Prefer Tape properties instead")]
        public Buff Buff { get; } = new Buff();

        // Names

        /// <inheritdoc cref="docs._tapename" />
        public string GetName(string name = null, [CallerMemberName] string callerMemberName = null)
            => ResolveName(name, Signal, Signals, FallBackName, FilePathSuggested, callerMemberName);
        
        public string GetFilePath(string filePath = null, [CallerMemberName] string callerMemberName = null)
            => ResolveFilePath(AudioFormat, filePath, FilePathResolved, FilePathSuggested, GetName(callerMemberName: callerMemberName));

        public string FallBackName { get; internal set; }
        public string FilePathSuggested { get; internal set; }
        
        // Signals

        internal FlowNode Signal { get; set; }
        /// <summary> For stereo tapes. </summary>
        internal IList<FlowNode> Signals { get; set; }
        internal IList<FlowNode> ConcatSignals()
        {
            var signals = new List<FlowNode>();
            if (Signal != null) signals.Add(Signal);
            if (Signals != null) signals.AddRange(Signals.Where(FilledIn));
            return signals;
        }

        // Durations
        
        public double Duration { get; internal set; }
        /// <inheritdoc cref="docs._padding"/>
        public double LeadingSilence { get; internal set; }
        /// <inheritdoc cref="docs._padding"/>
        public double TrailingSilence { get; internal set; }

        // Audio Properties
        
        public int SamplingRate { get; internal set; }
        public int Bits { get; internal set; }
        public int? Channel { get; internal set; }
        public int? Channels { get; internal set; }
        public bool IsMono => Channels == 1;
        public bool IsStereo => Channels == 2;
        public AudioFileFormatEnum AudioFormat { get; internal set; }
        /// <summary> Not so much used for taping, as much as when reusing a tape as a Sample. </summary>
        public InterpolationTypeEnum Interpolation { get; set; }

        // Actions

        public TapeActions Actions { get; }
        public bool IsPadded { get; internal set; }
        /// <inheritdoc cref="docs._istape" />
        public bool IsTape { get; internal set; }

        // Options

        public int CourtesyFrames { get; internal set; }

        // Hierarchy

        internal HashSet<Tape> ParentTapes { get; } = new HashSet<Tape>();
        internal HashSet<Tape> ChildTapes { get; } = new HashSet<Tape>();
        internal int NestingLevel { get; set; }
        
        internal void ClearRelationships()
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
            PlayChannel = new TapeAction(tape, nameof(PlayChannel));
            SaveChannel = new TapeAction(tape, nameof(SaveChannel));
            BeforeRecordChannel = new TapeAction(tape, nameof(BeforeRecordChannel));
            AfterRecordChannel = new TapeAction(tape, nameof(AfterRecordChannel));
            PlayAllTapes = new TapeAction(tape, nameof(PlayAllTapes));
            DiskCache = new TapeAction(tape, nameof(DiskCache));
        }
                
        /// <summary> Always filled in. </summary>
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
        public TapeAction PlayChannel { get; }
        
        /// <inheritdoc cref="docs._tapeaction" />
        public TapeAction SaveChannel { get; }
        
        /// <inheritdoc cref="docs._tapeaction" />
        public TapeAction BeforeRecordChannel { get; }
        
        /// <inheritdoc cref="docs._tapeaction" />
        public TapeAction AfterRecordChannel { get; }
        
        /// <inheritdoc cref="docs._tapeaction" />
        public TapeAction PlayAllTapes { get; }
        
        /// <inheritdoc cref="docs._tapeaction" />
        public TapeAction DiskCache { get; }
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
        public bool Done { get; internal set; }
        /// <summary> Not always there </summary>
        internal Action<Tape> Callback { get; set; }
    }
}