﻿using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.Entities;
// ReSharper disable UnusedMember.Global

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class DimensionMapping : ClassMap<Dimension>
    {
        public DimensionMapping()
        {
            Id(x => x.ID).GeneratedBy.Assigned();
            Map(x => x.Name);
        }
    }
}
