using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Business.Synthesizer.Extensions
{
    public static class DocumentExtensions
    {
        public static IEnumerable<Document> EnumerateSelfAndParentAndChildren(this Document document)
        {
            if (document == null) throw new NullException(() => document);

            yield return document;

            if (document.ParentDocument != null)
            {
                yield return document.ParentDocument;
            }

            foreach (Document childDocument in document.ChildDocuments)
            {
                yield return childDocument;
            }
        }

        public static IEnumerable<Document> EnumerateParentAndChildren(this Document document)
        {
            if (document == null) throw new NullException(() => document);

            if (document.ParentDocument != null)
            {
                yield return document.ParentDocument;
            }

            foreach (Document childDocument in document.ChildDocuments)
            {
                yield return childDocument;
            }
        }

        public static IEnumerable<Document> EnumerateSelfAndChildren(this Document document)
        {
            if (document == null) throw new NullException(() => document);

            yield return document;

            foreach (Document childDocument in document.ChildDocuments)
            {
                yield return childDocument;
            }
        }

        /// <summary> Gets the parent of the document or otherwise returns the document itself. </summary>
        public static Document GetRootDocument(this Document document)
        {
            if (document == null) throw new NullException(() => document);

            if (document.ParentDocument != null)
            {
                // Parent-Child relations go but one level deep, so parent document = root document.
                return document.ParentDocument;
            }
            else
            {
                return document;
            }
        }

        /// <summary> Note that dependencies caused by library references (The DocumentReference entity) are not checked. </summary>
        public static IEnumerable<Operator> EnumerateDependentCustomOperators(this Document document, IDocumentRepository documentRepository)
        {
            if (document == null) throw new NullException(() => document);
            if (documentRepository == null) throw new NullException(() => documentRepository);

            // TODO: Program circularity check on parent-child relationships and check it.

            // We cannot use an SQL query, because that only operates on flushed / committed data.
            IEnumerable<Operator> enumerable = document.EnumerateSelfAndParentAndChildren()
                                                       .SelectMany(x => x.Patches)
                                                       .SelectMany(x => x.Operators)
                                                       .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.CustomOperator &&
                                                                   UnderlyingDocumentIsMatch(document, x, documentRepository));
            return enumerable;
        }

        private static bool UnderlyingDocumentIsMatch(Document underlyingDocument, Operator customOperator, IDocumentRepository documentRepository)
        {
            var wrapper = new OperatorWrapper_CustomOperator(customOperator, documentRepository);

            Document underlyingDocument2 = wrapper.UnderlyingDocument;

            if (underlyingDocument2 == null)
            {
                return false;
            }

            return underlyingDocument2.ID == underlyingDocument.ID;
        }
    }
}