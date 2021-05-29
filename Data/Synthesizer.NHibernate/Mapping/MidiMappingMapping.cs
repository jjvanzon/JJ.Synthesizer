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
            Map(x => x.FromMidiValue);
            Map(x => x.TillMidiValue);
            Map(x => x.MidiControllerCode);
            References(x => x.Dimension, ColumnNames.DimensionID);
            Map(x => x.Name);
            Map(x => x.FromDimensionValue);
            Map(x => x.TillDimensionValue);
            Map(x => x.MinDimensionValue);
            Map(x => x.MaxDimensionValue);
            Map(x => x.Position);
            References(x => x.EntityPosition, ColumnNames.EntityPositionID);
            References(x => x.MidiMappingType, ColumnNames.MidiMappingTypeID);
            References(x => x.MidiMappingGroup, ColumnNames.MidiMappingGroupID);
        }
    }
}