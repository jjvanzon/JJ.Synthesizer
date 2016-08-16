using System;
using System.Collections.Generic;
using System.Diagnostics;
using JJ.Data.Synthesizer.Helpers;

namespace JJ.Data.Synthesizer
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class Document
    {
        public Document()
        {
            AudioFileOutputs = new List<AudioFileOutput>();
            ChildDocuments = new List<Document>();
            Curves = new List<Curve>();
            Patches = new List<Patch>();
            Samples = new List<Sample>();
            Scales = new List<Scale>();
            DependentOnDocuments = new List<DocumentReference>();
            DependentDocuments = new List<DocumentReference>();
        }

        public virtual int ID { get; set; }
        public virtual string Name { get; set; }

        public virtual IList<AudioFileOutput> AudioFileOutputs { get; set; }

        /// <summary> Null for child documents, not null for root documents. </summary>
        public virtual AudioOutput AudioOutput { get; set; }
        public virtual IList<Curve> Curves { get; set; }

        /// <summary> Currently contains exactly 1 patch for ChildDocuments, and 0 patches for root Documents. </summary>
        public virtual IList<Patch> Patches { get; set; }
        public virtual IList<Sample> Samples { get; set; }
        public virtual IList<Scale> Scales { get; set; }

        public virtual IList<Document> ChildDocuments { get; set; }
        public virtual Document ParentDocument { get; set; }
        public virtual string GroupName { get; set; }

        public virtual IList<DocumentReference> DependentOnDocuments { get; set; }
        public virtual IList<DocumentReference> DependentDocuments { get; set; }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
