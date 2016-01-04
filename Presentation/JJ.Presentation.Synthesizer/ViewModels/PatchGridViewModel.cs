using System;
using System.Collections.Generic;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels.Entities;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class PatchGridViewModel
    {
        public int RootDocumentID { get; set; }
        public string Group { get; set; }
        public bool Visible { get; set; }
        public IList<ChildDocumentIDAndNameViewModel> List { get; set; }
    }
}
