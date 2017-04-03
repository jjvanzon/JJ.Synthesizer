using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.NHibernate.Names;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class AudioOutputMapping : ClassMap<AudioOutput>
    {
        protected AudioOutputMapping()
        {
            Id(x => x.ID);
            Map(x => x.SamplingRate);
            Map(x => x.MaxConcurrentNotes);
            Map(x => x.DesiredBufferDuration);
            References(x => x.SpeakerSetup, ColumnNames.SpeakerSetupID);
        }
    }
}