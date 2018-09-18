using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace JJ.Business.Synthesizer.EntityWrappers
{
	/// <summary>
	/// Scaler has a specialized OperatorWrapper, because its inlets are not identified by Dimension, but by position,
	/// which makes then harder to use through the standard OperatorWrapper(_WithUnderlyingPatch).
	/// </summary>
	public class Scaler_OperatorWrapper : OperatorWrapper
	{
		private const int SOURCE_VALUE_A_INDEX = 1;
		private const int SOURCE_VALUE_B_INDEX = 2;
		private const int TARGET_VALUE_A_INDEX = 3;
		private const int TARGET_VALUE_B_INDEX = 4;

		public Scaler_OperatorWrapper(Operator op)
			: base(op)
		{ }

		public Outlet SignalInput
		{
			get => SignalInlet.InputOutlet;
			set => SignalInlet.LinkTo(value);
		}

		public Inlet SignalInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Signal);

		public Outlet SourceValueA
		{
			get => SourceValueAInlet.InputOutlet;
			set => SourceValueAInlet.LinkTo(value);
		}

		public Inlet SourceValueAInlet => InletOutletSelector.GetInlet(WrappedOperator, SOURCE_VALUE_A_INDEX);

		public Outlet SourceValueB
		{
			get => SourceValueBInlet.InputOutlet;
			set => SourceValueBInlet.LinkTo(value);
		}

		public Inlet SourceValueBInlet => InletOutletSelector.GetInlet(WrappedOperator, SOURCE_VALUE_B_INDEX);

		public Outlet TargetValueA
		{
			get => TargetValueAInlet.InputOutlet;
			set => TargetValueAInlet.LinkTo(value);
		}

		public Inlet TargetValueAInlet => InletOutletSelector.GetInlet(WrappedOperator, TARGET_VALUE_A_INDEX);

		public Outlet TargetValueB
		{
			get => TargetValueBInlet.InputOutlet;
			set => TargetValueBInlet.LinkTo(value);
		}

		public Inlet TargetValueBInlet => InletOutletSelector.GetInlet(WrappedOperator, TARGET_VALUE_B_INDEX);

		public Outlet SignalOutlet => InletOutletSelector.GetOutlet(WrappedOperator, DimensionEnum.Signal);

		public override string GetInletDisplayName(Inlet inlet)
		{
			if (inlet == null) throw new NullException(() => inlet);

			switch (inlet.Position)
			{
				case SOURCE_VALUE_A_INDEX:
					return ResourceFormatter.GetDisplayName(() => SourceValueA);

				case SOURCE_VALUE_B_INDEX:
					return ResourceFormatter.GetDisplayName(() => SourceValueB);

				case TARGET_VALUE_A_INDEX:
					return ResourceFormatter.GetDisplayName(() => TargetValueA);

				case TARGET_VALUE_B_INDEX:
					return ResourceFormatter.GetDisplayName(() => TargetValueB);
			}

			return base.GetInletDisplayName(inlet);
		}
	}
}