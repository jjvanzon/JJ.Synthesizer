using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer
{
    public class LibraryReference
    {
        public virtual int ID { get; set; }
        public virtual string Alias { get; set; }
        public virtual Workspace ReferringWorkspace { get; set; }
        public virtual Workspace ReferencedWorkspace { get; set; }
    }
}
