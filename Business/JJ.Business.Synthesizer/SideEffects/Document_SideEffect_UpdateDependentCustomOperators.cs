using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Business;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Converters;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class Document_SideEffect_UpdateDependentCustomOperators : ISideEffect
    {
        private readonly Document _underlyingDocument;
        private readonly IDocumentRepository _documentRepository;
        private readonly DocumentToOperatorConverter _documentToOperatorConverter;

        public Document_SideEffect_UpdateDependentCustomOperators(
            Document underlyingDocument,
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            IDocumentRepository documentRepository,
            IOperatorTypeRepository operatorTypeRepository,
            IIDRepository idRepository)
        {
            if (underlyingDocument == null) throw new NullException(() => underlyingDocument);
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _underlyingDocument = underlyingDocument;
            _documentRepository = documentRepository;

            _documentToOperatorConverter = new DocumentToOperatorConverter(inletRepository, outletRepository, documentRepository, operatorTypeRepository, idRepository);
        }

        public void Execute()
        {
            IList<Operator> customOperators = _underlyingDocument.EnumerateDependentCustomOperators(_documentRepository).ToArray();

            foreach (Operator customOperator in customOperators)
            {
                _documentToOperatorConverter.Convert(_underlyingDocument, customOperator);
            }
        }
    }
}
