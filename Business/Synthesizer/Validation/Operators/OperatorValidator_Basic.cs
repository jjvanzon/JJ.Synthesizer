using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.StringResources;
using JJ.Business.Synthesizer.Validation.DataProperty;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Resources;
using JJ.Framework.Validation;
using JJ.Framework.Validation.Resources;
// ReSharper disable SuggestBaseTypeForParameter

namespace JJ.Business.Synthesizer.Validation.Operators
{
	internal class OperatorValidator_Basic : VersatileValidator
	{
		public OperatorValidator_Basic(Operator op, IList<string> expectedDataKeys = null)
		{
			if (op == null) throw new NullException(() => op);

			expectedDataKeys = expectedDataKeys ?? new string[0];

			For(op.EntityPosition, ResourceFormatter.EntityPosition).NotNull();
			if (op.EntityPosition != null)
			{
				ExecuteValidator(new EntityPositionValidator(op.EntityPosition), ValidationHelper.GetMessagePrefix(op.EntityPosition));
			}

			ExecuteValidator(new IDValidator(op.ID));
			ExecuteValidator(new NameValidator(op.Name, required: false));
			ExecuteValidator(new DataPropertyValidator(op.Data, expectedDataKeys));
			ExecuteValidator(new DimensionInfoValidator(op.HasDimension, op.StandardDimension, op.CustomDimensionName));
			ExecuteValidator(new InletOrOutletListValidator(op.Inlets));
			ExecuteValidator(new InletOrOutletListValidator(op.Outlets));

			// ReSharper disable once InvertIf
			if (op.UnderlyingPatch != null)
			{
				ValidateUnderlyingPatchReferenceConstraint(op);
				ValidateHasDimensionAgainstUnderlyingPatch(op);
				ValidateInletsAgainstUnderlyingPatch(op);
				ValidateOutletsAgainstUnderlyingPatch(op);
			}
		}

		private void ValidateUnderlyingPatchReferenceConstraint(Operator op)
		{
			Patch underlyingPatch = op.UnderlyingPatch;

			// We are quite tolerant here: we omit the check if it is not in a patch or document.
			bool mustCheckReference = op.Patch?.Document != null;
			if (!mustCheckReference)
			{
				return;
			}

			bool isInList = op.Patch.Document
							  .GetPatchesAndVisibleLowerDocumentPatches()
							  .Any(x => x.ID == underlyingPatch.ID);
			if (!isInList)
			{
				Messages.AddNotInListMessage(ResourceFormatter.UnderlyingPatch, underlyingPatch.ID);
			}
		}

		private void ValidateHasDimensionAgainstUnderlyingPatch(Operator op)
		{
			if (op.UnderlyingPatch.HasDimension != op.HasDimension)
			{
				var sb = new StringBuilder();

				sb.Append(ResourceFormatter.MismatchWithUnderlyingPatch);
				sb.Append(Environment.NewLine);
				sb.Append($"{ResourceFormatter.Operator}: {ResourceFormatter.HasDimension} = {op.HasDimension}.");
				sb.Append(Environment.NewLine);
				sb.Append($"{ResourceFormatter.UnderlyingPatch}: {ResourceFormatter.HasDimension} = {op.UnderlyingPatch.HasDimension}.");

				Messages.Add(sb.ToString());
			}
		}

		private void ValidateInletsAgainstUnderlyingPatch(Operator op)
		{
			IList<InletTuple> tuples = InletOutletMatcher.MatchSourceAndDestInlets(op);
			
			foreach (InletTuple tuple in tuples)
			{
				Inlet sourceInlet = tuple.SourceInlet;
				Inlet destInlet = tuple.DestInlet;

				ValidateInletAgainstSource(sourceInlet, destInlet);
			}
		}

