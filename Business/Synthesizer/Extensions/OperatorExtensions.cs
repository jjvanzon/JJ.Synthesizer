using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Extensions
{
	public static class OperatorExtensions
	{
		public static bool CanSetInletCount(this Operator op)
		{
			OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
			switch (operatorTypeEnum)
			{
				case OperatorTypeEnum.PatchInlet:
				case OperatorTypeEnum.PatchOutlet:
					return false;
			}

			bool hasRepeatingInlet = op.Inlets.Reverse().Any(x => x.IsRepeating);
			return hasRepeatingInlet;
		}

		public static bool CanSetOutletCount(this Operator op)
		{
			OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
			switch (operatorTypeEnum)
			{
				case OperatorTypeEnum.PatchInlet:
				case OperatorTypeEnum.PatchOutlet:
					return false;
			}

			bool hasRepeatingOutlet = op.Outlets.Reverse().Any(x => x.IsRepeating);
			return hasRepeatingOutlet;
		}

		public static IList<Operator> GetConnectedOperators(this Operator op)
		{
			if (op == null) throw new NullException(() => op);

			IList<Operator> connectedOperators =
				// ReSharper disable once InvokeAsExtensionMethod
				Enumerable.Union(
					          op.Inlets.Where(x => x.InputOutlet != null).Select(x => x.InputOutlet.Operator),
					          op.Outlets.SelectMany(x => x.ConnectedInlets).Select(x => x.Operator))
				          .ToArray();

			return connectedOperators;
		}

		public static OperatorTypeEnum GetOperatorTypeEnum(this Operator op)
		{
			if (op == null) throw new NullException(() => op);

			Enum.TryParse(op.UnderlyingPatch?.Name, out OperatorTypeEnum operatorTypeEnum);

			return operatorTypeEnum;
		}

		/// <see cref="EnumerateOwnedOperators"/>
		public static bool IsOwned(this Operator possiblyOwnedOperator)
		{
			if (possiblyOwnedOperator == null) throw new NullException(() => possiblyOwnedOperator);

			if (possiblyOwnedOperator.Outlets.Count <= 0)
			{
				return false;
			}

			if (possiblyOwnedOperator.GetOperatorTypeEnum() != OperatorTypeEnum.Number)
			{
				return false;
			}

			// Make sure the connected inlets are all of the same operator.
			bool isOwned = possiblyOwnedOperator.Outlets.Single().ConnectedInlets.Select(x => x.Operator).Distinct().Count() == 1;

			return isOwned;
		}

		/// <see cref="EnumerateOwnedOperators"/>
		public static IList<Operator> GetOwnedOperators(this Operator op) => EnumerateOwnedOperators(op).ToArray();

		/// <summary> A Number Operator can be considered 'owned' by another operator if it is the only operator it is connected to. </summary>
		public static IEnumerable<Operator> EnumerateOwnedOperators(this Operator ownerOperator)
		{
			if (ownerOperator == null) throw new ArgumentNullException(nameof(ownerOperator));

			// Note that the owned operator can be connected to the same owner operator twice (in two different inlets).

			IEnumerable<Operator> ownedOperators = ownerOperator.Inlets
			                                                    .Select(x => x.InputOutlet?.Operator)
			                                                    .Where(x => x != null)
			                                                    .Where(x => x.IsOwned())
			                                                    .Distinct();
			return ownedOperators;
		}

		/// <summary> Moves owned operators along with the owner. </summary>
		public static void Move(this Operator op, float x, float y)
		{
			if (op == null) throw new NullException(() => op);
			if (op.EntityPosition == null) throw new NullException(() => op.EntityPosition);

			EntityPosition entityPosition = op.EntityPosition;

			float deltaX = x - entityPosition.X;
			float deltaY = y - entityPosition.Y;

			entityPosition.X += deltaX;
			entityPosition.Y += deltaY;

			// Move owned operators along with the owner.

			IEnumerable<Operator> ownedOperators = op.GetOwnedOperators();
			foreach (Operator ownedOperator in ownedOperators)
			{
				EntityPosition entityPosition2 = ownedOperator.EntityPosition;
				entityPosition2.X += deltaX;
				entityPosition2.Y += deltaY;
			}
		}
	}
}