using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels.Partials
{
    public sealed class ChildDocumentTreeViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsExpanded { get; set; }

        /// <summary>
        /// Can be used to uniquely identify entities of a type throughout the view models,
        /// even for newly created, uncommitted, ID'less entities.
        /// </summary>
        public int ListIndex { get; set; }

        /// <summary>
        /// Can be used to uniquely identify a node within the DocumentTreeViewModel,
        /// e.g for collapsing and expanding specific nodes and remembering IsExpanded
        /// even when refreshing / recreating the view model.
        /// </summary>
        public int NodeIndex { get; set; }

        public DummyViewModel CurvesNode { get; set; }
        public DummyViewModel SamplesNode { get; set; }
        public DummyViewModel PatchesNode { get; set; }
    }
}
