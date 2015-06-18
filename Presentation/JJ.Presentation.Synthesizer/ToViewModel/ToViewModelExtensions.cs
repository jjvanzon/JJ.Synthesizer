using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.EntityWrappers;
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

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static class ToViewModelExtensions
    {
        // AudioFileOutput

        public static AudioFileOutputViewModel ToViewModelWithRelatedEntities(this AudioFileOutput entity, int audioFileOutputListIndex)
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
                Keys = new AudioFileOutputKeysViewModel
                {
                    ID = entity.ID,
                    DocumentID = entity.Document.ID,
                    ListIndex = audioFileOutputListIndex
                }
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

            viewModel.Channels = new List<AudioFileOutputChannelViewModel>(entity.AudioFileOutputChannels.Count);
            for (int i = 0; i < entity.AudioFileOutputChannels.Count; i++)
            {
                AudioFileOutputChannel audioFileOutputChannel = entity.AudioFileOutputChannels[i];
                AudioFileOutputChannelViewModel audioFileOutputChannelViewModel = 
                    audioFileOutputChannel.ToViewModelWithRelatedEntities(audioFileOutputListIndex, i);

                viewModel.Channels.Add(audioFileOutputChannelViewModel);
            }

            return viewModel;
        }

        public static AudioFileOutputChannelViewModel ToViewModelWithRelatedEntities(
            this AudioFileOutputChannel entity, 
            int audioFileOutputListIndex, 
            int listIndex)
        {
            if (entity == null) throw new NullException(() => entity);
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            if (entity.AudioFileOutput.Document == null) throw new NullException(() => entity.AudioFileOutput.Document);

            var viewModel = new AudioFileOutputChannelViewModel
            {
                Keys = new AudioFileOutputChannelKeysViewModel
                {
                    ID = entity.ID,
                    DocumentID = entity.AudioFileOutput.Document.ID,
                    AudioFileOutputListIndex = audioFileOutputListIndex,
                    IndexNumber = entity.IndexNumber
                }
            };

            if (entity.Outlet != null)
            {
                viewModel.Outlet = entity.Outlet.ToIDAndName();
            }

            return viewModel;
        }

        // Curve

        public static CurveViewModel ToViewModelWithRelatedEntities(
            this Curve entity,
            int rootDocumentID,
            ChildDocumentTypeEnum? childDocumentTypeEnum,
            int? childDocumentListIndex,
            int listIndex)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new CurveViewModel
            {
                Name = entity.Name,
                Nodes = entity.Nodes.ToViewModels(rootDocumentID, childDocumentTypeEnum, childDocumentListIndex, listIndex),
                Keys = new CurveKeysViewModel
                {
                    ID = entity.ID,
                    RootDocumentID = rootDocumentID,
                    ChildDocumentTypeEnum = childDocumentTypeEnum,
                    ChildDocumentListIndex = childDocumentListIndex,
                    ListIndex = listIndex
                }
            };

            return viewModel;
        }

        public static IList<NodeViewModel> ToViewModels(
            this IList<Node> entities,
            int rootDocumentID,
            ChildDocumentTypeEnum? childDocumentTypeEnum,
            int? childDocumentListIndex,
            int curveListIndex)
        {
            if (entities == null) throw new NullException(() => entities);

            entities = entities.OrderBy(x => x.Time).ToArray();

            IList<NodeViewModel> viewModels = new List<NodeViewModel>(entities.Count);

            for (int i = 0; i < entities.Count; i++)
            {
                Node entity = entities[i];
                NodeViewModel viewModel = entity.ToViewModel(rootDocumentID, childDocumentTypeEnum, childDocumentListIndex, curveListIndex, i);
                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public static NodeViewModel ToViewModel(
            this Node entity,
            int rootDocumentID,
            ChildDocumentTypeEnum? childDocumentTypeEnum,
            int? childDocumentListIndex,
            int curveListIndex,
            int nodeListIndex)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new NodeViewModel
            {
                Time = entity.Time,
                Value = entity.Value,
                NodeType = entity.NodeType.ToIDAndName(),
                Direction = entity.Direction,
                Keys = entity.ToKeyViewModel(rootDocumentID, childDocumentTypeEnum, childDocumentListIndex, curveListIndex, nodeListIndex),
            };

            return viewModel;
        }

        private static NodeKeysViewModel ToKeyViewModel(
            this Node entity, 
            int rootDocumentID,
            ChildDocumentTypeEnum? childDocumentTypeEnum,
            int? childDocumentListIndex,
            int curveListIndex,
            int nodeListIndex)
        {
            if (entity.Curve == null) throw new NullException(() => entity.Curve);
            if (entity.Curve.Document == null) throw new NullException(() => entity.Curve.Document);

            var viewModel = new NodeKeysViewModel
            {
                ID = entity.ID,
                RootDocumentID = rootDocumentID,
                ChildDocumentTypeEnum = childDocumentTypeEnum,
                ChildDocumentListIndex = childDocumentListIndex,
                CurveListIndex = curveListIndex,
                ListIndex = nodeListIndex,
                TemporaryID = Guid.NewGuid()
            };

            return viewModel;
        }

        // Document

        public static ReferencedDocumentViewModel ToReferencedDocumentViewModelWithRelatedEntities(this Document entity, int listIndex)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new ReferencedDocumentViewModel
            {
                Name = entity.Name,
                Instruments = entity.Instruments.OrderBy(x => x.Name).Select(x => x.ToIDAndName()).ToList(),
                Effects = entity.Effects.OrderBy(x => x.Name).Select(x => x.ToIDAndName()).ToList(),
                Keys = new ReferencedDocumentKeysViewModel
                {
                    ID = entity.ID,
                    ListIndex = listIndex
                }
            };

            return viewModel;
        }

        // Patch

        public static PatchViewModel ToViewModel(
            this Patch entity,
            int rootDocumentID,
            ChildDocumentTypeEnum? childDocumentTypeEnum,
            int? childDocumentListIndex,
            int listIndex)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new PatchViewModel
            {
                Name = entity.Name,
                Keys = new PatchKeysViewModel
                {
                    ID = entity.ID,
                    RootDocumentID = rootDocumentID,
                    ListIndex = listIndex,
                    ChildDocumentTypeEnum = childDocumentTypeEnum,
                    ChildDocumentListIndex = childDocumentListIndex
                }
            };

            return viewModel;
        }

        public static OperatorViewModel ToViewModel(this Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            string name;
            if (String.Equals(entity.OperatorTypeName, PropertyNames.ValueOperator))
            {
                var wrapper = new ValueOperatorWrapper(entity);
                name = wrapper.Value.ToString("0.####");
            }
            else
            {
                name = entity.Name;
            }

            var viewModel = new OperatorViewModel
            {
                ID = entity.ID,
                TemporaryID = Guid.NewGuid(),
                Name = name,
                OperatorTypeName = entity.OperatorTypeName
            };

            return viewModel;
        }

        public static IList<InletViewModel> ToViewModels(this IList<Inlet> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            // TODO: Introduce SortOrder property and then sort.
            //entities = entities.OrderBy(x => x.SortOrder).ToArray();

            IList<InletViewModel> viewModels = new List<InletViewModel>(entities.Count);

            for (int i = 0; i < entities.Count; i++)
            {
                Inlet entity = entities[i];
                InletViewModel viewModel = entity.ToViewModel();
                viewModel.ListIndex = i;
                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public static InletViewModel ToViewModel(this Inlet entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new InletViewModel
            {
                ID = entity.ID,
                Name = entity.Name
            };

            return viewModel;
        }

        public static IList<OutletViewModel> ToViewModels(this IList<Outlet> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            // TODO: Introduce SortOrder property and then sort.
            //entities = entities.OrderBy(x => x.SortOrder).ToArray();

            IList<OutletViewModel> viewModels = new List<OutletViewModel>(entities.Count);

            for (int i = 0; i < entities.Count; i++)
            {
                Outlet entity = entities[i];
                OutletViewModel viewModel = entity.ToViewModel();
                viewModel.ListIndex = i;
                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public static OutletViewModel ToViewModel(this Outlet entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new OutletViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
                TemporaryID = Guid.NewGuid()
            };

            return viewModel;
        }

        // Sample

        public static SampleViewModel ToViewModel(
            this Sample entity, 
            int rootDocumentID,
            ChildDocumentTypeEnum? childDocumentTypeEnum,
            int? childDocumentListIndex,
            int listIndex)
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
                Keys = new SampleKeysViewModel
                {
                    ID = entity.ID,
                    RootDocumentID = rootDocumentID,
                    ListIndex = listIndex,
                    ChildDocumentTypeEnum = childDocumentTypeEnum,
                    ChildDocumentListIndex = childDocumentListIndex
                }
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
