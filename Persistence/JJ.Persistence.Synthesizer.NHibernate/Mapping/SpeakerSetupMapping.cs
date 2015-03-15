using FluentNHibernate.Mapping;
using JJ.Persistence.Synthesizer.NHibernate.Names;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.NHibernate.Mapping
{
    public class SpeakerSetupMapping : ClassMap<SpeakerSetup>
    {
        public SpeakerSetupMapping()
        {
            Id(x => x.ID);
            Map(x => x.Name);
            HasMany(x => x.SpeakerSetupChannels).KeyColumn(ColumnNames.SpeakerSetupID).Inverse();
        }
    }
}
