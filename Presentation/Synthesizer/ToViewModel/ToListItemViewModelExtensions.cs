using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Presentation.Synthesizer.ViewModels.Items;
// ReSharper disable MemberCanBePrivate.Global

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static class ToListItemViewModelExtensions
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
                viewModel.AudioFileFormat = ResourceFormatter.GetDisplayName(entity.AudioFileFormat.Name);
            }

            if (entity.SampleDataType != null)
            {
                viewModel.SampleDataType = ResourceFormatter.GetDisplayName(entity.SampleDataType.Name);
            }

            if (entity.SpeakerSetup != null)
            {
                viewModel.SpeakerSetup = ResourceFormatter.GetDisplayName(entity.SpeakerSetup.Name);
            }

            return viewModel;
        }

        // Libraries

        public static IList<IDAndName> ToIDAndNameList(IList<Document> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            IList<IDAndName> viewModels = entities.Select(x => x.ToIDAndName())
                                                  .OrderBy(x => x.Name)
                                                  .ToList();

            return viewModels;
        }
    }
}