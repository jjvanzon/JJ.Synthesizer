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
        // TODO: The fact that these methods need to exist seem one giant design flaw.

        // TODO: Error handling to produce clear error messages (instead of SingleOrDefault use a more tollerant method and throw an exception).

        // TODO: There are many unused methods here.

        // Documents

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

        public static Document TryGetRootDocumentOrChildDocument(int rootDocumentID, int? childDocumentID, IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            if (!childDocumentID.HasValue)
            {
                return documentRepository.TryGet(rootDocumentID);
            }
            else
            {
                Document childDocument = documentRepository.TryGet(childDocumentID.Value);
                if (childDocument != null)
                {
                    return childDocument;
                }


                Document rootDocument = documentRepository.Get(rootDocumentID);
                childDocument = Enumerable.Union(rootDocument.Instruments, rootDocument.Effects)
                                          .Where(x => x.ID == childDocumentID.Value)
                                          .SingleOrDefault();
                return childDocument;
            }
        }

        // ChildDocument ViewModels

        public static ChildDocumentViewModel GetChildDocumentViewModel_ByID(DocumentViewModel documentViewModel, int childDocumentID)
        {
            ChildDocumentViewModel childDocumentViewModel = Enumerable.Union(documentViewModel.InstrumentDocumentList, documentViewModel.EffectDocumentList)
                                                                      .Where(x => x.ID == childDocumentID)
                                                                      .SingleOrDefault();
            if (childDocumentViewModel == null)
            {
                throw new Exception(String.Format("documentViewModel does not have ID '{0}' and neither do any ChildDocumentViewModels.", childDocumentID));
            }

            return childDocumentViewModel;
        }

        // Curve ViewModels

        public static CurveListViewModel GetCurveListViewModel_ByDocumentID(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel.ID == documentID)
            {
                return rootDocumentViewModel.CurveList;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel_ByID(rootDocumentViewModel, documentID);
                return childDocumentViewModel.CurveList;
            }
        }

        public static IList<CurveDetailsViewModel> GetCurveDetailsViewModels_ByDocumentID(DocumentViewModel documentViewModel, int documentID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            if (documentViewModel.ID == documentID)
            {
                return documentViewModel.CurveDetailsList;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel_ByID(documentViewModel, documentID);
                return childDocumentViewModel.CurveDetailsList;
            }
        }

        public static ChildDocumentItemAlternativeKey GetAlternativeCurveKey(DocumentViewModel rootDocumentViewModel, int curveID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            int? listIndex = rootDocumentViewModel.CurveDetailsList.TryGetIndexOf(x => x.Entity.ID == curveID);
            if (listIndex.HasValue)
            {
                return new ChildDocumentItemAlternativeKey
                {
                    ChildDocumentTypeEnum = null,
                    ChildDocumentListIndex = null,
                    EntityListIndex = listIndex.Value,
                    // TODO: Remove outcommented code.
                    //ChildDocumentID = null
                };
            }

            int? childDocumentListIndex = rootDocumentViewModel.InstrumentDocumentList
                                                               .TryGetIndexOf(x => x.CurveDetailsList
                                                                                    .Any(y => y.Entity.ID == curveID));
            if (childDocumentListIndex.HasValue)
            {
                listIndex = rootDocumentViewModel.InstrumentDocumentList[childDocumentListIndex.Value]
                                                 .CurveDetailsList
                                                 .IndexOf(x => x.Entity.ID == curveID);

                return new ChildDocumentItemAlternativeKey
                {
                    ChildDocumentTypeEnum = ChildDocumentTypeEnum.Instrument,
                    ChildDocumentListIndex = childDocumentListIndex.Value,
                    EntityListIndex = listIndex.Value,
                    // TODO: Remove outcommented code.
                    //ChildDocumentID = rootDocumentViewModel.InstrumentDocumentList[childDocumentListIndex.Value].ID
                };
            }

            childDocumentListIndex = rootDocumentViewModel.EffectDocumentList
                                                          .TryGetIndexOf(x => x.CurveDetailsList
                                                                               .Any(y => y.Entity.ID == curveID));
            if (childDocumentListIndex.HasValue)
            {
                listIndex = rootDocumentViewModel.EffectDocumentList[childDocumentListIndex.Value]
                                                 .CurveDetailsList
                                                 .IndexOf(x => x.Entity.ID == curveID);

                return new ChildDocumentItemAlternativeKey
                {
                    ChildDocumentTypeEnum = ChildDocumentTypeEnum.Effect,
                    ChildDocumentListIndex = childDocumentListIndex.Value,
                    EntityListIndex = listIndex.Value,
                    // TODO: Remove outcommented code.
                    //ChildDocumentID = rootDocumentViewModel.EffectDocumentList[childDocumentListIndex.Value].ID
                };
            }

            throw new Exception(String.Format("documentViewModel does not have CurveDetailsViewModel with ID '{0}' and neither do any ChildDocumentViewModels.", curveID));
        }

        public static IList<CurveDetailsViewModel> GetCurveDetailsViewModels_ByAlternativeKey(DocumentViewModel documentViewModel, ChildDocumentItemAlternativeKey key)
        {
            if (key == null) throw new NullException(() => key);

            return GetCurveDetailsViewModels_ByAlternativeKey(documentViewModel, key.ChildDocumentTypeEnum, key.ChildDocumentListIndex);
        }

        public static IList<CurveDetailsViewModel> GetCurveDetailsViewModels_ByAlternativeKey(DocumentViewModel documentViewModel, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            if (childDocumentTypeEnum.HasValue != childDocumentListIndex.HasValue)
            {
                throw new Exception("childDocumentTypeEnum and childDocumentListIndex must both have a value or must neither have a value.");
            }

            if (childDocumentTypeEnum.HasValue)
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
            else
            {
                return documentViewModel.CurveDetailsList;
            }
        }

        public static CurveDetailsViewModel GetCurveDetailsViewModel_ByAlternativeKey(DocumentViewModel documentViewModel, ChildDocumentItemAlternativeKey key)
        {
            if (key == null) throw new NullException(() => key);

            return GetCurveDetailsViewModel_ByAlternativeKey(documentViewModel, key.EntityListIndex, key.ChildDocumentTypeEnum, key.ChildDocumentListIndex);
        }

        public static CurveDetailsViewModel GetCurveDetailsViewModel_ByAlternativeKey(DocumentViewModel documentViewModel, int curveListIndex, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            IList<CurveDetailsViewModel> list = GetCurveDetailsViewModels_ByAlternativeKey(documentViewModel, childDocumentTypeEnum, childDocumentListIndex);

            return list[curveListIndex];
        }

        // Patch ViewModels

        public static PatchListViewModel GetPatchListViewModel_ByDocumentID(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel.ID == documentID)
            {
                return rootDocumentViewModel.PatchList;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel_ByID(rootDocumentViewModel, documentID);
                return childDocumentViewModel.PatchList;
            }
        }

        public static IList<PatchDetailsViewModel> GetPatchDetailsViewModels_ByDocumentID(DocumentViewModel documentViewModel, int documentID)
        {
            if (documentViewModel.ID == documentID)
            {
                return documentViewModel.PatchDetailsList;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel_ByID(documentViewModel, documentID);
                return childDocumentViewModel.PatchDetailsList;
            }
        }

        public static ChildDocumentItemAlternativeKey GetAlternativePatchKey(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            int? listIndex = rootDocumentViewModel.PatchDetailsList.TryGetIndexOf(x => x.Entity.ID == patchID);
            if (listIndex.HasValue)
            {
                return new ChildDocumentItemAlternativeKey
                {
                    ChildDocumentTypeEnum = null,
                    ChildDocumentListIndex = null,
                    EntityListIndex = listIndex.Value,
                    // TODO: Remove outcommented code.
                    //ChildDocumentID = null
                };
            }

            int? childDocumentListIndex = rootDocumentViewModel.InstrumentDocumentList
                                                               .TryGetIndexOf(x => x.PatchDetailsList
                                                                                    .Any(y => y.Entity.ID == patchID));
            if (childDocumentListIndex.HasValue)
            {
                listIndex = rootDocumentViewModel.InstrumentDocumentList[childDocumentListIndex.Value]
                                                 .PatchDetailsList
                                                 .IndexOf(x => x.Entity.ID == patchID);

                return new ChildDocumentItemAlternativeKey
                {
                    ChildDocumentTypeEnum = ChildDocumentTypeEnum.Instrument,
                    ChildDocumentListIndex = childDocumentListIndex.Value,
                    EntityListIndex = listIndex.Value,
                    // TODO: Remove outcommented code.
                    //ChildDocumentID = rootDocumentViewModel.InstrumentDocumentList[childDocumentListIndex.Value].ID
                };
            }

            childDocumentListIndex = rootDocumentViewModel.EffectDocumentList
                                                          .TryGetIndexOf(x => x.PatchDetailsList
                                                                               .Any(y => y.Entity.ID == patchID));
            if (childDocumentListIndex.HasValue)
            {
                listIndex = rootDocumentViewModel.EffectDocumentList[childDocumentListIndex.Value]
                                                 .PatchDetailsList
                                                 .IndexOf(x => x.Entity.ID == patchID);

                return new ChildDocumentItemAlternativeKey
                {
                    ChildDocumentTypeEnum = ChildDocumentTypeEnum.Effect,
                    ChildDocumentListIndex = childDocumentListIndex.Value,
                    EntityListIndex = listIndex.Value,
                    // TODO: Remove outcommented code.
                    //ChildDocumentID = rootDocumentViewModel.EffectDocumentList[childDocumentListIndex.Value].ID
                };
            }

            throw new Exception(String.Format("documentViewModel does not have PatchDetailsViewModel with ID '{0}' and neither do any ChildDocumentViewModels.", patchID));
        }

        public static IList<PatchDetailsViewModel> GetPatchDetailsViewModels_ByAlternativeKey(DocumentViewModel documentViewModel, ChildDocumentItemAlternativeKey key)
        {
            if (key == null) throw new NullException(() => key);

            return GetPatchDetailsViewModels_ByAlternativeKey(documentViewModel, key.ChildDocumentTypeEnum, key.ChildDocumentListIndex);
        }

        public static IList<PatchDetailsViewModel> GetPatchDetailsViewModels_ByAlternativeKey(DocumentViewModel documentViewModel, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            if (childDocumentTypeEnum.HasValue != childDocumentListIndex.HasValue)
            {
                throw new Exception("childDocumentTypeEnum and childDocumentListIndex must both have a value or must neither have a value.");
            }

            if (childDocumentTypeEnum.HasValue)
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
            else
            {
                return documentViewModel.PatchDetailsList;
            }
        }

        public static PatchDetailsViewModel GetPatchDetailsViewModel_ByAlternativeKey(DocumentViewModel documentViewModel, ChildDocumentItemAlternativeKey key)
        {
            if (key == null) throw new NullException(() => key);

            return GetPatchDetailsViewModel_ByAlternativeKey(documentViewModel, key.EntityListIndex, key.ChildDocumentTypeEnum, key.ChildDocumentListIndex);
        }

        public static PatchDetailsViewModel GetPatchDetailsViewModel_ByAlternativeKey(DocumentViewModel documentViewModel, int patchListIndex, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            IList<PatchDetailsViewModel> list = GetPatchDetailsViewModels_ByAlternativeKey(documentViewModel, childDocumentTypeEnum, childDocumentListIndex);

            return list[patchListIndex];
        }

        // Sample ViewModels

        public static SampleListViewModel GetSampleListViewModel_ByAlternativeKey(DocumentViewModel documentViewModel, ChildDocumentItemAlternativeKey key)
        {
            if (key == null) throw new NullException(() => key);

            return GetSampleListViewModel_ByAlternativeKey(documentViewModel, key.ChildDocumentTypeEnum, key.ChildDocumentListIndex);
        }

        // TODO: Make a template method by passing a delegate.
        public static SampleListViewModel GetSampleListViewModel_ByAlternativeKey(DocumentViewModel documentViewModel, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            if (childDocumentTypeEnum.HasValue != childDocumentListIndex.HasValue)
            {
                throw new Exception("childDocumentTypeEnum and childDocumentListIndex must both have a value or must neither have a value.");
            }

            if (childDocumentTypeEnum.HasValue)
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
            else
            {
                return documentViewModel.SampleList;
            }
        }

        public static SampleListViewModel GetSampleListViewModel_ByDocumentID(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel.ID == documentID)
            {
                return rootDocumentViewModel.SampleList;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel_ByID(rootDocumentViewModel, documentID);
                return childDocumentViewModel.SampleList;
            }
        }

        public static IList<SamplePropertiesViewModel> GetSamplePropertiesViewModels_ByDocumentID(DocumentViewModel documentViewModel, int documentID)
        {
            if (documentViewModel.ID == documentID)
            {
                return documentViewModel.SamplePropertiesList;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel_ByID(documentViewModel, documentID);
                return childDocumentViewModel.SamplePropertiesList;
            }
        }

        public static ChildDocumentItemAlternativeKey GetAlternativeSampleKey(DocumentViewModel rootDocumentViewModel, int sampleID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            int? listIndex = rootDocumentViewModel.SamplePropertiesList.TryGetIndexOf(x => x.Entity.ID == sampleID);
            if (listIndex.HasValue)
            {
                return new ChildDocumentItemAlternativeKey
                {
                    ChildDocumentTypeEnum = null,
                    ChildDocumentListIndex = null,
                    EntityListIndex = listIndex.Value,
                    // TODO: Remove outcommented code.
                    //ChildDocumentID = null
                };
            }

            int? childDocumentListIndex = rootDocumentViewModel.InstrumentDocumentList
                                                               .TryGetIndexOf(x => x.SamplePropertiesList
                                                                                    .Any(y => y.Entity.ID == sampleID));
            if (childDocumentListIndex.HasValue)
            {
                listIndex = rootDocumentViewModel.InstrumentDocumentList[childDocumentListIndex.Value]
                                                 .SamplePropertiesList
                                                 .IndexOf(x => x.Entity.ID == sampleID);

                return new ChildDocumentItemAlternativeKey
                {
                    ChildDocumentTypeEnum = ChildDocumentTypeEnum.Instrument,
                    ChildDocumentListIndex = childDocumentListIndex.Value,
                    EntityListIndex = listIndex.Value,
                    // TODO: Remove outcommented code.
                    //ChildDocumentID = rootDocumentViewModel.InstrumentDocumentList[childDocumentListIndex.Value].ID
                };
            }

            childDocumentListIndex = rootDocumentViewModel.EffectDocumentList
                                                          .TryGetIndexOf(x => x.SamplePropertiesList
                                                                               .Any(y => y.Entity.ID == sampleID));
            if (childDocumentListIndex.HasValue)
            {
                listIndex = rootDocumentViewModel.EffectDocumentList[childDocumentListIndex.Value]
                                                 .SamplePropertiesList
                                                 .IndexOf(x => x.Entity.ID == sampleID);

                return new ChildDocumentItemAlternativeKey
                {
                    ChildDocumentTypeEnum = ChildDocumentTypeEnum.Effect,
                    ChildDocumentListIndex = childDocumentListIndex.Value,
                    EntityListIndex = listIndex.Value,
                    // TODO: Remove outcommented code.
                    //ChildDocumentID = rootDocumentViewModel.EffectDocumentList[childDocumentListIndex.Value].ID
                };
            }

            throw new Exception(String.Format("documentViewModel does not have SamplePropertiesViewModel with ID '{0}' and neither do any ChildDocumentViewModels.", sampleID));
        }

        public static IList<SamplePropertiesViewModel> GetSamplePropertiesViewModels_ByAlternativeKey(DocumentViewModel documentViewModel, ChildDocumentItemAlternativeKey key)
        {
            if (key == null) throw new NullException(() => key);

            return GetSamplePropertiesViewModels_ByAlternativeKey(documentViewModel, key.ChildDocumentTypeEnum, key.ChildDocumentListIndex);
        }

        public static IList<SamplePropertiesViewModel> GetSamplePropertiesViewModels_ByAlternativeKey(DocumentViewModel documentViewModel, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            if (childDocumentTypeEnum.HasValue != childDocumentListIndex.HasValue)
            {
                throw new Exception("childDocumentTypeEnum and childDocumentListIndex must both have a value or must neither have a value.");
            }

            if (childDocumentTypeEnum.HasValue)
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
            else
            {
                return documentViewModel.SamplePropertiesList;
            }
        }

        public static SamplePropertiesViewModel GetSamplePropertiesViewModel_ByAlternativeKey(DocumentViewModel documentViewModel, ChildDocumentItemAlternativeKey key)
        {
            if (key == null) throw new NullException(() => key);

            return GetSamplePropertiesViewModel_ByAlternativeKey(documentViewModel, key.EntityListIndex, key.ChildDocumentTypeEnum, key.ChildDocumentListIndex);
        }

        public static SamplePropertiesViewModel GetSamplePropertiesViewModel_ByAlternativeKey(DocumentViewModel documentViewModel, int sampleListIndex, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            IList<SamplePropertiesViewModel> list = GetSamplePropertiesViewModels_ByAlternativeKey(documentViewModel, childDocumentTypeEnum, childDocumentListIndex);

            return list[sampleListIndex];
        }

        // Entities

        public static Curve TryGetCurve(Document rootDocument, int curveID)
        {
            Curve curve = rootDocument.Curves.Where(x => x.ID == curveID).SingleOrDefault();

            if (curve == null)
            {
                curve = Enumerable.Union(rootDocument.Instruments, rootDocument.Effects)
                                  .SelectMany(x => x.Curves)
                                  .Where(x => x.ID == curveID)
                                  .SingleOrDefault();
            }

            return curve;
        }

        public static Patch TryGetPatch(Document rootDocument, int patchID)
        {
            Patch patch = rootDocument.Patches.Where(x => x.ID == patchID).SingleOrDefault();

            if (patch == null)
            {
                patch = Enumerable.Union(rootDocument.Instruments, rootDocument.Effects)
                                  .SelectMany(x => x.Patches)
                                  .Where(x => x.ID == patchID)
                                  .SingleOrDefault();
            }

            return patch;
        }

        public static Patch GetPatch(Document rootDocument, int patchID)
        {
            Patch patch = TryGetPatch(rootDocument, patchID);

            if (patch == null)
            {
                throw new Exception(String.Format("Patch with ID '{0}' not found in either root Document or child Documents.", patchID));
            }

            return patch;
        }

        public static Sample TryGetSample(Document rootDocument, int sampleID)
        {
            Sample sample = rootDocument.Samples.Where(x => x.ID == sampleID).SingleOrDefault();

            if (sample == null)
            {
                sample = Enumerable.Union(rootDocument.Instruments, rootDocument.Effects)
                                  .SelectMany(x => x.Samples)
                                  .Where(x => x.ID == sampleID)
                                  .SingleOrDefault();
            }

            return sample;
        }

        public static Sample GetSample(Document rootDocument, int sampleID)
        {
            Sample sample = TryGetSample(rootDocument, sampleID);

            if (sample == null)
            {
                throw new Exception(String.Format("Sample with ID '{0}' not found in either root Document or child Documents.", sampleID));
            }

            return sample;
        }
    }
}