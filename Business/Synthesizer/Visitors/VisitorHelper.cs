using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Visitors
{
    internal static class VisitorHelper
    {
        public static bool IsDimensionWriter(IOperatorDto dto)
        {
            Type dtoType = dto.GetType();
            bool isDimensionWriter = _dimensionWriting_OperatorDto_Types.Contains(dtoType);
            return isDimensionWriter;
        }

        private static HashSet<Type> _dimensionWriting_OperatorDto_Types = new HashSet<Type>
        {
            typeof(DimensionToOutlets_Outlet_OperatorDto),
            typeof(Loop_OperatorDto),
            typeof(Loop_OperatorDto_NoSkipOrRelease_ManyConstants),
            typeof(Loop_OperatorDto_ManyConstants),
            typeof(Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration),
            typeof(Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration),
            typeof(Loop_OperatorDto_NoSkipOrRelease),
            typeof(Loop_OperatorDto_SignalVarOrConst_OtherInputsVar),
            typeof(SetDimension_OperatorDto),
            typeof(SetDimension_OperatorDto_VarPassThrough_VarNumber),
            typeof(SetDimension_OperatorDto_VarPassThrough_ConstNumber),
            typeof(Squash_OperatorDto),
            typeof(Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin),
            typeof(Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin),
            typeof(Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin),
            typeof(Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin),
            typeof(Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin),
            typeof(Squash_OperatorDto_VarSignal_VarFactor_VarOrigin),
            typeof(Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting),
            typeof(Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking),
            typeof(Stretch_OperatorDto),
            typeof(Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin),
            typeof(Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin),
            typeof(Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin),
            typeof(Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin),
            typeof(Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin),
            typeof(Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin),
            typeof(Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting),
            typeof(Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking)
        };

        /// <summary>
        /// Checks whether the stack count is incremented by exactly 1 after the action.
        /// It looks like this functionality belongs in a base visitor,
        /// but there already is a base class, and this feature applies only to very specific derived
        /// classes and you cannot just add on features without repeating code,
        /// unless you put it in a helper method like this.
        /// </summary>
        /// <param name="stack">
        /// You need to call it stack in the error messages,
        /// ICollection is used in lack of an IStack interface.
        /// </param>
        [DebuggerHidden]
        public static void WithStackCheck(ICollection stack, Action action)
        {
            if (stack == null) throw new NullException(() => stack);
            if (action == null) throw new NullException(() => action);

            int stackCountBefore = stack.Count;

            action();

            int expectedStackCount = stackCountBefore + 1;

            if (stack.Count != expectedStackCount)
            {
                throw new Exception(string.Format(
                    "{2} was not incremented by exactly 1 after visitation. {0} = {1}, {2} = {3}.",
                    nameof(expectedStackCount),
                    expectedStackCount,
                    ExpressionHelper.GetText(() => stack.Count),
                    stack.Count));
            }
        }

        public static int GetSamplesBetweenApplyFilterVariables(double secondsBetweenApplyFilterVariables, int samplingRate)
        {
            double samplesBetweenApplyFilterVariablesDouble = secondsBetweenApplyFilterVariables * samplingRate;
            if (!ConversionHelper.CanCastToPositiveInt32(samplesBetweenApplyFilterVariablesDouble))
            {
                throw new Exception($"{new { samplesBetweenApplyFilterVariablesDouble }} cannot be cast to positive Int32.");
            }
            int samplesBetweenApplyFilterVariables = (int)(secondsBetweenApplyFilterVariables * samplingRate);
            return samplesBetweenApplyFilterVariables;
        }

        public static void AssertDimensionStacksCountsAre1(DimensionStackCollection dimensionStackCollection)
        {
            if (dimensionStackCollection == null) throw new NullException(() => dimensionStackCollection);

            foreach (DimensionStack dimensionStack in dimensionStackCollection.GetDimensionStacks())
            {
                // ReSharper disable once InvertIf
                if (dimensionStack.Count != 1) // 1, because a single item is added by default as when the DimensionStackCollection is initialized.
                {
                    var dimensionIdentifier = new { dimensionStack.CanonicalCustomDimensionName, dimensionStack.StandardDimensionEnum };
                    throw new Exception($"DimensionStack.Count for DimensionStack {dimensionIdentifier} should be 1 but it is {dimensionStack.Count}.");
                }
            }
        }
    }
}
