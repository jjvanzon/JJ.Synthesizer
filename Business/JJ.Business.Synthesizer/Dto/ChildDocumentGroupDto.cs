using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Dto
{
    public class ChildDocumentGroupDto
    {
        public string GroupName { get; set; }
        public IList<Document> Documents { get; set; }
    }
}
