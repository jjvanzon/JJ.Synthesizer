using System.Linq;
using System.Collections.Generic;
using JJ.Business.Synthesizer;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Dto;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static class ToItemViewModelExtensions
    {
        // AudioFileOutput

        public static AudioFileOutputViewModel ToViewModel(this AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);
            if (entity.Document == null) throw new NullException(() => entity.Document);

            var viewModel = new AudioFileOutputViewModel
            {
                Name = entity.Name,
                SamplingRate = entity.SamplingRate,
                StartTime = entity.StartTime,
                Duration = entity.Duration,
                Amplifier = entity.Amplifier,
                TimeMultiplier = entity.TimeMultiplier,
                FilePath = entity.FilePath,
                ID = entity.ID
            };

            if (entity.AudioFileFormat != null)
            {
                viewModel.AudioFileFormat = entity.AudioFileFormat.ToIDAndDisplayName();
            }
            else
            {
                viewModel.AudioFileFormat = ToViewModelHelper.CreateEmptyIDAndName();
            }

            if (entity.SampleDataType != null)
            {
                viewModel.SampleDataType = entity.SampleDataType.ToIDAndDisplayName();
            }
            else
            {
                viewModel.SampleDataType = ToViewModelHelper.CreateEmptyIDAndName();
            }

            if (entity.SpeakerSetup != null)
            {
                viewModel.SpeakerSetup = entity.SpeakerSetup.ToIDAndDisplayName();
            }
            else
            {
                viewModel.SpeakerSetup = ToViewModelHelper.CreateEmptyIDAndName();
            }

            if (entity.Outlet != null)
            {
                viewModel.Outlet = entity.Outlet.ToIDAndName();
            }
            else
            {
                viewModel.Outlet = ToViewModelHelper.CreateEmptyIDAndName();
            }

            return viewModel;
        }

        // AudioOutput

        public static AudioOutputViewModel ToViewModel(this AudioOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new AudioOutputViewModel
            {
                ID = entity.ID,
                SamplingRate = entity.SamplingRate,
                MaxConcurrentNotes = entity.MaxConcurrentNotes,
                DesiredBufferDuration = entity.DesiredBufferDuration
            };

            if (entity.SpeakerSetup != null)
            {
                viewModel.SpeakerSetup = entity.SpeakerSetup.ToIDAndDisplayName();
            }
            else
            {
                viewModel.SpeakerSetup = ToViewModelHelper.CreateEmptyIDAndName();
            }

            return viewModel;
        }

        // Curve

        public static IDAndName ToIDAndNameWithUsedIn(this UsedInDto<Curve> dto)
        {
            if (dto == null) throw new NullException(() => dto);

            var idAndName = new IDAndName
            {
                ID = dto.Entity.ID,
                Name = ToViewModelHelper.FormatUsedInDto(dto)
            };

            return idAndName;
        }

        // Inlet

        public static IList<InletViewModel> ToViewModels(
            this IList<Inlet> entities,
            IDimensionRepository dimensionRepository,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            EntityPositionManager entityPositionManager)
        {
            if (entities == null) throw new NullException(() => entities);

            IList<InletViewModel> viewModels = entities.Sort()
                                                       .Select(
                                                           x => x.ToViewModel(
                                                               dimensionRepository,
                                                               curveRepository,
                                                               sampleRepository,
                                                               entityPositionManager))
                                                       .ToList();
            return viewModels;
        }

        public static InletViewModel ToViewModel(
            this Inlet entity,
            IDimensionRepository dimensionRepository,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            EntityPositionManager entityPositionManager)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new InletViewModel();

            entity.ToViewModel(viewModel, curveRepository, sampleRepository, entityPositionManager);

            return viewModel;
        }

        /// <summary> Overload for in-place refreshing of a view model </summary>
        public static void ToViewModel(
            this Inlet entity,
            InletViewModel viewModel,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            EntityPositionManager entityPositionManager)
        {
            if (entity == null) throw new NullException(() => entity);
            if (viewModel == null) throw new NullException(() => viewModel);

            viewModel.ID = entity.ID;
            viewModel.Name = entity.Name;
            viewModel.Position = entity.Position;
            viewModel.DefaultValue = entity.DefaultValue;
            viewModel.IsObsolete = entity.IsObsolete;
            viewModel.HasWarningAppearance = entity.IsObsolete;
            viewModel.WarnIfEmpty = entity.WarnIfEmpty;
            viewModel.NameOrDimensionHidden = entity.NameOrDimensionHidden;
            viewModel.IsRepeating = entity.IsRepeating;
            viewModel.RepetitionPosition = entity.RepetitionPosition;
            viewModel.Visible = ToViewModelValueHelper.GetInletVisible(entity);
            viewModel.Caption = ToViewModelValueHelper.GetInletCaption(entity, sampleRepository, curveRepository);
            viewModel.ConnectionDistance = ToViewModelValueHelper.TryGetConnectionDistance(entity, entityPositionManager);

            if (entity.Dimension != null)
            {
                viewModel.Dimension = entity.Dimension.ToIDAndDisplayName();
            }
            else
            {
                viewModel.Dimension = ToViewModelHelper.CreateEmptyIDAndName();
            }
        }

        // Node

        public static Dictionary<int, NodeViewModel> ToViewModelDictionary(this IList<Node> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            Dictionary<int, NodeViewModel> viewModels = entities.Select(x => x.ToViewModel()).ToDictionary(x => x.ID);

            return viewModels;
        }

        public static NodeViewModel ToViewModel(this Node entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new NodeViewModel
            {
                X = entity.X,
                Y = entity.Y,
                NodeType = entity.NodeType.ToIDAndDisplayName(),
                ID = entity.ID,
                Caption = ToViewModelValueHelper.GetNodeCaption(entity)
            };

            return viewModel;
        }

        // Operator

        public static OperatorViewModel ToViewModel(
            this Operator entity,
            ISampleRepository sampleRepository,
            ICurveRepository curveRepository,
            EntityPositionManager entityPositionManager)
        {
            if (entity == null) throw new NullException(() => entity);
            if (entityPositionManager == null) throw new NullException(() => entityPositionManager);

            var viewModel = new OperatorViewModel();

            ToViewModelHelper.RefreshViewModel(
                entity,
                viewModel,
                sampleRepository,
                curveRepository,
                entityPositionManager);

            return viewModel;
        }

        public static OperatorViewModel ToViewModel_WithRelatedEntities_AndInverseProperties(
            this Operator op,
            IDimensionRepository dimensionRepository,
            ISampleRepository sampleRepository,
            ICurveRepository curveRepository,
            EntityPositionManager entityPositionManager)
        {
            if (op == null) throw new NullException(() => op);
            if (entityPositionManager == null) throw new NullException(() => entityPositionManager);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

            OperatorViewModel operatorViewModel = op.ToViewModel(sampleRepository, curveRepository, entityPositionManager);
            operatorViewModel.Inlets = op.Inlets.ToViewModels(dimensionRepository, curveRepository, sampleRepository, entityPositionManager);
            operatorViewModel.Outlets = op.Outlets.ToViewModels(dimensionRepository, curveRepository, sampleRepository, entityPositionManager);

            // This is the inverse property in the view model!
            foreach (OutletViewModel outletViewModel in operatorViewModel.Outlets)
            {
                outletViewModel.Operator = operatorViewModel;
            }

            return operatorViewModel;
        }

        // Outlet

        public static IList<OutletViewModel> ToViewModels(
            this IList<Outlet> entities,
            IDimensionRepository dimensionRepository,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            EntityPositionManager entityPositionManager)
        {
            if (entities == null) throw new NullException(() => entities);

            IList<OutletViewModel> viewModels = entities.Sort()
                                                        .Select(x => x.ToViewModel(curveRepository, sampleRepository, entityPositionManager))
                                                        .ToList();
            return viewModels;
        }

        public static OutletViewModel ToViewModel(
            this Outlet entity,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            EntityPositionManager entityPositionManager)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new OutletViewModel();
            entity.ToViewModel(viewModel, curveRepository, sampleRepository, entityPositionManager);
            return viewModel;
        }

        /// <summary> Overload for in-place refreshing of a view model. </summary>
        public static void ToViewModel(
            this Outlet entity,
            OutletViewModel viewModel,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            EntityPositionManager entityPositionManager)
        {
            if (entity == null) throw new NullException(() => entity);
            if (viewModel == null) throw new NullException(() => viewModel);

            viewModel.ID = entity.ID;
            viewModel.Name = entity.Name;
            viewModel.Position = entity.Position;
            viewModel.NameOrDimensionHidden = entity.NameOrDimensionHidden;
            viewModel.IsObsolete = entity.IsObsolete;
            viewModel.HasWarningAppearance = entity.IsObsolete;
            viewModel.IsRepeating = entity.IsRepeating;
            viewModel.RepetitionPosition = entity.RepetitionPosition;
            viewModel.Visible = ToViewModelValueHelper.GetOutletVisible(entity);
            viewModel.Caption = ToViewModelValueHelper.GetOutletCaption(entity, sampleRepository, curveRepository);
            viewModel.AverageConnectionDistance = ToViewModelValueHelper.TryGetAverageConnectionDistance(entity, entityPositionManager);

            if (entity.Dimension != null)
            {
                viewModel.Dimension = entity.Dimension.ToIDAndDisplayName();
            }
            else
            {
                viewModel.Dimension = ToViewModelHelper.CreateEmptyIDAndName();
            }
        }

        // Patch

        public static PatchViewModel ToViewModel(this Patch entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new PatchViewModel
            {
                ID = entity.ID
            };

            return viewModel;
        }

        // Sample

        public static SampleViewModel ToViewModel(this Sample entity, byte[] bytes)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new SampleViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
                Amplifier = entity.Amplifier,
                TimeMultiplier = entity.TimeMultiplier,
                IsActive = entity.IsActive,
                SamplingRate = entity.SamplingRate,
                BytesToSkip = entity.BytesToSkip,
                OriginalLocation = entity.OriginalLocation,
                Bytes = bytes,
                Duration = entity.GetDuration(bytes)
            };

            if (entity.AudioFileFormat != null)
            {
                viewModel.AudioFileFormat = entity.AudioFileFormat.ToIDAndDisplayName();
            }
            else
            {
                viewModel.AudioFileFormat = ToViewModelHelper.CreateEmptyIDAndName();
            }

            if (entity.SampleDataType != null)
            {
                viewModel.SampleDataType = entity.SampleDataType.ToIDAndDisplayName();
            }
            else
            {
                viewModel.SampleDataType = ToViewModelHelper.CreateEmptyIDAndName();
            }

            if (entity.SpeakerSetup != null)
            {
                viewModel.SpeakerSetup = entity.SpeakerSetup.ToIDAndDisplayName();
            }
            else
            {
                viewModel.SpeakerSetup = ToViewModelHelper.CreateEmptyIDAndName();
            }

            if (entity.InterpolationType != null)
            {
                viewModel.InterpolationType = entity.InterpolationType.ToIDAndDisplayName();
            }
            else
            {
                viewModel.InterpolationType = ToViewModelHelper.CreateEmptyIDAndName();
            }

            return viewModel;
        }

        // Scale

        public static ScaleViewModel ToViewModel(this Scale entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new ScaleViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
                BaseFrequency = entity.BaseFrequency,
            };

            if (entity.ScaleType != null)
            {
                viewModel.ScaleType = entity.ScaleType.ToIDAndDisplayNamePlural();
            }
            else
            {
                viewModel.ScaleType = ToViewModelHelper.CreateEmptyIDAndName();
            }

            return viewModel;
        }

        // Tone

        public static ToneViewModel ToViewModel(this Tone entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new ToneViewModel
            {
                ID = entity.ID,
                Number = entity.Number.ToString(),
                Octave = entity.Octave.ToString(),
                Frequency = entity.GetFrequency()
            };

            return viewModel;
        }
    }
}
