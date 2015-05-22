using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels.Partials
{
    public sealed class ReferencedDocumentsTreeNodeViewModel
    {
        public IList<ReferencedDocumentViewModel> List { get; set; }
    }
}
