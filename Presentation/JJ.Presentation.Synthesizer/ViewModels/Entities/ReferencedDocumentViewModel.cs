using JJ.Business.CanonicalModel;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    public sealed class ReferencedDocumentViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public IList<IDAndName> Instruments { get; set; }
        public IList<IDAndName> Effects { get; set; }
    }
}
