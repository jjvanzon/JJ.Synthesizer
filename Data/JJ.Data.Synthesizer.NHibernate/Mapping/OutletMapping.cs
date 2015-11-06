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
            References(x => x.Operator, ColumnNames.OperatorID);
            HasMany(x => x.ConnectedInlets).KeyColumn(ColumnNames.InputOutletID).Inverse();
            HasMany(x => x.AsAudioFileOutputChannels).KeyColumn(ColumnNames.OutletID).Inverse();
        }
    }
}