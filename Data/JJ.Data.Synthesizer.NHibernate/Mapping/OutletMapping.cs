using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.NHibernate.Names;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class OutletMapping : ClassMap<Outlet>
    {
        public OutletMapping()
        {
            Id(x => x.ID);
            Map(x => x.Name);
            References(x => x.Operator, ColumnNames.OperatorID);
            HasMany(x => x.ConnectedInlets).KeyColumn(ColumnNames.InputOutletID).Inverse();
            HasMany(x => x.AsAudioFileOutputChannels).KeyColumn(ColumnNames.OutletID).Inverse();
        }
    }
}