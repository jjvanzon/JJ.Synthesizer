using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Canonical;
using JJ.Framework.Configuration;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Framework.Common;
using JJ.Business.Synthesizer.Helpers;
using System.Text;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Business.Synthesizer;
using JJ.Framework.Mathematics;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    /// <summary> Empty view models start out with Visible = false. </summary>
    internal static partial class ViewModelHelper
    {
        private const int STRETCH_AND_SQUASH_ORIGIN_LIST_INDEX = 2;
        private static readonly bool _showAutoPatchPolyphonicEnabled = CustomConfigurationManager.GetSection<ConfigurationSection>().ShowAutoPatchPolyphonicEnabled;

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
            OperatorTypeEnum.Resample
        };

        public static HashSet<OperatorTypeEnum> OperatorTypeEnums_WithOutletCountPropertyViews { get; } =
                  new HashSet<OperatorTypeEnum>
        {
            OperatorTypeEnum.MakeDiscrete,
            OperatorTypeEnum.Unbundle
        };

        public static HashSet<OperatorTypeEnum> OperatorTypeEnums_WithDimension { get; } =
                  new HashSet<OperatorTypeEnum>
        {
            OperatorTypeEnum.AverageFollower,
            OperatorTypeEnum.AverageOverDimension,
            OperatorTypeEnum.Bundle,
            OperatorTypeEnum.Cache,
            OperatorTypeEnum.ClosestOverDimension,
            OperatorTypeEnum.ClosestOverDimensionExp,
            OperatorTypeEnum.Curve,
            OperatorTypeEnum.GetDimension,
            OperatorTypeEnum.Loop,
            OperatorTypeEnum.MakeContinuous,
            OperatorTypeEnum.MakeDiscrete,
            OperatorTypeEnum.MaxFollower,
            OperatorTypeEnum.MaxOverDimension,
            OperatorTypeEnum.MinFollower,
            OperatorTypeEnum.MinOverDimension,
            OperatorTypeEnum.Noise,
            OperatorTypeEnum.PatchInlet,
            OperatorTypeEnum.PatchOutlet,
            OperatorTypeEnum.Pulse,
            OperatorTypeEnum.Random,
            OperatorTypeEnum.Range,
            OperatorTypeEnum.Resample,
            OperatorTypeEnum.Reverse,
            OperatorTypeEnum.Sample,
            OperatorTypeEnum.SawDown,
            OperatorTypeEnum.SawUp,
            OperatorTypeEnum.Select,
            OperatorTypeEnum.SetDimension,
            OperatorTypeEnum.Shift,
            OperatorTypeEnum.Sine,
            OperatorTypeEnum.SortOverDimension,
            OperatorTypeEnum.Spectrum,
            OperatorTypeEnum.Square,
            OperatorTypeEnum.Squash,
            OperatorTypeEnum.Stretch,
            OperatorTypeEnum.SumFollower,
            OperatorTypeEnum.SumOverDimension,
            OperatorTypeEnum.TimePower,
            OperatorTypeEnum.Triangle,
            OperatorTypeEnum.Unbundle
        };

        public static HashSet<OperatorTypeEnum> OperatorTypeEnums_WithInletCountPropertyViews { get; } =
                  new HashSet<OperatorTypeEnum>
        {
            OperatorTypeEnum.Add,
            OperatorTypeEnum.Average,
            OperatorTypeEnum.Closest,
            OperatorTypeEnum.ClosestExp,
            OperatorTypeEnum.Max,
            OperatorTypeEnum.Min,
            OperatorTypeEnum.Multiply,
            OperatorTypeEnum.Sort
        };

        public static HashSet<OperatorTypeEnum> OperatorTypeEnums_WithTheirOwnPropertyViews { get; } =
                  new HashSet<OperatorTypeEnum>
        {
            OperatorTypeEnum.Bundle,
            OperatorTypeEnum.Cache,
            OperatorTypeEnum.Curve,
            OperatorTypeEnum.CustomOperator,
            OperatorTypeEnum.MakeContinuous,
            OperatorTypeEnum.Number,
            OperatorTypeEnum.PatchInlet,
            OperatorTypeEnum.PatchOutlet,
            OperatorTypeEnum.Sample
        };

        public static HashSet<OperatorTypeEnum> OperatorTypeEnums_WithoutAlternativePropertiesView { get; } =
            EnumHelper.GetValues<OperatorTypeEnum>().Except(OperatorTypeEnums_WithTheirOwnPropertyViews)
                                                    .Except(OperatorTypeEnums_WithInterpolationPropertyViews)
                                                    .Except(OperatorTypeEnums_WithCollectionRecalculationPropertyViews)
                                                    .Except(OperatorTypeEnums_WithOutletCountPropertyViews)
                                                    .Except(OperatorTypeEnums_WithInletCountPropertyViews)
                                                    .ToHashSet();

        public static HashSet<OperatorTypeEnum> OperatorTypeEnums_WithStyledDimension { get; } =
                  new HashSet<OperatorTypeEnum>
        {
            OperatorTypeEnum.Sine,
            OperatorTypeEnum.TimePower,
            OperatorTypeEnum.Curve,
            OperatorTypeEnum.Sample,
            OperatorTypeEnum.Noise,
            OperatorTypeEnum.Resample,
            OperatorTypeEnum.SawUp,
            OperatorTypeEnum.Square,
            OperatorTypeEnum.Triangle,
            OperatorTypeEnum.Loop,
            OperatorTypeEnum.Select,
            OperatorTypeEnum.Bundle,
            OperatorTypeEnum.Unbundle,
            OperatorTypeEnum.Stretch,
            OperatorTypeEnum.Squash,
            OperatorTypeEnum.Shift,
            OperatorTypeEnum.Spectrum,
            OperatorTypeEnum.Pulse,
            OperatorTypeEnum.Random,
            OperatorTypeEnum.MinFollower,
            OperatorTypeEnum.MaxFollower,
            OperatorTypeEnum.AverageFollower,
            OperatorTypeEnum.SawDown,
            OperatorTypeEnum.Reverse,
            OperatorTypeEnum.Cache,
            // Specifically not:
            // OperatorTypeEnum.GetDimension
            // OperatorTypeEnum.SetDimension
            OperatorTypeEnum.Range,
            OperatorTypeEnum.MakeDiscrete,
            OperatorTypeEnum.MakeContinuous,
            OperatorTypeEnum.MaxOverDimension,
            OperatorTypeEnum.MinOverDimension,
            OperatorTypeEnum.AverageOverDimension,
            OperatorTypeEnum.SumOverDimension,
            OperatorTypeEnum.SumFollower,
            OperatorTypeEnum.ClosestOverDimension,
            OperatorTypeEnum.ClosestOverDimensionExp,
            OperatorTypeEnum.SortOverDimension
        };

        public static HashSet<OperatorTypeEnum> OperatorTypeEnums_WithHiddenInletNames { get; } =
                  new HashSet<OperatorTypeEnum>
        {
            OperatorTypeEnum.Add,
            //OperatorTypeEnum.Divide,
            //OperatorTypeEnum.MultiplyWithOrigin,
            OperatorTypeEnum.PatchInlet,
            OperatorTypeEnum.PatchOutlet,
            //OperatorTypeEnum.Power,
            //OperatorTypeEnum.Sine,
            OperatorTypeEnum.Subtract,
            //OperatorTypeEnum.TimePower,
            OperatorTypeEnum.Number,
            OperatorTypeEnum.Curve,
            //OperatorTypeEnum.Sample,
            OperatorTypeEnum.Noise,
            //OperatorTypeEnum.Resample,
            //OperatorTypeEnum.CustomOperator,
            //OperatorTypeEnum.SawUp,
            //OperatorTypeEnum.Square,
            //OperatorTypeEnum.Triangle,
            //OperatorTypeEnum.Exponent,
            //OperatorTypeEnum.Loop,
            //OperatorTypeEnum.Select,
            OperatorTypeEnum.Bundle,
            OperatorTypeEnum.Unbundle,
            //OperatorTypeEnum.Stretch,
            //OperatorTypeEnum.Squash,
            //OperatorTypeEnum.Shift,
            //OperatorTypeEnum.Reset,
            //OperatorTypeEnum.LowPassFilter,
            //OperatorTypeEnum.HighPassFilter,
            //OperatorTypeEnum.Spectrum,
            //OperatorTypeEnum.Pulse,
            //OperatorTypeEnum.Random,
            OperatorTypeEnum.Equal,
            OperatorTypeEnum.NotEqual,
            OperatorTypeEnum.LessThan,
            OperatorTypeEnum.GreaterThan,
            OperatorTypeEnum.LessThanOrEqual,
            OperatorTypeEnum.GreaterThanOrEqual,
            OperatorTypeEnum.And,
            OperatorTypeEnum.Or,
            OperatorTypeEnum.Not,
            //OperatorTypeEnum.If,
            //OperatorTypeEnum.MinFollower,
            //OperatorTypeEnum.MaxFollower,
            //OperatorTypeEnum.AverageFollower,
            //OperatorTypeEnum.Scaler,
            //OperatorTypeEnum.SawDown,
            OperatorTypeEnum.Absolute,
            //OperatorTypeEnum.Reverse,
            //OperatorTypeEnum.Round,
            OperatorTypeEnum.Negative,
            OperatorTypeEnum.OneOverX,
            //OperatorTypeEnum.Cache,
            //OperatorTypeEnum.PulseTrigger,
            //OperatorTypeEnum.ChangeTrigger,
            //OperatorTypeEnum.ToggleTrigger,
            OperatorTypeEnum.GetDimension,
            //OperatorTypeEnum.SetDimension,
            OperatorTypeEnum.Hold,
            //OperatorTypeEnum.Range,
            OperatorTypeEnum.MakeDiscrete,
            OperatorTypeEnum.MakeContinuous,
            OperatorTypeEnum.Max,
            OperatorTypeEnum.Min,
            OperatorTypeEnum.Average,
            //OperatorTypeEnum.MaxOverDimension,
            //OperatorTypeEnum.MinOverDimension,
            //OperatorTypeEnum.AverageOverDimension,
            //OperatorTypeEnum.SumOverDimension,
            //OperatorTypeEnum.SumFollower,
            OperatorTypeEnum.Multiply,
            //OperatorTypeEnum.Closest,
            //OperatorTypeEnum.ClosestOverDimension,
            //OperatorTypeEnum.ClosestExp,
            //OperatorTypeEnum.ClosestOverDimensionExp,
            //OperatorTypeEnum.Sort,
            //OperatorTypeEnum.SortOverDimension,
            //OperatorTypeEnum.BandPassFilterConstantTransitionGain,
            //OperatorTypeEnum.BandPassFilterConstantPeakGain,
            //OperatorTypeEnum.NotchFilter,
            //OperatorTypeEnum.AllPassFilter,
            //OperatorTypeEnum.PeakingEQFilter,
            //OperatorTypeEnum.LowShelfFilter,
            //OperatorTypeEnum.HighShelfFilter
        };

        // A list until it will have more items.
        public static List<OperatorTypeEnum> OperatorTypeEnums_WithVisibleOutletNames { get; } =
                  new List<OperatorTypeEnum>
        {
            //OperatorTypeEnum.Absolute,
            //OperatorTypeEnum.Add,
            //OperatorTypeEnum.AllPassFilter,
            //OperatorTypeEnum.And,
            //OperatorTypeEnum.Average,
            //OperatorTypeEnum.AverageFollower,
            //OperatorTypeEnum.AverageOverDimension,
            //OperatorTypeEnum.BandPassFilterConstantPeakGain,
            //OperatorTypeEnum.BandPassFilterConstantTransitionGain,
            //OperatorTypeEnum.Bundle,
            //OperatorTypeEnum.Cache,
            OperatorTypeEnum.ChangeTrigger,
            //OperatorTypeEnum.Closest,
            //OperatorTypeEnum.ClosestExp,
            //OperatorTypeEnum.ClosestOverDimension,
            //OperatorTypeEnum.ClosestOverDimensionExp,
            //OperatorTypeEnum.Curve,
            OperatorTypeEnum.CustomOperator,
            //OperatorTypeEnum.Divide,
            //OperatorTypeEnum.Equal,
            //OperatorTypeEnum.Exponent,
            //OperatorTypeEnum.GetDimension,
            //OperatorTypeEnum.GreaterThan,
            //OperatorTypeEnum.GreaterThanOrEqual,
            //OperatorTypeEnum.HighPassFilter,
            //OperatorTypeEnum.HighShelfFilter,
            //OperatorTypeEnum.Hold,
            //OperatorTypeEnum.If,
            //OperatorTypeEnum.LessThan,
            //OperatorTypeEnum.LessThanOrEqual,
            //OperatorTypeEnum.Loop,
            //OperatorTypeEnum.LowPassFilter,
            //OperatorTypeEnum.LowShelfFilter,
            //OperatorTypeEnum.MakeContinuous,
            //OperatorTypeEnum.MakeDiscrete,
            //OperatorTypeEnum.Max,
            //OperatorTypeEnum.MaxFollower,
            //OperatorTypeEnum.MaxOverDimension,
            //OperatorTypeEnum.Min,
            //OperatorTypeEnum.MinFollower,
            //OperatorTypeEnum.MinOverDimension,
            //OperatorTypeEnum.Multiply,
            //OperatorTypeEnum.MultiplyWithOrigin,
            //OperatorTypeEnum.Negative,
            //OperatorTypeEnum.Noise,
            //OperatorTypeEnum.Not,
            //OperatorTypeEnum.NotchFilter,
            //OperatorTypeEnum.NotEqual,
            //OperatorTypeEnum.Number,
            //OperatorTypeEnum.OneOverX,
            //OperatorTypeEnum.Or,
            //OperatorTypeEnum.PatchInlet,
            //OperatorTypeEnum.PatchOutlet,
            //OperatorTypeEnum.PeakingEQFilter,
            //OperatorTypeEnum.Power,
            //OperatorTypeEnum.Pulse,
            OperatorTypeEnum.PulseTrigger,
            //OperatorTypeEnum.Random,
            //OperatorTypeEnum.Range,
            //OperatorTypeEnum.Resample,
            OperatorTypeEnum.Reset,
            //OperatorTypeEnum.Reverse,
            //OperatorTypeEnum.Round,
            //OperatorTypeEnum.Sample,
            //OperatorTypeEnum.SawDown,
            //OperatorTypeEnum.SawUp,
            //OperatorTypeEnum.Scaler,
            //OperatorTypeEnum.Select,
            OperatorTypeEnum.SetDimension,
            //OperatorTypeEnum.Shift,
            //OperatorTypeEnum.Sine,
            //OperatorTypeEnum.Sort,
            //OperatorTypeEnum.SortOverDimension,
            //OperatorTypeEnum.Spectrum,
            //OperatorTypeEnum.Square,
            //OperatorTypeEnum.Squash,
            //OperatorTypeEnum.Stretch,
            //OperatorTypeEnum.Subtract,
            //OperatorTypeEnum.SumFollower,
            //OperatorTypeEnum.SumOverDimension,
            //OperatorTypeEnum.TimePower,
            OperatorTypeEnum.ToggleTrigger,
            //OperatorTypeEnum.Triangle,
            //OperatorTypeEnum.Unbundle
        };

        // CurrentPatches

        public static CurrentPatchesViewModel CreateCurrentPatchesViewModel(IList<Patch> patches)
        {
            if (patches == null) throw new NullException(() => patches);

            var viewModel = new CurrentPatchesViewModel
            {
                List = patches.Select(x => x.ToIDAndName()).ToList(),
                CanShowAutoPatchPolyphonic = _showAutoPatchPolyphonicEnabled,
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        // Document

        public static DocumentDeletedViewModel CreateDocumentDeletedViewModel()
        {
            var viewModel = new DocumentDeletedViewModel
            {
                ValidationMessages = new List<Message>()
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
                DocumentSave = new MenuItemViewModel { Visible = documentIsOpen },
                CurrentPatches = new MenuItemViewModel { Visible = documentIsOpen },
                DocumentProperties = new MenuItemViewModel { Visible = documentIsOpen },
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        // Tone Grid

        public static string GetToneGridEditNumberTitle(Scale entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return ResourceHelper.GetScaleTypeDisplayNameSingular(entity);
        }

        // Patch-Related

        public static Dictionary<string, PatchGridViewModel> CreatePatchGridViewModelDictionary(
            IList<UsedInDto<Patch>> grouplessPatchUsedInDtos,
            IList<PatchGroupDto_WithUsedIn> patchGroupDtos,
            int rootDocumentID)
        {
            if (grouplessPatchUsedInDtos == null) throw new NullException(() => grouplessPatchUsedInDtos);
            if (patchGroupDtos == null) throw new NullException(() => patchGroupDtos);

            var list = new List<PatchGridViewModel>();

            list.Add(grouplessPatchUsedInDtos.ToPatchGridViewModel(rootDocumentID, null));
            list.AddRange(patchGroupDtos.Select(x => x.PatchUsedInDtos.ToPatchGridViewModel(rootDocumentID, x.GroupName)));

            return list.ToDictionary(x => x.Group?.ToLower() ?? "");
        }

        public static bool GetInletVisible(Inlet inlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            if (inlet.InputOutlet != null)
            {
                return true;
            }

            Operator op = inlet.Operator;

            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();

            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.PatchInlet:
                    return false;

                case OperatorTypeEnum.Stretch:
                case OperatorTypeEnum.Squash:
                    if (inlet.ListIndex == STRETCH_AND_SQUASH_ORIGIN_LIST_INDEX)
                    {
                        var wrapper = new Stretch_OperatorWrapper(inlet.Operator);
                        if (op.GetDimensionEnum() == DimensionEnum.Time)
                        {
                            return false;
                        }
                    }
                    break;
            }

            return true;
        }

        public static bool GetOutletVisible(Outlet outlet)
        {
            if (outlet == null) throw new NullException(() => outlet);

            OperatorTypeEnum operatorTypeEnum = outlet.Operator.GetOperatorTypeEnum();

            if (operatorTypeEnum == OperatorTypeEnum.PatchOutlet)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// A Number Operator can be considered 'owned' by another operator if
        /// it is the only operator it is connected to.
        /// In that case it is convenient that the Number Operator moves along
        /// with the operator it is connected to.
        /// In the Vector Graphics we accomplish this by making the Number Operator Rectangle a child of the owning Operator's Rectangle. 
        /// But also in the MoveOperator action we must move the owned operators along with their owner.
        /// </summary>
        public static bool GetOperatorIsOwned(Operator entity)
        {
            if (entity.Outlets.Count > 0)
            {
                bool isOwned = entity.GetOperatorTypeEnum() == OperatorTypeEnum.Number &&
                               // Make sure the connected inlets are all of the same operator.
                               entity.Outlets[0].ConnectedInlets.Select(x => x.Operator).Distinct().Count() == 1;

                return isOwned;
            }

            return false;
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
            IPatchRepository patchRepository,
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

                inlet.ToViewModel(inletViewModel, curveRepository, sampleRepository, patchRepository, entityPositionManager);

                inletViewModelsToKeep.Add(inletViewModel);
            }

            IList<InletViewModel> existingInletViewModels = destOperatorViewModel.Inlets;
            IList<InletViewModel> inletViewModelsToDelete = existingInletViewModels.Except(inletViewModelsToKeep).ToArray();
            foreach (InletViewModel inletViewModelToDelete in inletViewModelsToDelete)
            {
                inletViewModelToDelete.InputOutlet = null;
                destOperatorViewModel.Inlets.Remove(inletViewModelToDelete);
            }

            destOperatorViewModel.Inlets = destOperatorViewModel.Inlets.OrderBy(x => x.ListIndex).ToList();
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
            IPatchRepository patchRepository,
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

                outlet.ToViewModel(outletViewModel, curveRepository, sampleRepository, patchRepository, entityPositionManager);

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

            destOperatorViewModel.Outlets = destOperatorViewModel.Outlets.OrderBy(x => x.ListIndex).ToList();
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
            IPatchRepository patchRepository,
            EntityPositionManager entityPositionManager)
        {
            RefreshViewModel(entity, operatorViewModel, sampleRepository, curveRepository, patchRepository, entityPositionManager);
            RefreshInletViewModels(entity.Inlets, operatorViewModel, curveRepository, sampleRepository, patchRepository, entityPositionManager);
            RefreshOutletViewModels(entity.Outlets, operatorViewModel, curveRepository, sampleRepository, patchRepository, entityPositionManager);
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
            IPatchRepository patchRepository,
            EntityPositionManager entityPositionManager)
        {
            if (entity == null) throw new NullException(() => entity);
            if (viewModel == null) throw new NullException(() => viewModel);

            viewModel.ID = entity.ID;
            viewModel.Caption = GetOperatorCaption(entity, sampleRepository, curveRepository, patchRepository);
            viewModel.OperatorType = entity.OperatorType?.ToIDAndDisplayName();
            viewModel.IsOwned = GetOperatorIsOwned(entity);

            EntityPosition entityPosition = entityPositionManager.GetOrCreateOperatorPosition(entity.ID);
            viewModel.EntityPositionID = entityPosition.ID;
            viewModel.CenterX = entityPosition.X;
            viewModel.CenterY = entityPosition.Y;
        }

        public static string GetOperatorCaption(
            Operator op,
            ISampleRepository sampleRepository,
            ICurveRepository curveRepository,
            IPatchRepository patchRepository)
        {
            if (op == null) throw new NullException(() => op);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (patchRepository == null) throw new NullException(() => patchRepository);

            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();

            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.Curve:
                    return GetOperatorCaption_ForCurve(op, curveRepository);

                case OperatorTypeEnum.CustomOperator:
                    return GetOperatorCaption_ForCustomOperator(op, patchRepository);

                case OperatorTypeEnum.Number:
                    return GetOperatorCaption_ForNumber(op);

                case OperatorTypeEnum.PatchInlet:
                    return GetOperatorCaption_ForPatchInlet(op);

                case OperatorTypeEnum.PatchOutlet:
                    return GetOperatorCaption_ForPatchOutlet(op);

                case OperatorTypeEnum.Sample:
                    return GetOperatorCaption_ForSample(op, sampleRepository);

                case OperatorTypeEnum.GetDimension:
                    return GetOperatorCaption_ForGetDimension(op);

                case OperatorTypeEnum.SetDimension:
                    return GetOperatorCaption_ForSetDimension(op);

                case OperatorTypeEnum.MaxOverDimension:
                    return GetOperatorCaption_ForMaxOverDimension(op);

                case OperatorTypeEnum.MinOverDimension:
                    return GetOperatorCaption_ForMinOverDimension(op);

                case OperatorTypeEnum.AverageOverDimension:
                    return GetOperatorCaption_ForAverageOverDimension(op);

                case OperatorTypeEnum.SumOverDimension:
                    return GetOperatorCaption_ForSumOverDimension(op);

                case OperatorTypeEnum.ClosestOverDimension:
                    return GetOperatorCaption_ForClosestOverDimension(op);

                case OperatorTypeEnum.ClosestOverDimensionExp:
                    return GetOperatorCaption_ForClosestOverDimensionExp(op);

                case OperatorTypeEnum.SortOverDimension:
                    return GetOperatorCaption_ForSortOverDimension(op);

                default:
                    return GetOperatorCaption_ForOtherOperators(op);
            }
        }

        private static string GetOperatorCaption_ForCurve(Operator op, ICurveRepository curveRepository)
        {
            string operatorTypeDisplayName = PropertyDisplayNames.Curve;

            // Use Operator.Name
            if (!String.IsNullOrWhiteSpace(op.Name))
            {
                return String.Format("{0}: {1}", operatorTypeDisplayName, op.Name);
            }

            // Use Curve.Name
            var wrapper = new Curve_OperatorWrapper(op, curveRepository);
            Curve underlyingEntity = wrapper.Curve;
            if (underlyingEntity != null)
            {
                if (!String.IsNullOrWhiteSpace(underlyingEntity.Name))
                {
                    return String.Format("{0}: {1}", operatorTypeDisplayName, underlyingEntity.Name);
                }
            }

            // Use OperatorTypeDisplayName
            string caption = operatorTypeDisplayName;
            return caption;
        }

        private static string GetOperatorCaption_ForCustomOperator(Operator op, IPatchRepository patchRepository)
        {
            // Use Operator.Name
            if (!String.IsNullOrWhiteSpace(op.Name))
            {
                return op.Name;
            }

            // Use UnderlyingPatch.Name
            var wrapper = new CustomOperator_OperatorWrapper(op, patchRepository);
            Patch underlyingPatch = wrapper.UnderlyingPatch;
            if (underlyingPatch != null)
            {
                if (!String.IsNullOrWhiteSpace(underlyingPatch.Name))
                {
                    return underlyingPatch.Name;
                }
            }

            // Use OperatorTypeDisplayName
            string caption = ResourceHelper.GetDisplayName(op.GetOperatorTypeEnum());
            return caption;
        }

        /// <summary> Value Operator: display name and value or only value. </summary>
        private static string GetOperatorCaption_ForNumber(Operator op)
        {
            var wrapper = new Number_OperatorWrapper(op);
            string formattedValue = wrapper.Number.ToString("0.####");

            if (String.IsNullOrWhiteSpace(op.Name))
            {
                return formattedValue;
            }
            else
            {
                return String.Format("{0}: {1}", op.Name, formattedValue);
            }
        }

        private static string GetOperatorCaption_ForPatchInlet(Operator op)
        {
            var sb = new StringBuilder();

            var wrapper = new PatchInlet_OperatorWrapper(op);
            Inlet inlet = wrapper.Inlet;
            DimensionEnum dimensionEnum = inlet.GetDimensionEnum();

            // Use OperatorType DisplayName
            sb.Append(PropertyDisplayNames.Inlet);

            // Try Use Operator Name
            if (!String.IsNullOrWhiteSpace(op.Name))
            {
                sb.AppendFormat(": {0}", op.Name);
            }
            // Try Use Dimension
            else if (dimensionEnum != DimensionEnum.Undefined)
            {
                sb.AppendFormat(": {0}", ResourceHelper.GetDisplayName(dimensionEnum));
            }
            // Try Use List Index
            else
            {
                sb.AppendFormat(" {0}", wrapper.ListIndex + 1);
            }

            // Try Use DefaultValue
            double? defaultValue = inlet.DefaultValue;
            if (defaultValue.HasValue)
            {
                sb.AppendFormat(" = {0}", defaultValue.Value);
            }

            return sb.ToString();
        }

        private static string GetOperatorCaption_ForPatchOutlet(Operator op)
        {
            var sb = new StringBuilder();

            var wrapper = new PatchOutlet_OperatorWrapper(op);
            Outlet outlet = wrapper.Result;
            DimensionEnum dimensionEnum = outlet.GetDimensionEnum();

            // Use OperatorType DisplayName
            sb.Append(PropertyDisplayNames.Outlet);

            // Try Use Operator Name
            if (!String.IsNullOrWhiteSpace(op.Name))
            {
                sb.AppendFormat(": {0}", op.Name);
            }
            // Try Use Dimension
            else if (dimensionEnum != DimensionEnum.Undefined)
            {
                sb.AppendFormat(": {0}", ResourceHelper.GetDisplayName(dimensionEnum));
            }
            // Try Use List Index
            else
            {
                sb.AppendFormat(" {0}", wrapper.ListIndex + 1);
            }

            return sb.ToString(); ;
        }

        private static string GetOperatorCaption_ForSample(Operator op, ISampleRepository sampleRepository)
        {
            string operatorTypeDisplayName = PropertyDisplayNames.Sample;

            // Use Operator.Name
            if (!String.IsNullOrWhiteSpace(op.Name))
            {
                return String.Format("{0}: {1}", operatorTypeDisplayName, op.Name);
            }

            // Use Sample.Name
            var wrapper = new Sample_OperatorWrapper(op, sampleRepository);
            Sample underlyingEntity = wrapper.Sample;
            if (underlyingEntity != null)
            {
                if (!String.IsNullOrWhiteSpace(underlyingEntity.Name))
                {
                    return String.Format("{0}: {1}", operatorTypeDisplayName, underlyingEntity.Name);
                }
            }

            // Use OperatorType DisplayName
            string caption = operatorTypeDisplayName;
            return caption;
        }

        private static string GetOperatorCaption_ForGetDimension(Operator op)
        {
            return GetOperatorCaption_WithDimensionPlaceholder(op, Titles.GetDimensionWithPlaceholder);
        }

        private static string GetOperatorCaption_ForSetDimension(Operator op)
        {
            return GetOperatorCaption_WithDimensionPlaceholder(op, Titles.SetDimensionWithPlaceholder);
        }

        private static string GetOperatorCaption_ForMaxOverDimension(Operator op)
        {
            return PropertyDisplayNames.Max;
        }

        private static string GetOperatorCaption_ForMinOverDimension(Operator op)
        {
            return PropertyDisplayNames.Min;
        }

        private static string GetOperatorCaption_ForAverageOverDimension(Operator op)
        {
            return PropertyDisplayNames.Average;
        }

        private static string GetOperatorCaption_ForSumOverDimension(Operator op)
        {
            return Titles.Sum;
        }

        private static string GetOperatorCaption_ForClosestOverDimension(Operator op)
        {
            return PropertyDisplayNames.Closest;
        }

        private static string GetOperatorCaption_ForClosestOverDimensionExp(Operator op)
        {
            return PropertyDisplayNames.ClosestExp;
        }

        private static string GetOperatorCaption_ForSortOverDimension(Operator op)
        {
            return PropertyDisplayNames.Sort;
        }

        private static string GetOperatorCaption_WithDimensionPlaceholder(Operator op, string operatorTypeDisplayNameWithPlaceholder)
        {
            DimensionEnum dimensionEnum = op.GetDimensionEnum();
            string formattedOperatorTypeDisplayName;
            if (dimensionEnum != DimensionEnum.Undefined)
            {
                string dimensionDisplayName = ResourceHelper.GetDisplayName(dimensionEnum);
                formattedOperatorTypeDisplayName = String.Format(operatorTypeDisplayNameWithPlaceholder, dimensionDisplayName);
            }
            else
            {
                formattedOperatorTypeDisplayName = ResourceHelper.GetOperatorTypeDisplayName(op);
            }

            // Use Operator.Name
            if (!String.IsNullOrWhiteSpace(op.Name))
            {
                return String.Format("{0}: {1}", formattedOperatorTypeDisplayName, op.Name);
            }
            // Use OperatorType DisplayName only.
            else
            {
                return formattedOperatorTypeDisplayName;
            }
        }

        private static string GetOperatorCaption_ForOtherOperators(Operator op)
        {
            string operatorTypeDisplayName = ResourceHelper.GetDisplayName(op.GetOperatorTypeEnum());

            // Use Operator.Name
            if (!String.IsNullOrWhiteSpace(op.Name))
            {
                return String.Format("{0}: {1}", operatorTypeDisplayName, op.Name);
            }

            // Use OperatorType DisplayName
            string caption = operatorTypeDisplayName;
            return caption;
        }

        public static string GetInletCaption(
            Inlet inlet,
            ISampleRepository sampleRepository,
            ICurveRepository curveRepository,
            IPatchRepository patchRepository)
        {
            var sb = new StringBuilder();

            OperatorTypeEnum operatorTypeEnum = inlet.Operator.GetOperatorTypeEnum();
            if (!OperatorTypeEnums_WithHiddenInletNames.Contains(operatorTypeEnum))
            {
                var wrapper = OperatorWrapperFactory.CreateOperatorWrapper(
                    inlet.Operator,
                    curveRepository,
                    sampleRepository,
                    patchRepository);
                string inletDisplayName = wrapper.GetInletDisplayName(inlet.ListIndex);
                sb.Append(inletDisplayName);
            }

            if (inlet.InputOutlet == null)
            {
                if (inlet.DefaultValue.HasValue)
                {
                    if (sb.Length != 0)
                    {
                        sb.Append(' ');
                    }
                    sb.AppendFormat("= {0:0.####}", inlet.DefaultValue.Value);
                }
            }

            if (inlet.IsObsolete)
            {
                if (sb.Length != 0)
                {
                    sb.Append(' ');
                }
                sb.AppendFormat("({0})", PropertyDisplayNames.IsObsolete);
            }

            return sb.ToString();
        }

        public static string GetOutletCaption(
            Outlet outlet,
            ISampleRepository sampleRepository,
            ICurveRepository curveRepository,
            IPatchRepository patchRepository)
        {
            if (outlet == null) throw new NullException(() => outlet);

            var sb = new StringBuilder();

            OperatorTypeEnum operatorTypeEnum = outlet.Operator.GetOperatorTypeEnum();
            if (OperatorTypeEnums_WithVisibleOutletNames.Contains(operatorTypeEnum))
            {
                bool isCustomOperatorWithSingleOutlet = operatorTypeEnum == OperatorTypeEnum.CustomOperator &&
                                                        outlet.Operator.Outlets.Count == 1;
                if (!isCustomOperatorWithSingleOutlet)
                {
                    var wrapper = OperatorWrapperFactory.CreateOperatorWrapper(
                        outlet.Operator,
                        curveRepository,
                        sampleRepository,
                        patchRepository);

                    string inletDisplayName = wrapper.GetOutletDisplayName(outlet.ListIndex);
                    sb.Append(inletDisplayName);
                }
            }

            if (outlet.IsObsolete)
            {
                if (sb.Length != 0)
                {
                    sb.Append(' ');
                }

                sb.AppendFormat("({0})", PropertyDisplayNames.IsObsolete);
            }

            return sb.ToString();
        }

        public static float? TryGetConnectionDistance(Inlet entity, EntityPositionManager entityPositionManager)
        {
            if (entity == null) throw new NullException(() => entity);

            if (entity.InputOutlet == null)
            {
                return null;
            }

            Operator operator1 = entity.Operator;
            Operator operator2 = entity.InputOutlet.Operator;

            EntityPosition entityPosition1 = entityPositionManager.GetOrCreateOperatorPosition(operator1.ID);
            EntityPosition entityPosition2 = entityPositionManager.GetOrCreateOperatorPosition(operator2.ID);

            float distance = Geometry.AbsoluteDistance(
                entityPosition1.X,
                entityPosition1.Y,
                entityPosition2.X,
                entityPosition2.Y);

            return distance;
        }

        internal static float? TryGetAverageConnectionDistance(Outlet entity, EntityPositionManager entityPositionManager)
        {
            if (entity == null) throw new NullException(() => entity);

            int connectedInletCount = entity.ConnectedInlets.Count;

            if (connectedInletCount == 0)
            {
                return null;
            }

            Operator operator1 = entity.Operator;

            float aggregate = 0f;

            foreach (Inlet connectedInlet in entity.ConnectedInlets)
            {
                Operator operator2 = connectedInlet.Operator;

                EntityPosition entityPosition1 = entityPositionManager.GetOrCreateOperatorPosition(operator1.ID);
                EntityPosition entityPosition2 = entityPositionManager.GetOrCreateOperatorPosition(operator2.ID);

                float distance = Geometry.AbsoluteDistance(
                    entityPosition1.X,
                    entityPosition1.Y,
                    entityPosition2.X,
                    entityPosition2.Y);

                aggregate += distance;
            }

            aggregate /= connectedInletCount;

            return aggregate;
        }

        // TreeLeaf

        public static TreeLeafViewModel CreateTreeLeafViewModel(string displayName)
        {
            var viewModel = new TreeLeafViewModel
            {
                Text = displayName
            };

            return viewModel;
        }

        public static TreeLeafViewModel CreateTreeLeafViewModel(string displayName, int count)
        {
            var viewModel = new TreeLeafViewModel
            {
                Text = GetTreeNodeText(displayName, count)
            };

            return viewModel;
        }

        public static string GetTreeNodeText(string displayName, int count)
        {
            string text = displayName;

            if (count != 0)
            {
                text += String.Format(" ({0})", count);
            }

            return text;
        }

        // Other

        public static string FormatUsedInDto(UsedInDto<Curve> dto)
        {
            if (dto == null) throw new NullException(() => dto);

            var sb = new StringBuilder();

            sb.Append(dto.Entity.Name);

            if (dto.UsedInIDAndNames.Count > 0)
            {
                string formattedUsedInList = FormatUsedInList(dto.UsedInIDAndNames);
                sb.AppendFormat(" ({0}: {1})", Titles.UsedIn, formattedUsedInList);
            }

            return sb.ToString();
        }

        public static string FormatUsedInDto(UsedInDto<Sample> dto)
        {
            if (dto == null) throw new NullException(() => dto);

            var sb = new StringBuilder();

            sb.Append(dto.Entity.Name);

            if (dto.UsedInIDAndNames.Count > 0)
            {
                string formattedUsedInList = FormatUsedInList(dto.UsedInIDAndNames);
                sb.AppendFormat(" ({0}: {1})", Titles.UsedIn, formattedUsedInList);
            }

            return sb.ToString();
        }

        public static string FormatUsedInList(IList<IDAndName> idAndNames)
        {
            if (idAndNames == null) throw new NullException(() => idAndNames);

            string concatinatedUsedIn = String.Join(", ", idAndNames.Select(x => x.Name).OrderBy(x => x));

            return concatinatedUsedIn;
        }
    }
}