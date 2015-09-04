using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.NHibernate.Names;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class PatchMapping : ClassMap<Patch>
    {
        public PatchMapping()
        {
            Id(x => x.ID).GeneratedBy.Assigned();
            Map(x => x.Name);
            HasMany(x => x.Operators).KeyColumn(ColumnNames.PatchID).Inverse();
            References(x => x.Document, ColumnNames.DocumentID);
        }
    }
}
