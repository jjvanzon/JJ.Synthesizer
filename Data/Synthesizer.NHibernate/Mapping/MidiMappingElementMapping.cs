using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.NHibernate.Names;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
	public class MidiMappingElementMapping : ClassMap<MidiMappingElement>
	{
		public MidiMappingElementMapping()
		{
			Id(x => x.ID).GeneratedBy.Assigned();

			Map(x => x.IsActive);
			Map(x => x.IsRelative);
			Map(x => x.FromNoteNumber);
			Map(x => x.TillNoteNumber);
			Map(x => x.ControllerCode);
			Map(x => x.FromControllerValue);
			Map(x => x.TillControllerValue);
			Map(x => x.FromVelocity);
			Map(x => x.TillVelocity);
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

			References(x => x.MidiMapping, ColumnNames.MidiMappingElementID);
		}
	}
}