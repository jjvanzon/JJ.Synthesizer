using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    public static class ToListItemViewModelExtensions
    {
        // AudioFileOutput

        public static IList<AudioFileOutputListItemViewModel> ToListItemViewModels(this IList<AudioFileOutput> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            IList<AudioFileOutputListItemViewModel> viewModels = entities.OrderBy(x => x.Name)
                                                                         .Select(x => x.ToListItemViewModel())
                                                                         .ToList();
            return viewModels;
        }

        public static AudioFileOutputListItemViewModel ToListItemViewModel(this AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new AudioFileOutputListItemViewModel
            {
                Name = entity.Name,
                SamplingRate = entity.SamplingRate,
                ID = entity.ID
            };

            if (entity.AudioFileFormat != null)
            {
                viewModel.AudioFileFormat = entity.AudioFileFormat.Name;
            }

            if (entity.SampleDataType != null)
            {
                viewModel.SampleDataType = entity.SampleDataType.Name;
            }

            if (entity.SpeakerSetup != null)
            {
                viewModel.SpeakerSetup = entity.SpeakerSetup.Name;
            }

            return viewModel;
        }

        // Curve

        public static IList<CurveListItemViewModel> ToListItemViewModels(this IList<Curve> entities, int documentID)
        {
            if (entities == null) throw new NullException(() => entities);

            IList<CurveListItemViewModel> viewModels = entities.OrderBy(x => x.Name)
                                                               .Select(x => x.ToListItemViewModel())
                                                               .ToList();
            return viewModels;
        }

        public static CurveListItemViewModel ToListItemViewModel(this Curve entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new CurveListItemViewModel
            {
                Name = entity.Name,
                ID = entity.ID
            };
        }

        // ChildDocument

        public static IList<ChildDocumentListItemViewModel> ToChildDocumentListItemsViewModel(this IList<Document> sourceEntities)
        {
            if (sourceEntities == null) throw new NullException(() => sourceEntities);

            IList<ChildDocumentListItemViewModel> destList = sourceEntities.OrderBy(x => x.Name)
                                                                           .Select(x => x.ToChildDocumentListItemViewModel())
                                                                           .ToList();
            return destList;
        }

        public static ChildDocumentListItemViewModel ToChildDocumentListItemViewModel(this Document entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new ChildDocumentListItemViewModel
            {
                Name = entity.Name,
                ID = entity.ID,
            };
        }

        // Patch

        public static IList<PatchListItemViewModel> ToListItemViewModels(this IList<Patch> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            IList<PatchListItemViewModel> viewModels = entities.OrderBy(x => x.Name)
                                                               .Select(x => x.ToListItemViewModel())
                                                               .ToList();
            return viewModels;
        }

        public static PatchListItemViewModel ToListItemViewModel(this Patch entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new PatchListItemViewModel
            {
                Name = entity.Name,
                ID = entity.ID
            };

            return viewModel;
        }

        // Samples

        public static IList<SampleListItemViewModel> ToListItemViewModels(this IList<Sample> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            IList<SampleListItemViewModel> viewModels = entities.OrderBy(x => x.Name)
                                                                .Select(x => x.ToListItemViewModel())
                                                                .ToList();
            return viewModels;
        }

        public static SampleListItemViewModel ToListItemViewModel(this Sample entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new SampleListItemViewModel
            {
                IsActive = entity.IsActive,
                Name = entity.Name,
                SamplingRate = entity.SamplingRate,
                ID = entity.ID
            };

            if (entity.AudioFileFormat != null)
            {
                viewModel.AudioFileFormat = entity.AudioFileFormat.Name;
            }

            if (entity.SampleDataType != null)
            {
                viewModel.SampleDataType = entity.SampleDataType.Name;
            }

            if (entity.SpeakerSetup != null)
            {
                viewModel.SpeakerSetup = entity.SpeakerSetup.Name;
            }

            return viewModel;
        }
    }
}