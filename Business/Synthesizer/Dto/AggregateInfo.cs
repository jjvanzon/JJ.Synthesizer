using System;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal class AggregateInfo
    {
        public IList<InputDto> Vars { get; set; }
        public IList<InputDto> Consts { get; set; }
        public bool HasVars { get; set; }
        public bool HasConsts { get; set; }
        public bool OnlyVars { get; set; }
        public bool OnlyConsts { get; set; }
        public bool ConstIsOne { get; set; }
        public bool ConstIsZero { get; set; }
        /// <summary> nullable </summary>
        public InputDto Const { get; set; }
        public bool IsEmpty { get; set; }
    }
}
