using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Extensions
{
    internal static class RecursionExtensions
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

        /// <summary>
        /// Tells us whether the document contains custom operators that have circular references to their associated documents.
        /// </summary>
        public static bool HasCircularUnderlyingDocuments(this Document document, IDocumentRepository documentRepository)
        {
            if (document == null) throw new NullException(() => document);

            return document.HasCircularUnderlyingDocument(documentRepository, new HashSet<object>());
        }

        /// <summary>
        /// Tells us whether the patch contains custom operators that have circular references to their associated documents.
        /// </summary>
        public static bool HasCircularUnderlyingDocuments(this Patch patch, IDocumentRepository documentRepository)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.HasCircularUnderlyingDocument(documentRepository, new HashSet<object>());
        }

        /// <summary>
        /// Tells us whether the custom operators has a circular reference to its associated document.
        /// </summary>
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

            IList<Operator> customOperators = patch.Operators.Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.CustomOperator).ToArray();
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

            var wrapper = new Custom_OperatorWrapper(op, documentRepository);
            Document underlyingDocument = wrapper.UnderlyingDocument;

            if (underlyingDocument != null)
            {
                foreach (Document document2 in underlyingDocument.EnumerateSelfAndParentAndChildren())
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
    }
}
