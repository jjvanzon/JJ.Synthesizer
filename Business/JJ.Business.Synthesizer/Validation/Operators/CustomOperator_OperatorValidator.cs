﻿using System;
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

            Execute(new OperatorValidator_Data(op, _allowedDataKeys));

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
                    ValidationMessages.Add(PropertyNames.UnderlyingPatch, MessageFormatter.NotFoundInList_WithItemName_AndID(PropertyDisplayNames.UnderlyingPatch, underlyingPatch.ID));
                }
            }
        }

        private void ValidateInletsAgainstUnderlyingPatch(Patch underlyingPatch)
        {
            Operator customOperator = Object;

            IList<Operator> underlyingPatchInletOperators = underlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet);

            foreach (Inlet customOperatorInlet in customOperator.Inlets)
            {
                Operator underlyingPatchInletOperator = InletOutletMatcher.TryGetPatchInlet(customOperatorInlet, _patchRepository);

                if (underlyingPatchInletOperator == null)
                {
                    string message = MessageFormatter.InletNotFoundInUnderlyingPatch(
                        customOperatorInlet.Name,
                        ResourceHelper.GetDisplayName(customOperatorInlet.GetInletTypeEnum()),
                        customOperatorInlet.ListIndex);

                    ValidationMessages.Add(PropertyNames.Inlet, message);
                }
                else
                {
                    var underlyingPatchInletWrapper = new PatchInlet_OperatorWrapper(underlyingPatchInletOperator);

                    int? underlyingPatchInlet_ListIndex = TryGetListIndex(underlyingPatchInletOperator);

                    if (customOperatorInlet.ListIndex != underlyingPatchInlet_ListIndex)
                    {
                        string message = GetInletPropertyDoesNotMatchMessage(customOperatorInlet, PropertyDisplayNames.ListIndex);
                        ValidationMessages.Add(PropertyNames.Inlet, message);
                    }

                    if (!String.Equals(customOperatorInlet.Name, underlyingPatchInletOperator.Name))
                    {
                        string message = GetInletPropertyDoesNotMatchMessage(customOperatorInlet, CommonTitles.Name);
                        ValidationMessages.Add(PropertyNames.Inlet, message);
                    }

                    if (customOperatorInlet.GetInletTypeEnum() != underlyingPatchInletWrapper.Inlet.GetInletTypeEnum())
                    {
                        string message = GetInletPropertyDoesNotMatchMessage(customOperatorInlet, PropertyDisplayNames.InletType);
                        ValidationMessages.Add(PropertyNames.Inlet, message);
                    }

                    if (customOperatorInlet.DefaultValue != underlyingPatchInletWrapper.Inlet.DefaultValue)
                    {
                        string message = GetInletPropertyDoesNotMatchMessage(customOperatorInlet, PropertyDisplayNames.DefaultValue);
                        ValidationMessages.Add(PropertyNames.Inlet, message);
                    }
                }
            }
        }

        private static int? TryGetListIndex(Operator patchInletOrPatchOutletOperator)
        {
            string listIndex_String = DataPropertyParser.TryGetString(patchInletOrPatchOutletOperator, PropertyNames.ListIndex);

            if (!String.IsNullOrEmpty(listIndex_String))
            {
                int listIndex;
                if (Int32.TryParse(listIndex_String, out listIndex))
                {
                    return listIndex;
                }
            }

            return null;
        }

        private void ValidateOutletsAgainstUnderlyingPatch(Patch underlyingPatch)
        {
            Operator customOperator = Object;

            IList<Operator> underlyingPatchOutletOperators = underlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet);

            foreach (Outlet customOperatorOutlet in customOperator.Outlets)
            {
                Operator underlyingPatchOutlet = InletOutletMatcher.TryGetPatchOutlet(customOperatorOutlet, _patchRepository);
                if (underlyingPatchOutlet == null)
                {
                    string message = MessageFormatter.OutletNotFoundInUnderlyingPatch(
                        customOperatorOutlet.Name,
                        ResourceHelper.GetDisplayName(customOperatorOutlet.GetOutletTypeEnum()),
                        customOperatorOutlet.ListIndex);

                    ValidationMessages.Add(PropertyNames.Outlet, message);
                }
                else
                {
                    var underlyingPatchOutletWrapper = new PatchOutlet_OperatorWrapper(underlyingPatchOutlet);

                    int? underlyingPatchOutlet_ListIndex = TryGetListIndex(underlyingPatchOutlet);

                    if (customOperatorOutlet.ListIndex != underlyingPatchOutlet_ListIndex)
                    {
                        string message = GetOutletPropertyDoesNotMatchMessage(underlyingPatchOutletWrapper, PropertyDisplayNames.ListIndex);
                        ValidationMessages.Add(PropertyNames.Outlet, message);
                    }

                    if (!String.Equals(customOperatorOutlet.Name, underlyingPatchOutlet.Name))
                    {
                        string message = GetOutletPropertyDoesNotMatchMessage(underlyingPatchOutletWrapper, CommonTitles.Name);
                        ValidationMessages.Add(PropertyNames.Outlet, message);
                    }

                    if (customOperatorOutlet.GetOutletTypeEnum() != underlyingPatchOutletWrapper.Result.GetOutletTypeEnum())
                    {
                        string message = GetOutletPropertyDoesNotMatchMessage(underlyingPatchOutletWrapper, PropertyDisplayNames.OutletType);
                        ValidationMessages.Add(PropertyNames.Outlet, message);
                    }
                }
            }
        }

        private static string GetInletPropertyDoesNotMatchMessage(Inlet customOperatorInlet, string propertyDisplayName)
        {
            return MessageFormatter.InletPropertyDoesNotMatchWithUnderlyingPatch(
                propertyDisplayName,
                customOperatorInlet.Name,
                ResourceHelper.GetDisplayName(customOperatorInlet.InletType),
                customOperatorInlet.ListIndex);
        }

        private static string GetOutletPropertyDoesNotMatchMessage(Outlet customOperatorOutlet, string propertyDisplayName)
        {
            return MessageFormatter.OutletPropertyDoesNotMatchWithUnderlyingPatch(
                propertyDisplayName,
                customOperatorOutlet.Name,
                ResourceHelper.GetDisplayName(customOperatorOutlet.OutletType),
                customOperatorOutlet.ListIndex);
        }
    }
}