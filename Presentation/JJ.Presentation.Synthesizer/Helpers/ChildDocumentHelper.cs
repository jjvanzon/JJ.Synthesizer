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

        // TODO: Remove outcommented code.
        //public static Document GetRootDocumentOrChildDocument(int documentID, int? childDocumentID, IDocumentRepository documentRepository)
        //{
        //    Document document = TryGetRootDocumentOrChildDocument(documentID, childDocumentID, documentRepository);
        //    if (document == null)
        //    {
        //        throw new Exception(String.Format("Root Document or Child Document with documentID '{0}' and childDocumentID '{1}' not found.", documentID, childDocumentID));
        //    }
        //    return document;
        //}

        //public static Document TryGetRootDocumentOrChildDocument(int documentID, IDocumentRepository documentRepository)
        //{
        //    if (documentRepository == null) throw new NullException(() => documentRepository);

        //    Document document = documentRepository.TryGet(documentID);
        //    if (document != null)
        //    {
        //        return null;
        //    }

        //    Document childDocument = Enumerable.Union(document.Instruments, document.Effects)
        //                                       .Where(x => x.ID == documentID)
        //                                       .SingleOrDefault();
        //    return childDocument;
        //}

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

        public static SampleListViewModel GetSampleListViewModel(DocumentViewModel documentViewModel, int documentID)
        {
            if (documentViewModel.ID == documentID)
            {
                return documentViewModel.SampleList;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel(documentViewModel, documentID);
                return childDocumentViewModel.SampleList;
            }
        }

        public static SamplePropertiesViewModel GetSamplePropertiesViewModel(DocumentViewModel documentViewModel, int sampleID)
        {
            SamplePropertiesViewModel viewModel = documentViewModel.SamplePropertiesList.Where(x => x.Entity.ID == sampleID).SingleOrDefault();

            if (viewModel == null)
            {
                viewModel = Enumerable.Union(documentViewModel.InstrumentDocumentList, documentViewModel.EffectDocumentList)
                                      .SelectMany(x => x.SamplePropertiesList)
                                      .Where(x => x.Entity.ID == sampleID)
                                      .Single();
            }

            return viewModel;
        }

        public static IList<SamplePropertiesViewModel> GetSamplePropertiesViewModels_ByDocumentID(DocumentViewModel documentViewModel, int documentID)
        {
            if (documentViewModel.ID == documentID)
            {
                return documentViewModel.SamplePropertiesList;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel(documentViewModel, documentID);
                return childDocumentViewModel.SamplePropertiesList;
            }
        }

        public static IList<SamplePropertiesViewModel> GetSamplePropertiesViewModels_BySampleID(DocumentViewModel documentViewModel, int sampleID)
        {
            if (documentViewModel.SamplePropertiesList.Any(x => x.Entity.ID == sampleID))
            {
                return documentViewModel.SamplePropertiesList;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = Enumerable.Union(documentViewModel.InstrumentDocumentList, documentViewModel.EffectDocumentList)
                                                                          .Where(x => x.SamplePropertiesList.Any(y => y.Entity.ID == sampleID))
                                                                          .SingleOrDefault();
                if (childDocumentViewModel == null)
                {
                    throw new Exception(String.Format("documentViewModel does not have SamplePropertiesViewModel with ID '{0}' and neither do any ChildDocumentViewModels.", sampleID));
                }

                return childDocumentViewModel.SamplePropertiesList;
            }
        }

        public static CurveListViewModel GetCurveListViewModel(DocumentViewModel documentViewModel, int documentID)
        {
            if (documentViewModel.ID == documentID)
            {
                return documentViewModel.CurveList;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel(documentViewModel, documentID);
                return childDocumentViewModel.CurveList;
            }
        }

        public static CurveDetailsViewModel GetCurveDetailsViewModel(DocumentViewModel documentViewModel, int curveID)
        {
            CurveDetailsViewModel viewModel = documentViewModel.CurveDetailsList.Where(x => x.Entity.ID == curveID).SingleOrDefault();

            if (viewModel == null)
            {
                viewModel = Enumerable.Union(documentViewModel.InstrumentDocumentList, documentViewModel.EffectDocumentList)
                                      .SelectMany(x => x.CurveDetailsList)
                                      .Where(x => x.Entity.ID == curveID)
                                      .Single();
            }

            return viewModel;
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
                ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel(documentViewModel, documentID);
                return childDocumentViewModel.CurveDetailsList;
            }
        }

        public static IList<CurveDetailsViewModel> GetCurveDetailsViewModels_ByCurveID(DocumentViewModel documentViewModel, int curveID)
        {
            if (documentViewModel.CurveDetailsList.Any(x => x.Entity.ID == curveID))
            {
                return documentViewModel.CurveDetailsList;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = Enumerable.Union(documentViewModel.InstrumentDocumentList, documentViewModel.EffectDocumentList)
                                                                          .Where(x => x.CurveDetailsList.Any(y => y.Entity.ID == curveID))
                                                                          .SingleOrDefault();
                if (childDocumentViewModel == null)
                {
                    throw new Exception(String.Format("documentViewModel does not have CurveDetailsViewModel with ID '{0}' and neither do any ChildDocumentViewModels.", curveID));
                }

                return childDocumentViewModel.CurveDetailsList;
            }
        }

        public static PatchListViewModel GetPatchListViewModel(DocumentViewModel documentViewModel, int documentID)
        {
            if (documentViewModel.ID == documentID)
            {
                return documentViewModel.PatchList;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel(documentViewModel, documentID);
                return childDocumentViewModel.PatchList;
            }
        }

        public static PatchDetailsViewModel GetPatchDetailsViewModel(DocumentViewModel documentViewModel, int patchID)
        {
            PatchDetailsViewModel viewModel = documentViewModel.PatchDetailsList.Where(x => x.Entity.ID == patchID).SingleOrDefault();

            if (viewModel == null)
            {
                viewModel = Enumerable.Union(documentViewModel.InstrumentDocumentList, documentViewModel.EffectDocumentList)
                                      .SelectMany(x => x.PatchDetailsList)
                                      .Where(x => x.Entity.ID == patchID)
                                      .Single();
            }

            return viewModel;
        }

        public static IList<PatchDetailsViewModel> GetPatchDetailsViewModels_ByDocumentID(DocumentViewModel documentViewModel, int documentID)
        {
            if (documentViewModel.ID == documentID)
            {
                return documentViewModel.PatchDetailsList;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel(documentViewModel, documentID);
                return childDocumentViewModel.PatchDetailsList;
            }
        }

        public static IList<PatchDetailsViewModel> GetPatchDetailsViewModels_ByPatchID(DocumentViewModel documentViewModel, int patchID)
        {
            if (documentViewModel.PatchDetailsList.Any(x => x.Entity.ID == patchID))
            {
                return documentViewModel.PatchDetailsList;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = Enumerable.Union(documentViewModel.InstrumentDocumentList, documentViewModel.EffectDocumentList)
                                                                          .Where(x => x.PatchDetailsList.Any(y => y.Entity.ID == patchID))
                                                                          .SingleOrDefault();
                if (childDocumentViewModel == null)
                {
                    throw new Exception(String.Format("documentViewModel does not have PatchDetailsViewModel with ID '{0}' and neither do any ChildDocumentViewModels.", patchID));
                }

                return childDocumentViewModel.PatchDetailsList;
            }
        }

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

        public static Curve GetCurve(Document rootDocument, int curveID)
        {
            Curve curve = TryGetCurve(rootDocument, curveID);

            if (curve == null)
            {
                throw new Exception(String.Format("Curve with ID '{0}' not found in either root Document or child Documents.", curveID));
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

        public static ChildDocumentViewModel GetChildDocumentViewModel(DocumentViewModel documentViewModel, int childDocumentID)
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

        // TODO: Remove outcommented code. It does the same as the method above.
        //private ChildDocumentViewModel GetChildDocumentViewModel(DocumentViewModel documentViewModel, int childDocumentID)
        //{
        //    if (documentViewModel == null) throw new NullException(() => documentViewModel);

        //    int? childDocumentListIndex = documentViewModel.InstrumentDocumentList.TryGetIndexOf(x => x.ID == childDocumentID);
        //    ChildDocumentViewModel childDocumentViewModel;
        //    if (childDocumentListIndex.HasValue)
        //    {
        //        childDocumentViewModel = documentViewModel.EffectDocumentList[childDocumentListIndex.Value];
        //    }
        //    else
        //    {
        //        childDocumentListIndex = documentViewModel.EffectDocumentList.TryGetIndexOf(x => x.ID == childDocumentID);
        //        if (!childDocumentListIndex.HasValue)
        //        {
        //            throw new Exception(String.Format("ChildDocument with ID '{0}' not found in documentViewModel.InstrumentDocumentList or documentViewModel.EffectDocumentList.", childDocumentID));
        //        }

        //        childDocumentViewModel = documentViewModel.EffectDocumentList[childDocumentListIndex.Value];
        //    }
        //    return childDocumentViewModel;
        //}
    }
}