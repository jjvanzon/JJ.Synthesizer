using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Data.Synthesizer
{
    /// <summary>
    /// Not that Outlet does not have a relation to OutletType.
    /// Operators of Type PatchOutlet have an OutletType encoded into their Data property.
    /// </summary>
    public class OutletType
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
    }
}
