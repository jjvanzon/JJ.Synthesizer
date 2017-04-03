using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class AudioFileFormatMapping : ClassMap<AudioFileFormat>
    {
        public AudioFileFormatMapping()
        {
            Id(x => x.ID).GeneratedBy.Assigned();
            Map(x => x.Name);
        }
    }
}
                    