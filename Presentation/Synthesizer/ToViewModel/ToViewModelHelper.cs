using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Collections;
using JJ.Framework.Common;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.ViewModels.Partials;

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

		public static HashSet<OperatorTypeEnum> OperatorTypeEnums_WithInterpolationPropertyViews { get; } =
			new HashSet<OperatorTypeEnum>
			{
				OperatorTypeEnum.Random,
				OperatorTypeEnum.Interpolate
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

		public static HashSet<OperatorTypeEnum> OperatorTypeEnums_WithStandardPropertiesView { get; } =
			EnumHelper.GetValues<OperatorTypeEnum>()
					  .Except(OperatorTypeEnums_WithCollectionRecalculationPropertyViews)
					  .Except(OperatorTypeEnums_WithInterpolationPropertyViews)
					  .Except(OperatorTypeEnums_WithSpecializedPropertiesViews)
					  .ToHashSet();

		// Document

		public static DocumentDeletedViewModel CreateDocumentDeletedViewModel()
		{
			var viewModel = new DocumentDeletedViewModel
			{
				ValidationMessages = new List<string>()
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
				DocumentProperties = new MenuItemViewModel { Visible = documentIsOpen },
				ValidationMessages = new List<string>(),
				Successful = true
			};

			return viewModel;
		}

		// Patch-Related

		/// <summary>
		/// Is used to be able to update an existing operator view model in-place
		/// without having to re-establish the intricate relations with other operators.
		/// </summary>
		public static void RefreshInletViewModels(
			IList<Inlet> sourceInlets,
			OperatorViewModel destOperatorViewModel,
			ICurveRepository curveRepository,
			EntityPositionFacade entityPositionFacade)
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

				inlet.ToViewModel(inletViewModel, curveRepository, entityPositionFacade);

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
				x => x.IsRepeating,
				x => x.RepetitionPosition,
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
			EntityPositionFacade entityPositionFacade)
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

				outlet.ToViewModel(outletViewModel, curveRepository, entityPositionFacade);

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
				x => x.IsRepeating,
				x => x.RepetitionPosition,
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
			ICurveRepository curveRepository,
			EntityPositionFacade entityPositionFacade)
		{
			RefreshViewModel(entity, operatorViewModel, curveRepository, entityPositionFacade);
			RefreshInletViewModels(entity.Inlets, operatorViewModel, curveRepository, entityPositionFacade);
			RefreshOutletViewModels(entity.Outlets, operatorViewModel, curveRepository, entityPositionFacade);
		}

		/// <summary>
		/// Is used to be able to update an existing operator view model in-place
		/// without having to re-establish the intricate relations with other operators.
		/// </summary>
		public static void RefreshViewModel(
			Operator entity,
			OperatorViewModel viewModel,
			ICurveRepository curveRepository,
			EntityPositionFacade entityPositionFacade)
		{
			if (entity == null) throw new NullException(() => entity);
			if (viewModel == null) throw new NullException(() => viewModel);

			viewModel.ID = entity.ID;
			viewModel.IsSmaller = GetOperatorIsSmaller(entity);
			viewModel.StyleGrade = StyleGradeEnum.StyleGradeNeutral;
			viewModel.Caption = GetCaption(entity);
			viewModel.IsOwned = entity.IsOwned();

			EntityPosition entityPosition = entityPositionFacade.GetOrCreateOperatorPosition(entity.ID);
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

		public static string FormatUsedInList(IList<IDAndName> idAndNames)
		{
			if (idAndNames == null) throw new NullException(() => idAndNames);

			string concatinatedUsedIn = string.Join(", ", idAndNames.Select(x => x.Name).OrderBy(x => x));

			return concatinatedUsedIn;
		}
	}
}