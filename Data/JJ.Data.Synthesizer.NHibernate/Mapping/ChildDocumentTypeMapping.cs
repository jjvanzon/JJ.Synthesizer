using FluentNHibernate.Mapping;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class ChildDocumentTypeMapping : ClassMap<ChildDocumentType>
    {
        public ChildDocumentTypeMapping()
        {
            Id(x => x.ID).GeneratedBy.Assigned();
            Map(x => x.Name);
        }
    }
}
