using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.NHibernate.Names;
// ReSharper disable UnusedMember.Global

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class SpeakerSetupChannelMapping : ClassMap<SpeakerSetupChannel>
    {
        public SpeakerSetupChannelMapping()
        {
            Id(x => x.ID).GeneratedBy.Assigned();
            Map(x => x.IndexNumber);
            References(x => x.SpeakerSetup, ColumnNames.SpeakerSetupID);
            References(x => x.Channel, ColumnNames.ChannelID);
        }
    }
}
