﻿using System.Diagnostics;
using JJ.Data.Synthesizer.Helpers;

namespace JJ.Data.Synthesizer
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class EntityPosition
    {
        /// <summary> Data store generated, auto-incremented ID, unlike other entities. </summary>
        public virtual int ID { get; set; }
        public virtual string EntityTypeName { get; set; }
        public virtual int EntityID { get; set; }
        public virtual float X { get; set; }
        public virtual float Y { get; set; }

        private string DebuggerDisplay
        {
            get { return DebugHelper.GetDebuggerDisplay(this); }
        }
    }
}