using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.NHibernate.Names;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
	public class MidiMappingMapping : ClassMap<MidiMapping>
	{
		public MidiMappingMapping()
		{
			Id(x => x.ID).GeneratedBy.Assigned();
			Map(x => x.Name);
			References(x => x.Document, ColumnNames.DocumentID);
			HasMany(x => x.MidiMappingElements).KeyColumn(ColumnNames.MidiMappingID).Inverse();
		}
	}
}
