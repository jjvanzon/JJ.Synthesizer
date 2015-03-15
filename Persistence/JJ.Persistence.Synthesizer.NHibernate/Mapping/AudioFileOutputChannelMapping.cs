using FluentNHibernate.Mapping;
using JJ.Persistence.Synthesizer.NHibernate.Names;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.NHibernate.Mapping
{
    public class AudioFileOutputChannelMapping : ClassMap<AudioFileOutputChannel>
    {
        public AudioFileOutputChannelMapping()
        {
            Id(x => x.ID);
            Map(x => x.IndexNumber);
            References(x => x.Outlet, ColumnNames.OutletID);
            References(x => x.AudioFileOutput, ColumnNames.AudioFileOutputID);
        }
    }
}
