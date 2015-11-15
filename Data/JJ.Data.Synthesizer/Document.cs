using System.Collections.Generic;

namespace JJ.Data.Synthesizer
{
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
        public virtual IList<Curve> Curves { get; set; }
        public virtual IList<Patch> Patches { get; set; }
        public virtual IList<Sample> Samples { get; set; }
        public virtual IList<Scale> Scales { get; set; }

        public virtual ChildDocumentType ChildDocumentType { get; set; }
        public virtual IList<Document> ChildDocuments { get; set; }
        public virtual Document ParentDocument { get; set; }

        public virtual IList<DocumentReference> DependentOnDocuments { get; set; }
        public virtual IList<DocumentReference> DependentDocuments { get; set; }

        /// <summary>
        /// Nullable.
        /// Has no inverse property 
        /// (with as the only reason that 1 to 1 relations are horrible in NHibernate).
        /// Can only refer to something out of the Patches list.
        /// The Patch Inlets and Outlets will be the inlets and outlets of the document.
        /// </summary>
        public virtual Patch MainPatch { get; set; }
    }
}
