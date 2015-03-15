using FluentNHibernate.Mapping;
using JJ.Persistence.Synthesizer.NHibernate.Names;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.NHibernate.Mapping
{
    public class CurveMapping : ClassMap<Curve>
    {
        public CurveMapping()
        {
            Id(x => x.ID);
            Map(x => x.Name);
            HasMany(x => x.Nodes).KeyColumn(ColumnNames.CurveID).Inverse();
            HasMany(x => x.CurvesIn).KeyColumn(ColumnNames.CurveID).Inverse();
        }
    }
}
