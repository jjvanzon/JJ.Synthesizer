using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.NHibernate.Names;
// ReSharper disable UnusedMember.Global

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class ScaleMapping : ClassMap<Scale>
    {
        public ScaleMapping()
        {
            Id(x => x.ID).GeneratedBy.Assigned();
            Map(x => x.Name);
            Map(x => x.BaseFrequency);
            References(x => x.ScaleType, ColumnNames.ScaleTypeID);
            References(x => x.Document, ColumnNames.DocumentID);
            HasMany(x => x.Tones).KeyColumn(ColumnNames.ScaleID).Inverse();
        }
    }
}