		/// <param name="sourceInlet">nullable</param>
		/// <param name="destInlet">nullable</param>
		private void ValidateInletAgainstSource(Inlet sourceInlet, Inlet destInlet)
		{
			ValidateIsObsolete(sourceInlet, destInlet);

			if (sourceInlet == null)
			{
				return;
			}

			if (destInlet == null)
			{
				return;
			}

			if (destInlet.Operator.GetOperatorTypeEnum() == OperatorTypeEnum.PatchInlet)
			{
				// Do not evaluate properties for PatchInlet.Inlet, since those are all custom filled in by the user.
				return;
			}

			if (!NameHelper.AreEqual(destInlet.Name, sourceInlet.Name))
			{
				string message = GetInletPropertyDoesNotMatchMessage(
					CommonResourceFormatter.Name,
					sourceInlet,
					destInlet,
					sourceInlet.Name,
					destInlet.Name);
				Messages.Add(message);
			}

			if (destInlet.GetDimensionEnum() != sourceInlet.GetDimensionEnum())
			{
				string message = GetInletPropertyDoesNotMatchMessage(
					ResourceFormatter.Dimension,
					sourceInlet,
					destInlet,
					sourceInlet.GetDimensionEnum(),
					destInlet.GetDimensionEnum());
				Messages.Add(message);
			}

			if (destInlet.Position != sourceInlet.Position)
			{
				string message = GetInletPropertyDoesNotMatchMessage(
					ResourceFormatter.Position,
					sourceInlet,
					destInlet,
					sourceInlet.Position,
					destInlet.Position);
				Messages.Add(message);
			}

			// ReSharper disable once CompareOfFloatsByEqualityOperator
			if (destInlet.DefaultValue != sourceInlet.DefaultValue)
			{
				string message = GetInletPropertyDoesNotMatchMessage(
					ResourceFormatter.DefaultValue,
					sourceInlet,
					destInlet,
					sourceInlet.DefaultValue,
					destInlet.DefaultValue);
				Messages.Add(message);
			}

			if (destInlet.WarnIfEmpty != sourceInlet.WarnIfEmpty)
			{
				string message = GetInletPropertyDoesNotMatchMessage(
					ResourceFormatter.WarnIfEmpty,
					sourceInlet,
					destInlet,
					sourceInlet.WarnIfEmpty,
					destInlet.WarnIfEmpty);
				Messages.Add(message);
			}

			if (destInlet.NameOrDimensionHidden != sourceInlet.NameOrDimensionHidden)
			{
				string message = GetInletPropertyDoesNotMatchMessage(
					ResourceFormatter.NameOrDimensionHidden,
					sourceInlet,
					destInlet,
					sourceInlet.NameOrDimensionHidden,
					destInlet.NameOrDimensionHidden);
				Messages.Add(message);
			}

			if (destInlet.IsRepeating != sourceInlet.IsRepeating)
			{
				string message = GetInletPropertyDoesNotMatchMessage(
					ResourceFormatter.IsRepeating,
					sourceInlet,
					destInlet,
					sourceInlet.IsRepeating,
					destInlet.IsRepeating);
				Messages.Add(message);
			}
		}

		/// <param name="sourceInlet">nullable</param>
		/// <param name="destInlet">nullable</param>
		private void ValidateIsObsolete(Inlet sourceInlet, Inlet destInlet)
		{
			if (destInlet == null)
			{
				return;
			}

			if (sourceInlet == null)
			{
				// ReSharper disable once InvertIf
				if (!destInlet.IsObsolete)
				{
					string messagePrefix = ValidationHelper.GetMessagePrefix(destInlet);
					string message = ValidationResourceFormatter.NotEqual(ResourceFormatter.IsObsolete, CommonResourceFormatter.True);
					Messages.Add(messagePrefix + message);
				}
			}
			else
			{
				// ReSharper disable once InvertIf
				if (destInlet.IsObsolete)
				{
					string messagePrefix = ValidationHelper.GetMessagePrefix(destInlet);
					string message = ValidationResourceFormatter.NotEqual(ResourceFormatter.IsObsolete, CommonResourceFormatter.False);
					Messages.Add(messagePrefix + message);
				}
			}
		}

		private void ValidateOutletsAgainstUnderlyingPatch(Operator customOperator)
		{
			IList<OutletTuple> tuples = InletOutletMatcher.MatchSourceAndDestOutlets(customOperator);

			foreach (OutletTuple tuple in tuples)
			{
				Outlet sourceOutlet = tuple.SourceOutlet;
				Outlet destOutlet = tuple.DestOutlet;

				ValidateOutletAgainstSource(sourceOutlet, destOutlet);
			}
		}

		/// <param name="sourceOutlet">nullable</param>
		/// <param name="destOutlet">nullable</param>
		private void ValidateOutletAgainstSource(Outlet sourceOutlet, Outlet destOutlet)
		{
			ValidateIsObsolete(sourceOutlet, destOutlet);

			if (sourceOutlet == null)
			{
				return;
			}

			if (destOutlet == null)
			{
				return;
			}

			if (destOutlet.Operator.GetOperatorTypeEnum() == OperatorTypeEnum.PatchOutlet)
			{
				// Do not evaluate properties for PatchOutlet.Outlet, since those are all custom filled in by the user.
				return;
			}

			if (!NameHelper.AreEqual(destOutlet.Name, sourceOutlet.Name))
			{
				string message = GetOutletPropertyDoesNotMatchMessage(
					CommonResourceFormatter.Name,
					sourceOutlet,
					destOutlet,
					sourceOutlet.Name,
					destOutlet.Name);
				Messages.Add(message);
			}

			if (destOutlet.GetDimensionEnum() != sourceOutlet.GetDimensionEnum())
			{
				string message = GetOutletPropertyDoesNotMatchMessage(
					ResourceFormatter.Dimension,
					sourceOutlet,
					destOutlet,
					sourceOutlet.GetDimensionEnum(),
					destOutlet.GetDimensionEnum());
				Messages.Add(message);
			}

			if (destOutlet.Position != sourceOutlet.Position)
			{
				string message = GetOutletPropertyDoesNotMatchMessage(
					ResourceFormatter.Position,
					sourceOutlet,
					destOutlet,
					sourceOutlet.Position,
					destOutlet.Position);
				Messages.Add(message);
			}

			if (destOutlet.NameOrDimensionHidden != sourceOutlet.NameOrDimensionHidden)
			{
				string message = GetOutletPropertyDoesNotMatchMessage(
					ResourceFormatter.NameOrDimensionHidden,
					sourceOutlet,
					destOutlet,
					sourceOutlet.NameOrDimensionHidden,
					destOutlet.NameOrDimensionHidden);
				Messages.Add(message);
			}

			if (destOutlet.IsRepeating != sourceOutlet.IsRepeating)
			{
				string message = GetOutletPropertyDoesNotMatchMessage(
					ResourceFormatter.IsRepeating,
					sourceOutlet,
					destOutlet,
					sourceOutlet.IsRepeating,
					destOutlet.IsRepeating);
				Messages.Add(message);
			}
		}

