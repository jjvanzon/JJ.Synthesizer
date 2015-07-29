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
using System.Linq.Expressions;
using JJ.Framework.Reflection;

namespace JJ.Presentation.Synthesizer.Helpers
{
    internal static class ChildDocumentHelper
    {
        // ChildDocument

        public static ChildDocumentViewModel GetChildDocumentViewModel(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            ChildDocumentViewModel childDocumentViewModel = rootDocumentViewModel.ChildDocumentList
                                                                                 .Where(x => x.ID == childDocumentID)
                                                                                 .FirstOrDefault(); // First for performance.
            if (childDocumentViewModel == null)
            {
                throw new Exception(String.Format("ChildDocumentViewModel with ID '{0}' not found in documentViewModel.ChildDocumentList.", childDocumentID));
            }

            return childDocumentViewModel;
        }

        // Curve

        public static CurveDetailsViewModel GetCurveDetailsViewModel(DocumentViewModel rootDocumentViewModel, int curveID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            CurveDetailsViewModel detailsViewModel = ChildDocumentHelper.EnumerateCurveDetailsViewModels(rootDocumentViewModel)
                                                                        .FirstOrDefault(x => x.Entity.ID == curveID); // First for performance.
            if (detailsViewModel == null)
            {
                throw new Exception(String.Format("CurveDetailsViewModel with ID '{0}' not found in rootDocumentViewModel nor its ChildDocumentViewModels.", curveID));
            }

            return detailsViewModel;
        }

        public static CurveGridViewModel GetCurveGridViewModel_ByDocumentID(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.ID == documentID)
            {
                return rootDocumentViewModel.CurveGrid;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel(rootDocumentViewModel, documentID);
                return childDocumentViewModel.CurveGrid;
            }
        }

        public static IList<CurveDetailsViewModel> GetCurveDetailsViewModels_ByDocumentID(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.ID == documentID)
            {
                return rootDocumentViewModel.CurveDetailsList;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel(rootDocumentViewModel, documentID);
                return childDocumentViewModel.CurveDetailsList;
            }
        }

        private static IEnumerable<CurveDetailsViewModel> EnumerateCurveDetailsViewModels(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (CurveDetailsViewModel curveDetailsViewModel in rootDocumentViewModel.CurveDetailsList)
            {
                yield return curveDetailsViewModel;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                foreach (CurveDetailsViewModel curveDetailsViewModel in childDocumentViewModel.CurveDetailsList)
                {
                    yield return curveDetailsViewModel;
                }
            }
        }

        // Patch

        public static PatchDetailsViewModel GetPatchDetailsViewModel(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            PatchDetailsViewModel detailsViewModel = ChildDocumentHelper.EnumeratePatchDetailsViewModels(rootDocumentViewModel)
                                                                        .FirstOrDefault(x => x.Entity.ID == patchID); // First for performance.
            if (detailsViewModel == null)
            {
                throw new Exception(String.Format("PatchDetailsViewModel with ID '{0}' not found in rootDocumentViewModel nor its ChildDocumentViewModels.", patchID));
            }

            return detailsViewModel;
        }

        public static PatchGridViewModel GetPatchGridViewModel_ByDocumentID(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.ID == documentID)
            {
                return rootDocumentViewModel.PatchGrid;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel(rootDocumentViewModel, documentID);
                return childDocumentViewModel.PatchGrid;
            }
        }

        public static IList<PatchDetailsViewModel> GetPatchDetailsViewModels_ByDocumentID(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.ID == documentID)
            {
                return rootDocumentViewModel.PatchDetailsList;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel(rootDocumentViewModel, documentID);
                return childDocumentViewModel.PatchDetailsList;
            }
        }

        private static IEnumerable<PatchDetailsViewModel> EnumeratePatchDetailsViewModels(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDetailsViewModel patchDetailsViewModel in rootDocumentViewModel.PatchDetailsList)
            {
                yield return patchDetailsViewModel;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                foreach (PatchDetailsViewModel patchDetailsViewModel in childDocumentViewModel.PatchDetailsList)
                {
                    yield return patchDetailsViewModel;
                }
            }
        }

        // Sample

        public static SamplePropertiesViewModel GetSamplePropertiesViewModel(DocumentViewModel rootDocumentViewModel, int sampleID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            SamplePropertiesViewModel propertiesViewModel = ChildDocumentHelper.EnumerateSamplePropertiesViewModels(rootDocumentViewModel)
                                                                               .FirstOrDefault(x => x.Entity.ID == sampleID); // First for performance.
            if (propertiesViewModel == null)
            {
                throw new Exception(String.Format("SamplePropertiesViewModel with ID '{0}' not found in rootDocumentViewModel nor its ChildDocumentViewModels.", sampleID));
            }

            return propertiesViewModel;
        }

        public static IList<SamplePropertiesViewModel> GetSamplePropertiesViewModels_ByDocumentID(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.ID == documentID)
            {
                return rootDocumentViewModel.SamplePropertiesList;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel(rootDocumentViewModel, documentID);
                return childDocumentViewModel.SamplePropertiesList;
            }
        }

        public static SampleGridViewModel GetSampleGridViewModel_BySampleID(DocumentViewModel rootDocumentViewModel, int sampleID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            SampleGridViewModel gridViewModel = ChildDocumentHelper.EnumerateSampleGridViewModels(rootDocumentViewModel)
                                                                   .Where(x => x.List.Any(y => y.ID == sampleID))
                                                                   .Single();
            return gridViewModel;
        }

        public static SampleGridViewModel GetSampleGridViewModel_ByDocumentID(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.ID == documentID)
            {
                return rootDocumentViewModel.SampleGrid;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel(rootDocumentViewModel, documentID);
                return childDocumentViewModel.SampleGrid;
            }
        }

        private static IEnumerable<SampleGridViewModel> EnumerateSampleGridViewModels(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            yield return rootDocumentViewModel.SampleGrid;

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                yield return childDocumentViewModel.SampleGrid;
            }
        }

        private static IEnumerable<SamplePropertiesViewModel> EnumerateSamplePropertiesViewModels(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (SamplePropertiesViewModel samplePropertiesViewModel in rootDocumentViewModel.SamplePropertiesList)
            {
                yield return samplePropertiesViewModel;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                foreach (SamplePropertiesViewModel samplePropertiesViewModel in childDocumentViewModel.SamplePropertiesList)
                {
                    yield return samplePropertiesViewModel;
                }
            }

        }
    }
}