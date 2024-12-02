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
        public string GetName => NameHelper.FetchName(Signal, FallBackName, FilePath);
        
        public FlowNode Signal { get; set; }
        public FlowNode Duration { get; set; }
        public int ChannelIndex { get; set; }
        
        public bool WithPlay { get; set; }
        public bool WithSave { get; set; }
        public bool WithCache { get; set; }
        public bool WithPlayChannel { get; set; }
        public bool WithSaveChannel { get; set; }
        public bool WithCacheChannel { get; set; }
        
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