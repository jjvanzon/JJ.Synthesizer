using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class DocumentValidator_Delete : FluentValidator<Document>
    {
        private IDocumentRepository _documentRepository;

        public DocumentValidator_Delete(Document obj, IDocumentRepository documentRepository)
            : base(obj, postponeExecute: true)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;

            Execute();
        }

        protected override void Execute()
        {
            Document document = Object;

            foreach (DocumentReference dependentDocument in document.DependentDocuments)
            {
                string message = MessageFormatter.DocumentIsDependentOnDocument(dependentDocument.DependentDocument.Name, dependentDocument.DependentOnDocument.Name);
                ValidationMessages.Add(PropertyNames.DocumentReference, message);
            }

            bool isChildDocument = document.ParentDocument != null;
            if (isChildDocument)
            {
                bool hasCustomOperators = document.EnumerateDependentCustomOperators(_documentRepository).Any();
                if (hasCustomOperators)
                {
                    ValidationMessages.Add(PropertyNames.Document, MessageFormatter.CannotDeleteBecauseHasReferences());
                }

                // TODO: Be more specific about which custom operators still refer to this document,
                // but beware that just describing the custom operator is not enough,
                // because that is probably the same as th underlying document name anyway.

                //IList<Operator> customOperators = document.EnumerateDependentCustomOperators(_documentRepository).ToArray();
                //foreach (Operator customOperator in customOperators)
                //{
                //    string customOperatordescriptor = ValidationHelper.GetMessagePrefix_ForCustomOperator(customOperator, _documentRepository);

                //    string message = MessageFormatter.CustomOperatorIsDependentOnDocument(customOperatordescriptor, document.Name);
                //    ValidationMessages.Add(PropertyNames.Document, message);
                //}
            }
        }

        //public string GetCustomOperatorDescriptor(Operator entity)
        //{
        //    if (entity == null) throw new NullException(() => entity);

        //    if (!String.IsNullOrEmpty(entity.Name))
        //    {
        //        return entity.Name;
        //    }

        //    var wrapper = new Custom_OperatorWrapper(entity, _documentRepository);
        //    Document underlyingEntity = wrapper.UnderlyingDocument;
        //    string underlyingEntityName = null;
        //    if (underlyingEntity != null)
        //    {
        //        if (!String.IsNullOrEmpty(underlyingEntity.Name))
        //        {
        //            return underlyingEntity.Name;
        //        }
        //    }

        //    string operatorTypeDisplayName = ResourceHelper.GetOperatorTypeDisplayName(entity);
        //    return operatorTypeDisplayName;
        //}
    }
}
