using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.NHibernate.Names;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class OutletMapping : ClassMap<Outlet>
    {
        public OutletMapping()
        {
            Id(x => x.ID).GeneratedBy.Assigned();
            Map(x => x.Name);
            Map(x => x.ListIndex);
            Map(x => x.IsObsolete);
            References(x => x.Operator, ColumnNames.OperatorID);
            References(x => x.Dimension, ColumnNames.DimensionID);
            HasMany(x => x.ConnectedInlets).KeyColumn(ColumnNames.InputOutletID).Inverse();
            HasMany(x => x.InAudioFileOutputs).KeyColumn(ColumnNames.OutletID).Inverse();
        }
    }
}