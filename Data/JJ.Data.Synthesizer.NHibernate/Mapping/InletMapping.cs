using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.NHibernate.Names;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class InletMapping : ClassMap<Inlet>
    {
        public InletMapping()
        {
            Id(x => x.ID).GeneratedBy.Assigned();
            Map(x => x.Name);
            Map(x => x.ListIndex);
            Map(x => x.DefaultValue);
            Map(x => x.IsObsolete);
            References(x => x.Operator, ColumnNames.OperatorID);
            References(x => x.InputOutlet, ColumnNames.InputOutletID);
            References(x => x.Dimension, ColumnNames.DimensionID);
        }
    }
}
