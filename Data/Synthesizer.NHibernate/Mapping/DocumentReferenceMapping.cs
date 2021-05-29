using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.NHibernate.Names;
// ReSharper disable UnusedMember.Global

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class DocumentReferenceMapping : ClassMap<DocumentReference>
    {
        public DocumentReferenceMapping()
        {
            Id(x => x.ID).GeneratedBy.Assigned();
            Map(x => x.Alias);

            References(x => x.LowerDocument, ColumnNames.LowerDocumentID);
            References(x => x.HigherDocument, ColumnNames.HigherDocumentID);
        }
    }
}
