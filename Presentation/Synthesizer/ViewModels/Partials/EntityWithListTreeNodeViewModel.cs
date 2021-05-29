using System.Collections.Generic;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels.Partials
{
    public sealed class EntityWithListTreeNodeViewModel
    {
        public bool Visible { get; set; }
        public string Text { get; set; }
        public int EntityID { get; set; }
        public IList<IDAndName> List { get; set; }
    }
}