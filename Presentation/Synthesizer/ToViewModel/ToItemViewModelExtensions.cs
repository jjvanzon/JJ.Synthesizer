using System.Linq;
using System.Collections.Generic;
using JJ.Business.Synthesizer;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Business.Synthesizer.Extensions;
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

        // Inlet

        public static InletViewModel ToViewModel(
            this Inlet entity,
            ICurveRepository curveRepository,
            EntityPositionManager entityPositionManager)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new InletViewModel();

            entity.ToViewModel(viewModel, curveRepository, entityPositionManager);

            return viewModel;
        }

        /// <summary> Overload for in-place refreshing of a view model </summary>
        public static void ToViewModel(
            this Inlet entity,
            InletViewModel viewModel,
            ICurveRepository curveRepository,
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
            viewModel.Visible = ToViewModelHelper.GetInletVisible(entity);
            viewModel.Caption = ToViewModelHelper.GetInletCaption(entity, curveRepository);
            viewModel.ConnectionDistance = ToViewModelHelper.TryGetConnectionDistance(entity, entityPositionManager);

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
                Caption = ToViewModelHelper.GetNodeCaption(entity)
            };

            return viewModel;
        }

        // Operator

        public static OperatorViewModel ToViewModel(
            this Operator entity,
            ICurveRepository curveRepository,
            EntityPositionManager entityPositionManager)
        {
            if (entity == null) throw new NullException(() => entity);
            if (entityPositionManager == null) throw new NullException(() => entityPositionManager);

            var viewModel = new OperatorViewModel();

            ToViewModelHelper.RefreshViewModel(
                entity,
                viewModel,
                curveRepository,
                entityPositionManager);

            return viewModel;
        }

        // Outlet

        public static OutletViewModel ToViewModel(
            this Outlet entity,
            ICurveRepository curveRepository,
            EntityPositionManager entityPositionManager)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new OutletViewModel();
            entity.ToViewModel(viewModel, curveRepository, entityPositionManager);
            return viewModel;
        }

        /// <summary> Overload for in-place refreshing of a view model. </summary>
        public static void ToViewModel(
            this Outlet entity,
            OutletViewModel viewModel,
            ICurveRepository curveRepository,
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
            viewModel.Visible = ToViewModelHelper.GetOutletVisible(entity);
            viewModel.Caption = ToViewModelHelper.GetOutletCaption(entity, curveRepository);
            viewModel.AverageConnectionDistance = ToViewModelHelper.TryGetAverageConnectionDistance(entity, entityPositionManager);

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
