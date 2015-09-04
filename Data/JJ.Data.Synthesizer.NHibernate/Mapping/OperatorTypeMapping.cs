using FluentNHibernate.Mapping;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class OperatorTypeMapping : ClassMap<OperatorType>
    {
        public OperatorTypeMapping()
        {
            Id(x => x.ID).GeneratedBy.Assigned();
            Map(x => x.Name);
            Map(x => x.SortOrder);
        }
    }
}
