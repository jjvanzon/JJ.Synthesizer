using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.NHibernate.Names;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class ToneMapping : ClassMap<Tone>
    {
        public ToneMapping()
        {
            Id(x => x.ID, ColumnNames.ToneID).GeneratedBy.Assigned();
            Map(x => x.Octave);
            Map(x => x.ID);
            Map(x => x.Number);
            References(x => x.Scale);
        }
    }
}
