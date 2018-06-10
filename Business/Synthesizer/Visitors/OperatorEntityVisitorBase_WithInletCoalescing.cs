using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Visitors
{
	internal abstract class OperatorEntityVisitorBase_WithInletCoalescing : OperatorEntityVisitorBase
	{
		protected abstract void InsertNumber(double number);
		protected abstract void InsertEmptyInput();

		protected override void VisitMultiplyInlet(Inlet inlet) => CoalesceToOne(inlet);

	    protected override void VisitAverageOverInletsInlet(Inlet inlet) => CoalesceToEmptyInput(inlet);

	    protected override void VisitClosestOverInletsExpInlet(Inlet inlet) => CoalesceToEmptyInput(inlet);

	    protected override void VisitClosestOverInletsInlet(Inlet inlet) => CoalesceToEmptyInput(inlet);

	    protected override void VisitMaxOverInletsInlet(Inlet inlet) => CoalesceToEmptyInput(inlet);

	    protected override void VisitMinOverInletsInlet(Inlet inlet) => CoalesceToEmptyInput(inlet);

	    protected override void VisitSortOverInletsInlet(Inlet inlet) => CoalesceToEmptyInput(inlet);

	    protected override void VisitInletOther(Inlet inlet) => CoalesceToDefaultValueOrZero(inlet);

	    /// <summary>
		/// Loop inlets have special coalescing: null ReleaseEndMarker or NoteDuration must coalesce to quasi-infinity.
		/// </summary>
		protected override void VisitLoopInlet(Inlet inlet)
		{
			var wrapper = new OperatorWrapper(inlet.Operator);

			if (inlet == wrapper.Inlets[DimensionEnum.ReleaseEndMarker] ||
				inlet == wrapper.Inlets[DimensionEnum.NoteDuration])
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
			// ReSharper disable once ConvertIfStatementToSwitchStatement
			if (inlet.InputOutlet == null)
			{
				if (inlet.DefaultValue.HasValue)
				{
					InsertNumber(inlet.DefaultValue.Value);
					return;
				}
				// ReSharper disable once RedundantIfElseBlock
				else
				{
					InsertNumber(0.0);
					return;
				}
			}

			VisitInletBase(inlet);
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
		private void CoalesceToEmptyInput(Inlet inlet)
		{
			if (inlet.InputOutlet == null)
			{
				InsertEmptyInput();
				return;
			}

			VisitInletBase(inlet);
		}
	}
}