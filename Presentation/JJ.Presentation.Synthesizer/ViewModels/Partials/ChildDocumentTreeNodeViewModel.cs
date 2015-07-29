using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels.Partials
{
    public sealed class ChildDocumentTreeNodeViewModel
    {
        public int ChildDocumentID { get; set; }

        /// <summary>
        /// TODO: The ChildDocumentID is probably unique enough for this.
        /// Can be used to uniquely identify a node within the DocumentTreeViewModel,
        /// e.g for collapsing and expanding specific nodes and remembering IsExpanded
        /// even when refreshing / recreating the view model.
        /// </summary>
        public int NodeIndex { get; set; }

        public string Name { get; set; }
        public bool IsExpanded { get; set; }

        public DummyViewModel CurvesNode { get; set; }
        public DummyViewModel SamplesNode { get; set; }
        public DummyViewModel PatchesNode { get; set; }
    }
}
