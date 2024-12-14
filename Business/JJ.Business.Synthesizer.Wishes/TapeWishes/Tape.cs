using System;
using System.Collections.Generic;
using System.Diagnostics;
using JJ.Business.Synthesizer.Wishes.Helpers;

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
        public bool IsTape { get;set; }
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
        
        public Tape ParentTape { get; set; }
        public IList<Tape> ChildTapes { get; } = new List<Tape>();
        public int NestingLevel { get; set; }
        
        private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
    }
}