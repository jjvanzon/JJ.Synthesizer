using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.ViewModels.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
