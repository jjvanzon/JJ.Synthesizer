using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Validation;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Validation.Resources;
using JJ.Framework.Exceptions;
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

        private void ValidateInletsAgainstUnderlyingPatch(Operator customOperator)
        {
            IList<InletTuple> tuples = InletOutletMatcher.Match_PatchInlets_With_CustomOperatorInlets(customOperator);
            
            foreach (InletTuple tuple in tuples)
            {
                Inlet customOperatorInlet = tuple.CustomOperatorInlet;
                Operator underlyingPatchInlet = tuple.UnderlyingPatchInlet;

                ValidateIsObsolete(customOperatorInlet, underlyingPatchInlet);

                if (underlyingPatchInlet == null)
                {
                    // Obsolete CustomOperator Inlets are allowed.
                    continue;
                }

                Inlet underlyingPatchInlet_Inlet = TryGetInlet(underlyingPatchInlet);
                if (underlyingPatchInlet_Inlet == null)
                {
                    // Error tollerance, because it is a validator.
                    continue;
                }

                if (customOperatorInlet.ListIndex != underlyingPatchInlet_Inlet.ListIndex)
                {
                    string message = GetInletPropertyDoesNotMatchMessage(
                        ResourceFormatter.ListIndex,
                        customOperatorInlet,
                        underlyingPatchInlet,
                        customOperatorInlet.ListIndex,
                        underlyingPatchInlet_Inlet.ListIndex);
                    ValidationMessages.Add(nameof(Inlet), message);
                }

                if (!NameHelper.AreEqual(customOperatorInlet.Name, underlyingPatchInlet_Inlet.Name))
                {
                    string message = GetInletPropertyDoesNotMatchMessage(
                        CommonResourceFormatter.Name,
                        customOperatorInlet,
                        underlyingPatchInlet,
                        customOperatorInlet.Name,
                        underlyingPatchInlet_Inlet.Name);
                    ValidationMessages.Add(nameof(Inlet), message);
                }

                if (customOperatorInlet.GetDimensionEnum() != underlyingPatchInlet_Inlet.GetDimensionEnum())
                {
                    string message = GetInletPropertyDoesNotMatchMessage(
                        ResourceFormatter.Dimension,
                        customOperatorInlet,
                        underlyingPatchInlet,
                        customOperatorInlet.GetDimensionEnum(),
                        underlyingPatchInlet_Inlet.GetDimensionEnum());
                    ValidationMessages.Add(nameof(Inlet), message);
                }

                // ReSharper disable once CompareOfFloatsByEqualityOperator
                // ReSharper disable once InvertIf
                if (customOperatorInlet.DefaultValue != underlyingPatchInlet_Inlet.DefaultValue)
                {
                    string message = GetInletPropertyDoesNotMatchMessage(
                        ResourceFormatter.DefaultValue,
                        customOperatorInlet,
                        underlyingPatchInlet,
                        customOperatorInlet.DefaultValue,
                        underlyingPatchInlet_Inlet.DefaultValue);
                    ValidationMessages.Add(nameof(Inlet), message);
                }
            }
        }

        /// <param name="underlyingPatchInletOperator">nullable</param>
        private void ValidateIsObsolete(Inlet customOperatorInlet, Operator underlyingPatchInletOperator)
        {
            if (customOperatorInlet == null) throw new NullException(() => customOperatorInlet);

            if (underlyingPatchInletOperator == null)
            {
                // ReSharper disable once InvertIf
                if (!customOperatorInlet.IsObsolete)
                {
                    string messagePrefix = ValidationHelper.GetMessagePrefix(customOperatorInlet);
                    string message = ValidationResourceFormatter.NotEqual(ResourceFormatter.IsObsolete, CommonResourceFormatter.True);
                    ValidationMessages.Add(nameof(customOperatorInlet.IsObsolete), messagePrefix + message);
                }
            }
            else
            {
                // ReSharper disable once InvertIf
                if (customOperatorInlet.IsObsolete)
                {
                    string messagePrefix = ValidationHelper.GetMessagePrefix(customOperatorInlet);
                    string message = ValidationResourceFormatter.NotEqual(ResourceFormatter.IsObsolete, CommonResourceFormatter.False);
                    ValidationMessages.Add(nameof(customOperatorInlet.IsObsolete), messagePrefix + message);
                }
            }
        }

        private void ValidateOutletsAgainstUnderlyingPatch(Operator customOperator)
        {
            IList<OutletTuple> tuples = InletOutletMatcher.Match_PatchOutlets_With_CustomOperatorOutlets(customOperator);

            foreach (OutletTuple tuple in tuples)
            {
                Operator underlyingPatchOutlet = tuple.UnderlyingPatchOutlet;
                Outlet customOperatorOutlet = tuple.CustomOperatorOutlet;

                ValidateIsObsolete(customOperatorOutlet, underlyingPatchOutlet);

                if (underlyingPatchOutlet == null)
                {
                    // Obsolete CustomOperator Outlets are allowed.
                    continue;
                }

                Outlet underlyingPatchOutlet_Outlet = TryGetOutlet(underlyingPatchOutlet);
                if (underlyingPatchOutlet_Outlet == null)
                {
                    // Error tollerance, because it is a validator.
                    continue;
                }

                if (customOperatorOutlet.ListIndex != underlyingPatchOutlet_Outlet.ListIndex)
                {
                    string message = GetOutletPropertyDoesNotMatchMessage(
                        ResourceFormatter.ListIndex,
                        customOperatorOutlet,
                        underlyingPatchOutlet,
                        customOperatorOutlet.ListIndex,
                        underlyingPatchOutlet_Outlet.ListIndex);
                    ValidationMessages.Add(nameof(Outlet), message);
                }

                if (!NameHelper.AreEqual(customOperatorOutlet.Name, underlyingPatchOutlet_Outlet.Name))
                {
                    string message = GetOutletPropertyDoesNotMatchMessage(
                        CommonResourceFormatter.Name,
                        customOperatorOutlet,
                        underlyingPatchOutlet,
                        customOperatorOutlet.Name,
                        underlyingPatchOutlet.Name);
                    ValidationMessages.Add(nameof(Outlet), message);
                }

                // ReSharper disable once InvertIf
                if (customOperatorOutlet.GetDimensionEnum() != underlyingPatchOutlet_Outlet.GetDimensionEnum())
                {
                    string message = GetOutletPropertyDoesNotMatchMessage(
                        ResourceFormatter.Dimension,
                        customOperatorOutlet,
                        underlyingPatchOutlet,
                        customOperatorOutlet.GetDimensionEnum(),
                        underlyingPatchOutlet_Outlet.GetDimensionEnum());
                    ValidationMessages.Add(nameof(Outlet), message);
                }
            }
        }

        /// <param name="underlyingPatchOutlet">nullable</param>
        private void ValidateIsObsolete(Outlet customOperatorOutlet, Operator underlyingPatchOutlet)
        {
            if (customOperatorOutlet == null) throw new NullException(() => customOperatorOutlet);

            if (underlyingPatchOutlet == null)
            {
                // ReSharper disable once InvertIf
                if (!customOperatorOutlet.IsObsolete)
                {
                    string messagePrefix = ValidationHelper.GetMessagePrefix(customOperatorOutlet);
                    string message = ValidationResourceFormatter.NotEqual(ResourceFormatter.IsObsolete, CommonResourceFormatter.True);
                    ValidationMessages.Add(nameof(customOperatorOutlet.IsObsolete), messagePrefix + message);
                }
            }
            else
            {
                // ReSharper disable once InvertIf
                if (customOperatorOutlet.IsObsolete)
                {
                    string messagePrefix = ValidationHelper.GetMessagePrefix(customOperatorOutlet);
                    string message = ValidationResourceFormatter.NotEqual(ResourceFormatter.IsObsolete, CommonResourceFormatter.False);
                    ValidationMessages.Add(nameof(customOperatorOutlet.IsObsolete), messagePrefix + message);
                }
            }
        }

        // Helpers

        private Inlet TryGetInlet(Operator op) => op.Inlets.FirstOrDefault();

        private Outlet TryGetOutlet(Operator op) => op.Outlets.FirstOrDefault();

        private static string GetInletPropertyDoesNotMatchMessage(
            string propertyDisplayName,
            Inlet customOperatorInlet,
            Operator patchInlet,
            object customOperatorInletPropertyValue,
            object patchInletPropertyValue)
        {
            // Number (0-based) of inlet does not match with underlying patch.
            // Custom Operator: Inlet 'Signal': Number (0-based) = '0'.
            // Underlying Patch: Inlet 'Signal': Number (0-based) = '1'.

            var sb = new StringBuilder();

            sb.Append(ResourceFormatter.MismatchBetweenCustomOperatorAndUnderlyingPatch);

            string customOperatorInletIdentifier = ValidationHelper.GetUserFriendlyIdentifier(customOperatorInlet);
            sb.AppendFormat(
                " {0}: {1} {2}: {3} = '{4}'.",
                ResourceFormatter.CustomOperator,
                ResourceFormatter.Inlet,
                customOperatorInletIdentifier,
                propertyDisplayName,
                customOperatorInletPropertyValue);

            string patchInletIdentifier = ValidationHelper.GetUserFriendlyIdentifier_ForPatchInlet(patchInlet);

            sb.AppendFormat(
                "{0}: {1} {2}: {3} = '{4}'.",
                ResourceFormatter.UnderlyingPatch,
                ResourceFormatter.PatchInlet,
                patchInletIdentifier,
                propertyDisplayName,
                patchInletPropertyValue);

            return sb.ToString();
        }

        private static string GetOutletPropertyDoesNotMatchMessage(
            string propertyDisplayName,
            Outlet customOperatorOutlet,
            Operator patchOutlet,
            object customOperatorOutletPropertyValue,
            object patchOutletPropertyValue)
        {
            // Number (0-based) of outlet does not match with underlying patch.
            // Custom Operator: Outlet 'Signal': Number (0-based) = '0'.
            // Underlying Patch: Outlet 'Signal': Number (0-based) = '1'.

            var sb = new StringBuilder();

            sb.Append(ResourceFormatter.MismatchBetweenCustomOperatorAndUnderlyingPatch);

            string customOperatorOutletIdentifier = ValidationHelper.GetUserFriendlyIdentifier(customOperatorOutlet);
            sb.AppendFormat(
                " {0}: {1} '{2}': {3} = '{4}'.",
                ResourceFormatter.CustomOperator,
                ResourceFormatter.Outlet,
                customOperatorOutletIdentifier,
                propertyDisplayName,
                customOperatorOutletPropertyValue);

            string patchOutletIdentifier = ValidationHelper.GetUserFriendlyIdentifier_ForPatchOutlet(patchOutlet);
            sb.AppendFormat(
                "{0}: {1} {2}: {3} = '{4}'.",
                ResourceFormatter.UnderlyingPatch,
                ResourceFormatter.PatchOutlet,
                patchOutletIdentifier,
                propertyDisplayName,
                patchOutletPropertyValue);

            return sb.ToString();
        }
    }
}
