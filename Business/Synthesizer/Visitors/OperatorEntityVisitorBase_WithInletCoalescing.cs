using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Visitors
{
    internal abstract class OperatorEntityVisitorBase_WithInletCoalescing : OperatorEntityVisitorBase
    {
        protected abstract void InsertNumber(double number);

        protected override void VisitMultiplyInlet(Inlet inlet)
        {
            CoalesceToOne(inlet);
        }

        protected override void VisitAverageOverInletsInlet(Inlet inlet)
        {
            CoalesceByIgnoringEmptyInlet(inlet);
        }

        protected override void VisitClosestOverInletsExpInlet(Inlet inlet)
        {
            CoalesceByIgnoringEmptyInlet(inlet);
        }

        protected override void VisitClosestOverInletsInlet(Inlet inlet)
        {
            CoalesceByIgnoringEmptyInlet(inlet);
        }

        protected override void VisitMaxOverInletsInlet(Inlet inlet)
        {
            CoalesceByIgnoringEmptyInlet(inlet);
        }

        protected override void VisitMinOverInletsInlet(Inlet inlet)
        {
            CoalesceByIgnoringEmptyInlet(inlet);
        }

        protected override void VisitSortOverInletsInlet(Inlet inlet)
        {
            CoalesceByIgnoringEmptyInlet(inlet);
        }

        protected override void VisitInletOther(Inlet inlet)
        {
            CoalesceToDefaultValueOrZero(inlet);
        }

        /// <summary>
        /// Loop inlets have special coalescing: null ReleaseEndMarker or NoteDuration must coalesce to quasi-infinity.
        /// </summary>
        protected override void VisitLoopInlet(Inlet inlet)
        {
            var wrapper = new Loop_OperatorWrapper(inlet.Operator);

            if (inlet == wrapper.ReleaseEndMarkerInlet ||
                inlet == wrapper.NoteDurationInlet)
            {
                if (inlet.InputOutlet == null)
                {
                    InsertNumber(CalculationHelper.VERY_HIGH_VALUE);
                    return;
                }
            }

            CoalesceToDefaultValueOrZero(inlet);
        }

        private void CoalesceToDefaultValueOrZero(Inlet inlet)
        {
            if (inlet.InputOutlet == null)
            {
                if (inlet.DefaultValue.HasValue)
                {
                    InsertNumber(inlet.DefaultValue.Value);
                    return;
                }
                else
                {
                    InsertNumber(0.0);
                    return;
                }
            }

            base.VisitInletBase(inlet);
        }

        private void CoalesceToOne(Inlet inlet)
        {
            if (inlet.InputOutlet == null)
            {
                InsertNumber(1.0);
                return;
            }

            VisitInletBase(inlet);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CoalesceByIgnoringEmptyInlet(Inlet inlet)
        {
            base.VisitInletBase(inlet);
        }
    }
}