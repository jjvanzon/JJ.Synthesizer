using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;

namespace JJ.Presentation.Synthesizer.Helpers
{
    internal class ToPatchWithRelatedEntitiesResult
    {
        public Patch Patch {get;set;}
        public IList<Operator> OperatorsToDelete { get; set; }
    }
}
