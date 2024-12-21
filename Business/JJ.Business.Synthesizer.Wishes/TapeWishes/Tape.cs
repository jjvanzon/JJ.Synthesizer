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
        internal string GetName => ResolveName(Signal, Signals, FallBackName, FilePathSuggested, callerMemberName: null);
        internal string FallBackName { get; set; }
        internal string FilePathResolved { get; set; }
        internal string FilePathSuggested { get; set; }
        
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
        
        internal double Duration { get; set; }
        /// <inheritdoc cref="docs._padding"/>
        internal double LeadingSilence { get; set; }
        /// <inheritdoc cref="docs._padding"/>
        internal double TrailingSilence { get; set; }

        // Audio Properties
        
        internal int SamplingRate { get; set; }
        internal int Bits { get; set; }
        public int? Channel { get; internal set; }
        internal int? Channels { get; set; }
        internal bool IsMono => Channels == 1;
        internal bool IsStereo => Channels == 2;
        internal AudioFileFormatEnum AudioFormat { get; set; }
        
        // Actions

        /// <inheritdoc cref="docs._istape" />
        internal bool IsTape { get; set; }
        internal bool IsPlay { get; set; }
        internal bool IsPlayed { get; set; }
        internal bool IsSave { get; set; }
        internal bool IsSaved { get; set; }
        internal bool IsIntercept { get; set; }
        internal bool IsIntercepted { get; set; }
        internal bool IsPlayChannel { get; set; }
        internal bool ChannelIsPlayed { get; set; }
        internal bool IsSaveChannel { get; set; }
        internal bool ChannelIsSaved { get; set; }
        internal bool IsInterceptChannel { get; set; }
        internal bool ChannelIsIntercepted { get; set; }
        internal bool IsPadded { get; set; }
        internal Action<Tape> Callback { get; set; }
        internal Action<Tape> ChannelCallback { get; set; }
        
        // Buff
        
        public Buff Buff { get; internal set; }
                        
        // Options

        internal bool CacheToDisk { get; set; }
        internal bool PlayAllTapes { get; set; }
        internal int ExtraBufferFrames { get; set; }

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