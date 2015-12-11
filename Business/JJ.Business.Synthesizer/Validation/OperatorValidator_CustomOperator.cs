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
using JJ.Business.Synthesizer.EntityWrappers;

namespace JJ.Business.Synthesizer.Validation
{
    /// <summary> Does not derive from OperatorValidator_Base, because CustomOperator has very specific requirements. </summary>
    internal class OperatorValidator_CustomOperator : FluentValidator<Operator>
    {
        private IPatchRepository _patchRepository;

        public OperatorValidator_CustomOperator(Operator op, IPatchRepository patchRepository)
            : base(op, postponeExecute: true)
        {
            _patchRepository = patchRepository;

            Execute();
        }

        protected override void Execute()
        {
            Operator op = Object;

            int i = 0;
            foreach (Inlet inlet in op.Inlets)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(inlet, i + 1);
                Execute(new InletValidator_ForCustomOperator(inlet), messagePrefix);
                i++;
            }

            i = 0;
            foreach (Outlet outlet in op.Outlets)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(outlet, i + 1);
                Execute(new OutletValidator_ForCustomOperator(outlet), messagePrefix);
                i++;
            }

            ValidateInletNamesUnique();
            ValidateOutletNamesUnique();

            For(() => op.Data, PropertyDisplayNames.Data).IsInteger();

            int underlyingPatchID;
            if (Int32.TryParse(op.Data, out underlyingPatchID))
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
                    ValidationMessages.Add(PropertyNames.UnderlyingPatch, MessageFormatter.NotFoundInList_WithItemName_AndID(PropertyDisplayNames.UnderlyingPatch, underlyingPatch.ID));
                }
            }
        }

        private void ValidateInletsAgainstUnderlyingPatch(Patch underlyingPatch)
        {
            Operator op = Object;

            IList<Operator> underlyingPatchInletOperators = underlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet);

            foreach (Operator underlyingPatchInletOperator in underlyingPatchInletOperators)
            {
                var wrapper = new PatchInlet_OperatorWrapper(underlyingPatchInletOperator);

                // Check Name Existence
                string expectedName = underlyingPatchInletOperator.Name;
                Inlet inletWithMatchingName = op.Inlets.Where(x => String.Equals(x.Name, expectedName)).FirstOrDefault();
                bool nameExists = inletWithMatchingName != null;
                if (!nameExists)
                {
                    ValidationMessages.Add(PropertyNames.Inlet, MessageFormatter.NotFound_WithTypeName_AndName(PropertyDisplayNames.Inlet, expectedName));
                }
                
                // Check DefaultValue equality
                if (inletWithMatchingName != null)
                {
                    if (inletWithMatchingName.DefaultValue != wrapper.DefaultValue)
                    {
                        ValidationMessages.Add(PropertyNames.Inlet, MessageFormatter.InletDefaultValueDoesNotMatchWithUnderlyingPatch(inletWithMatchingName.Name));
                    }
                }

                // Check InletTypeEnum Existence
                InletTypeEnum expectedInletTypeEnum = wrapper.InletTypeEnum;
                bool inletTypeEnumExists = op.Inlets.Where(x => x.GetInletTypeEnum() == expectedInletTypeEnum).Any();
                if (!inletTypeEnumExists)
                {
                    string inletTypeDisplayName = ResourceHelper.GetInletTypeDisplayName(expectedInletTypeEnum);
                    ValidationMessages.Add(PropertyNames.Inlet, MessageFormatter.InletTypeNotFoundInUnderlyingPatch(inletTypeDisplayName));
                }
            }
        }

        private void ValidateOutletsAgainstUnderlyingPatch(Patch underlyingPatch)
        {
            Operator op = Object;

            IList<Operator> underlyingPatchOutletOperators = underlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet);

            foreach (Operator underlyingPatchOutletOperator in underlyingPatchOutletOperators)
            {
                // Check Name Existence
                string name = underlyingPatchOutletOperator.Name;
                bool nameExists = op.Outlets.Where(x => String.Equals(x.Name, name)).Any();
                if (!nameExists)
                {
                    ValidationMessages.Add(PropertyNames.Outlet, MessageFormatter.NotFound_WithTypeName_AndName(PropertyDisplayNames.Outlet, name));
                }

                // Check OutletTypeEnum Existence
                var wrapper = new PatchOutlet_OperatorWrapper(underlyingPatchOutletOperator);
                OutletTypeEnum expectedOutletTypeEnum = wrapper.OutletTypeEnum;
                bool outletTypeEnumExists = op.Outlets.Where(x => x.GetOutletTypeEnum() == expectedOutletTypeEnum).Any();
                if (!outletTypeEnumExists)
                {
                    string outletTypeDisplayName = ResourceHelper.GetOutletTypeDisplayName(expectedOutletTypeEnum);
                    ValidationMessages.Add(PropertyNames.Outlet, MessageFormatter.OutletTypeNotFoundInUnderlyingPatch(outletTypeDisplayName));
                }
            }
        }
    }
}