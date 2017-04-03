using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.NHibernate.Names;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class AudioFileOutputMapping : ClassMap<AudioFileOutput>
    {
        public AudioFileOutputMapping()
        {
            Id(x => x.ID).GeneratedBy.Assigned();

            Map(x => x.Name);
            Map(x => x.Amplifier);
            Map(x => x.TimeMultiplier);
            Map(x => x.StartTime);
            Map(x => x.Duration);
            Map(x => x.SamplingRate);
            Map(x => x.FilePath);

            References(x => x.AudioFileFormat, ColumnNames.AudioFileFormatID);
            References(x => x.SampleDataType, ColumnNames.SampleDataTypeID);
            References(x => x.SpeakerSetup, ColumnNames.SpeakerSetupID);
            References(x => x.Document, ColumnNames.DocumentID);
            References(x => x.Outlet, ColumnNames.OutletID);
        }
    }
}
