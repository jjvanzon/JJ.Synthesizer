using FluentNHibernate.Mapping;

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
