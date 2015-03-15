using FluentNHibernate.Mapping;
using JJ.Persistence.Synthesizer.NHibernate.Names;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.NHibernate.Mapping
{
    public class SampleOperatorMapping : ClassMap<SampleOperator>
    {
        public SampleOperatorMapping()
        {
            Id(x => x.ID);
            References(x => x.Operator, ColumnNames.OperatorID);
            References(x => x.Sample, ColumnNames.SampleID);
        }
    }
}
