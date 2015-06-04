using JJ.Presentation.Synthesizer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels.Keys
{
    public sealed class InletKeysViewModel
    {
        public int ID { get; set; }

        public int DocumentID { get; set; }
        public ChildDocumentTypeEnum? ChildDocumentTypeEnum { get; set; }
        public int? ChildDocumentListIndex { get; set; }
        public int PatchListIndex { get; set; }

        /// <summary>
        /// You cannot identify the operator with a list index, 
        /// because you cannot sort by anything that gives it a specific position.
        /// </summary>
        public Guid OperatorTemporaryID { get; set; }
        public int ListIndex { get; set; }
    }
}
