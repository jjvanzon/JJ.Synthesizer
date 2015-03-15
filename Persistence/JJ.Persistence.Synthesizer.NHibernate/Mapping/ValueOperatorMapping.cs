using FluentNHibernate.Mapping;
using JJ.Persistence.Synthesizer.NHibernate.Names;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.NHibernate.Mapping
{
    public class ValueOperatorMapping : ClassMap<ValueOperator>
    {
        public ValueOperatorMapping()
        {
            Id(x => x.ID);
            Map(x => x.Value);
            References(x => x.Operator, ColumnNames.OperatorID);
        }
    }
}
