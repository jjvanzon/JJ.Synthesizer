using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.NHibernate.Names;
// ReSharper disable UnusedMember.Global

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
	public class MidiMappingGroupMapping : ClassMap<MidiMappingGroup>
	{
		public MidiMappingGroupMapping()
		{
			Id(x => x.ID).GeneratedBy.Assigned();
			Map(x => x.Name);
			References(x => x.Document, ColumnNames.DocumentID);
			HasMany(x => x.MidiMappingElements).KeyColumn(ColumnNames.MidiMappingGroupID).Inverse();
		}
	}
}
