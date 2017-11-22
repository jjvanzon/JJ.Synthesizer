using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
	public class EntityPositionMapping : ClassMap<EntityPosition>
	{
		public EntityPositionMapping()
		{
			Id(x => x.ID).GeneratedBy.Assigned();
			Map(x => x.EntityTypeName);
			Map(x => x.EntityID);
			Map(x => x.X);
			Map(x => x.Y);
		}
	}
}
