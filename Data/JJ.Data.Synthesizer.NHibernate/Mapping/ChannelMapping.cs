using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.NHibernate.Names;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class ChannelMapping : ClassMap<Channel>
    {
        public ChannelMapping()
        {
            Id(x => x.ID).GeneratedBy.Assigned();
            Map(x => x.Name);
            Map(x => x.IndexNumber);
            HasMany(x => x.SpeakerSetupChannels).KeyColumn(ColumnNames.ChannelID).Inverse();
        }
    }
}
