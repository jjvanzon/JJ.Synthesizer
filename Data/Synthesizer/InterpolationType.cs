﻿using System.Diagnostics;
using JJ.Data.Synthesizer.Helpers;

namespace JJ.Data.Synthesizer
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class InterpolationType
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual float SortOrder { get; set; }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}