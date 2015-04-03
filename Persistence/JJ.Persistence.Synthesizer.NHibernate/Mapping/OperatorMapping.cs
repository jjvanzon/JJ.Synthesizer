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

            // The tierlantijnen, like HasOne and Cascade.All(), are necessary for NHibernate to understand 1-to-1 relationships.
            HasOne(x => x.AsCurveIn).Cascade.All();
            HasOne(x => x.AsSampleOperator).Cascade.All();
            HasOne(x => x.AsValueOperator).Cascade.All();

            HasMany(x => x.Inlets).KeyColumn(ColumnNames.OperatorID).Inverse();
            HasMany(x => x.Outlets).KeyColumn(ColumnNames.OperatorID).Inverse();
        }
    }
}
