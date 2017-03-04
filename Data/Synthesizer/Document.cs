using System.Collections.Generic;
using System.Diagnostics;
using JJ.Data.Synthesizer.Helpers;
// ReSharper disable VirtualMemberCallInConstructor

namespace JJ.Data.Synthesizer
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class Document
    {
        public Document()
        {
            AudioFileOutputs = new List<AudioFileOutput>();
            Curves = new List<Curve>();
            Patches = new List<Patch>();
            Samples = new List<Sample>();
            Scales = new List<Scale>();
            LowerDocumentReferences = new List<DocumentReference>();
            HigherDocumentReferences = new List<DocumentReference>();
        }

        public virtual int ID { get; set; }
        public virtual string Name { get; set; }

        public virtual IList<AudioFileOutput> AudioFileOutputs { get; set; }

        public virtual AudioOutput AudioOutput { get; set; }
        public virtual IList<Curve> Curves { get; set; }

        public virtual IList<Patch> Patches { get; set; }
        public virtual IList<Sample> Samples { get; set; }
        public virtual IList<Scale> Scales { get; set; }
        public virtual IList<DocumentReference> LowerDocumentReferences { get; set; }
        public virtual IList<DocumentReference> HigherDocumentReferences { get; set; }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
