using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.Entities;
// ReSharper disable UnusedMember.Global

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
	public class ScaleTypeMapping : ClassMap<ScaleType>
	{
		public ScaleTypeMapping()
		{
			Id(x => x.ID).GeneratedBy.Assigned();
			Map(x => x.Name);
		}
	}
}
