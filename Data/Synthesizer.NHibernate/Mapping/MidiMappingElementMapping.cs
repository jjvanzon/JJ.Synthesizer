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
			Map(x => x.NoteNumberFrom);
			Map(x => x.NoteNumberTill);
			Map(x => x.ControllerCode);
			Map(x => x.ControllerValueFrom);
			Map(x => x.ControllerValueTill);
			Map(x => x.VelocityValueFrom);
			Map(x => x.VelocityValueTill);
			References(x => x.StandardDimension, ColumnNames.StandardDimensionID);
			Map(x => x.CustomDimensionName);
			Map(x => x.DimensionValueFrom);
			Map(x => x.DimensionValueTill);
			Map(x => x.DimensionMinValue);
			Map(x => x.DimensionMaxValue);
			Map(x => x.ListIndexFrom);
			Map(x => x.ListIndexTill);
			References(x => x.Scale, ColumnNames.ScaleID);
			Map(x => x.ToneIndexFrom);
			Map(x => x.ToneIndexTill);

			References(x => x.MidiMapping, ColumnNames.MidiMappingElementID);
		}
	}
}