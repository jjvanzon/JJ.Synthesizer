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
                { OperatorTypeEnum.AverageOverDimension, Create_OperatorWrapper_WithCollectionRecalculation },
                { OperatorTypeEnum.Cache, Create_Cache_OperatorWrapper },
                { OperatorTypeEnum.ClosestOverDimension, Create_OperatorWrapper_WithCollectionRecalculation },
                { OperatorTypeEnum.ClosestOverDimensionExp, Create_OperatorWrapper_WithCollectionRecalculation },
                { OperatorTypeEnum.Exponent, Create_Exponent_OperatorWrapper },
                { OperatorTypeEnum.InletsToDimension, Create_InletsToDimension_OperatorWrapper },
                { OperatorTypeEnum.Interpolate, Create_Interpolate_OperatorWrapper },
                { OperatorTypeEnum.Loop, Create_Loop_OperatorWrapper },
                { OperatorTypeEnum.MaxOverDimension, Create_OperatorWrapper_WithCollectionRecalculation },
                { OperatorTypeEnum.MinOverDimension, Create_OperatorWrapper_WithCollectionRecalculation },
                { OperatorTypeEnum.Number, Create_Number_OperatorWrapper },
                { OperatorTypeEnum.PatchInlet, Create_PatchInlet_OperatorWrapper },
                { OperatorTypeEnum.PatchOutlet, Create_PatchOutlet_OperatorWrapper },
                { OperatorTypeEnum.Random, Create_Random_OperatorWrapper },
                { OperatorTypeEnum.Reset, Create_Reset_OperatorWrapper },
                { OperatorTypeEnum.Reverse, Create_Reverse_OperatorWrapper },
                { OperatorTypeEnum.Scaler, Create_Scaler_OperatorWrapper },
                { OperatorTypeEnum.Shift, Create_Shift_OperatorWrapper },
                { OperatorTypeEnum.SortOverDimension, Create_OperatorWrapper_WithCollectionRecalculation },
                { OperatorTypeEnum.Squash, Create_Squash_OperatorWrapper },
                { OperatorTypeEnum.Stretch, Create_Stretch_OperatorWrapper },
                { OperatorTypeEnum.SumFollower, Create_OperatorWrapper_WithCollectionRecalculation },
                { OperatorTypeEnum.SumOverDimension, Create_OperatorWrapper_WithCollectionRecalculation },
                { OperatorTypeEnum.TimePower, Create_TimePower_OperatorWrapper },
            };

        private static OperatorWrapper_WithCollectionRecalculation Create_OperatorWrapper_WithCollectionRecalculation(Operator op) => new OperatorWrapper_WithCollectionRecalculation(op);
        private static Cache_OperatorWrapper Create_Cache_OperatorWrapper(Operator op) => new Cache_OperatorWrapper(op);
        private static Exponent_OperatorWrapper Create_Exponent_OperatorWrapper(Operator op) => new Exponent_OperatorWrapper(op);
        private static InletsToDimension_OperatorWrapper Create_InletsToDimension_OperatorWrapper(Operator op) => new InletsToDimension_OperatorWrapper(op);
        private static Interpolate_OperatorWrapper Create_Interpolate_OperatorWrapper(Operator op) => new Interpolate_OperatorWrapper(op);
        private static Loop_OperatorWrapper Create_Loop_OperatorWrapper(Operator op) => new Loop_OperatorWrapper(op);
        private static Number_OperatorWrapper Create_Number_OperatorWrapper(Operator op) => new Number_OperatorWrapper(op);
        private static PatchInlet_OperatorWrapper Create_PatchInlet_OperatorWrapper(Operator op) => new PatchInlet_OperatorWrapper(op);
        private static PatchOutlet_OperatorWrapper Create_PatchOutlet_OperatorWrapper(Operator op) => new PatchOutlet_OperatorWrapper(op);
        private static Random_OperatorWrapper Create_Random_OperatorWrapper(Operator op) => new Random_OperatorWrapper(op);
        private static Reset_OperatorWrapper Create_Reset_OperatorWrapper(Operator op) => new Reset_OperatorWrapper(op);
        private static Reverse_OperatorWrapper Create_Reverse_OperatorWrapper(Operator op) => new Reverse_OperatorWrapper(op);
        private static Scaler_OperatorWrapper Create_Scaler_OperatorWrapper(Operator op) => new Scaler_OperatorWrapper(op);
        private static Shift_OperatorWrapper Create_Shift_OperatorWrapper(Operator op) => new Shift_OperatorWrapper(op);
        private static Squash_OperatorWrapper Create_Squash_OperatorWrapper(Operator op) => new Squash_OperatorWrapper(op);
        private static Stretch_OperatorWrapper Create_Stretch_OperatorWrapper(Operator op) => new Stretch_OperatorWrapper(op);
        private static TimePower_OperatorWrapper Create_TimePower_OperatorWrapper(Operator op) => new TimePower_OperatorWrapper(op);
    }
}