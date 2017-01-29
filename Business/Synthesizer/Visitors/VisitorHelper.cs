using System;
using System.Collections;
using System.Diagnostics;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Visitors
{
    internal static class VisitorHelper
    {
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
                if (dimensionStack.Count != 1) // 1, because a single item is added by default as when the DimensionStackCollection is initialized.
                {
                    var dimensionIdentifier = new { dimensionStack.CanonicalCustomDimensionName, dimensionStack.StandardDimensionEnum };
                    throw new Exception($"DimensionStack.Count for DimensionStack {dimensionIdentifier} should be 1 but it is {dimensionStack.Count}.");
                }
            }
        }
    }
}
