using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.NHibernate.Names;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
	public class SpeakerSetupMapping : ClassMap<SpeakerSetup>
	{
		public SpeakerSetupMapping()
		{
			Id(x => x.ID).GeneratedBy.Assigned();
			Map(x => x.Name);
			HasMany(x => x.SpeakerSetupChannels).KeyColumn(ColumnNames.SpeakerSetupID).Inverse();
		}
	}
}
