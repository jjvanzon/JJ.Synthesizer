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

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static class ToEntityViewModelExtensions
    {
        public static PatchListItemViewModel ToListItemViewModel(this Patch entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new PatchListItemViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
            };

            if (entity.Document != null)
            {
                viewModel.DocumentName = entity.Document.Name;
            }

            return viewModel;
        }

        public static PatchViewModel ToViewModel(this Patch entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new PatchViewModel
            {
                ID = entity.ID,
                PatchName = entity.Name
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

        public static CurveViewModel ToViewModelWithRelatedEntities(this Curve entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new CurveViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
                Nodes = entity.Nodes.Select(x => x.ToViewModel()).ToArray()
            };

            return viewModel;
        }

        public static NodeViewModel ToViewModel(this Node entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new NodeViewModel
            {
                ID = entity.ID,
                Time = entity.Time,
                Value = entity.Value,
                NodeType = entity.NodeType.ToIDName(),
                Direction = entity.Direction
            };

            return viewModel;
        }

        public static AudioFileOutputViewModel ToViewModelWithRelatedEntities(this AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new AudioFileOutputViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
                SamplingRate = entity.SamplingRate
            };

            if (entity.AudioFileFormat != null)
            {
                viewModel.AudioFileFormat = entity.AudioFileFormat.ToIDName();
            }

            if (entity.SampleDataType != null)
            {
                viewModel.SampleDataType = entity.SampleDataType.ToIDName();
            }

            if (entity.SpeakerSetup != null)
            {
                viewModel.SpeakerSetup = entity.SpeakerSetup.ToIDName();
            }

            viewModel.Channels = entity.AudioFileOutputChannels.Select(x => x.ToViewModelWithRelatedEntities()).ToArray();

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
                viewModel.Outlet = entity.Outlet.ToIDName();
            }

            return viewModel;
        }

        public static AudioFileOutputListItemViewModel ToListItemViewModel(this AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new AudioFileOutputListItemViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
                SamplingRate = entity.SamplingRate
            };

            if (entity.AudioFileFormat != null)
            {
                viewModel.AudioFileFormat = entity.AudioFileFormat.Name;
            }

            if (entity.SampleDataType != null)
            {
                viewModel.SampleDataType = entity.SampleDataType.Name;
            }

            if (viewModel.SpeakerSetup != null)
            {
                viewModel.SpeakerSetup = entity.SpeakerSetup.Name;
            }

            return viewModel;
        }

        public static SampleListItemViewModel ToListItemViewModel(this Sample entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new SampleListItemViewModel
            {
                ID = entity.ID,
                IsActive = entity.IsActive,
                Name = entity.Name,
                SamplingRate = entity.SamplingRate,
            };

            if (entity.AudioFileFormat != null)
            {
                viewModel.AudioFileFormat = entity.AudioFileFormat.Name;
            }

            if (entity.SampleDataType != null)
            {
                viewModel.SampleDataType = entity.SampleDataType.Name;
            }

            if (viewModel.SpeakerSetup != null)
            {
                viewModel.SpeakerSetup = entity.SpeakerSetup.Name;
            }

            return viewModel;
        }

        public static SampleViewModel ToViewModel(this Sample entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new SampleViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
                SamplingRate = entity.SamplingRate
            };

            if (entity.AudioFileFormat != null)
            {
                viewModel.AudioFileFormat = entity.AudioFileFormat.ToIDName();
            }

            if (entity.SampleDataType != null)
            {
                viewModel.SampleDataType = entity.SampleDataType.ToIDName();
            }

            if (entity.SpeakerSetup != null)
            {
                viewModel.SpeakerSetup = entity.SpeakerSetup.ToIDName();
            }

            if (entity.InterpolationType != null)
            {
                viewModel.InterpolationType = entity.InterpolationType.ToIDName();
            }

            return viewModel;
        }

        public static ReferencedDocumentViewModel ToReferencedDocumentViewModelWithRelatedEntities(this Document entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new ReferencedDocumentViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
                Instruments = entity.Instruments.Select(x => x.ToIDName()).ToList(),
                Effects = entity.Effects.Select(x => x.ToIDName()).ToList()
            };

            return viewModel;
        }
    }
}
