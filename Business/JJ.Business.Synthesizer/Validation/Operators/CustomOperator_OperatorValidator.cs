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

            IList<Operator> underlyingPatchInletOperators = underlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet);

            foreach (Inlet customOperatorInlet in customOperator.Inlets)
            {
                Operator underlyingPatchInletOperator = InletOutletMatcher.TryGetPatchInlet(customOperatorInlet, _patchRepository);

                if (underlyingPatchInletOperator == null)
                {
                    // Obsolete CustomOperator Inlets are allowed.
                    continue;
                }

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

                Inlet underlyingPatchInlet_Inlet = TryGetInlet(underlyingPatchInletOperator);
                if (underlyingPatchInlet_Inlet != null)
                {
                    if (customOperatorInlet.GetDimensionEnum() != underlyingPatchInlet_Inlet.GetDimensionEnum())
                    {
                        string message = GetInletPropertyDoesNotMatchMessage(customOperatorInlet, PropertyDisplayNames.Dimension);
                        ValidationMessages.Add(PropertyNames.Inlet, message);
                    }

                    if (customOperatorInlet.DefaultValue != underlyingPatchInlet_Inlet.DefaultValue)
                    {
                        string message = GetInletPropertyDoesNotMatchMessage(customOperatorInlet, PropertyDisplayNames.DefaultValue);
                        ValidationMessages.Add(PropertyNames.Inlet, message);
                    }
                }
            }
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
                    // Obsolete CustomOperator Outlets are allowed.
                    continue;
                }

                int? underlyingPatchOutlet_ListIndex = TryGetListIndex(underlyingPatchOutlet);

                if (customOperatorOutlet.ListIndex != underlyingPatchOutlet_ListIndex)
                {
                    string message = GetOutletPropertyDoesNotMatchMessage(customOperatorOutlet, PropertyDisplayNames.ListIndex);
                    ValidationMessages.Add(PropertyNames.Outlet, message);
                }

                if (!String.Equals(customOperatorOutlet.Name, underlyingPatchOutlet.Name))
                {
                    string message = GetOutletPropertyDoesNotMatchMessage(customOperatorOutlet, CommonTitles.Name);
                    ValidationMessages.Add(PropertyNames.Outlet, message);
                }

                Outlet underlyingPatchOutlet_Outlet = TryGetOutlet(underlyingPatchOutlet);
                if (underlyingPatchOutlet_Outlet != null)
                {
                    if (customOperatorOutlet.GetDimensionEnum() != underlyingPatchOutlet_Outlet.GetDimensionEnum())
                    {
                        string message = GetOutletPropertyDoesNotMatchMessage(customOperatorOutlet, PropertyDisplayNames.Dimension);
                        ValidationMessages.Add(PropertyNames.Outlet, message);
                    }
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

        private static string GetInletPropertyDoesNotMatchMessage(Inlet customOperatorInlet, string propertyDisplayName)
        {
            return MessageFormatter.InletPropertyDoesNotMatchWithUnderlyingPatch(
                propertyDisplayName,
                customOperatorInlet.Name,
                ResourceHelper.GetDisplayName(customOperatorInlet.Dimension),
                customOperatorInlet.ListIndex);
        }

        private static string GetOutletPropertyDoesNotMatchMessage(Outlet customOperatorOutlet, string propertyDisplayName)
        {
            return MessageFormatter.OutletPropertyDoesNotMatchWithUnderlyingPatch(
                propertyDisplayName,
                customOperatorOutlet.Name,
                ResourceHelper.GetDisplayName(customOperatorOutlet.Dimension),
                customOperatorOutlet.ListIndex);
        }
    }
}
