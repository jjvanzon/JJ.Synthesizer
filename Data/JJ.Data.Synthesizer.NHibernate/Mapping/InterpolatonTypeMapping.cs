using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class InterpolationTypeMapping : ClassMap<InterpolationType>
    {
        public InterpolationTypeMapping()
        {
            Id(x => x.ID);
            Map(x => x.Name);
        }
    }
}
