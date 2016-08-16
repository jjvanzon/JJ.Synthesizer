using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JJ.Data.Synthesizer.Helpers;

namespace JJ.Data.Synthesizer
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class Dimension
    {
        public Dimension()
        {
            AsXDimensionInCurves = new List<Curve>();
            AsYDimensionInCurves = new List<Curve>();
        }

        public virtual int ID { get; set; }
        public virtual string Name { get; set; }

        public virtual IList<Curve> AsXDimensionInCurves { get; set; }
        public virtual IList<Curve> AsYDimensionInCurves { get; set; }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
