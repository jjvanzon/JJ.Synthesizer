using FluentNHibernate.Mapping;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class InterpolationTypeMapping : ClassMap<InterpolationType>
    {
        public InterpolationTypeMapping()
        {
            Id(x => x.ID).GeneratedBy.Assigned();
            Map(x => x.Name);
        }
    }
}
