using System;
using System.Collections.Generic;
using JJ.Business.CanonicalModel;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class PatchGridViewModel
    {
        public int RootDocumentID { get; set; }
        public string Group { get; set; }
        public bool Visible { get; set; }
        public IList<IDAndName> List { get; set; }
    }
}
