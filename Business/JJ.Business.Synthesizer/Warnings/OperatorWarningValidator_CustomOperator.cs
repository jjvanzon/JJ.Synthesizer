using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Warnings
{
    public class OperatorWarningValidator_CustomOperator : OperatorWarningValidator_Base
    {
        private IDocumentRepository _documentRepository;

        public OperatorWarningValidator_CustomOperator(Operator op, IDocumentRepository documentRepository)
            : base(op, postponeExecute: true)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;

            Execute();
        }

        protected override void Execute()
        {
            For(() => Object.Data, PropertyDisplayNames.Data)
                .NotNull();

            int underlyingDocumentID;
            if (Int32.TryParse(Object.Data, out underlyingDocumentID))
            {
                Document underlyingDocument = _documentRepository.TryGet(underlyingDocumentID);
                if (underlyingDocument != null)
                {
                    if (underlyingDocument.MainPatch == null)
                    {
                        ValidationMessages.Add(() => underlyingDocument, Messages.UnderlyingDocumentMainPatchIsNull);
                    }
                }
            }
        }
    }
}
