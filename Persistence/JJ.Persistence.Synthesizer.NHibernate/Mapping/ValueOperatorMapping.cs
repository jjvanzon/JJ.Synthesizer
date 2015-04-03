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
            // Horrible code for NHibernate to understand a 1-to-1 relationship.
            Id(x => x.OperatorID).GeneratedBy.Foreign(TableNames.Operator);
            HasOne(x => x.Operator).Constrained().ForeignKey();
 
            Map(x => x.Value);
        }
    }
}
