using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.NHibernate.Names;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
	public class OutletMapping : ClassMap<Outlet>
	{
		public OutletMapping()
		{
			Id(x => x.ID).GeneratedBy.Assigned();
			Map(x => x.Name);
			Map(x => x.Position);
			Map(x => x.IsRepeating);
			Map(x => x.RepetitionPosition);
			Map(x => x.IsObsolete);
			Map(x => x.NameOrDimensionHidden);
			References(x => x.Operator, ColumnNames.OperatorID);
			References(x => x.Dimension, ColumnNames.DimensionID);
			HasMany(x => x.ConnectedInlets).KeyColumn(ColumnNames.InputOutletID).Inverse();
			HasMany(x => x.InAudioFileOutputs).KeyColumn(ColumnNames.OutletID).Inverse();
		}
	}
}