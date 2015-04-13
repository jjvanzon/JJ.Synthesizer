using FluentNHibernate.Mapping;
using JJ.Persistence.Synthesizer.NHibernate.Names;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.NHibernate.Mapping
{
    public class OperatorMapping : ClassMap<Operator>
    {
        public OperatorMapping()
        {
            Id(x => x.ID);
            Map(x => x.Name);
            Map(x => x.OperatorTypeName);
            Map(x => x.Data);

            References(x => x.Patch, ColumnNames.PatchID);

            HasMany(x => x.Inlets).KeyColumn(ColumnNames.OperatorID).Inverse();
            HasMany(x => x.Outlets).KeyColumn(ColumnNames.OperatorID).Inverse();
        }
    }
}
