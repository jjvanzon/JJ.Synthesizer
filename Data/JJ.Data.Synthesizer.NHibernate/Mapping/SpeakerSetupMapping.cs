using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.NHibernate.Names;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class SpeakerSetupMapping : ClassMap<SpeakerSetup>
    {
        public SpeakerSetupMapping()
        {
            Id(x => x.ID).GeneratedBy.Assigned();
            Map(x => x.Name);
            HasMany(x => x.SpeakerSetupChannels).KeyColumn(ColumnNames.SpeakerSetupID).Inverse();
        }
    }
}
