using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Validation;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Validation.Resources;
using System.Text;
using JJ.Business.Synthesizer.Validation.DataProperty;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal abstract class OperatorValidator_Base_WithUnderlyingPatch : VersatileValidator
    {
        public OperatorValidator_Base_WithUnderlyingPatch(Operator op, IList<string> expectedDataKeys = null)
        {
            if (op == null) throw new NullException(() => op);

            expectedDataKeys = expectedDataKeys ?? new string[0];

            ExecuteValidator(new DataPropertyValidator(op.Data, expectedDataKeys));
            ExecuteValidator(new DimensionInfoValidator(op.HasDimension, op.StandardDimension, op.CustomDimensionName));
            ExecuteValidator(new InletOrOutletListValidator_WithUnderlyingPatch(op.Inlets));
            ExecuteValidator(new InletOrOutletListValidator_WithUnderlyingPatch(op.Outlets));

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
                ValidationMessages.AddNotInListMessage(nameof(Operator.UnderlyingPatch), ResourceFormatter.UnderlyingPatch, underlyingPatch.ID);
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

                ValidationMessages.Add(nameof(Operator.HasDimension), sb.ToString());
            }
        }

        private void ValidateInletsAgainstUnderlyingPatch(Operator op)
        {
            IList<InletTuple> tuples = InletOutletMatcher.MatchSourceAndDestInlets(op);
            
            foreach (InletTuple tuple in tuples)
            {
                Inlet sourceInlet = tuple.SourceInlet;
                Inlet destInlet = tuple.DestInlet;

                ValidateIsObsolete(sourceInlet, destInlet);

                if (sourceInlet == null)
                {
                    continue;
                }

                if (destInlet == null)
                {
                    continue;
                }

                if (destInlet.Position != sourceInlet.Position)
                {
                    string message = GetInletPropertyDoesNotMatchMessage(
                        ResourceFormatter.Position,
                        sourceInlet,
                        destInlet,
                        sourceInlet.Position,
                        destInlet.Position);
                    ValidationMessages.Add(nameof(Inlet), message);
                }

                if (!NameHelper.AreEqual(destInlet.Name, sourceInlet.Name))
                {
                    string message = GetInletPropertyDoesNotMatchMessage(
                        CommonResourceFormatter.Name,
                        sourceInlet,
                        destInlet,
                        sourceInlet.Name,
                        destInlet.Name);
                    ValidationMessages.Add(nameof(Inlet), message);
                }

                if (destInlet.GetDimensionEnum() != sourceInlet.GetDimensionEnum())
                {
                    string message = GetInletPropertyDoesNotMatchMessage(
                        ResourceFormatter.Dimension,
                        sourceInlet,
                        destInlet,
                        sourceInlet.GetDimensionEnum(),
                        destInlet.GetDimensionEnum());
                    ValidationMessages.Add(nameof(Inlet), message);
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
                    ValidationMessages.Add(nameof(Inlet), message);
                }

                if (destInlet.WarnIfEmpty != sourceInlet.WarnIfEmpty)
                {
                    string message = GetInletPropertyDoesNotMatchMessage(
                        ResourceFormatter.WarnIfEmpty,
                        sourceInlet,
                        destInlet,
                        sourceInlet.WarnIfEmpty,
                        destInlet.WarnIfEmpty);
                    ValidationMessages.Add(nameof(Inlet), message);
                }

                if (destInlet.NameOrDimensionHidden != sourceInlet.NameOrDimensionHidden)
                {
                    string message = GetInletPropertyDoesNotMatchMessage(
                        ResourceFormatter.NameOrDimensionHidden,
                        sourceInlet,
                        destInlet,
                        sourceInlet.NameOrDimensionHidden,
                        destInlet.NameOrDimensionHidden);
                    ValidationMessages.Add(nameof(Inlet), message);
                }

                if (destInlet.IsRepeating != sourceInlet.IsRepeating)
                {
                    string message = GetInletPropertyDoesNotMatchMessage(
                        ResourceFormatter.IsRepeating,
                        sourceInlet,
                        destInlet,
                        sourceInlet.IsRepeating,
                        destInlet.IsRepeating);
                    ValidationMessages.Add(nameof(Inlet), message);
                }
            }
        }

        /// <param name="sourceInlet">nullable</param>
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
                    ValidationMessages.Add(nameof(destInlet.IsObsolete), messagePrefix + message);
                }
            }
            else
            {
                // ReSharper disable once InvertIf
                if (destInlet.IsObsolete)
                {
                    string messagePrefix = ValidationHelper.GetMessagePrefix(destInlet);
                    string message = ValidationResourceFormatter.NotEqual(ResourceFormatter.IsObsolete, CommonResourceFormatter.False);
                    ValidationMessages.Add(nameof(destInlet.IsObsolete), messagePrefix + message);
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

                ValidateIsObsolete(sourceOutlet, destOutlet);

                if (sourceOutlet == null)
                {
                    continue;
                }

                if (destOutlet == null)
                {
                    continue;
                }

                if (destOutlet.Position != sourceOutlet.Position)
                {
                    string message = GetOutletPropertyDoesNotMatchMessage(
                        ResourceFormatter.Position,
                        sourceOutlet,
                        destOutlet,
                        sourceOutlet.Position,
                        destOutlet.Position);
                    ValidationMessages.Add(nameof(Outlet), message);
                }

                if (!NameHelper.AreEqual(destOutlet.Name, sourceOutlet.Name))
                {
                    string message = GetOutletPropertyDoesNotMatchMessage(
                        CommonResourceFormatter.Name,
                        sourceOutlet,
                        destOutlet,
                        sourceOutlet.Name,
                        destOutlet.Name);
                    ValidationMessages.Add(nameof(Outlet), message);
                }

                if (destOutlet.GetDimensionEnum() != sourceOutlet.GetDimensionEnum())
                {
                    string message = GetOutletPropertyDoesNotMatchMessage(
                        ResourceFormatter.Dimension,
                        sourceOutlet,
                        destOutlet,
                        sourceOutlet.GetDimensionEnum(),
                        destOutlet.GetDimensionEnum());
                    ValidationMessages.Add(nameof(Outlet), message);
                }

                if (destOutlet.NameOrDimensionHidden != sourceOutlet.NameOrDimensionHidden)
                {
                    string message = GetOutletPropertyDoesNotMatchMessage(
                        ResourceFormatter.NameOrDimensionHidden,
                        sourceOutlet,
                        destOutlet,
                        sourceOutlet.NameOrDimensionHidden,
                        destOutlet.NameOrDimensionHidden);
                    ValidationMessages.Add(nameof(Outlet), message);
                }

                if (destOutlet.IsRepeating != sourceOutlet.IsRepeating)
                {
                    string message = GetOutletPropertyDoesNotMatchMessage(
                        ResourceFormatter.IsRepeating,
                        sourceOutlet,
                        destOutlet,
                        sourceOutlet.IsRepeating,
                        destOutlet.IsRepeating);
                    ValidationMessages.Add(nameof(Outlet), message);
                }
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
                    ValidationMessages.Add(nameof(destOutlet.IsObsolete), messagePrefix + message);
                }
            }
            else
            {
                // ReSharper disable once InvertIf
                if (destOutlet.IsObsolete)
                {
                    string messagePrefix = ValidationHelper.GetMessagePrefix(destOutlet);
                    string message = ValidationResourceFormatter.NotEqual(ResourceFormatter.IsObsolete, CommonResourceFormatter.False);
                    ValidationMessages.Add(nameof(destOutlet.IsObsolete), messagePrefix + message);
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

            return sb.ToString();
        }
    }
}
