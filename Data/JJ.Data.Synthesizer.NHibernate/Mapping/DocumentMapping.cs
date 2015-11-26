using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.NHibernate.Names;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class DocumentMapping : ClassMap<Document>
    {
        public DocumentMapping()
        {
            Id(x => x.ID).GeneratedBy.Assigned();

            Map(x => x.Name);
            Map(x => x.GroupName);

            References(x => x.ParentDocument, ColumnNames.ParentDocumentID);

            HasMany(x => x.Curves).KeyColumn(ColumnNames.DocumentID).Inverse();
            HasMany(x => x.Patches).KeyColumn(ColumnNames.DocumentID).Inverse();
            HasMany(x => x.Samples).KeyColumn(ColumnNames.DocumentID).Inverse();
            HasMany(x => x.Scales).KeyColumn(ColumnNames.DocumentID).Inverse();
            HasMany(x => x.AudioFileOutputs).KeyColumn(ColumnNames.DocumentID).Inverse();
            HasMany(x => x.ChildDocuments).KeyColumn(ColumnNames.ParentDocumentID).Inverse();

            HasMany(x => x.DependentOnDocuments).KeyColumn(ColumnNames.DependentDocumentID).Inverse();
            HasMany(x => x.DependentDocuments).KeyColumn(ColumnNames.DependentOnDocumentID).Inverse();
        }
    }
}
