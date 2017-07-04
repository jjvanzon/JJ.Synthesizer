using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Common;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using JJ.Framework.Collections;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    /// <summary> Empty view models start out with Visible = false. </summary>
    internal static partial class ToViewModelHelper
    {
        // OperatorTypeEnum HashSets

        public static HashSet<OperatorTypeEnum> OperatorTypeEnums_WithCollectionRecalculationPropertyViews { get; } =
            new HashSet<OperatorTypeEnum>
            {
                OperatorTypeEnum.AverageOverDimension,
                OperatorTypeEnum.ClosestOverDimension,
                OperatorTypeEnum.ClosestOverDimensionExp,
                OperatorTypeEnum.MaxOverDimension,
                OperatorTypeEnum.MinOverDimension,
                OperatorTypeEnum.SortOverDimension,
                OperatorTypeEnum.SumOverDimension
            };

        public static HashSet<OperatorTypeEnum> OperatorTypeEnums_WithSpecializedPropertiesViews { get; } =
            new HashSet<OperatorTypeEnum>
            {
                OperatorTypeEnum.Cache,
                OperatorTypeEnum.Curve,
                OperatorTypeEnum.InletsToDimension,
                OperatorTypeEnum.Number,
                OperatorTypeEnum.PatchInlet,
                OperatorTypeEnum.PatchOutlet,
                OperatorTypeEnum.Sample
            };

        public static HashSet<OperatorTypeEnum> OperatorTypeEnums_WithInterpolationPropertyViews { get; } =
            new HashSet<OperatorTypeEnum>
            {
                OperatorTypeEnum.Random,
                OperatorTypeEnum.Interpolate
            };

        public static HashSet<OperatorTypeEnum> OperatorTypeEnums_WithStandardPropertiesView { get; } =
            EnumHelper.GetValues<OperatorTypeEnum>()
                      .Except(OperatorTypeEnums_WithCollectionRecalculationPropertyViews)
                      .Except(OperatorTypeEnums_WithSpecializedPropertiesViews)
                      .Except(OperatorTypeEnums_WithInterpolationPropertyViews)
                      .ToHashSet();

        // CurrentInstrument

        public static CurrentInstrumentViewModel CreateCurrentInstrumentViewModel(IList<Patch> patches, Document higherDocument)
        {
            if (patches == null) throw new NullException(() => patches);
            if (higherDocument == null) throw new NullException(() => higherDocument);

            Dictionary<int?, DocumentReference> documentReferenceDictionary = higherDocument.LowerDocumentReferences.ToDictionary(x => x.LowerDocument?.ID);

            var viewModel = new CurrentInstrumentViewModel
            {
                DocumentID = higherDocument.ID,
                List = patches.Select(x => toIDAndName(x)).ToList(),
                ValidationMessages = new List<MessageDto>()
            };

            IDAndName toIDAndName(Patch entity)
            {
                if (entity.Document?.ID == higherDocument.ID)
                {
                    return entity.ToIDAndName();
                }

                int? lowerDocumentID = entity.Document?.ID;
                DocumentReference documentReference = documentReferenceDictionary[lowerDocumentID];

                var idAndName = new IDAndName
                {
                    ID = entity.ID,
                    Name = $"{documentReference.GetAliasOrName()} | {entity.Name}"
                };

                return idAndName;
            }

            return viewModel;
        }

        public static CurrentInstrumentViewModel CreateCurrentInstrumentViewModelWithEmptyList(Document higherDocument)
        {
            if (higherDocument == null) throw new NullException(() => higherDocument);

            var viewModel = new CurrentInstrumentViewModel
            {
                DocumentID = higherDocument.ID,
                List = new List<IDAndName>(),
                ValidationMessages = new List<MessageDto>()
            };

            return viewModel;
        }

        // Document

        public static DocumentDeletedViewModel CreateDocumentDeletedViewModel()
        {
            var viewModel = new DocumentDeletedViewModel
            {
                ValidationMessages = new List<MessageDto>()
            };

            return viewModel;
        }

        public static DocumentOrPatchNotFoundPopupViewModel CreateDocumentOrPatchNotFoundPopupViewModel(string message = null)
        {
            DocumentOrPatchNotFoundPopupViewModel viewModel = CreateEmptyDocumentOrPatchNotFoundPopupViewModel();

            viewModel.NotFoundMessage = message;

            return viewModel;
        }

        // Menu

        public static MenuViewModel CreateMenuViewModel(bool documentIsOpen)
        {
            var viewModel = new MenuViewModel
            {
                DocumentList = new MenuItemViewModel { Visible = true },
                DocumentTree = new MenuItemViewModel { Visible = documentIsOpen },
                DocumentClose = new MenuItemViewModel { Visible = documentIsOpen },
                CurrentInstrument = new MenuItemViewModel { Visible = documentIsOpen },
                DocumentProperties = new MenuItemViewModel { Visible = documentIsOpen },
                ValidationMessages = new List<MessageDto>()
            };

            return viewModel;
        }

        // Patch-Related

        public static Dictionary<string, PatchGridViewModel> CreatePatchGridViewModelDictionary(
            IList<UsedInDto<Patch>> grouplessPatchUsedInDtos,
            IList<PatchGroupDto_WithUsedIn> patchGroupDtos,
            int rootDocumentID)
        {
            if (grouplessPatchUsedInDtos == null) throw new NullException(() => grouplessPatchUsedInDtos);
            if (patchGroupDtos == null) throw new NullException(() => patchGroupDtos);

            // ReSharper disable once UseObjectOrCollectionInitializer
            var list = new List<PatchGridViewModel>();

            list.Add(grouplessPatchUsedInDtos.ToGridViewModel(rootDocumentID, null));
            list.AddRange(patchGroupDtos.Select(x => x.PatchUsedInDtos.ToGridViewModel(rootDocumentID, x.GroupName)));

            return list.ToDictionary(x => NameHelper.ToCanonical(x.Group));
        }

        /// <summary>
        /// Is used to be able to update an existing operator view model in-place
        /// without having to re-establish the intricate relations with other operators.
        /// </summary>
        public static void RefreshInletViewModels(
            IList<Inlet> sourceInlets,
            OperatorViewModel destOperatorViewModel,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            EntityPositionManager entityPositionManager)
        {
            if (sourceInlets == null) throw new NullException(() => sourceInlets);
            if (destOperatorViewModel == null) throw new NullException(() => destOperatorViewModel);

            var inletViewModelsToKeep = new List<InletViewModel>(sourceInlets.Count);
            foreach (Inlet inlet in sourceInlets)
            {
                InletViewModel inletViewModel = destOperatorViewModel.Inlets.Where(x => x.ID == inlet.ID).FirstOrDefault();
                if (inletViewModel == null)
                {
                    inletViewModel = new InletViewModel();
                    destOperatorViewModel.Inlets.Add(inletViewModel);
                }

                inlet.ToViewModel(inletViewModel, curveRepository, sampleRepository, entityPositionManager);

                inletViewModelsToKeep.Add(inletViewModel);
            }

            IList<InletViewModel> existingInletViewModels = destOperatorViewModel.Inlets;
            IList<InletViewModel> inletViewModelsToDelete = existingInletViewModels.Except(inletViewModelsToKeep).ToArray();
            foreach (InletViewModel inletViewModelToDelete in inletViewModelsToDelete)
            {
                inletViewModelToDelete.InputOutlet = null;
                destOperatorViewModel.Inlets.Remove(inletViewModelToDelete);
            }

            destOperatorViewModel.Inlets = destOperatorViewModel.Inlets.Sort(
                x => x.Position,
                x => (DimensionEnum)x.Dimension.ID,
                x => x.Name,
                x => x.IsObsolete).ToList();
        }

        /// <summary>
        /// Is used to be able to update an existing operator view model in-place
        /// without having to re-establish the intricate relations with other operators.
        /// </summary>
        public static void RefreshOutletViewModels(
            IList<Outlet> sourceOutlets,
            OperatorViewModel destOperatorViewModel,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            EntityPositionManager entityPositionManager)
        {
            if (sourceOutlets == null) throw new NullException(() => sourceOutlets);
            if (destOperatorViewModel == null) throw new NullException(() => destOperatorViewModel);

            var outletViewModelsToKeep = new List<OutletViewModel>(sourceOutlets.Count);
            foreach (Outlet outlet in sourceOutlets)
            {
                OutletViewModel outletViewModel = destOperatorViewModel.Outlets.Where(x => x.ID == outlet.ID).FirstOrDefault();
                if (outletViewModel == null)
                {
                    outletViewModel = new OutletViewModel();
                    destOperatorViewModel.Outlets.Add(outletViewModel);

                    // The only inverse property in all the view models.
                    outletViewModel.Operator = destOperatorViewModel;
                }

                outlet.ToViewModel(outletViewModel, curveRepository, sampleRepository, entityPositionManager);

                outletViewModelsToKeep.Add(outletViewModel);
            }

            IList<OutletViewModel> existingOutletViewModels = destOperatorViewModel.Outlets;
            IList<OutletViewModel> outletViewModelsToDelete = existingOutletViewModels.Except(outletViewModelsToKeep).ToArray();
            foreach (OutletViewModel outletViewModelToDelete in outletViewModelsToDelete)
            {
                // The only inverse property in all the view models.
                outletViewModelToDelete.Operator = null;

                destOperatorViewModel.Outlets.Remove(outletViewModelToDelete);
            }

            destOperatorViewModel.Outlets = destOperatorViewModel.Outlets.Sort(
                x => x.Position,
                x => (DimensionEnum)x.Dimension.ID,
                x => x.Name,
                x => x.IsObsolete).ToList();
        }

        /// <summary>
        /// Is used to be able to update an existing operator view model in-place
        /// without having to re-establish the intricate relations with other operators.
        /// </summary>
        public static void RefreshViewModel_WithInletsAndOutlets(
            Operator entity,
            OperatorViewModel operatorViewModel,
            ISampleRepository sampleRepository,
            ICurveRepository curveRepository,
            EntityPositionManager entityPositionManager)
        {
            RefreshViewModel(entity, operatorViewModel, sampleRepository, curveRepository, entityPositionManager);
            RefreshInletViewModels(entity.Inlets, operatorViewModel, curveRepository, sampleRepository, entityPositionManager);
            RefreshOutletViewModels(entity.Outlets, operatorViewModel, curveRepository, sampleRepository, entityPositionManager);
        }

        /// <summary>
        /// Is used to be able to update an existing operator view model in-place
        /// without having to re-establish the intricate relations with other operators.
        /// </summary>
        public static void RefreshViewModel(
            Operator entity,
            OperatorViewModel viewModel,
            ISampleRepository sampleRepository,
            ICurveRepository curveRepository,
            EntityPositionManager entityPositionManager)
        {
            if (entity == null) throw new NullException(() => entity);
            if (viewModel == null) throw new NullException(() => viewModel);

            viewModel.ID = entity.ID;
            viewModel.StyleGrade = StyleGradeEnum.StyleGradeNeutral;
            viewModel.Caption = GetOperatorCaption(entity, sampleRepository, curveRepository);
            viewModel.IsOwned = GetOperatorIsOwned(entity);

            if (entity.OperatorType != null)
            {
                viewModel.OperatorType = entity.OperatorType.ToIDAndDisplayName();
            }
            else
            {
                viewModel.OperatorType = CreateEmptyIDAndName();
            }

            EntityPosition entityPosition = entityPositionManager.GetOrCreateOperatorPosition(entity.ID);
            viewModel.EntityPositionID = entityPosition.ID;
            viewModel.CenterX = entityPosition.X;
            viewModel.CenterY = entityPosition.Y;
            viewModel.Dimension = entity.ToDimensionViewModel();
        }

        public static DimensionViewModel ToDimensionViewModel(this Operator entity)
        {
            var viewModel = new DimensionViewModel
            {
                Key = GetDimensionKey(entity),
                Name = TryGetDimensionName(entity),
                Visible = MustStyleDimension(entity)
            };

            return viewModel;
        }

        // UsedIn

        public static string FormatUsedInDto(UsedInDto<Curve> dto)
        {
            if (dto == null) throw new NullException(() => dto);

            var sb = new StringBuilder();

            sb.Append(dto.Entity.Name);

            // ReSharper disable once InvertIf
            if (dto.UsedInIDAndNames.Count > 0)
            {
                string formattedUsedInList = FormatUsedInList(dto.UsedInIDAndNames);
                sb.AppendFormat(" ({0}: {1})", ResourceFormatter.UsedIn, formattedUsedInList);
            }

            return sb.ToString();
        }

        public static string FormatUsedInDto(UsedInDto<Sample> dto)
        {
            if (dto == null) throw new NullException(() => dto);

            var sb = new StringBuilder();

            sb.Append(dto.Entity.Name);

            // ReSharper disable once InvertIf
            if (dto.UsedInIDAndNames.Count > 0)
            {
                string formattedUsedInList = FormatUsedInList(dto.UsedInIDAndNames);
                sb.Append($" ({ResourceFormatter.UsedIn}: {formattedUsedInList})");
            }

            return sb.ToString();
        }

        public static string FormatUsedInList(IList<IDAndName> idAndNames)
        {
            if (idAndNames == null) throw new NullException(() => idAndNames);

            string concatinatedUsedIn = string.Join(", ", idAndNames.Select(x => x.Name).OrderBy(x => x));

            return concatinatedUsedIn;
        }
    }
}