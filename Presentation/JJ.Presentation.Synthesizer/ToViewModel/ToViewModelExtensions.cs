using System;
using System.Linq;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels.Partials;

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
                viewModel.AudioFileFormat = entity.AudioFileFormat.ToIDAndDisplayName();
            }

            if (entity.SampleDataType != null)
            {
                viewModel.SampleDataType = entity.SampleDataType.ToIDAndDisplayName();
            }

            if (entity.SpeakerSetup != null)
            {
                viewModel.SpeakerSetup = entity.SpeakerSetup.ToIDAndDisplayName();
            }

            viewModel.Channels = entity.AudioFileOutputChannels.Select(x => x.ToViewModelWithRelatedEntities())
                                                               .OrderBy(x => x.IndexNumber)
                                                               .ToList();
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

            // TODO: Adding a (display) name to the view model requires cross referencing with SpeakerSetup, 
            // which you might want to do in extension methods.

            return viewModel;
        }

        // Curve

        public static CurveViewModel ToViewModelWithRelatedEntities(this Curve entity)
        {
            if (entity == null) throw new NullException(() => entity);

            CurveViewModel viewModel = entity.ToViewModel();

            viewModel.Nodes = entity.Nodes.ToViewModels();

            return viewModel;
        }

        public static CurveViewModel ToViewModel(this Curve entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new CurveViewModel
            {
                Name = entity.Name,
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
                NodeType = entity.NodeType.ToIDAndDisplayName(),
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
                Patches = entity.ChildDocuments.OrderBy(x => x.Name).Select(x => x.ToIDAndName()).ToList(),
                ID = entity.ID
            };

            return viewModel;
        }

        public static CurrentPatchItemViewModel ToCurrentPatchViewModel(this Document document)
        {
            if (document == null) throw new NullException(() => document);

            var viewModel = new CurrentPatchItemViewModel
            {
                ChildDocumentID = document.ID,
                Name = document.Name
            };

            return viewModel;
        }

        // Patch

        public static PatchViewModel ToViewModel(this Patch entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new PatchViewModel
            {
                PatchID = entity.ID
            };

            return viewModel;
        }

        public static OperatorViewModel ToViewModel(
            this Operator entity, 
            ISampleRepository sampleRepository, ICurveRepository curveRepository, IPatchRepository patchRepository, 
            EntityPositionManager entityPositionManager)
        {
            if (entity == null) throw new NullException(() => entity);
            if (entityPositionManager == null) throw new NullException(() => entityPositionManager);

            var viewModel = new OperatorViewModel();

            ViewModelHelper.RefreshViewModel_WithoutEntityPosition(entity, viewModel, sampleRepository, curveRepository, patchRepository);

            EntityPosition entityPosition = entityPositionManager.GetOrCreateOperatorPosition(entity.ID);
            viewModel.EntityPositionID = entityPosition.ID;
            viewModel.CenterX = entityPosition.X;
            viewModel.CenterY = entityPosition.Y;

            return viewModel;
        }

        public static IList<InletViewModel> ToViewModels(this IList<Inlet> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            IList<InletViewModel> viewModels = entities.Where(x => ViewModelHelper.MustConvertToInletViewModel(x))
                                                       .Select(x => x.ToViewModel())
                                                       .OrderBy(x => x.ListIndex)
                                                       .ToList();
            return viewModels;
        }

        public static InletViewModel ToViewModel(this Inlet entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new InletViewModel();
            entity.ToViewModel(viewModel);

            return viewModel;
        }

        /// <summary> Overload for in-place refreshing of a view model </summary>
        public static void ToViewModel(this Inlet entity, InletViewModel viewModel)
        {
            if (entity == null) throw new NullException(() => entity);
            if (viewModel == null) throw new NullException(() => viewModel);
            if (!ViewModelHelper.MustConvertToInletViewModel(entity)) throw new MustNotConvertToInletViewModelException(entity);

            viewModel.ID = entity.ID;
            viewModel.Name = entity.Name;
            viewModel.ListIndex = entity.ListIndex;
            viewModel.DefaultValue = entity.DefaultValue;

            if (entity.InletType != null)
            {
                viewModel.InletType = entity.InletType.ToIDAndDisplayName();
            }
            else
            {
                viewModel.InletType = null;
            }
        }

        public static IList<OutletViewModel> ToViewModels(this IList<Outlet> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            IList<OutletViewModel> viewModels = entities.Where(x => ViewModelHelper.MustConvertToOutletViewModel(x))
                                                        .Select(x => x.ToViewModel())
                                                        .OrderBy(x => x.ListIndex)
                                                        .ToList();
            return viewModels;
        }

        public static OutletViewModel ToViewModel(this Outlet entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new OutletViewModel();
            entity.ToViewModel(viewModel);
            return viewModel;
        }

        /// <summary> Overload for in-place refreshing of a view model. </summary>
        public static void ToViewModel(this Outlet entity, OutletViewModel viewModel)
        {
            if (entity == null) throw new NullException(() => entity);
            if (viewModel == null) throw new NullException(() => viewModel);
            if (!ViewModelHelper.MustConvertToOutletViewModel(entity)) throw new MustNotConvertToOutletViewModelException(entity);

            viewModel.ID = entity.ID;
            viewModel.Name = entity.Name;
            viewModel.ListIndex = entity.ListIndex;

            if (entity.OutletType != null)
            {
                viewModel.OutletType = entity.OutletType.ToIDAndDisplayName();
            }
            else
            {
                viewModel.OutletType = null;
            }
        }

        public static OperatorTypeViewModel ToViewModel(this OperatorType operatorType)
        {
            if (operatorType == null) throw new NullException(() => operatorType);

            var viewModel = new OperatorTypeViewModel
            {
                ID = operatorType.ID,
                DisplayName = ResourceHelper.GetPropertyDisplayName(operatorType.Name)
            };

            return viewModel;
        }

        /// <summary>
        /// Includes its inlets and outlets.
        /// Also includes the inverse property OutletViewModel.Operator.
        /// That view model is one the few with an inverse property.
        /// </summary>
        public static OperatorViewModel ToViewModelWithRelatedEntitiesAndInverseProperties(
            this Operator op,
            ISampleRepository sampleRepository, ICurveRepository curveRepository, IPatchRepository patchRepository,
            EntityPositionManager entityPositionManager)
        {
            if (op == null) throw new NullException(() => op);
            if (entityPositionManager == null) throw new NullException(() => entityPositionManager);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (patchRepository == null) throw new NullException(() => patchRepository);

            OperatorViewModel operatorViewModel = op.ToViewModel(sampleRepository, curveRepository, patchRepository, entityPositionManager);
            operatorViewModel.Inlets = op.Inlets.ToViewModels();
            operatorViewModel.Outlets = op.Outlets.ToViewModels();

            // This is the inverse property in the view model!
            foreach (OutletViewModel outletViewModel in operatorViewModel.Outlets)
            {
                outletViewModel.Operator = operatorViewModel;
            }

            return operatorViewModel;
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
                OriginalLocation = entity.OriginalLocation
            };

            viewModel.Bytes = bytes;
            viewModel.Duration = entity.GetDuration(bytes);

            if (entity.AudioFileFormat != null)
            {
                viewModel.AudioFileFormat = entity.AudioFileFormat.ToIDAndDisplayName();
            }

            if (entity.SampleDataType != null)
            {
                viewModel.SampleDataType = entity.SampleDataType.ToIDAndDisplayName();
            }

            if (entity.SpeakerSetup != null)
            {
                viewModel.SpeakerSetup = entity.SpeakerSetup.ToIDAndDisplayName();
            }

            if (entity.InterpolationType != null)
            {
                viewModel.InterpolationType = entity.InterpolationType.ToIDAndDisplayName();
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
                BaseFrequency = entity.BaseFrequency,
                Name = entity.Name,
                ScaleType = new IDAndName()
            };

            if (entity.ScaleType != null)
            {
                viewModel.ScaleType = entity.ScaleType.ToIDAndName();
            }

            return viewModel;
        }

        public static ToneViewModel ToViewModel(this Tone entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new ToneViewModel
            {
                 ID = entity.ID,
                 Number = entity.Number.ToString(),
                 Octave = entity.Octave.ToString()
            };

            return viewModel;
        }
    }
}