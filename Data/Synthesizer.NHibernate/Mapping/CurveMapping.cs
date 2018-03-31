using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.NHibernate.Names;
// ReSharper disable UnusedMember.Global

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
	public class CurveMapping : ClassMap<Curve>
	{
		public CurveMapping()
		{
			Id(x => x.ID).GeneratedBy.Assigned();
			Map(x => x.Name);
			HasMany(x => x.Nodes).KeyColumn(ColumnNames.CurveID).Inverse();
		}
	}
}
