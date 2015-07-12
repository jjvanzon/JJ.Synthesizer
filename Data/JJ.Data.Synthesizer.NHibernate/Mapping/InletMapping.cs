using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.NHibernate.Names;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class InletMapping : ClassMap<Inlet>
    {
        public InletMapping()
        {
            Id(x => x.ID).GeneratedBy.Assigned();
            Map(x => x.Name);
            References(x => x.Operator, ColumnNames.OperatorID);
            References(x => x.InputOutlet, ColumnNames.InputOutletID);
        }
    }
}
