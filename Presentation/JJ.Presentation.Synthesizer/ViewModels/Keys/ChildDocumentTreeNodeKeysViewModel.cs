using JJ.Presentation.Synthesizer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels.Keys
{
    public sealed class ChildDocumentTreeNodeKeysViewModel
    {
        public int ID { get; set; }

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

        public ChildDocumentTypeEnum ChildDocumentTypeEnum { get; set; }
    }
}