using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Validation;
using JJ.Framework.Presentation.Resources;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Validation.DataProperty;
using JJ.Framework.Validation.Resources;
using JJ.Framework.Exceptions;
using System.Text;
using JetBrains.Annotations;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    /// <summary> Does not derive from OperatorValidator_Base, because CustomOperator has very specific requirements. </summary>
    internal class CustomOperator_OperatorValidator : VersatileValidator<Operator>
    {
        private static readonly string[] _allowedDataKeys = new[] { PropertyNames.UnderlyingPatchID };

        private readonly IPatchRepository _patchRepository;

        public CustomOperator_OperatorValidator([NotNull] Operator op, [NotNull] IPatchRepository patchRepository)
            : base(op, postponeExecute: true)
        {
            if (patchRepository == null) throw new NullException(() => patchRepository);

            _patchRepository = patchRepository;

            Execute();
        }

        protected sealed override void Execute()
        {
            Operator op = Obj;

            ValidateInletNamesUnique();
            ValidateOutletNamesUnique();

            ExecuteValidator(new DataPropertyValidator(op.Data, _allowedDataKeys));

            if (!DataPropertyParser.DataIsWellFormed(op))
            {
                return;
            }

            string underlyingPatchIDString = DataPropertyParser.TryGetString(op, PropertyNames.UnderlyingPatchID);

            For(() => underlyingPatchIDString, CommonResourceFormatter.ID_WithName(ResourceFormatter.UnderlyingPatch)).IsInteger();

            int underlyingPatchID;
            if (!int.TryParse(underlyingPatchIDString, out underlyingPatchID))
            {
                return;
            }

            Patch underlyingPatch = _patchRepository.TryGet(underlyingPatchID);
            if (underlyingPatch == null)
            {
                ValidationMessages.Add(() => underlyingPatch, CommonResourceFormatter.NotFound_WithName_AndID(ResourceFormatter.UnderlyingPatch, underlyingPatchID));
            }
            else
            {
                ValidateUnderlyingPatchReferenceConstraint(underlyingPatch);
                ValidateInletsAgainstUnderlyingPatch();
                ValidateOutletsAgainstUnderlyingPatch();
            }
        }

        private void ValidateInletNamesUnique()
        {
            IList<string> names = Obj.Inlets
                                     .Where(x => !string.IsNullOrEmpty(x.Name))
                                     .Select(x => x.Name)
                                     .ToArray();

            bool namesAreUnique = names.Distinct().Count() == names.Count;
            if (!namesAreUnique)
            {
                string message = ResourceFormatter.Inlets + ": " + ValidationResourceFormatter.NotUniquePlural(CommonResourceFormatter.Names);
                ValidationMessages.Add(PropertyNames.Inlets, message);
            }
        }

        private void ValidateOutletNamesUnique()
        {
            IList<string> names = Obj.Outlets.Where(x => !string.IsNullOrEmpty(x.Name))
                                                .Select(x => x.Name)
                                                .ToArray();

            bool namesAreUnique = names.Distinct().Count() == names.Count;
            if (!namesAreUnique)
            {
                string message = ResourceFormatter.Outlets + ": " + ValidationResourceFormatter.NotUniquePlural(CommonResourceFormatter.Names);
                ValidationMessages.Add(PropertyNames.Outlets, message);
            }
        }

        private void ValidateUnderlyingPatchReferenceConstraint(Patch underlyingPatch)
        {
            Operator op = Obj;

            // We are quite tollerant here: we omit the check if it is not in a patch or document.
            bool mustCheckReference = op.Patch?.Document != null;
            if (!mustCheckReference)
            {
                return;
            }

            bool isInList = op.Patch.Document.Patches.Any(x => x.ID == underlyingPatch.ID);
            if (!isInList)
            {
                ValidationMessages.AddNotInListMessage(PropertyNames.UnderlyingPatch, ResourceFormatter.UnderlyingPatch, underlyingPatch.ID);
            }
        }

        private void ValidateInletsAgainstUnderlyingPatch()
        {
            Operator customOperator = Obj;

            foreach (Inlet customOperatorInlet in customOperator.Inlets)
            {
                Operator underlyingPatchInletOperator = InletOutletMatcher.TryGetPatchInlet(customOperatorInlet, _patchRepository);

                ValidateIsObsolete(customOperatorInlet, underlyingPatchInletOperator);

                if (underlyingPatchInletOperator == null)
                {
                    // Obsolete CustomOperator Inlets are allowed.
                    continue;
                }

                int? underlyingPatchInlet_ListIndex = TryGetListIndex(underlyingPatchInletOperator);

                if (customOperatorInlet.ListIndex != underlyingPatchInlet_ListIndex)
                {
                    string message = GetInletPropertyDoesNotMatchMessage(
                        ResourceFormatter.ListIndex,
                        customOperatorInlet,
                        underlyingPatchInletOperator,
                        customOperatorInlet.ListIndex,
                        underlyingPatchInlet_ListIndex);
                    ValidationMessages.Add(PropertyNames.Inlet, message);
                }

                if (!string.Equals(customOperatorInlet.Name, underlyingPatchInletOperator.Name))
                {
                    string message = GetInletPropertyDoesNotMatchMessage(
                        CommonResourceFormatter.Name,
                        customOperatorInlet,
                        underlyingPatchInletOperator,
                        customOperatorInlet.Name,
                        underlyingPatchInletOperator.Name);
                    ValidationMessages.Add(PropertyNames.Inlet, message);
                }

                Inlet underlyingPatchInlet_Inlet = TryGetInlet(underlyingPatchInletOperator);
                // ReSharper disable once InvertIf
                if (underlyingPatchInlet_Inlet != null)
                {
                    if (customOperatorInlet.GetDimensionEnum() != underlyingPatchInlet_Inlet.GetDimensionEnum())
                    {
                        string message = GetInletPropertyDoesNotMatchMessage(
                            ResourceFormatter.Dimension,
                            customOperatorInlet,
                            underlyingPatchInletOperator,
                            customOperatorInlet.GetDimensionEnum(),
                            underlyingPatchInlet_Inlet.GetDimensionEnum());
                        ValidationMessages.Add(PropertyNames.Inlet, message);
                    }

                    // ReSharper disable once CompareOfFloatsByEqualityOperator
                    // ReSharper disable once InvertIf
                    if (customOperatorInlet.DefaultValue != underlyingPatchInlet_Inlet.DefaultValue)
                    {
                        string message = GetInletPropertyDoesNotMatchMessage(
                            ResourceFormatter.DefaultValue,
                            customOperatorInlet,
                            underlyingPatchInletOperator,
                            customOperatorInlet.DefaultValue,
                            underlyingPatchInlet_Inlet.DefaultValue);
                        ValidationMessages.Add(PropertyNames.Inlet, message);
                    }
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
                    ValidationMessages.Add(PropertyNames.IsObsolete, messagePrefix + message);
                }
            }
            else
            {
                // ReSharper disable once InvertIf
                if (customOperatorInlet.IsObsolete)
                {
                    string messagePrefix = ValidationHelper.GetMessagePrefix(customOperatorInlet);
                    string message = ValidationResourceFormatter.NotEqual(ResourceFormatter.IsObsolete, CommonResourceFormatter.False);
                    ValidationMessages.Add(PropertyNames.IsObsolete, messagePrefix + message);
                }
            }
        }

        private void ValidateOutletsAgainstUnderlyingPatch()
        {
            Operator customOperator = Obj;

            foreach (Outlet customOperatorOutlet in customOperator.Outlets)
            {
                Operator underlyingPatchOutlet = InletOutletMatcher.TryGetPatchOutlet(customOperatorOutlet, _patchRepository);

                ValidateIsObsolete(customOperatorOutlet, underlyingPatchOutlet);

                if (underlyingPatchOutlet == null)
                {
                    // Obsolete CustomOperator Outlets are allowed.
                    continue;
                }

                int? underlyingPatchOutlet_ListIndex = TryGetListIndex(underlyingPatchOutlet);

                if (customOperatorOutlet.ListIndex != underlyingPatchOutlet_ListIndex)
                {
                    string message = GetOutletPropertyDoesNotMatchMessage(
                        ResourceFormatter.ListIndex,
                        customOperatorOutlet,
                        underlyingPatchOutlet,
                        customOperatorOutlet.ListIndex,
                        underlyingPatchOutlet_ListIndex);
                    ValidationMessages.Add(PropertyNames.Outlet, message);
                }

                if (!string.Equals(customOperatorOutlet.Name, underlyingPatchOutlet.Name))
                {
                    string message = GetOutletPropertyDoesNotMatchMessage(
                        CommonResourceFormatter.Name,
                        customOperatorOutlet,
                        underlyingPatchOutlet,
                        customOperatorOutlet.Name,
                        underlyingPatchOutlet.Name);
                    ValidationMessages.Add(PropertyNames.Outlet, message);
                }

                Outlet underlyingPatchOutlet_Outlet = TryGetOutlet(underlyingPatchOutlet);
                // ReSharper disable once InvertIf
                if (underlyingPatchOutlet_Outlet != null)
                {
                    // ReSharper disable once InvertIf
                    if (customOperatorOutlet.GetDimensionEnum() != underlyingPatchOutlet_Outlet.GetDimensionEnum())
                    {
                        string message = GetOutletPropertyDoesNotMatchMessage(
                            ResourceFormatter.Dimension,
                            customOperatorOutlet,
                            underlyingPatchOutlet,
                            customOperatorOutlet.GetDimensionEnum(),
                            underlyingPatchOutlet_Outlet.GetDimensionEnum());
                        ValidationMessages.Add(PropertyNames.Outlet, message);
                    }
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
                    ValidationMessages.Add(PropertyNames.IsObsolete, messagePrefix + message);
                }
            }
            else
            {
                // ReSharper disable once InvertIf
                if (customOperatorOutlet.IsObsolete)
                {
                    string messagePrefix = ValidationHelper.GetMessagePrefix(customOperatorOutlet);
                    string message = ValidationResourceFormatter.NotEqual(ResourceFormatter.IsObsolete, CommonResourceFormatter.False);
                    ValidationMessages.Add(PropertyNames.IsObsolete, messagePrefix + message);
                }
            }
        }

        // Helpers

        private static int? TryGetListIndex(Operator patchInletOrPatchOutletOperator)
        {
            // ReSharper disable once InvertIf
            if (DataPropertyParser.DataIsWellFormed(patchInletOrPatchOutletOperator))
            {
                int? listIndex = DataPropertyParser.TryParseInt32(patchInletOrPatchOutletOperator, PropertyNames.ListIndex);
                return listIndex;
            }

            return null;
        }

        private Inlet TryGetInlet(Operator op)
        {
            return op.Inlets.FirstOrDefault();
        }

        private Outlet TryGetOutlet(Operator op)
        {
            return op.Outlets.FirstOrDefault();
        }

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

            sb.Append(ResourceFormatter.InletPropertyDoesNotMatchWithUnderlyingPatch(propertyDisplayName));

            string customOperatorInletIdentifier = ValidationHelper.GetIdentifier(customOperatorInlet);
            sb.AppendFormat(
                " {0}: {1} {2}: {3} = '{4}'.",
                ResourceFormatter.CustomOperator,
                ResourceFormatter.Inlet,
                customOperatorInletIdentifier,
                propertyDisplayName,
                customOperatorInletPropertyValue);

            string patchInletIdentifier = ValidationHelper.GetIdentifier_ForPatchInlet(patchInlet);

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

            sb.Append(ResourceFormatter.OutletPropertyDoesNotMatchWithUnderlyingPatch(propertyDisplayName));

            string customOperatorOutletIdentifier = ValidationHelper.GetIdentifier(customOperatorOutlet);
            sb.AppendFormat(
                " {0}: {1} '{2}': {3} = '{4}'.",
                ResourceFormatter.CustomOperator,
                ResourceFormatter.Outlet,
                customOperatorOutletIdentifier,
                propertyDisplayName,
                customOperatorOutletPropertyValue);

            string patchOutletIdentifier = ValidationHelper.GetIdentifier_ForPatchOutlet(patchOutlet);
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
