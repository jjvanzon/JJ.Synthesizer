using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Business;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Converters;

namespace JJ.Business.Synthesizer.SideEffects
{
    // TODO: Make internal after you have encapsulated this functionality in a Manager class.
    public class Document_SideEffect_UpdateDependentCustomOperators : ISideEffect
    {
        private readonly Document _underlyingDocument;
        private readonly IDocumentRepository _documentRepository;
        private DocumentToOperatorConverter _documentToOperatorConverter;

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
            // TODO: Program circularity check on parent-child relationships and check it.

            IList<Operator> customOperators = _underlyingDocument.EnumerateSelfAndParentAndChildren() // We cannot use an SQL query, because that only operates on flushed / committed data.
                                                                 .SelectMany(x => x.Patches)
                                                                 .SelectMany(x => x.Operators)
                                                                 .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.CustomOperator &&
                                                                             UnderlyingDocumentIsMatch(x))
                                                                 .ToArray();
            foreach (Operator customOperator in customOperators)
            {
                _documentToOperatorConverter.Convert(_underlyingDocument, customOperator);
            }
        }

        private bool UnderlyingDocumentIsMatch(Operator customOperator)
        {
            var wrapper = new Custom_OperatorWrapper(customOperator, _documentRepository);

            Document underlyingDocument = wrapper.UnderlyingDocument;

            if (underlyingDocument == null)
            {
                return false;
            }

            return underlyingDocument.ID == _underlyingDocument.ID;
        }
    }
}
