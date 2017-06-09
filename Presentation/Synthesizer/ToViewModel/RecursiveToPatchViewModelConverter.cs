using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Collections;
using JJ.Framework.Common;
using JJ.Framework.Exceptions;
using JJ.Framework.Mathematics;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    /// <summary>
    /// Handles the recursive conversion of viewmodels of operators and their inlets and outlets
    /// to entities. It delegates to the 'singular' forms of those conversions: the extension methods
    /// that do not convert anything other than the entity itself without any related entities.
    /// </summary>
    internal class RecursiveToPatchViewModelConverter
    {
        private static readonly string _timeDimensionKey = ViewModelHelper.GetDimensionKey(DimensionEnum.Time);
        private static readonly IList<StyleGradeEnum> _styleGradesNonNeutral = GetStyleGradesNonNeutral();

        private readonly ISampleRepository _sampleRepository;
        private readonly ICurveRepository _curveRepository;
        private readonly IPatchRepository _patchRepository;
        private readonly EntityPositionManager _entityPositionManager;

        private Dictionary<Operator, OperatorViewModel> _dictionary;

        public RecursiveToPatchViewModelConverter(
            ISampleRepository sampleRepository, 
            ICurveRepository curveRepository,
            IPatchRepository patchRepository,
            EntityPositionManager entityPositionManager)
        {
            _sampleRepository = sampleRepository ?? throw new NullException(() => sampleRepository);
            _curveRepository = curveRepository ?? throw new NullException(() => curveRepository);
            _patchRepository = patchRepository ?? throw new NullException(() => patchRepository);
            _entityPositionManager = entityPositionManager ?? throw new NullException(() => entityPositionManager);
        }

        public PatchDetailsViewModel ConvertToDetailsViewModel(Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            _dictionary = new Dictionary<Operator, OperatorViewModel>();

            var viewModel = new PatchDetailsViewModel
            {
                Entity = ConvertToViewModelRecursive(patch),
                ValidationMessages = new List<MessageDto>(),
                OperatorToolboxItems = ViewModelHelper.GetOperatorTypesViewModel()
            };

            foreach (OperatorViewModel operatorViewModel in viewModel.Entity.OperatorDictionary.Values)
            {
                SetViewModelPosition(operatorViewModel);
            }

            return viewModel;
        }

        private PatchViewModel ConvertToViewModelRecursive(Patch patch)
        {
            PatchViewModel viewModel = patch.ToViewModel();

            viewModel.OperatorDictionary = ConvertToViewModelDictionaryRecursive(patch.Operators);

            return viewModel;
        }

        private Dictionary<int, OperatorViewModel> ConvertToViewModelDictionaryRecursive(IList<Operator> operators)
        {
            IList<OperatorViewModel> operatorViewModels = operators.Select(x => ConvertToViewModelRecursive(x)).ToArray();

            // Style the different dimensions.
            IList<string> dimensionKeysToStyle = operatorViewModels.Select(x => x.Dimension)
                                                                   .Where(x => x.Visible)
                                                                   .Select(x => x.Key)
                                                                   .Except(_timeDimensionKey)
                                                                   .OrderBy(x => x) // Sort arbitrary, but consistently.
                                                                   .ToArray();
            if (dimensionKeysToStyle.Count > 0)
            {
                Dictionary<string, StyleGradeEnum> dimensionKey_To_StyleGrade_Dictionary =
                    MathHelper.Spread(dimensionKeysToStyle, _styleGradesNonNeutral);

                foreach (OperatorViewModel operatorViewModel in operatorViewModels)
                {
                    if (!operatorViewModel.Dimension.Visible)
                    {
                        continue;
                    }

                    if (dimensionKey_To_StyleGrade_Dictionary.TryGetValue(operatorViewModel.Dimension.Key, out StyleGradeEnum styleGradeEnum))
                    {
                        operatorViewModel.StyleGrade = styleGradeEnum;
                    }
                }
            }

            // A custom dimension name could clash with a standard dimension name.
            // Disambiguate by putting (Custom) or (Standard) behind the dimension names.
            
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dimensionNameGroups = operatorViewModels.GroupBy(x => NameHelper.ToCanonical(x.Dimension.Name));

            // ReSharper disable once SuggestVarOrType_Elsewhere
            foreach (var dimensionNameGroup in dimensionNameGroups)
            {
                // ReSharper disable once SuggestVarOrType_Elsewhere
                var dimensionKeyGroups = dimensionNameGroup.GroupBy(x => x.Dimension.Key);

                // ReSharper disable once PossibleMultipleEnumeration
                bool customDimensionNameClashesWithStandardDimensionName = dimensionKeyGroups.Take(2).Count() >= 2;
                if (!customDimensionNameClashesWithStandardDimensionName)
                {
                    continue;
                }

                // ReSharper disable once PossibleMultipleEnumeration
                // ReSharper disable once SuggestVarOrType_Elsewhere
                foreach (var dimensionKeyGroup in dimensionKeyGroups)
                {
                    foreach (OperatorViewModel operatorViewModel in dimensionKeyGroup)
                    {
                        if (operatorViewModel.Dimension.Key.StartsWith(ViewModelHelper.CUSTOM_DIMENSION_KEY_PREFIX))
                        {
                            operatorViewModel.Dimension.Name += $" ({ResourceFormatter.Custom})";
                        }
                        else
                        {
                            operatorViewModel.Dimension.Name += $" ({ResourceFormatter.Standard})";
                        }
                    }
                }
            }

            return operatorViewModels.ToDictionary(x => x.ID);
        }

        private OperatorViewModel ConvertToViewModelRecursive(Operator op)
        {
            if (_dictionary.TryGetValue(op, out OperatorViewModel viewModel))
            {
                return viewModel;
            }

            viewModel = op.ToViewModel(_sampleRepository, _curveRepository, _patchRepository, _entityPositionManager);

            _dictionary.Add(op, viewModel);

            viewModel.Inlets = ConvertToViewModelsRecursive(op.Inlets);
            viewModel.Outlets = ConvertToViewModelsRecursive(op.Outlets);

            return viewModel;
        }

        private IList<InletViewModel> ConvertToViewModelsRecursive(IList<Inlet> entities)
        {
            IList<InletViewModel> viewModels = entities.Select(x => ConvertToViewModelRecursive(x))
                                                       .OrderBy(x => x.IsObsolete)
                                                       .ThenBy(x => x.ListIndex)
                                                       .ThenBy(x => !string.IsNullOrWhiteSpace(x.Name))
                                                       .ThenBy(x => x.Name)
                                                       .ThenBy(x => !string.IsNullOrWhiteSpace(x.Dimension.Name))
                                                       .ThenBy(x => x.Dimension.Name)
                                                       .ToList();
            return viewModels;
        }

        private InletViewModel ConvertToViewModelRecursive(Inlet inlet)
        {
            InletViewModel viewModel = inlet.ToViewModel(
                _curveRepository,
                _sampleRepository,
                _patchRepository,
                _entityPositionManager);

            if (inlet.InputOutlet != null)
            {
                viewModel.InputOutlet = ConvertToViewModelRecursive(inlet.InputOutlet);
            }

            return viewModel;
        }

        private IList<OutletViewModel> ConvertToViewModelsRecursive(IList<Outlet> entities)
        {
            IList<OutletViewModel> viewModels = entities.Select(x => ConvertToViewModelRecursive(x))
                                                        .OrderBy(x => x.IsObsolete)
                                                        .ThenBy(x => x.ListIndex)
                                                        .ThenBy(x => !string.IsNullOrWhiteSpace(x.Name))
                                                        .ThenBy(x => x.Name)
                                                        .ThenBy(x => !string.IsNullOrWhiteSpace(x.Dimension.Name))
                                                        .ThenBy(x => x.Dimension.Name)
                                                        .ToList();
            return viewModels;
        }

        private OutletViewModel ConvertToViewModelRecursive(Outlet outlet)
        {
            OutletViewModel viewModel = outlet.ToViewModel(_curveRepository, _sampleRepository, _patchRepository, _entityPositionManager);

            // Recursive call
            viewModel.Operator = ConvertToViewModelRecursive(outlet.Operator);

            return viewModel;
        }

        private void SetViewModelPosition(OperatorViewModel operatorViewModel)
        {
            EntityPosition entityPosition = _entityPositionManager.GetOrCreateOperatorPosition(operatorViewModel.ID);
            operatorViewModel.EntityPositionID = entityPosition.ID;
            operatorViewModel.CenterX = entityPosition.X;
            operatorViewModel.CenterY = entityPosition.Y;
        }

        // Helpers

        private static IList<StyleGradeEnum> GetStyleGradesNonNeutral()
        {
            IList<StyleGradeEnum> list = EnumHelper.GetValues<StyleGradeEnum>()
                                                   .Except(StyleGradeEnum.Undefined)
                                                   .Except(StyleGradeEnum.StyleGradeNeutral)
                                                   .ToArray();
            return list;
        }
   }
}