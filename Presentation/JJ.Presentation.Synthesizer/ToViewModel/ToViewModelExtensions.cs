using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Names;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using JJ.Presentation.Synthesizer.ViewModels.Keys;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static class ToViewModelExtensions
    {
        // AudioFileOutput

        public static AudioFileOutputViewModel ToViewModelWithRelatedEntities(this AudioFileOutput entity)
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
                viewModel.AudioFileFormat = entity.AudioFileFormat.ToIDAndName();
            }

            if (entity.SampleDataType != null)
            {
                viewModel.SampleDataType = entity.SampleDataType.ToIDAndName();
            }

            if (entity.SpeakerSetup != null)
            {
                viewModel.SpeakerSetup = entity.SpeakerSetup.ToIDAndName();
            }

            // TODO: OrderBy something.
            viewModel.Channels = entity.AudioFileOutputChannels.Select(x => x.ToViewModelWithRelatedEntities()).ToList();

            return viewModel;
        }

        public static AudioFileOutputChannelViewModel ToViewModelWithRelatedEntities(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new AudioFileOutputChannelViewModel
            {
                ID = entity.ID,
                IndexNumber = entity.IndexNumber
            };

            if (entity.Outlet != null)
            {
                viewModel.Outlet = entity.Outlet.ToIDAndName();
            }

            return viewModel;
        }

        // Curve

        public static CurveViewModel ToViewModelWithRelatedEntities(this Curve entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new CurveViewModel
            {
                Name = entity.Name,
                Nodes = entity.Nodes.ToViewModels(),
                ID = entity.ID
            };

            return viewModel;
        }

        public static IList<NodeViewModel> ToViewModels(this IList<Node> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            IList<NodeViewModel> viewModels = entities.OrderBy(x => x.Time)
                                                      .Select(x => x.ToViewModel())
                                                      .ToList();
            return viewModels;
        }

        public static NodeViewModel ToViewModel(this Node entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new NodeViewModel
            {
                Time = entity.Time,
                Value = entity.Value,
                NodeType = entity.NodeType.ToIDAndName(),
                Direction = entity.Direction,
                ID = entity.ID
            };

            return viewModel;
        }

        // Document

        public static ReferencedDocumentViewModel ToReferencedDocumentViewModelWithRelatedEntities(this Document entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new ReferencedDocumentViewModel
            {
                Name = entity.Name,
                Instruments = entity.ChildDocuments.OrderBy(x => x.Name).Select(x => x.ToIDAndName()).ToList(),
                Effects = entity.ChildDocuments.OrderBy(x => x.Name).Select(x => x.ToIDAndName()).ToList(),
                ID = entity.ID
            };

            return viewModel;
        }

        // Patch

        public static PatchViewModel ToViewModel(this Patch entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new PatchViewModel
            {
                Name = entity.Name,
                ID = entity.ID
            };

            return viewModel;
        }

        public static OperatorViewModel ToViewModel(this Operator entity, EntityPositionManager entityPositionManager)
        {
            if (entity == null) throw new NullException(() => entity);

            EntityPosition entityPosition = entityPositionManager.GetOrCreateOperatorPosition(entity.ID);

            var viewModel = new OperatorViewModel
            {
                Name = entity.Name,
                ID = entity.ID,
                EntityPositionID = entityPosition.ID,
                CenterX = entityPosition.X,
                CenterY = entityPosition.Y
            };

            if (entity.GetOperatorTypeEnum() == OperatorTypeEnum.Value)
            {
                var wrapper = new Value_OperatorWrapper(entity);
                viewModel.Caption = wrapper.Value.ToString("0.####");
                viewModel.Value = wrapper.Value.ToString();
            }
            else
            {
                viewModel.Caption = entity.Name;
            }

            if (entity.OperatorType != null)
            {
                viewModel.OperatorTypeID = entity.OperatorType.ID;
            }

            return viewModel;
        }

        public static IList<InletViewModel> ToViewModels(this IList<Inlet> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            IList<InletViewModel> viewModels = entities/*.OrderBy(x => x.SortOrder)*/ // TODO: Introduce SortOrder property and then sort.
                                                       .Select(x => x.ToViewModel())
                                                       .ToList();
            return viewModels;
        }

        public static InletViewModel ToViewModel(this Inlet entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new InletViewModel
            {
                Name = entity.Name,
                ID = entity.ID
            };

            return viewModel;
        }

        public static IList<OutletViewModel> ToViewModels(this IList<Outlet> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            IList<OutletViewModel> viewModels = entities/*.OrderBy(x => x.SortOrder)*/ // TODO: Introduce SortOrder property and then sort.
                                                        .Select(x => x.ToViewModel())
                                                        .ToList();
            return viewModels;
        }

        public static OutletViewModel ToViewModel(this Outlet entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new OutletViewModel
            {
                Name = entity.Name,
                ID = entity.ID
            };

            return viewModel;
        }

        public static OperatorTypeViewModel ToViewModel(this OperatorType operatorType)
        {
            if (operatorType == null) throw new NullException(() => operatorType);

            var viewModel = new OperatorTypeViewModel
            {
                ID = operatorType.ID,
                DisplayText = ResourceHelper.GetPropertyDisplayName(operatorType.Name)
            };

            return viewModel;
        }

        // Sample

        public static SampleViewModel ToViewModel(this Sample entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new SampleViewModel
            {
                Name = entity.Name,
                Amplifier = entity.Amplifier,
                TimeMultiplier = entity.TimeMultiplier,
                IsActive = entity.IsActive,
                SamplingRate = entity.SamplingRate,
                BytesToSkip = entity.BytesToSkip,
                Location = entity.Location,
                ID = entity.ID
            };

            if (entity.AudioFileFormat != null)
            {
                viewModel.AudioFileFormat = entity.AudioFileFormat.ToIDAndName();
            }

            if (entity.SampleDataType != null)
            {
                viewModel.SampleDataType = entity.SampleDataType.ToIDAndName();
            }

            if (entity.SpeakerSetup != null)
            {
                viewModel.SpeakerSetup = entity.SpeakerSetup.ToIDAndName();
            }

            if (entity.InterpolationType != null)
            {
                viewModel.InterpolationType = entity.InterpolationType.ToIDAndName();
            }

            return viewModel;
        }
    }
}