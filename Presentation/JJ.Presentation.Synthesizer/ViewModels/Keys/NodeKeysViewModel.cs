using JJ.Presentation.Synthesizer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels.Keys
{
    public sealed class NodeKeysViewModel
    {
        public int ID { get; set; }

        public int RootDocumentID { get; set; }
        public ChildDocumentTypeEnum? ChildDocumentTypeEnum { get; set; }
        public int? ChildDocumentListIndex { get; set; }
        public int CurveListIndex { get; set; }

        /// <summary>
        /// You cannot identify the node with with a list index, 
        /// because even if you sort by time, the time of two nodes might be the same, 
        /// so cannot sort by anything that gives it a specific position.
        /// </summary>
        public Guid TemporaryID { get; set; }

        [Obsolete("Use TemporaryID instead.")]
        public int ListIndex { get; set; }
    }
}
