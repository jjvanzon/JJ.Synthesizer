using System.Collections;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Exceptions.Comparative;
// ReSharper disable UnusedMember.Global

namespace JJ.Business.Synthesizer.EntityWrappers
{
	public class OperatorWrapper_Inputs : IEnumerable<Outlet>
	{
		private readonly Operator _operator;

		internal OperatorWrapper_Inputs(Operator op) => _operator = op ?? throw new NullException(() => op);

	    // TODO: Composite keys Name-Position and DimensionEnum-Position have also become normal.

		// By Name

		public Outlet this[string name]
		{
			get => InletOutletSelector.GetInputOutlet(_operator, name);
			set => InletOutletSelector.GetInlet(_operator, name).LinkTo(value);
		}

		public Outlet TryGet(string name) => InletOutletSelector.TryGetInputOutlet(_operator, name);

		public IList<Outlet> GetMany(string name) => InletOutletSelector.GetInputOutlets(_operator, name);

		public void SetMany(string name, IList<Outlet> inputs)
		{
			IList<Inlet> inlets = InletOutletSelector.GetInlets(_operator, name);
			SetMany(inputs, inlets);
		}

		// By Position

		public Outlet this[int position]
		{
			get => InletOutletSelector.GetInputOutlet(_operator, position);
			set => InletOutletSelector.GetInlet(_operator, position).LinkTo(value);
		}

		public Outlet TryGet(int position) => InletOutletSelector.TryGetInputOutlet(_operator, position);

		public IList<Outlet> GetMany(int position) => InletOutletSelector.GetInputOutlets(_operator, position);

		public void SetMany(int position, IList<Outlet> inputs)
		{
			IList<Inlet> inlets = InletOutletSelector.GetInlets(_operator, position);
			SetMany(inputs, inlets);
		}

		// By Dimension

		public Outlet this[DimensionEnum dimensionEnum]
		{
			get => InletOutletSelector.GetInputOutlet(_operator, dimensionEnum);
			set => InletOutletSelector.GetInlet(_operator, dimensionEnum).LinkTo(value);
		}

		public Outlet TryGet(DimensionEnum dimensionEnum) => InletOutletSelector.TryGetInputOutlet(_operator, dimensionEnum);

		public IList<Outlet> GetMany(DimensionEnum dimensionEnum) => InletOutletSelector.GetInputOutlets(_operator, dimensionEnum);

		public void SetMany(DimensionEnum dimensionEnum, IList<Outlet> inputs)
		{
			IList<Inlet> inlets = InletOutletSelector.GetInlets(_operator, dimensionEnum);
			SetMany(inputs, inlets);
		}

		// Enumerable

		public IEnumerator<Outlet> GetEnumerator() => InletOutletSelector.EnumerateSortedInputOutlets(_operator).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => InletOutletSelector.EnumerateSortedInputOutlets(_operator).GetEnumerator();

		// Helpers

		private static void SetMany(IList<Outlet> inputs, IList<Inlet> inlets)
		{
			if (inputs == null) throw new NullException(() => inputs);

			if (inputs.Count != inlets.Count)
			{
				throw new NotEqualException(() => inputs.Count, () => inlets.Count);
			}

			int count = inputs.Count;
			for (int i = 0; i < count; i++)
			{
				Inlet inlet = inlets[i];
				Outlet input = inputs[i];
				inlet.LinkTo(input);
			}

			inlets.ReassignRepetitionPositions();
		}
	}
}
