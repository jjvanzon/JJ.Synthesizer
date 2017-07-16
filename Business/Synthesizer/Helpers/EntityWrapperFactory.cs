using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Helpers
{
    public static class EntityWrapperFactory
    {
        public static OperatorWrapper CreateOperatorWrapper(
            Operator op,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository)
        {
            if (op == null) throw new NullException(() => op);

            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.Curve: return new Curve_OperatorWrapper(op, curveRepository);
                case OperatorTypeEnum.Sample: return new Sample_OperatorWrapper(op, sampleRepository);
                case OperatorTypeEnum.Cache: return new Cache_OperatorWrapper(op);
                case OperatorTypeEnum.InletsToDimension: return new InletsToDimension_OperatorWrapper(op);
                case OperatorTypeEnum.Interpolate: return new Interpolate_OperatorWrapper(op);
                case OperatorTypeEnum.Number: return new Number_OperatorWrapper(op);
                case OperatorTypeEnum.PatchInlet: return new PatchInletOrOutlet_OperatorWrapper(op);
                case OperatorTypeEnum.PatchOutlet: return new PatchInletOrOutlet_OperatorWrapper(op);
                case OperatorTypeEnum.Random: return new Random_OperatorWrapper(op);
                case OperatorTypeEnum.Reset: return new Reset_OperatorWrapper(op);
                case OperatorTypeEnum.AverageOverDimension:
                case OperatorTypeEnum.ClosestOverDimension:
                case OperatorTypeEnum.ClosestOverDimensionExp:
                case OperatorTypeEnum.MaxOverDimension:
                case OperatorTypeEnum.MinOverDimension:
                case OperatorTypeEnum.SortOverDimension:
                case OperatorTypeEnum.SumFollower:
                case OperatorTypeEnum.SumOverDimension:
                    return new OperatorWrapper_WithCollectionRecalculation(op);

                default:
                    return new OperatorWrapper(op);
            }
        }
    }
}