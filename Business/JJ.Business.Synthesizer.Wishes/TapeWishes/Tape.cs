using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static JJ.Business.Synthesizer.Wishes.Helpers.DebuggerDisplayFormatter;

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal class Tape
    {
        private string DebuggerDisplay => GetDebuggerDisplay(this);

        // Names

        /// <inheritdoc cref="docs._tapename" />
        public string GetName => NameHelper.ResolveName(Signal, Signals, FallBackName, FilePath);
        public string FallBackName { get; set; }
        public string FilePath { get; set; }

        // Signals

        public FlowNode Signal { get; set; }
        /// <summary> For stereo tapes. </summary>
        public IList<FlowNode> Signals { get; set; }
        
        // Audio Properties

        public FlowNode Duration { get; set; }
        public int? Channel { get; set; }
        
        public int? GetChannels()
        {
            int? channelCount = Signals?.Count;
            if (Signal != null) channelCount = 1;
            return channelCount;
        }

        // Actions

        /// <inheritdoc cref="docs._istape" />
        public bool IsTape { get; set; }
        public bool IsPlay { get; set; }
        public bool IsPlayed { get; set; }
        public bool IsSave { get; set; }
        public bool IsSaved { get; set; }
        public bool IsIntercept { get; set; }
        public bool IsIntercepted { get; set; }
        public bool IsPlayChannel { get; set; }
        public bool ChannelIsPlayed { get; set; }
        public bool IsSaveChannel { get; set; }
        public bool ChannelsIsSaved { get; set; }
        public bool IsInterceptChannel { get; set; }
        public bool ChannelIsIntercepted { get; set; }
        public bool IsPadding { get; set; }
        public bool IsPadded { get; set; }
        public Func<Buff, Buff> Callback { get; set; }
        public Func<Buff, int, Buff> ChannelCallback { get; set; }
        
        // Buff

        public Buff Buff { get; set; }
        
        // Hierarchy

        public HashSet<Tape> ParentTapes { get; } = new HashSet<Tape>();
        public HashSet<Tape> ChildTapes { get; } = new HashSet<Tape>();
        public int NestingLevel { get; set; }

        public void ClearRelationships()
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