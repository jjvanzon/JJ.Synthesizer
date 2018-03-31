using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.NHibernate.Names;
// ReSharper disable UnusedMember.Global

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
	public class InletMapping : ClassMap<Inlet>
	{
		public InletMapping()
		{
			Id(x => x.ID).GeneratedBy.Assigned();
			Map(x => x.Name);
			Map(x => x.Position);
			Map(x => x.DefaultValue);
			Map(x => x.IsRepeating);
			Map(x => x.RepetitionPosition);
			Map(x => x.IsObsolete);
			Map(x => x.WarnIfEmpty);
			Map(x => x.NameOrDimensionHidden);
			References(x => x.Operator, ColumnNames.OperatorID);
			References(x => x.InputOutlet, ColumnNames.InputOutletID);
			References(x => x.Dimension, ColumnNames.DimensionID);
		}
	}
}
