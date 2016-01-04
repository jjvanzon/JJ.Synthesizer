using JJ.Data.Canonical;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    public sealed class ReferencedDocumentViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public IList<ChildDocumentIDAndNameViewModel> Patches { get; set; }
    }
}
