using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.NHibernate.Names;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class PatchMapping : ClassMap<Patch>
    {
        public PatchMapping()
        {
            Id(x => x.ID);
            Map(x => x.Name);
            HasMany(x => x.Operators).KeyColumn(ColumnNames.PatchID).Inverse();
            References(x => x.Document, ColumnNames.DocumentID);
        }
    }
}
