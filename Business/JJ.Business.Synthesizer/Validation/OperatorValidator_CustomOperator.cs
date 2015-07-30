using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Common;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_CustomOperator : FluentValidator<Operator>
    {
        private const string INLET_COUNT_KEY = "InletCount";
        private const string OUTLET_COUNT_KEY = "OutletCount";

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

            For(() => op.Data, PropertyDisplayNames.Data)
                .IsInteger();

            int documentID;
            if (Int32.TryParse(op.Data, out documentID))
            {
                Document document = _documentRepository.TryGet(documentID);

                For(() => document, PropertyDisplayNames.Document)
                    .NotNull();

                if (document != null)
                {
                    For(() => document.MainPatch, PropertyDisplayNames.MainPatch)
                        .NotNull();

                    if (document.MainPatch != null)
                    {
                        int operatorInletCount = op.Inlets.Count;
                        int mainPatchInletCount = document.MainPatch.Operators.Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.PatchInlet).Count();

                        if (operatorInletCount != mainPatchInletCount)
                        {
                            ValidationMessages.Add(INLET_COUNT_KEY, Messages.OperatorInletCountNotEqualToDocumentMainPatchInletCount);
                        }

                        int operatorOutletCount = op.Outlets.Count;
                        int mainPatchOutletCount = document.MainPatch.Operators.Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.PatchOutlet).Count();

                        if (operatorOutletCount != mainPatchOutletCount)
                        {
                            ValidationMessages.Add(OUTLET_COUNT_KEY, Messages.OperatorOutletCountNotEqualToDocumentMainPatchOutletCount);
                        }
                    }

                    // Check reference constraint of the Document.
                    // (We are quite tollerant here: we omit the check if it is not in a patch or document.)
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

                        bool isInList = documents.Any(x => x.ID == documentID);
                        if (!isInList)
                        {
                            ValidationMessages.Add(PropertyNames.Document, MessageFormatter.NotFoundInList_WithItemName_AndID(PropertyDisplayNames.Document, documentID));
                        }
                    }
                }
            }
        }
    }
}
