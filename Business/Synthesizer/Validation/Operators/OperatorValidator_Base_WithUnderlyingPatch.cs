using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Validation;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Validation.Resources;
using System.Text;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    /// <summary> Does not derive from OperatorValidator_Base, because it has very specific requirements. </summary>
    internal abstract class OperatorValidator_Base_WithUnderlyingPatch : VersatileValidator<Operator>
    {
        public OperatorValidator_Base_WithUnderlyingPatch(Operator op)
            : base(op)
        { 
            For(() => op.Data, ResourceFormatter.Data).IsNullOrEmpty();

            if (op.UnderlyingPatch == null)
            {
                return;
            }

            ValidateUnderlyingPatchReferenceConstraint(op);
            ValidateInletsAgainstUnderlyingPatch(op);
            ValidateOutletsAgainstUnderlyingPatch(op);
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

            bool isInList = op.Patch.Document.GetPatchesAndVisibleLowerDocumentPatches().Any(x => x.ID == underlyingPatch.ID);
            
            if (!isInList)
            {
                ValidationMessages.AddNotInListMessage(nameof(Operator.UnderlyingPatch), ResourceFormatter.UnderlyingPatch, underlyingPatch.ID);
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

                if (destInlet.ListIndex != sourceInlet.ListIndex)
                {
                    string message = GetInletPropertyDoesNotMatchMessage(
                        ResourceFormatter.ListIndex,
                        sourceInlet,
                        destInlet,
                        sourceInlet.ListIndex,
                        destInlet.ListIndex);
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

                // ReSharper disable once InvertIf
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

                if (destOutlet.ListIndex != sourceOutlet.ListIndex)
                {
                    string message = GetOutletPropertyDoesNotMatchMessage(
                        ResourceFormatter.ListIndex,
                        sourceOutlet,
                        destOutlet,
                        sourceOutlet.ListIndex,
                        destOutlet.ListIndex);
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

                // ReSharper disable once InvertIf
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

            sb.Append(ResourceFormatter.MismatchBetweenOperatorAndUnderlyingPatch);

            sb.Append(' ');

            string destInletIdentifier = ValidationHelper.GetUserFriendlyIdentifier(destInlet);
            sb.AppendFormat(
                "{0}: {1} {2}: {3} = '{4}'.",
                ResourceFormatter.Operator,
                ResourceFormatter.Inlet,
                destInletIdentifier,
                propertyDisplayName,
                destValue);

            sb.Append(' ');

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

            sb.Append(ResourceFormatter.MismatchBetweenOperatorAndUnderlyingPatch);

            sb.Append(' ');

            string destOutletIdentifier = ValidationHelper.GetUserFriendlyIdentifier(destOutlet);
            sb.AppendFormat(
                "{0}: {1} '{2}': {3} = '{4}'.",
                ResourceFormatter.Operator,
                ResourceFormatter.Outlet,
                destOutletIdentifier,
                propertyDisplayName,
                destValue);

            sb.Append(' ');

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
