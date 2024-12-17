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
        /// <inheritdoc cref="docs._tapename" />
        public string GetName => NameHelper.ResolveName(Signal, ChannelSignals, FallBackName, FilePath);
        
        public FlowNode Duration { get; set; }
        public FlowNode Signal { get; set; }
        public int? Channel { get; set; }
        
        /// <summary> For stereo tapes. </summary>
        public IList<FlowNode> ChannelSignals { get; set; }
        
        /// <inheritdoc cref="docs._istape" />
        public bool IsTape { get; set; }
        public bool IsPlay { get; set; }
        public bool IsSave { get; set; }
        public bool IsIntercept { get; set; }
        public bool IsPlayChannel { get; set; }
        public bool IsSaveChannel { get; set; }
        public bool IsInterceptChannel { get; set; }
        public bool IsPadding { get; set; }
        
        public string FallBackName { get; set; }
        public string FilePath { get; set; }
        public Func<Buff, Buff> Callback { get; set; }
        public Func<Buff, int, Buff> ChannelCallback { get; set; }
        public Buff Buff { get; set; }
        
        public HashSet<Tape> ParentTapes { get; } = new HashSet<Tape>();
        public HashSet<Tape> ChildTapes { get; } = new HashSet<Tape>();
        public int NestingLevel { get; set; }
        
        private int? GetChannels()
        {
            int? channelCount = ChannelSignals?.Count;
            if (Signal != null) channelCount = 1;
            return channelCount;
        }

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

        private string DebuggerDisplay => GetDebuggerDisplay(this);
    }
}