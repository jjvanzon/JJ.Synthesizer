using System;
using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.NHibernate.Names;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class CurveMapping : ClassMap<Curve>
    {
        public CurveMapping()
        {
            Id(x => x.ID).GeneratedBy.Assigned();
            Map(x => x.Name);
            HasMany(x => x.Nodes).KeyColumn(ColumnNames.CurveID).Inverse();
            References(x => x.Document, ColumnNames.DocumentID);
            References(x => x.XDimension, ColumnNames.XDimensionID);
            References(x => x.YDimension, ColumnNames.YDimensionID);
        }
    }
}
