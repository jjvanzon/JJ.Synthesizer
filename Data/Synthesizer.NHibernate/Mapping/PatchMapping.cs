using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.NHibernate.Names;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
	public class PatchMapping : ClassMap<Patch>
	{
		public PatchMapping()
		{
			Id(x => x.ID).GeneratedBy.Assigned();
			Map(x => x.Name);
			Map(x => x.GroupName);
			Map(x => x.Hidden);
			Map(x => x.HasDimension);
			Map(x => x.CustomDimensionName);
			HasMany(x => x.Operators).KeyColumn(ColumnNames.PatchID).Inverse();
			HasMany(x => x.DerivedOperators).KeyColumn(ColumnNames.UnderlyingPatchID).Inverse();
			References(x => x.Document, ColumnNames.DocumentID);
			References(x => x.StandardDimension, ColumnNames.StandardDimensionID);
		}
	}
}
