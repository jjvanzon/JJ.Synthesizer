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

            References(x => x.Patch, ColumnNames.PatchID);

            // TODO: Check if these 0-to-1 relations work. 
            // I have had problems with 1-to-1 relations with NHibernate in the past.
            // I tried the Nullable() method here. Maybe it works. I don't know.
            References(x => x.AsCurveIn, ColumnNames.OperatorID).Nullable();
            References(x => x.AsSampleOperator, ColumnNames.OperatorID).Nullable();
            References(x => x.AsValueOperator, ColumnNames.OperatorID).Nullable();

            HasMany(x => x.Inlets).KeyColumn(ColumnNames.OperatorID).Inverse();
            HasMany(x => x.Outlets).KeyColumn(ColumnNames.OperatorID).Inverse();
        }
    }
}
