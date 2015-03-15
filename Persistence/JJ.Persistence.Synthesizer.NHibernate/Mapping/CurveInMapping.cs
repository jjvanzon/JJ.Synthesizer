using FluentNHibernate.Mapping;
using JJ.Persistence.Synthesizer.NHibernate.Names;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.NHibernate.Mapping
{
    public class CurveInMapping : ClassMap<CurveIn>
    {
        public CurveInMapping()
        {
            Id(x => x.ID);
            References(x => x.Curve, ColumnNames.CurveID);
            References(x => x.Operator, ColumnNames.OperatorID);
        }
    }
}
