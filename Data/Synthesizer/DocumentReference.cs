namespace JJ.Data.Synthesizer
{
    public class DocumentReference
    {
        public virtual int ID { get; set; }

        /// <summary>
        /// Used to disambiguates in case of name clashes between documents.
        /// Even when things are referenced by ID, this can be useful in either the user interface,
        /// so the user sees what he is picking, or in a type of persistence where
        /// we do reference by name, e.g. a readable XML format.
        /// </summary>
        public virtual string Alias { get; set; }
        public virtual Document DependentDocument { get; set; }
        public virtual Document DependentOnDocument { get; set; }
    }
}
