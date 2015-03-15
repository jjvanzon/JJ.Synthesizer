using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.NHibernate.Mapping
{
    public class SampleDataTypeMapping : ClassMap<SampleDataType>
    {
        public SampleDataTypeMapping()
        {
            Id(x => x.ID);
            Map(x => x.Name);
        }
    }
}
