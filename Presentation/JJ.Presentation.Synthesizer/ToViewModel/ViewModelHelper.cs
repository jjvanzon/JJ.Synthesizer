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

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    /// <summary> Empty view models start out with Visible = false. </summary>
    internal static partial class ViewModelHelper
    {
        private const int STRETCH_AND_SQUASH_ORIGIN_LIST_INDEX = 2;

        private static readonly bool _previewAutoPatchPolyphonicEnabled = GetPreviewAutoPatchPolyphonicEnabled();

        public static HashSet<OperatorTypeEnum> OperatorTypeEnums_WithDimensionAndCollectionRecalculationPropertyViews { get; } =
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

        public static HashSet<OperatorTypeEnum> OperatorTypeEnums_WithDimensionAndInterpolationPropertyViews { get; } =
          new HashSet<OperatorTypeEnum>
{
            OperatorTypeEnum.Random,
            OperatorTypeEnum.Resample,
};

        public static HashSet<OperatorTypeEnum> OperatorTypeEnums_WithDimensionAndOutletCountPropertyViews { get; } =
                  new HashSet<OperatorTypeEnum>
        {
            OperatorTypeEnum.MakeDiscrete,
            OperatorTypeEnum.Unbundle
        };

        public static HashSet<OperatorTypeEnum> OperatorTypeEnums_WithDimensionPropertyViews { get; } =
                  new HashSet<OperatorTypeEnum>
        {
            OperatorTypeEnum.AverageFollower,
            OperatorTypeEnum.GetDimension,
            OperatorTypeEnum.Loop,
            OperatorTypeEnum.MaxFollower,
            OperatorTypeEnum.MinFollower,
            OperatorTypeEnum.Squash,
            OperatorTypeEnum.Noise,
            OperatorTypeEnum.Pulse,
            OperatorTypeEnum.Range,
            OperatorTypeEnum.Reverse,
            OperatorTypeEnum.SawDown,
            OperatorTypeEnum.SawUp,
            OperatorTypeEnum.Select,
            OperatorTypeEnum.SetDimension,
            OperatorTypeEnum.Shift,
            OperatorTypeEnum.Sine,
            OperatorTypeEnum.Spectrum,
            OperatorTypeEnum.Square,
            OperatorTypeEnum.Stretch,
            OperatorTypeEnum.SumFollower,
            OperatorTypeEnum.TimePower,
            OperatorTypeEnum.Triangle
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
                                                    .Except(OperatorTypeEnums_WithDimensionPropertyViews)
                                                    .Except(OperatorTypeEnums_WithDimensionAndInterpolationPropertyViews)
                                                    .Except(OperatorTypeEnums_WithDimensionAndCollectionRecalculationPropertyViews)
                                                    .Except(OperatorTypeEnums_WithDimensionAndOutletCountPropertyViews)
                                                    .Except(OperatorTypeEnums_WithInletCountPropertyViews)
                                                    .ToHashSet();

        public static HashSet<OperatorTypeEnum> OperatorTypeEnums_WithStyledDimension { get; } = new HashSet<OperatorTypeEnum>
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

        public static CurrentPatchesViewModel CreateCurrentPatchesViewModel(IList<Document> childDocuments)
        {
            if (childDocuments == null) throw new NullException(() => childDocuments);

            var viewModel = new CurrentPatchesViewModel
            {
                List = childDocuments.Select(x => x.ToCurrentPatchViewModel()).ToList(),
                CanPreviewAutoPatchPolyphonic = _previewAutoPatchPolyphonicEnabled,
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static DocumentDeletedViewModel CreateDocumentDeletedViewModel()
        {
            var viewModel = new DocumentDeletedViewModel
            {
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

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

        public static string GetToneGridEditNumberTitle(Scale entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return ResourceHelper.GetScaleTypeDisplayNameSingular(entity);
        }

        public static bool GetInletVisible(Inlet inlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            if (inlet.InputOutlet != null)
            {
                return true;
            }

            OperatorTypeEnum operatorTypeEnum = inlet.Operator.GetOperatorTypeEnum();

            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.PatchInlet:
                    return false;

                case OperatorTypeEnum.Stretch:
                case OperatorTypeEnum.Squash:
                    if (inlet.ListIndex == STRETCH_AND_SQUASH_ORIGIN_LIST_INDEX)
                    {
                        var wrapper = new Stretch_OperatorWrapper(inlet.Operator);
                        if (wrapper.Dimension == DimensionEnum.Time)
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
        /// Is used to be able to update an existing operator view model in-place
        /// without having to re-establish the intricate relations with other operators.
        /// </summary>
        public static void RefreshInletViewModels(
            IList<Inlet> sourceInlets, 
            OperatorViewModel destOperatorViewModel,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            IPatchRepository patchRepository)
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

                inlet.ToViewModel(inletViewModel, curveRepository, sampleRepository, patchRepository);

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
            IPatchRepository patchRepository)
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

                outlet.ToViewModel(outletViewModel, curveRepository, sampleRepository, patchRepository);

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
        public static void RefreshViewModel_WithInletsAndOutlets_WithoutEntityPosition(
            Operator entity, OperatorViewModel operatorViewModel,
            ISampleRepository sampleRepository, ICurveRepository curveRepository, IPatchRepository patchRepository)
        {
            RefreshViewModel_WithoutEntityPosition(entity, operatorViewModel, sampleRepository, curveRepository, patchRepository);
            RefreshInletViewModels(entity.Inlets, operatorViewModel, curveRepository, sampleRepository, patchRepository);
            RefreshOutletViewModels(entity.Outlets, operatorViewModel, curveRepository, sampleRepository, patchRepository);
        }

        /// <summary>
        /// Is used to be able to update an existing operator view model in-place
        /// without having to re-establish the intricate relations with other operators.
        /// </summary>
        public static void RefreshViewModel_WithoutEntityPosition(
            Operator entity, 
            OperatorViewModel viewModel,
            ISampleRepository sampleRepository, 
            ICurveRepository curveRepository, 
            IPatchRepository patchRepository)
        {
            if (entity == null) throw new NullException(() => entity);
            if (viewModel == null) throw new NullException(() => viewModel);

            viewModel.ID = entity.ID;
            viewModel.Caption = GetOperatorCaption(entity, sampleRepository, curveRepository, patchRepository);
            viewModel.OperatorType = entity.OperatorType?.ToViewModel();
            viewModel.IsOwned = GetOperatorIsOwned(entity);
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
            return GetOperatorCaption_WithDimensionPlaceholder(op, Titles.MaxOverDimensionWithPlaceholder);
        }

        private static string GetOperatorCaption_ForMinOverDimension(Operator op)
        {
            return GetOperatorCaption_WithDimensionPlaceholder(op, Titles.MinOverDimensionWithPlaceholder);
        }

        private static string GetOperatorCaption_ForAverageOverDimension(Operator op)
        {
            return GetOperatorCaption_WithDimensionPlaceholder(op, Titles.AverageOverDimensionWithPlaceholder);
        }

        private static string GetOperatorCaption_ForSumOverDimension(Operator op)
        {
            return GetOperatorCaption_WithDimensionPlaceholder(op, Titles.SumOverDimensionWithPlaceholder);
        }

        private static string GetOperatorCaption_ForClosestOverDimension(Operator op)
        {
            return GetOperatorCaption_WithDimensionPlaceholder(op, Titles.ClosestOverDimensionWithPlaceholder);
        }

        private static string GetOperatorCaption_ForClosestOverDimensionExp(Operator op)
        {
            return GetOperatorCaption_WithDimensionPlaceholder(op, Titles.ClosestOverDimensionExpWithPlaceholder);
        }

        private static string GetOperatorCaption_ForSortOverDimension(Operator op)
        {
            return GetOperatorCaption_WithDimensionPlaceholder(op, Titles.SortOverDimensionWithPlaceholder);
        }

        private static string GetOperatorCaption_WithDimensionPlaceholder(Operator op, string operatorTypeDisplayNameWithPlaceholder)
        {
            DimensionEnum dimensionEnum = GetDimensionEnum(op);
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

            var wrapper = OperatorWrapperFactory.CreateOperatorWrapper(
                inlet.Operator,
                curveRepository,
                sampleRepository,
                patchRepository);

            string inletDisplayName = wrapper.GetInletDisplayName(inlet.ListIndex);

            sb.Append(inletDisplayName);

            if (inlet.InputOutlet == null)
            {
                if (inlet.DefaultValue.HasValue)
                {
                    sb.AppendFormat(" = {0:0.####}", inlet.DefaultValue.Value);
                }
            }

            return sb.ToString();
        }

        public static DimensionEnum GetDimensionEnum(Operator entity)
        {
            DimensionEnum dimensionEnum = DimensionEnum.Undefined;

            if (DataPropertyParser.DataIsWellFormed(entity))
            {
                dimensionEnum = DataPropertyParser.GetEnum<DimensionEnum>(entity, PropertyNames.Dimension);
            }

            return dimensionEnum;
        }

        private static bool GetPreviewAutoPatchPolyphonicEnabled()
        {
            return CustomConfigurationManager.GetSection<ConfigurationSection>().PreviewAutoPatchPolyphonicEnabled;
        }
    }
}