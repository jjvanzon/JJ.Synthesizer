using JJ.Data.Canonical;
using JJ.Business.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using System.Collections.Generic;
using System.Linq;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Common;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Extensions;
using System;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Mathematics;

namespace JJ.Presentation.Synthesizer.Converters
{
    internal class RecursiveToViewModelConverter
    {
        private const string STANDARD_DIMENSION_GUID = "0C26ADA8-0BFC-484C-BF80-774D055DAA3F";
        private const string CUSTOM_DIMENSION_GUID = "5133584A-BA76-42DB-BD0E-42801FCB96DF";
        private const StyleGradeEnum NEUTRAL_STYLE_GRADE = StyleGradeEnum.StyleGrade16;

        private readonly ISampleRepository _sampleRepository;
        private readonly ICurveRepository _curveRepository;
        private readonly IPatchRepository _patchRepository;
        private readonly EntityPositionManager _entityPositionManager;

        private readonly string _timeDimensionKey = TryGetDimensionKey(DimensionEnum.Time);

        private Dictionary<Operator, OperatorViewModel> _dictionary;


        public RecursiveToViewModelConverter(
            ISampleRepository sampleRepository, 
            ICurveRepository curveRepository,
            IPatchRepository patchRepository,
            EntityPositionManager entityPositionManager)
        {
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (patchRepository == null) throw new NullException(() => patchRepository);
            if (entityPositionManager == null) throw new NullException(() => entityPositionManager);

            _sampleRepository = sampleRepository;
            _curveRepository = curveRepository;
            _patchRepository = patchRepository;
            _entityPositionManager = entityPositionManager;
        }

        public PatchDetailsViewModel ConvertToDetailsViewModel(Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            _dictionary = new Dictionary<Operator, OperatorViewModel>();

            var viewModel = new PatchDetailsViewModel
            {
                Entity = ConvertToViewModelRecursive(patch),
                ValidationMessages = new List<Message>()
            };

            viewModel.OperatorToolboxItems = ViewModelHelper.GetOperatorTypesViewModel();

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


        /// <summary> Does a ton of specific things regarding the display of dimensions. </summary>
        private Dictionary<int, OperatorViewModel> ConvertToViewModelDictionaryRecursive(IList<Operator> operators)
        {
            var operatorViewModels = new List<OperatorViewModel>(operators.Count);
            var dimensionKeyHashSet = new HashSet<string>();

            foreach (Operator op in operators)
            {
                OperatorViewModel operatorViewModel = ConvertToViewModelRecursive(op);

                operatorViewModel.Dimension = new DimensionViewModel
                {
                    Key = TryGetDimensionKey(op),
                    Name = TryGetDimensionName(op),
                    Visible = ViewModelHelper.OperatorTypeEnums_WithStyledDimension.Contains(op.GetOperatorTypeEnum())
                };

                operatorViewModels.Add(operatorViewModel);
                dimensionKeyHashSet.Add(operatorViewModel.Dimension.Key);
            }

            // Assign style depending on Dimension.
            if (dimensionKeyHashSet.Count <= 1)
            {
                // Only 1 dimensions: display neutrally.
                // TODO: I might disagree with this.
                operatorViewModels.ForEach(x => x.StyleGrade = NEUTRAL_STYLE_GRADE);
            }
            else
            {
                IList<OperatorViewModel> operatorViewModelsWithStyledDimensions =
                    operatorViewModels.Where(x => x.Dimension.Visible &&
                                                  // Time should be displayed neutrally.
                                                  !String.Equals(x.Dimension.Key, _timeDimensionKey)) 
                                      .ToArray();

                IList<OperatorViewModel> operatorViewModelsWithNeutralDimensionStyle = operatorViewModels.Except(operatorViewModelsWithStyledDimensions).ToArray();
                operatorViewModelsWithNeutralDimensionStyle.ForEach(x => x.StyleGrade = NEUTRAL_STYLE_GRADE);

                // Rest should be displayed in equally spread grades,
                // sorted by dimension ID (arbitrary, but at least consistent).
                IList<string> remainingDimensionKeys = dimensionKeyHashSet.Except((string)null)
                                                                          .Except(_timeDimensionKey)
                                                                          .OrderBy(x => x)
                                                                          .ToArray();

                // TODO: Cache this statically?
                IList<StyleGradeEnum> remainingStyleGrades = EnumHelper.GetValues<StyleGradeEnum>()
                                                                       .Except(StyleGradeEnum.Undefined)
                                                                       .Except(NEUTRAL_STYLE_GRADE)
                                                                       .ToArray();

                Dictionary<string, StyleGradeEnum> dimensionKey_To_StyleGrade_Dictionary = MathHelper.Spread(remainingDimensionKeys, remainingStyleGrades);

                foreach (OperatorViewModel operatorViewModel in operatorViewModelsWithStyledDimensions)
                {
                    string dimensionKey = operatorViewModel.Dimension.Key;
                    StyleGradeEnum styleGradeEnum = dimensionKey_To_StyleGrade_Dictionary[dimensionKey];
                    operatorViewModel.StyleGrade = styleGradeEnum;
                }
            }

            Dictionary<int, OperatorViewModel> dictionary = operatorViewModels.ToDictionary(x => x.ID);

            return dictionary;
        }

        private string TryGetDimensionKey(Operator op)
        {
            if (!String.IsNullOrEmpty(op.CustomDimensionName))
            {
                return String.Format("{0}{1}", CUSTOM_DIMENSION_GUID, op.CustomDimensionName);
            }

            return TryGetDimensionKey(op.GetStandardDimensionEnum());
        }

        private static string TryGetDimensionKey(DimensionEnum standardDimensionEnum)
        {
            if (standardDimensionEnum != DimensionEnum.Undefined)
            {
                return String.Format("{0}{1}", STANDARD_DIMENSION_GUID, standardDimensionEnum);
            }

            return null;
        }

        private string TryGetDimensionName(Operator op)
        {
            if (!String.IsNullOrEmpty(op.CustomDimensionName))
            {
                return op.CustomDimensionName;
            }

            DimensionEnum standardDimensionEnum = op.GetStandardDimensionEnum();
            if (standardDimensionEnum != DimensionEnum.Undefined)
            {
                return ResourceHelper.GetDisplayName(standardDimensionEnum);
            }

            return null;
        }

        private OperatorViewModel ConvertToViewModelRecursive(Operator op)
        {
            OperatorViewModel viewModel;
            if (_dictionary.TryGetValue(op, out viewModel))
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
                                                       .OrderBy(x => x.ListIndex)
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
                                                        .OrderBy(x => x.ListIndex)
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
    }
}