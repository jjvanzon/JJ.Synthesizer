using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.NHibernate.Names;
// ReSharper disable UnusedMember.Global

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
	public class ChannelMapping : ClassMap<Channel>
	{
		public ChannelMapping()
		{
			Id(x => x.ID).GeneratedBy.Assigned();
			Map(x => x.Name);
			HasMany(x => x.SpeakerSetupChannels).KeyColumn(ColumnNames.ChannelID).Inverse();
		}
	}
}
