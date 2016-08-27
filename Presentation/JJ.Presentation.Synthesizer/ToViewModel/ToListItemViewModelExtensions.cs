using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Canonical;

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

            // TODO: Do this with Enums.
            if (entity.AudioFileFormat != null)
            {
                viewModel.AudioFileFormat = PropertyDisplayNames.ResourceManager.GetString(entity.AudioFileFormat.Name);
            }

            if (entity.SampleDataType != null)
            {
                viewModel.SampleDataType = PropertyDisplayNames.ResourceManager.GetString(entity.SampleDataType.Name);
            }

            if (entity.SpeakerSetup != null)
            {
                viewModel.SpeakerSetup = PropertyDisplayNames.ResourceManager.GetString(entity.SpeakerSetup.Name);
            }

            return viewModel;
        }

        // Curves

        public static IList<CurveListItemViewModel> ToListItemViewModels(this IList<Curve> entities)
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

            var viewModel = new CurveListItemViewModel
            {
                ID = entity.ID,
                Name = entity.Name
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

            // TODO: Do this with enums.
            if (entity.SampleDataType != null)
            {
                viewModel.SampleDataType = PropertyDisplayNames.ResourceManager.GetString(entity.SampleDataType.Name);
            }

            if (entity.SpeakerSetup != null)
            {
                viewModel.SpeakerSetup = PropertyDisplayNames.ResourceManager.GetString(entity.SpeakerSetup.Name);
            }

            return viewModel;
        }
    }
}