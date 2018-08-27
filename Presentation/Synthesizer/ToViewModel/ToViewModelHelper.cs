using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;
using JJ.Framework.Common;
using JJ.Framework.Exceptions.Basic;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.ViewModels.Partials;

// ReSharper disable MemberCanBePrivate.Global

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
				OperatorTypeEnum.RandomStripe,
				OperatorTypeEnum.Interpolate
			};

		public static HashSet<OperatorTypeEnum> OperatorTypeEnums_WithFollowingMode { get; } =
			new HashSet<OperatorTypeEnum>
			{
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
		public static void RefreshInletViewModels(IList<Inlet> sourceInlets, OperatorViewModel destOperatorViewModel)
		{
			if (sourceInlets == null) throw new NullException(() => sourceInlets);
			if (destOperatorViewModel == null) throw new NullException(() => destOperatorViewModel);

			var inletViewModelsToKeep = new List<InletViewModel>(sourceInlets.Count);
			foreach (Inlet inlet in sourceInlets)
			{
				InletViewModel inletViewModel = destOperatorViewModel.Inlets.FirstOrDefault(x => x.ID == inlet.ID);
				if (inletViewModel == null)
				{
					inletViewModel = new InletViewModel();
					destOperatorViewModel.Inlets.Add(inletViewModel);
				}

				inlet.ToViewModel(inletViewModel);

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
				                                                    x => x.IsObsolete)
			                                                    .ToList();
		}

		/// <summary>
		/// Is used to be able to update an existing operator view model in-place
		/// without having to re-establish the intricate relations with other operators.
		/// </summary>
		public static void RefreshOutletViewModels(IList<Outlet> sourceOutlets, OperatorViewModel destOperatorViewModel)
		{
			if (sourceOutlets == null) throw new NullException(() => sourceOutlets);
			if (destOperatorViewModel == null) throw new NullException(() => destOperatorViewModel);

			var outletViewModelsToKeep = new List<OutletViewModel>(sourceOutlets.Count);
			foreach (Outlet outlet in sourceOutlets)
			{
				OutletViewModel outletViewModel = destOperatorViewModel.Outlets.FirstOrDefault(x => x.ID == outlet.ID);
				if (outletViewModel == null)
				{
					outletViewModel = new OutletViewModel();
					destOperatorViewModel.Outlets.Add(outletViewModel);

					// The only inverse property in all the view models.
					outletViewModel.Operator = destOperatorViewModel;
				}

				outlet.ToViewModel(outletViewModel);

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
				                                                     x => x.IsObsolete)
			                                                     .ToList();
		}

		/// <summary>
		/// Is used to be able to update an existing operator view model in-place
		/// without having to re-establish the intricate relations with other operators.
		/// </summary>
		public static void RefreshViewModel_WithInletsAndOutlets(Operator entity, OperatorViewModel operatorViewModel)
		{
			RefreshViewModel(entity, operatorViewModel);
			RefreshInletViewModels(entity.Inlets, operatorViewModel);
			RefreshOutletViewModels(entity.Outlets, operatorViewModel);
		}

		/// <summary>
		/// Is used to be able to update an existing operator view model in-place
		/// without having to re-establish the intricate relations with other operators.
		/// </summary>
		public static void RefreshViewModel(Operator entity, OperatorViewModel viewModel)
		{
			if (entity == null) throw new NullException(() => entity);
			if (viewModel == null) throw new NullException(() => viewModel);

			viewModel.ID = entity.ID;
			viewModel.IsSmaller = GetOperatorIsSmaller(entity);
			viewModel.StyleGrade = StyleGradeEnum.StyleGradeNeutral;
			viewModel.Caption = GetCaption(entity);
			viewModel.IsOwned = entity.IsOwned();
			viewModel.Dimension = entity.ToDimensionViewModel();

			EntityPosition entityPosition = entity.EntityPosition;
			viewModel.Position = entityPosition.ToViewModel();
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

        // TopButtonBar

	    public static TopButtonBarViewModel CreateTopButtonBarViewModel(bool documentIsOpen)
	    {
	        TopButtonBarViewModel viewModel = CreateEmptyTopButtonBarViewModel();
	        viewModel.Visible = documentIsOpen;
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