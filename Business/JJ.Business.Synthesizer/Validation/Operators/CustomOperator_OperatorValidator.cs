using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Common;
using JJ.Framework.Validation;
using JJ.Framework.Presentation.Resources;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Validation.DataProperty;
using JJ.Framework.Validation.Resources;
using JJ.Framework.Reflection.Exceptions;
using System.Text;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    /// <summary> Does not derive from OperatorValidator_Base, because CustomOperator has very specific requirements. </summary>
    internal class CustomOperator_OperatorValidator : FluentValidator<Operator>
    {
        private static readonly string[] _allowedDataKeys = new string[] { PropertyNames.UnderlyingPatchID };

        private readonly IPatchRepository _patchRepository;

        public CustomOperator_OperatorValidator(Operator op, IPatchRepository patchRepository)
            : base(op, postponeExecute: true)
        {
            _patchRepository = patchRepository;

            Execute();
        }

        protected override void Execute()
        {
            Operator op = Object;

            ValidateInletNamesUnique();
            ValidateOutletNamesUnique();

            ExecuteValidator(new DataPropertyValidator(op.Data, _allowedDataKeys));

            if (DataPropertyParser.DataIsWellFormed(op))
            {
                string underlyingPatchIDString = DataPropertyParser.TryGetString(op, PropertyNames.UnderlyingPatchID);

                For(() => underlyingPatchIDString, PropertyDisplayNames.UnderlyingPatchID).IsInteger();

                int underlyingPatchID;
                if (Int32.TryParse(underlyingPatchIDString, out underlyingPatchID))
                {
                    Patch underlyingPatch = _patchRepository.TryGet(underlyingPatchID);
                    if (underlyingPatch == null)
                    {
                        ValidationMessages.Add(() => underlyingPatch, CommonMessageFormatter.ObjectNotFoundWithID(PropertyDisplayNames.UnderlyingPatch, underlyingPatchID));
                    }
                    else
                    {
                        ValidateUnderlyingPatchReferenceConstraint(underlyingPatch);
                        ValidateInletsAgainstUnderlyingPatch(underlyingPatch);
                        ValidateOutletsAgainstUnderlyingPatch(underlyingPatch);
                    }
                }
            }
        }

        private void ValidateInletNamesUnique()
        {
            IList<string> names = Object.Inlets.Where(x => !String.IsNullOrEmpty(x.Name))
                                               .Select(x => x.Name)
                                               .ToArray();

            bool namesAreUnique = names.Distinct().Count() == names.Count;
            if (!namesAreUnique)
            {
                ValidationMessages.Add(PropertyNames.Inlets, Messages.InletNamesAreNotUnique);
            }
        }

        private void ValidateOutletNamesUnique()
        {
            IList<string> names = Object.Outlets.Where(x => !String.IsNullOrEmpty(x.Name))
                                                .Select(x => x.Name)
                                                .ToArray();

            bool namesAreUnique = names.Distinct().Count() == names.Count;
            if (!namesAreUnique)
            {
                ValidationMessages.Add(PropertyNames.Outlets, Messages.OutletNamesAreNotUnique);
            }
        }

        private void ValidateUnderlyingPatchReferenceConstraint(Patch underlyingPatch)
        {
            Operator op = Object;

            // We are quite tollerant here: we omit the check if it is not in a patch or document.
            bool mustCheckReference = op.Patch != null && op.Patch.Document != null;
            if (mustCheckReference)
            {
                IList<Document> documents;
                if (op.Patch.Document.ParentDocument != null)
                {
                    documents = op.Patch.Document.ParentDocument.ChildDocuments;
                }
                else
                {
                    documents = op.Patch.Document.ChildDocuments;
                }

                bool isInList = documents.SelectMany(x => x.Patches).Any(x => x.ID == underlyingPatch.ID);
                if (!isInList)
                {
                    ValidationMessages.AddNotInListMessage(PropertyNames.UnderlyingPatch, PropertyDisplayNames.UnderlyingPatch, underlyingPatch.ID);
                }
            }
        }

        private void ValidateInletsAgainstUnderlyingPatch(Patch underlyingPatch)
        {
            Operator customOperator = Object;

            // TODO:
            // Warning CA1804  'CustomOperator_OperatorValidator.ValidateInletsAgainstUnderlyingPatch(Patch)' declares a variable, 'underlyingPatchInletOperators', of type 'IList<Operator>', which is never used or is only assigned to. Use this variable or remove it.
            // You could make things faster by using this collection with InletOutletMatcher.TryGetPatchInlet.
            IList<Operator> underlyingPatchInletOperators = underlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet);

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
                        PropertyDisplayNames.ListIndex,
                        customOperatorInlet,
                        underlyingPatchInletOperator,
                        customOperatorInlet.ListIndex,
                        underlyingPatchInlet_ListIndex);
                    ValidationMessages.Add(PropertyNames.Inlet, message);
                }

                if (!String.Equals(customOperatorInlet.Name, underlyingPatchInletOperator.Name))
                {
                    string message = GetInletPropertyDoesNotMatchMessage(
                        CommonTitles.Name,
                        customOperatorInlet,
                        underlyingPatchInletOperator,
                        customOperatorInlet.Name,
                        underlyingPatchInletOperator.Name);
                    ValidationMessages.Add(PropertyNames.Inlet, message);
                }

                Inlet underlyingPatchInlet_Inlet = TryGetInlet(underlyingPatchInletOperator);
                if (underlyingPatchInlet_Inlet != null)
                {
                    if (customOperatorInlet.GetDimensionEnum() != underlyingPatchInlet_Inlet.GetDimensionEnum())
                    {
                        string message = GetInletPropertyDoesNotMatchMessage(
                            PropertyDisplayNames.Dimension,
                            customOperatorInlet,
                            underlyingPatchInletOperator,
                            customOperatorInlet.GetDimensionEnum(),
                            underlyingPatchInlet_Inlet.GetDimensionEnum());
                        ValidationMessages.Add(PropertyNames.Inlet, message);
                    }

                    if (customOperatorInlet.DefaultValue != underlyingPatchInlet_Inlet.DefaultValue)
                    {
                        string message = GetInletPropertyDoesNotMatchMessage(
                            PropertyDisplayNames.DefaultValue,
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
                if (!customOperatorInlet.IsObsolete)
                {
                    string messagePrefix = ValidationHelper.GetMessagePrefix(customOperatorInlet);
                    string message = ValidationMessageFormatter.NotEqual(PropertyDisplayNames.IsObsolete, CommonTitles.True);
                    ValidationMessages.Add(PropertyNames.IsObsolete, messagePrefix + message);
                }
            }
            else
            {
                if (customOperatorInlet.IsObsolete)
                {
                    string messagePrefix = ValidationHelper.GetMessagePrefix(customOperatorInlet);
                    string message = ValidationMessageFormatter.NotEqual(PropertyDisplayNames.IsObsolete, CommonTitles.False);
                    ValidationMessages.Add(PropertyNames.IsObsolete, messagePrefix + message);
                }
            }
        }

        private void ValidateOutletsAgainstUnderlyingPatch(Patch underlyingPatch)
        {
            Operator customOperator = Object;

            // TODO:
            // Warning CA1804  'CustomOperator_OperatorValidator.ValidateOutletsAgainstUnderlyingPatch(Patch)' declares a variable, 'underlyingPatchOutletOperators', of type 'IList<Operator>', which is never used or is only assigned to. Use this variable or remove it.
            // You could make things faster by using this collection with InletOutletMatcher.TryGetPatchInlet.
            IList<Operator> underlyingPatchOutletOperators = underlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet);

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
                        PropertyDisplayNames.ListIndex,
                        customOperatorOutlet,
                        underlyingPatchOutlet,
                        customOperatorOutlet.ListIndex,
                        underlyingPatchOutlet_ListIndex);
                    ValidationMessages.Add(PropertyNames.Outlet, message);
                }

                if (!String.Equals(customOperatorOutlet.Name, underlyingPatchOutlet.Name))
                {
                    string message = GetOutletPropertyDoesNotMatchMessage(
                        CommonTitles.Name,
                        customOperatorOutlet,
                        underlyingPatchOutlet,
                        customOperatorOutlet.Name,
                        underlyingPatchOutlet.Name);
                    ValidationMessages.Add(PropertyNames.Outlet, message);
                }

                Outlet underlyingPatchOutlet_Outlet = TryGetOutlet(underlyingPatchOutlet);
                if (underlyingPatchOutlet_Outlet != null)
                {
                    if (customOperatorOutlet.GetDimensionEnum() != underlyingPatchOutlet_Outlet.GetDimensionEnum())
                    {
                        string message = GetOutletPropertyDoesNotMatchMessage(
                            PropertyDisplayNames.Dimension,
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
                if (!customOperatorOutlet.IsObsolete)
                {
                    string messagePrefix = ValidationHelper.GetMessagePrefix(customOperatorOutlet);
                    string message = ValidationMessageFormatter.NotEqual(PropertyDisplayNames.IsObsolete, CommonTitles.True);
                    ValidationMessages.Add(PropertyNames.IsObsolete, messagePrefix + message);
                }
            }
            else
            {
                if (customOperatorOutlet.IsObsolete)
                {
                    string messagePrefix = ValidationHelper.GetMessagePrefix(customOperatorOutlet);
                    string message = ValidationMessageFormatter.NotEqual(PropertyDisplayNames.IsObsolete, CommonTitles.False);
                    ValidationMessages.Add(PropertyNames.IsObsolete, messagePrefix + message);
                }
            }
        }

        // Helpers

        private static int? TryGetListIndex(Operator patchInletOrPatchOutletOperator)
        {
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

            sb.Append(MessageFormatter.InletPropertyDoesNotMatchWithUnderlyingPatch(propertyDisplayName));

            string customOperatorInletIdentifier = ValidationHelper.GetInletIdentifier(customOperatorInlet);
            sb.AppendFormat(
                " {0}: {1} '{2}': {3} = '{4}'.",
                PropertyDisplayNames.CustomOperator,
                PropertyDisplayNames.Inlet,
                customOperatorInletIdentifier,
                propertyDisplayName,
                customOperatorInletPropertyValue);

            string patchInletIdentifier = ValidationHelper.GetOperatorIdentifier(patchInlet);

            sb.AppendFormat(
                "{0}: {1} '{2}': {3} = '{4}'.",
                PropertyDisplayNames.UnderlyingPatch,
                PropertyDisplayNames.PatchInlet,
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

            sb.Append(MessageFormatter.OutletPropertyDoesNotMatchWithUnderlyingPatch(propertyDisplayName));

            string customOperatorOutletIdentifier = ValidationHelper.GetOutletIdentifier(customOperatorOutlet);
            sb.AppendFormat(
                " {0}: {1} '{2}': {3} = '{4}'.",
                PropertyDisplayNames.CustomOperator,
                PropertyDisplayNames.Outlet,
                customOperatorOutletIdentifier,
                propertyDisplayName,
                customOperatorOutletPropertyValue);

            string patchOutletIdentifier = ValidationHelper.GetOperatorIdentifier(patchOutlet);
            sb.AppendFormat(
                "{0}: {1} '{2}': {3} = '{4}'.",
                PropertyDisplayNames.UnderlyingPatch,
                PropertyDisplayNames.PatchOutlet,
                patchOutletIdentifier,
                propertyDisplayName,
                patchOutletPropertyValue);

            return sb.ToString();
        }
    }
}
