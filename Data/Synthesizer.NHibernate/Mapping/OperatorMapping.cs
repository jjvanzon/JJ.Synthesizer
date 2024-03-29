﻿using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.NHibernate.Names;
// ReSharper disable UnusedMember.Global

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class OperatorMapping : ClassMap<Operator>
    {
        public OperatorMapping()
        {
            Id(x => x.ID).GeneratedBy.Assigned();
            Map(x => x.Name);
            Map(x => x.Data);
            Map(x => x.CustomDimensionName);
            Map(x => x.HasDimension);

            References(x => x.Patch, ColumnNames.PatchID);
            References(x => x.UnderlyingPatch, ColumnNames.UnderlyingPatchID);
            References(x => x.StandardDimension, ColumnNames.StandardDimensionID);
            References(x => x.Sample, ColumnNames.SampleID);
            References(x => x.Curve, ColumnNames.CurveID);
            References(x => x.EntityPosition, ColumnNames.EntityPositionID);

            HasMany(x => x.Inlets).KeyColumn(ColumnNames.OperatorID).Inverse();
            HasMany(x => x.Outlets).KeyColumn(ColumnNames.OperatorID).Inverse();
        }
    }
}
