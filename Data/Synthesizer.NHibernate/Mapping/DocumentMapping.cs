using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.NHibernate.Names;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
	public class DocumentMapping : ClassMap<Document>
	{
		public DocumentMapping()
		{
			Id(x => x.ID).GeneratedBy.Assigned();

			Map(x => x.Name);

			References(x => x.AudioOutput, ColumnNames.AudioOutputID);

			HasMany(x => x.Patches).KeyColumn(ColumnNames.DocumentID).Inverse();
			HasMany(x => x.Scales).KeyColumn(ColumnNames.DocumentID).Inverse();
			HasMany(x => x.AudioFileOutputs).KeyColumn(ColumnNames.DocumentID).Inverse();
			HasMany(x => x.MidiMappings).KeyColumn(ColumnNames.DocumentID).Inverse();

			HasMany(x => x.LowerDocumentReferences).KeyColumn(ColumnNames.HigherDocumentID).Inverse();
			HasMany(x => x.HigherDocumentReferences).KeyColumn(ColumnNames.LowerDocumentID).Inverse();
		}
	}
}
