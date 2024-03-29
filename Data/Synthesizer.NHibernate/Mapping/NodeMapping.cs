﻿using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.NHibernate.Names;
// ReSharper disable UnusedMember.Global

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class NodeMapping : ClassMap<Node>
    {
        public NodeMapping()
        {
            Id(x => x.ID).GeneratedBy.Assigned();
            Map(x => x.X);
            Map(x => x.Y);
            Map(x => x.Slope);

            References(x => x.Curve, ColumnNames.CurveID);
            References(x => x.InterpolationType, ColumnNames.InterpolationTypeID);
        }
    }
}
