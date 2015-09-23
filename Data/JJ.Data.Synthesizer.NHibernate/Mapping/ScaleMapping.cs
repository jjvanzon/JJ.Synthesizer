using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.NHibernate.Names;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class ScaleMapping : ClassMap<Scale>
    {
        public ScaleMapping()
        {
            Id(x => x.ID).GeneratedBy.Assigned();
            Map(x => x.Name);
            Map(x => x.BaseFrequency);
            References(x => x.ScaleType, ColumnNames.ScaleTypeID);
            References(x => x.Document, ColumnNames.DocumentID);
            HasMany(x => x.Tones).KeyColumn(ColumnNames.ToneID).Inverse();
        }
    }
}
