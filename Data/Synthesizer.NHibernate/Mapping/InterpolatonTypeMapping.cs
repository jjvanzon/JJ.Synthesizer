using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class InterpolationTypeMapping : ClassMap<InterpolationType>
    {
        public InterpolationTypeMapping()
        {
            Id(x => x.ID).GeneratedBy.Assigned();
            Map(x => x.Name);
            Map(x => x.SortOrder);
        }
    }
}
