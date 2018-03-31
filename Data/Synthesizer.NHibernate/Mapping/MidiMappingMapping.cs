using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.NHibernate.Names;
// ReSharper disable UnusedMember.Global

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
	public class MidiMappingMapping : ClassMap<MidiMapping>
	{
		public MidiMappingMapping()
		{
			Id(x => x.ID).GeneratedBy.Assigned();

			Map(x => x.IsActive);
			Map(x => x.IsRelative);
			Map(x => x.FromMidiNoteNumber);
			Map(x => x.TillMidiNoteNumber);
			Map(x => x.MidiControllerCode);
			Map(x => x.FromMidiControllerValue);
			Map(x => x.TillMidiControllerValue);
			Map(x => x.FromMidiVelocity);
			Map(x => x.TillMidiVelocity);
			References(x => x.StandardDimension, ColumnNames.StandardDimensionID);
			Map(x => x.CustomDimensionName);
			Map(x => x.FromDimensionValue);
			Map(x => x.TillDimensionValue);
			Map(x => x.MinDimensionValue);
			Map(x => x.MaxDimensionValue);
			Map(x => x.FromPosition);
			Map(x => x.TillPosition);
			References(x => x.Scale, ColumnNames.ScaleID);
			Map(x => x.FromToneNumber);
			Map(x => x.TillToneNumber);
			References(x => x.EntityPosition, ColumnNames.EntityPositionID);

			References(x => x.MidiMappingGroup, ColumnNames.MidiMappingGroupID);
		}
	}
}