using FluentNHibernate.Mapping;
using JJ.Persistence.Synthesizer.NHibernate.Names;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.NHibernate.Mapping
{
    public class InletMapping : ClassMap<Inlet>
    {
        public InletMapping()
        {
            Id(x => x.ID);
            Map(x => x.Name);
            References(x => x.Operator, ColumnNames.OperatorID);
            References(x => x.InputOutlet, ColumnNames.InputOutletID);
        }
    }
}
