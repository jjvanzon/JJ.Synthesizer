using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Extensions
{
    public static class RecursionExtensions
    {
        /// <summary>
        /// Tells us whether an operator is circular within a patch.
        /// </summary>
        public static bool IsCircular(this Operator op)
        {
            if (op == null) throw new NullException(() => op);

            var alreadyDone = new HashSet<Operator>();

            return IsCircular(op, alreadyDone);
        }

        private static bool IsCircular(this Operator op, HashSet<Operator> alreadyDone)
        {
            // Be null-tollerant, because you might call it in places where the entities are not valid.
            if (op == null)
            {
                return false;
            }

            if (alreadyDone.Contains(op))
            {
                return true;
            }
            alreadyDone.Add(op);

            foreach (Inlet inlet in op.Inlets)
            {
                if (inlet.InputOutlet != null)
                {
                    if (IsCircular(inlet.InputOutlet.Operator, alreadyDone))
                    {
                        return true;
                    }
                }
            }

            alreadyDone.Remove(op);

            return false;
        }

        public static bool HasCircularUnderlyingDocument(this Document document, IDocumentRepository documentRepository)
        {
            if (document == null) throw new NullException(() => document);

            return document.HasCircularUnderlyingDocument(documentRepository, new HashSet<object>());
        }

        public static bool HasCircularUnderlyingDocument(this Patch patch, IDocumentRepository documentRepository)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.HasCircularUnderlyingDocument(documentRepository, new HashSet<object>());
        }

        public static bool HasCircularUnderlyingDocument(this Operator op, IDocumentRepository documentRepository)
        {
            if (op == null) throw new NullException(() => op);
            if (op.GetOperatorTypeEnum() != OperatorTypeEnum.CustomOperator) throw new NotEqualException(() => op.GetOperatorTypeEnum(), OperatorTypeEnum.CustomOperator);

            return op.HasCircularUnderlyingDocument(documentRepository, new HashSet<object>());
        }

        private static bool HasCircularUnderlyingDocument(this Patch patch, IDocumentRepository documentRepository, HashSet<object> alreadyDone)
        {
            if (alreadyDone.Contains(patch))
            {
                return true;
            }
            alreadyDone.Add(patch);

            IList<Operator> customOperators = patch.GetOperatorsOfType(OperatorTypeEnum.CustomOperator);
            foreach (Operator customOperator in customOperators)
            {
                if (customOperator.HasCircularUnderlyingDocument(documentRepository, alreadyDone))
                {
                    return true;
                }
            }

            alreadyDone.Remove(patch);

            return false;
        }

        private static bool HasCircularUnderlyingDocument(this Operator op, IDocumentRepository documentRepository, HashSet<object> alreadyDone)
        {
            if (alreadyDone.Contains(op))
            {
                return true;
            }
            alreadyDone.Add(op);

            var wrapper = new OperatorWrapper_CustomOperator(op, documentRepository);
            Document underlyingDocument = wrapper.UnderlyingDocument;

            if (underlyingDocument != null)
            {
                foreach (Document document2 in underlyingDocument.EnumerateSelfAndParentAndTheirChildren())
                {
                    if (document2.HasCircularUnderlyingDocument(documentRepository, alreadyDone))
                    {
                        return true;
                    }
                }
            }

            alreadyDone.Remove(op);

            return false;
        }

        private static bool HasCircularUnderlyingDocument(this Document document, IDocumentRepository documentRepository, HashSet<object> alreadyDone)
        {
            if (alreadyDone.Contains(document))
            {
                return true;
            }
            alreadyDone.Add(document);

            foreach (Patch patch2 in document.Patches)
            {
                if (patch2.HasCircularUnderlyingDocument(documentRepository, alreadyDone))
                {
                    return true;
                }
            }

            alreadyDone.Remove(document);

            return false;
        }

        public static IEnumerable<Document> EnumerateSelfAndParentAndTheirChildren(this Document document)
        {
            if (document == null) throw new NullException(() => document);

            Document rootDocument = document.ParentDocument ?? document;

            yield return rootDocument;

            foreach (Document childDocument in rootDocument.ChildDocuments)
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
            IEnumerable<Operator> enumerable = document.EnumerateSelfAndParentAndTheirChildren()
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
