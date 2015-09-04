using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.NHibernate.Names;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class AudioFileOutputChannelMapping : ClassMap<AudioFileOutputChannel>
    {
        public AudioFileOutputChannelMapping()
        {
            Id(x => x.ID).GeneratedBy.Assigned();
            Map(x => x.IndexNumber);
            References(x => x.Outlet, ColumnNames.OutletID);
            References(x => x.AudioFileOutput, ColumnNames.AudioFileOutputID);
        }
    }
}
