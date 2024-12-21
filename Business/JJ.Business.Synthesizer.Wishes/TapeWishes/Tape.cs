using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using static JJ.Business.Synthesizer.Wishes.Helpers.DebuggerDisplayFormatter;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Common_Wishes.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.NameHelper;

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class Tape
    {
        private string DebuggerDisplay => GetDebuggerDisplay(this);

        // Names

        /// <inheritdoc cref="docs._tapename" />
        public string GetName => ResolveName(Signal, Signals, FallBackName, FilePathSuggested, callerMemberName: null);
        public string FallBackName { get; internal set; }
        public string FilePathResolved { get; internal set; }
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
        
        // Actions

        /// <inheritdoc cref="docs._istape" />
        public bool IsTape { get; internal set; }
        public bool IsPlay { get; internal set; }
        public bool IsPlayed { get; internal set; }
        public bool IsSave { get; internal set; }
        public bool IsSaved { get; internal set; }
        public bool IsIntercept { get; internal set; }
        public bool IsIntercepted { get; internal set; }
        public bool IsPlayChannel { get; internal set; }
        public bool ChannelIsPlayed { get; internal set; }
        public bool IsSaveChannel { get; internal set; }
        public bool ChannelIsSaved { get; internal set; }
        public bool IsInterceptChannel { get; internal set; }
        public bool ChannelIsIntercepted { get; internal set; }
        public bool IsPadded { get; internal set; }
        internal Action<Tape> Callback { get; set; }
        internal Action<Tape> ChannelCallback { get; set; }
        
        // Buff
        
        public Buff Buff { get; internal set; }
                        
        // Options

        public bool CacheToDisk { get; internal set; }
        public bool PlayAllTapes { get; internal set; }
        public int ExtraBufferFrames { get; internal set; }

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
}