using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using JJ.Framework.Wishes.Common;
using static JJ.Business.Synthesizer.Tests.Helpers.DebuggerDisplayFormatter;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    #pragma warning disable CS0611
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal class NullyPair<T> where T : struct
    {
        // Properties
        
        public T? Nully     { get; set; }
        public T  Coalesced { get; set; }
        
        // Conversion Operators
        
        public static implicit operator T?(NullyPair<T>                      pair)  => pair.Nully;
        public static implicit operator T (NullyPair<T>                      pair)  => pair.Coalesced;
        public static implicit operator NullyPair<T>(T?                      value) => new NullyPair<T> { Nully = value };
        public static implicit operator NullyPair<T>(T                       value) => new NullyPair<T> { Nully = value, Coalesced   = value };
        public static implicit operator NullyPair<T>((T? nully, T coalesced) x)     => new NullyPair<T> { Nully = x.nully, Coalesced = x.coalesced };
        
        public static   bool operator ==(NullyPair<T> a, NullyPair<T> b) => Equals(a?.Coalesced, b?.Coalesced);
        public static   bool operator !=(NullyPair<T> a, NullyPair<T> b) => !Equals(a?.Coalesced, b?.Coalesced);
        public override bool Equals(object            obj) => obj is NullyPair<T> other && Equals(Coalesced, other.Coalesced);
        
        // Descriptions
        
        string                 DebuggerDisplay => DebuggerDisplay(this);
        public override string ToString()      => Descriptor;
        
        public string Descriptor
        {
            get
            {
                if (!FilledInWishes.Has(Nully) && !FilledInWishes.Has(Coalesced)) return "";
                if (Equals(Nully, Coalesced)) return $"{Nully}";
                if (FilledInWishes.Has(Nully) && !FilledInWishes.Has(Coalesced)) return $"{Nully}";
                return $"({Nully},{Coalesced})";
            }
        }
    }
}