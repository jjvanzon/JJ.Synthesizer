using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.Helpers
{
    internal static class ChildDocumentHelper
    {
        public static Document GetRootDocument(Document document)
        {
            if (document == null) throw new NullException(() => document);

            return document.AsInstrumentInDocument ?? document.AsEffectInDocument ?? document;
        }

        public static Document TryGetParentDocument(Document document)
        {
            if (document == null) throw new NullException(() => document);

            return document.AsInstrumentInDocument ?? document.AsEffectInDocument;
        }

        public static int GetParentDocumentID(Document childDocument)
        {
            if (childDocument == null) throw new NullException(() => childDocument);

            if (childDocument.AsInstrumentInDocument != null)
            {
                return childDocument.AsInstrumentInDocument.ID;
            }
            else if (childDocument.AsEffectInDocument != null)
            {
                return childDocument.AsEffectInDocument.ID;
            }

            throw new Exception("Either document.AsInstrumentInDocument or document.AsEffectInDocument must be filled in.");
        }

        public static ChildDocumentTypeEnum? TryGetChildDocumentTypeEnum(Document document)
        {
            if (document == null) throw new NullException(() => document);

            if (document.AsInstrumentInDocument != null)
            {
                return ChildDocumentTypeEnum.Instrument;
            }
            else if (document.AsEffectInDocument != null)
            {
                return ChildDocumentTypeEnum.Effect;
            }

            return null;
        }

        public static ChildDocumentTypeEnum GetChildDocumentTypeEnum(Document childDocument)
        {
            ChildDocumentTypeEnum? value = TryGetChildDocumentTypeEnum(childDocument);

            if (!value.HasValue)
            {
                throw new Exception("Either document.AsInstrumentInDocument or document.AsEffectInDocument must be filled in.");
            }

            return value.Value;
        }

        public static IList<Document> GetChildDocuments(Document parentDocument, ChildDocumentTypeEnum childDocumentTypeEnum)
        {
            switch (childDocumentTypeEnum)
            {
                case ChildDocumentTypeEnum.Instrument:
                    return parentDocument.Instruments;

                case ChildDocumentTypeEnum.Effect:
                    return parentDocument.Effects;

                default:
                    throw new ValueNotSupportedException(childDocumentTypeEnum);
            }
        }
 
        public static Document GetDocument(int documentID, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex, IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            Document document = documentRepository.TryGet(documentID);

            if (document == null)
            {
                return null;
            }

            if (childDocumentTypeEnum.HasValue != childDocumentListIndex.HasValue)
            {
                throw new Exception("Both childDocumentTypeEnum and childDocumentListIndex must be filled in or both should not be filled in.");
            }

            if (!childDocumentTypeEnum.HasValue)
            {
                return document;
            }

            switch (childDocumentTypeEnum.Value)
            {
                case ChildDocumentTypeEnum.Instrument:
                    Document instrument = document.Instruments.OrderBy(x => x.Name).ElementAt(childDocumentListIndex.Value);
                    return instrument;

                case ChildDocumentTypeEnum.Effect:
                    Document effect = document.Effects.OrderBy(x => x.Name).ElementAt(childDocumentListIndex.Value);
                    return effect;

                default:
                    throw new ValueNotSupportedException(childDocumentTypeEnum.Value);
            }
        }
   }
}