using FluentNHibernate.Mapping;
using JJ.Persistence.Synthesizer.NHibernate.Names;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.NHibernate.Mapping
{
    public class SpeakerSetupChannelMapping : ClassMap<SpeakerSetupChannel>
    {
        public SpeakerSetupChannelMapping()
        {
            Id(x => x.ID);
            Map(x => x.IndexNumber);
            References(x => x.SpeakerSetup, ColumnNames.SpeakerSetupID);
            References(x => x.Channel, ColumnNames.ChannelID);
        }
    }
}
