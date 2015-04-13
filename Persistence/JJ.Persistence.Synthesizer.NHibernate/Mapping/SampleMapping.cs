using FluentNHibernate.Mapping;
using JJ.Persistence.Synthesizer.NHibernate.Names;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.NHibernate.Mapping
{
    public class SampleMapping : ClassMap<Sample>
    {
        public SampleMapping()
        {
            Id(x => x.ID);

            Map(x => x.Name);
            Map(x => x.Amplifier);
            Map(x => x.TimeMultiplier);
            Map(x => x.IsActive);
            Map(x => x.SamplingRate);
            Map(x => x.BytesToSkip);
            Map(x => x.Location);

            References(x => x.SampleDataType, ColumnNames.SampleDataTypeID);
            References(x => x.SpeakerSetup, ColumnNames.SpeakerSetupID);
            References(x => x.AudioFileFormat, ColumnNames.AudioFileFormatID);
            References(x => x.InterpolationType, ColumnNames.InterpolationTypeID);

            // TODO: Remove outcommented code.
            //HasMany(x => x.SampleOperators).KeyColumn(ColumnNames.SampleID).Inverse();
        }
    }
}
