using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.NHibernate.Names;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class OperatorMapping : ClassMap<Operator>
    {
        public OperatorMapping()
        {
            Id(x => x.ID).GeneratedBy.Assigned();
            Map(x => x.Name);
            Map(x => x.Data);
            Map(x => x.CustomDimensionName);

            References(x => x.OperatorType, ColumnNames.OperatorTypeID);
            References(x => x.Patch, ColumnNames.PatchID);
            References(x => x.StandardDimension, ColumnNames.StandardDimensionID);

            HasMany(x => x.Inlets).KeyColumn(ColumnNames.OperatorID).Inverse();
            HasMany(x => x.Outlets).KeyColumn(ColumnNames.OperatorID).Inverse();
        }
    }
}