		/// <param name="sourceOutlet">nullable</param>
		private void ValidateIsObsolete(Outlet sourceOutlet, Outlet destOutlet)
		{
			if (destOutlet == null)
			{
				return;
			}

			if (sourceOutlet == null)
			{
				// ReSharper disable once InvertIf
				if (!destOutlet.IsObsolete)
				{
					string messagePrefix = ValidationHelper.GetMessagePrefix(destOutlet);
					string message = ValidationResourceFormatter.NotEqual(ResourceFormatter.IsObsolete, CommonResourceFormatter.True);
					Messages.Add(messagePrefix + message);
				}
			}
			else
			{
				// ReSharper disable once InvertIf
				if (destOutlet.IsObsolete)
				{
					string messagePrefix = ValidationHelper.GetMessagePrefix(destOutlet);
					string message = ValidationResourceFormatter.NotEqual(ResourceFormatter.IsObsolete, CommonResourceFormatter.False);
					Messages.Add(messagePrefix + message);
				}
			}
		}

		// Helpers

		private static string GetInletPropertyDoesNotMatchMessage(
			string propertyDisplayName,
			Inlet sourceInlet,
			Inlet destInlet,
			object sourceValue,
			object destValue)
		{
			// Mismatch between operator and underlying patch.
			// Operator: Inlet 'Signal': Number (0-based) = '0'.
			// Underlying Patch: Inlet 'Signal': Number (0-based) = '1'.

			var sb = new StringBuilder();

			sb.Append(ResourceFormatter.MismatchWithUnderlyingPatch);

			sb.Append(Environment.NewLine);

			string destInletIdentifier = ValidationHelper.GetUserFriendlyIdentifier(destInlet);
			sb.AppendFormat(
				"{0}: {1} {2}: {3} = '{4}'.",
				ResourceFormatter.Operator,
				ResourceFormatter.Inlet,
				destInletIdentifier,
				propertyDisplayName,
				destValue);

			sb.Append(Environment.NewLine);

			string sourceInletIdentifier = ValidationHelper.GetUserFriendlyIdentifier(sourceInlet);
			sb.AppendFormat(
				"{0}: {1} {2}: {3} = '{4}'.",
				ResourceFormatter.UnderlyingPatch,
				ResourceFormatter.PatchInlet,
				sourceInletIdentifier,
				propertyDisplayName,
				sourceValue);

			sb.AppendLine();

			return sb.ToString();
		}

		private static string GetOutletPropertyDoesNotMatchMessage(
			string propertyDisplayName,
			Outlet sourceOutlet,
			Outlet destOutlet,
			object sourceValue,
			object destValue)
		{
			// Mismatch between operator and underlying patch.
			// Operator: Outlet 'Signal': Number (0-based) = '0'.
			// Underlying Patch: Outlet 'Signal': Number (0-based) = '1'.

			var sb = new StringBuilder();

			sb.Append(ResourceFormatter.MismatchWithUnderlyingPatch);

			sb.Append(Environment.NewLine);

			string destOutletIdentifier = ValidationHelper.GetUserFriendlyIdentifier(destOutlet);
			sb.AppendFormat(
				"{0}: {1} '{2}': {3} = '{4}'.",
				ResourceFormatter.Operator,
				ResourceFormatter.Outlet,
				destOutletIdentifier,
				propertyDisplayName,
				destValue);

			sb.Append(Environment.NewLine);

			string sourceOutletIdentifier = ValidationHelper.GetUserFriendlyIdentifier(sourceOutlet);
			sb.AppendFormat(
				"{0}: {1} {2}: {3} = '{4}'.",
				ResourceFormatter.UnderlyingPatch,
				ResourceFormatter.PatchOutlet,
				sourceOutletIdentifier,
				propertyDisplayName,
				sourceValue);

			sb.AppendLine();

			return sb.ToString();
		}
	}
}
