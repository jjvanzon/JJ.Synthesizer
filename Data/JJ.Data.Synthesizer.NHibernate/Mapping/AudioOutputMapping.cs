using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.NHibernate.Names;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class AudioOutputMapping : ClassMap<AudioOutput>
    {
        protected AudioOutputMapping()
        {
            Id(x => x.ID);
            Map(x => x.SamplingRate);
            Map(x => x.VolumeFactor);
            Map(x => x.SpeedFactor);
            Map(x => x.MaxConcurrentNotes);
            Map(x => x.BufferDuration);
            References(x => x.SpeakerSetup, ColumnNames.SpeakerSetupID);
        }
    }
}