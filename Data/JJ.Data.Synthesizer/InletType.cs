using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Data.Synthesizer
{
    /// <summary>
    /// Not that Inlet does not have a relation to InletType.
    /// Operators of Type PatchInlet have an InletType encoded into their Data property.
    /// </summary>
    public class InletType
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
    }
}
