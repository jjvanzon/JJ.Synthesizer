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

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_CustomOperator : FluentValidator<Operator>
    {
        private IDocumentRepository _documentRepository;

        public OperatorValidator_CustomOperator(Operator op, IDocumentRepository documentRepository)
            : base(op, postponeExecute: true)
        {
            _documentRepository = documentRepository;

            Execute();
        }

        protected override void Execute()
        {
            Operator op = Object;

            int i = 0;
            foreach (Inlet inlet in op.Inlets)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(inlet, i + 1);
                Execute(new InletValidator_Basic(inlet, i), messagePrefix);
                Execute(new InletValidator_ForCustomOperator(inlet), messagePrefix);
                i++;
            }

            i = 0;
            foreach (Outlet outlet in op.Outlets)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(outlet, i + 1);
                Execute(new OutletValidator_Basic(outlet, i), messagePrefix);
                Execute(new OutletValidator_ForCustomOperator(outlet), messagePrefix);
                i++;
            }

            ValidateInletNamesUnique();
            ValidateOutletNamesUnique();

            For(() => op.Data, PropertyDisplayNames.Data).IsInteger();

            int underlyingDocumentID;
            if (Int32.TryParse(op.Data, out underlyingDocumentID))
            {
                Document underlyingDocument = _documentRepository.TryGet(underlyingDocumentID);
                if (underlyingDocument == null)
                {
                    ValidationMessages.Add(() => underlyingDocument, CommonMessageFormatter.ObjectNotFoundWithID(PropertyDisplayNames.UnderlyingDocument, underlyingDocumentID));
                }
                else
                {
                    ValidateUnderlyingDocumentReferenceConstraint(underlyingDocument);

                    if (underlyingDocument.MainPatch != null)
                    {
                        ValidateInletsAgainstDocument(underlyingDocument);
                        ValidateOutletsAgainstDocument(underlyingDocument);
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

        private void ValidateUnderlyingDocumentReferenceConstraint(Document underlyingDocument)
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

                bool isInList = documents.Any(x => x.ID == underlyingDocument.ID);
                if (!isInList)
                {
                    ValidationMessages.Add(PropertyNames.UnderlyingDocument, MessageFormatter.NotFoundInList_WithItemName_AndID(PropertyDisplayNames.UnderlyingDocument, underlyingDocument.ID));
                }
            }
        }

        private void ValidateInletsAgainstDocument(Document document)
        {
            Operator op = Object;

            IList<Operator> underlyingPatch_InletOperators = document.MainPatch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet);

            foreach (Operator underlyingPatch_InletOperator in underlyingPatch_InletOperators)
            {
                string name = underlyingPatch_InletOperator.Name;
                bool exists = op.Inlets.Where(x => String.Equals(x.Name, name)).Any();
                if (!exists)
                {
                    ValidationMessages.Add(PropertyNames.Inlet, MessageFormatter.NotFound_WithTypeName_AndName(PropertyDisplayNames.Inlet, name));
                }
            }
        }

        private void ValidateOutletsAgainstDocument(Document document)
        {
            Operator op = Object;

            IList<Operator> underlyingPatch_OutletOperators = document.MainPatch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet);

            foreach (Operator underlyingPatch_OutletOperator in underlyingPatch_OutletOperators)
            {
                string name = underlyingPatch_OutletOperator.Name;
                bool exists = op.Outlets.Where(x => String.Equals(x.Name, name)).Any();
                if (!exists)
                {
                    ValidationMessages.Add(PropertyNames.Outlet, MessageFormatter.NotFound_WithTypeName_AndName(PropertyDisplayNames.Outlet, name));
                }
            }                       
        }
    }
}