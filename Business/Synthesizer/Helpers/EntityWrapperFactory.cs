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
        public static OperatorWrapperBase CreateOperatorWrapper(
            Operator op,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository)
        {
            if (op == null) throw new NullException(() => op);

            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();

            if (operatorTypeEnum == OperatorTypeEnum.Curve)
            {
                return new Curve_OperatorWrapper(op, curveRepository);
            }

            if (operatorTypeEnum == OperatorTypeEnum.Sample)
            {
                return new Sample_OperatorWrapper(op, sampleRepository);
            }

            if (_createOperatorWrapperDelegateDictionary.TryGetValue(operatorTypeEnum, out Func<Operator, OperatorWrapperBase> func))
            {
                OperatorWrapperBase wrapper = func(op);
                return wrapper;
            }

            // Otherwise assume WithUnderlyingPatch.
            return new OperatorWrapper_WithUnderlyingPatch(op);
        }

        private static readonly Dictionary<OperatorTypeEnum, Func<Operator, OperatorWrapperBase>> _createOperatorWrapperDelegateDictionary =
            new Dictionary<OperatorTypeEnum, Func<Operator, OperatorWrapperBase>>
            {
                { OperatorTypeEnum.AverageOverDimension, x => new OperatorWrapper_WithCollectionRecalculation(x) },
                { OperatorTypeEnum.Cache, x => new Cache_OperatorWrapper(x) },
                { OperatorTypeEnum.ClosestOverDimension, x => new OperatorWrapper_WithCollectionRecalculation(x)  },
                { OperatorTypeEnum.ClosestOverDimensionExp, x => new OperatorWrapper_WithCollectionRecalculation(x)  },
                { OperatorTypeEnum.InletsToDimension, x => new InletsToDimension_OperatorWrapper(x) },
                { OperatorTypeEnum.Interpolate, x => new Interpolate_OperatorWrapper(x) },
                { OperatorTypeEnum.MaxOverDimension, x => new OperatorWrapper_WithCollectionRecalculation(x)  },
                { OperatorTypeEnum.MinOverDimension, x => new OperatorWrapper_WithCollectionRecalculation(x)  },
                { OperatorTypeEnum.Number, x => new Number_OperatorWrapper(x) },
                { OperatorTypeEnum.PatchInlet, x => new PatchInlet_OperatorWrapper(x) },
                { OperatorTypeEnum.PatchOutlet, x => new PatchOutlet_OperatorWrapper(x) },
                { OperatorTypeEnum.Random, x => new Random_OperatorWrapper(x) },
                { OperatorTypeEnum.Reset, x => new Reset_OperatorWrapper(x) },
                { OperatorTypeEnum.SortOverDimension, x => new OperatorWrapper_WithCollectionRecalculation(x)  },
                { OperatorTypeEnum.SumFollower, x => new OperatorWrapper_WithCollectionRecalculation(x)  },
                { OperatorTypeEnum.SumOverDimension, x => new OperatorWrapper_WithCollectionRecalculation(x)  }
            };
    }
}