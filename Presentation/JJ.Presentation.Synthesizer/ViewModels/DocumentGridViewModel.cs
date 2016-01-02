using JJ.Data.Canonical;
using JJ.Framework.Presentation;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class DocumentGridViewModel
    {
        public bool Visible { get; set; }
        public IList<IDAndName> List { get; set; }
        public PagerViewModel Pager { get; set; }
    }
}
