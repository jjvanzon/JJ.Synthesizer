using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.NHibernate.Names;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class ToneMapping : ClassMap<Tone>
    {
        public ToneMapping()
        {
            Id(x => x.ID).GeneratedBy.Assigned();
            Map(x => x.Octave);
            Map(x => x.ID);
            Map(x => x.Number);
            References(x => x.Scale);
        }
    }
}
