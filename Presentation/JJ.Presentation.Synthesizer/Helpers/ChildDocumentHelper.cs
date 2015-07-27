using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
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

                childDocument = rootDocument.ChildDocuments
                                            .Where(x => x.ID == childDocumentID.Value)
                                            .SingleOrDefault();
                return childDocument;
            }
        }

        // ChildDocument ViewModels

        public static ChildDocumentViewModel GetChildDocumentViewModel_ByID(DocumentViewModel documentViewModel, int childDocumentID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            ChildDocumentViewModel childDocumentViewModel = documentViewModel.ChildDocumentList
                                                                             .Where(x => x.ID == childDocumentID)
                                                                             .SingleOrDefault();
            if (childDocumentViewModel == null)
            {
                // TODO: This is not an accurate message.
                throw new Exception(String.Format("documentViewModel does not have ID '{0}' and neither do any ChildDocumentViewModels.", childDocumentID));
            }

            return childDocumentViewModel;
        }

        // Curve ViewModels

        public static CurveGridViewModel GetCurveGridViewModel_ByDocumentID(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel.ID == documentID)
            {
                return rootDocumentViewModel.CurveGrid;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel_ByID(rootDocumentViewModel, documentID);
                return childDocumentViewModel.CurveGrid;
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
                    ChildDocumentListIndex = null,
                    EntityListIndex = listIndex.Value,
                };
            }

            int? childDocumentListIndex = rootDocumentViewModel.ChildDocumentList
                                                               .TryGetIndexOf(x => x.CurveDetailsList
                                                                                    .Any(y => y.Entity.ID == curveID));
            if (childDocumentListIndex.HasValue)
            {
                listIndex = rootDocumentViewModel.ChildDocumentList[childDocumentListIndex.Value]
                                                 .CurveDetailsList
                                                 .IndexOf(x => x.Entity.ID == curveID);

                return new ChildDocumentItemAlternativeKey
                {
                    ChildDocumentListIndex = childDocumentListIndex.Value,
                    EntityListIndex = listIndex.Value,
                };
            }

            throw new Exception(String.Format("documentViewModel does not have CurveDetailsViewModel with ID '{0}' and neither do any ChildDocumentViewModels.", curveID));
        }

        private static IList<CurveDetailsViewModel> GetCurveDetailsViewModels_ByAlternativeKey(DocumentViewModel documentViewModel, int? childDocumentListIndex)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            if (childDocumentListIndex.HasValue)
            {
                return documentViewModel.ChildDocumentList[childDocumentListIndex.Value].CurveDetailsList;
            }
            else
            {
                return documentViewModel.CurveDetailsList;
            }
        }

        public static CurveDetailsViewModel GetCurveDetailsViewModel_ByAlternativeKey(DocumentViewModel documentViewModel, ChildDocumentItemAlternativeKey key)
        {
            if (key == null) throw new NullException(() => key);
            return GetCurveDetailsViewModel_ByAlternativeKey(documentViewModel, key.EntityListIndex, key.ChildDocumentListIndex);
        }

        private static CurveDetailsViewModel GetCurveDetailsViewModel_ByAlternativeKey(DocumentViewModel documentViewModel, int curveListIndex, int? childDocumentListIndex)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            IList<CurveDetailsViewModel> list = GetCurveDetailsViewModels_ByAlternativeKey(documentViewModel, childDocumentListIndex);

            return list[curveListIndex];
        }

        // Patch ViewModels

        public static PatchGridViewModel GetPatchGridViewModel_ByDocumentID(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel.ID == documentID)
            {
                return rootDocumentViewModel.PatchGrid;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel_ByID(rootDocumentViewModel, documentID);
                return childDocumentViewModel.PatchGrid;
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
                    ChildDocumentListIndex = null,
                    EntityListIndex = listIndex.Value,
                };
            }

            int? childDocumentListIndex = rootDocumentViewModel.ChildDocumentList
                                                               .TryGetIndexOf(x => x.PatchDetailsList
                                                                                    .Any(y => y.Entity.ID == patchID));
            if (childDocumentListIndex.HasValue)
            {
                listIndex = rootDocumentViewModel.ChildDocumentList[childDocumentListIndex.Value]
                                                 .PatchDetailsList
                                                 .IndexOf(x => x.Entity.ID == patchID);

                return new ChildDocumentItemAlternativeKey
                {
                    ChildDocumentListIndex = childDocumentListIndex.Value,
                    EntityListIndex = listIndex.Value,
                };
            }

            throw new Exception(String.Format("documentViewModel does not have PatchDetailsViewModel with ID '{0}' and neither do any ChildDocumentViewModels.", patchID));
        }

        private static IList<PatchDetailsViewModel> GetPatchDetailsViewModels_ByAlternativeKey(DocumentViewModel documentViewModel, int? childDocumentListIndex)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            if (childDocumentListIndex.HasValue)
            {
                return documentViewModel.ChildDocumentList[childDocumentListIndex.Value].PatchDetailsList;
            }
            else
            {
                return documentViewModel.PatchDetailsList;
            }
        }

        public static PatchDetailsViewModel GetPatchDetailsViewModel_ByAlternativeKey(DocumentViewModel documentViewModel, ChildDocumentItemAlternativeKey key)
        {
            if (key == null) throw new NullException(() => key);

            return GetPatchDetailsViewModel_ByAlternativeKey(documentViewModel, key.EntityListIndex, key.ChildDocumentListIndex);
        }

        private static PatchDetailsViewModel GetPatchDetailsViewModel_ByAlternativeKey(DocumentViewModel documentViewModel, int patchListIndex, int? childDocumentListIndex)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            IList<PatchDetailsViewModel> list = GetPatchDetailsViewModels_ByAlternativeKey(documentViewModel, childDocumentListIndex);

            return list[patchListIndex];
        }

        // Sample ViewModels

        public static SampleGridViewModel GetSampleGridViewModel_ByAlternativeKey(DocumentViewModel documentViewModel, ChildDocumentItemAlternativeKey key)
        {
            if (key == null) throw new NullException(() => key);
            return GetSampleGridViewModel_ByAlternativeKey(documentViewModel, key.ChildDocumentListIndex);
        }

        private static SampleGridViewModel GetSampleGridViewModel_ByAlternativeKey(DocumentViewModel documentViewModel, int? childDocumentListIndex)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            if (childDocumentListIndex.HasValue)
            {
                return documentViewModel.ChildDocumentList[childDocumentListIndex.Value].SampleGrid;
            }
            else
            {
                return documentViewModel.SampleGrid;
            }
        }

        public static SampleGridViewModel GetSampleGridViewModel_ByDocumentID(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel.ID == documentID)
            {
                return rootDocumentViewModel.SampleGrid;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel_ByID(rootDocumentViewModel, documentID);
                return childDocumentViewModel.SampleGrid;
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
                    EntityListIndex = listIndex.Value
                };
            }

            int? childDocumentListIndex = rootDocumentViewModel.ChildDocumentList
                                                               .TryGetIndexOf(x => x.SamplePropertiesList
                                                                                    .Any(y => y.Entity.ID == sampleID));
            if (childDocumentListIndex.HasValue)
            {
                listIndex = rootDocumentViewModel.ChildDocumentList[childDocumentListIndex.Value]
                                                 .SamplePropertiesList
                                                 .IndexOf(x => x.Entity.ID == sampleID);

                return new ChildDocumentItemAlternativeKey
                {
                    ChildDocumentListIndex = childDocumentListIndex.Value,
                    EntityListIndex = listIndex.Value
                };
            }

            throw new Exception(String.Format("documentViewModel does not have SamplePropertiesViewModel with ID '{0}' and neither do any ChildDocumentViewModels.", sampleID));
        }

        private static IList<SamplePropertiesViewModel> GetSamplePropertiesViewModels_ByAlternativeKey(DocumentViewModel documentViewModel, int? childDocumentListIndex)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            if (childDocumentListIndex.HasValue)
            {
                return documentViewModel.ChildDocumentList[childDocumentListIndex.Value].SamplePropertiesList;
            }
            else
            {
                return documentViewModel.SamplePropertiesList;
            }
        }

        public static SamplePropertiesViewModel GetSamplePropertiesViewModel_ByAlternativeKey(DocumentViewModel documentViewModel, ChildDocumentItemAlternativeKey key)
        {
            if (key == null) throw new NullException(() => key);
            return GetSamplePropertiesViewModel_ByAlternativeKey(documentViewModel, key.EntityListIndex, key.ChildDocumentListIndex);
        }

        private static SamplePropertiesViewModel GetSamplePropertiesViewModel_ByAlternativeKey(DocumentViewModel documentViewModel, int sampleListIndex, int? childDocumentListIndex)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            IList<SamplePropertiesViewModel> list = GetSamplePropertiesViewModels_ByAlternativeKey(documentViewModel, childDocumentListIndex);

            return list[sampleListIndex];
        }

        // Entities

        public static Curve TryGetCurve(Document rootDocument, int curveID)
        {
            Curve curve = rootDocument.EnumerateSelfAndChildDocuments()
                                      .SelectMany(x => x.Curves)
                                      .Where(x => x.ID == curveID)
                                      .SingleOrDefault();
            return curve;
        }

        public static Patch TryGetPatch(Document rootDocument, int patchID)
        {
            Patch patch = rootDocument.EnumerateSelfAndChildDocuments()
                                      .SelectMany(x => x.Patches)
                                      .Where(x => x.ID == patchID)
                                      .SingleOrDefault();
            return patch;
        }

        public static Sample TryGetSample(Document rootDocument, int sampleID)
        {
            Sample sample = rootDocument.EnumerateSelfAndChildDocuments()
                                        .SelectMany(x => x.Samples)
                                        .Where(x => x.ID == sampleID)
                                        .SingleOrDefault();
            return sample;
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
    }
}