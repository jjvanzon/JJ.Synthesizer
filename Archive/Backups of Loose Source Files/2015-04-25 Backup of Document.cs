using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer
{
    public class Document
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }

        /// <summary> nullable </summary>
        public virtual Workspace AsMainDocumentInWorkspace { get; set; }

        /// <summary> nullable </summary>
        public virtual Workspace AsInstrumentDocumentInWorkspace { get; set; }

        /// <summary> nullable </summary>
        public virtual Workspace AsEffectDocumentInWorkspace { get; set; }

        // TODO: Do the other properties later.
    }
}
