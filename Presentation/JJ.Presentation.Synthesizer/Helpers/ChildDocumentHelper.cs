using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
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

        public static Document GetParentDocument(Document childDocument)
        {
            Document parentDocument = TryGetParentDocument(childDocument);
            if (parentDocument == null)
            {
                throw new Exception("Either document.AsInstrumentInDocument or document.AsEffectInDocument must be filled in.");
            }
            return parentDocument;
        }

        public static Document TryGetParentDocument(Document childDocument)
        {
            if (childDocument == null) throw new NullException(() => childDocument);

            return childDocument.AsInstrumentInDocument ?? childDocument.AsEffectInDocument;
        }

        public static int GetParentDocumentID(Document childDocument)
        {
            if (childDocument == null) throw new NullException(() => childDocument);

            return GetParentDocument(childDocument).ID;
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

        public static Document TryGetDocument(int documentID, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex, IDocumentRepository documentRepository)
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

        public static SampleListViewModel GetSampleListViewModel(DocumentViewModel documentViewModel, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (childDocumentTypeEnum.HasValue != childDocumentListIndex.HasValue)
            {
                throw new Exception("Both childDocumentTypeEnum and childDocumentListIndex must be filled in or both should not be filled in.");
            }

            if (!childDocumentTypeEnum.HasValue)
            {
                return documentViewModel.SampleList;
            }
            else
            {
                switch (childDocumentTypeEnum.Value)
                {
                    case ChildDocumentTypeEnum.Instrument:
                        return documentViewModel.InstrumentDocumentList[childDocumentListIndex.Value].SampleList;

                    case ChildDocumentTypeEnum.Effect:
                        return documentViewModel.EffectDocumentList[childDocumentListIndex.Value].SampleList;

                    default:
                        throw new ValueNotSupportedException(childDocumentTypeEnum.Value);
                }
            }
        }

        public static IList<SamplePropertiesViewModel> GetSamplePropertiesViewModels(DocumentViewModel documentViewModel, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (childDocumentTypeEnum.HasValue != childDocumentListIndex.HasValue)
            {
                throw new Exception("Both childDocumentTypeEnum and childDocumentListIndex must be filled in or both should not be filled in.");
            }

            if (!childDocumentTypeEnum.HasValue)
            {
                return documentViewModel.SamplePropertiesList;
            }
            else
            {
                switch (childDocumentTypeEnum.Value)
                {
                    case ChildDocumentTypeEnum.Instrument:
                        return documentViewModel.InstrumentDocumentList[childDocumentListIndex.Value].SamplePropertiesList;

                    case ChildDocumentTypeEnum.Effect:
                        return documentViewModel.EffectDocumentList[childDocumentListIndex.Value].SamplePropertiesList;

                    default:
                        throw new ValueNotSupportedException(childDocumentTypeEnum.Value);
                }
            }
        }

        public static CurveListViewModel GetCurveListViewModel(DocumentViewModel documentViewModel, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (childDocumentTypeEnum.HasValue != childDocumentListIndex.HasValue)
            {
                throw new Exception("Both childDocumentTypeEnum and childDocumentListIndex must be filled in or both should not be filled in.");
            }

            if (!childDocumentTypeEnum.HasValue)
            {
                return documentViewModel.CurveList;
            }
            else
            {
                switch (childDocumentTypeEnum.Value)
                {
                    case ChildDocumentTypeEnum.Instrument:
                        return documentViewModel.InstrumentDocumentList[childDocumentListIndex.Value].CurveList;

                    case ChildDocumentTypeEnum.Effect:
                        return documentViewModel.EffectDocumentList[childDocumentListIndex.Value].CurveList;

                    default:
                        throw new ValueNotSupportedException(childDocumentTypeEnum.Value);
                }
            }
        }

        public static IList<CurveDetailsViewModel> GetCurveDetailsViewModels(DocumentViewModel documentViewModel, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (childDocumentTypeEnum.HasValue != childDocumentListIndex.HasValue)
            {
                throw new Exception("Both childDocumentTypeEnum and childDocumentListIndex must be filled in or both should not be filled in.");
            }

            if (!childDocumentTypeEnum.HasValue)
            {
                return documentViewModel.CurveDetailsList;
            }
            else
            {
                switch (childDocumentTypeEnum.Value)
                {
                    case ChildDocumentTypeEnum.Instrument:
                        return documentViewModel.InstrumentDocumentList[childDocumentListIndex.Value].CurveDetailsList;

                    case ChildDocumentTypeEnum.Effect:
                        return documentViewModel.EffectDocumentList[childDocumentListIndex.Value].CurveDetailsList;

                    default:
                        throw new ValueNotSupportedException(childDocumentTypeEnum.Value);
                }
            }
        }

        public static PatchListViewModel GetPatchListViewModel(DocumentViewModel documentViewModel, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (childDocumentTypeEnum.HasValue != childDocumentListIndex.HasValue)
            {
                throw new Exception("Both childDocumentTypeEnum and childDocumentListIndex must be filled in or both should not be filled in.");
            }

            if (!childDocumentTypeEnum.HasValue)
            {
                return documentViewModel.PatchList;
            }
            else
            {
                switch (childDocumentTypeEnum.Value)
                {
                    case ChildDocumentTypeEnum.Instrument:
                        return documentViewModel.InstrumentDocumentList[childDocumentListIndex.Value].PatchList;

                    case ChildDocumentTypeEnum.Effect:
                        return documentViewModel.EffectDocumentList[childDocumentListIndex.Value].PatchList;

                    default:
                        throw new ValueNotSupportedException(childDocumentTypeEnum.Value);
                }
            }
        }

        public static IList<PatchDetailsViewModel> GetPatchDetailsViewModels(DocumentViewModel documentViewModel, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (childDocumentTypeEnum.HasValue != childDocumentListIndex.HasValue)
            {
                throw new Exception("Both childDocumentTypeEnum and childDocumentListIndex must be filled in or both should not be filled in.");
            }

            if (!childDocumentTypeEnum.HasValue)
            {
                return documentViewModel.PatchDetailsList;
            }
            else
            {
                switch (childDocumentTypeEnum.Value)
                {
                    case ChildDocumentTypeEnum.Instrument:
                        return documentViewModel.InstrumentDocumentList[childDocumentListIndex.Value].PatchDetailsList;

                    case ChildDocumentTypeEnum.Effect:
                        return documentViewModel.EffectDocumentList[childDocumentListIndex.Value].PatchDetailsList;

                    default:
                        throw new ValueNotSupportedException(childDocumentTypeEnum.Value);
                }
            }
        }
    }
}