using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer
{
    public class Workspace
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }

        public virtual Document MainDocument { get; set; }
        public virtual IList<Document> InstrumentDocuments { get; set; }
        public virtual IList<Document> EffectDocuments { get; set; }

        public virtual IList<LibraryReference> LibraryReferences { get; set; }
        public virtual IList<LibraryReference> AsReferencedWorkspaceInLibraryReferences { get; set; }
    }
}
