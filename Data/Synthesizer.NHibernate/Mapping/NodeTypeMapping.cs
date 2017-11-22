using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
	public class NodeTypeMapping : ClassMap<NodeType>
	{
		public NodeTypeMapping()
		{
			Id(x => x.ID).GeneratedBy.Assigned();
			Map(x => x.Name);
		}
	}
}
