using System.Collections.Generic;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Presentation.Synthesizer.Helpers
{
    internal class ToPatchWithRelatedEntitiesResult
    {
        public Patch Patch {get;set;}
        public IList<Operator> OperatorsToDelete { get; set; }
    }
}
