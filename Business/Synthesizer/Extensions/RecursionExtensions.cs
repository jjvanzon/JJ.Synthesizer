using JJ.Framework.Exceptions;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Extensions
{
	public static class RecursionExtensions
	{
		/// <summary> Tells us whether an operator is circular within a patch. </summary>
		public static bool HasCircularInputOutput(this Operator op)
		{
			if (op == null) throw new NullException(() => op);

			var alreadyDone = new HashSet<Operator>();

			return HasCircularInputOutput(op, alreadyDone);
		}

		// ReSharper disable once SuggestBaseTypeForParameter
		private static bool HasCircularInputOutput(this Operator op, HashSet<Operator> alreadyDone)
		{
			// Be null-tolerant, because you might call it in places where the entities are not valid.
			if (op == null)
			{
				return false;
			}

			bool wasAlreadyAdded = !alreadyDone.Add(op);
			if (wasAlreadyAdded)
			{
				return true;
			}

			// ReSharper disable once LoopCanBeConvertedToQuery
			foreach (Inlet inlet in op.Inlets)
			{
				// ReSharper disable once InvertIf
				if (inlet.InputOutlet != null)
				{
					if (HasCircularInputOutput(inlet.InputOutlet.Operator, alreadyDone))
					{
						return true;
					}
				}
			}

			alreadyDone.Remove(op);

			return false;
		}

		public static bool HasInvalidCircularUnderlyingPatch(this Patch patch)
		{
			if (patch == null) throw new NullException(() => patch);

			return patch.HasInvalidCircularUnderlyingPatch(new HashSet<Patch>());
		}

		public static bool HasInvalidCircularUnderlyingPatch(this Operator op)
		{
			if (op == null) throw new NullException(() => op);

			return op.HasInvalidCircularUnderlyingPatch(new HashSet<Patch>());
		}

		private static bool HasInvalidCircularUnderlyingPatch(this Patch patch, HashSet<Patch> alreadyDone)
		{
			if (alreadyDone.Contains(patch))
			{
				return true;
			}
			alreadyDone.Add(patch);

			if (patch.Operators.Any(op => op.HasInvalidCircularUnderlyingPatch(alreadyDone)))
			{
				return true;
			}

			alreadyDone.Remove(patch);

			return false;
		}

		private static bool HasInvalidCircularUnderlyingPatch(this Operator op, HashSet<Patch> alreadyDone)
		{
			OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
			if (operatorTypeEnum == OperatorTypeEnum.PatchInlet ||
				operatorTypeEnum == OperatorTypeEnum.PatchOutlet)
			{
				// These operator types are allowed to have circular references.
				// (PatchInlet and PatchOutlet's system Patches are based on (yet again) PatchInlets and PatchOutlets.)
				return false;
			}

			if (op.UnderlyingPatch != null)
			{
				if (op.UnderlyingPatch.HasInvalidCircularUnderlyingPatch(alreadyDone))
				{
					return true;
				}
			}

			return false;
		}

		public static IEnumerable<Operator> EnumerateDerivedOperators(this Patch patch)
		{
			if (patch == null) throw new NullException(() => patch);

			// In case of no document, there are no derived operators.
			if (patch.Document == null)
			{
				return new Operator[0];
			}

			// We cannot use an SQL query, because that only operates on flushed / committed data.
			IEnumerable<Operator> enumerable =
				patch.Document
					 .GetPatchesAndHigherDocumentPatches()
					 .SelectMany(x => x.Operators)
					 .Where(x => x.UnderlyingPatch?.ID == patch.ID);

			return enumerable;
		}

		/// <summary> Should be same as patch.Operators, but in case of an invalid entity structure it might not be. </summary>
		public static IList<Operator> GetOperatorsRecursive(this Patch patch) => EnumerateOperatorsRecursive(patch).ToArray();

		/// <summary>  Should be same as patch.Operators, but in case of an invalid entity structure it might not be. </summary>
		public static IEnumerable<Operator> EnumerateOperatorsRecursive(this Patch patch)
		{
			var hashSet = new HashSet<Operator>();

			AddOperatorsInPatchRecursive(hashSet, patch);

			return hashSet;
		}

		private static void AddOperatorsInPatchRecursive(HashSet<Operator> hashSet, Patch patch)
		{
			if (patch == null) throw new NullException(() => patch);

			foreach (Operator op in patch.Operators)
			{
				AddOperatorsRecursive(hashSet, op);
			}
		}

		// ReSharper disable once SuggestBaseTypeForParameter
		private static void AddOperatorsRecursive(HashSet<Operator> hashSet, Operator op)
		{
			bool wasAlreadyAdded = !hashSet.Contains(op);
			if (wasAlreadyAdded)
			{
				return;
			}

			foreach (Inlet inlet in op.Inlets)
			{
				if (inlet.InputOutlet != null)
				{
					AddOperatorsRecursive(hashSet, inlet.InputOutlet.Operator);
				}
			}

			foreach (Outlet outlet in op.Outlets)
			{
				foreach (Inlet inlet in outlet.ConnectedInlets)
				{
					AddOperatorsRecursive(hashSet, inlet.InputOutlet.Operator);
				}
			}
		}
	}
}