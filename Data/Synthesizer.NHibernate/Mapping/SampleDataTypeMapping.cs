using FluentNHibernate.Mapping;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class SampleDataTypeMapping : ClassMap<SampleDataType>
    {
        public SampleDataTypeMapping()
        {
            Id(x => x.ID).GeneratedBy.Assigned();
            Map(x => x.Name);
        }
    }
}
